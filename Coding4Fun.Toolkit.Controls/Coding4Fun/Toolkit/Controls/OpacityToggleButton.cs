// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.OpacityToggleButton
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class OpacityToggleButton : ToggleButtonBase
  {
    public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(nameof (AnimationDuration), typeof (Duration), typeof (OpacityToggleButton), new PropertyMetadata((object) new Duration(TimeSpan.FromMilliseconds(100.0))));
    public static readonly DependencyProperty UncheckedOpacityProperty = DependencyProperty.Register(nameof (UncheckedOpacity), typeof (double), typeof (OpacityToggleButton), new PropertyMetadata((object) 0.5));
    public static readonly DependencyProperty CheckedOpacityProperty = DependencyProperty.Register(nameof (CheckedOpacity), typeof (double), typeof (OpacityToggleButton), new PropertyMetadata((object) 1.0));

    public OpacityToggleButton() => ((Control) this).DefaultStyleKey = (object) typeof (OpacityToggleButton);

    public Duration AnimationDuration
    {
      get => (Duration) ((DependencyObject) this).GetValue(OpacityToggleButton.AnimationDurationProperty);
      set => ((DependencyObject) this).SetValue(OpacityToggleButton.AnimationDurationProperty, (object) value);
    }

    public double UncheckedOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(OpacityToggleButton.UncheckedOpacityProperty);
      set => ((DependencyObject) this).SetValue(OpacityToggleButton.UncheckedOpacityProperty, (object) value);
    }

    public double CheckedOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(OpacityToggleButton.CheckedOpacityProperty);
      set => ((DependencyObject) this).SetValue(OpacityToggleButton.CheckedOpacityProperty, (object) value);
    }
  }
}
