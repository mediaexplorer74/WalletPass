// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.ToggleSwitchButton
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Represents a switch that can be toggled between two states.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
  [TemplateVisualState(Name = "Unchecked", GroupName = "CheckStates")]
  [TemplatePart(Name = "SwitchRoot", Type = typeof (Grid))]
  [TemplatePart(Name = "SwitchBackground", Type = typeof (UIElement))]
  [TemplatePart(Name = "SwitchThumb", Type = typeof (FrameworkElement))]
  [TemplateVisualState(Name = "Checked", GroupName = "CheckStates")]
  [TemplateVisualState(Name = "Dragging", GroupName = "CheckStates")]
  [TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
  [TemplatePart(Name = "SwitchTrack", Type = typeof (Grid))]
  public class ToggleSwitchButton : ToggleButton
  {
    /// <summary>Common visual states.</summary>
    private const string CommonStates = "CommonStates";
    /// <summary>Normal visual state.</summary>
    private const string NormalState = "Normal";
    /// <summary>Disabled visual state.</summary>
    private const string DisabledState = "Disabled";
    /// <summary>Check visual states.</summary>
    private const string CheckStates = "CheckStates";
    /// <summary>Checked visual state.</summary>
    private const string CheckedState = "Checked";
    /// <summary>Dragging visual state.</summary>
    private const string DraggingState = "Dragging";
    /// <summary>Unchecked visual state.</summary>
    private const string UncheckedState = "Unchecked";
    /// <summary>Switch root template part name.</summary>
    private const string SwitchRootPart = "SwitchRoot";
    /// <summary>Switch background template part name.</summary>
    private const string SwitchBackgroundPart = "SwitchBackground";
    /// <summary>Switch track template part name.</summary>
    private const string SwitchTrackPart = "SwitchTrack";
    /// <summary>Switch thumb template part name.</summary>
    private const string SwitchThumbPart = "SwitchThumb";
    /// <summary>The minimum translation.</summary>
    private const double _uncheckedTranslation = 0.0;
    /// <summary>Identifies the SwitchForeground dependency property.</summary>
    public static readonly DependencyProperty SwitchForegroundProperty = DependencyProperty.Register(nameof (SwitchForeground), typeof (Brush), typeof (ToggleSwitchButton), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>The background TranslateTransform.</summary>
    private TranslateTransform _backgroundTranslation;
    /// <summary>The thumb TranslateTransform.</summary>
    private TranslateTransform _thumbTranslation;
    /// <summary>The root template part.</summary>
    private Grid _root;
    /// <summary>The track template part.</summary>
    private Grid _track;
    /// <summary>The thumb template part.</summary>
    private FrameworkElement _thumb;
    /// <summary>The maximum translation.</summary>
    private double _checkedTranslation;
    /// <summary>The drag translation.</summary>
    private double _dragTranslation;
    /// <summary>Whether the translation ever changed during the drag.</summary>
    private bool _wasDragged;
    /// <summary>Whether the dragging state is current.</summary>
    private bool _isDragging;

    /// <summary>Gets or sets the switch foreground.</summary>
    public Brush SwitchForeground
    {
      get => (Brush) ((DependencyObject) this).GetValue(ToggleSwitchButton.SwitchForegroundProperty);
      set => ((DependencyObject) this).SetValue(ToggleSwitchButton.SwitchForegroundProperty, (object) value);
    }

    /// <summary>Initializes a new instance of the ToggleSwitch class.</summary>
    public ToggleSwitchButton() => ((Control) this).DefaultStyleKey = (object) typeof (ToggleSwitchButton);

    /// <summary>Gets and sets the thumb and background translation.</summary>
    /// <returns>The translation.</returns>
    private double Translation
    {
      get => this._backgroundTranslation == null ? this._thumbTranslation.X : this._backgroundTranslation.X;
      set
      {
        if (this._backgroundTranslation != null)
          this._backgroundTranslation.X = value;
        if (this._thumbTranslation == null)
          return;
        this._thumbTranslation.X = value;
      }
    }

    /// <summary>Change the visual state.</summary>
    /// <param name="useTransitions">Indicates whether to use animation transitions.</param>
    private void ChangeVisualState(bool useTransitions)
    {
      if (((Control) this).IsEnabled)
        VisualStateManager.GoToState((Control) this, "Normal", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Disabled", useTransitions);
      if (this._isDragging)
        VisualStateManager.GoToState((Control) this, "Dragging", useTransitions);
      else if (((int) this.IsChecked ?? 0) != 0)
        VisualStateManager.GoToState((Control) this, "Checked", useTransitions);
      else
        VisualStateManager.GoToState((Control) this, "Unchecked", useTransitions);
    }

    /// <summary>
    /// Called by the OnClick method to implement toggle behavior.
    /// </summary>
    protected virtual void OnToggle()
    {
      this.IsChecked = new bool?(((int) this.IsChecked ?? 0) == 0);
      this.ChangeVisualState(true);
    }

    /// <summary>
    /// Gets all the template parts and initializes the corresponding state.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      if (this._track != null)
        ((FrameworkElement) this._track).SizeChanged -= new SizeChangedEventHandler(this.OnSizeChanged);
      if (this._thumb != null)
        this._thumb.SizeChanged -= new SizeChangedEventHandler(this.OnSizeChanged);
      if (this._root != null)
      {
        ((UIElement) this._root).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
        ((UIElement) this._root).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
        ((UIElement) this._root).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
      }
      base.OnApplyTemplate();
      this._root = ((Control) this).GetTemplateChild("SwitchRoot") as Grid;
      this._backgroundTranslation = !(((Control) this).GetTemplateChild("SwitchBackground") is UIElement templateChild) ? (TranslateTransform) null : templateChild.RenderTransform as TranslateTransform;
      this._track = ((Control) this).GetTemplateChild("SwitchTrack") as Grid;
      this._thumb = (FrameworkElement) (((Control) this).GetTemplateChild("SwitchThumb") as Border);
      this._thumbTranslation = this._thumb == null ? (TranslateTransform) null : ((UIElement) this._thumb).RenderTransform as TranslateTransform;
      if (this._root != null && this._track != null && this._thumb != null && (this._backgroundTranslation != null || this._thumbTranslation != null))
      {
        ((UIElement) this._root).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
        ((UIElement) this._root).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
        ((UIElement) this._root).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
        ((FrameworkElement) this._track).SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        this._thumb.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
      }
      this.ChangeVisualState(false);
    }

    /// <summary>Handles started drags on the root.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      e.Handled = true;
      this._isDragging = true;
      this._dragTranslation = this.Translation;
      this.ChangeVisualState(true);
      this.Translation = this._dragTranslation;
    }

    /// <summary>Handles drags on the root.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      e.Handled = true;
      double x = e.DeltaManipulation.Translation.X;
      if ((Math.Abs(x) >= Math.Abs(e.DeltaManipulation.Translation.Y) ? (Orientation) 1 : (Orientation) 0) != 1 || x == 0.0)
        return;
      this._wasDragged = true;
      this._dragTranslation += x;
      this.Translation = Math.Max(0.0, Math.Min(this._checkedTranslation, this._dragTranslation));
    }

    private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      e.Handled = true;
      this._isDragging = false;
      bool flag = false;
      if (this._wasDragged)
      {
        if (this.Translation != (((int) this.IsChecked ?? 0) != 0 ? this._checkedTranslation : 0.0))
          flag = true;
      }
      else
        flag = true;
      if (flag)
        ((ButtonBase) this).OnClick();
      this._wasDragged = false;
    }

    /// <summary>Handles the mouse leave event.</summary>
    /// <param name="e">The event arguments.</param>
    protected virtual void OnMouseLeave(MouseEventArgs e)
    {
    }

    /// <summary>
    /// Handles changed sizes for the track and the thumb.
    /// Sets the clip of the track and computes the indeterminate and checked translations.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      ((UIElement) this._track).Clip = (Geometry) new RectangleGeometry()
      {
        Rect = new Rect(0.0, 0.0, ((FrameworkElement) this._track).ActualWidth, ((FrameworkElement) this._track).ActualHeight)
      };
      this._checkedTranslation = ((FrameworkElement) this._track).ActualWidth - this._thumb.ActualWidth - this._thumb.Margin.Left - this._thumb.Margin.Right;
    }
  }
}
