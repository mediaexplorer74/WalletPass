// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RatingItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A Control-derived class used by the Rating control to
  /// uniquely identify the objects used by the collections
  /// that contain the items displayed by the control.
  /// </summary>
  /// <remarks>
  /// This control exists to allow for custom ControlTemplates
  /// to be created to override the default visual style of the
  /// Rating control items.
  /// </remarks>
  public class RatingItem : Control
  {
    /// <summary>Identifies the StrokeThickness dependency property.</summary>
    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThinkness", typeof (double), typeof (RatingItem), (PropertyMetadata) null);

    /// <summary>Initializes a new instance of the RatingItem type.</summary>
    public RatingItem() => this.DefaultStyleKey = (object) typeof (RatingItem);

    /// <summary>
    /// Gets or sets the value indicating the thickness of a stroke
    /// around the path object.
    /// </summary>
    /// <remarks>
    /// A control element was neccessary to allow for control templating,
    /// however the default implementation uses a path, this property was
    /// created because a good substitute for StrokeThickeness of a path,
    /// which is a double type, does not exist in the default Control class.
    /// </remarks>
    public double StrokeThickness
    {
      get => (double) ((DependencyObject) this).GetValue(RatingItem.StrokeThicknessProperty);
      set => ((DependencyObject) this).SetValue(RatingItem.StrokeThicknessProperty, (object) value);
    }
  }
}
