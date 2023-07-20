// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.HourTimeSpanDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls
{
  public class HourTimeSpanDataSource : TimeSpanDataSource
  {
    public HourTimeSpanDataSource()
      : base(23, 1)
    {
    }

    public HourTimeSpanDataSource(int max, int step)
      : base(max, step)
    {
    }

    protected override TimeSpan? GetRelativeTo(TimeSpan relativeDate, int delta) => new TimeSpan?(new TimeSpan(this.ComputeRelativeTo(relativeDate.Hours, delta), relativeDate.Minutes, relativeDate.Seconds));
  }
}
