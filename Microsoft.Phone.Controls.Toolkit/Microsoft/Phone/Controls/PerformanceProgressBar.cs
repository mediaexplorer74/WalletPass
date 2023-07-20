// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PerformanceProgressBar
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A progress bar implementation for a smoother appearance of the
  /// indeterminate states, with the added behavior that after the behavior
  /// is no longer needed, it smoothly fades out the dots for a less jarring
  /// experience. No exposed Value property.
  /// </summary>
  /// <remarks>
  /// Important - this control is not intended for regular progress
  /// bar use, but only indeterminate. As a result, only an IsIndeterminate
  /// set of visual states are defined in the nested progress bar template.
  /// Use the standard ProgressBar control in the platform for determinate
  /// scenarios as there is no performance benefit in determinate mode.
  /// </remarks>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "Normal")]
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "Hidden")]
  [TemplatePart(Name = "EmbeddedProgressBar", Type = typeof (ProgressBar))]
  public class PerformanceProgressBar : Control
  {
    private const string EmbeddedProgressBarName = "EmbeddedProgressBar";
    private const string VisualStateGroupName = "VisibilityStates";
    private const string NormalState = "Normal";
    private const string HiddenState = "Hidden";
    private ProgressBar _progressBar;
    private static readonly PropertyPath ActualIsIndeterminatePath = new PropertyPath(nameof (ActualIsIndeterminate), new object[0]);
    /// <summary>
    /// The visual state group reference used to wait until the hidden state
    /// has fully transitioned to flip the underlying progress bar's
    /// indeterminate value to False.
    /// </summary>
    private VisualStateGroup _visualStateGroup;
    /// <summary>
    /// Gets or sets a value indicating the cached IsIndeterminate.
    /// </summary>
    private bool _cachedIsIndeterminate;
    /// <summary>
    /// Gets or sets a value indicating the cached IsIndeterminate binding expression.
    /// </summary>
    private BindingExpression _cachedIsIndeterminateBindingExpression;
    /// <summary>
    /// Identifies the ActualIsIndeterminate dependency property.
    /// </summary>
    public static readonly DependencyProperty ActualIsIndeterminateProperty = DependencyProperty.Register(nameof (ActualIsIndeterminate), typeof (bool), typeof (PerformanceProgressBar), new PropertyMetadata((object) false));
    /// <summary>Identifies the IsIndeterminate dependency property.</summary>
    public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(nameof (IsIndeterminate), typeof (bool), typeof (PerformanceProgressBar), new PropertyMetadata((object) false, new PropertyChangedCallback(PerformanceProgressBar.OnIsIndeterminatePropertyChanged)));

    /// <summary>
    /// Gets or sets the value indicating whether the actual indeterminate
    /// property should be reflecting a particular value.
    /// </summary>
    public bool ActualIsIndeterminate
    {
      get => (bool) ((DependencyObject) this).GetValue(PerformanceProgressBar.ActualIsIndeterminateProperty);
      set => ((DependencyObject) this).SetValue(PerformanceProgressBar.ActualIsIndeterminateProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control is in the
    /// indeterminate state.
    /// </summary>
    public bool IsIndeterminate
    {
      get => (bool) ((DependencyObject) this).GetValue(PerformanceProgressBar.IsIndeterminateProperty);
      set => ((DependencyObject) this).SetValue(PerformanceProgressBar.IsIndeterminateProperty, (object) value);
    }

    /// <summary>IsIndeterminateProperty property changed handler.</summary>
    /// <param name="d">PerformanceProgressBar that changed its IsIndeterminate.</param>
    /// <param name="e">Event arguments.</param>
    private static void OnIsIndeterminatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is PerformanceProgressBar performanceProgressBar))
        return;
      performanceProgressBar.OnIsIndeterminateChanged((bool) e.NewValue);
    }

    /// <summary>
    /// Initializes a new instance of the PerformanceProgressBar type.
    /// </summary>
    public PerformanceProgressBar()
    {
      this.DefaultStyleKey = (object) typeof (PerformanceProgressBar);
      ((FrameworkElement) this).Unloaded += new RoutedEventHandler(this.OnUnloaded);
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.OnLoaded);
    }

    /// <summary>
    /// Applies the template and extracts both a visual state and a template
    /// part.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      if (this._visualStateGroup != null)
        this._visualStateGroup.CurrentStateChanged -= new EventHandler<VisualStateChangedEventArgs>(this.OnVisualStateChanged);
      ((FrameworkElement) this).OnApplyTemplate();
      this._visualStateGroup = VisualStates.TryGetVisualStateGroup((DependencyObject) this, "VisibilityStates");
      if (this._visualStateGroup != null)
        this._visualStateGroup.CurrentStateChanged += new EventHandler<VisualStateChangedEventArgs>(this.OnVisualStateChanged);
      this._progressBar = this.GetTemplateChild("EmbeddedProgressBar") as ProgressBar;
      this._cachedIsIndeterminateBindingExpression = ((FrameworkElement) this).GetBindingExpression(PerformanceProgressBar.IsIndeterminateProperty);
      this.UpdateVisualStates(false);
    }

    private void OnVisualStateChanged(object sender, VisualStateChangedEventArgs e)
    {
      if (!this.ActualIsIndeterminate || e == null || e.NewState == null || !(e.NewState.Name == "Hidden") || this.IsIndeterminate)
        return;
      this.ActualIsIndeterminate = false;
    }

    private void OnIsIndeterminateChanged(bool newValue)
    {
      if (newValue)
      {
        this.ActualIsIndeterminate = true;
        this._cachedIsIndeterminate = true;
      }
      else if (this.ActualIsIndeterminate && this._visualStateGroup == null)
      {
        this.ActualIsIndeterminate = false;
        this._cachedIsIndeterminate = false;
      }
      this.UpdateVisualStates(true);
    }

    private void UpdateVisualStates(bool useTransitions) => VisualStateManager.GoToState((Control) this, this.IsIndeterminate ? "Normal" : "Hidden", useTransitions);

    private void OnUnloaded(object sender, RoutedEventArgs ea)
    {
      if (this._progressBar == null)
        return;
      this._cachedIsIndeterminate = this.IsIndeterminate;
      this._cachedIsIndeterminateBindingExpression = ((FrameworkElement) this).GetBindingExpression(PerformanceProgressBar.IsIndeterminateProperty);
      this._progressBar.IsIndeterminate = false;
    }

    private void OnLoaded(object sender, RoutedEventArgs ea)
    {
      if (this._progressBar == null)
        return;
      if (this._cachedIsIndeterminateBindingExpression != null)
        ((FrameworkElement) this).SetBinding(PerformanceProgressBar.IsIndeterminateProperty, this._cachedIsIndeterminateBindingExpression.ParentBinding);
      else
        this.IsIndeterminate = this._cachedIsIndeterminate;
      ((FrameworkElement) this._progressBar).SetBinding(ProgressBar.IsIndeterminateProperty, new Binding()
      {
        Source = (object) this,
        Path = PerformanceProgressBar.ActualIsIndeterminatePath
      });
    }

    private static T FindFirstChildOfType<T>(DependencyObject root) where T : class
    {
      Queue<DependencyObject> dependencyObjectQueue = new Queue<DependencyObject>();
      dependencyObjectQueue.Enqueue(root);
      while (0 < dependencyObjectQueue.Count)
      {
        DependencyObject dependencyObject = dependencyObjectQueue.Dequeue();
        for (int index = VisualTreeHelper.GetChildrenCount(dependencyObject) - 1; 0 <= index; --index)
        {
          DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, index);
          T firstChildOfType = child as T;
          if (null != (object) firstChildOfType)
            return firstChildOfType;
          dependencyObjectQueue.Enqueue(child);
        }
      }
      return default (T);
    }
  }
}
