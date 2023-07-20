// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MultiselectItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Microsoft.Phone.Controls
{
  /// <summary>An item container for a Multiselect List.</summary>
  /// <QualityBand>Experimental</QualityBand>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
  [TemplateVisualState(Name = "Closed", GroupName = "SelectionEnabledStates")]
  [TemplateVisualState(Name = "Exposed", GroupName = "SelectionEnabledStates")]
  [TemplateVisualState(Name = "Opened", GroupName = "SelectionEnabledStates")]
  [TemplatePart(Name = "OutterHintPanel", Type = typeof (Rectangle))]
  [TemplatePart(Name = "InnerHintPanel", Type = typeof (Rectangle))]
  [TemplatePart(Name = "OutterCover", Type = typeof (Grid))]
  [TemplatePart(Name = "InfoPresenter", Type = typeof (ContentControl))]
  public class MultiselectItem : ContentControl
  {
    /// <summary>Selection mode visual states.</summary>
    private const string SelectionEnabledStates = "SelectionEnabledStates";
    /// <summary>Closed visual state.</summary>
    private const string Closed = "Closed";
    /// <summary>Exposed visual state.</summary>
    private const string Exposed = "Exposed";
    /// <summary>Opened visual state.</summary>
    private const string Opened = "Opened";
    /// <summary>Select Box template part name.</summary>
    private const string SelectBox = "SelectBox";
    /// <summary>Outter Hint Panel template part name.</summary>
    private const string OutterHintPanel = "OutterHintPanel";
    /// <summary>Inner Hint Panel template part name.</summary>
    private const string InnerHintPanel = "InnerHintPanel";
    /// <summary>Outter Cover template part name.</summary>
    private const string OutterCover = "OutterCover";
    /// <summary>Item Info Presenter template part name.</summary>
    private const string InfoPresenter = "InfoPresenter";
    /// <summary>Limit for the manipulation delta in the X-axis.</summary>
    private const double _deltaLimitX = 0.0;
    /// <summary>Limit for the manipulation delta in the Y-axis.</summary>
    private const double _deltaLimitY = 0.4;
    /// <summary>Outter Hint Panel template part.</summary>
    private Rectangle _outterHintPanel;
    /// <summary>Inner Hint Panel template part.</summary>
    private Rectangle _innerHintPanel;
    /// <summary>Outter Cover template part.</summary>
    private Grid _outterCover;
    /// <summary>Item Info Presenter template part.</summary>
    private ContentControl _infoPresenter;
    /// <summary>Multiselect List that owns this Multiselect Item.</summary>
    private MultiselectList _parent;
    /// <summary>Manipulation delta in the x-axis.</summary>
    private double _manipulationDeltaX;
    /// <summary>Manipulation delta in the y-axis.</summary>
    private double _manipulationDeltaY;
    /// <summary>
    /// Indicates that this Multiselect Item is a container
    /// being reused for virtualization.
    /// </summary>
    internal bool _isBeingVirtualized;
    /// <summary>
    /// Flag that is used to prevent multiple selection changed
    /// events from being fired when all the items in the list are
    /// unselected. Instead, a single event is fired.
    /// </summary>
    internal bool _canTriggerSelectionChanged = true;
    /// <summary>Identifies the IsSelected dependency property.</summary>
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (MultiselectItem), new PropertyMetadata((object) false, new PropertyChangedCallback(MultiselectItem.OnIsSelectedPropertyChanged)));
    /// <summary>Identifies the State dependency property.</summary>
    internal static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof (State), typeof (SelectionEnabledState), typeof (MultiselectItem), new PropertyMetadata((object) SelectionEnabledState.Closed, (PropertyChangedCallback) null));
    /// <summary>Identifies the HintPanelHeight dependency property.</summary>
    public static readonly DependencyProperty HintPanelHeightProperty = DependencyProperty.Register(nameof (HintPanelHeight), typeof (double), typeof (MultiselectItem), new PropertyMetadata((object) double.NaN, (PropertyChangedCallback) null));
    /// <summary>Identifies the ContentInfo dependency property.</summary>
    public static readonly DependencyProperty ContentInfoProperty = DependencyProperty.Register(nameof (ContentInfo), typeof (object), typeof (MultiselectItem), new PropertyMetadata((object) null, new PropertyChangedCallback(MultiselectItem.OnContentInfoPropertyChanged)));
    /// <summary>
    /// Identifies the ContentInfoTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentInfoTemplateProperty = DependencyProperty.Register(nameof (ContentInfoTemplate), typeof (DataTemplate), typeof (MultiselectItem), new PropertyMetadata((object) null, new PropertyChangedCallback(MultiselectItem.OnContentInfoTemplatePropertyChanged)));

    /// <summary>Occurs when the multiselect item is selected.</summary>
    public event RoutedEventHandler Selected;

    /// <summary>Occurs when the multiselect item is unselected.</summary>
    public event RoutedEventHandler Unselected;

    /// <summary>
    /// Gets or sets the flag that indicates if the item
    /// is currently selected.
    /// </summary>
    public bool IsSelected
    {
      get => (bool) ((DependencyObject) this).GetValue(MultiselectItem.IsSelectedProperty);
      set => ((DependencyObject) this).SetValue(MultiselectItem.IsSelectedProperty, (object) value);
    }

    /// <summary>
    /// Adds or removes the item to the selected items collection
    /// in the Multiselect List that owns it.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsSelectedPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      MultiselectItem multiselectItem = (MultiselectItem) obj;
      RoutedEventArgs e1 = new RoutedEventArgs();
      bool newValue = (bool) e.NewValue;
      if (newValue)
        multiselectItem.OnSelected(e1);
      else
        multiselectItem.OnUnselected(e1);
      if (multiselectItem._parent == null || multiselectItem._isBeingVirtualized)
        return;
      if (newValue)
      {
        multiselectItem._parent.SelectedItems.Add(multiselectItem.Content);
        if (multiselectItem._canTriggerSelectionChanged)
          multiselectItem._parent.OnSelectionChanged((IList) new object[0], (IList) new object[1]
          {
            multiselectItem.Content
          });
      }
      else
      {
        multiselectItem._parent.SelectedItems.Remove(multiselectItem.Content);
        if (multiselectItem._canTriggerSelectionChanged)
          multiselectItem._parent.OnSelectionChanged((IList) new object[1]
          {
            multiselectItem.Content
          }, (IList) new object[0]);
      }
    }

    /// <summary>Gets or sets the visual state.</summary>
    internal SelectionEnabledState State
    {
      get => (SelectionEnabledState) ((DependencyObject) this).GetValue(MultiselectItem.StateProperty);
      set => ((DependencyObject) this).SetValue(MultiselectItem.StateProperty, (object) value);
    }

    /// <summary>Gets or sets the height of the hint panel.</summary>
    public double HintPanelHeight
    {
      get => (double) ((DependencyObject) this).GetValue(MultiselectItem.HintPanelHeightProperty);
      set => ((DependencyObject) this).SetValue(MultiselectItem.HintPanelHeightProperty, (object) value);
    }

    /// <summary>
    /// Sets the vertical alignment of the hint panels to stretch if the
    /// height is not manually set. If it is, the alignment is set to top.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnHintPanelHeightPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      MultiselectItem multiselectItem = (MultiselectItem) obj;
      if (multiselectItem._outterHintPanel != null)
      {
        if (double.IsNaN((double) e.NewValue))
          ((FrameworkElement) multiselectItem._outterHintPanel).VerticalAlignment = (VerticalAlignment) 3;
        else
          ((FrameworkElement) multiselectItem._outterHintPanel).VerticalAlignment = (VerticalAlignment) 0;
      }
      if (multiselectItem._innerHintPanel == null)
        return;
      if (double.IsNaN(multiselectItem.HintPanelHeight))
        ((FrameworkElement) multiselectItem._innerHintPanel).VerticalAlignment = (VerticalAlignment) 3;
      else
        ((FrameworkElement) multiselectItem._innerHintPanel).VerticalAlignment = (VerticalAlignment) 0;
    }

    /// <summary>Gets or sets the content information.</summary>
    public object ContentInfo
    {
      get => ((DependencyObject) this).GetValue(MultiselectItem.ContentInfoProperty);
      set => ((DependencyObject) this).SetValue(MultiselectItem.ContentInfoProperty, value);
    }

    /// <summary>ContentInfoProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnContentInfoPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((MultiselectItem) obj).OnContentInfoChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// Gets or sets the data template that defines
    /// the content information.
    /// </summary>
    public DataTemplate ContentInfoTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(MultiselectItem.ContentInfoTemplateProperty);
      set => ((DependencyObject) this).SetValue(MultiselectItem.ContentInfoTemplateProperty, (object) value);
    }

    /// <summary>ContentInfoTemplate changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnContentInfoTemplatePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((MultiselectItem) obj).OnContentInfoTemplateChanged(e.OldValue as DataTemplate, e.NewValue as DataTemplate);
    }

    /// <summary>Gets the template parts and sets event handlers.</summary>
    public virtual void OnApplyTemplate()
    {
      this._parent = ItemsControlExtensions.GetParentItemsControl<MultiselectList>((DependencyObject) this);
      if (this._innerHintPanel != null)
      {
        ((UIElement) this._innerHintPanel).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.HintPanel_ManipulationStarted);
        ((UIElement) this._innerHintPanel).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.HintPanel_ManipulationDelta);
        ((UIElement) this._innerHintPanel).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.HintPanel_ManipulationCompleted);
      }
      if (this._outterHintPanel != null)
      {
        ((UIElement) this._outterHintPanel).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.HintPanel_ManipulationStarted);
        ((UIElement) this._outterHintPanel).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.HintPanel_ManipulationDelta);
        ((UIElement) this._outterHintPanel).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.HintPanel_ManipulationCompleted);
      }
      if (this._outterCover != null)
        ((UIElement) this._outterCover).Tap -= new EventHandler<GestureEventArgs>(this.Cover_Tap);
      this._innerHintPanel = ((Control) this).GetTemplateChild("InnerHintPanel") as Rectangle;
      this._outterHintPanel = ((Control) this).GetTemplateChild("OutterHintPanel") as Rectangle;
      this._outterCover = ((Control) this).GetTemplateChild("OutterCover") as Grid;
      this._infoPresenter = ((Control) this).GetTemplateChild("InfoPresenter") as ContentControl;
      ((FrameworkElement) this).OnApplyTemplate();
      if (this._innerHintPanel != null)
      {
        ((UIElement) this._innerHintPanel).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.HintPanel_ManipulationStarted);
        ((UIElement) this._innerHintPanel).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.HintPanel_ManipulationDelta);
        ((UIElement) this._innerHintPanel).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.HintPanel_ManipulationCompleted);
      }
      if (this._outterHintPanel != null)
      {
        ((UIElement) this._outterHintPanel).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.HintPanel_ManipulationStarted);
        ((UIElement) this._outterHintPanel).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.HintPanel_ManipulationDelta);
        ((UIElement) this._outterHintPanel).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.HintPanel_ManipulationCompleted);
      }
      if (this._outterCover != null)
        ((UIElement) this._outterCover).Tap += new EventHandler<GestureEventArgs>(this.Cover_Tap);
      if (this.ContentInfo == null && this._parent != null && this._parent.ItemInfoTemplate != null)
      {
        this._infoPresenter.ContentTemplate = this._parent.ItemInfoTemplate;
        Binding binding = new Binding();
        ((FrameworkElement) this).SetBinding(MultiselectItem.ContentInfoProperty, binding);
      }
      if (this._outterHintPanel != null)
      {
        if (double.IsNaN(this.HintPanelHeight))
          ((FrameworkElement) this._outterHintPanel).VerticalAlignment = (VerticalAlignment) 3;
        else
          ((FrameworkElement) this._outterHintPanel).VerticalAlignment = (VerticalAlignment) 0;
      }
      if (this._innerHintPanel != null)
      {
        if (double.IsNaN(this.HintPanelHeight))
          ((FrameworkElement) this._innerHintPanel).VerticalAlignment = (VerticalAlignment) 3;
        else
          ((FrameworkElement) this._innerHintPanel).VerticalAlignment = (VerticalAlignment) 0;
      }
      this.UpdateVisualState(false);
    }

    /// <summary>
    /// Initializes a new instance of the MultiselectItem class.
    /// </summary>
    public MultiselectItem() => ((Control) this).DefaultStyleKey = (object) typeof (MultiselectItem);

    /// <summary>Updates the visual state.</summary>
    /// <param name="useTransitions">
    /// Indicates whether visual transitions should be used.
    /// </param>
    internal void UpdateVisualState(bool useTransitions)
    {
      string str;
      switch (this.State)
      {
        case SelectionEnabledState.Closed:
          str = "Closed";
          break;
        case SelectionEnabledState.Exposed:
          str = "Exposed";
          break;
        case SelectionEnabledState.Opened:
          str = "Opened";
          break;
        default:
          str = "Closed";
          break;
      }
      VisualStateManager.GoToState((Control) this, str, useTransitions);
    }

    /// <summary>Raises a routed event.</summary>
    /// <param name="handler">Event handler.</param>
    /// <param name="args">Event arguments.</param>
    private void RaiseEvent(RoutedEventHandler handler, RoutedEventArgs args)
    {
      if (handler == null)
        return;
      handler((object) this, args);
    }

    /// <summary>
    /// Raises a Selected event when the IsSelected property
    /// changes from false to true.
    /// </summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnSelected(RoutedEventArgs e)
    {
      if (this._parent == null)
      {
        this.State = SelectionEnabledState.Opened;
        this.UpdateVisualState(true);
      }
      this.RaiseEvent(this.Selected, e);
    }

    /// <summary>
    /// Raises an Unselected event when the IsSelected property
    /// changes from true to false.
    /// </summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnUnselected(RoutedEventArgs e)
    {
      if (this._parent == null)
      {
        this.State = SelectionEnabledState.Closed;
        this.UpdateVisualState(true);
      }
      this.RaiseEvent(this.Unselected, e);
    }

    /// <summary>
    /// Called when the value of the ContentInfo property changes.
    /// </summary>
    /// <param name="oldContentInfo">
    /// The old value of the ContentInfo property.
    /// </param>
    /// <param name="newContentInfo">
    /// The new value of the ContentInfo property.
    /// </param>
    protected virtual void OnContentInfoChanged(object oldContentInfo, object newContentInfo)
    {
    }

    /// <summary>
    /// Called when the value of the ContentInfoTemplate property chages.
    /// </summary>
    /// <param name="oldContentInfoTemplate">
    /// The old value of the ContentInfoTemplate property.
    /// </param>
    /// <param name="newContentInfoTemplate">
    /// The new value of the ContentInfoTemplate property.
    /// </param>
    protected virtual void OnContentInfoTemplateChanged(
      DataTemplate oldContentInfoTemplate,
      DataTemplate newContentInfoTemplate)
    {
    }

    /// <summary>
    /// Triggers a visual transition to the Exposed visual state.
    /// </summary>
    /// <param name="sender">The Hint Panel that triggers the event.</param>
    /// <param name="e">The event information.</param>
    private void HintPanel_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this.State = SelectionEnabledState.Exposed;
      this.UpdateVisualState(true);
    }

    /// <summary>
    /// Triggers a visual transition to the Closed visual state
    /// if the manipulation delta goes out of bounds.
    /// </summary>
    /// <param name="sender">The Hint Panel that triggers the event.</param>
    /// <param name="e">The event information.</param>
    private void HintPanel_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      this._manipulationDeltaX = e.DeltaManipulation.Translation.X;
      this._manipulationDeltaY = e.DeltaManipulation.Translation.Y;
      if (this._manipulationDeltaX < 0.0)
        this._manipulationDeltaX *= -1.0;
      if (this._manipulationDeltaY < 0.0)
        this._manipulationDeltaY *= -1.0;
      if (this._manipulationDeltaX <= 0.0 && this._manipulationDeltaY < 0.4)
        return;
      this.State = SelectionEnabledState.Closed;
      this.UpdateVisualState(true);
    }

    /// <summary>
    /// Selects this MultiselectItem if the manipulation delta
    /// is within limits and fires an OnSelectionChanged event accordingly.
    /// Resets the deltas for both axises.
    /// </summary>
    /// <param name="sender">The Hint Panel that triggers the event.</param>
    /// <param name="e">The event information.</param>
    private void HintPanel_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (this._manipulationDeltaX == 0.0 && this._manipulationDeltaY < 0.4)
        this.IsSelected = true;
      this._manipulationDeltaX = 0.0;
      this._manipulationDeltaY = 0.0;
    }

    /// <summary>
    /// Toogles the selection of a MultiselectItem
    /// and fires an OnSelectionChanged event accordingly.
    /// </summary>
    /// <param name="sender">The cover that triggers the event.</param>
    /// <param name="e">The event information.</param>
    private void Cover_Tap(object sender, GestureEventArgs e) => this.IsSelected = !this.IsSelected;
  }
}
