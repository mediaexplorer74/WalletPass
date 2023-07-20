// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ColorBaseControl
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public abstract class ColorBaseControl : Control
  {
    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof (Color), typeof (Color), typeof (ColorBaseControl), new PropertyMetadata(new PropertyChangedCallback(ColorBaseControl.OnColorChanged)));
    public static readonly DependencyProperty SolidColorBrushProperty = DependencyProperty.Register(nameof (SolidColorBrush), typeof (SolidColorBrush), typeof (ColorBaseControl), new PropertyMetadata((object) null));

    public event ColorBaseControl.ColorChangedHandler ColorChanged;

    public Color Color
    {
      get => (Color) ((DependencyObject) this).GetValue(ColorBaseControl.ColorProperty);
      set => ((DependencyObject) this).SetValue(ColorBaseControl.ColorProperty, (object) value);
    }

    public SolidColorBrush SolidColorBrush
    {
      get => (SolidColorBrush) ((DependencyObject) this).GetValue(ColorBaseControl.SolidColorBrushProperty);
      private set => ((DependencyObject) this).SetValue(ColorBaseControl.SolidColorBrushProperty, (object) value);
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ColorBaseControl colorBaseControl))
        return;
      colorBaseControl.UpdateLayoutBasedOnColor();
      colorBaseControl.SolidColorBrush = new SolidColorBrush((Color) e.NewValue);
    }

    protected internal virtual void UpdateLayoutBasedOnColor()
    {
    }

    protected internal void ColorChanging(Color color)
    {
      this.Color = color;
      this.SolidColorBrush = new SolidColorBrush(this.Color);
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, this.Color);
    }

    public delegate void ColorChangedHandler(object sender, Color color);
  }
}
