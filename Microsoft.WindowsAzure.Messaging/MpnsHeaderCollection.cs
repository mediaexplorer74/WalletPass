// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.MpnsHeaderCollection
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.WindowsAzure.Messaging
{
  [CollectionDataContract(Name = "MpnsHeaders", Namespace = "http://schemas.microsoft.com/netservices/2010/10/servicebus/connect", ItemName = "MpnsHeader", KeyName = "Header", ValueName = "Value")]
  public sealed class MpnsHeaderCollection : Dictionary<string, string>
  {
    public MpnsHeaderCollection()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }
  }
}
