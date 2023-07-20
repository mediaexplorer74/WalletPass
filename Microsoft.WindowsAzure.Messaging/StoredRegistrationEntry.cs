// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.StoredRegistrationEntry
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

namespace Microsoft.WindowsAzure.Messaging
{
  internal class StoredRegistrationEntry
  {
    public StoredRegistrationEntry(string name, string id, string ETag)
    {
      this.RegistrationName = name;
      this.RegistrationId = id;
      this.ETag = ETag;
    }

    public string RegistrationName { get; set; }

    public string RegistrationId { get; set; }

    public string ETag { get; set; }

    public override string ToString() => string.Format("{0}:{1}:{2}", (object) this.RegistrationName, (object) this.RegistrationId, (object) this.ETag);

    public static StoredRegistrationEntry CreateFromString(string str)
    {
      string[] strArray = str.Split(':');
      return new StoredRegistrationEntry(strArray[0], strArray[1], strArray[2]);
    }
  }
}
