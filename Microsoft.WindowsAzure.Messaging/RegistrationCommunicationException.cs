﻿// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.RegistrationCommunicationException
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;

namespace Microsoft.WindowsAzure.Messaging
{
  public sealed class RegistrationCommunicationException : RegistrationException
  {
    public RegistrationCommunicationException(string message)
      : base(message)
    {
    }

    public RegistrationCommunicationException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}