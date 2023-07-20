// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.IStaticLocalStorage
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Messaging
{
  internal interface IStaticLocalStorage
  {
    IDictionary<string, object> Settings { get; }

    string KeyNameForVersion { get; set; }

    string KeyNameForChannelUri { get; set; }

    string KeyNameForRegistrations { get; set; }
  }
}
