// Microsoft.WindowsAzure.Messaging.LocalStorageManager

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Microsoft.WindowsAzure.Messaging
{
  internal class LocalStorageManager : IDisposable
  {
    internal const string PrimaryChannelId = "$Primary";
    internal const string StorageVersion = "v1.0.0";
    internal static IStaticLocalStorage StaticStorage;
    private string channelUri;
    private string KeyNameForVersion;
    private string KeyNameForChannelUri;
    private string KeyNameForRegistrations;
    private ConcurrentDictionary<string, StoredRegistrationEntry> registartions;
    private IDictionary<string, object> storageValues;
    private object flushLock = new object();

    public LocalStorageManager(string notificationHubPath)
    {
      this.KeyNameForVersion = !string.IsNullOrEmpty(notificationHubPath)
                ? 
                string.Format("{0}-Version",
                (object) notificationHubPath) 
                :
                throw new ArgumentNullException(notificationHubPath);

      this.KeyNameForChannelUri = string.Format("{0}-Channel", (object) notificationHubPath);
      this.KeyNameForRegistrations = string.Format("{0}-Registrations", (object) notificationHubPath);

    if (LocalStorageManager.StaticStorage != null)
    {
        this.storageValues = LocalStorageManager.StaticStorage.Settings;
        LocalStorageManager.StaticStorage.KeyNameForChannelUri = this.KeyNameForChannelUri;
        LocalStorageManager.StaticStorage.KeyNameForRegistrations = this.KeyNameForRegistrations;
        LocalStorageManager.StaticStorage.KeyNameForVersion = this.KeyNameForVersion;
    }
    else
    {
        this.storageValues = default;//(IDictionary<string, object>)IsolatedStorageSettings.ApplicationSettings;
    }
     
     this.ReadContent();
    }

    public string ChannelUri
    {
      get => this.channelUri;
      set
      {
        if (this.channelUri != null && this.channelUri.Equals(value))
          return;
        this.channelUri = value;
        this.Flush();
      }
    }

    public bool IsRefreshNeeded { get; private set; }

    public StoredRegistrationEntry GetRegistration(string registrationName)
    {
      StoredRegistrationEntry registrationEntry;
      return this.registartions.TryGetValue(registrationName, out registrationEntry) ? registrationEntry : (StoredRegistrationEntry) null;
    }

    public void DeleteAllRegistrations() => this.registartions.Clear();

    public bool DeleteRegistration(string registrationName)
    {
      if (!this.registartions.Remove(registrationName))
        return false;
      this.Flush();
      return true;
    }

    public bool DeleteRegistration(Registration registration)
    {
      IEnumerable<KeyValuePair<string, StoredRegistrationEntry>> source = Enumerable.Where<KeyValuePair<string, StoredRegistrationEntry>>((IEnumerable<KeyValuePair<string, StoredRegistrationEntry>>) this.registartions, (Func<KeyValuePair<string, StoredRegistrationEntry>, bool>) (v => v.Value.RegistrationId.Equals(registration.RegistrationId)));
      return source.Count<KeyValuePair<string, StoredRegistrationEntry>>() > 0 && this.DeleteRegistration(source.First<KeyValuePair<string, StoredRegistrationEntry>>().Key);
    }

    public void UpdateRegistration<T>(string registrationName, ref T registration) where T : Registration
    {
      if ((object) registration == null)
      {
        this.DeleteRegistration(registrationName);
      }
      else
      {
        StoredRegistrationEntry newReg = new StoredRegistrationEntry(registrationName, registration.RegistrationId, registration.ETag);
        this.registartions.AddOrUpdate(registrationName, newReg, (Func<string, StoredRegistrationEntry, StoredRegistrationEntry>) ((key, oldReg) => newReg));
        this.channelUri = registration.ChannelUri;
        this.Flush();
      }
    }

    public void UpdateRegistration(Registration registration)
    {
      IEnumerable<KeyValuePair<string, StoredRegistrationEntry>> source = Enumerable.Where<KeyValuePair<string, StoredRegistrationEntry>>((IEnumerable<KeyValuePair<string, StoredRegistrationEntry>>) this.registartions, (Func<KeyValuePair<string, StoredRegistrationEntry>, bool>) (v => v.Value.RegistrationId.Equals(registration.RegistrationId)));
      if (source.Count<KeyValuePair<string, StoredRegistrationEntry>>() > 0)
        this.UpdateRegistration<Registration>(source.First<KeyValuePair<string, StoredRegistrationEntry>>().Key, ref registration);
      else
        this.UpdateRegistration<Registration>(registration.Name, ref registration);
    }

    public void RefreshFinished(string channelUri)
    {
      this.ChannelUri = channelUri;
      this.IsRefreshNeeded = false;
    }

    public void Flush()
    {
      lock (this.flushLock)
      {
        LocalStorageManager.SetContent(this.storageValues, this.KeyNameForVersion, "v1.0.0");
        LocalStorageManager.SetContent(this.storageValues, this.KeyNameForChannelUri, this.channelUri);
        string str = string.Empty;
        if (this.registartions != null)
          str = string.Join(";", Enumerable.Select<KeyValuePair<string, StoredRegistrationEntry>, string>((IEnumerable<KeyValuePair<string, StoredRegistrationEntry>>) this.registartions, (Func<KeyValuePair<string, StoredRegistrationEntry>, string>) (v => v.Value.ToString())));
        LocalStorageManager.SetContent(this.storageValues, this.KeyNameForRegistrations, str);
        //IsolatedStorageSettings.ApplicationSettings.Save();
      }
    }

    internal static string ReadContent(IDictionary<string, object> values, string key) => values.ContainsKey(key) ? values[key] as string : string.Empty;

    internal static void SetContent(IDictionary<string, object> values, string key, string value)
    {
      if (values.ContainsKey(key))
        values[key] = (object) value;
      else
        values.Add(key, (object) value);
    }

    private void ReadContent()
    {
      this.registartions = new ConcurrentDictionary<string, StoredRegistrationEntry>();
      this.channelUri = LocalStorageManager.ReadContent(this.storageValues, this.KeyNameForChannelUri);
      if (!string.Equals(LocalStorageManager.ReadContent(this.storageValues, this.KeyNameForVersion), "v1.0.0", StringComparison.OrdinalIgnoreCase))
      {
        this.IsRefreshNeeded = true;
      }
      else
      {
        this.IsRefreshNeeded = false;
        string str1 = LocalStorageManager.ReadContent(this.storageValues, this.KeyNameForRegistrations);
        if (string.IsNullOrEmpty(str1))
          return;
        string str2 = str1;
        char[] chArray = new char[1]{ ';' };
        foreach (string str3 in str2.Split(chArray))
        {
          StoredRegistrationEntry reg = StoredRegistrationEntry.CreateFromString(str3);
          this.registartions.AddOrUpdate(reg.RegistrationName, reg, (Func<string, StoredRegistrationEntry, StoredRegistrationEntry>) ((key, oldReg) => reg));
        }
      }
    }

    public void Dispose()
    {
      if (this.registartions == null)
        return;
      this.registartions.Dispose();
      this.registartions = (ConcurrentDictionary<string, StoredRegistrationEntry>) null;
    }
  }
}
