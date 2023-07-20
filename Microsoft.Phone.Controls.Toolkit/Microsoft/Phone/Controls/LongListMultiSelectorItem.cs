// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.LongListMultiSelectorItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Microsoft.Phone.Controls
{
  /// <summary>Class for LongListMultiSelector items</summary>
  [TemplatePart(Name = "InnerHintPanel", Type = typeof (Rectangle))]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
  [TemplatePart(Name = "InfoPresenter", Type = typeof (ContentControl))]
  [TemplateVisualState(Name = "Opened", GroupName = "HasSelectionStates")]
  [TemplateVisualState(Name = "Closed", GroupName = "HasSelectionStates")]
  [TemplateVisualState(Name = "Exposed", GroupName = "ManipulationStates")]
  [TemplateVisualState(Name = "Selected", GroupName = "SelectionStates")]
  [TemplateVisualState(Name = "Unselected", GroupName = "SelectionStates")]
  [TemplatePart(Name = "ContentContainer", Type = typeof (ContentControl))]
  [TemplatePart(Name = "OuterHintPanel", Type = typeof (Rectangle))]
  [TemplatePart(Name = "OuterCover", Type = typeof (Grid))]
  public class LongListMultiSelectorItem : ContentControl
  {
    private const string HasSelectionStatesesName = "HasSelectionStates";
    private const string OpenedStateName = "Opened";
    private const string ClosedStateName = "Closed";
    private const string ExposedStateName = "Exposed";
    private const string ManipulationStatesName = "ManipulationStates";
    private const string SelectionStatesName = "SelectionStates";
    private const string SelectedStateName = "Selected";
    private const string UnselectedStateName = "Unselected";
    private const string ContentContainerName = "ContentContainer";
    private const string OuterHintPanelName = "OuterHintPanel";
    private const string InnerHintPanelName = "InnerHintPanel";
    private const string OuterCoverName = "OuterCover";
    private const string InfoPresenterName = "InfoPresenter";
    /// <summary>Limit for the manipulation delta in the Y-axis.</summary>
    private const double _translationYLimit = 0.4;
    /// <summary>Outer Hint Panel template part.</summary>
    private Rectangle _outerHintPanel = (Rectangle) null;
    /// <summary>Inner Hint Panel template part.</summary>
    private Rectangle _innerHintPanel = (Rectangle) null;
    /// <summary>Outer Cover template part.</summary>
    private Grid _outerCover = (Grid) null;
    /// <summary>
    /// Indicator for SelectPanel manipulation : true if still inside acceptable limits and will trigger Selection.
    /// </summary>
    private bool _insideAndDown = false;
    /// <summary>
    /// Flag used to restore state when the current style is changed
    /// </summary>
    private bool _isOpened = false;
    /// <summary>Weak Reference used by the container</summary>
    private WeakReference<LongListMultiSelectorItem> _wr = (WeakReference<LongListMultiSelectorItem>) null;
    /// <summary>Identifies the ContentInfo dependency property.</summary>
    public static readonly DependencyProperty ContentInfoProperty = DependencyProperty.Register(nameof (ContentInfo), typeof (object), typeof (LongListMultiSelectorItem), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the ContentInfoTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty ContentInfoTemplateProperty = DependencyProperty.Register(nameof (ContentInfoTemplate), typeof (DataTemplate), typeof (LongListMultiSelectorItem), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>Identifies the IsSelected dependency property.</summary>
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (LongListMultiSelectorItem), new PropertyMetadata((object) false, new PropertyChangedCallback(LongListMultiSelectorItem.OnIsSelectedPropertyChanged)));
    /// <summary>Identifies the HintPanelHeight dependency property.</summary>
    public static readonly DependencyProperty HintPanelHeightProperty = DependencyProperty.Register(nameof (HintPanelHeight), typeof (double), typeof (LongListMultiSelectorItem), new PropertyMetadata((object) double.NaN, new PropertyChangedCallback(LongListMultiSelectorItem.OnHintPanelHeightPropertyChanged)));

    /// <summary>Weak Reference used by the container</summary>
    internal WeakReference<LongListMultiSelectorItem> WR
    {
      get
      {
        if (this._wr == null)
          this._wr = new WeakReference<LongListMultiSelectorItem>(this);
        return this._wr;
      }
    }

    /// <summary>Gets or sets the content information.</summary>
    public object ContentInfo
    {
      get => ((DependencyObject) this).GetValue(LongListMultiSelectorItem.ContentInfoProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelectorItem.ContentInfoProperty, value);
    }

    /// <summary>
    /// Gets or sets the data template that defines
    /// the content information.
    /// </summary>
    public DataTemplate ContentInfoTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LongListMultiSelectorItem.ContentInfoTemplateProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelectorItem.ContentInfoTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the flag indicating that the item is selected
    /// </summary>
    public bool IsSelected
    {
      get => (bool) ((DependencyObject) this).GetValue(LongListMultiSelectorItem.IsSelectedProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelectorItem.IsSelectedProperty, (object) value);
    }

    /// <summary>Called then the property is changed</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnIsSelectedPropertyChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelectorItem multiSelectorItem))
        return;
      multiSelectorItem.OnIsSelectedChanged();
    }

    /// <summary>Gets or sets the height of the hint panel.</summary>
    public double HintPanelHeight
    {
      get => (double) ((DependencyObject) this).GetValue(LongListMultiSelectorItem.HintPanelHeightProperty);
      set => ((DependencyObject) this).SetValue(LongListMultiSelectorItem.HintPanelHeightProperty, (object) value);
    }

    /// <summary>Handles the change of the HintPanelHeight property</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void OnHintPanelHeightPropertyChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is LongListMultiSelectorItem multiSelectorItem))
        return;
      multiSelectorItem.OnHintPanelHeightChanged();
    }

    /// <summary>Triggered when the IsSelected property has changed</summary>
    public event EventHandler IsSelectedChanged;

    /// <summary>Constructor</summary>
    public LongListMultiSelectorItem() => ((Control) this).DefaultStyleKey = (object) typeof (LongListMultiSelectorItem);

    /// <summary>
    /// Template application : hooks events which need to be handled
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (this._outerHintPanel != null)
      {
        ((UIElement) this._outerHintPanel).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnSelectPanelManipulationStarted);
        ((UIElement) this._outerHintPanel).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.OnSelectPanelManipulationDelta);
        ((UIElement) this._outerHintPanel).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnSelectPanelManipulationCompleted);
      }
      if (this._innerHintPanel != null)
      {
        ((UIElement) this._innerHintPanel).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnSelectPanelManipulationStarted);
        ((UIElement) this._innerHintPanel).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.OnSelectPanelManipulationDelta);
        ((UIElement) this._innerHintPanel).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnSelectPanelManipulationCompleted);
      }
      if (this._outerCover != null)
        ((UIElement) this._outerCover).Tap -= new EventHandler<GestureEventArgs>(this.OnCoverTap);
      this._outerHintPanel = ((Control) this).GetTemplateChild("OuterHintPanel") as Rectangle;
      if (this._outerHintPanel != null)
      {
        ((UIElement) this._outerHintPanel).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnSelectPanelManipulationStarted);
        ((UIElement) this._outerHintPanel).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnSelectPanelManipulationDelta);
        ((UIElement) this._outerHintPanel).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnSelectPanelManipulationCompleted);
      }
      this._innerHintPanel = ((Control) this).GetTemplateChild("InnerHintPanel") as Rectangle;
      if (this._innerHintPanel != null)
      {
        ((UIElement) this._innerHintPanel).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnSelectPanelManipulationStarted);
        ((UIElement) this._innerHintPanel).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnSelectPanelManipulationDelta);
        ((UIElement) this._innerHintPanel).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnSelectPanelManipulationCompleted);
      }
      this._outerCover = ((Control) this).GetTemplateChild("OuterCover") as Grid;
      if (this._outerCover != null)
        ((UIElement) this._outerCover).Tap += new EventHandler<GestureEventArgs>(this.OnCoverTap);
      this.OnHintPanelHeightChanged();
      this.GotoState(this._isOpened ? LongListMultiSelectorItem.State.Opened : LongListMultiSelectorItem.State.Closed);
      this.GotoState(this.IsSelected ? LongListMultiSelectorItem.State.Selected : LongListMultiSelectorItem.State.Unselected);
    }

    /// <summary>Updates the visual state of the item</summary>
    protected virtual void OnIsSelectedChanged()
    {
      this.GotoState(this.IsSelected ? LongListMultiSelectorItem.State.Selected : LongListMultiSelectorItem.State.Unselected, true);
      if (this.IsSelectedChanged == null)
        return;
      this.IsSelectedChanged((object) this, (EventArgs) null);
    }

    /// <summary>Tap on the cover grid : switch the selected state</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCoverTap(object sender, GestureEventArgs e) => this.IsSelected = !this.IsSelected;

    /// <summary>
    /// Triggers a visual transition to the Exposed visual state
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSelectPanelManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this._insideAndDown = true;
      this.GotoState(LongListMultiSelectorItem.State.Exposed, true);
    }

    /// <summary>
    /// Checks that the manipulation is still in correct bounds
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSelectPanelManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      int num;
      if (e.DeltaManipulation.Translation.X == 0.0)
      {
        Point translation = e.DeltaManipulation.Translation;
        if (translation.Y > -0.4)
        {
          translation = e.DeltaManipulation.Translation;
          num = translation.Y < 0.4 ? 1 : 0;
          goto label_4;
        }
      }
      num = 0;
