// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ToggleSwitch
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a switch that can be toggled between two states.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplatePart(Name = "Switch", Type = typeof (ToggleSwitchButton))]
  public class ToggleSwitch : ContentControl
  {
    /// <summary>Common visual states.</summary>
    private const string CommonStates = "CommonStates";
    /// <summary>Normal visual state.</summary>
    private const string NormalState = "Normal";
    /// <summary>Disabled visual state.</summary>
    private const string DisabledState = "Disabled";
    /// <summary>The ToggleButton that drives this.</summary>
    private const string SwitchPart = "Switch";
    /// <summary>Identifies the Header DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof (Header), typeof (object), typeof (ToggleSwitch), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>Identifies the HeaderTemplate DependencyProperty.</summary>
    public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(nameof (HeaderTemplate), typeof (DataTemplate), typeof (ToggleSwitch), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>Identifies the SwitchForeground DependencyProperty.</summary>
    public static readonly DependencyProperty SwitchForegroundProperty = DependencyProperty.Register(nameof (SwitchForeground), typeof (Brush), typeof (ToggleSwitch), (PropertyMetadata) null);
    /// <summary>Identifies the IsChecked DependencyProperty.</summary>
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof (IsChecked), typeof (bool?), typeof (ToggleSwitch), new PropertyMetadata((object) false, new PropertyChangedCallback(ToggleSwitch.OnIsCheckedChanged)));
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.Controls.Primitives.ToggleButton" />
    /// template part.
    /// </summary>
    private ToggleSwitchButton _toggleButton;
    /// <summary>Whether the content was set.</summary>
    private bool _wasContentSet;

    /// <summary>Gets or sets the header.</summary>
    public object Header
    {
      get => ((DependencyObject) this).GetValue(ToggleSwitch.HeaderProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.HeaderProperty, value);
    }

    /// <summary>
    /// Gets or sets the template used to display the control's header.
    /// </summary>
    public DataTemplate HeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(ToggleSwitch.HeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.HeaderTemplateProperty, (object) value);
    }

    /// <summary>Gets or sets the switch foreground.</summary>
    public Brush SwitchForeground
    {
      get => (Brush) ((DependencyObject) this).GetValue(ToggleSwitch.SwitchForegroundProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitch.SwitchForegroundProperty, (object) value);
    }

    /// <summary>Gets or sets whether the ToggleSwitch is checked.</summary>
    [TypeConverter(typeof (NullableBoolConverter))]
    public bool? IsChecked
    {
      get => (bool?) ((DependencyObject) this).GetValue(ToggleSwitch.IsCheckedProperty);
      set
      {
        bool? isChecked = this.IsChecked;
        bool? nullable = value;
        if ((isChecked.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : (isChecked.HasValue != nullable.HasValue ? 1 : 0)) == 0)
          return;
        ((DependencyObject) this).SetValue(ToggleSwitch.IsCheckedProperty, (object) value);
      }
    }

    /// <summary>
    /// Invoked when the IsChecked DependencyProperty is changed.
    /// </summary>
    /// <param name="d">The event sender.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ToggleSwitch toggleSwitch = (ToggleSwitch) d;
      if (toggleSwitch._toggleButton == null)
        return;
      toggleSwitch._toggleButton.IsChecked = (bool?) e.NewValue;
    }

    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.ToggleSwitch" />
    /// is checked.
    /// </summary>
    public event EventHandler<RoutedEventArgs> Checked;

    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.ToggleSwitch" />
    /// is unchecked.
    /// </summary>
    public event EventHandler<RoutedEventArgs> Unchecked;

    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.ToggleSwitch" />
    /// is indeterminate.
    /// </summary>
    public event EventHandler<RoutedEventArgs> Indeterminate;

    /// <summary>
    /// Occurs when the
    /// <see cref="T:System.Windows.Controls.Primitives.ToggleButton" />
    /// is clicked.
    /// </summary>
    public event EventHandler<RoutedEventArgs> Click;

    /// <summary>Initializes a new instance of the ToggleSwitch class.</summary>
    public ToggleSwitch() => ((Control) this).DefaultStyleKey = (object) typeof (ToggleSwitch);

    /// <summary>
    /// Makes the content an "Off" or "On" string to match the state.
    /// </summary>
    private void SetDefaultContent()
    {
      Binding binding = new Binding("IsChecked")
      {
        Source = (object) this,
        Converter = (IValueConverter) new OffOnConverter()
      };
      ((FrameworkElement) this).SetBinding(ContentControl.ContentProperty, binding);
    }

    /// <summary>Change the visual state.</summary>
    /// <param name="useTransitions">Indicates whether to use animation transitions.</param>
    private void ChangeVisualState(bool useTransitions)
    {
      if (((Control) this).IsEnabled)
        VisualStateManager.GoToState((Control) this, "Normal", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Disabled", useTransitions);
    }

    /// <summary>
    /// Makes the content an "Off" or "On" string to match the state if the content is set to null in the design tool.
    /// </summary>
    /// <param name="oldContent">The old content.</param>
    /// <param name="newContent">The new content.</param>
    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      base.OnContentChanged(oldContent, newContent);
      this._wasContentSet = true;
      if (!DesignerProperties.IsInDesignTool || newContent != null || ((FrameworkElement) this).GetBindingExpression(ContentControl.ContentProperty) != null)
        return;
      this.SetDefaultContent();
    }

    /// <summary>
    /// Gets all the template parts and initializes the corresponding state.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (!this._wasContentSet && ((FrameworkElement) this).GetBindingExpression(ContentControl.ContentProperty) == null)
        this.SetDefaultContent();
      if (this._toggleButton != null)
      {
        this._toggleButton.Checked -= new RoutedEventHandler(this.OnChecked);
        this._toggleButton.Unchecked -= new RoutedEventHandler(this.OnUnchecked);
        this._toggleButton.Indeterminate -= new RoutedEventHandler(this.OnIndeterminate);
        ((ButtonBase) this._toggleButton).Click -= new RoutedEventHandler(this.OnClick);
      }
      this._toggleButton = ((Control) this).GetTemplateChild("Switch") as ToggleSwitchButton;
      if (this._toggleButton != null)
      {
        this._toggleButton.Checked += new RoutedEventHandler(this.OnChecked);
        this._toggleButton.Unchecked += new RoutedEventHandler(this.OnUnchecked);
        this._toggleButton.Indeterminate += new RoutedEventHandler(this.OnIndeterminate);
        ((ButtonBase) this._toggleButton).Click += new RoutedEventHandler(this.OnClick);
        this._toggleButton.IsChecked = this.IsChecked;
      }
      ((Control) this).IsEnabledChanged += (DependencyPropertyChangedEventHandler) ((param0, param1) => this.ChangeVisualState(true));
      this.ChangeVisualState(false);
    }

    /// <summary>
    /// Mirrors the
    /// <see cref="E:System.Windows.Controls.Primitives.ToggleButton.Checked" />
    /// event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnChecked(object sender, RoutedEventArgs e)
    {
      this.IsChecked = new bool?(true);
      SafeRaise.Raise<RoutedEventArgs>(this.Checked, (object) this, e);
    }

    /// <summary>
    /// Mirrors the
    /// <see cref="E:System.Windows.Controls.Primitives.ToggleButton.Unchecked" />
    /// event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnUnchecked(object sender, RoutedEventArgs e)
    {
      this.IsChecked = new bool?(false);
      SafeRaise.Raise<RoutedEventArgs>(this.Unchecked, (object) this, e);
    }

    /// <summary>
    /// Mirrors the
    /// <see cref="E:System.Windows.Controls.Primitives.ToggleButton.Indeterminate" />
    /// event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnIndeterminate(object sender, RoutedEventArgs e)
    {
      this.IsChecked = new bool?();
      SafeRaise.Raise<RoutedEventArgs>(this.Indeterminate, (object) this, e);
    }

    /// <summary>
    /// Mirrors the
    /// <see cref="E:System.Windows.Controls.Primitives.ToggleButton.Click" />
    /// event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnClick(object sender, RoutedEventArgs e) => SafeRaise.Raise<RoutedEventArgs>(this.Click, (object) this, e);

    /// <summary>
    /// Returns a
    /// <see cref="T:System.String" />
    /// that represents the current
    /// <see cref="T:System.Object" />
    /// .
    /// </summary>
    /// <returns></returns>
    public virtual string ToString() => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{{ToggleSwitch IsChecked={0}, Content={1}}}", new object[2]
    {
      (object) this.IsChecked,
      this.Content
    });
  }
}
