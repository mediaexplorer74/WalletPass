// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Maps.Toolkit.MapItemsControl
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Phone.Maps.Toolkit
{
  /// <summary>
  /// Represents a control that can be used to present a collection of items on a map.
  /// </summary>
  [ContentProperty("Items")]
  public sealed class MapItemsControl : DependencyObject
  {
    /// <summary>
    /// Identifies the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapItemsControl.ItemsSource" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof (ItemsSource), typeof (IEnumerable), typeof (MapItemsControl), new PropertyMetadata(new PropertyChangedCallback(MapItemsControl.OnItemsSourceChanged)));
    /// <summary>
    /// Identifies the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapItemsControl.ItemTemplate" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (DataTemplate), typeof (MapItemsControl), new PropertyMetadata(new PropertyChangedCallback(MapItemsControl.OnItemTemplateChanged)));
    /// <summary>
    /// Identifies the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapItemsControl.Name" /> dependency property.
    /// </summary>
    public static readonly DependencyProperty NameProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (string), typeof (MapItemsControl), (PropertyMetadata) null);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsControl" /> class
    /// </summary>
    public MapItemsControl()
    {
      this.MapLayer = new MapLayer();
      this.Items = new MapChildCollection();
      this.ItemsChangeManager = new MapItemsControlChangeManager((INotifyCollectionChanged) this.Items)
      {
        MapLayer = this.MapLayer
      };
      this.ItemsSource = (IEnumerable) null;
      this.ItemTemplate = (DataTemplate) null;
    }

    /// <summary>
    /// Gets the collection used to generate the content of the control.
    /// </summary>
    public MapChildCollection Items { get; private set; }

    /// <summary>Gets or sets the name</summary>
    public string Name
    {
      get => (string) this.GetValue(MapItemsControl.NameProperty);
      set => this.SetValue(MapItemsControl.NameProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets a collection used to generate the content of the <see cref="T:System.Windows.Controls.ItemsControl" />.
    /// </summary>
    public IEnumerable ItemsSource
    {
      get => (IEnumerable) this.GetValue(MapItemsControl.ItemsSourceProperty);
      set => this.SetValue(MapItemsControl.ItemsSourceProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DataTemplate" /> used to display each item.
    /// </summary>
    public DataTemplate ItemTemplate
    {
      get => (DataTemplate) this.GetValue(MapItemsControl.ItemTemplateProperty);
      set => this.SetValue(MapItemsControl.ItemTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets the <see cref="T:Microsoft.Phone.Maps.Toolkit.MapItemsControlChangeManager" /> used in the backend
    /// </summary>
    internal MapItemsControlChangeManager ItemsChangeManager { get; private set; }

    /// <summary>
    /// Gets the <see cref="P:Microsoft.Phone.Maps.Toolkit.MapItemsControl.ItemsSourceChangeManager" /> used in the backend
    /// </summary>
    internal MapItemsSourceChangeManager ItemsSourceChangeManager { get; private set; }

    /// <summary>Gets or sets the MapLayer used to map the input</summary>
    internal MapLayer MapLayer { get; set; }

    /// <summary>Will handle the item template change</summary>
    /// <param name="d">DependencyObject that triggers the event.</param>
    /// <param name="e">Event args</param>
    private static void OnItemTemplateChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      MapItemsControl mapItemsControl = (MapItemsControl) d;
      DataTemplate newValue = (DataTemplate) e.NewValue;
      mapItemsControl.ItemsChangeManager.ItemTemplate = newValue;
      foreach (MapOverlay mapOverlay in (Collection<MapOverlay>) mapItemsControl.MapLayer)
      {
        MapChild.ClearMapOverlayBindings(mapOverlay);
        ((ContentPresenter) mapOverlay.Content).ContentTemplate = newValue;
      }
    }

    /// <summary>Will handle the items source change</summary>
    /// <param name="d">D9ependencyObject that triggers the event.</param>
    /// <param name="e">Event args</param>
    private static void OnItemsSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      MapItemsControl mapItemsControl = (MapItemsControl) d;
      IEnumerable newValue = (IEnumerable) e.NewValue;
      if (mapItemsControl.Items.Count > 0)
        throw new InvalidOperationException("Items must be empty before using Items Source");
      if (newValue != null)
      {
        if (mapItemsControl.ItemsSourceChangeManager != null)
        {
          mapItemsControl.ItemsSourceChangeManager.Disconnect();
          mapItemsControl.ItemsSourceChangeManager = (MapItemsSourceChangeManager) null;
        }
        Debug.Assert(mapItemsControl.Items.Count == 0, "Expected MapItemsControl.Items.Count == 0");
        mapItemsControl.Items.AddRange(newValue);
        if (newValue is INotifyCollectionChanged sourceCollection)
          mapItemsControl.ItemsSourceChangeManager = new MapItemsSourceChangeManager(sourceCollection)
          {
            Items = mapItemsControl.Items
          };
      }
      mapItemsControl.Items.IsReadOnly = newValue != null;
    }
  }
}
