﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.UpdateChannelUriPayload
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System.Runtime.Serialization;

namespace Microsoft.WindowsAzure.Messaging
{
  [DataContract(Name = "UpdatePnsHandlePayload")]
  internal class UpdateChannelUriPayload
  {
    [DataMember(Name = "NewPnsHandle")]
    public string NewChannelUri { get; set; }

    [DataMember(Name = "OriginalPnsHandle")]
    public string OriginalChannelUri { get; set; }
  }
}
