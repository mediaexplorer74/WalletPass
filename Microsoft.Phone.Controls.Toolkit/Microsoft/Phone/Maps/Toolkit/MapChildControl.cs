// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapChildControl
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System.ComponentModel;
using System.Device.Location;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Represents a child of a map, which uses geographic coordinates to position itself.
  /// </summary>
  [ContentProperty("Content")]
  public class MapChildControl : ContentControl
  {
    /// <summary>
    /// Identifies the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapChildControl.GeoCoordinate" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty GeoCoordinateProperty = DependencyProperty.Register(nameof (GeoCoordinate), typeof (GeoCoordinate), typeof (MapChildControl), new PropertyMetadata(new PropertyChangedCallback(MapChildControl.OnGeoCoordinateChangedCallback)));
    /// <summary>
    /// Identifies the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapChildControl.PositionOrigin" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty PositionOriginProperty = DependencyProperty.Register(nameof (PositionOrigin), typeof (Point), typeof (MapChildControl), new PropertyMetadata(new PropertyChangedCallback(MapChildControl.OnPositionOriginChangedCallback)));

    /// <summary>
    /// Gets or sets the geographic coordinate of the control on the map.
    /// </summary>
    [TypeConverter(typeof (GeoCoordinateConverter))]
    public GeoCoordinate GeoCoordinate
    {
      get => (GeoCoordinate) ((DependencyObject) this).GetValue(MapChildControl.GeoCoordinateProperty);
      set => ((DependencyObject) this).SetValue(MapChildControl.GeoCoordinateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the position origin of the control, which defines the position on the control to anchor to the map.
    /// </summary>
    public Point PositionOrigin
    {
      get => (Point) ((DependencyObject) this).GetValue(MapChildControl.PositionOriginProperty);
      set => ((DependencyObject) this).SetValue(MapChildControl.PositionOriginProperty, (object) value);
    }

    /// <summary>
    /// Callback method on object when GeoCoordinate is changed.
    /// </summary>
    /// <param name="d">dependency object</param>
    /// <param name="e">event args</param>
    private static void OnGeoCoordinateChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      d.SetValue(MapChild.GeoCoordinateProperty, e.NewValue);
    }

    /// <summary>
    /// Callback method on object when PositionOrigin is changed.
    /// </summary>
    /// <param name="d">dependency object</param>
    /// <param name="e">event args</param>
    private static void OnPositionOriginChangedCallback(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      d.SetValue(MapChild.PositionOriginProperty, e.NewValue);
    }
  }
}
