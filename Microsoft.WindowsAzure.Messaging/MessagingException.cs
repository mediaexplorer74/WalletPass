// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.MessagingException
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;

namespace Microsoft.WindowsAzure.Messaging
{
  public class MessagingException : Exception
  {
    public MessagingException(string message)
      : base(message)
    {
      this.Initialize(DateTime.UtcNow);
    }

    public MessagingException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.Initialize(DateTime.UtcNow);
    }

    public DateTime Timestamp { get; private set; }

    public bool IsTransient { get; protected set; }

    private void Initialize(DateTime timestamp)
    {
      this.IsTransient = true;
      this.Timestamp = timestamp;
    }
  }
}
