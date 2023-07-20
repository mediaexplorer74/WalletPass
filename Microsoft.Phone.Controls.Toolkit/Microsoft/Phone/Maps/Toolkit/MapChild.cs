// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapChild
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System;
using System.ComponentModel;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Represents a child of a map, which uses geographic coordinates to position itself.
  /// </summary>
  public static class MapChild
  {
    /// <summary>
    /// Gets or sets the child's geographic coordinate position
    /// </summary>
    public static readonly DependencyProperty GeoCoordinateProperty = DependencyProperty.RegisterAttached("GeoCoordinate", typeof (object), typeof (MapChild), (PropertyMetadata) null);
    /// <summary>Gets or sets the child's position origin.</summary>
    public static readonly DependencyProperty PositionOriginProperty = DependencyProperty.RegisterAttached("PositionOrigin", typeof (Point), typeof (MapChild), (PropertyMetadata) null);
    /// <summary>
    /// Identifies the Microsoft.Phone.Maps.Toolkit.MapChild.ToolkitCreatedProperty attached dependency property.
    /// </summary>
    internal static readonly DependencyProperty ToolkitCreatedProperty = DependencyProperty.RegisterAttached("ToolkitCreated", typeof (bool), typeof (MapChild), (PropertyMetadata) null);

    /// <summary>Gets the geographic coordinate position of the child.</summary>
    /// <param name="element">The dependency object</param>
    /// <returns>Returns <see cref="T:System.Device.Location.GeoCoordinate" /></returns>
    [TypeConverter(typeof (GeoCoordinateConverter))]
    public static GeoCoordinate GetGeoCoordinate(DependencyObject element) => element != null ? (GeoCoordinate) element.GetValue(MapChild.GeoCoordinateProperty) : throw new ArgumentNullException(nameof (element));

    /// <summary>
    /// Sets the geographic coordinate position of the child.
    /// </summary>
    /// <param name="element">The dependency object</param>
    /// <param name="value">The coordinate to use to position the child</param>
    public static void SetGeoCoordinate(DependencyObject element, GeoCoordinate value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      element.SetValue(MapChild.GeoCoordinateProperty, (object) value);
    }

    /// <summary>Gets the position origin of the child.</summary>
    /// <param name="element">The dependency object</param>
    /// <returns>Returns <see cref="T:System.Device.Location.GeoCoordinate" /></returns>
    public static Point GetPositionOrigin(DependencyObject element) => element != null ? (Point) element.GetValue(MapChild.PositionOriginProperty) : throw new ArgumentNullException(nameof (element));

    /// <summary>Sets the position origin of the child.</summary>
    /// <param name="element">The dependency object</param>
    /// <param name="value">The position origin of the child</param>
    public static void SetPositionOrigin(DependencyObject element, Point value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      element.SetValue(MapChild.PositionOriginProperty, (object) value);
    }

    /// <summary>
    /// Creates a MapOverlay with the specified content and content template.
    /// It will have special setup so that later the dependency properties from MapOverlay
    /// and the attached properties from the target UI can be in a binding.
    /// </summary>
    /// <param name="content">Content of the MapOverlay</param>
    /// <param name="contentTemplate">Template to be used in the MapOverlay</param>
    /// <returns>The MapOverlay that was created</returns>
    internal static MapOverlay CreateMapOverlay(object content, DataTemplate contentTemplate)
    {
      MapOverlay mapOverlay = new MapOverlay();
      MapOverlayItem mapOverlayItem = new MapOverlayItem(content, contentTemplate, mapOverlay);
      ((DependencyObject) mapOverlay).SetValue(MapChild.ToolkitCreatedProperty, (object) true);
      mapOverlay.Content = (object) mapOverlayItem;
      return mapOverlay;
    }

    /// <summary>
    /// Method that takes care of setting the two way bindings for all the attached properties
    /// from MapOverlay to the actual UI (not the intermediary presenter).
    /// </summary>
    /// <remarks>
    /// Even though the MapOverlay is supposed to be the target, it is the source due to issues with
    /// setting a two way binding where the source is an attached property
    /// </remarks>
    /// <param name="mapOverlay">MapOverlay that will be used in the two way binding</param>
    internal static void BindMapOverlayProperties(MapOverlay mapOverlay) => MapChild.BindMapOverlayProperties(mapOverlay, ((DependencyObject) mapOverlay.Content).GetVisualChildren().FirstOrDefault<DependencyObject>() ?? throw new InvalidOperationException("Could not bind the properties because there was no UI"));

    /// <summary>
    /// Method that takes care of setting the two way bindings for all the attached properties
    /// from MapOverlay to the actual UI (not the intermediary presenter).
    /// </summary>
    /// <remarks>
    /// Even though the MapOverlay is supposed to be the target, it is the source due to issues with
    /// setting a two way binding where the source is an attached property
    /// </remarks>
    /// <param name="mapOverlay">MapOverlay that will be used in the two way binding</param>
    /// <param name="targetObject">Source object that will be used in the two way binding</param>
    internal static void BindMapOverlayProperties(
      MapOverlay mapOverlay,
      DependencyObject targetObject)
    {
      Debug.Assert(mapOverlay.GeoCoordinate == (GeoCoordinate) null, "Expected to have mapOverlay as null");
      mapOverlay.GeoCoordinate = (GeoCoordinate) targetObject.GetValue(MapChild.GeoCoordinateProperty);
      MapChild.BindMapOverlayProperty(mapOverlay, "GeoCoordinate", targetObject, MapChild.GeoCoordinateProperty);
      Debug.Assert(mapOverlay.PositionOrigin.X == 0.0, "Expected to have a default PositionOrigin.X with value of zero");
      Debug.Assert(mapOverlay.PositionOrigin.Y == 0.0, "Expected to have a default PositionOrigin.X with value of zero");
      mapOverlay.PositionOrigin = (Point) targetObject.GetValue(MapChild.PositionOriginProperty);
      MapChild.BindMapOverlayProperty(mapOverlay, "PositionOrigin", targetObject, MapChild.PositionOriginProperty);
    }

    /// <summary>
    /// Binds two ways the map overlay to the target dependency property
    /// </summary>
    /// <param name="mapOverlay">MapOverlay that will be used as source</param>
    /// <param name="mapOverlayDependencyProperty">Name of the source dependency property from the MapOverlay</param>
    /// <param name="targetObject">Target object</param>
    /// <param name="targetDependencyProperty">Target dependency property</param>
    internal static void BindMapOverlayProperty(
      MapOverlay mapOverlay,
      string mapOverlayDependencyProperty,
      DependencyObject targetObject,
      DependencyProperty targetDependencyProperty)
    {
      Binding binding = new Binding()
      {
        Source = (object) mapOverlay,
        Mode = (BindingMode) 3,
        Path = new PropertyPath(mapOverlayDependencyProperty, new object[0])
      };
      BindingOperations.SetBinding(targetObject, targetDependencyProperty, (BindingBase) binding);
    }

    /// <summary>
    /// Clear the bindings created when the overlay was created by MapChild
    /// </summary>
    /// <param name="mapOverlay">MapOverlay that was created by MapChild</param>
    internal static void ClearMapOverlayBindings(MapOverlay mapOverlay)
    {
      DependencyObject targetObject = ((DependencyObject) mapOverlay.Content).GetVisualChildren().FirstOrDefault<DependencyObject>();
      if (targetObject == null)
        return;
      MapChild.ClearMapOverlayBindings(mapOverlay, targetObject);
    }

    /// <summary>
    /// Clear the bindings created when the overlay was created by MapChild
    /// </summary>
    /// <param name="mapOverlay">MapOverlay that was created by MapChild</param>
    /// <param name="targetObject">Target object to be used to clear the bindings</param>
    internal static void ClearMapOverlayBindings(
      MapOverlay mapOverlay,
      DependencyObject targetObject)
    {
      Debug.Assert((bool) ((DependencyObject) mapOverlay).GetValue(MapChild.ToolkitCreatedProperty), "expected that we only get this calls for overlays created by the toolkit");
      GeoCoordinate geoCoordinate = (GeoCoordinate) targetObject.GetValue(MapChild.GeoCoordinateProperty);
      Point point = (Point) targetObject.GetValue(MapChild.PositionOriginProperty);
      targetObject.ClearValue(MapChild.GeoCoordinateProperty);
      targetObject.ClearValue(MapChild.PositionOriginProperty);
      targetObject.SetValue(MapChild.GeoCoordinateProperty, (object) geoCoordinate);
      targetObject.SetValue(MapChild.PositionOriginProperty, (object) point);
    }
  }
}
