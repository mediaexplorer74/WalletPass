// Decompiled with JetBrains decompiler
// Type: WPControls.MonthChangedEventArgs
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System;

namespace WPControls
{
  public class MonthChangedEventArgs : EventArgs
  {
    private MonthChangedEventArgs()
    {
    }

    internal MonthChangedEventArgs(int year, int month)
    {
      this.Year = year;
      this.Month = month;
    }

    public int Year { get; private set; }

    public int Month { get; private set; }
  }
}
