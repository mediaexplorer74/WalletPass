// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Device.Location;
using System.Windows;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Represents a class that can extend the capabilities of the <see cref="T:Microsoft.Phone.Maps.Controls.Map" /> class.
  /// </summary>
  public static class MapExtensions
  {
    /// <summary>Identifies the Children dependency property.</summary>
    public static readonly DependencyProperty ChildrenProperty = DependencyProperty.RegisterAttached("Children", typeof (ObservableCollection<DependencyObject>), typeof (MapExtensions), (PropertyMetadata) null);
    /// <summary>
    /// Identifies the Microsoft.Phone.Maps.Toolkit.MapExtensions.ChildrenChangedManagerProperty attached dependency property.
    /// </summary>
    private static readonly DependencyProperty ChildrenChangedManagerProperty = DependencyProperty.RegisterAttached("ChildrenChangedManager", typeof (MapExtensionsChildrenChangeManager), typeof (MapExtensions), (PropertyMetadata) null);

    /// <summary>Gets the Children collection of a map.</summary>
    /// <param name="element">The dependency object</param>
    /// <returns>Returns <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /></returns>
    public static ObservableCollection<DependencyObject> GetChildren(Map element)
    {
      ObservableCollection<DependencyObject> sourceCollection = element != null ? (ObservableCollection<DependencyObject>) ((DependencyObject) element).GetValue(MapExtensions.ChildrenProperty) : throw new ArgumentNullException(nameof (element));
      if (sourceCollection == null)
      {
        sourceCollection = new ObservableCollection<DependencyObject>();
        MapExtensionsChildrenChangeManager childrenChangeManager = new MapExtensionsChildrenChangeManager((INotifyCollectionChanged) sourceCollection)
        {
          Map = element
        };
        ((DependencyObject) element).SetValue(MapExtensions.ChildrenProperty, (object) sourceCollection);
        ((DependencyObject) element).SetValue(MapExtensions.ChildrenChangedManagerProperty, (object) childrenChangeManager);
      }
      return sourceCollection;
    }

    /// <summary>
    /// Adds a dependency object to the map at the specified location.
    /// </summary>
    /// <param name="childrenCollection">An <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> to add to.</param>
    /// <param name="dependencyObject">The dependency object to add.</param>
    /// <param name="geoCoordinate">The geographic coordinate at which to add the dependency object.</param>
    public static void Add(
      this ObservableCollection<DependencyObject> childrenCollection,
      DependencyObject dependencyObject,
      GeoCoordinate geoCoordinate)
    {
      if (childrenCollection == null)
        throw new ArgumentNullException(nameof (childrenCollection));
      if (dependencyObject == null)
        throw new ArgumentNullException(nameof (dependencyObject));
      if (geoCoordinate == (GeoCoordinate) null)
        throw new ArgumentNullException(nameof (geoCoordinate));
      dependencyObject.SetValue(MapChild.GeoCoordinateProperty, (object) geoCoordinate);
      childrenCollection.Add(dependencyObject);
    }

    /// <summary>
    /// Adds a dependency object to the map at the specified location.
    /// </summary>
    /// <param name="childrenCollection">An <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> to add to.</param>
    /// <param name="dependencyObject">The dependency object to add.</param>
    /// <param name="geoCoordinate">The geographic coordinate at which to add the dependency object.</param>
    /// <param name="positionOrigin">The position origin to use.</param>
    public static void Add(
      this ObservableCollection<DependencyObject> childrenCollection,
      DependencyObject dependencyObject,
      GeoCoordinate geoCoordinate,
      Point positionOrigin)
    {
      if (childrenCollection == null)
        throw new ArgumentNullException(nameof (childrenCollection));
      if (dependencyObject == null)
        throw new ArgumentNullException(nameof (dependencyObject));
      if (geoCoordinate == (GeoCoordinate) null)
        throw new ArgumentNullException(nameof (geoCoordinate));
      dependencyObject.SetValue(MapChild.GeoCoordinateProperty, (object) geoCoordinate);
      dependencyObject.SetValue(MapChild.PositionOriginProperty, (object) positionOrigin);
      childrenCollection.Add(dependencyObject);
    }
  }
}
