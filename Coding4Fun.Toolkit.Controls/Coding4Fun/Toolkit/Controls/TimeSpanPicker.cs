// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanPicker
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Primitives;
using System;
using System.Globalization;
using System.Windows;

namespace Coding4Fun.Toolkit.Controls
{
  public class TimeSpanPicker : ValuePickerBase<TimeSpan>
  {
    public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(nameof (Maximum), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.FromHours(24.0), new PropertyChangedCallback(TimeSpanPicker.OnLimitsChanged)));
    public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof (Minimum), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.Zero, new PropertyChangedCallback(TimeSpanPicker.OnLimitsChanged)));
    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof (StepFrequency), typeof (TimeSpan), typeof (ValuePickerBase<TimeSpan>), new PropertyMetadata((object) TimeSpan.FromSeconds(1.0)));

    public TimeSpanPicker()
    {
      this.DefaultStyleKey = (object) typeof (TimeSpanPicker);
      this.Value = new TimeSpan?(TimeSpan.FromMinutes(30.0));
      this.DialogTitle = ValuePickerResources.TimeSpanPickerTitle;
    }

    protected internal override void UpdateValueString()
    {
      if (this.Value.HasValue)
      {
        TimeSpan time = this.Value.Value;
        if (!string.IsNullOrEmpty(this.ValueStringFormat))
        {
          this.ValueString = Coding4Fun.Toolkit.Controls.Common.TimeSpanFormat.Format(time, this.ValueStringFormat);
          return;
        }
      }
      this.ValueString = string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.ValueStringFormat ?? this.ValueStringFormatFallback, new object[1]
      {
        (object) this.Value
      });
    }

    public TimeSpan Maximum
    {
      get => (TimeSpan) ((DependencyObject) this).GetValue(TimeSpanPicker.MaxProperty);
      set => ((DependencyObject) this).SetValue(TimeSpanPicker.MaxProperty, (object) value);
    }

    public TimeSpan Minimum
    {
      get => (TimeSpan) ((DependencyObject) this).GetValue(TimeSpanPicker.MinProperty);
      set => ((DependencyObject) this).SetValue(TimeSpanPicker.MinProperty, (object) value);
    }

    private static void OnLimitsChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is TimeSpanPicker timeSpanPicker))
        return;
      timeSpanPicker.ValidateBounds();
    }

    private void ValidateBounds()
    {
      if (this.Minimum < TimeSpan.Zero)
        this.Minimum = TimeSpan.Zero;
      if (this.Maximum > TimeSpan.MaxValue)
        this.Maximum = TimeSpan.MaxValue;
      if (this.Maximum < this.Minimum)
        this.Maximum = this.Minimum;
      if (this.Value.HasValue)
        this.Value = new TimeSpan?(this.Value.Value.CheckBound(this.Minimum, this.Maximum));
      else
        this.Value = new TimeSpan?(this.Minimum);
    }

    public TimeSpan StepFrequency
    {
      get => (TimeSpan) ((DependencyObject) this).GetValue(TimeSpanPicker.StepProperty);
      set => ((DependencyObject) this).SetValue(TimeSpanPicker.StepProperty, (object) value);
    }

    protected override void NavigateToNewPage(object page)
    {
      if (page is ITimeSpanPickerPage<TimeSpan> timeSpanPickerPage)
      {
        timeSpanPickerPage.Minimum = this.Minimum;
        timeSpanPickerPage.Maximum = this.Maximum;
        timeSpanPickerPage.StepFrequency = this.StepFrequency;
      }
      base.NavigateToNewPage(page);
    }
  }
}
