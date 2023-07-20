// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Registration
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using Microsoft.WindowsAzure.Messaging.Http;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.WindowsAzure.Messaging
{
  public class Registration
  {
    public const string NativeRegistrationName = "$Default";
    private ISet<string> tags = (ISet<string>) new HashSet<string>();

    public Registration(string channelUri) => this.ChannelUri = !string.IsNullOrWhiteSpace(channelUri) ? channelUri : throw new ArgumentNullException(nameof (channelUri));

    public Registration(string channelUri, IEnumerable<string> tags)
      : this(channelUri)
    {
      if (tags == null)
        return;
      this.tags = (ISet<string>) new HashSet<string>(tags);
    }

    internal Registration(string notificationHubPath, string channelUri, IEnumerable<string> tags)
      : this(channelUri, tags)
    {
      this.NotificationHubPath = !string.IsNullOrWhiteSpace(notificationHubPath) ? notificationHubPath : throw new ArgumentNullException(nameof (notificationHubPath));
    }

    internal Registration(XElement content)
    {
      this.RegistrationId = content != null ? content.GetElementValueAsString(nameof (RegistrationId)) : throw new ArgumentNullException(nameof (content));
      string elementValueAsString = content.GetElementValueAsString(nameof (Tags));
      HashSet<string> stringSet;
      if (elementValueAsString == null)
        stringSet = (HashSet<string>) null;
      else
        stringSet = new HashSet<string>((IEnumerable<string>) elementValueAsString.Split(','));
      this.tags = (ISet<string>) stringSet;
      this.ExpiresAt = content.GetElementValueAsDateTime("ExpirationTime");
      this.ETag = content.GetElementValueAsString(nameof (ETag));
      this.ChannelUri = content.GetElementValueAsString(nameof (ChannelUri));
    }

    public string RegistrationId { get; internal set; }

    public DateTime? ExpiresAt { get; internal set; }

    public string ETag { get; internal set; }

    public string ChannelUri { get; internal set; }

    public string NotificationHubPath { get; internal set; }

    public ISet<string> Tags
    {
      get => this.tags;
      set => this.tags = value;
    }

    internal virtual string Name => "$Default";

    internal virtual List<XElement> GetXElements()
    {
      List<XElement> xelements = new List<XElement>();
      if (!string.IsNullOrEmpty(this.RegistrationId))
        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "RegistrationId", (object) this.RegistrationId));
      if (this.tags != null && ((ICollection<string>) this.tags).Count > 0)
        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "Tags", (object) string.Join(",", (IEnumerable<string>) this.tags)));
      if (!string.IsNullOrEmpty(this.ChannelUri))
        xelements.Add(new XElement(HttpUtilities.serviceBusDef + "ChannelUri", (object) this.ChannelUri));
      return xelements;
    }

    internal virtual string ToXmlString() => ((object) new XElement(HttpUtilities.serviceBusDef + "MpnsRegistrationDescription", (object) this.GetXElements())).ToString();
  }
}
