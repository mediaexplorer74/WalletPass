// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DateTimePickerBase
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a base class for controls that allow the user to choose a date/time.
  /// </summary>
  [TemplatePart(Name = "DateTimeButton", Type = typeof (ButtonBase))]
  public class DateTimePickerBase : Control
  {
    private const string ButtonPartName = "DateTimeButton";
    private ButtonBase _dateButtonPart;
    private PhoneApplicationFrame _frame;
    private object _frameContentWhenOpened;
    private NavigationInTransition _savedNavigationInTransition;
    private NavigationOutTransition _savedNavigationOutTransition;
    private IDateTimePickerPage _dateTimePickerPage;
    /// <summary>Identifies the Value DependencyProperty.</summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (DateTime?), typeof (DateTimePickerBase), new PropertyMetadata((object) null, new PropertyChangedCallback(DateTimePickerBase.OnValueChanged)));
    /// <summary>Identifies the ValueString DependencyProperty.</summary>
    public static readonly DependencyProperty ValueStringProperty = DependencyProperty.Register(nameof (ValueString), typeof (string), typeof (DateTimePickerBase), (PropertyMetadata) null);
    /// <summary>Identifies the ValueStringFormat DependencyProperty.</summary>
    public static readonly DependencyProperty ValueStringFormatProperty = DependencyProperty.Register(nameof (ValueStringFormat), typeof (string), typeof (DateTimePickerBase), new PropertyMetadata((object) null, new PropertyChangedCallback(DateTimePickerBase.OnValueStringFormatChanged)));
    /// <summary>Identifies the Header DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (DateTimePickerBase), (PropertyMetadata) null);
    /// <summary>Identifies the HeaderTemplate DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(nameof (HeaderTemplate), typeof (DataTemplate), typeof (DateTimePickerBase), (PropertyMetadata) null);
    /// <summary>Identifies the PickerPageUri DependencyProperty.</summary>
    public static readonly DependencyProperty PickerPageUriProperty = DependencyProperty.Register(nameof (PickerPageUri), typeof (Uri), typeof (DateTimePickerBase), (PropertyMetadata) null);

    /// <summary>
    /// Event that is invoked when the Value property changes.
    /// </summary>
    public event EventHandler<DateTimeValueChangedEventArgs> ValueChanged;

    /// <summary>Gets or sets the DateTime value.</summary>
    [TypeConverter(typeof (TimeTypeConverter))]
    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Matching the use of Value as a Picker naming convention.")]
    public DateTime? Value
    {
      get => (DateTime?) ((DependencyObject) this).GetValue(DateTimePickerBase.ValueProperty);
      set => ((DependencyObject) this).SetValue(DateTimePickerBase.ValueProperty, (object) value);
    }

    private static void OnValueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) => ((DateTimePickerBase) o).OnValueChanged((DateTime?) e.OldValue, (DateTime?) e.NewValue);

    private void OnValueChanged(DateTime? oldValue, DateTime? newValue)
    {
      this.UpdateValueString();
      this.OnValueChanged(new DateTimeValueChangedEventArgs(oldValue, newValue));
    }

    /// <summary>Called when the value changes.</summary>
    /// <param name="e">The event data.</param>
    protected virtual void OnValueChanged(DateTimeValueChangedEventArgs e)
    {
      EventHandler<DateTimeValueChangedEventArgs> valueChanged = this.ValueChanged;
      if (null == valueChanged)
        return;
      valueChanged((object) this, e);
    }

    /// <summary>Gets the string representation of the selected value.</summary>
    public string ValueString
    {
      get => (string) ((DependencyObject) this).GetValue(DateTimePickerBase.ValueStringProperty);
      private set => ((DependencyObject) this).SetValue(DateTimePickerBase.ValueStringProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the format string to use when converting the Value property to a string.
    /// </summary>
    public string ValueStringFormat
    {
      get => (string) ((DependencyObject) this).GetValue(DateTimePickerBase.ValueStringFormatProperty);
      set => ((DependencyObject) this).SetValue(DateTimePickerBase.ValueStringFormatProperty, (object) value);
    }

    private static void OnValueStringFormatChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((DateTimePickerBase) o).OnValueStringFormatChanged();
    }

    private void OnValueStringFormatChanged() => this.UpdateValueString();

    /// <summary>Gets or sets the header of the control.</summary>
    public object Header
    {
      get => ((DependencyObject) this).GetValue(DateTimePickerBase.HeaderProperty);
      set => ((DependencyObject) this).SetValue(DateTimePickerBase.HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the control's header.
    /// </summary>
    public DataTemplate HeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(DateTimePickerBase.HeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(DateTimePickerBase.HeaderTemplateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the Uri to use for loading the IDateTimePickerPage instance when the control is clicked.
    /// </summary>
    public Uri PickerPageUri
    {
      get => (Uri) ((DependencyObject) this).GetValue(DateTimePickerBase.PickerPageUriProperty);
      set => ((DependencyObject) this).SetValue(DateTimePickerBase.PickerPageUriProperty, (object) value);
    }

    /// <summary>
    /// Gets the fallback value for the ValueStringFormat property.
    /// </summary>
    protected virtual string ValueStringFormatFallback => "{0}";

    /// <summary>Called when the control's Template is expanded.</summary>
    public virtual void OnApplyTemplate()
    {
      if (null != this._dateButtonPart)
        this._dateButtonPart.Click -= new RoutedEventHandler(this.OnDateButtonClick);
      ((FrameworkElement) this).OnApplyTemplate();
      this._dateButtonPart = this.GetTemplateChild("DateTimeButton") as ButtonBase;
      if (null == this._dateButtonPart)
        return;
      this._dateButtonPart.Click += new RoutedEventHandler(this.OnDateButtonClick);
    }

    /// <summary>
    /// Date should flow from right to left for arabic and persian.
    /// </summary>
    internal static bool DateShouldFlowRTL()
    {
      string letterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
      return letterIsoLanguageName == "ar" || letterIsoLanguageName == "fa";
    }

    /// <summary>Returns true if the current language is RTL.</summary>
    internal static bool IsRTLLanguage()
    {
      string letterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
      return letterIsoLanguageName == "ar" || letterIsoLanguageName == "he" || letterIsoLanguageName == "fa";
    }

    /// <summary>Makes open the page</summary>
    public void Click() => this.OpenPickerPage();

    private void OnDateButtonClick(object sender, RoutedEventArgs e) => this.OpenPickerPage();

    private void UpdateValueString() => this.ValueString = string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.ValueStringFormat ?? this.ValueStringFormatFallback, new object[1]
    {
      (object) this.Value
    });

    private void OpenPickerPage()
    {
      if ((Uri) null == this.PickerPageUri)
        throw new ArgumentException("PickerPageUri property must not be null.");
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
        ((Frame) this._frame).Navigate(this.PickerPageUri);
      }
    }

    private void ClosePickerPage()
    {
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
      if (null == this._dateTimePickerPage)
        return;
      DateTime? nullable = this._dateTimePickerPage.Value;
      if (nullable.HasValue)
      {
        nullable = this._dateTimePickerPage.Value;
        this.Value = new DateTime?(nullable.Value);
      }
      this._dateTimePickerPage = (IDateTimePickerPage) null;
    }

    private void OnFrameNavigated(object sender, NavigationEventArgs e)
    {
      if (e.Content == this._frameContentWhenOpened)
      {
        this.ClosePickerPage();
      }
      else
      {
        if (null != this._dateTimePickerPage)
          return;
        DateTimePickerPageBase content = e.Content as DateTimePickerPageBase;
        if (null != content)
        {
          this._dateTimePickerPage = (IDateTimePickerPage) content;
          this._dateTimePickerPage.Value = new DateTime?(this.Value.GetValueOrDefault(DateTime.Now));
          content.SetFlowDirection(((FrameworkElement) this).FlowDirection);
        }
      }
    }

    private void OnFrameNavigationStoppedOrFailed(object sender, EventArgs e) => this.ClosePickerPage();
  }
}
