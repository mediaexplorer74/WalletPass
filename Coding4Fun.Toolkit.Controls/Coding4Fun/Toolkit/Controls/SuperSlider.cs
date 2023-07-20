// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.SuperSlider
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Binding;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Coding4Fun.Toolkit.Controls
{
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  public class SuperSlider : Control
  {
    private const string BackgroundRectangleName = "BackgroundRectangle";
    private const string ProgressRectangleName = "ProgressRectangle";
    private const string BodyName = "Body";
    private const string BarBodyName = "BarBody";
    private bool _isLayoutInit;
    protected Rectangle BackgroundRectangle;
    protected Rectangle ProgressRectangle;
    private MovementMonitor _monitor;
    public static readonly DependencyProperty BarHeightProperty = DependencyProperty.Register(nameof (BarHeight), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 24.0, new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));
    public static readonly DependencyProperty BarWidthProperty = DependencyProperty.Register(nameof (BarWidth), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 24.0, new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (SuperSlider), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty ThumbProperty = DependencyProperty.Register(nameof (Thumb), typeof (object), typeof (SuperSlider), new PropertyMetadata(new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));
    public static readonly DependencyProperty BackgroundSizeProperty = DependencyProperty.Register(nameof (BackgroundSize), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));
    public static readonly DependencyProperty ProgressSizeProperty = DependencyProperty.Register(nameof (ProgressSize), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(SuperSlider.OnValueChanged)));
    public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof (Minimum), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof (Maximum), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 10.0));
    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(nameof (StepFrequency), typeof (double), typeof (SuperSlider), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (SuperSlider), new PropertyMetadata((object) (Orientation) 1, new PropertyChangedCallback(SuperSlider.OnLayoutChanged)));

    public event RoutedPropertyChangedEventHandler<double> ValueChanged;

    public SuperSlider()
    {
      this.DefaultStyleKey = (object) typeof (SuperSlider);
      PreventScrollBinding.SetIsEnabled((DependencyObject) this, true);
      this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.SuperSlider_IsEnabledChanged);
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.SuperSlider_Loaded);
      ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.SuperSlider_SizeChanged);
    }

    private void SuperSlider_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e) => this.IsEnabledVisualStateUpdate();

    private void SuperSlider_Loaded(object sender, RoutedEventArgs e)
    {
      this._isLayoutInit = true;
      this.AdjustAndUpdateLayout();
      this.IsEnabledVisualStateUpdate();
    }

    private void SuperSlider_SizeChanged(object sender, SizeChangedEventArgs e) => this.AdjustAndUpdateLayout();

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this.BackgroundRectangle = this.GetTemplateChild("BackgroundRectangle") as Rectangle;
      this.ProgressRectangle = this.GetTemplateChild("ProgressRectangle") as Rectangle;
      if (this.GetTemplateChild("Body") is Grid templateChild)
      {
        this._monitor = new MovementMonitor();
        this._monitor.Movement += new EventHandler<MovementMonitorEventArgs>(this._monitor_Movement);
        this._monitor.MonitorControl((Panel) templateChild);
      }
      this.AdjustLayout();
    }

    public double BarHeight
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.BarHeightProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.BarHeightProperty, (object) value);
    }

    public double BarWidth
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.BarWidthProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.BarWidthProperty, (object) value);
    }

    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(SuperSlider.TitleProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.TitleProperty, (object) value);
    }

    public object Thumb
    {
      get => ((DependencyObject) this).GetValue(SuperSlider.ThumbProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.ThumbProperty, value);
    }

    public double BackgroundSize
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.BackgroundSizeProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.BackgroundSizeProperty, (object) value);
    }

    public double ProgressSize
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.ProgressSizeProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.ProgressSizeProperty, (object) value);
    }

    public double Value
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.ValueProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.ValueProperty, (object) value);
    }

    public double Minimum
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.MinimumProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.MinimumProperty, (object) value);
    }

    public double Maximum
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.MaximumProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.MaximumProperty, (object) value);
    }

    public double StepFrequency
    {
      get => (double) ((DependencyObject) this).GetValue(SuperSlider.StepProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.StepProperty, (object) value);
    }

    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(SuperSlider.OrientationProperty);
      set => ((DependencyObject) this).SetValue(SuperSlider.OrientationProperty, (object) value);
    }

    private void _monitor_Movement(object sender, MovementMonitorEventArgs e) => this.UpdateSampleBasedOnManipulation(e.X, e.Y);

    private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is SuperSlider superSlider) || e.NewValue == e.OldValue)
        return;
      superSlider.SyncValueAndPosition((double) e.NewValue, (double) e.OldValue);
    }

    private static void OnLayoutChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is SuperSlider superSlider) || e.NewValue == e.OldValue)
        return;
      superSlider.AdjustAndUpdateLayout();
    }

    private void UpdateSampleBasedOnManipulation(double x, double y)
    {
      double controlMax = this.GetControlMax();
      double num = (this.IsVertical() ? controlMax - y : x).CheckBound(controlMax);
      double minimum = this.Minimum;
      if (!controlMax.AlmostEquals(0.0))
        minimum += (this.Maximum - this.Minimum) * (num / controlMax);
      this.SyncValueAndPosition(minimum, this.Value);
    }

    private double GetControlMax() => !this.IsVertical() ? ((FrameworkElement) this).ActualWidth : ((FrameworkElement) this).ActualHeight;

    private void SyncValueAndPosition(double newValue, double oldValue)
    {
      if (!this._isLayoutInit)
        return;
      this._isLayoutInit = true;
      if (this.StepFrequency > 0.0)
      {
        double num1 = newValue % this.StepFrequency;
        double num2 = Math.Floor(newValue - num1);
        newValue = num1 < this.StepFrequency / 2.0 ? num2 : num2 + this.StepFrequency;
      }
      newValue = newValue.CheckBound(this.Minimum, this.Maximum);
      if (oldValue.AlmostEquals(newValue))
        return;
      this.Value = newValue;
      this.UpdateUserInterface();
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new RoutedPropertyChangedEventArgs<double>(oldValue, this.Value));
    }

    private void UpdateUserInterface()
    {
      double controlMax = this.GetControlMax();
      double offset = (this.Value - this.Minimum) / (this.Maximum - this.Minimum) * controlMax;
      bool isVert = this.IsVertical();
      SuperSlider.SetSizeBasedOnOrientation((FrameworkElement) this.ProgressRectangle, isVert, offset);
      if (!(this.Thumb is FrameworkElement thumb))
        return;
      double num1 = isVert ? thumb.ActualHeight : thumb.ActualWidth;
      double num2 = (offset - num1 / 2.0).CheckBound(controlMax - num1);
      thumb.Margin = isVert ? new Thickness(0.0, 0.0, 0.0, num2) : new Thickness(num2, 0.0, 0.0, 0.0);
    }

    private void AdjustAndUpdateLayout()
    {
      this.AdjustLayout();
      this.UpdateUserInterface();
    }

    private void AdjustLayout()
    {
      if (this.ProgressRectangle == null || this.BackgroundRectangle == null)
        return;
      bool isVert = this.IsVertical();
      if (this.GetTemplateChild("BarBody") is Grid templateChild)
      {
        if (isVert)
        {
          ((FrameworkElement) templateChild).Width = this.BarWidth;
          ((FrameworkElement) templateChild).Height = double.NaN;
        }
        else
        {
          ((FrameworkElement) templateChild).Width = double.NaN;
          ((FrameworkElement) templateChild).Height = this.BarHeight;
        }
      }
      SuperSlider.SetAlignment((FrameworkElement) this.ProgressRectangle, isVert);
      ((FrameworkElement) this.ProgressRectangle).Width = double.NaN;
      ((FrameworkElement) this.ProgressRectangle).Height = double.NaN;
      ((FrameworkElement) this.BackgroundRectangle).Width = double.NaN;
      ((FrameworkElement) this.BackgroundRectangle).Height = double.NaN;
      if (this.ProgressSize > 0.0)
        SuperSlider.SetSizeBasedOnOrientation((FrameworkElement) this.ProgressRectangle, !isVert, this.ProgressSize);
      if (this.BackgroundSize > 0.0)
        SuperSlider.SetSizeBasedOnOrientation((FrameworkElement) this.BackgroundRectangle, !isVert, this.BackgroundSize);
      if (this.Thumb == null)
        return;
      SuperSlider.SetAlignment(this.Thumb as FrameworkElement, isVert);
    }

    private void IsEnabledVisualStateUpdate() => VisualStateManager.GoToState((Control) this, this.IsEnabled ? "Normal" : "Disabled", true);

    private static void SetSizeBasedOnOrientation(
      FrameworkElement control,
      bool isVert,
      double offset)
    {
      if (control == null)
        return;
      if (isVert)
        control.Height = offset;
      else
        control.Width = offset;
    }

    private bool IsVertical() => this.Orientation == 0;

    private static void SetAlignment(FrameworkElement control, bool isVert)
    {
      if (control == null)
        return;
      control.HorizontalAlignment = isVert ? (HorizontalAlignment) 3 : (HorizontalAlignment) 0;
      control.VerticalAlignment = isVert ? (VerticalAlignment) 2 : (VerticalAlignment) 3;
    }
  }
}
