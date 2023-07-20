// Microsoft.WindowsAzure.Messaging.Entity

using System;

namespace Microsoft.WindowsAzure.Messaging
{
  public abstract class Entity : IDisposable
  {
    protected volatile bool disposed;

    protected Entity(string connectionString)
    {
        this.Connection = connectionString;
    }

    public string Connection { get; private set; }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed && disposing)
        this.OnInternalDispose();
      this.disposed = true;
    }

    protected void ThrowIfDisposed()
    {
      if (this.disposed)
        throw new ObjectDisposedException("ObjectDisposedMessage");
    }

    protected abstract void OnInternalDispose();
  }
}
