﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.SafeDispatcher
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public class SafeDispatcher
  {
    public static async Task Run(Action func)
    {
      Dispatcher currentDispatcher = ApplicationSpace.CurrentDispatcher;
      if (currentDispatcher == null)
        return;
      if (!currentDispatcher.CheckAccess())
        currentDispatcher.BeginInvoke(func);
      else
        func();
    }

    public static async Task<T> Run<T>(Func<T> func)
    {
      T returnData = default (T);
      Dispatcher currentDispatcher = ApplicationSpace.CurrentDispatcher;
      if (currentDispatcher == null)
        return returnData;
      if (!currentDispatcher.CheckAccess())
      {
        AutoResetEvent holdMutex = new AutoResetEvent(true);
        currentDispatcher.BeginInvoke((Action) (() =>
        {
          returnData = func();
          holdMutex.Set();
        }));
        holdMutex.Reset();
        holdMutex.WaitOne();
      }
      else
        returnData = func();
      return returnData;
    }
  }
}
