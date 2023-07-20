// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.LongListMultiSelector
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Extension of the standard LongListSelector control which allows multiple selection of items
  /// </summary>
  [TemplatePart(Name = "InnerSelector", Type = typeof (LongListSelector))]
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (LongListMultiSelectorItem))]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
  public class LongListMultiSelector : Control
  {
    private const string InnerSelectorName = "InnerSelector";
    private LongListSelector _innerSelector = (LongListSelector) null;
    private HashSet<WeakReference<LongListMultiSelectorItem>> _realizedItems = new HashSet<WeakReference<LongListMultiSelectorItem>>();
    private LongListMultiSelector.SelectedItemsList _selectedItems = new LongListMultiSelector.SelectedItemsList();
    private LongListSelectorLayoutMode _layoutMode = (LongListSelectorLayoutMode) 0;
    /// <summary>
    ///     Identifies the Microsoft.Phone.Controls.LongListMultiSelector.GridCellSize dependency
    ///     property.
    /// </summary>
    public static readonly DependencyProperty GridCellSizeProperty = DependencyProperty.Register(nameof (GridCellSize), typeof (Size), typeof (LongListMultiSelector), new PropertyMetadata((object) Size.Empty));
    /// <summary>
    ///     Identifies the Microsoft.Phone.Controls.LongListMultiSelector.GroupFooterTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty GroupFooterTemplateProperty = DependencyProperty.Register(nameof (GroupFooterTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///    Identifies the Microsoft.Phone.Controls.LongListMultiSelector.GroupHeaderTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty GroupHeaderTemplateProperty = DependencyProperty.Register(nameof (GroupHeaderTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.HideEmptyGroups  dependency property.
    /// </summary>
    public static readonly DependencyProperty HideEmptyGroupsProperty = DependencyProperty.Register(nameof (HideEmptyGroups), typeof (bool), typeof (LongListMultiSelector), new PropertyMetadata((object) false));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.IsGroupingEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty IsGroupingEnabledProperty = DependencyProperty.Register(nameof (IsGroupingEnabled), typeof (bool), typeof (LongListMultiSelector), new PropertyMetadata((object) false));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ItemContainerStyle dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(nameof (ItemContainerStyle), typeof (Style), typeof (LongListMultiSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(LongListMultiSelector.OnItemContainerStylePropertyChanged)));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ItemsSource dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof (ItemsSource), typeof (IList), typeof (LongListMultiSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(LongListMultiSelector.OnItemsSourcePropertyChanged)));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ItemTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>Identifies the ItemInfoTemplate dependency property.</summary>
    public static readonly DependencyProperty ItemInfoTemplateProperty = DependencyProperty.Register(nameof (ItemInfoTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(LongListMultiSelector.OnItemInfoTemplatePropertyChanged)));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.JumpListStyle dependency property.
    /// </summary>
    public static readonly DependencyProperty JumpListStyleProperty = DependencyProperty.Register(nameof (JumpListStyle), typeof (Style), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ListFooter dependency property.
    /// </summary>
    public static readonly DependencyProperty ListFooterProperty = DependencyProperty.Register(nameof (ListFooter), typeof (object), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ListFooterTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty ListFooterTemplateProperty = DependencyProperty.Register(nameof (ListFooterTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ListHeader dependency property.
    /// </summary>
    public static readonly DependencyProperty ListHeaderProperty = DependencyProperty.Register(nameof (ListHeader), typeof (object), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///    Identifies the Microsoft.Phone.Controls.LongListMultiSelector.ListHeaderTemplate dependency
    ///    property.
    /// </summary>
    public static readonly DependencyProperty ListHeaderTemplateProperty = DependencyProperty.Register(nameof (ListHeaderTemplate), typeof (DataTemplate), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///    Identifies the Microsoft.Phone.Controls.LongListMultiSelector.SelectedItems dependency
    ///    property.
    /// </summary>
    public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof (SelectedItems), typeof (IList), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the IsSelectionEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty IsSelectionEnabledProperty = DependencyProperty.Register(nameof (IsSelectionEnabled), typeof (bool), typeof (LongListMultiSelector), new PropertyMetadata((object) false, new PropertyChangedCallback(LongListMultiSelector.OnIsSelectionEnabledPropertyChanged)));
    /// <summary>
    /// Identifies the EnforceIsSelectionEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty EnforceIsSelectionEnabledProperty = DependencyProperty.Register(nameof (EnforceIsSelectionEnabled), typeof (bool), typeof (LongListMultiSelector), new PropertyMetadata((object) false, new PropertyChangedCallback(LongListMultiSelector.OnEnforceIsSelectionEnabledPropertyChanged)));
    /// <summary>
    /// Identifies the DefaultListItemContainerStyle dependency property.
    /// </summary>
    internal static readonly DependencyProperty DefaultListItemContainerStyleProperty = DependencyProperty.Register(nameof (DefaultListItemContainerStyle), typeof (Style), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the DefaultGridItemContainerStyle dependency property.
    /// </summary>
    internal static readonly DependencyProperty DefaultGridItemContainerStyleProperty = DependencyProperty.Register(nameof (DefaultGridItemContainerStyle), typeof (Style), typeof (LongListMultiSelector), new PropertyMetadata((PropertyChangedCallback) null));

    /// <summary>
    ///    Gets the state of manipulation handling on the Microsoft.Phone.Controls.LongListSelector
    ///    control.
    /// </summary>
    public ManipulationState ManipulationState => this._innerSelector == null ? (ManipulationState) (object) 0 : this._innerSelector.ManipulationState;

    /// <summary>
    ///     Gets or sets the size used when displaying an item in the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    public Size GridCellSize
    {
      get => (Size) ((DependencyObject) this).GetValue(LongListMultiSelector.GridCellSizeProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.GridCellSizeProperty, (object) value);
    }

    /// <summary>
    ///     Gets or sets the template for the group footer in the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    public DataTemplate GroupFooterTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.GroupFooterTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.GroupFooterTemplateProperty, (object) value);
    }

    /// <summary>
    ///    Gets or sets the template for the group header in the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    public DataTemplate GroupHeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.GroupHeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.GroupHeaderTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether to hide empty groups in the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    public bool HideEmptyGroups
    {
      get => (bool) ((DependencyObject) this).GetValue(LongListMultiSelector.HideEmptyGroupsProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.HideEmptyGroupsProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets a value that indicates whether grouping is enabled in the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    public bool IsGroupingEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(LongListMultiSelector.IsGroupingEnabledProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.IsGroupingEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the template for items hosting in the Microsoft.Phone.Controls.LongListMultiSelector, targeted to customize selection highlighting
    /// </summary>
    public Style ItemContainerStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(LongListMultiSelector.ItemContainerStyleProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ItemContainerStyleProperty, (object) value);
    }

    /// <summary>
    /// Called when ItemContainerStyle property has been changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnItemContainerStylePropertyChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelector listMultiSelector))
        return;
      listMultiSelector.OnItemContainerStyleChanged();
    }

    /// <summary>
    /// Gets or sets a collection used to generate the content of the Microsoft.Phone.Controls.LongListMultiSelector.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Framework LongListSelector as a R/W IList ItemsSource property.")]
    public IList ItemsSource
    {
      get => (IList) ((DependencyObject) this).GetValue(LongListMultiSelector.ItemsSourceProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ItemsSourceProperty, (object) value);
    }

    /// <summary>
    /// Handles the change of ItemsSource property : disconnects event handler from old value and reconnects event handler to the new value
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnItemsSourcePropertyChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelector listMultiSelector))
        return;
      listMultiSelector.OnItemsSourceChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// Gets or sets the template for the items in the items view
    /// </summary>
    public DataTemplate ItemTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.ItemTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ItemTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the data template that is to be right align in default template and will not move when selection is opened
    /// </summary>
    public DataTemplate ItemInfoTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.ItemInfoTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ItemInfoTemplateProperty, (object) value);
    }

    /// <summary>
    /// Called when ItemInfoTemplate property has been changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnItemInfoTemplatePropertyChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelector listMultiSelector))
        return;
      listMultiSelector.OnItemInfoTemplateChanged();
    }

    /// <summary>
    /// Gets or sets the System.Windows.Style for jump list in the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public Style JumpListStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(LongListMultiSelector.JumpListStyleProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.JumpListStyleProperty, (object) value);
    }

    /// <summary>
    ///     Gets or sets a value that specifies if the Microsoft.Phone.Controls.LongListSelector
    ///     is in a list mode or grid mode from the Microsoft.Phone.Controls.LongListSelectorLayoutMode
    ///     enum.
    /// </summary>
    public LongListSelectorLayoutMode LayoutMode
    {
      get => this._layoutMode;
      set
      {
        this._layoutMode = value;
        if (this._innerSelector == null)
          return;
        this._innerSelector.LayoutMode = value;
      }
    }

    /// <summary>
    /// Gets or sets the object that is displayed at the foot of the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public object ListFooter
    {
      get => ((DependencyObject) this).GetValue(LongListMultiSelector.ListFooterProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ListFooterProperty, value);
    }

    /// <summary>
    ///    Gets or sets the System.Windows.DataTemplatefor an item to display at the
    ///    foot of the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public DataTemplate ListFooterTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.ListFooterTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ListFooterTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the object to display at the head of the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public object ListHeader
    {
      get => ((DependencyObject) this).GetValue(LongListMultiSelector.ListHeaderProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ListHeaderProperty, value);
    }

    /// <summary>
    ///    Gets or sets the System.Windows.DataTemplatefor an item to display at the
    ///    head of the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public DataTemplate ListHeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelector.ListHeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.ListHeaderTemplateProperty, (object) value);
    }

    /// <summary>
    ///    Gets the currently selected items in the Microsoft.Phone.Controls.LongListSelector.
    /// </summary>
    public IList SelectedItems => (IList) ((DependencyObject) this).GetValue(LongListMultiSelector.SelectedItemsProperty);

    /// <summary>
    /// Gets or sets the flag that indicates if the list
    /// is in selection mode or not.
    /// </summary>
    public bool IsSelectionEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(LongListMultiSelector.IsSelectionEnabledProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.IsSelectionEnabledProperty, (object) value);
    }

    /// <summary>Gets or sets the EnforceIsSelectionEnabled property</summary>
    public bool EnforceIsSelectionEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(LongListMultiSelector.EnforceIsSelectionEnabledProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.EnforceIsSelectionEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the DefaultListItemContainerStyle property
    /// </summary>
    internal Style DefaultListItemContainerStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(LongListMultiSelector.DefaultListItemContainerStyleProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.DefaultListItemContainerStyleProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the DefaultGridItemContainerStyle property
    /// </summary>
    internal Style DefaultGridItemContainerStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(LongListMultiSelector.DefaultGridItemContainerStyleProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelector.DefaultGridItemContainerStyleProperty, (object) value);
    }

    /// <summary>Occurs when a new item is realized.</summary>
    public event EventHandler<ItemRealizationEventArgs> ItemRealized;

    /// <summary>
    ///    Occurs when an item in the Microsoft.Phone.Controls.LongListMultiSelector is unrealized.
    /// </summary>
    public event EventHandler<ItemRealizationEventArgs> ItemUnrealized;

    /// <summary>Occurs when the jump list is closed.</summary>
    public event EventHandler JumpListClosed;

    /// <summary>Occurs when a jump list is opened.</summary>
    public event EventHandler JumpListOpening;

    /// <summary>
    ///    Occurs when Microsoft.Phone.Controls.ManipulationState changes.
    /// </summary>
    public event EventHandler ManipulationStateChanged;

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Occurs when the currently selected item changes.</summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>Occurs when the selection mode is opened or closed.</summary>
    public event DependencyPropertyChangedEventHandler IsSelectionEnabledChanged;

    /// <summary>
    /// Initializes a new instance of the Microsoft.Phone.Cointrols.LongListMultiSelector
    /// </summary>
    public LongListMultiSelector()
    {
      this.DefaultStyleKey = (object) typeof (LongListMultiSelector);
      ((DependencyObject) this).SetValue(LongListMultiSelector.SelectedItemsProperty, (object) this._selectedItems);
      this._selectedItems.CollectionCleared += new EventHandler<LongListMultiSelector.ClearedChangedArgs>(this.OnSelectedItemsCollectionCleared);
      this._selectedItems.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSelectedItemsCollectionChanged);
    }

    /// <summary>
    /// Template application : gets and hooks the inner LongListSelector
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this._realizedItems.Clear();
      if (this._innerSelector != null)
      {
        this._innerSelector.ItemRealized -= new EventHandler<ItemRealizationEventArgs>(this.OnInnerSelectorItemRealized);
        this._innerSelector.ItemUnrealized -= new EventHandler<ItemRealizationEventArgs>(this.OnInnerSelectorItemUnrealized);
        this._innerSelector.JumpListClosed -= new EventHandler(this.OnInnerSelectorJumpListClosed);
        this._innerSelector.JumpListOpening -= new EventHandler(this.OnInnerSelectorJumpListOpening);
        this._innerSelector.ManipulationStateChanged -= new EventHandler(this.OnInnerSelectorManipulationStateChanged);
        this._innerSelector.PropertyChanged -= new PropertyChangedEventHandler(this.OnInnerSelectorPropertyChanged);
      }
      this._innerSelector = this.GetTemplateChild("InnerSelector") as LongListSelector;
      if (this._innerSelector == null)
        return;
      this._innerSelector.LayoutMode = this.LayoutMode;
      this._innerSelector.ItemRealized += new EventHandler<ItemRealizationEventArgs>(this.OnInnerSelectorItemRealized);
      this._innerSelector.ItemUnrealized += new EventHandler<ItemRealizationEventArgs>(this.OnInnerSelectorItemUnrealized);
      this._innerSelector.JumpListClosed += new EventHandler(this.OnInnerSelectorJumpListClosed);
      this._innerSelector.JumpListOpening += new EventHandler(this.OnInnerSelectorJumpListOpening);
      this._innerSelector.ManipulationStateChanged += new EventHandler(this.OnInnerSelectorManipulationStateChanged);
      this._innerSelector.PropertyChanged += new PropertyChangedEventHandler(this.OnInnerSelectorPropertyChanged);
    }

    /// <summary>Applyies the new style to already realized items</summary>
    private void OnItemContainerStyleChanged() => this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (item => ((FrameworkElement) item).Style = this.ItemContainerStyle));

    /// <summary>
    /// Called when the ItemsSource property if the LLMS has been changed
    /// </summary>
    /// <param name="oldValue"></param>
    /// <param name="newValue"></param>
    protected virtual void OnItemsSourceChanged(object oldValue, object newValue)
    {
      if (oldValue is INotifyCollectionChanged collectionChanged1)
        collectionChanged1.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnItemsSourceCollectionChanged);
      if (newValue is INotifyCollectionChanged collectionChanged2)
        collectionChanged2.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsSourceCollectionChanged);
      this.SelectedItems.Clear();
    }

    /// <summary>
    /// Handles changes inside the ItemSource collection : removes removed item from the selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void OnItemsSourceCollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      if (e == null || e.OldItems == null)
        return;
      this.UnselectItems(e.OldItems);
    }

    /// <summary>Applyies the new style to already realized items</summary>
    protected virtual void OnItemInfoTemplateChanged() => this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (item => item.ContentInfoTemplate = this.ItemInfoTemplate));

    /// <summary>Relays event from the inner LongListSelector</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged(sender, e);
    }

    /// <summary>Relays event from the inner LongListSelector</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorManipulationStateChanged(object sender, EventArgs e)
    {
      if (this.ManipulationStateChanged == null)
        return;
      this.ManipulationStateChanged(sender, e);
    }

    /// <summary>Relays event from the inner LongListSelector</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorJumpListOpening(object sender, EventArgs e)
    {
      if (this.JumpListOpening == null)
        return;
      this.JumpListOpening(sender, e);
    }

    /// <summary>Relays event from the inner LongListSelector</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorJumpListClosed(object sender, EventArgs e)
    {
      if (this.JumpListClosed == null)
        return;
      this.JumpListClosed(sender, e);
    }

    /// <summary>Disconnects an item when it is unrealized</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorItemUnrealized(object sender, ItemRealizationEventArgs e)
    {
      if (e.ItemKind == 2 && VisualTreeHelper.GetChildrenCount((DependencyObject) e.Container) > 0 && VisualTreeHelper.GetChild((DependencyObject) e.Container, 0) is LongListMultiSelectorItem child)
      {
        child.IsSelectedChanged -= new EventHandler(this.OnLongListMultiSelectorItemIsSelectedChanged);
        this._realizedItems.Remove(child.WR);
      }
      if (this.ItemUnrealized == null)
        return;
      this.ItemUnrealized(sender, e);
    }

    /// <summary>
    /// Configure an item's template according to the current state
    /// </summary>
    /// <param name="item"></param>
    internal void ConfigureItem(LongListMultiSelectorItem item)
    {
      if (item == null)
        return;
      item.ContentTemplate = this.ItemTemplate;
      if (this.ItemContainerStyle != null)
      {
        if (((FrameworkElement) item).Style != this.ItemContainerStyle)
          ((FrameworkElement) item).Style = this.ItemContainerStyle;
      }
      else if (this.LayoutMode == 1)
      {
        if (((FrameworkElement) item).Style != this.DefaultGridItemContainerStyle)
          ((FrameworkElement) item).Style = this.DefaultGridItemContainerStyle;
      }
      else if (((FrameworkElement) item).Style != this.DefaultListItemContainerStyle)
        ((FrameworkElement) item).Style = this.DefaultListItemContainerStyle;
      if (this.ItemInfoTemplate != null && item.ContentInfoTemplate != this.ItemInfoTemplate)
      {
        ((FrameworkElement) item).SetBinding(LongListMultiSelectorItem.ContentInfoProperty, new Binding());
        item.ContentInfoTemplate = this.ItemInfoTemplate;
      }
    }

    /// <summary>Called when an item is realized :</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnInnerSelectorItemRealized(object sender, ItemRealizationEventArgs e)
    {
      if (e.ItemKind == 2 && VisualTreeHelper.GetChildrenCount((DependencyObject) e.Container) > 0 && VisualTreeHelper.GetChild((DependencyObject) e.Container, 0) is LongListMultiSelectorItem child)
      {
        this.ConfigureItem(child);
        child.IsSelected = this._selectedItems.Contains(child.Content);
        child.IsSelectedChanged += new EventHandler(this.OnLongListMultiSelectorItemIsSelectedChanged);
        child.GotoState(this.IsSelectionEnabled ? LongListMultiSelectorItem.State.Opened : LongListMultiSelectorItem.State.Closed);
        this._realizedItems.Add(child.WR);
      }
      if (this.ItemRealized == null)
        return;
      this.ItemRealized(sender, e);
    }

    /// <summary>Called when an Item's IsSelected property has changed</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnLongListMultiSelectorItemIsSelectedChanged(object sender, EventArgs e)
    {
      if (!(sender is LongListMultiSelectorItem multiSelectorItem))
        return;
      object content = multiSelectorItem.Content;
      if (content != null)
      {
        if (multiSelectorItem.IsSelected)
          this.SelectedItems.Add(content);
        else
          this.SelectedItems.Remove(content);
      }
    }

    /// <summary>
    /// Changes the state of the items given the value of the property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnIsSelectionEnabledPropertyChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelector listMultiSelector))
        return;
      listMultiSelector.OnIsSelectionEnabledChanged(e);
    }

    /// <summary>
    /// Called when the IsSelectionEnabled property is changed.
    /// </summary>
    /// <param name="e">DependencyPropertyChangedEventArgs associated to the event</param>
    protected virtual void OnIsSelectionEnabledChanged(DependencyPropertyChangedEventArgs e)
    {
      bool newValue = (bool) e.NewValue;
      if (!newValue)
        this.SelectedItems.Clear();
      this.ApplyItemsState(newValue ? LongListMultiSelectorItem.State.Opened : LongListMultiSelectorItem.State.Closed, true);
      if (this.IsSelectionEnabledChanged == null)
        return;
      this.IsSelectionEnabledChanged((object) this, e);
    }

    /// <summary>
    /// Called when the OnEnforceIsSelectionEnabled dependency property has been changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnEnforceIsSelectionEnabledPropertyChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelector listMultiSelector))
        return;
      listMultiSelector.OnEnforceIsSelectionEnabledChanged();
    }

    /// <summary>
    /// Called when the OnEnforceIsSelectionEnabled property has been changed
    /// </summary>
    protected virtual void OnEnforceIsSelectionEnabledChanged()
    {
      if (!this.EnforceIsSelectionEnabled)
        this.SelectedItems.Clear();
      this.UpdateIsSelectionEnabled();
    }

    /// <summary>
    /// Updates the IsSelectionEnabled property according to the possibly enforced value and the selected items count
    /// </summary>
    protected virtual void UpdateIsSelectionEnabled() => this.IsSelectionEnabled = this.EnforceIsSelectionEnabled || this.SelectedItems.Count > 0;

    /// <summary>
    /// Triggers SelectionChanged event and updates the IsSelectionEnabled property
    /// </summary>
    /// <param name="removedItems"></param>
    /// <param name="addedItems"></param>
    private void OnSelectionChanged(IList removedItems, IList addedItems)
    {
      this.UpdateIsSelectionEnabled();
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, new SelectionChangedEventArgs(removedItems ?? (IList) new List<object>(), addedItems ?? (IList) new List<object>()));
    }

    /// <summary>
    /// Executes an action to all live items and cleanup dead references
    /// </summary>
    /// <param name="action"></param>
    protected void ApplyLiveItems(Action<LongListMultiSelectorItem> action)
    {
      if (action == null)
        return;
      HashSet<WeakReference<LongListMultiSelectorItem>> weakReferenceSet = new HashSet<WeakReference<LongListMultiSelectorItem>>();
      foreach (WeakReference<LongListMultiSelectorItem> realizedItem in this._realizedItems)
      {
        LongListMultiSelectorItem target;
        if (realizedItem.TryGetTarget(out target))
        {
          action(target);
          weakReferenceSet.Add(realizedItem);
        }
      }
      this._realizedItems = weakReferenceSet;
    }

    /// <summary>
    /// Handles selection changes made throught the SelectedItems property
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          this.SelectItems(e.NewItems);
          this.OnSelectionChanged((IList) null, e.NewItems);
          break;
        case NotifyCollectionChangedAction.Remove:
          this.UnselectItems(e.OldItems);
          this.OnSelectionChanged(e.OldItems, (IList) null);
          break;
        case NotifyCollectionChangedAction.Replace:
          this.UnselectItems(e.OldItems);
          this.SelectItems(e.NewItems);
          this.OnSelectionChanged(e.OldItems, e.NewItems);
          break;
      }
    }

    private void OnSelectedItemsCollectionCleared(
      object sender,
      LongListMultiSelector.ClearedChangedArgs e)
    {
      this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (item => item.IsSelected = false));
      this.OnSelectionChanged(e.OldItems, (IList) null);
    }

    /// <summary>
    /// Selects the LongListMultiSelectorItems whose content matches the provided list
    /// </summary>
    /// <param name="items">List of content (i.e. from ItemsSource)</param>
    private void SelectItems(IList items) => this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (item =>
    {
      if (!items.Contains(item.Content))
        return;
      item.IsSelected = true;
    }));

    /// <summary>
    /// Unselects the LongListMultiSelectorItems whose content matches the provided list
    /// </summary>
    /// <param name="items">List of content (i.e. from ItemsSource)</param>
    private void UnselectItems(IList items) => this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (item =>
    {
      if (!items.Contains(item.Content))
        return;
      item.IsSelected = false;
    }));

    /// <summary>
    /// Returns the LongListMultiSelectorItem corresponding to the given item
    /// </summary>
    /// <param name="item">Item whose container has to be returned</param>
    /// <returns></returns>
    public object ContainerFromItem(object item)
    {
      object ret = (object) null;
      this.ApplyLiveItems((Action<LongListMultiSelectorItem>) (llmsItem =>
      {
        if (llmsItem.Content != item)
          return;
        ret = (object) llmsItem;
      }));
      return ret;
    }

    /// <summary>
    /// Applies a new state to all items. Visible items will use transitions if useTransitions parameter is set, others will not
    /// </summary>
    /// <param name="state">State to apply</param>
    /// <param name="useTransitions">Specify whether to use transitions or not for visible items</param>
    private void ApplyItemsState(LongListMultiSelectorItem.State state, bool useTransitions)
    {
      if (this._innerSelector == null)
        return;
      if (useTransitions)
      {
        List<LongListMultiSelectorItem> invisibleItems = new List<LongListMultiSelectorItem>();
        double actualHeight = ((FrameworkElement) this._innerSelector).ActualHeight;
        foreach (WeakReference<LongListMultiSelectorItem> realizedItem in this._realizedItems)
        {
          LongListMultiSelectorItem target;
          if (realizedItem.TryGetTarget(out target))
          {
            GeneralTransform visual = ((UIElement) target).TransformToVisual((UIElement) this._innerSelector);
            Point point = visual.Transform(new Point(0.0, 0.0));
            bool flag;
            if (point.Y > actualHeight)
              flag = false;
            else if (point.Y >= 0.0)
            {
              flag = true;
            }
            else
            {
              point = visual.Transform(new Point(((FrameworkElement) target).ActualHeight, 0.0));
              flag = point.Y >= 0.0;
            }
            if (flag)
              target.GotoState(state, true);
            else
              invisibleItems.Add(target);
          }
        }
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
        {
          foreach (LongListMultiSelectorItem multiSelectorItem in invisibleItems)
            multiSelectorItem.GotoState(state);
        }));
      }
      else
      {
        foreach (WeakReference<LongListMultiSelectorItem> realizedItem in this._realizedItems)
        {
          LongListMultiSelectorItem target;
          if (realizedItem.TryGetTarget(out target))
            target.GotoState(state);
        }
      }
    }

    /// <summary>Internal event data associated to list changes</summary>
    private class ClearedChangedArgs : EventArgs
    {
      /// <summary>Items removed from the list</summary>
      public IList OldItems { get; private set; }

      /// <summary>
      /// Constructs a NotifyItemsChangedArgs from an action and a list of items
      /// </summary>
      /// <param name="items"></param>
      public ClearedChangedArgs(IList items) => this.OldItems = items;
    }

    /// <summary>
    /// Collection for holding selected items
    /// It adds CollectionCleared event to the ObservableCollection in order to provide removed items with the event when the collection is cleared
    /// </summary>
    private class SelectedItemsList : ObservableCollection<object>
    {
      /// <summary>Event indicating that collection has changed</summary>
      public event EventHandler<LongListMultiSelector.ClearedChangedArgs> CollectionCleared;

      /// <summary>Overrides the base class ClearItems method</summary>
      protected override void ClearItems()
      {
        if (this.Count <= 0)
          return;
        LongListMultiSelector.ClearedChangedArgs e = this.CollectionCleared != null ? new LongListMultiSelector.ClearedChangedArgs((IList) new List<object>((IEnumerable<object>) this)) : (LongListMultiSelector.ClearedChangedArgs) null;
        base.ClearItems();
        if (this.CollectionCleared != null)
          this.CollectionCleared((object) this, e);
      }
    }
  }
}
