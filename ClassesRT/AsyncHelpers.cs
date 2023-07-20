// Wallet_Pass.AsyncHelpers

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Wallet_Pass
{
  public static class AsyncHelpers
  {
    public static void RunSync(Func<Task> task)
    {
      SynchronizationContext current = SynchronizationContext.Current;
      AsyncHelpers.ExclusiveSynchronizationContext synch = new AsyncHelpers.ExclusiveSynchronizationContext();
      SynchronizationContext.SetSynchronizationContext((SynchronizationContext) synch);
      synch.Post((SendOrPostCallback) (async _ =>
      {
        try
        {
          await task();
        }
        catch (Exception ex)
        {
          synch.InnerException = ex;
          throw;
        }
        finally
        {
          synch.EndMessageLoop();
        }
      }), (object) null);
      synch.BeginMessageLoop();
      SynchronizationContext.SetSynchronizationContext(current);
    }

    public static T RunSync<T>(Func<Task<T>> task)
    {
      SynchronizationContext current = SynchronizationContext.Current;
      AsyncHelpers.ExclusiveSynchronizationContext synch = new AsyncHelpers.ExclusiveSynchronizationContext();
      SynchronizationContext.SetSynchronizationContext((SynchronizationContext) synch);
      T ret = default (T);
      // ISSUE: variable of a compiler-generated type
      AsyncHelpers.\u003C\u003Ec__DisplayClass7<T> cDisplayClass7;
      synch.Post((SendOrPostCallback) (async _ =>
      {
        try
        {
          // ISSUE: reference to a compiler-generated field
          ((AsyncHelpers.\u003C\u003Ec__DisplayClass7<T>) cDisplayClass7).ret = await task();
        }
        catch (Exception ex)
        {
          synch.InnerException = ex;
          throw;
        }
        finally
        {
          synch.EndMessageLoop();
        }
      }), (object) null);
      synch.BeginMessageLoop();
      SynchronizationContext.SetSynchronizationContext(current);
      return ret;
    }

    private class ExclusiveSynchronizationContext : SynchronizationContext
    {
      private readonly Queue<Tuple<SendOrPostCallback, object>> _items = new Queue<Tuple<SendOrPostCallback, object>>();
      private readonly AutoResetEvent _workItemsWaiting = new AutoResetEvent(false);
      private bool done;

      public Exception InnerException { get; set; }

      public override void Send(SendOrPostCallback d, object state) => throw new NotSupportedException("We cannot send to our same thread");

      public override void Post(SendOrPostCallback d, object state)
      {
        lock (this._items)
          this._items.Enqueue(Tuple.Create<SendOrPostCallback, object>(d, state));
        this._workItemsWaiting.Set();
      }

      public void EndMessageLoop() => this.Post((SendOrPostCallback) (_ => this.done = true), (object) null);

      public void BeginMessageLoop()
      {
        while (!this.done)
        {
          Tuple<SendOrPostCallback, object> tuple = (Tuple<SendOrPostCallback, object>) null;
          lock (this._items)
          {
            if (this._items.Count > 0)
              tuple = this._items.Dequeue();
          }
          if (tuple != null)
          {
            tuple.Item1(tuple.Item2);
            if (this.InnerException != null)
              throw new AggregateException("AsyncHelpers.Run method threw an exception.", this.InnerException);
          }
          else
            this._workItemsWaiting.WaitOne();
        }
      }

    public override SynchronizationContext CreateCopy()
    {
        return (SynchronizationContext)this;
    }
}
  }
}
