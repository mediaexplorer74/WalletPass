// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.ListWithContinuation`1
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System.Collections.Generic;

namespace Microsoft.WindowsAzure.Messaging
{
  internal sealed class ListWithContinuation<T> : List<T>
  {
    public string ContinuationToken { get; private set; }

    public ListWithContinuation(string continuationToken = null) => this.ContinuationToken = continuationToken;

    public ListWithContinuation(int capacity, string continuationToken = null)
      : base(capacity)
    {
      this.ContinuationToken = continuationToken;
    }

    public ListWithContinuation(IEnumerable<T> collection, string continuationToken = null)
      : base(collection)
    {
      this.ContinuationToken = continuationToken;
    }
  }
}
