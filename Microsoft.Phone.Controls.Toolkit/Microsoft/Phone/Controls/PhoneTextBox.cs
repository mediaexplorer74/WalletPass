// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PhoneTextBox
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// An extended TextBox for Windows Phone which implements hint text, an action icon, and a
  /// length indicator.
  /// </summary>
  /// <QualityBand>Experimental</QualityBand>
  [TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
  [TemplatePart(Name = "LengthIndicator", Type = typeof (TextBlock))]
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplatePart(Name = "HintContent", Type = typeof (ContentControl))]
  [TemplateVisualState(Name = "LengthIndicatorHidden", GroupName = "LengthIndicatorStates")]
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "LengthIndicatorVisible", GroupName = "LengthIndicatorStates")]
  [TemplateVisualState(Name = "ReadOnly", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
  [TemplatePart(Name = "Text", Type = typeof (TextBox))]
  public class PhoneTextBox : TextBox
  {
    /// <summary>Root grid.</summary>
    private const string RootGridName = "RootGrid";
    /// <summary>Main text box.</summary>
    private const string TextBoxName = "Text";
    /// <summary>Hint Content.</summary>
    private const string HintContentName = "HintContent";
    /// <summary>Hint border.</summary>
    private const string HintBorderName = "HintBorder";
    /// <summary>Length indicator name.</summary>
    private const string LengthIndicatorName = "LengthIndicator";
    /// <summary>Action icon canvas.</summary>
    private const string ActionIconCanvasName = "ActionIconCanvas";
    /// <summary>Measurement Text Block name.</summary>
    private const string MeasurementTextBlockName = "MeasurementTextBlock";
    /// <summary>Action icon image name.</summary>
    private const string ActionIconBorderName = "ActionIconBorder";
    /// <summary>Length indicator states.</summary>
    private const string LengthIndicatorStates = "LengthIndicatorStates";
    /// <summary>Length indicator visible visual state.</summary>
    private const string LengthIndicatorVisibleState = "LengthIndicatorVisible";
    /// <summary>Length indicator hidden visual state.</summary>
    private const string LengthIndicatorHiddenState = "LengthIndicatorHidden";
    /// <summary>Common States.</summary>
    private const string CommonStates = "CommonStates";
    /// <summary>Normal state.</summary>
    private const string NormalState = "Normal";
    /// <summary>Disabled state.</summary>
    private const string DisabledState = "Disabled";
    /// <summary>ReadOnly state.</summary>
    private const string ReadOnlyState = "ReadOnly";
    /// <summary>Focus states.</summary>
    private const string FocusStates = "FocusStates";
    /// <summary>Focused state.</summary>
    private const string FocusedState = "Focused";
    /// <summary>Unfocused state.</summary>
    private const string UnfocusedState = "Unfocused";
    private Grid RootGrid;
    private TextBox TextBox;
    private TextBlock MeasurementTextBlock;
    private Brush ForegroundBrushInactive = (Brush) Application.Current.Resources[(object) "PhoneTextBoxReadOnlyBrush"];
    private Brush ForegroundBrushEdit;
    private ContentControl HintContent;
    private Border HintBorder;
    private TextBlock LengthIndicator;
    private Border ActionIconBorder;
    private bool _ignorePropertyChange = false;
    private bool _ignoreFocus = false;
    /// <summary>Identifies the Hint DependencyProperty.</summary>
    public static readonly DependencyProperty HintProperty = DependencyProperty.Register(nameof (Hint), typeof (string), typeof (PhoneTextBox), new PropertyMetadata(new PropertyChangedCallback(PhoneTextBox.OnHintPropertyChanged)));
    /// <summary>Identifies the HintStyle DependencyProperty.</summary>
    public static readonly DependencyProperty HintStyleProperty = DependencyProperty.Register(nameof (HintStyle), typeof (Style), typeof (PhoneTextBox), (PropertyMetadata) null);
    /// <summary>Identifies the HintVisibility DependencyProperty</summary>
    public static readonly DependencyProperty ActualHintVisibilityProperty = DependencyProperty.Register(nameof (ActualHintVisibility), typeof (Visibility), typeof (PhoneTextBox), (PropertyMetadata) null);
    /// <summary>Length Indicator Visibile Dependency Property.</summary>
    public static readonly DependencyProperty LengthIndicatorVisibleProperty = DependencyProperty.Register(nameof (LengthIndicatorVisible), typeof (bool), typeof (PhoneTextBox), (PropertyMetadata) null);
    /// <summary>
    /// Length Indicator Visibility Threshold Dependency Property.
    /// </summary>
    public static readonly DependencyProperty LengthIndicatorThresholdProperty = DependencyProperty.Register(nameof (LengthIndicatorThreshold), typeof (int), typeof (PhoneTextBox), new PropertyMetadata(new PropertyChangedCallback(PhoneTextBox.OnLengthIndicatorThresholdChanged)));
    /// <summary>
    /// The displayed maximum length of text that can be entered. This value takes
    /// priority over the MaxLength property in the Length Indicator display.
    /// </summary>
    public static readonly DependencyProperty DisplayedMaxLengthProperty = DependencyProperty.Register(nameof (DisplayedMaxLength), typeof (int), typeof (PhoneTextBox), new PropertyMetadata(new PropertyChangedCallback(PhoneTextBox.DisplayedMaxLengthChanged)));
    /// <summary>Identifies the ActionIcon DependencyProperty.</summary>
    public static readonly DependencyProperty ActionIconProperty = DependencyProperty.Register(nameof (ActionIcon), typeof (ImageSource), typeof (PhoneTextBox), new PropertyMetadata(new PropertyChangedCallback(PhoneTextBox.OnActionIconChanged)));
    /// <summary>
    /// Identifies the HidesActionItemWhenEmpty DependencyProperty.
    /// </summary>
    public static readonly DependencyProperty HidesActionItemWhenEmptyProperty = DependencyProperty.Register(nameof (HidesActionItemWhenEmpty), typeof (bool), typeof (PhoneTextBox), new PropertyMetadata((object) false, new PropertyChangedCallback(PhoneTextBox.OnActionIconChanged)));

    /// <summary>Gets or sets the Hint</summary>
    public string Hint
    {
      get => ((DependencyObject) this).GetValue(PhoneTextBox.HintProperty) as string;
      set => ((DependencyObject) this).SetValue(PhoneTextBox.HintProperty, (object) value);
    }

    /// <summary>Gets or sets the Hint style</summary>
    public Style HintStyle
    {
      get => ((DependencyObject) this).GetValue(PhoneTextBox.HintStyleProperty) as Style;
      set => ((DependencyObject) this).SetValue(PhoneTextBox.HintStyleProperty, (object) value);
    }

    /// <summary>Gets or sets whether the hint is actually visible.</summary>
    public Visibility ActualHintVisibility
    {
      get => (Visibility) ((DependencyObject) this).GetValue(PhoneTextBox.ActualHintVisibilityProperty);
      set => ((DependencyObject) this).SetValue(PhoneTextBox.ActualHintVisibilityProperty, (object) value);
    }

    /// <summary>
    /// When the Hint is changed, check if it needs to be hidden or shown.
    /// </summary>
    /// <param name="sender">Sending PhoneTextBox.</param>
    /// <param name="args">DependencyPropertyChangedEvent Arguments.</param>
    private static void OnHintPropertyChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(sender is PhoneTextBox phoneTextBox) || phoneTextBox.HintContent == null)
        return;
      phoneTextBox.UpdateHintVisibility();
    }

    /// <summary>
    /// Determines if the Hint should be shown or not based on if there is content in the TextBox.
    /// </summary>
    private void UpdateHintVisibility()
    {
      if (this.HintContent == null)
        return;
      if (string.IsNullOrEmpty(this.Text))
      {
        this.ActualHintVisibility = (Visibility) 0;
        ((Control) this).Foreground = this.ForegroundBrushInactive;
      }
      else
      {
        this.ActualHintVisibility = (Visibility) 1;
        ((Control) this).Foreground = this.ForegroundBrushEdit;
      }
    }

    /// <summary>
    /// Override the Blur/LostFocus event to show the Hint if needed.
    /// </summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnLostFocus(RoutedEventArgs e)
    {
      this.UpdateHintVisibility();
      base.OnLostFocus(e);
    }

    /// <summary>Override the Focus event to hide the Hint.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnGotFocus(RoutedEventArgs e)
    {
      if (this._ignoreFocus)
      {
        this._ignoreFocus = false;
        ((Control) (Application.Current.RootVisual as Frame)).Focus();
      }
      else
      {
        ((Control) this).Foreground = this.ForegroundBrushEdit;
        if (this.HintContent != null)
          this.ActualHintVisibility = (Visibility) 1;
        base.OnGotFocus(e);
      }
    }

    /// <summary>
    /// Boolean that determines if the length indicator should be visible.
    /// </summary>
    public bool LengthIndicatorVisible
    {
      get => (bool) ((DependencyObject) this).GetValue(PhoneTextBox.LengthIndicatorVisibleProperty);
      set => ((DependencyObject) this).SetValue(PhoneTextBox.LengthIndicatorVisibleProperty, (object) value);
    }

    /// <summary>
    /// Threshold after which the length indicator will appear.
    /// </summary>
    public int LengthIndicatorThreshold
    {
      get => (int) ((DependencyObject) this).GetValue(PhoneTextBox.LengthIndicatorThresholdProperty);
      set => ((DependencyObject) this).SetValue(PhoneTextBox.LengthIndicatorThresholdProperty, (object) value);
    }

    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "The property name is the appropriate value to throw.")]
    private static void OnLengthIndicatorThresholdChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      PhoneTextBox phoneTextBox = sender as PhoneTextBox;
      if (phoneTextBox._ignorePropertyChange)
        phoneTextBox._ignorePropertyChange = false;
      else if (phoneTextBox.LengthIndicatorThreshold < 0)
      {
        phoneTextBox._ignorePropertyChange = true;
        ((DependencyObject) phoneTextBox).SetValue(PhoneTextBox.LengthIndicatorThresholdProperty, args.OldValue);
        throw new ArgumentOutOfRangeException("LengthIndicatorThreshold", "The length indicator visibility threshold must be greater than zero.");
      }
    }

    /// <summary>
    /// The displayed value for the maximum length of the input.
    /// </summary>
    public int DisplayedMaxLength
    {
      get => (int) ((DependencyObject) this).GetValue(PhoneTextBox.DisplayedMaxLengthProperty);
      set => ((DependencyObject) this).SetValue(PhoneTextBox.DisplayedMaxLengthProperty, (object) value);
    }

    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "The property name is the appropriate value to throw.")]
    private static void DisplayedMaxLengthChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      PhoneTextBox phoneTextBox = sender as PhoneTextBox;
      if (phoneTextBox.DisplayedMaxLength > phoneTextBox.MaxLength && phoneTextBox.MaxLength > 0)
        throw new ArgumentOutOfRangeException("DisplayedMaxLength", "The displayed maximum length cannot be greater than the MaxLength.");
    }

    [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Controls.TextBlock.set_Text(System.String)", Justification = "At this time the length indicator is not culture-specific or retrieved from the resources.")]
    private void UpdateLengthIndicatorVisibility()
    {
      if (this.RootGrid == null || this.LengthIndicator == null)
        return;
      bool flag = true;
      if (this.LengthIndicatorVisible)
      {
        this.LengthIndicator.Text = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}/{1}", new object[2]
        {
          (object) this.Text.Length,
          (object) (this.DisplayedMaxLength > 0 ? this.DisplayedMaxLength : this.MaxLength)
        });
        if (this.Text.Length >= this.LengthIndicatorThreshold)
          flag = false;
      }
      VisualStateManager.GoToState((Control) this, flag ? "LengthIndicatorHidden" : "LengthIndicatorVisible", false);
    }

    /// <summary>Gets or sets the ActionIcon.</summary>
    public ImageSource ActionIcon
    {
      get => ((DependencyObject) this).GetValue(PhoneTextBox.ActionIconProperty) as ImageSource;
      set => ((DependencyObject) this).SetValue(PhoneTextBox.ActionIconProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets whether the ActionItem is hidden when there is not text entered in the PhoneTextBox.
    /// </summary>
    public bool HidesActionItemWhenEmpty
    {
      get => (bool) ((DependencyObject) this).GetValue(PhoneTextBox.HidesActionItemWhenEmptyProperty);
      set => ((DependencyObject) this).SetValue(PhoneTextBox.HidesActionItemWhenEmptyProperty, (object) value);
    }

    /// <summary>Action Icon Tapped event.</summary>
    public event EventHandler ActionIconTapped;

    private static void OnActionIconChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(sender is PhoneTextBox phoneTextBox))
        return;
      phoneTextBox.UpdateActionIconVisibility();
    }

    private void UpdateActionIconVisibility()
    {
      if (this.ActionIconBorder == null)
        return;
      if (this.ActionIcon == null || this.HidesActionItemWhenEmpty && string.IsNullOrEmpty(this.Text))
      {
        ((UIElement) this.ActionIconBorder).Visibility = (Visibility) 1;
        this.HintBorder.Padding = new Thickness(0.0);
      }
      else
      {
        ((UIElement) this.ActionIconBorder).Visibility = (Visibility) 0;
        if (this.TextWrapping != 2)
          this.HintBorder.Padding = new Thickness(0.0, 0.0, 48.0, 0.0);
      }
    }

    /// <summary>
    /// Determines if the developer set an event for ActionIconTapped.
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">The RoutedEventArgs for the event</param>
    private void OnActionIconTapped(object sender, RoutedEventArgs e)
    {
      this._ignoreFocus = true;
      EventHandler actionIconTapped = this.ActionIconTapped;
      if (actionIconTapped == null)
        return;
      actionIconTapped((object) this, (EventArgs) e);
    }

    private void ResizeTextBox()
    {
      if (this.ActionIcon == null || this.TextWrapping != 2)
        return;
      ((FrameworkElement) this.MeasurementTextBlock).Width = ((FrameworkElement) this).ActualWidth;
      if (((FrameworkElement) this.MeasurementTextBlock).ActualHeight > ((FrameworkElement) this).ActualHeight - 72.0)
      {
        ((FrameworkElement) this).Height = ((FrameworkElement) this).ActualHeight + 72.0;
      }
      else
      {
        if (((FrameworkElement) this).ActualHeight <= ((FrameworkElement) this.MeasurementTextBlock).ActualHeight + 144.0)
          return;
        ((FrameworkElement) this).Height = ((FrameworkElement) this).ActualHeight - 72.0;
      }
    }

    /// <summary>Initializes a new instance of the PhoneTextBox class.</summary>
    public PhoneTextBox() => ((Control) this).DefaultStyleKey = (object) typeof (PhoneTextBox);

    /// <summary>
    /// Applies the template and checks to see if the Hint should be shown
    /// when the page is first loaded.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (this.TextBox != null)
        this.TextBox.TextChanged -= new TextChangedEventHandler(this.OnTextChanged);
      if (this.ActionIconBorder != null)
        ((UIElement) this.ActionIconBorder).MouseLeftButtonDown -= new MouseButtonEventHandler(this.OnActionIconTapped);
      this.RootGrid = ((Control) this).GetTemplateChild("RootGrid") as Grid;
      this.TextBox = ((Control) this).GetTemplateChild("Text") as TextBox;
      this.ForegroundBrushEdit = ((Control) this).Foreground;
      this.HintContent = ((Control) this).GetTemplateChild("HintContent") as ContentControl;
      this.HintBorder = ((Control) this).GetTemplateChild("HintBorder") as Border;
      if (this.HintContent != null)
        this.UpdateHintVisibility();
      this.LengthIndicator = ((Control) this).GetTemplateChild("LengthIndicator") as TextBlock;
      this.ActionIconBorder = ((Control) this).GetTemplateChild("ActionIconBorder") as Border;
      if (this.RootGrid != null && this.LengthIndicator != null)
        this.UpdateLengthIndicatorVisibility();
      if (this.TextBox != null)
        this.TextBox.TextChanged += new TextChangedEventHandler(this.OnTextChanged);
      if (this.ActionIconBorder != null)
      {
        ((UIElement) this.ActionIconBorder).MouseLeftButtonDown += new MouseButtonEventHandler(this.OnActionIconTapped);
        this.UpdateActionIconVisibility();
      }
      this.MeasurementTextBlock = ((Control) this).GetTemplateChild("MeasurementTextBlock") as TextBlock;
    }

    /// <summary>
    /// Called when the selection changed event occurs. This determines whether the length indicator should be shown or
    /// not and if the TextBox needs to grow.
    /// </summary>
    /// <param name="sender">Sender TextBox</param>
    /// <param name="e">Event arguments</param>
    private void OnTextChanged(object sender, RoutedEventArgs e)
    {
      this.UpdateLengthIndicatorVisibility();
      this.UpdateActionIconVisibility();
      this.UpdateHintVisibility();
      this.ResizeTextBox();
    }
  }
}
