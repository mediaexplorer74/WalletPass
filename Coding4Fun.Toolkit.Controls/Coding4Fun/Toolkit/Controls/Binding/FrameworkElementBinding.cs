// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Binding.FrameworkElementBinding
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls.Binding
{
  public class FrameworkElementBinding
  {
    public static readonly DependencyProperty ClipToBoundsProperty = DependencyProperty.RegisterAttached("ClipToBounds", typeof (bool), typeof (FrameworkElementBinding), new PropertyMetadata((object) false, new PropertyChangedCallback(FrameworkElementBinding.OnClipToBoundsPropertyChanged)));

    public static bool GetClipToBounds(DependencyObject obj) => (bool) obj.GetValue(FrameworkElementBinding.ClipToBoundsProperty);

    public static void SetClipToBounds(DependencyObject obj, bool value) => obj.SetValue(FrameworkElementBinding.ClipToBoundsProperty, (object) value);

    private static void OnClipToBoundsPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue == e.OldValue)
        return;
      FrameworkElementBinding.HandleClipToBoundsEventAppend((object) obj, (bool) e.NewValue);
    }

    private static void HandleClipToBoundsEventAppend(object sender, bool value)
    {
      if (!(sender is FrameworkElement element))
        return;
      FrameworkElementBinding.SetClippingBound(element, value);
      if (value)
      {
        element.Loaded += new RoutedEventHandler(FrameworkElementBinding.ClipToBoundsPropertyChanged);
        element.SizeChanged += new SizeChangedEventHandler(FrameworkElementBinding.ClipToBoundsPropertyChanged);
      }
      else
      {
        element.Loaded -= new RoutedEventHandler(FrameworkElementBinding.ClipToBoundsPropertyChanged);
        element.SizeChanged -= new SizeChangedEventHandler(FrameworkElementBinding.ClipToBoundsPropertyChanged);
      }
    }

    private static void ClipToBoundsPropertyChanged(object sender, RoutedEventArgs e)
    {
      if (!(sender is FrameworkElement element))
        return;
      FrameworkElementBinding.SetClippingBound(element, FrameworkElementBinding.GetClipToBounds((DependencyObject) element));
    }

    private static void SetClippingBound(FrameworkElement element, bool setClippingBound)
    {
      if (setClippingBound)
        ((UIElement) element).Clip = (Geometry) new RectangleGeometry()
        {
          Rect = new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight)
        };
      else
        ((UIElement) element).Clip = (Geometry) null;
    }
  }
}
