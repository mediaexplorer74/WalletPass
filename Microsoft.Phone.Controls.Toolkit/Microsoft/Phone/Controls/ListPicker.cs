// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ListPicker
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Class that implements a flexible list-picking experience with a custom interface for few/many items.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplatePart(Name = "ItemsPresenterTranslateTransform", Type = typeof (TranslateTransform))]
  [TemplateVisualState(GroupName = "PickerStates", Name = "Normal")]
  [TemplateVisualState(GroupName = "PickerStates", Name = "Highlighted")]
  [TemplateVisualState(GroupName = "PickerStates", Name = "Disabled")]
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "This is a complicated control.")]
  [TemplatePart(Name = "ItemsPresenter", Type = typeof (ItemsPresenter))]
  [TemplatePart(Name = "ItemsPresenterHost", Type = typeof (Canvas))]
  [TemplatePart(Name = "MultipleSelectionModeSummary", Type = typeof (TextBlock))]
  public class ListPicker : ItemsControl
  {
    private const string ItemsPresenterPartName = "ItemsPresenter";
    private const string ItemsPresenterTranslateTransformPartName = "ItemsPresenterTranslateTransform";
    private const string ItemsPresenterHostPartName = "ItemsPresenterHost";
    private const string MultipleSelectionModeSummaryPartName = "MultipleSelectionModeSummary";
    private const string BorderPartName = "Border";
    private const string PickerStatesGroupName = "PickerStates";
    private const string PickerStatesNormalStateName = "Normal";
    private const string PickerStatesHighlightedStateName = "Highlighted";
    private const string PickerStatesDisabledStateName = "Disabled";
    /// <summary>
    /// In Mango, the size of list pickers in expanded mode was given extra offset.
    /// </summary>
    private const double NormalModeOffset = 4.0;
    private readonly DoubleAnimation _heightAnimation = new DoubleAnimation();
    private readonly DoubleAnimation _translateAnimation = new DoubleAnimation();
    private readonly Storyboard _storyboard = new Storyboard();
    private PhoneApplicationFrame _frame;
    private PhoneApplicationPage _page;
    private FrameworkElement _itemsPresenterHostParent;
    private Canvas _itemsPresenterHostPart;
    private ItemsPresenter _itemsPresenterPart;
    private TranslateTransform _itemsPresenterTranslateTransformPart;
    private bool _updatingSelection;
    private int _deferredSelectedIndex = -1;
    private object _deferredSelectedItem = (object) null;
    private object _frameContentWhenOpened;
    private NavigationInTransition _savedNavigationInTransition;
    private NavigationOutTransition _savedNavigationOutTransition;
    private ListPickerPage _listPickerPage;
    private TextBlock _multipleSelectionModeSummary;
    private Border _border;
    /// <summary>Whether this list picker has the picker page opened.</summary>
    private bool _hasPickerPageOpen;
    /// <summary>
    /// Identifies the SummaryForSelectedItemsDelegate DependencyProperty.
    /// </summary>
    public static readonly DependencyProperty SummaryForSelectedItemsDelegateProperty = DependencyProperty.Register(nameof (SummaryForSelectedItemsDelegate), typeof (Func<IList, string>), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the ListPickerMode DependencyProperty.</summary>
    public static readonly DependencyProperty ListPickerModeProperty = DependencyProperty.Register(nameof (ListPickerMode), typeof (ListPickerMode), typeof (ListPicker), new PropertyMetadata((object) ListPickerMode.Normal, new PropertyChangedCallback(ListPicker.OnListPickerModeChanged)));
    private static readonly DependencyProperty IsHighlightedProperty = DependencyProperty.Register(nameof (IsHighlighted), typeof (bool), typeof (ListPicker), new PropertyMetadata((object) false, new PropertyChangedCallback(ListPicker.OnIsHighlightedChanged)));
    /// <summary>Identifies the SelectedIndex DependencyProperty.</summary>
    public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register(nameof (SelectedIndex), typeof (int), typeof (ListPicker), new PropertyMetadata((object) -1, new PropertyChangedCallback(ListPicker.OnSelectedIndexChanged)));
    /// <summary>Identifies the SelectedItem DependencyProperty.</summary>
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof (SelectedItem), typeof (object), typeof (ListPicker), new PropertyMetadata((object) null, new PropertyChangedCallback(ListPicker.OnSelectedItemChanged)));
    private static readonly DependencyProperty ShadowItemTemplateProperty = DependencyProperty.Register("ShadowItemTemplate", typeof (DataTemplate), typeof (ListPicker), new PropertyMetadata((object) null, new PropertyChangedCallback(ListPicker.OnShadowOrFullModeItemTemplateChanged)));
    /// <summary>
    /// Identifies the FullModeItemTemplate DependencyProperty.
    /// </summary>
    public static readonly DependencyProperty FullModeItemTemplateProperty = DependencyProperty.Register(nameof (FullModeItemTemplate), typeof (DataTemplate), typeof (ListPicker), new PropertyMetadata((object) null, new PropertyChangedCallback(ListPicker.OnShadowOrFullModeItemTemplateChanged)));
    private static readonly DependencyProperty ActualFullModeItemTemplateProperty = DependencyProperty.Register("ActualFullModeItemTemplate", typeof (DataTemplate), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the Header DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the HeaderTemplate DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(nameof (HeaderTemplate), typeof (DataTemplate), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the FullModeHeader DependencyProperty.</summary>
    public static readonly DependencyProperty FullModeHeaderProperty = DependencyProperty.Register(nameof (FullModeHeader), typeof (object), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the ItemCountThreshold DependencyProperty.</summary>
    public static readonly DependencyProperty ItemCountThresholdProperty = DependencyProperty.Register(nameof (ItemCountThreshold), typeof (int), typeof (ListPicker), new PropertyMetadata((object) 5, new PropertyChangedCallback(ListPicker.OnItemCountThresholdChanged)));
    /// <summary>Identifies the PickerPageUri DependencyProperty.</summary>
    public static readonly DependencyProperty PickerPageUriProperty = DependencyProperty.Register(nameof (PickerPageUri), typeof (Uri), typeof (ListPicker), (PropertyMetadata) null);
    /// <summary>Identifies the ExpansionMode DependencyProperty.</summary>
    public static readonly DependencyProperty ExpansionModeProperty = DependencyProperty.Register(nameof (ExpansionMode), typeof (ExpansionMode), typeof (ListPicker), new PropertyMetadata((object) ExpansionMode.ExpansionAllowed, (PropertyChangedCallback) null));
    /// <summary>Identifies the SelectionMode DependencyProperty.</summary>
    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(nameof (SelectionMode), typeof (SelectionMode), typeof (ListPicker), new PropertyMetadata((object) (SelectionMode) 0, new PropertyChangedCallback(ListPicker.OnSelectionModeChanged)));
    /// <summary>Identifies the SelectedItems DependencyProperty.</summary>
    public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(nameof (SelectedItems), typeof (IList), typeof (ListPicker), new PropertyMetadata(new PropertyChangedCallback(ListPicker.OnSelectedItemsChanged)));

    /// <summary>Event that is raised when the selection changes.</summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>
    /// Gets or sets the delegate, which is called to summarize a list of selections into a string.
    /// If not implemented, the default summarizing behavior will be used.
    /// If this delegate is implemented, default summarizing behavior can be achieved by returning
    /// null instead of a string.
    /// </summary>
    public Func<IList, string> SummaryForSelectedItemsDelegate
    {
      get => (Func<IList, string>) ((DependencyObject) this).GetValue(ListPicker.SummaryForSelectedItemsDelegateProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.SummaryForSelectedItemsDelegateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the ListPickerMode (ex: Normal/Expanded/Full).
    /// </summary>
    public ListPickerMode ListPickerMode
    {
      get => (ListPickerMode) ((DependencyObject) this).GetValue(ListPicker.ListPickerModeProperty);
      private set => ((DependencyObject) this).SetValue(ListPicker.ListPickerModeProperty, (object) value);
    }

    private static void OnListPickerModeChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnListPickerModeChanged((ListPickerMode) e.OldValue, (ListPickerMode) e.NewValue);
    }

    private void OnListPickerModeChanged(ListPickerMode oldValue, ListPickerMode newValue)
    {
      if (ListPickerMode.Expanded == oldValue)
      {
        if (null != this._page)
        {
          this._page.BackKeyPress -= new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
          this._page = (PhoneApplicationPage) null;
        }
        if (null != this._frame)
        {
          ((UIElement) this._frame).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnFrameManipulationStarted);
          this._frame = (PhoneApplicationFrame) null;
        }
      }
      if (ListPickerMode.Expanded == newValue)
      {
        if (null == this._frame)
        {
          this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
          if (null != this._frame)
            ((UIElement) this._frame).AddHandler(UIElement.ManipulationStartedEvent, (Delegate) new EventHandler<ManipulationStartedEventArgs>(this.OnFrameManipulationStarted), true);
        }
        if (null != this._frame)
        {
          this._page = ((ContentControl) this._frame).Content as PhoneApplicationPage;
          if (null != this._page)
            this._page.BackKeyPress += new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
        }
      }
      if (ListPickerMode.Full == oldValue)
        this.ClosePickerPage();
      if (ListPickerMode.Full == newValue)
        this.OpenPickerPage();
      this.SizeForAppropriateView(ListPickerMode.Full != oldValue);
      this.IsHighlighted = ListPickerMode.Expanded == newValue;
    }

    /// <summary>
    /// Whether the list picker is highlighted.
    /// This occurs when the user is manipulating the box or when in expanded mode.
    /// </summary>
    private bool IsHighlighted
    {
      get => (bool) ((DependencyObject) this).GetValue(ListPicker.IsHighlightedProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.IsHighlightedProperty, (object) value);
    }

    /// <summary>Highlight property changed</summary>
    private static void OnIsHighlightedChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      (o as ListPicker).OnIsHighlightedChanged();
    }

    /// <summary>Highlight property changed</summary>
    private void OnIsHighlightedChanged() => this.UpdateVisualStates(true);

    /// <summary>Enabled property changed</summary>
    private static void OnIsEnabledChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) => (o as ListPicker).OnIsEnabledChanged();

    /// <summary>Enabled property changed</summary>
    private void OnIsEnabledChanged() => this.UpdateVisualStates(true);

    /// <summary>Gets or sets the index of the selected item.</summary>
    public int SelectedIndex
    {
      get => (int) ((DependencyObject) this).GetValue(ListPicker.SelectedIndexProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.SelectedIndexProperty, (object) value);
    }

    private static void OnSelectedIndexChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnSelectedIndexChanged((int) e.OldValue, (int) e.NewValue);
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SelectedIndex", Justification = "Property name.")]
    private void OnSelectedIndexChanged(int oldValue, int newValue)
    {
      if (((PresentationFrameworkCollection<object>) this.Items).Count <= newValue || 0 < ((PresentationFrameworkCollection<object>) this.Items).Count && newValue < 0 || ((PresentationFrameworkCollection<object>) this.Items).Count == 0 && newValue != -1)
      {
        this._deferredSelectedIndex = ((Control) this).Template == null && 0 <= newValue ? newValue : throw new InvalidOperationException(Resources.InvalidSelectedIndex);
      }
      else
      {
        if (!this._updatingSelection)
        {
          this._updatingSelection = true;
          this.SelectedItem = -1 != newValue ? ((PresentationFrameworkCollection<object>) this.Items)[newValue] : (object) null;
          this._updatingSelection = false;
        }
        if (-1 == oldValue)
          return;
        ListPickerItem listPickerItem = (ListPickerItem) this.ItemContainerGenerator.ContainerFromIndex(oldValue);
        if (null != listPickerItem)
          listPickerItem.IsSelected = false;
      }
    }

    /// <summary>Gets or sets the selected item.</summary>
    public object SelectedItem
    {
      get => ((DependencyObject) this).GetValue(ListPicker.SelectedItemProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.SelectedItemProperty, value);
    }

    private static void OnSelectedItemChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnSelectedItemChanged(e.OldValue, e.NewValue);
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SelectedItem", Justification = "Property name.")]
    private void OnSelectedItemChanged(object oldValue, object newValue)
    {
      if (newValue != null && (this.Items == null || ((PresentationFrameworkCollection<object>) this.Items).Count == 0))
      {
        if (null != ((Control) this).Template)
          throw new InvalidOperationException(Resources.InvalidSelectedItem);
        this._deferredSelectedItem = newValue;
      }
      else
      {
        int num = newValue != null ? ((PresentationFrameworkCollection<object>) this.Items).IndexOf(newValue) : -1;
        if (-1 == num && 0 < ((PresentationFrameworkCollection<object>) this.Items).Count)
          throw new InvalidOperationException(Resources.InvalidSelectedItem);
        if (!this._updatingSelection)
        {
          this._updatingSelection = true;
          this.SelectedIndex = num;
          this._updatingSelection = false;
        }
        if (ListPickerMode.Normal != this.ListPickerMode)
          this.ListPickerMode = ListPickerMode.Normal;
        else
          this.SizeForAppropriateView(false);
        SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
        if (null == selectionChanged)
          return;
        object[] objArray1;
        if (oldValue != null)
          objArray1 = new object[1]{ oldValue };
        else
          objArray1 = new object[0];
        IList list1 = (IList) objArray1;
        object[] objArray2;
        if (newValue != null)
          objArray2 = new object[1]{ newValue };
        else
          objArray2 = new object[0];
        IList list2 = (IList) objArray2;
        selectionChanged((object) this, new SelectionChangedEventArgs(list1, list2));
      }
    }

    /// <summary>
    /// Gets or sets the DataTemplate used to display each item when ListPickerMode is set to Full.
    /// </summary>
    public DataTemplate FullModeItemTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(ListPicker.FullModeItemTemplateProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.FullModeItemTemplateProperty, (object) value);
    }

    private static void OnShadowOrFullModeItemTemplateChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnShadowOrFullModeItemTemplateChanged();
    }

    private void OnShadowOrFullModeItemTemplateChanged() => ((DependencyObject) this).SetValue(ListPicker.ActualFullModeItemTemplateProperty, (object) (this.FullModeItemTemplate ?? this.ItemTemplate));

    /// <summary>Gets or sets the header of the control.</summary>
    public object Header
    {
      get => ((DependencyObject) this).GetValue(ListPicker.HeaderProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the control's header.
    /// </summary>
    public DataTemplate HeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(ListPicker.HeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.HeaderTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the header to use when ListPickerMode is set to Full.
    /// </summary>
    public object FullModeHeader
    {
      get => ((DependencyObject) this).GetValue(ListPicker.FullModeHeaderProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.FullModeHeaderProperty, value);
    }

    /// <summary>
    /// Gets the maximum number of items for which Expanded mode will be used, 5.
    /// </summary>
    public int ItemCountThreshold
    {
      get => (int) ((DependencyObject) this).GetValue(ListPicker.ItemCountThresholdProperty);
      private set => ((DependencyObject) this).SetValue(ListPicker.ItemCountThresholdProperty, (object) value);
    }

    private static void OnItemCountThresholdChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnItemCountThresholdChanged((int) e.NewValue);
    }

    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Following DependencyProperty property changed handler convention.")]
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "Providing the DependencyProperty name is preferred here.")]
    private void OnItemCountThresholdChanged(int newValue)
    {
      if (newValue < 0)
        throw new ArgumentOutOfRangeException("ItemCountThreshold");
    }

    /// <summary>
    /// Gets or sets the Uri to use for loading the ListPickerPage instance when the control is tapped.
    /// </summary>
    public Uri PickerPageUri
    {
      get => (Uri) ((DependencyObject) this).GetValue(ListPicker.PickerPageUriProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.PickerPageUriProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets how the list picker expands when tapped.
    /// This property has an effect only when SelectionMode is Single.
    /// When SelectionMode is Multiple, the ExpansionMode will be treated as FullScreenOnly.
    /// ExpansionAllowed will only expand when the number of items is less than or equalt to ItemCountThreshold
    /// Single by default.
    /// </summary>
    public ExpansionMode ExpansionMode
    {
      get => (ExpansionMode) ((DependencyObject) this).GetValue(ListPicker.ExpansionModeProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.ExpansionModeProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the SelectionMode. Extended is treated as Multiple.
    /// Single by default.
    /// </summary>
    public SelectionMode SelectionMode
    {
      get => (SelectionMode) ((DependencyObject) this).GetValue(ListPicker.SelectionModeProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.SelectionModeProperty, (object) value);
    }

    private static void OnSelectionModeChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnSelectionModeChanged((SelectionMode) e.NewValue);
    }

    private void OnSelectionModeChanged(SelectionMode newValue)
    {
      if (newValue == 1 || newValue == 2)
      {
        if (this._multipleSelectionModeSummary == null || this._itemsPresenterHostPart == null)
          return;
        ((UIElement) this._multipleSelectionModeSummary).Visibility = (Visibility) 0;
        ((UIElement) this._itemsPresenterHostPart).Visibility = (Visibility) 1;
      }
      else if (this._multipleSelectionModeSummary != null && this._itemsPresenterHostPart != null)
      {
        ((UIElement) this._multipleSelectionModeSummary).Visibility = (Visibility) 1;
        ((UIElement) this._itemsPresenterHostPart).Visibility = (Visibility) 0;
      }
    }

    /// <summary>Gets the selected items.</summary>
    [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Want to allow this to be bound to.")]
    public IList SelectedItems
    {
      get => (IList) ((DependencyObject) this).GetValue(ListPicker.SelectedItemsProperty);
      set => ((DependencyObject) this).SetValue(ListPicker.SelectedItemsProperty, (object) value);
    }

    private static void OnSelectedItemsChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ListPicker) o).OnSelectedItemsChanged((IList) e.OldValue, (IList) e.NewValue);
    }

    private void OnSelectedItemsChanged(IList oldValue, IList newValue)
    {
      this.UpdateSummary(newValue);
      SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
      if (null == selectionChanged)
        return;
      IList list1 = (IList) new List<object>();
      if (null != oldValue)
      {
        foreach (object obj in (IEnumerable) oldValue)
        {
          if (newValue == null || !newValue.Contains(obj))
            list1.Add(obj);
        }
      }
      IList list2 = (IList) new List<object>();
      if (null != newValue)
      {
        foreach (object obj in (IEnumerable) newValue)
        {
          if (oldValue == null || !oldValue.Contains(obj))
            list2.Add(obj);
        }
      }
      selectionChanged((object) this, new SelectionChangedEventArgs(list1, list2));
    }

    /// <summary>Initializes a new instance of the ListPicker class.</summary>
    public ListPicker()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (ListPicker);
      Storyboard.SetTargetProperty((Timeline) this._heightAnimation, new PropertyPath((object) FrameworkElement.HeightProperty));
      Storyboard.SetTargetProperty((Timeline) this._translateAnimation, new PropertyPath((object) TranslateTransform.YProperty));
      Duration duration = Duration.op_Implicit(TimeSpan.FromSeconds(0.2));
      ((Timeline) this._heightAnimation).Duration = duration;
      ((Timeline) this._translateAnimation).Duration = duration;
      ExponentialEase exponentialEase = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase).EasingMode = (EasingMode) 2;
      exponentialEase.Exponent = 4.0;
      IEasingFunction ieasingFunction = (IEasingFunction) exponentialEase;
      this._heightAnimation.EasingFunction = ieasingFunction;
      this._translateAnimation.EasingFunction = ieasingFunction;
      ((FrameworkElement) this).RegisterNotification("IsEnabled", new PropertyChangedCallback(ListPicker.OnIsEnabledChanged));
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.OnLoaded);
      ((FrameworkElement) this).Unloaded += new RoutedEventHandler(this.OnUnloaded);
    }

    private void OnLoaded(object sender, RoutedEventArgs e) => this.UpdateVisualStates(true);

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
      if (null == this._frame)
        return;
      ((UIElement) this._frame).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnFrameManipulationStarted);
      this._frame = (PhoneApplicationFrame) null;
    }

    /// <summary>
    /// Builds the visual tree for the control when a new template is applied.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      if (null != this._itemsPresenterHostParent)
        this._itemsPresenterHostParent.SizeChanged -= new SizeChangedEventHandler(this.OnItemsPresenterHostParentSizeChanged);
      this._storyboard.Stop();
      ((FrameworkElement) this).OnApplyTemplate();
      this._itemsPresenterPart = ((Control) this).GetTemplateChild("ItemsPresenter") as ItemsPresenter;
      this._itemsPresenterTranslateTransformPart = ((Control) this).GetTemplateChild("ItemsPresenterTranslateTransform") as TranslateTransform;
      this._itemsPresenterHostPart = ((Control) this).GetTemplateChild("ItemsPresenterHost") as Canvas;
      this._itemsPresenterHostParent = this._itemsPresenterHostPart != null ? ((FrameworkElement) this._itemsPresenterHostPart).Parent as FrameworkElement : (FrameworkElement) null;
      this._multipleSelectionModeSummary = ((Control) this).GetTemplateChild("MultipleSelectionModeSummary") as TextBlock;
      this._border = ((Control) this).GetTemplateChild("Border") as Border;
      if (null != this._itemsPresenterHostParent)
        this._itemsPresenterHostParent.SizeChanged += new SizeChangedEventHandler(this.OnItemsPresenterHostParentSizeChanged);
      if (null != this._itemsPresenterHostPart)
      {
        Storyboard.SetTarget((Timeline) this._heightAnimation, (DependencyObject) this._itemsPresenterHostPart);
        if (!((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Contains((Timeline) this._heightAnimation))
          ((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Add((Timeline) this._heightAnimation);
      }
      else if (((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Contains((Timeline) this._heightAnimation))
        ((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Remove((Timeline) this._heightAnimation);
      if (null != this._itemsPresenterTranslateTransformPart)
      {
        Storyboard.SetTarget((Timeline) this._translateAnimation, (DependencyObject) this._itemsPresenterTranslateTransformPart);
        if (!((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Contains((Timeline) this._translateAnimation))
          ((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Add((Timeline) this._translateAnimation);
      }
      else if (((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Contains((Timeline) this._translateAnimation))
        ((PresentationFrameworkCollection<Timeline>) this._storyboard.Children).Remove((Timeline) this._translateAnimation);
      ((FrameworkElement) this).SetBinding(ListPicker.ShadowItemTemplateProperty, new Binding("ItemTemplate")
      {
        Source = (object) this
      });
      if (-1 != this._deferredSelectedIndex)
      {
        this.SelectedIndex = this._deferredSelectedIndex;
        this._deferredSelectedIndex = -1;
      }
      if (null != this._deferredSelectedItem)
      {
        this.SelectedItem = this._deferredSelectedItem;
        this._deferredSelectedItem = (object) null;
      }
      this.OnSelectionModeChanged(this.SelectionMode);
      this.OnSelectedItemsChanged(this.SelectedItems, this.SelectedItems);
    }

    /// <summary>
    /// Determines if the specified item is (or is eligible to be) its own item container.
    /// </summary>
    /// <param name="item">The specified item.</param>
    /// <returns>True if the item is its own item container; otherwise, false.</returns>
    protected virtual bool IsItemItsOwnContainerOverride(object item) => item is ListPickerItem;

    /// <summary>
    /// Creates or identifies the element used to display a specified item.
    /// </summary>
    /// <returns>A container corresponding to a specified item.</returns>
    protected virtual DependencyObject GetContainerForItemOverride() => (DependencyObject) new ListPickerItem();

    /// <summary>
    /// Prepares the specified element to display the specified item.
    /// </summary>
    /// <param name="element">The element used to display the specified item.</param>
    /// <param name="item">The item to display.</param>
    protected virtual void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      base.PrepareContainerForItemOverride(element, item);
      ContentControl contentControl = (ContentControl) element;
      ((UIElement) contentControl).Tap += new EventHandler<GestureEventArgs>(this.OnContainerTap);
      ((FrameworkElement) contentControl).SizeChanged += new SizeChangedEventHandler(this.OnListPickerItemSizeChanged);
      if (!object.Equals(item, this.SelectedItem))
        return;
      this.SizeForAppropriateView(false);
    }

    /// <summary>
    /// Undoes the effects of the PrepareContainerForItemOverride method.
    /// </summary>
    /// <param name="element">The container element.</param>
    /// <param name="item">The item.</param>
    protected virtual void ClearContainerForItemOverride(DependencyObject element, object item)
    {
      base.ClearContainerForItemOverride(element, item);
      ContentControl contentControl = (ContentControl) element;
      ((UIElement) contentControl).Tap -= new EventHandler<GestureEventArgs>(this.OnContainerTap);
      ((FrameworkElement) contentControl).SizeChanged -= new SizeChangedEventHandler(this.OnListPickerItemSizeChanged);
    }

    /// <summary>
    /// Provides handling for the ItemContainerGenerator.ItemsChanged event.
    /// </summary>
    /// <param name="e">A NotifyCollectionChangedEventArgs that contains the event data.</param>
    protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
      base.OnItemsChanged(e);
      if (0 < ((PresentationFrameworkCollection<object>) this.Items).Count && null == this.SelectedItem)
      {
        if (((FrameworkElement) this).GetBindingExpression(ListPicker.SelectedIndexProperty) == null && null == ((FrameworkElement) this).GetBindingExpression(ListPicker.SelectedItemProperty))
          this.SelectedIndex = 0;
      }
      else if (0 == ((PresentationFrameworkCollection<object>) this.Items).Count)
      {
        this.SelectedIndex = -1;
        this.ListPickerMode = ListPickerMode.Normal;
      }
      else if (((PresentationFrameworkCollection<object>) this.Items).Count <= this.SelectedIndex)
        this.SelectedIndex = ((PresentationFrameworkCollection<object>) this.Items).Count - 1;
      else if (!object.Equals(((PresentationFrameworkCollection<object>) this.Items)[this.SelectedIndex], this.SelectedItem))
      {
        int num = ((PresentationFrameworkCollection<object>) this.Items).IndexOf(this.SelectedItem);
        if (-1 == num)
          this.SelectedItem = ((PresentationFrameworkCollection<object>) this.Items)[0];
        else
          this.SelectedIndex = num;
      }
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.SizeForAppropriateView(false)));
    }

    private bool IsValidManipulation(object OriginalSource, Point p)
    {
      for (DependencyObject dependencyObject = OriginalSource as DependencyObject; null != dependencyObject; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        if (this._itemsPresenterHostPart == dependencyObject || this._multipleSelectionModeSummary == dependencyObject || this._border == dependencyObject)
        {
          double num1 = 11.0;
          int num2;
          if (p.X > 0.0 && p.Y > 0.0 - num1)
          {
            double x = p.X;
            Size renderSize = ((UIElement) this._border).RenderSize;
            double width = renderSize.Width;
            if (x < width)
            {
              double y = p.Y;
              renderSize = ((UIElement) this._border).RenderSize;
              double num3 = renderSize.Height + num1;
              num2 = y < num3 ? 1 : 0;
              goto label_6;
            }
          }
          num2 = 0;
label_6:
          return num2 != 0;
        }
      }
      return false;
    }

    /// <summary>Handles the tap event.</summary>
    /// <param name="e">Event args</param>
    protected virtual void OnTap(GestureEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      if (this.ListPickerMode != ListPickerMode.Normal)
        return;
      if (!((Control) this).IsEnabled)
      {
        e.Handled = true;
      }
      else
      {
        Point position = e.GetPosition((UIElement) ((RoutedEventArgs) e).OriginalSource);
        if (this.IsValidManipulation(((RoutedEventArgs) e).OriginalSource, position) && this.Open())
          e.Handled = true;
      }
    }

    /// <summary>Called when the ManipulationStarted event occurs.</summary>
    /// <param name="e">Event data for the event.</param>
    protected virtual void OnManipulationStarted(ManipulationStartedEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Control) this).OnManipulationStarted(e);
      if (this.ListPickerMode != ListPickerMode.Normal)
        return;
      if (!((Control) this).IsEnabled)
      {
        e.Complete();
      }
      else
      {
        Point p = e.ManipulationOrigin;
        if (((RoutedEventArgs) e).OriginalSource != e.ManipulationContainer)
          p = e.ManipulationContainer.TransformToVisual((UIElement) ((RoutedEventArgs) e).OriginalSource).Transform(p);
        if (this.IsValidManipulation(((RoutedEventArgs) e).OriginalSource, p))
          this.IsHighlighted = true;
      }
    }

    /// <summary>Called when the ManipulationDelta event occurs.</summary>
    /// <param name="e">Event data for the event.</param>
    protected virtual void OnManipulationDelta(ManipulationDeltaEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Control) this).OnManipulationDelta(e);
      if (this.ListPickerMode != ListPickerMode.Normal)
        return;
      if (!((Control) this).IsEnabled)
      {
        e.Complete();
      }
      else
      {
        Point p = e.ManipulationOrigin;
        if (((RoutedEventArgs) e).OriginalSource != e.ManipulationContainer)
          p = e.ManipulationContainer.TransformToVisual((UIElement) ((RoutedEventArgs) e).OriginalSource).Transform(p);
        if (!this.IsValidManipulation(((RoutedEventArgs) e).OriginalSource, p))
        {
          this.IsHighlighted = false;
          e.Complete();
        }
      }
    }

    /// <summary>Called when the ManipulationCompleted event occurs.</summary>
    /// <param name="e">Event data for the event.</param>
    protected virtual void OnManipulationCompleted(ManipulationCompletedEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Control) this).OnManipulationCompleted(e);
      if (!((Control) this).IsEnabled || this.ListPickerMode != ListPickerMode.Normal)
        return;
      this.IsHighlighted = false;
    }

    /// <summary>
    /// Opens the picker for selection either into Expanded or Full mode depending on the picker's state.
    /// </summary>
    /// <returns>Whether the picker was succesfully opened.</returns>
    public bool Open()
    {
      if (this.SelectionMode == 0)
      {
        if (ListPickerMode.Normal != this.ListPickerMode)
          return false;
        this.ListPickerMode = this.ExpansionMode != ExpansionMode.ExpansionAllowed || ((PresentationFrameworkCollection<object>) this.Items).Count > this.ItemCountThreshold ? ListPickerMode.Full : ListPickerMode.Expanded;
        return true;
      }
      this.ListPickerMode = ListPickerMode.Full;
      return true;
    }

    private void OnItemsPresenterHostParentSizeChanged(object sender, SizeChangedEventArgs e)
    {
      Size size;
      int num;
      if (this._itemsPresenterPart != null && this._itemsPresenterHostPart != null)
      {
        size = e.NewSize;
        double width1 = size.Width;
        size = e.PreviousSize;
        double width2 = size.Width;
        if (width1 == width2)
        {
          size = e.NewSize;
          num = size.Width != 0.0 ? 1 : 0;
        }
        else
          num = 0;
      }
      else
        num = 1;
      if (num == 0)
      {
        size = e.NewSize;
        this.UpdateItemsPresenterWidth(size.Width);
      }
      ((UIElement) this._itemsPresenterHostParent).Clip = (Geometry) new RectangleGeometry()
      {
        Rect = new Rect(new Point(), e.NewSize)
      };
    }

    private void UpdateItemsPresenterWidth(double availableWidth)
    {
      ((FrameworkElement) this._itemsPresenterPart).Width = ((FrameworkElement) this._itemsPresenterHostPart).Width = double.NaN;
      ((UIElement) this._itemsPresenterPart).Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      if (double.IsNaN(((FrameworkElement) this).Width) && ((FrameworkElement) this).HorizontalAlignment != 3)
        ((FrameworkElement) this._itemsPresenterHostPart).Width = ((UIElement) this._itemsPresenterPart).DesiredSize.Width;
      if (availableWidth <= ((UIElement) this._itemsPresenterPart).DesiredSize.Width)
        return;
      ((FrameworkElement) this._itemsPresenterPart).Width = availableWidth;
    }

    private void OnListPickerItemSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (object.Equals(this.ItemContainerGenerator.ItemFromContainer((DependencyObject) sender), this.SelectedItem))
        this.SizeForAppropriateView(false);
      if (!double.IsNaN(((FrameworkElement) this).Width) || ((FrameworkElement) this).HorizontalAlignment == 3)
        return;
      ((FrameworkElement) this._itemsPresenterHostPart).Width = ((UIElement) this._itemsPresenterPart).DesiredSize.Width;
    }

    private void OnPageBackKeyPress(object sender, CancelEventArgs e)
    {
      this.ListPickerMode = ListPickerMode.Normal;
      e.Cancel = true;
    }

    private void SizeForAppropriateView(bool animate)
    {
      switch (this.ListPickerMode)
      {
        case ListPickerMode.Normal:
          this.SizeForNormalMode(animate);
          break;
        case ListPickerMode.Expanded:
          this.SizeForExpandedMode();
          break;
        case ListPickerMode.Full:
          return;
      }
      this._storyboard.Begin();
      if (animate)
        return;
      this._storyboard.SkipToFill();
    }

    private void SizeForNormalMode(bool animate)
    {
      ContentControl contentControl = (ContentControl) this.ItemContainerGenerator.ContainerFromItem(this.SelectedItem);
      if (null != contentControl)
      {
        Thickness margin;
        if (0.0 < ((FrameworkElement) contentControl).ActualHeight)
        {
          double actualHeight = ((FrameworkElement) contentControl).ActualHeight;
          margin = ((FrameworkElement) contentControl).Margin;
          double top = margin.Top;
          double num = actualHeight + top;
          margin = ((FrameworkElement) contentControl).Margin;
          double bottom = margin.Bottom;
          this.SetContentHeight(num + bottom - 8.0);
        }
        if (null != this._itemsPresenterTranslateTransformPart)
        {
          if (!animate)
            this._itemsPresenterTranslateTransformPart.Y = -4.0;
          DoubleAnimation translateAnimation = this._translateAnimation;
          margin = ((FrameworkElement) contentControl).Margin;
          double? nullable = new double?(margin.Top - LayoutInformation.GetLayoutSlot((FrameworkElement) contentControl).Top - 4.0);
          translateAnimation.To = nullable;
          this._translateAnimation.From = animate ? new double?() : this._translateAnimation.To;
        }
      }
      else
        this.SetContentHeight(0.0);
      ListPickerItem listPickerItem = (ListPickerItem) this.ItemContainerGenerator.ContainerFromIndex(this.SelectedIndex);
      if (null == listPickerItem)
        return;
      listPickerItem.IsSelected = false;
    }

    private void SizeForExpandedMode()
    {
      if (null != this._itemsPresenterPart)
        this.SetContentHeight(((FrameworkElement) this._itemsPresenterPart).ActualHeight);
      if (null != this._itemsPresenterTranslateTransformPart)
        this._translateAnimation.To = new double?(0.0);
      ListPickerItem listPickerItem = (ListPickerItem) this.ItemContainerGenerator.ContainerFromIndex(this.SelectedIndex);
      if (null == listPickerItem)
        return;
      listPickerItem.IsSelected = true;
    }

    private void SetContentHeight(double height)
    {
      if (this._itemsPresenterHostPart == null || double.IsNaN(height))
        return;
      double height1 = ((FrameworkElement) this._itemsPresenterHostPart).Height;
      this._heightAnimation.From = new double?(double.IsNaN(height1) ? height : height1);
      this._heightAnimation.To = new double?(height);
    }

    private void OnFrameManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      if (ListPickerMode.Expanded != this.ListPickerMode)
        return;
      DependencyObject dependencyObject1 = ((RoutedEventArgs) e).OriginalSource as DependencyObject;
      DependencyObject dependencyObject2 = (DependencyObject) (this._itemsPresenterHostPart ?? (Canvas) this);
      for (; null != dependencyObject1; dependencyObject1 = VisualTreeHelper.GetParent(dependencyObject1))
      {
        if (dependencyObject2 == dependencyObject1)
          return;
      }
      this.ListPickerMode = ListPickerMode.Normal;
    }

    private void OnContainerTap(object sender, GestureEventArgs e)
    {
      if (ListPickerMode.Expanded != this.ListPickerMode)
        return;
      this.SelectedItem = this.ItemContainerGenerator.ItemFromContainer((DependencyObject) sender);
      this.ListPickerMode = ListPickerMode.Normal;
      e.Handled = true;
    }

    private void UpdateVisualStates(bool useTransitions)
    {
      if (!((Control) this).IsEnabled)
        VisualStateManager.GoToState((Control) this, "Disabled", useTransitions);
      else if (this.IsHighlighted)
        VisualStateManager.GoToState((Control) this, "Highlighted", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Normal", useTransitions);
    }

    /// <summary>
    /// Updates the summary of the selected items to be displayed in the ListPicker.
    /// </summary>
    /// <param name="newValue">The list selected items</param>
    [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Controls.TextBlock.set_Text(System.String)", Justification = "By design.")]
    private void UpdateSummary(IList newValue)
    {
      string str = (string) null;
      if (null != this.SummaryForSelectedItemsDelegate)
        str = this.SummaryForSelectedItemsDelegate(newValue);
      if (str == null)
        str = newValue != null && newValue.Count != 0 ? newValue[0].ToString() : " ";
      if (string.IsNullOrEmpty(str))
        str = " ";
      if (null == this._multipleSelectionModeSummary)
        return;
      this._multipleSelectionModeSummary.Text = str;
    }

    private void OpenPickerPage()
    {
      if ((Uri) null == this.PickerPageUri)
        throw new ArgumentException("PickerPageUri");
      if (null != this._frame)
        return;
      this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
      if (null != this._frame)
      {
        this._frameContentWhenOpened = ((ContentControl) this._frame).Content;
        UIElement contentWhenOpened = this._frameContentWhenOpened as UIElement;
        if (null != contentWhenOpened)
        {
          this._savedNavigationInTransition = TransitionService.GetNavigationInTransition(contentWhenOpened);
          TransitionService.SetNavigationInTransition(contentWhenOpened, (NavigationInTransition) null);
          this._savedNavigationOutTransition = TransitionService.GetNavigationOutTransition(contentWhenOpened);
          TransitionService.SetNavigationOutTransition(contentWhenOpened, (NavigationOutTransition) null);
        }
        ((Frame) this._frame).Navigated += new NavigatedEventHandler(this.OnFrameNavigated);
        ((Frame) this._frame).NavigationStopped += new NavigationStoppedEventHandler(this.OnFrameNavigationStoppedOrFailed);
        ((Frame) this._frame).NavigationFailed += new NavigationFailedEventHandler(this.OnFrameNavigationStoppedOrFailed);
        this._hasPickerPageOpen = true;
        ((Frame) this._frame).Navigate(this.PickerPageUri);
      }
    }

    private void ClosePickerPage()
    {
      if (null == this._frame)
      {
        this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
        if (null != this._frame)
        {
          ((Frame) this._frame).Navigated -= new NavigatedEventHandler(this.OnFrameNavigated);
          ((Frame) this._frame).NavigationStopped -= new NavigationStoppedEventHandler(this.OnFrameNavigationStoppedOrFailed);
          ((Frame) this._frame).NavigationFailed -= new NavigationFailedEventHandler(this.OnFrameNavigationStoppedOrFailed);
          UIElement contentWhenOpened = this._frameContentWhenOpened as UIElement;
          if (null != contentWhenOpened)
          {
            TransitionService.SetNavigationInTransition(contentWhenOpened, this._savedNavigationInTransition);
            this._savedNavigationInTransition = (NavigationInTransition) null;
            TransitionService.SetNavigationOutTransition(contentWhenOpened, this._savedNavigationOutTransition);
            this._savedNavigationOutTransition = (NavigationOutTransition) null;
          }
          this._frame = (PhoneApplicationFrame) null;
          this._frameContentWhenOpened = (object) null;
        }
      }
      if (null == this._listPickerPage)
        return;
      if (this.SelectionMode == null && null != this._listPickerPage.SelectedItem)
        this.SelectedItem = this._listPickerPage.SelectedItem;
      else if ((this.SelectionMode == 1 || this.SelectionMode == 2) && null != this._listPickerPage.SelectedItems)
        this.SelectedItems = this._listPickerPage.SelectedItems;
      this._listPickerPage = (ListPickerPage) null;
    }

    private void OnFrameNavigated(object sender, NavigationEventArgs e)
    {
      if (e.Content == this._frameContentWhenOpened)
      {
        this.ListPickerMode = ListPickerMode.Normal;
      }
      else
      {
        if (this._listPickerPage != null || !this._hasPickerPageOpen)
          return;
        this._hasPickerPageOpen = false;
        this._listPickerPage = e.Content as ListPickerPage;
        if (null != this._listPickerPage)
        {
          ((FrameworkElement) this._listPickerPage).FlowDirection = ((FrameworkElement) this).FlowDirection;
          this._listPickerPage.HeaderText = null == this.FullModeHeader ? (string) this.Header : (string) this.FullModeHeader;
          this._listPickerPage.FullModeItemTemplate = this.FullModeItemTemplate;
          this._listPickerPage.Items.Clear();
          if (null != this.Items)
          {
            foreach (object obj in (PresentationFrameworkCollection<object>) this.Items)
              this._listPickerPage.Items.Add(obj);
          }
          this._listPickerPage.SelectionMode = this.SelectionMode;
          if (this.SelectionMode == 0)
          {
            this._listPickerPage.SelectedItem = this.SelectedItem;
          }
          else
          {
            this._listPickerPage.SelectedItems.Clear();
            if (null != this.SelectedItems)
            {
              foreach (object selectedItem in (IEnumerable) this.SelectedItems)
                this._listPickerPage.SelectedItems.Add(selectedItem);
            }
          }
        }
      }
    }

    private void OnFrameNavigationStoppedOrFailed(object sender, EventArgs e) => this.ListPickerMode = ListPickerMode.Normal;
  }
}
