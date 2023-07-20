// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TimeSpanPickerPage
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Primitives;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Coding4Fun.Toolkit.Controls
{
  public class TimeSpanPickerPage : TimeSpanPickerBasePage
  {
    internal VisualStateGroup VisibilityStates;
    internal VisualState Open;
    internal VisualState Closed;
    internal PlaneProjection PlaneProjection;
    internal Rectangle SystemTrayPlaceholder;
    internal TextBlock HeaderTitle;
    internal LoopingSelector PrimarySelector;
    internal LoopingSelector SecondarySelector;
    internal LoopingSelector TertiarySelector;
    private bool _contentLoaded;

    public TimeSpanPickerPage() => this.InitializeComponent();

    public override void InitDataSource()
    {
      int seconds = this.StepFrequency.Seconds;
      this.TertiarySelector.DataSource = (ILoopingSelectorDataSource) new SecondTimeSpanDataSource(this.Maximum >= TimeSpan.FromMinutes(1.0) ? 60 : Math.Min(this.Maximum.Seconds + seconds, 60), seconds);
      int step1 = this.StepFrequency > TimeSpan.FromMinutes(1.0) ? this.StepFrequency.Minutes : 1;
      this.SecondarySelector.DataSource = (ILoopingSelectorDataSource) new MinuteTimeSpanDataSource(this.Maximum >= TimeSpan.FromHours(1.0) ? 60 : Math.Min(this.Maximum.Minutes + step1, 60), step1);
      int step2 = this.StepFrequency > TimeSpan.FromHours(1.0) ? this.StepFrequency.Hours : 1;
      this.PrimarySelector.DataSource = (ILoopingSelectorDataSource) new HourTimeSpanDataSource(this.Maximum >= TimeSpan.FromHours(24.0) ? 24 : this.Maximum.Hours + step2, step2);
      this.InitializeValuePickerPage(this.PrimarySelector, this.SecondarySelector, this.TertiarySelector);
    }

    protected override IEnumerable<LoopingSelector> GetSelectorsOrderedByCulturePattern() => ValuePickerBasePage<TimeSpan>.GetSelectorsOrderedByCulturePattern(CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern.ToUpperInvariant(), new char[3]
    {
      'H',
      'M',
      'S'
    }, new LoopingSelector[3]
    {
      this.PrimarySelector,
      this.SecondarySelector,
      this.TertiarySelector
    }).Where<LoopingSelector>((Func<LoopingSelector, bool>) (s => !s.DataSource.IsEmpty));

    protected virtual void OnOrientationChanged(OrientationChangedEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      base.OnOrientationChanged(e);
      ((UIElement) this.SystemTrayPlaceholder).Visibility = (1 & e.Orientation) != null ? (Visibility) 0 : (Visibility) 1;
    }

    public override void SetFlowDirection(FlowDirection flowDirection)
    {
      ((FrameworkElement) this.HeaderTitle).FlowDirection = flowDirection;
      ((FrameworkElement) this.PrimarySelector).FlowDirection = flowDirection;
      ((FrameworkElement) this.SecondarySelector).FlowDirection = flowDirection;
      ((FrameworkElement) this.TertiarySelector).FlowDirection = flowDirection;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Coding4Fun.Toolkit.Controls;component/ValuePicker/TimeSpanPicker/TimespanPickerPage.xaml", UriKind.Relative));
      this.VisibilityStates = (VisualStateGroup) ((FrameworkElement) this).FindName("VisibilityStates");
      this.Open = (VisualState) ((FrameworkElement) this).FindName("Open");
      this.Closed = (VisualState) ((FrameworkElement) this).FindName("Closed");
      this.PlaneProjection = (PlaneProjection) ((FrameworkElement) this).FindName("PlaneProjection");
      this.SystemTrayPlaceholder = (Rectangle) ((FrameworkElement) this).FindName("SystemTrayPlaceholder");
      this.HeaderTitle = (TextBlock) ((FrameworkElement) this).FindName("HeaderTitle");
      this.PrimarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("PrimarySelector");
      this.SecondarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("SecondarySelector");
      this.TertiarySelector = (LoopingSelector) ((FrameworkElement) this).FindName("TertiarySelector");
    }
  }
}
