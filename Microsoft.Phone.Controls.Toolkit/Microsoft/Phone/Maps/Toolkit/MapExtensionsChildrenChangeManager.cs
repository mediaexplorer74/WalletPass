// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapExtensionsChildrenChangeManager
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Class MapExtensionsChildrenChangedManager.
  /// Concrete implementation of <see cref="T:Microsoft.Phone.Maps.Toolkit.MapExtensionsChildrenChangeManager" /> that will listen
  /// to change events in the source collection events to the Map.Layers collection
  /// </summary>
  internal class MapExtensionsChildrenChangeManager : CollectionChangeListener<DependencyObject>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapExtensionsChildrenChangeManager" /> class
    /// </summary>
    /// <param name="sourceCollection">Source collection that will be managed</param>
    public MapExtensionsChildrenChangeManager(INotifyCollectionChanged sourceCollection)
    {
      if (sourceCollection == null)
        throw new ArgumentNullException(nameof (sourceCollection));
      this.ObjectToMapLayerMapping = new Dictionary<DependencyObject, MapLayer>();
      sourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<DependencyObject>) this).CollectionChanged);
    }

    /// <summary>
    /// Gets or sets Map to be used to insert/remove/etc MapLayers per the forwarded events
    /// </summary>
    public Map Map { get; set; }

    /// <summary>
    /// Gets or sets the dictionary to be used to track the map layers created here.
    /// When removing the objects, the appropriate actions will be taken on the target MapLayer.
    /// Each object from the source collection is translated into a MapLayer.
    /// </summary>
    private Dictionary<DependencyObject, MapLayer> ObjectToMapLayerMapping { get; set; }

    /// <summary>
    /// Implements the InsertItemInternal from the parent class.
    /// Will take the object and map into object of MapLayer
    /// that can be added to Map.Layers
    /// </summary>
    /// <param name="index">The index at which the object was inserted</param>
    /// <param name="obj">Object to be inserted</param>
    protected override void InsertItemInternal(int index, DependencyObject obj)
    {
      MapLayer mapLayer = !this.ObjectToMapLayerMapping.ContainsKey(obj) ? MapExtensionsChildrenChangeManager.GetMapLayerForObject((object) obj) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapLayerMapping[obj] = mapLayer;
      this.Map.Layers.Insert(index, mapLayer);
    }

    /// <summary>
    /// Implements the RemoveItemInternal from the parent class.
    /// Will take the object and remove the target item from the Map.Layers collection
    /// </summary>
    /// <param name="obj">Object to be removed</param>
    protected override void RemoveItemInternal(DependencyObject obj)
    {
      bool condition = this.ObjectToMapLayerMapping.ContainsKey(obj);
      Debug.Assert(condition, "It is expected that there is a mapping for the object");
      if (!condition)
        return;
      MapLayer mapLayer = this.ObjectToMapLayerMapping[obj];
      this.ObjectToMapLayerMapping.Remove(obj);
      this.Map.Layers.Remove(mapLayer);
      if (!(obj is MapItemsControl))
        Debug.Assert(((Collection<MapOverlay>) mapLayer).Count == 1, "Expected that the map overlay once created is still there");
      foreach (MapOverlay mapOverlay in (Collection<MapOverlay>) mapLayer)
        MapChild.ClearMapOverlayBindings(mapOverlay);
    }

    /// <summary>
    /// Implements the behavior the target collection will have when the source is reset
    /// </summary>
    protected override void ResetInternal()
    {
      foreach (Collection<MapOverlay> layer in this.Map.Layers)
      {
        foreach (MapOverlay mapOverlay in layer)
          MapChild.ClearMapOverlayBindings(mapOverlay);
      }
      this.Map.Layers.Clear();
      this.ObjectToMapLayerMapping.Clear();
    }

    /// <summary>
    /// Implements the AddInternal from the parent class.
    /// Will take the object and map into object of MapLayer
    /// that can be added to Map.Layers
    /// </summary>
    /// <param name="obj">Object to be added</param>
    protected override void AddInternal(DependencyObject obj)
    {
      MapLayer mapLayer = !this.ObjectToMapLayerMapping.ContainsKey(obj) ? MapExtensionsChildrenChangeManager.GetMapLayerForObject((object) obj) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapLayerMapping[obj] = mapLayer;
      this.Map.Layers.Add(mapLayer);
    }

    /// <summary>
    /// Moves the specified object from the old index to the new index
    /// </summary>
    /// <param name="obj">Object to be moved</param>
    /// <param name="newIndex">New index</param>
    protected override void MoveInternal(DependencyObject obj, int newIndex)
    {
      bool condition = this.ObjectToMapLayerMapping.ContainsKey(obj);
      Debug.Assert(condition, "target object should be in the mapping table");
      if (!condition)
        return;
      ObservableCollection<MapLayer> layers = (ObservableCollection<MapLayer>) this.Map.Layers;
      layers.Move(layers.IndexOf(this.ObjectToMapLayerMapping[obj]), newIndex);
    }

    /// <summary>
    /// Takes the target object and create the corresponding MapLayer that will be used
    /// to host in MapOverlays all the items provided.
    /// </summary>
    /// <param name="obj">Object from the source collection to be processed</param>
    /// <returns>The MapLayer that will be used to host the items from the source</returns>
    /// <remarks>
    /// It only supports two types of objects 1) MapItemsControl or 2) anything else.
    /// For MapItemsControls, the creation of the MapOverlays will be deferred to the MapItemsControl
    /// </remarks>
    private static MapLayer GetMapLayerForObject(object obj)
    {
      MapLayer mapLayerForObject;
      if (obj is MapItemsControl mapItemsControl)
      {
        mapLayerForObject = mapItemsControl.MapLayer;
        Debug.Assert(((Collection<MapOverlay>) mapLayerForObject).Count == mapItemsControl.Items.Count, "MapLayer and MapItemsControl.Items count should match");
      }
      else
      {
        mapLayerForObject = new MapLayer();
        MapOverlay mapOverlay = MapChild.CreateMapOverlay(obj, (DataTemplate) null);
        ((Collection<MapOverlay>) mapLayerForObject).Add(mapOverlay);
      }
      return mapLayerForObject;
    }
  }
}