label_4:
      if (num != 0)
        return;
      this._insideAndDown = false;
      this.GotoState(LongListMultiSelectorItem.State.Closed, true);
    }

    /// <summary>End of the manipulation, select the item</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnSelectPanelManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (!this._insideAndDown)
        return;
      this._insideAndDown = false;
      this.IsSelected = true;
    }

    /// <summary>Changes the visual state of the item</summary>
    /// <param name="state">New state</param>
    /// <param name="useTransitions">Indicates whether display or not transitions between states</param>
    internal void GotoState(LongListMultiSelectorItem.State state, bool useTransitions = false)
    {
      string str;
      switch (state)
      {
        case LongListMultiSelectorItem.State.Opened:
          this._isOpened = true;
          str = "Opened";
          break;
        case LongListMultiSelectorItem.State.Exposed:
          str = "Exposed";
          break;
        case LongListMultiSelectorItem.State.Selected:
          str = "Selected";
          break;
        case LongListMultiSelectorItem.State.Unselected:
          str = "Unselected";
          break;
        default:
          this._isOpened = false;
          str = "Closed";
          break;
      }
      VisualStateManager.GoToState((Control) this, str, useTransitions);
    }

    /// <summary>
    /// Sets the vertical alignment of the hint panels to stretch if the
    /// height is not manually set. If it is, the alignment is set to top.
    /// </summary>
    protected virtual void OnHintPanelHeightChanged()
    {
      if (this._outerHintPanel != null)
        ((FrameworkElement) this._outerHintPanel).VerticalAlignment = double.IsNaN(this.HintPanelHeight) ? (VerticalAlignment) 3 : (VerticalAlignment) 0;
      if (this._innerHintPanel == null)
        return;
      ((FrameworkElement) this._innerHintPanel).VerticalAlignment = double.IsNaN(this.HintPanelHeight) ? (VerticalAlignment) 3 : (VerticalAlignment) 0;
    }

    /// <summary>
    /// Finds the LongListMultiSelector to which the LongListMultiSelectorItem belongs
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected static LongListMultiSelector FindContainer(DependencyObject item)
    {
      while (item != null)
      {
        item = VisualTreeHelper.GetParent(item);
        if (item is LongListMultiSelector container)
          return container;
      }
      return (LongListMultiSelector) null;
    }

    /// <summary>
    /// Called when content is changed. This is a good place to get the style (which depends on the LLMS layout)
    /// because the controll template has not yet been expanded
    /// </summary>
    /// <param name="oldContent"></param>
    /// <param name="newContent"></param>
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      LongListMultiSelectorItem.FindContainer((DependencyObject) this)?.ConfigureItem(this);
      base.OnContentChanged(oldContent, newContent);
    }

    internal enum State
    {
      Opened,
      Exposed,
      Closed,
      Selected,
      Unselected,
    }
  }
}
