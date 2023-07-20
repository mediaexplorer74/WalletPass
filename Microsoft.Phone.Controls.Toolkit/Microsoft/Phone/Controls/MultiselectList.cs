// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MultiselectList
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A collection of items that supports multiple selection.
  /// </summary>
  /// <QualityBand>Experimental</QualityBand>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (MultiselectList))]
  public class MultiselectList : ItemsControl
  {
    /// <summary>
    /// Identifies the IsSelectionEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty IsInSelectionModeProperty = DependencyProperty.Register(nameof (IsSelectionEnabled), typeof (bool), typeof (MultiselectList), new PropertyMetadata((object) false, new PropertyChangedCallback(MultiselectList.OnIsSelectionEnabledPropertyChanged)));
    /// <summary>Identifies the ItemInfoTemplate dependency property.</summary>
    public static readonly DependencyProperty ItemInfoTemplateProperty = DependencyProperty.Register(nameof (ItemInfoTemplate), typeof (DataTemplate), typeof (MultiselectList), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the ItemContainerStyle dependency property.
    /// </summary>
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(nameof (ItemContainerStyle), typeof (Style), typeof (MultiselectList), new PropertyMetadata((object) null, (PropertyChangedCallback) null));

    /// <summary>Collection of items that are currently selected.</summary>
    public IList SelectedItems { get; private set; }

    /// <summary>
    /// Occurs when there is a change to the SelectedItems collection.
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>Occurs when the selection mode is opened or closed.</summary>
    public event DependencyPropertyChangedEventHandler IsSelectionEnabledChanged;

    /// <summary>
    /// Gets or sets the flag that indicates if the list
    /// is in selection mode or not.
    /// </summary>
    public bool IsSelectionEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(MultiselectList.IsInSelectionModeProperty);
      set => ((DependencyObject) this).SetValue(MultiselectList.IsInSelectionModeProperty, (object) value);
    }

    /// <summary>
    /// Opens or closes the selection mode accordingly.
    /// If closing, it unselects any selected item.
    /// Finally, it fires up an IsSelectionEnabledChanged event.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsSelectionEnabledPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      MultiselectList multiselectList = (MultiselectList) obj;
      if ((bool) e.NewValue)
      {
        multiselectList.OpenSelection();
      }
      else
      {
        if (multiselectList.SelectedItems.Count > 0)
        {
          IList removedItems = (IList) new List<object>();
          foreach (object selectedItem in (IEnumerable) multiselectList.SelectedItems)
            removedItems.Add(selectedItem);
          for (int index = 0; index < ((PresentationFrameworkCollection<object>) multiselectList.Items).Count && multiselectList.SelectedItems.Count > 0; ++index)
          {
            MultiselectItem multiselectItem = (MultiselectItem) multiselectList.ItemContainerGenerator.ContainerFromIndex(index);
            if (multiselectItem != null && multiselectItem.IsSelected)
            {
              multiselectItem._canTriggerSelectionChanged = false;
              multiselectItem.IsSelected = false;
              multiselectItem._canTriggerSelectionChanged = true;
            }
          }
          multiselectList.SelectedItems.Clear();
          multiselectList.OnSelectionChanged(removedItems, (IList) new object[0]);
        }
        multiselectList.CloseSelection();
      }
      DependencyPropertyChangedEventHandler selectionEnabledChanged = multiselectList.IsSelectionEnabledChanged;
      if (selectionEnabledChanged == null)
        return;
      selectionEnabledChanged((object) obj, e);
    }

    /// <summary>
    /// Gets or sets the data template that is to be
    /// used on the item information field of the MultiselectItems.
    /// </summary>
    public DataTemplate ItemInfoTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(MultiselectList.ItemInfoTemplateProperty);
      set => ((DependencyObject) this).SetValue(MultiselectList.ItemInfoTemplateProperty, (object) value);
    }

    /// <summary>Gets or sets the item container style.</summary>
    public Style ItemContainerStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(MultiselectList.ItemContainerStyleProperty);
      set => ((DependencyObject) this).SetValue(MultiselectList.ItemContainerStyleProperty, (object) value);
    }

    /// <summary>
    /// Initializes a new instance of the MultiselectList class.
    /// </summary>
    public MultiselectList()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (MultiselectList);
      this.SelectedItems = (IList) new List<object>();
    }

    /// <summary>
    /// Toogles the selection mode based on the count of selected items,
    /// and fires a SelectionChanged event.
    /// </summary>
    /// <param name="removedItems">
    /// A collection containing the items that were unselected.
    /// </param>
    /// <param name="addedItems">
    /// A collection containing the items that were selected.
    /// </param>
    internal void OnSelectionChanged(IList removedItems, IList addedItems)
    {
      if (this.SelectedItems.Count <= 0)
        this.IsSelectionEnabled = false;
      else if (this.SelectedItems.Count == 1 && removedItems.Count == 0)
        this.IsSelectionEnabled = true;
      SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged((object) this, new SelectionChangedEventArgs(removedItems, addedItems));
    }

    /// <summary>
    /// Triggers the visual state changes and visual transitions
    /// to open the list into selection mode.
    /// </summary>
    private void OpenSelection()
    {
      foreach (WeakReference weakReference in (IEnumerable<WeakReference>) ItemsControlExtensions.GetItemsInViewPort((ItemsControl) this))
      {
        MultiselectItem target = (MultiselectItem) weakReference.Target;
        target.State = SelectionEnabledState.Opened;
        target.UpdateVisualState(true);
      }
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
      {
        for (int index = 0; index < ((PresentationFrameworkCollection<object>) this.Items).Count; ++index)
        {
          MultiselectItem multiselectItem = (MultiselectItem) this.ItemContainerGenerator.ContainerFromIndex(index);
          if (multiselectItem != null)
          {
            multiselectItem.State = SelectionEnabledState.Opened;
            multiselectItem.UpdateVisualState(false);
          }
        }
      }));
    }

    /// <summary>
    /// Triggers the visual state changes and visual transitions
    /// to close the list out of selection mode.
    /// </summary>
    private void CloseSelection()
    {
      foreach (WeakReference weakReference in (IEnumerable<WeakReference>) ItemsControlExtensions.GetItemsInViewPort((ItemsControl) this))
      {
        MultiselectItem target = (MultiselectItem) weakReference.Target;
        target.State = SelectionEnabledState.Closed;
        target.UpdateVisualState(true);
      }
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
      {
        for (int index = 0; index < ((PresentationFrameworkCollection<object>) this.Items).Count; ++index)
        {
          MultiselectItem multiselectItem = (MultiselectItem) this.ItemContainerGenerator.ContainerFromIndex(index);
          if (multiselectItem != null)
          {
            multiselectItem.State = SelectionEnabledState.Closed;
            multiselectItem.UpdateVisualState(false);
          }
        }
      }));
    }

    /// <summary>
    /// Overrides the DependencyObject used by this ItemsControl
    /// to be a MultiselectItem.
    /// </summary>
    /// <returns>A new instance of a MultiselectItem.</returns>
    protected virtual DependencyObject GetContainerForItemOverride() => (DependencyObject) new MultiselectItem();

    /// <summary>
    /// Acknowledges an item as being of the same type as its container
    /// if it is a MultiselectItem.
    /// </summary>
    /// <param name="item">An item owned by the ItemsControl.</param>
    /// <returns>True if the item is a MultiselectItem.</returns>
    protected virtual bool IsItemItsOwnContainerOverride(object item) => item is MultiselectItem;

    /// <summary>Resets new or reused item containers appropiately.</summary>
    /// <param name="element"></param>
    /// <param name="item"></param>
    protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      base.PrepareContainerForItemOverride(element, item);
      MultiselectItem multiselectItem = (MultiselectItem) element;
      ((FrameworkElement) multiselectItem).Style = this.ItemContainerStyle;
      multiselectItem._isBeingVirtualized = true;
      multiselectItem.IsSelected = this.SelectedItems.Contains(item);
      multiselectItem.State = this.IsSelectionEnabled ? SelectionEnabledState.Opened : SelectionEnabledState.Closed;
      multiselectItem.UpdateVisualState(false);
      multiselectItem._isBeingVirtualized = false;
    }

    /// <summary>
    /// Unselects any selected item which was removed from the source.
    /// </summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
      base.OnItemsChanged(e);
      if (this.SelectedItems.Count <= 0)
        return;
      IList removedItems = (IList) new List<object>();
      for (int index = 0; index < this.SelectedItems.Count; ++index)
      {
        object selectedItem = this.SelectedItems[index];
        if (!((PresentationFrameworkCollection<object>) this.Items).Contains(selectedItem))
        {
          this.SelectedItems.Remove(selectedItem);
          removedItems.Add(selectedItem);
          --index;
        }
      }
      this.OnSelectionChanged(removedItems, (IList) new object[0]);
    }
  }
}
