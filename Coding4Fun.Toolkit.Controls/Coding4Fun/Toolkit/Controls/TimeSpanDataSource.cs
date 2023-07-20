// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls
{
  public abstract class TimeSpanDataSource : DataSource<TimeSpan>
  {
    protected int Max;
    protected int Step;

    protected TimeSpanDataSource(int max, int step)
    {
      this.Max = max;
      this.Step = step;
    }

    public override bool IsEmpty => this.Max - 1 == 0 || this.Step == 0;

    protected int ComputeRelativeTo(int value, int delta)
    {
      int max = this.Max;
      return max <= 0 ? value : (max + value + delta * this.Step) % max + (max <= value ? max : 0);
    }
  }
}
