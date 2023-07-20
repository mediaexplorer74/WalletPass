// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapItemsControlChangeManager
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
  /// Class MapItemsControlChangedManager.
  /// Works as a middle man between the internal ItemsControl in MapItemsControl and a MapLayer
  /// </summary>
  internal class MapItemsControlChangeManager : CollectionChangeListener<object>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsControlChangeManager" /> class
    /// </summary>
    /// <param name="sourceCollection">Source collection that will be managed</param>
    public MapItemsControlChangeManager(INotifyCollectionChanged sourceCollection)
    {
      if (sourceCollection == null)
        throw new ArgumentNullException(nameof (sourceCollection));
      this.ObjectToMapOverlayMapping = new Dictionary<object, MapOverlay>();
      sourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(((CollectionChangeListener<object>) this).CollectionChanged);
    }

    /// <summary>
    /// Gets or sets the ItemTemplate to be used when creating MapOverlays
    /// </summary>
    public DataTemplate ItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets the MapLayer used to host all the MapOverlays created here
    /// </summary>
    public MapLayer MapLayer { get; set; }

    /// <summary>
    /// Gets or sets the dictionary that maps the objects to a MapOverlay
    /// </summary>
    private Dictionary<object, MapOverlay> ObjectToMapOverlayMapping { get; set; }

    /// <summary>
    /// Inserts the item at the specified index in the target collection
    /// </summary>
    /// <param name="index">Index at which object will be inserted</param>
    /// <param name="obj">Object to be inserted</param>
    protected override void InsertItemInternal(int index, object obj)
    {
      MapOverlay mapOverlay = !this.ObjectToMapOverlayMapping.ContainsKey(obj) ? MapChild.CreateMapOverlay(obj, this.ItemTemplate) : throw new InvalidOperationException("Attempted to insert the same object twice");
      ((Collection<MapOverlay>) this.MapLayer).Insert(index, mapOverlay);
      this.ObjectToMapOverlayMapping.Add(obj, mapOverlay);
    }

    /// <summary>Remove the specified item from the target collection</summary>
    /// <param name="obj">Object to be removed</param>
    protected override void RemoveItemInternal(object obj)
    {
      bool condition = this.ObjectToMapOverlayMapping.ContainsKey(obj);
      Debug.Assert(condition, "expected to have a mapping for the object");
      if (!condition)
        return;
      MapOverlay mapOverlay = this.ObjectToMapOverlayMapping[obj];
      this.ObjectToMapOverlayMapping.Remove(obj);
      ((Collection<MapOverlay>) this.MapLayer).Remove(mapOverlay);
      MapChild.ClearMapOverlayBindings(mapOverlay);
    }

    /// <summary>Resets the target collection and internal state</summary>
    protected override void ResetInternal()
    {
      foreach (MapOverlay mapOverlay in (Collection<MapOverlay>) this.MapLayer)
        MapChild.ClearMapOverlayBindings(mapOverlay);
      ((Collection<MapOverlay>) this.MapLayer).Clear();
      this.ObjectToMapOverlayMapping.Clear();
    }

    /// <summary>Adds the object to the target collection</summary>
    /// <param name="obj">Object to be added</param>
    protected override void AddInternal(object obj)
    {
      MapOverlay mapOverlay = !this.ObjectToMapOverlayMapping.ContainsKey(obj) ? MapChild.CreateMapOverlay(obj, this.ItemTemplate) : throw new InvalidOperationException("Attempted to insert the same object twice");
      this.ObjectToMapOverlayMapping[obj] = mapOverlay;
      ((Collection<MapOverlay>) this.MapLayer).Add(mapOverlay);
    }

    /// <summary>
    /// Moves the specified object from the old index to the new index
    /// </summary>
    /// <param name="obj">Object to be moved</param>
    /// <param name="newIndex">New index</param>
    protected override void MoveInternal(object obj, int newIndex)
    {
      bool condition = this.ObjectToMapOverlayMapping.ContainsKey(obj);
      Debug.Assert(condition, "target object should be in the mapping table");
      if (!condition)
        return;
      ((ObservableCollection<MapOverlay>) this.MapLayer).Move(((Collection<MapOverlay>) this.MapLayer).IndexOf(this.ObjectToMapOverlayMapping[obj]), newIndex);
    }
  }
}
