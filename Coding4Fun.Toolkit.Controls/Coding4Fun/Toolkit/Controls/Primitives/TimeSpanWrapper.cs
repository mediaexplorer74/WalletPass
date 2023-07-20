// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Primitives.TimeSpanWrapper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;

namespace Coding4Fun.Toolkit.Controls.Primitives
{
  public class TimeSpanWrapper : ValueWrapper<TimeSpan>
  {
    public override ValueWrapper<TimeSpan> CreateNew(TimeSpan? value) => (ValueWrapper<TimeSpan>) new TimeSpanWrapper(value.GetValueOrDefault(TimeSpan.FromMinutes(30.0)));

    public TimeSpan TimeSpan => this.Value;

    public string HourNumber => this.TimeSpan.Hours.ToString((IFormatProvider) CultureInfo.InvariantCulture);

    public string MinuteNumber => this.TimeSpan.Minutes.ToString((IFormatProvider) CultureInfo.InvariantCulture);

    public string SecondNumber => this.TimeSpan.Seconds.ToString((IFormatProvider) CultureInfo.InvariantCulture);

    public TimeSpanWrapper(TimeSpan timeSpan)
      : base(timeSpan)
    {
    }
  }
}
