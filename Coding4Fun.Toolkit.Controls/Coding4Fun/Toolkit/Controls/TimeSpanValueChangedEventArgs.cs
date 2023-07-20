// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanValueChangedEventArgs
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls
{
  public class TimeSpanValueChangedEventArgs : ValueChangedEventArgs<TimeSpan>
  {
    public TimeSpanValueChangedEventArgs(TimeSpan? oldTimeSpanValue, TimeSpan? newTimeSpanValue)
      : base(oldTimeSpanValue, newTimeSpanValue)
    {
    }
  }
}
