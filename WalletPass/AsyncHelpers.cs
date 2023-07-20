﻿// Decompiled with JetBrains decompiler
// Type: WalletPass.AsyncHelpers
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WalletPass
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
      private bool done;
      private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
      private readonly Queue<Tuple<SendOrPostCallback, object>> items = new Queue<Tuple<SendOrPostCallback, object>>();

      public Exception InnerException { get; set; }

      public override void Send(SendOrPostCallback d, object state) => throw new NotSupportedException("We cannot send to our same thread");

      public override void Post(SendOrPostCallback d, object state)
      {
        lock (this.items)
          this.items.Enqueue(Tuple.Create<SendOrPostCallback, object>(d, state));
        this.workItemsWaiting.Set();
      }

      public void EndMessageLoop() => this.Post((SendOrPostCallback) (_ => this.done = true), (object) null);

      public void BeginMessageLoop()
      {
        while (!this.done)
        {
          Tuple<SendOrPostCallback, object> tuple = (Tuple<SendOrPostCallback, object>) null;
          lock (this.items)
          {
            if (this.items.Count > 0)
              tuple = this.items.Dequeue();
          }
          if (tuple != null)
          {
            tuple.Item1(tuple.Item2);
            if (this.InnerException != null)
              throw new AggregateException("AsyncHelpers.Run method threw an exception.", this.InnerException);
          }
          else
            this.workItemsWaiting.WaitOne();
        }
      }

      public override SynchronizationContext CreateCopy() => (SynchronizationContext) this;
    }
  }
}
