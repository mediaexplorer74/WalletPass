// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ValueChangedEventArgs`1
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls
{
  public class ValueChangedEventArgs<T> : EventArgs where T : struct
  {
    public ValueChangedEventArgs(T? oldValue, T? newValue)
    {
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }

    public T? OldValue { get; private set; }

    public T? NewValue { get; private set; }
  }
}
