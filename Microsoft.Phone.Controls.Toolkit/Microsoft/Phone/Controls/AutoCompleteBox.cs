// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.AutoCompleteBox
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a control that provides a text box for user input and a
  /// drop-down that contains possible matches based on the input in the text
  /// box.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  [TemplatePart(Name = "Selector", Type = typeof (Selector))]
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Pressed", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "PopupOpened", GroupName = "PopupStates")]
  [TemplateVisualState(Name = "Valid", GroupName = "ValidationStates")]
  [TemplateVisualState(Name = "InvalidFocused", GroupName = "ValidationStates")]
  [TemplateVisualState(Name = "InvalidUnfocused", GroupName = "ValidationStates")]
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Large implementation keeps the components contained.")]
  [ContentProperty("ItemsSource")]
  [TemplatePart(Name = "Text", Type = typeof (TextBox))]
  [TemplatePart(Name = "Popup", Type = typeof (Popup))]
  [TemplateVisualState(Name = "PopupClosed", GroupName = "PopupStates")]
  [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
  [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
  [StyleTypedProperty(Property = "TextBoxStyle", StyleTargetType = typeof (TextBox))]
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof (ListBox))]
  [TemplatePart(Name = "SelectionAdapter", Type = typeof (ISelectionAdapter))]
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
  public class AutoCompleteBox : Control, IUpdateVisualState
  {
    /// <summary>
    /// Specifies the name of the selection adapter TemplatePart.
    /// </summary>
    private const string ElementSelectionAdapter = "SelectionAdapter";
    /// <summary>Specifies the name of the Selector TemplatePart.</summary>
    private const string ElementSelector = "Selector";
    /// <summary>Specifies the name of the Popup TemplatePart.</summary>
    private const string ElementPopup = "Popup";
    /// <summary>The name for the text box part.</summary>
    private const string ElementTextBox = "Text";
    /// <summary>The name for the text box style.</summary>
    private const string ElementTextBoxStyle = "TextBoxStyle";
    /// <summary>The name for the adapter's item container style.</summary>
    private const string ElementItemContainerStyle = "ItemContainerStyle";
    /// <summary>Gets or sets a local cached copy of the items data.</summary>
    private List<object> _items;
    /// <summary>
    /// Gets or sets the observable collection that contains references to
    /// all of the items in the generated view of data that is provided to
    /// the selection-style control adapter.
    /// </summary>
    private ObservableCollection<object> _view;
    /// <summary>
    /// Gets or sets a value to ignore a number of pending change handlers.
    /// The value is decremented after each use. This is used to reset the
    /// value of properties without performing any of the actions in their
    /// change handlers.
    /// </summary>
    /// <remarks>The int is important as a value because the TextBox
    /// TextChanged event does not immediately fire, and this will allow for
    /// nested property changes to be ignored.</remarks>
    private int _ignoreTextPropertyChange;
    /// <summary>
    /// Gets or sets a value indicating whether to ignore calling a pending
    /// change handlers.
    /// </summary>
    private bool _ignorePropertyChange;
    /// <summary>
    /// Gets or sets a value indicating whether to ignore the selection
    /// changed event.
    /// </summary>
    private bool _ignoreTextSelectionChange;
    /// <summary>
    /// Gets or sets a value indicating whether to skip the text update
    /// processing when the selected item is updated.
    /// </summary>
    private bool _skipSelectedItemTextUpdate;
    /// <summary>
    /// Gets or sets the last observed text box selection start location.
    /// </summary>
    private int _textSelectionStart;
    /// <summary>
    /// Gets or sets a value indicating whether the user is in the process
    /// of inputting text.  This is used so that we do not update
    /// _textSelectionStart while the user is using an IME.
    /// </summary>
    private bool _inputtingText;
    /// <summary>
    /// Gets or sets a value indicating whether the user initiated the
    /// current populate call.
    /// </summary>
    private bool _userCalledPopulate;
    /// <summary>
    /// A value indicating whether the popup has been opened at least once.
    /// </summary>
    private bool _popupHasOpened;
    /// <summary>
    /// Gets or sets the DispatcherTimer used for the MinimumPopulateDelay
    /// condition for auto completion.
    /// </summary>
    private DispatcherTimer _delayTimer;
    /// <summary>
    /// Gets or sets a value indicating whether a read-only dependency
    /// property change handler should allow the value to be set.  This is
    /// used to ensure that read-only properties cannot be changed via
    /// SetValue, etc.
    /// </summary>
    private bool _allowWrite;
    /// <summary>
    /// Gets or sets the BindingEvaluator, a framework element that can
    /// provide updated string values from a single binding.
    /// </summary>
    private BindingEvaluator<string> _valueBindingEvaluator;
    /// <summary>
    /// A weak event listener for the collection changed event.
    /// </summary>
    private WeakEventListener<AutoCompleteBox, object, NotifyCollectionChangedEventArgs> _collectionChangedWeakEventListener;
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MinimumPrefixLength" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MinimumPrefixLength" />
    /// dependency property.</value>
    public static readonly DependencyProperty MinimumPrefixLengthProperty = DependencyProperty.Register(nameof (MinimumPrefixLength), typeof (int), typeof (AutoCompleteBox), new PropertyMetadata((object) 1, new PropertyChangedCallback(AutoCompleteBox.OnMinimumPrefixLengthPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MinimumPopulateDelay" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MinimumPopulateDelay" />
    /// dependency property.</value>
    public static readonly DependencyProperty MinimumPopulateDelayProperty = DependencyProperty.Register(nameof (MinimumPopulateDelay), typeof (int), typeof (AutoCompleteBox), new PropertyMetadata(new PropertyChangedCallback(AutoCompleteBox.OnMinimumPopulateDelayPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsTextCompletionEnabled" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsTextCompletionEnabled" />
    /// dependency property.</value>
    public static readonly DependencyProperty IsTextCompletionEnabledProperty = DependencyProperty.Register(nameof (IsTextCompletionEnabled), typeof (bool), typeof (AutoCompleteBox), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemTemplate" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemTemplate" />
    /// dependency property.</value>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (DataTemplate), typeof (AutoCompleteBox), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemContainerStyle" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemContainerStyle" />
    /// dependency property.</value>
    public static readonly DependencyProperty ItemContainerStyleProperty = DependencyProperty.Register(nameof (ItemContainerStyle), typeof (Style), typeof (AutoCompleteBox), new PropertyMetadata((object) null, (PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.TextBoxStyle" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.TextBoxStyle" />
    /// dependency property.</value>
    public static readonly DependencyProperty TextBoxStyleProperty = DependencyProperty.Register(nameof (TextBoxStyle), typeof (Style), typeof (AutoCompleteBox), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MaxDropDownHeight" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.MaxDropDownHeight" />
    /// dependency property.</value>
    public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register(nameof (MaxDropDownHeight), typeof (double), typeof (AutoCompleteBox), new PropertyMetadata((object) double.PositiveInfinity, new PropertyChangedCallback(AutoCompleteBox.OnMaxDropDownHeightPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// dependency property.</value>
    public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register(nameof (IsDropDownOpen), typeof (bool), typeof (AutoCompleteBox), new PropertyMetadata((object) false, new PropertyChangedCallback(AutoCompleteBox.OnIsDropDownOpenPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// dependency property.</value>
    public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof (ItemsSource), typeof (IEnumerable), typeof (AutoCompleteBox), new PropertyMetadata(new PropertyChangedCallback(AutoCompleteBox.OnItemsSourcePropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.SelectedItem" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.SelectedItem" />
    /// dependency property.</value>
    public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(nameof (SelectedItem), typeof (object), typeof (AutoCompleteBox), new PropertyMetadata(new PropertyChangedCallback(AutoCompleteBox.OnSelectedItemPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.Text" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.Text" />
    /// dependency property.</value>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof (Text), typeof (string), typeof (AutoCompleteBox), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(AutoCompleteBox.OnTextPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.SearchText" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.SearchText" />
    /// dependency property.</value>
    public static readonly DependencyProperty SearchTextProperty = DependencyProperty.Register(nameof (SearchText), typeof (string), typeof (AutoCompleteBox), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(AutoCompleteBox.OnSearchTextPropertyChanged)));
    /// <summary>
    /// Gets the identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.FilterMode" />
    /// dependency property.
    /// </summary>
    public static readonly DependencyProperty FilterModeProperty = DependencyProperty.Register(nameof (FilterMode), typeof (AutoCompleteFilterMode), typeof (AutoCompleteBox), new PropertyMetadata((object) AutoCompleteFilterMode.StartsWith, new PropertyChangedCallback(AutoCompleteBox.OnFilterModePropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemFilter" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemFilter" />
    /// dependency property.</value>
    public static readonly DependencyProperty ItemFilterProperty = DependencyProperty.Register(nameof (ItemFilter), typeof (AutoCompleteFilterPredicate<object>), typeof (AutoCompleteBox), new PropertyMetadata(new PropertyChangedCallback(AutoCompleteBox.OnItemFilterPropertyChanged)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.TextFilter" />
    /// dependency property.
    /// </summary>
    /// <value>The identifier for the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.TextFilter" />
    /// dependency property.</value>
    public static readonly DependencyProperty TextFilterProperty = DependencyProperty.Register(nameof (TextFilter), typeof (AutoCompleteFilterPredicate<string>), typeof (AutoCompleteBox), new PropertyMetadata((object) AutoCompleteSearch.GetFilter(AutoCompleteFilterMode.StartsWith)));
    /// <summary>
    /// Identifies the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.InputScope" />
    /// dependency property.
    /// </summary>
    public static readonly DependencyProperty InputScopeProperty = DependencyProperty.Register(nameof (InputScope), typeof (InputScope), typeof (AutoCompleteBox), (PropertyMetadata) null);
    /// <summary>The TextBox template part.</summary>
    private TextBox _text;
    /// <summary>The SelectionAdapter.</summary>
    private ISelectionAdapter _adapter;

    /// <summary>
    /// Gets or sets the helper that provides all of the standard
    /// interaction functionality. Making it internal for subclass access.
    /// </summary>
    internal InteractionHelper Interaction { get; set; }

    /// <summary>
    /// Gets or sets the minimum number of characters required to be entered
    /// in the text box before the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> displays
    /// possible matches.
    /// matches.
    /// </summary>
    /// <value>
    /// The minimum number of characters to be entered in the text box
    /// before the <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" />
    /// displays possible matches. The default is 1.
    /// </value>
    /// <remarks>
    /// If you set MinimumPrefixLength to -1, the AutoCompleteBox will
    /// not provide possible matches. There is no maximum value, but
    /// setting MinimumPrefixLength to value that is too large will
    /// prevent the AutoCompleteBox from providing possible matches as well.
    /// </remarks>
    public int MinimumPrefixLength
    {
      get => (int) ((DependencyObject) this).GetValue(AutoCompleteBox.MinimumPrefixLengthProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.MinimumPrefixLengthProperty, (object) value);
    }

    /// <summary>MinimumPrefixLengthProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its MinimumPrefixLength.</param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "MinimumPrefixLength is the name of the actual dependency property.")]
    private static void OnMinimumPrefixLengthPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      int newValue = (int) e.NewValue;
      if (newValue < 0 && newValue != -1)
        throw new ArgumentOutOfRangeException("MinimumPrefixLength");
    }

    /// <summary>
    /// Gets or sets the minimum delay, in milliseconds, after text is typed
    /// in the text box before the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control
    /// populates the list of possible matches in the drop-down.
    /// </summary>
    /// <value>The minimum delay, in milliseconds, after text is typed in
    /// the text box, but before the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> populates
    /// the list of possible matches in the drop-down. The default is 0.</value>
    /// <exception cref="T:System.ArgumentException">The set value is less than 0.</exception>
    public int MinimumPopulateDelay
    {
      get => (int) ((DependencyObject) this).GetValue(AutoCompleteBox.MinimumPopulateDelayProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.MinimumPopulateDelayProperty, (object) value);
    }

    /// <summary>
    /// MinimumPopulateDelayProperty property changed handler. Any current
    /// dispatcher timer will be stopped. The timer will not be restarted
    /// until the next TextUpdate call by the user.
    /// </summary>
    /// <param name="d">AutoCompleteTextBox that changed its
    /// MinimumPopulateDelay.</param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "The exception is most likely to be called through the CLR property setter.")]
    private static void OnMinimumPopulateDelayPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (autoCompleteBox._ignorePropertyChange)
      {
        autoCompleteBox._ignorePropertyChange = false;
      }
      else
      {
        int newValue = (int) e.NewValue;
        if (newValue < 0)
        {
          autoCompleteBox._ignorePropertyChange = true;
          d.SetValue(e.Property, e.OldValue);
        }
        if (autoCompleteBox._delayTimer != null)
        {
          autoCompleteBox._delayTimer.Stop();
          if (newValue == 0)
            autoCompleteBox._delayTimer = (DispatcherTimer) null;
        }
        if (newValue > 0 && autoCompleteBox._delayTimer == null)
        {
          autoCompleteBox._delayTimer = new DispatcherTimer();
          autoCompleteBox._delayTimer.Tick += new EventHandler(autoCompleteBox.PopulateDropDown);
        }
        if (newValue <= 0 || autoCompleteBox._delayTimer == null)
          return;
        autoCompleteBox._delayTimer.Interval = TimeSpan.FromMilliseconds((double) newValue);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the first possible match
    /// found during the filtering process will be displayed automatically
    /// in the text box.
    /// </summary>
    /// <value>
    /// True if the first possible match found will be displayed
    /// automatically in the text box; otherwise, false. The default is
    /// false.
    /// </value>
    public bool IsTextCompletionEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(AutoCompleteBox.IsTextCompletionEnabledProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.IsTextCompletionEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.DataTemplate" /> used
    /// to display each item in the drop-down portion of the control.
    /// </summary>
    /// <value>The <see cref="T:System.Windows.DataTemplate" /> used to
    /// display each item in the drop-down. The default is null.</value>
    /// <remarks>
    /// You use the ItemTemplate property to specify the visualization
    /// of the data objects in the drop-down portion of the AutoCompleteBox
    /// control. If your AutoCompleteBox is bound to a collection and you
    /// do not provide specific display instructions by using a
    /// DataTemplate, the resulting UI of each item is a string
    /// representation of each object in the underlying collection.
    /// </remarks>
    public DataTemplate ItemTemplate
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.ItemTemplateProperty) as DataTemplate;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.ItemTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Style" /> that is
    /// applied to the selection adapter contained in the drop-down portion
    /// of the <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" />
    /// control.
    /// </summary>
    /// <value>The <see cref="T:System.Windows.Style" /> applied to the
    /// selection adapter contained in the drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// The default is null.</value>
    /// <remarks>
    /// The default selection adapter contained in the drop-down is a
    /// ListBox control.
    /// </remarks>
    public Style ItemContainerStyle
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.ItemContainerStyleProperty) as Style;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.ItemContainerStyleProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Style" /> applied to
    /// the text box portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// </summary>
    /// <value>The <see cref="T:System.Windows.Style" /> applied to the text
    /// box portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// The default is null.</value>
    public Style TextBoxStyle
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.TextBoxStyleProperty) as Style;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.TextBoxStyleProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the maximum height of the drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// </summary>
    /// <value>The maximum height of the drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// The default is <see cref="F:System.Double.PositiveInfinity" />.</value>
    /// <exception cref="T:System.ArgumentException">The specified value is less than 0.</exception>
    public double MaxDropDownHeight
    {
      get => (double) ((DependencyObject) this).GetValue(AutoCompleteBox.MaxDropDownHeightProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.MaxDropDownHeightProperty, (object) value);
    }

    /// <summary>MaxDropDownHeightProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteTextBox that changed its MaxDropDownHeight.</param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "The exception will be called through a CLR setter in most cases.")]
    private static void OnMaxDropDownHeightPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (autoCompleteBox._ignorePropertyChange)
      {
        autoCompleteBox._ignorePropertyChange = false;
      }
      else
      {
        double newValue = (double) e.NewValue;
        if (newValue < 0.0)
        {
          autoCompleteBox._ignorePropertyChange = true;
          ((DependencyObject) autoCompleteBox).SetValue(e.Property, e.OldValue);
        }
        autoCompleteBox.OnMaxDropDownHeightChanged(newValue);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the drop-down portion of
    /// the control is open.
    /// </summary>
    /// <value>
    /// True if the drop-down is open; otherwise, false. The default is
    /// false.
    /// </value>
    public bool IsDropDownOpen
    {
      get => (bool) ((DependencyObject) this).GetValue(AutoCompleteBox.IsDropDownOpenProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.IsDropDownOpenProperty, (object) value);
    }

    /// <summary>IsDropDownOpenProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteTextBox that changed its IsDropDownOpen.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnIsDropDownOpenPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (autoCompleteBox._ignorePropertyChange)
      {
        autoCompleteBox._ignorePropertyChange = false;
      }
      else
      {
        bool oldValue = (bool) e.OldValue;
        if ((bool) e.NewValue)
          autoCompleteBox.TextUpdated(autoCompleteBox.Text, true);
        else
          autoCompleteBox.ClosingDropDown(oldValue);
        autoCompleteBox.UpdateVisualState(true);
      }
    }

    /// <summary>
    /// Gets or sets a collection that is used to generate the items for the
    /// drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// </summary>
    /// <value>The collection that is used to generate the items of the
    /// drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.</value>
    public IEnumerable ItemsSource
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.ItemsSourceProperty) as IEnumerable;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.ItemsSourceProperty, (object) value);
    }

    /// <summary>ItemsSourceProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its ItemsSource.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnItemsSourcePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      (d as AutoCompleteBox).OnItemsSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue);
    }

    /// <summary>Gets or sets the selected item in the drop-down.</summary>
    /// <value>The selected item in the drop-down.</value>
    /// <remarks>
    /// If the IsTextCompletionEnabled property is true and text typed by
    /// the user matches an item in the ItemsSource collection, which is
    /// then displayed in the text box, the SelectedItem property will be
    /// a null reference.
    /// </remarks>
    public object SelectedItem
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.SelectedItemProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.SelectedItemProperty, value);
    }

    /// <summary>
    /// SelectedItemProperty property changed handler. Fires the
    /// SelectionChanged event. The event data will contain any non-null
    /// removed items and non-null additions.
    /// </summary>
    /// <param name="d">AutoCompleteBox that changed its SelectedItem.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnSelectedItemPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (autoCompleteBox._ignorePropertyChange)
      {
        autoCompleteBox._ignorePropertyChange = false;
      }
      else
      {
        if (autoCompleteBox._skipSelectedItemTextUpdate)
          autoCompleteBox._skipSelectedItemTextUpdate = false;
        else
          autoCompleteBox.OnSelectedItemChanged(e.NewValue);
        List<object> objectList1 = new List<object>();
        if (e.OldValue != null)
          objectList1.Add(e.OldValue);
        List<object> objectList2 = new List<object>();
        if (e.NewValue != null)
          objectList2.Add(e.NewValue);
        autoCompleteBox.OnSelectionChanged(new SelectionChangedEventArgs((IList) objectList1, (IList) objectList2));
      }
    }

    /// <summary>
    /// Called when the selected item is changed, updates the text value
    /// that is displayed in the text box part.
    /// </summary>
    /// <param name="newItem">The new item.</param>
    private void OnSelectedItemChanged(object newItem)
    {
      this.UpdateTextValue(newItem != null ? this.FormatValue(newItem, true) : this.SearchText);
      if (this.TextBox == null || this.Text == null)
        return;
      this.TextBox.SelectionStart = this.Text.Length;
    }

    /// <summary>
    /// Gets or sets the text in the text box portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// </summary>
    /// <value>The text in the text box portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.</value>
    public string Text
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.TextProperty) as string;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.TextProperty, (object) value);
    }

    /// <summary>TextProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its Text.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnTextPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      (d as AutoCompleteBox).TextUpdated((string) e.NewValue, false);
    }

    /// <summary>
    /// Gets the text that is used to filter items in the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// item collection.
    /// </summary>
    /// <value>The text that is used to filter items in the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// item collection.</value>
    /// <remarks>
    /// The SearchText value is typically the same as the
    /// Text property, but is set after the TextChanged event occurs
    /// and before the Populating event.
    /// </remarks>
    public string SearchText
    {
      get => (string) ((DependencyObject) this).GetValue(AutoCompleteBox.SearchTextProperty);
      private set
      {
        try
        {
          this._allowWrite = true;
          ((DependencyObject) this).SetValue(AutoCompleteBox.SearchTextProperty, (object) value);
        }
        finally
        {
          this._allowWrite = false;
        }
      }
    }

    /// <summary>OnSearchTextProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its SearchText.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnSearchTextPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (autoCompleteBox._ignorePropertyChange)
      {
        autoCompleteBox._ignorePropertyChange = false;
      }
      else
      {
        if (autoCompleteBox._allowWrite)
          return;
        autoCompleteBox._ignorePropertyChange = true;
        ((DependencyObject) autoCompleteBox).SetValue(e.Property, e.OldValue);
      }
    }

    /// <summary>
    /// Gets or sets how the text in the text box is used to filter items
    /// specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property for display in the drop-down.
    /// </summary>
    /// <value>One of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteFilterMode" />
    /// values The default is
    /// <see cref="F:Microsoft.Phone.Controls.AutoCompleteFilterMode.StartsWith" />.</value>
    /// <exception cref="T:System.ArgumentException">The specified value is
    /// not a valid
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteFilterMode" />.</exception>
    /// <remarks>
    /// Use the FilterMode property to specify how possible matches are
    /// filtered. For example, possible matches can be filtered in a
    /// predefined or custom way. The search mode is automatically set to
    /// Custom if you set the ItemFilter property.
    /// </remarks>
    public AutoCompleteFilterMode FilterMode
    {
      get => (AutoCompleteFilterMode) ((DependencyObject) this).GetValue(AutoCompleteBox.FilterModeProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.FilterModeProperty, (object) value);
    }

    /// <summary>FilterModeProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its FilterMode.</param>
    /// <param name="e">Event arguments.</param>
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "The exception will be thrown when the CLR setter is used in most situations.")]
    private static void OnFilterModePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      AutoCompleteFilterMode newValue1 = (AutoCompleteFilterMode) e.NewValue;
      int num;
      switch (newValue1)
      {
        case AutoCompleteFilterMode.None:
        case AutoCompleteFilterMode.StartsWith:
        case AutoCompleteFilterMode.StartsWithCaseSensitive:
        case AutoCompleteFilterMode.StartsWithOrdinal:
        case AutoCompleteFilterMode.Contains:
        case AutoCompleteFilterMode.ContainsCaseSensitive:
        case AutoCompleteFilterMode.ContainsOrdinal:
        case AutoCompleteFilterMode.ContainsOrdinalCaseSensitive:
        case AutoCompleteFilterMode.Equals:
        case AutoCompleteFilterMode.EqualsCaseSensitive:
        case AutoCompleteFilterMode.EqualsOrdinal:
        case AutoCompleteFilterMode.EqualsOrdinalCaseSensitive:
        case AutoCompleteFilterMode.Custom:
          num = 1;
          break;
        default:
          num = newValue1 == AutoCompleteFilterMode.StartsWithOrdinalCaseSensitive ? 1 : 0;
          break;
      }
      if (num == 0)
        ((DependencyObject) autoCompleteBox).SetValue(e.Property, e.OldValue);
      AutoCompleteFilterMode newValue2 = (AutoCompleteFilterMode) e.NewValue;
      autoCompleteBox.TextFilter = AutoCompleteSearch.GetFilter(newValue2);
    }

    /// <summary>
    /// Gets or sets the custom method that uses user-entered text to filter
    /// the items specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property for display in the drop-down.
    /// </summary>
    /// <value>The custom method that uses the user-entered text to filter
    /// the items specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property. The default is null.</value>
    /// <remarks>
    /// The filter mode is automatically set to Custom if you set the
    /// ItemFilter property.
    /// </remarks>
    public AutoCompleteFilterPredicate<object> ItemFilter
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.ItemFilterProperty) as AutoCompleteFilterPredicate<object>;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.ItemFilterProperty, (object) value);
    }

    /// <summary>ItemFilterProperty property changed handler.</summary>
    /// <param name="d">AutoCompleteBox that changed its ItemFilter.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnItemFilterPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      AutoCompleteBox autoCompleteBox = d as AutoCompleteBox;
      if (!(e.NewValue is AutoCompleteFilterPredicate<object>))
      {
        autoCompleteBox.FilterMode = AutoCompleteFilterMode.None;
      }
      else
      {
        autoCompleteBox.FilterMode = AutoCompleteFilterMode.Custom;
        autoCompleteBox.TextFilter = (AutoCompleteFilterPredicate<string>) null;
      }
    }

    /// <summary>
    /// Gets or sets the custom method that uses the user-entered text to
    /// filter items specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property in a text-based way for display in the drop-down.
    /// </summary>
    /// <value>The custom method that uses the user-entered text to filter
    /// items specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property in a text-based way for display in the drop-down.</value>
    /// <remarks>
    /// The search mode is automatically set to Custom if you set the
    /// TextFilter property.
    /// </remarks>
    public AutoCompleteFilterPredicate<string> TextFilter
    {
      get => ((DependencyObject) this).GetValue(AutoCompleteBox.TextFilterProperty) as AutoCompleteFilterPredicate<string>;
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.TextFilterProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the
    /// <see cref="T:System.Windows.Input.InputScope" />
    /// used by the Text template part.
    /// </summary>
    public InputScope InputScope
    {
      get => (InputScope) ((DependencyObject) this).GetValue(AutoCompleteBox.InputScopeProperty);
      set => ((DependencyObject) this).SetValue(AutoCompleteBox.InputScopeProperty, (object) value);
    }

    /// <summary>Gets or sets the drop down popup control.</summary>
    private PopupHelper DropDownPopup { get; set; }

    /// <summary>Determines whether text completion should be done.</summary>
    private static bool IsCompletionEnabled
    {
      get
      {
        PhoneApplicationFrame phoneApplicationFrame;
        return PhoneHelper.TryGetPhoneApplicationFrame(out phoneApplicationFrame) && phoneApplicationFrame.IsPortrait();
      }
    }

    /// <summary>Gets or sets the Text template part.</summary>
    internal TextBox TextBox
    {
      get => this._text;
      set
      {
        if (this._text != null)
        {
          this._text.SelectionChanged -= new RoutedEventHandler(this.OnTextBoxSelectionChanged);
          this._text.TextChanged -= new TextChangedEventHandler(this.OnTextBoxTextChanged);
        }
        this._text = value;
        if (this._text == null)
          return;
        this._text.SelectionChanged += new RoutedEventHandler(this.OnTextBoxSelectionChanged);
        this._text.TextChanged += new TextChangedEventHandler(this.OnTextBoxTextChanged);
        if (this.Text != null)
          this.UpdateTextValue(this.Text);
      }
    }

    /// <summary>
    /// Gets or sets the selection adapter used to populate the drop-down
    /// with a list of selectable items.
    /// </summary>
    /// <value>The selection adapter used to populate the drop-down with a
    /// list of selectable items.</value>
    /// <remarks>
    /// You can use this property when you create an automation peer to
    /// use with AutoCompleteBox or deriving from AutoCompleteBox to
    /// create a custom control.
    /// </remarks>
    protected internal ISelectionAdapter SelectionAdapter
    {
      get => this._adapter;
      set
      {
        if (this._adapter != null)
        {
          this._adapter.SelectionChanged -= new SelectionChangedEventHandler(this.OnAdapterSelectionChanged);
          this._adapter.Commit -= new RoutedEventHandler(this.OnAdapterSelectionComplete);
          this._adapter.Cancel -= new RoutedEventHandler(this.OnAdapterSelectionCanceled);
          this._adapter.Cancel -= new RoutedEventHandler(this.OnAdapterSelectionComplete);
          this._adapter.ItemsSource = (IEnumerable) null;
        }
        this._adapter = value;
        if (this._adapter == null)
          return;
        this._adapter.SelectionChanged += new SelectionChangedEventHandler(this.OnAdapterSelectionChanged);
        this._adapter.Commit += new RoutedEventHandler(this.OnAdapterSelectionComplete);
        this._adapter.Cancel += new RoutedEventHandler(this.OnAdapterSelectionCanceled);
        this._adapter.Cancel += new RoutedEventHandler(this.OnAdapterSelectionComplete);
        this._adapter.ItemsSource = (IEnumerable) this._view;
      }
    }

    /// <summary>
    /// Occurs when the text in the text box portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> changes.
    /// </summary>
    public event RoutedEventHandler TextChanged;

    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> is
    /// populating the drop-down with possible matches based on the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.Text" />
    /// property.
    /// </summary>
    /// <remarks>
    /// If the event is canceled, by setting the PopulatingEventArgs.Cancel
    /// property to true, the AutoCompleteBox will not automatically
    /// populate the selection adapter contained in the drop-down.
    /// In this case, if you want possible matches to appear, you must
    /// provide the logic for populating the selection adapter.
    /// </remarks>
    public event PopulatingEventHandler Populating;

    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> has
    /// populated the drop-down with possible matches based on the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.Text" />
    /// property.
    /// </summary>
    public event PopulatedEventHandler Populated;

    /// <summary>
    /// Occurs when the value of the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// property is changing from false to true.
    /// </summary>
    public event RoutedPropertyChangingEventHandler<bool> DropDownOpening;

    /// <summary>
    /// Occurs when the value of the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// property has changed from false to true and the drop-down is open.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> DropDownOpened;

    /// <summary>
    /// Occurs when the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// property is changing from true to false.
    /// </summary>
    public event RoutedPropertyChangingEventHandler<bool> DropDownClosing;

    /// <summary>
    /// Occurs when the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.IsDropDownOpen" />
    /// property was changed from true to false and the drop-down is open.
    /// </summary>
    public event RoutedPropertyChangedEventHandler<bool> DropDownClosed;

    /// <summary>
    /// Occurs when the selected item in the drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> has
    /// changed.
    /// </summary>
    public event SelectionChangedEventHandler SelectionChanged;

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Data.Binding" /> that
    /// is used to get the values for display in the text portion of
    /// the <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" />
    /// control.
    /// </summary>
    /// <value>The <see cref="T:System.Windows.Data.Binding" /> object used
    /// when binding to a collection property.</value>
    public Binding ValueMemberBinding
    {
      get => this._valueBindingEvaluator != null ? this._valueBindingEvaluator.ValueBinding : (Binding) null;
      set => this._valueBindingEvaluator = new BindingEvaluator<string>(value);
    }

    /// <summary>
    /// Gets or sets the property path that is used to get values for
    /// display in the text portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.
    /// </summary>
    /// <value>The property path that is used to get values for display in
    /// the text portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.</value>
    public string ValueMemberPath
    {
      get => this.ValueMemberBinding != null ? this.ValueMemberBinding.Path.Path : (string) null;
      set => this.ValueMemberBinding = value == null ? (Binding) null : new Binding(value);
    }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> class.
    /// </summary>
    public AutoCompleteBox()
    {
      this.DefaultStyleKey = (object) typeof (AutoCompleteBox);
      ((FrameworkElement) this).Loaded += (RoutedEventHandler) ((sender, e) => this.ApplyTemplate());
      ((FrameworkElement) this).Loaded += (RoutedEventHandler) ((param0_1, param1_1) =>
      {
        PhoneApplicationFrame phoneApplicationFrame;
        if (!PhoneHelper.TryGetPhoneApplicationFrame(out phoneApplicationFrame))
          return;
        phoneApplicationFrame.OrientationChanged += (EventHandler<OrientationChangedEventArgs>) ((param0_2, param1_2) => this.IsDropDownOpen = false);
      });
      this.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.ControlIsEnabledChanged);
      this.Interaction = new InteractionHelper((Control) this);
      this.ClearView();
    }

    /// <summary>
    /// Arranges and sizes the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" />
    /// control and its contents.
    /// </summary>
    /// <param name="finalSize">The size allowed for the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control.</param>
    /// <returns>The <paramref name="finalSize" />, unchanged.</returns>
    protected virtual Size ArrangeOverride(Size finalSize)
    {
      Size size = ((FrameworkElement) this).ArrangeOverride(finalSize);
      if (this.DropDownPopup != null)
        this.DropDownPopup.Arrange(new Size?(finalSize));
      return size;
    }

    /// <summary>
    /// Builds the visual tree for the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control
    /// when a new template is applied.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      if (this.TextBox != null)
      {
        ((UIElement) this.TextBox).RemoveHandler(UIElement.KeyDownEvent, (Delegate) new KeyEventHandler(this.OnUIElementKeyDown));
        ((UIElement) this.TextBox).RemoveHandler(UIElement.KeyUpEvent, (Delegate) new KeyEventHandler(this.OnUIElementKeyUp));
      }
      if (this.DropDownPopup != null)
      {
        this.DropDownPopup.Closed -= new EventHandler(this.DropDownPopup_Closed);
        this.DropDownPopup.FocusChanged -= new EventHandler(this.OnDropDownFocusChanged);
        this.DropDownPopup.UpdateVisualStates -= new EventHandler(this.OnDropDownPopupUpdateVisualStates);
        this.DropDownPopup.BeforeOnApplyTemplate();
        this.DropDownPopup = (PopupHelper) null;
      }
      ((FrameworkElement) this).OnApplyTemplate();
      if (this.GetTemplateChild("Popup") is Popup templateChild)
      {
        this.DropDownPopup = new PopupHelper((Control) this, templateChild);
        this.DropDownPopup.MaxDropDownHeight = this.MaxDropDownHeight;
        this.DropDownPopup.AfterOnApplyTemplate();
        this.DropDownPopup.Closed += new EventHandler(this.DropDownPopup_Closed);
        this.DropDownPopup.FocusChanged += new EventHandler(this.OnDropDownFocusChanged);
        this.DropDownPopup.UpdateVisualStates += new EventHandler(this.OnDropDownPopupUpdateVisualStates);
      }
      this.SelectionAdapter = this.GetSelectionAdapterPart();
      this.TextBox = this.GetTemplateChild("Text") as TextBox;
      if (this.TextBox != null)
      {
        ((UIElement) this.TextBox).AddHandler(UIElement.KeyDownEvent, (Delegate) new KeyEventHandler(this.OnUIElementKeyDown), true);
        ((UIElement) this.TextBox).AddHandler(UIElement.KeyUpEvent, (Delegate) new KeyEventHandler(this.OnUIElementKeyUp), true);
      }
      this.Interaction.OnApplyTemplateBase();
      if (!this.IsDropDownOpen || this.DropDownPopup == null || this.DropDownPopup.IsOpen)
        return;
      this.OpeningDropDown(false);
    }

    /// <summary>
    /// Allows the popup wrapper to fire visual state change events.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnDropDownPopupUpdateVisualStates(object sender, EventArgs e) => this.UpdateVisualState(true);

    /// <summary>
    /// Allows the popup wrapper to fire the FocusChanged event.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnDropDownFocusChanged(object sender, EventArgs e) => this.FocusChanged(this.HasFocus());

    /// <summary>Begin closing the drop-down.</summary>
    /// <param name="oldValue">The original value.</param>
    private void ClosingDropDown(bool oldValue)
    {
      bool flag = false;
      if (this.DropDownPopup != null)
        flag = this.DropDownPopup.UsesClosingVisualState;
      RoutedPropertyChangingEventArgs<bool> e = new RoutedPropertyChangingEventArgs<bool>(AutoCompleteBox.IsDropDownOpenProperty, oldValue, false, true);
      this.OnDropDownClosing(e);
      if (this._view == null || this._view.Count == 0)
        flag = false;
      if (e.Cancel)
      {
        this._ignorePropertyChange = true;
        ((DependencyObject) this).SetValue(AutoCompleteBox.IsDropDownOpenProperty, (object) oldValue);
      }
      else if (!flag)
        this.CloseDropDown(oldValue, false);
      this.UpdateVisualState(true);
    }

    /// <summary>
    /// Begin opening the drop down by firing cancelable events, opening the
    /// drop-down or reverting, depending on the event argument values.
    /// </summary>
    /// <param name="oldValue">The original value, if needed for a revert.</param>
    private void OpeningDropDown(bool oldValue)
    {
      if (!AutoCompleteBox.IsCompletionEnabled)
        return;
      RoutedPropertyChangingEventArgs<bool> e = new RoutedPropertyChangingEventArgs<bool>(AutoCompleteBox.IsDropDownOpenProperty, oldValue, true, true);
      this.OnDropDownOpening(e);
      if (e.Cancel)
      {
        this._ignorePropertyChange = true;
        ((DependencyObject) this).SetValue(AutoCompleteBox.IsDropDownOpenProperty, (object) oldValue);
      }
      else
        this.OpenDropDown(oldValue, true);
      this.UpdateVisualState(true);
    }

    /// <summary>Connects to the DropDownPopup Closed event.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void DropDownPopup_Closed(object sender, EventArgs e)
    {
      if (this.IsDropDownOpen)
        this.IsDropDownOpen = false;
      if (!this._popupHasOpened)
        return;
      this.OnDropDownClosed(new RoutedPropertyChangedEventArgs<bool>(true, false));
    }

    /// <summary>Handles the FocusChanged event.</summary>
    /// <param name="hasFocus">A value indicating whether the control
    /// currently has the focus.</param>
    private void FocusChanged(bool hasFocus)
    {
      if (hasFocus)
      {
        if (this.TextBox == null || this.TextBox.SelectionLength != 0)
          return;
        ((Control) this.TextBox).Focus();
      }
      else
      {
        this.IsDropDownOpen = false;
        this._userCalledPopulate = false;
      }
    }

    /// <summary>
    /// Determines whether the text box or drop-down portion of the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> control has
    /// focus.
    /// </summary>
    /// <returns>true to indicate the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> has focus;
    /// otherwise, false.</returns>
    protected bool HasFocus()
    {
      DependencyObject parent;
      for (DependencyObject objA = FocusManager.GetFocusedElement() as DependencyObject; objA != null; objA = parent)
      {
        if (object.ReferenceEquals((object) objA, (object) this))
          return true;
        parent = VisualTreeHelper.GetParent(objA);
        if (parent == null && objA is FrameworkElement frameworkElement)
          parent = frameworkElement.Parent;
      }
      return false;
    }

    /// <summary>
    /// Provides handling for the
    /// <see cref="E:System.Windows.UIElement.GotFocus" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.RoutedEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnGotFocus(RoutedEventArgs e)
    {
      base.OnGotFocus(e);
      this.FocusChanged(this.HasFocus());
    }

    /// <summary>
    /// Provides handling for the
    /// <see cref="E:System.Windows.UIElement.LostFocus" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.RoutedEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnLostFocus(RoutedEventArgs e)
    {
      base.OnLostFocus(e);
      this.FocusChanged(this.HasFocus());
    }

    /// <summary>Handle the change of the IsEnabled property.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void ControlIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      if ((bool) e.NewValue)
        return;
      this.IsDropDownOpen = false;
    }

    /// <summary>
    /// Returns the
    /// <see cref="T:Microsoft.Phone.Controls.ISelectionAdapter" /> part, if
    /// possible.
    /// </summary>
    /// <returns>
    /// A <see cref="T:Microsoft.Phone.Controls.ISelectionAdapter" /> object,
    /// if possible. Otherwise, null.
    /// </returns>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Following the GetTemplateChild pattern for the method.")]
    protected virtual ISelectionAdapter GetSelectionAdapterPart()
    {
      selectionAdapterPart = (ISelectionAdapter) null;
      if (this.GetTemplateChild("Selector") is Selector templateChild && !(templateChild is ISelectionAdapter selectionAdapterPart))
        selectionAdapterPart = (ISelectionAdapter) new SelectorSelectionAdapter(templateChild);
      if (selectionAdapterPart == null)
        selectionAdapterPart = this.GetTemplateChild("SelectionAdapter") as ISelectionAdapter;
      return selectionAdapterPart;
    }

    /// <summary>Handles the timer tick when using a populate delay.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event arguments.</param>
    private void PopulateDropDown(object sender, EventArgs e)
    {
      if (this._delayTimer != null)
        this._delayTimer.Stop();
      this.SearchText = this.Text;
      PopulatingEventArgs e1 = new PopulatingEventArgs(this.SearchText);
      this.OnPopulating(e1);
      if (e1.Cancel)
        return;
      this.PopulateComplete();
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.Populating" />
    /// event.
    /// </summary>
    /// <param name="e">A
    /// <see cref="T:Microsoft.Phone.Controls.PopulatingEventArgs" /> that
    /// contains the event data.</param>
    protected virtual void OnPopulating(PopulatingEventArgs e)
    {
      PopulatingEventHandler populating = this.Populating;
      if (populating == null)
        return;
      populating((object) this, e);
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.Populated" />
    /// event.
    /// </summary>
    /// <param name="e">A
    /// <see cref="T:Microsoft.Phone.Controls.PopulatedEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnPopulated(PopulatedEventArgs e)
    {
      PopulatedEventHandler populated = this.Populated;
      if (populated == null)
        return;
      populated((object) this, e);
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.SelectionChanged" />
    /// event.
    /// </summary>
    /// <param name="e">A
    /// <see cref="T:Microsoft.Phone.Controls.SelectionChangedEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnSelectionChanged(SelectionChangedEventArgs e)
    {
      SelectionChangedEventHandler selectionChanged = this.SelectionChanged;
      if (selectionChanged == null)
        return;
      selectionChanged((object) this, e);
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.DropDownOpening" />
    /// event.
    /// </summary>
    /// <param name="e">A
    /// <see cref="T:Microsoft.Phone.Controls.RoutedPropertyChangingEventArgs`1" />
    /// that contains the event data.</param>
    protected virtual void OnDropDownOpening(RoutedPropertyChangingEventArgs<bool> e)
    {
      RoutedPropertyChangingEventHandler<bool> dropDownOpening = this.DropDownOpening;
      if (dropDownOpening == null)
        return;
      dropDownOpening((object) this, e);
    }

    protected virtual void OnDropDownOpened(RoutedPropertyChangedEventArgs<bool> e)
    {
      RoutedPropertyChangedEventHandler<bool> dropDownOpened = this.DropDownOpened;
      if (dropDownOpened == null)
        return;
      dropDownOpened((object) this, e);
    }

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.DropDownClosing" />
    /// event.
    /// </summary>
    /// <param name="e">A
    /// <see cref="T:Microsoft.Phone.Controls.RoutedPropertyChangingEventArgs`1" />
    /// that contains the event data.</param>
    protected virtual void OnDropDownClosing(RoutedPropertyChangingEventArgs<bool> e)
    {
      RoutedPropertyChangingEventHandler<bool> dropDownClosing = this.DropDownClosing;
      if (dropDownClosing == null)
        return;
      dropDownClosing((object) this, e);
    }

    protected virtual void OnDropDownClosed(RoutedPropertyChangedEventArgs<bool> e)
    {
      RoutedPropertyChangedEventHandler<bool> dropDownClosed = this.DropDownClosed;
      if (dropDownClosed == null)
        return;
      dropDownClosed((object) this, e);
    }

    /// <summary>
    /// Formats an Item for text comparisons based on Converter
    /// and ConverterCulture properties.
    /// </summary>
    /// <param name="value">The object to format.</param>
    /// <param name="clearDataContext">A value indicating whether to clear
    /// the data context after the lookup is performed.</param>
    /// <returns>Formatted Value.</returns>
    private string FormatValue(object value, bool clearDataContext)
    {
      string str = this.FormatValue(value);
      if (clearDataContext && this._valueBindingEvaluator != null)
        this._valueBindingEvaluator.ClearDataContext();
      return str;
    }

    /// <summary>
    /// Converts the specified object to a string by using the
    /// <see cref="P:System.Windows.Data.Binding.Converter" /> and
    /// <see cref="P:System.Windows.Data.Binding.ConverterCulture" /> values
    /// of the binding object specified by the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ValueMemberBinding" />
    /// property.
    /// </summary>
    /// <param name="value">The object to format as a string.</param>
    /// <returns>The string representation of the specified object.</returns>
    /// <remarks>
    /// Override this method to provide a custom string conversion.
    /// </remarks>
    protected virtual string FormatValue(object value) => this._valueBindingEvaluator != null ? this._valueBindingEvaluator.GetDynamicValue(value) ?? string.Empty : (value == null ? string.Empty : value.ToString());

    /// <summary>
    /// Raises the
    /// <see cref="E:Microsoft.Phone.Controls.AutoCompleteBox.TextChanged" />
    /// event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.RoutedEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnTextChanged(RoutedEventArgs e)
    {
      RoutedEventHandler textChanged = this.TextChanged;
      if (textChanged == null)
        return;
      textChanged((object) this, e);
    }

    /// <summary>
    /// Handle the TextChanged event that is directly attached to the
    /// TextBox part. This ensures that only user initiated actions will
    /// result in an AutoCompleteBox suggestion and operation.
    /// </summary>
    /// <param name="sender">The source TextBox object.</param>
    /// <param name="e">The TextChanged event data.</param>
    private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e) => this.TextUpdated(this._text.Text, true);

    /// <summary>
    /// When selection changes, save the location of the selection start.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnTextBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
      if (this._ignoreTextSelectionChange || this._inputtingText)
        return;
      this._textSelectionStart = this._text.SelectionStart;
    }

    /// <summary>
    /// Handles KeyDown to set a flag that indicates that the user is inputting
    /// text.  This is important for IME input.
    /// </summary>
    /// <param name="sender">The source UIElement object.</param>
    /// <param name="e">The KeyDown event data.</param>
    private void OnUIElementKeyDown(object sender, KeyEventArgs e) => this._inputtingText = true;

    /// <summary>
    /// Handles KeyUp to turn off the flag that indicates that the user is inputting
    /// text.  This is important for IME input.
    /// </summary>
    /// <param name="sender">The source UIElement object.</param>
    /// <param name="e">The KeyUp event data.</param>
    private void OnUIElementKeyUp(object sender, KeyEventArgs e) => this._inputtingText = false;

    /// <summary>
    /// Updates both the text box value and underlying text dependency
    /// property value if and when they change. Automatically fires the
    /// text changed events when there is a change.
    /// </summary>
    /// <param name="value">The new string value.</param>
    private void UpdateTextValue(string value) => this.UpdateTextValue(value, new bool?());

    /// <summary>
    /// Updates both the text box value and underlying text dependency
    /// property value if and when they change. Automatically fires the
    /// text changed events when there is a change.
    /// </summary>
    /// <param name="value">The new string value.</param>
    /// <param name="userInitiated">A nullable bool value indicating whether
    /// the action was user initiated. In a user initiated mode, the
    /// underlying text dependency property is updated. In a non-user
    /// interaction, the text box value is updated. When user initiated is
    /// null, all values are updated.</param>
    private void UpdateTextValue(string value, bool? userInitiated)
    {
      bool? nullable;
      int num1;
      if (userInitiated.HasValue)
      {
        nullable = userInitiated;
        if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
        {
          num1 = 1;
          goto label_4;
        }
      }
      num1 = !(this.Text != value) ? 1 : 0;
label_4:
      if (num1 == 0)
      {
        ++this._ignoreTextPropertyChange;
        this.Text = value;
        this.OnTextChanged(new RoutedEventArgs());
      }
      if (userInitiated.HasValue)
      {
        nullable = userInitiated;
        if ((nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
          goto label_10;
      }
      int num2;
      if (this.TextBox != null)
      {
        num2 = !(this.TextBox.Text != value) ? 1 : 0;
        goto label_11;
      }
label_10:
      num2 = 1;
label_11:
      if (num2 != 0)
        return;
      ++this._ignoreTextPropertyChange;
      this.TextBox.Text = value ?? string.Empty;
      if (this.Text == value || this.Text == null)
        this.OnTextChanged(new RoutedEventArgs());
    }

    /// <summary>
    /// Handle the update of the text for the control from any source,
    /// including the TextBox part and the Text dependency property.
    /// </summary>
    /// <param name="newText">The new text.</param>
    /// <param name="userInitiated">A value indicating whether the update
    /// is a user-initiated action. This should be a True value when the
    /// TextUpdated method is called from a TextBox event handler.</param>
    private void TextUpdated(string newText, bool userInitiated)
    {
      if (this._ignoreTextPropertyChange > 0)
      {
        --this._ignoreTextPropertyChange;
      }
      else
      {
        if (newText == null)
          newText = string.Empty;
        if (this.IsTextCompletionEnabled && this.TextBox != null && this.TextBox.SelectionLength > 0 && this.TextBox.SelectionStart != this.TextBox.Text.Length)
          return;
        bool flag = newText.Length >= this.MinimumPrefixLength && this.MinimumPrefixLength >= 0;
        this._userCalledPopulate = flag && userInitiated;
        this.UpdateTextValue(newText, new bool?(userInitiated));
        if (flag)
        {
          this._ignoreTextSelectionChange = true;
          if (this._delayTimer != null)
            this._delayTimer.Start();
          else
            this.PopulateDropDown((object) this, EventArgs.Empty);
        }
        else
        {
          this.SearchText = string.Empty;
          if (this.SelectedItem != null)
            this._skipSelectedItemTextUpdate = true;
          this.SelectedItem = (object) null;
          if (this.IsDropDownOpen)
            this.IsDropDownOpen = false;
        }
      }
    }

    /// <summary>
    /// Notifies the
    /// <see cref="T:Microsoft.Phone.Controls.AutoCompleteBox" /> that the
    /// <see cref="P:Microsoft.Phone.Controls.AutoCompleteBox.ItemsSource" />
    /// property has been set and the data can be filtered to provide
    /// possible matches in the drop-down.
    /// </summary>
    /// <remarks>
    /// Call this method when you are providing custom population of
    /// the drop-down portion of the AutoCompleteBox, to signal the control
    /// that you are done with the population process.
    /// Typically, you use PopulateComplete when the population process
    /// is a long-running process and you want to cancel built-in filtering
    ///  of the ItemsSource items. In this case, you can handle the
    /// Populated event and set PopulatingEventArgs.Cancel to true.
    /// When the long-running process has completed you call
    /// PopulateComplete to indicate the drop-down is populated.
    /// </remarks>
    public void PopulateComplete()
    {
      this.RefreshView();
      this.OnPopulated(new PopulatedEventArgs((IEnumerable) new ReadOnlyCollection<object>((IList<object>) this._view)));
      if (this.SelectionAdapter != null && this.SelectionAdapter.ItemsSource != this._view)
        this.SelectionAdapter.ItemsSource = (IEnumerable) this._view;
      bool flag = this._userCalledPopulate && this._view.Count > 0;
      if (flag != this.IsDropDownOpen)
      {
        this._ignorePropertyChange = true;
        this.IsDropDownOpen = flag;
      }
      if (this.IsDropDownOpen)
      {
        this.OpeningDropDown(false);
        if (this.DropDownPopup != null)
          this.DropDownPopup.Arrange(new Size?());
      }
      else
        this.ClosingDropDown(true);
      this.UpdateTextCompletion(this._userCalledPopulate);
    }

    /// <summary>
    /// Performs text completion, if enabled, and a lookup on the underlying
    /// item values for an exact match. Will update the SelectedItem value.
    /// </summary>
    /// <param name="userInitiated">A value indicating whether the operation
    /// was user initiated. Text completion will not be performed when not
    /// directly initiated by the user.</param>
    private void UpdateTextCompletion(bool userInitiated)
    {
      object obj1 = (object) null;
      string text = this.Text;
      if (this._view.Count > 0)
      {
        if (this.IsTextCompletionEnabled && this.TextBox != null && userInitiated)
        {
          int length1 = this.TextBox.Text.Length;
          int selectionStart = this.TextBox.SelectionStart;
          if (selectionStart == text.Length && selectionStart > this._textSelectionStart)
          {
            object obj2 = this.FilterMode == AutoCompleteFilterMode.StartsWith || this.FilterMode == AutoCompleteFilterMode.StartsWithCaseSensitive ? this._view[0] : this.TryGetMatch(text, this._view, AutoCompleteSearch.GetFilter(AutoCompleteFilterMode.StartsWith));
            if (obj2 != null)
            {
              obj1 = obj2;
              string str = this.FormatValue(obj2, true);
              int length2 = Math.Min(str.Length, this.Text.Length);
              if (AutoCompleteSearch.Equals(this.Text.Substring(0, length2), str.Substring(0, length2)))
              {
                this.UpdateTextValue(str);
                this.TextBox.SelectionStart = length1;
                this.TextBox.SelectionLength = str.Length - length1;
              }
            }
          }
        }
        else
          obj1 = this.TryGetMatch(text, this._view, AutoCompleteSearch.GetFilter(AutoCompleteFilterMode.EqualsCaseSensitive));
      }
      if (this.SelectedItem != obj1)
        this._skipSelectedItemTextUpdate = true;
      this.SelectedItem = obj1;
      if (!this._ignoreTextSelectionChange)
        return;
      this._ignoreTextSelectionChange = false;
      if (this.TextBox != null && !this._inputtingText)
        this._textSelectionStart = this.TextBox.SelectionStart;
    }

    /// <summary>
    /// Attempts to look through the view and locate the specific exact
    /// text match.
    /// </summary>
    /// <param name="searchText">The search text.</param>
    /// <param name="view">The view reference.</param>
    /// <param name="predicate">The predicate to use for the partial or
    /// exact match.</param>
    /// <returns>Returns the object or null.</returns>
    private object TryGetMatch(
      string searchText,
      ObservableCollection<object> view,
      AutoCompleteFilterPredicate<string> predicate)
    {
      if (view != null && view.Count > 0)
      {
        foreach (object match in (Collection<object>) view)
        {
          if (predicate(searchText, this.FormatValue(match)))
            return match;
        }
      }
      return (object) null;
    }

    /// <summary>
    /// A simple helper method to clear the view and ensure that a view
    /// object is always present and not null.
    /// </summary>
    private void ClearView()
    {
      if (this._view == null)
        this._view = new ObservableCollection<object>();
      else
        this._view.Clear();
    }

    /// <summary>
    /// Walks through the items enumeration. Performance is not going to be
    /// perfect with the current implementation.
    /// </summary>
    private void RefreshView()
    {
      if (this._items == null)
      {
        this.ClearView();
      }
      else
      {
        string search = this.Text ?? string.Empty;
        bool flag1 = this.TextFilter != null;
        bool flag2 = this.FilterMode == AutoCompleteFilterMode.Custom && this.TextFilter == null;
        int index = 0;
        int count = this._view.Count;
        foreach (object obj in this._items)
        {
          bool flag3 = !flag1 && !flag2;
          if (!flag3)
            flag3 = flag1 ? this.TextFilter(search, this.FormatValue(obj)) : this.ItemFilter(search, obj);
          if (count > index && flag3 && this._view[index] == obj)
            ++index;
          else if (flag3)
          {
            if (count > index && this._view[index] != obj)
            {
              this._view.RemoveAt(index);
              this._view.Insert(index, obj);
              ++index;
            }
            else
            {
              if (index == count)
                this._view.Add(obj);
              else
                this._view.Insert(index, obj);
              ++index;
              ++count;
            }
          }
          else if (count > index && this._view[index] == obj)
          {
            this._view.RemoveAt(index);
            --count;
          }
        }
        if (this._valueBindingEvaluator == null)
          return;
        this._valueBindingEvaluator.ClearDataContext();
      }
    }

    /// <summary>
    /// Handle any change to the ItemsSource dependency property, update
    /// the underlying ObservableCollection view, and set the selection
    /// adapter's ItemsSource to the view if appropriate.
    /// </summary>
    /// <param name="oldValue">The old enumerable reference.</param>
    /// <param name="newValue">The new enumerable reference.</param>
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "oldValue", Justification = "This makes it easy to add validation or other changes in the future.")]
    private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
      if (oldValue is INotifyCollectionChanged && null != this._collectionChangedWeakEventListener)
      {
        this._collectionChangedWeakEventListener.Detach();
        this._collectionChangedWeakEventListener = (WeakEventListener<AutoCompleteBox, object, NotifyCollectionChangedEventArgs>) null;
      }
      INotifyCollectionChanged newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
      if (null != newValueINotifyCollectionChanged)
      {
        this._collectionChangedWeakEventListener = new WeakEventListener<AutoCompleteBox, object, NotifyCollectionChangedEventArgs>(this);
        this._collectionChangedWeakEventListener.OnEventAction = (Action<AutoCompleteBox, object, NotifyCollectionChangedEventArgs>) ((instance, source, eventArgs) => instance.ItemsSourceCollectionChanged(source, eventArgs));
        this._collectionChangedWeakEventListener.OnDetachAction = (Action<WeakEventListener<AutoCompleteBox, object, NotifyCollectionChangedEventArgs>>) (weakEventListener => newValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(weakEventListener.OnEvent));
        newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(this._collectionChangedWeakEventListener.OnEvent);
      }
      this._items = newValue == null ? (List<object>) null : new List<object>((IEnumerable<object>) newValue.Cast<object>().ToList<object>());
      this.ClearView();
      if (this.SelectionAdapter != null && this.SelectionAdapter.ItemsSource != this._view)
        this.SelectionAdapter.ItemsSource = (IEnumerable) this._view;
      if (!this.IsDropDownOpen)
        return;
      this.RefreshView();
    }

    /// <summary>
    /// Method that handles the ObservableCollection.CollectionChanged event for the ItemsSource property.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event data.</param>
    private void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
      {
        for (int index = 0; index < e.OldItems.Count; ++index)
          this._items.RemoveAt(e.OldStartingIndex);
      }
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null && this._items.Count >= e.NewStartingIndex)
      {
        for (int index = 0; index < e.NewItems.Count; ++index)
          this._items.Insert(e.NewStartingIndex + index, e.NewItems[index]);
      }
      if (e.Action == NotifyCollectionChangedAction.Replace && e.NewItems != null && e.OldItems != null)
      {
        for (int index = 0; index < e.NewItems.Count; ++index)
          this._items[e.NewStartingIndex] = e.NewItems[index];
      }
      if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
      {
        for (int index = 0; index < e.OldItems.Count; ++index)
          this._view.Remove(e.OldItems[index]);
      }
      if (e.Action == NotifyCollectionChangedAction.Reset)
      {
        this.ClearView();
        if (this.ItemsSource != null)
          this._items = new List<object>((IEnumerable<object>) this.ItemsSource.Cast<object>().ToList<object>());
      }
      this.RefreshView();
    }

    /// <summary>
    /// Handles the SelectionChanged event of the selection adapter.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The selection changed event data.</param>
    private void OnAdapterSelectionChanged(object sender, SelectionChangedEventArgs e) => this.SelectedItem = this._adapter.SelectedItem;

    /// <summary>Handles the Commit event on the selection adapter.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnAdapterSelectionComplete(object sender, RoutedEventArgs e)
    {
      this.IsDropDownOpen = false;
      this.UpdateTextCompletion(false);
      if (this.TextBox == null)
        return;
      this.TextBox.Select(this.TextBox.Text.Length, 0);
      ((Control) this.TextBox).Focus();
    }

    /// <summary>Handles the Cancel event on the selection adapter.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OnAdapterSelectionCanceled(object sender, RoutedEventArgs e)
    {
      this.UpdateTextValue(this.SearchText);
      this.UpdateTextCompletion(false);
    }

    /// <summary>
    /// Handles MaxDropDownHeightChanged by re-arranging and updating the
    /// popup arrangement.
    /// </summary>
    /// <param name="newValue">The new value.</param>
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "newValue", Justification = "This makes it easy to add validation or other changes in the future.")]
    private void OnMaxDropDownHeightChanged(double newValue)
    {
      if (this.DropDownPopup != null)
      {
        this.DropDownPopup.MaxDropDownHeight = newValue;
        this.DropDownPopup.Arrange(new Size?());
      }
      this.UpdateVisualState(true);
    }

    /// <summary>
    /// Private method that directly opens the popup, checks the expander
    /// button, and then fires the Opened event.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private void OpenDropDown(bool oldValue, bool newValue)
    {
      if (this.DropDownPopup != null)
        this.DropDownPopup.IsOpen = true;
      this._popupHasOpened = true;
      this.OnDropDownOpened(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue));
    }

    /// <summary>
    /// Private method that directly closes the popup, flips the Checked
    /// value, and then fires the Closed event.
    /// </summary>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    private void CloseDropDown(bool oldValue, bool newValue)
    {
      if (!this._popupHasOpened)
        return;
      if (this.SelectionAdapter != null)
        this.SelectionAdapter.SelectedItem = (object) null;
      if (this.DropDownPopup != null)
        this.DropDownPopup.IsOpen = false;
      this.OnDropDownClosed(new RoutedPropertyChangedEventArgs<bool>(oldValue, newValue));
    }

    /// <summary>
    /// Provides handling for the
    /// <see cref="E:System.Windows.UIElement.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Input.KeyEventArgs" />
    /// that contains the event data.</param>
    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      base.OnKeyDown(e);
      if (e.Handled || !this.IsEnabled)
        return;
      if (this.IsDropDownOpen)
      {
        if (this.SelectionAdapter != null)
        {
          this.SelectionAdapter.HandleKeyDown(e);
          if (e.Handled)
            return;
        }
        if (e.Key == 8)
        {
          this.OnAdapterSelectionCanceled((object) this, new RoutedEventArgs());
          e.Handled = true;
        }
      }
      else if (e.Key == 17)
      {
        this.IsDropDownOpen = true;
        e.Handled = true;
      }
      Key key = e.Key;
      if (key != 3)
      {
        if (key != 59)
          return;
        this.IsDropDownOpen = !this.IsDropDownOpen;
        e.Handled = true;
      }
      else
      {
        this.OnAdapterSelectionComplete((object) this, new RoutedEventArgs());
        e.Handled = true;
      }
    }

    /// <summary>Update the visual state of the control.</summary>
    /// <param name="useTransitions">
    /// A value indicating whether to automatically generate transitions to
    /// the new state, or instantly transition to the new state.
    /// </param>
    void IUpdateVisualState.UpdateVisualState(bool useTransitions) => this.UpdateVisualState(useTransitions);

    /// <summary>Update the current visual state of the button.</summary>
    /// <param name="useTransitions">
    /// True to use transitions when updating the visual state, false to
    /// snap directly to the new visual state.
    /// </param>
    internal virtual void UpdateVisualState(bool useTransitions)
    {
      VisualStateManager.GoToState((Control) this, this.IsDropDownOpen ? "PopupOpened" : "PopupClosed", useTransitions);
      this.Interaction.UpdateVisualStateBase(useTransitions);
    }
  }
}
