// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.ClipToBounds
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Provides an attachable property that clips a FrameworkElement
  /// to its bounds, e.g. clip the element when it is translated outside
  /// of the panel it is placed in.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public class ClipToBounds : DependencyObject
  {
    /// <summary>Identifies the IsEnabled dependency property.</summary>
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof (bool), typeof (ClipToBounds), new PropertyMetadata((object) false, new PropertyChangedCallback(ClipToBounds.OnIsEnabledPropertyChanged)));

    /// <summary>
    /// Gets the IsEnabled dependency property from an object.
    /// </summary>
    /// <param name="obj">The object to get the value from.</param>
    /// <returns>The property's value.</returns>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static bool GetIsEnabled(DependencyObject obj) => (bool) obj.GetValue(ClipToBounds.IsEnabledProperty);

    /// <summary>Sets the IsEnabled dependency property on an object.</summary>
    /// <param name="obj">The object to set the value on.</param>
    /// <param name="value">The value to set.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(ClipToBounds.IsEnabledProperty, (object) value);

    /// <summary>Attaches or detaches the SizeChanged event handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsEnabledPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(obj is FrameworkElement frameworkElement))
        return;
      if ((bool) e.NewValue)
        frameworkElement.SizeChanged += new SizeChangedEventHandler(ClipToBounds.OnSizeChanged);
      else
        frameworkElement.SizeChanged -= new SizeChangedEventHandler(ClipToBounds.OnSizeChanged);
    }

    /// <summary>
    /// Clips the associated object to a rectangular geometry determined
    /// by its actual width and height.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (!(sender is FrameworkElement frameworkElement))
        return;
      ((UIElement) frameworkElement).Clip = (Geometry) new RectangleGeometry()
      {
        Rect = new Rect(0.0, 0.0, frameworkElement.ActualWidth, frameworkElement.ActualHeight)
      };
    }
  }
}
