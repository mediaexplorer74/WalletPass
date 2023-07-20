// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ExpanderView
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a collection of items that can be expanded or collapsed.
  /// </summary>
  /// <QualityBand>Experimental</QualityBand>
  [TemplatePart(Name = "CollapsedToExpandedKeyFrame", Type = typeof (EasingDoubleKeyFrame))]
  [TemplatePart(Name = "ExpandedToCollapsedKeyFrame", Type = typeof (EasingDoubleKeyFrame))]
  [TemplateVisualState(Name = "Collapsed", GroupName = "ExpansionStates")]
  [TemplateVisualState(Name = "Expanded", GroupName = "ExpansionStates")]
  [TemplateVisualState(Name = "Expandable", GroupName = "ExpandabilityStates")]
  [TemplateVisualState(Name = "NonExpandable", GroupName = "ExpandabilityStates")]
  [TemplatePart(Name = "Presenter", Type = typeof (ItemsPresenter))]
  [TemplatePart(Name = "ExpanderPanel", Type = typeof (Grid))]
  [TemplatePart(Name = "ExpandedStateAnimation", Type = typeof (DoubleAnimation))]
  public class ExpanderView : HeaderedItemsControl
  {
    /// <summary>Expansion visual states.</summary>
    public const string ExpansionStates = "ExpansionStates";
    /// <summary>Expandability visual states.</summary>
    public const string ExpandabilityStates = "ExpandabilityStates";
    /// <summary>Collapsed visual state.</summary>
    public const string CollapsedState = "Collapsed";
    /// <summary>Expanded visual state.</summary>
    public const string ExpandedState = "Expanded";
    /// <summary>Expandable visual state.</summary>
    public const string ExpandableState = "Expandable";
    /// <summary>NonExpandable visual state.</summary>
    public const string NonExpandableState = "NonExpandable";
    /// <summary>Presenter template part name.</summary>
    private const string Presenter = "Presenter";
    /// <summary>Expander Panel template part name.</summary>
    private const string ExpanderPanel = "ExpanderPanel";
    /// <summary>Expanded State Animation template part name.</summary>
    private const string ExpandedStateAnimation = "ExpandedStateAnimation";
    /// <summary>Collapsed to Expanded Key Frame template part name.</summary>
    private const string CollapsedToExpandedKeyFrame = "CollapsedToExpandedKeyFrame";
    /// <summary>Expanded to Collapsed Key Frame template part name.</summary>
    private const string ExpandedToCollapsedKeyFrame = "ExpandedToCollapsedKeyFrame";
    /// <summary>
    /// Step between the keytimes of drop-down animations
    /// to create a feather effect.
    /// </summary>
    private const int KeyTimeStep = 35;
    /// <summary>Initial key time for drop-down animations.</summary>
    private const int InitialKeyTime = 225;
    /// <summary>Final key time for drop-down animations.</summary>
    private const int FinalKeyTime = 250;
    /// <summary>Presenter template part.</summary>
    private ItemsPresenter _presenter;
    /// <summary>Canvas template part</summary>
    private Canvas _itemsCanvas;
    /// <summary>Expander Panel template part.</summary>
    private Grid _expanderPanel;
    /// <summary>Expanded State Animation template part.</summary>
    private DoubleAnimation _expandedStateAnimation;
    /// <summary>Collapsed to Expanded Key Frame template part.</summary>
    private EasingDoubleKeyFrame _collapsedToExpandedFrame;
    /// <summary>Expanded to Collapsed Key Frame template part.</summary>
    private EasingDoubleKeyFrame _expandedToCollapsedFrame;
    /// <summary>Identifies the Expander dependency property.</summary>
    public static readonly DependencyProperty ExpanderProperty = DependencyProperty.Register(nameof (Expander), typeof (object), typeof (ExpanderView), new PropertyMetadata((object) null, new PropertyChangedCallback(ExpanderView.OnExpanderPropertyChanged)));
    /// <summary>Identifies the ExpanderTemplate dependency property.</summary>
    public static readonly DependencyProperty ExpanderTemplateProperty = DependencyProperty.Register(nameof (ExpanderTemplate), typeof (DataTemplate), typeof (ExpanderView), new PropertyMetadata((object) null, new PropertyChangedCallback(ExpanderView.OnExpanderTemplatePropertyChanged)));
    /// <summary>
    /// Identifies the NonExpandableHeader dependency property.
    /// </summary>
    public static readonly DependencyProperty NonExpandableHeaderProperty = DependencyProperty.Register(nameof (NonExpandableHeader), typeof (object), typeof (ExpanderView), new PropertyMetadata((object) null, new PropertyChangedCallback(ExpanderView.OnNonExpandableHeaderPropertyChanged)));
    /// <summary>
    /// Identifies the NonExpandableHeaderTemplate dependency property.
    /// </summary>
    public static readonly DependencyProperty NonExpandableHeaderTemplateProperty = DependencyProperty.Register(nameof (NonExpandableHeaderTemplate), typeof (DataTemplate), typeof (ExpanderView), new PropertyMetadata((object) null, new PropertyChangedCallback(ExpanderView.OnNonExpandableHeaderTemplatePropertyChanged)));
    /// <summary>Identifies the IsExpanded dependency property.</summary>
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof (IsExpanded), typeof (bool), typeof (ExpanderView), new PropertyMetadata((object) false, new PropertyChangedCallback(ExpanderView.OnIsExpandedPropertyChanged)));
    /// <summary>Identifies the HasItems dependency property.</summary>
    public static readonly DependencyProperty HasItemsProperty = DependencyProperty.Register(nameof (HasItems), typeof (bool), typeof (ExpanderView), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
    /// <summary>Identifies the NonExpandable dependency property.</summary>
    public static readonly DependencyProperty IsNonExpandableProperty = DependencyProperty.Register(nameof (IsNonExpandable), typeof (bool), typeof (ExpanderView), new PropertyMetadata((object) false, new PropertyChangedCallback(ExpanderView.OnIsNonExpandablePropertyChanged)));

    /// <summary>
    /// Occurs when the Expander View opens to display its content.
    /// </summary>
    public event RoutedEventHandler Expanded;

    /// <summary>
    /// Occurs when the Expander View closes to hide its content.
    /// </summary>
    public event RoutedEventHandler Collapsed;

    /// <summary>Gets or sets the expander object.</summary>
    public object Expander
    {
      get => ((DependencyObject) this).GetValue(ExpanderView.ExpanderProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.ExpanderProperty, value);
    }

    /// <summary>ExpanderProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnExpanderPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((ExpanderView) obj).OnExpanderChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// Gets or sets the data template that defines
    /// the expander.
    /// </summary>
    public DataTemplate ExpanderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(ExpanderView.ExpanderTemplateProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.ExpanderTemplateProperty, (object) value);
    }

    /// <summary>ExpanderTemplateProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnExpanderTemplatePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((ExpanderView) obj).OnExpanderTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
    }

    /// <summary>
    /// Gets or sets the header object that is displayed
    /// when the Expander View is non-expandable.
    /// </summary>
    public object NonExpandableHeader
    {
      get => ((DependencyObject) this).GetValue(ExpanderView.NonExpandableHeaderProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.NonExpandableHeaderProperty, value);
    }

    /// <summary>NonExpandableHeaderProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnNonExpandableHeaderPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((ExpanderView) obj).OnNonExpandableHeaderChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// Gets or sets the data template that defines
    /// the non-expandable header.
    /// </summary>
    public DataTemplate NonExpandableHeaderTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(ExpanderView.NonExpandableHeaderTemplateProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.NonExpandableHeaderTemplateProperty, (object) value);
    }

    /// <summary>NonExpandableHeaderTemplate changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnNonExpandableHeaderTemplatePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((ExpanderView) obj).OnNonExpandableHeaderTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
    }

    /// <summary>
    /// Gets or sets the flag that indicates whether the
    /// Expander View is expanded.
    /// </summary>
    public bool IsExpanded
    {
      get => (bool) ((DependencyObject) this).GetValue(ExpanderView.IsExpandedProperty);
      set
      {
        if (this.IsNonExpandable)
          throw new InvalidOperationException(Resources.InvalidExpanderViewOperation);
        ((DependencyObject) this).SetValue(ExpanderView.IsExpandedProperty, (object) value);
      }
    }

    /// <summary>IsExpandedProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsExpandedPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ExpanderView expanderView = (ExpanderView) obj;
      RoutedEventArgs e1 = new RoutedEventArgs();
      if ((bool) e.NewValue)
        expanderView.OnExpanded(e1);
      else
        expanderView.OnCollapsed(e1);
      expanderView.UpdateVisualState(true);
    }

    /// <summary>
    /// Gets or sets the flag that indicates whether the
    /// Expander View has items.
    /// </summary>
    public bool HasItems
    {
      get => (bool) ((DependencyObject) this).GetValue(ExpanderView.HasItemsProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.HasItemsProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the flag that indicates whether the
    /// Expander View is non-expandable.
    /// </summary>
    public bool IsNonExpandable
    {
      get => (bool) ((DependencyObject) this).GetValue(ExpanderView.IsNonExpandableProperty);
      set => ((DependencyObject) this).SetValue(ExpanderView.IsNonExpandableProperty, (object) value);
    }

    /// <summary>IsNonExpandableProperty changed handler.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnIsNonExpandablePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ExpanderView expanderView = (ExpanderView) obj;
      if ((bool) e.NewValue && expanderView.IsExpanded)
        expanderView.IsExpanded = false;
      expanderView.UpdateVisualState(true);
    }

    /// <summary>Gets the template parts and sets event handlers.</summary>
    public override void OnApplyTemplate()
    {
      if (this._expanderPanel != null)
        ((UIElement) this._expanderPanel).Tap -= new EventHandler<GestureEventArgs>(this.OnExpanderPanelTap);
      base.OnApplyTemplate();
      this._expanderPanel = ((Control) this).GetTemplateChild("ExpanderPanel") as Grid;
      this._expandedToCollapsedFrame = ((Control) this).GetTemplateChild("ExpandedToCollapsedKeyFrame") as EasingDoubleKeyFrame;
      this._collapsedToExpandedFrame = ((Control) this).GetTemplateChild("CollapsedToExpandedKeyFrame") as EasingDoubleKeyFrame;
      this._itemsCanvas = ((Control) this).GetTemplateChild("ItemsCanvas") as Canvas;
      if (((Control) this).GetTemplateChild("Expanded") is VisualState templateChild)
        this._expandedStateAnimation = ((PresentationFrameworkCollection<Timeline>) templateChild.Storyboard.Children)[0] as DoubleAnimation;
      this._presenter = ((Control) this).GetTemplateChild("Presenter") as ItemsPresenter;
      if (this._presenter != null)
        ((FrameworkElement) this._presenter).SizeChanged += new SizeChangedEventHandler(this.OnPresenterSizeChanged);
      if (this._expanderPanel != null)
        ((UIElement) this._expanderPanel).Tap += new EventHandler<GestureEventArgs>(this.OnExpanderPanelTap);
      this.UpdateVisualState(false);
    }

    /// <summary>Initializes a new instance of the ExpanderView class.</summary>
    public ExpanderView()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (ExpanderView);
      ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
    }

    /// <summary>
    /// Recalculates the size of the presenter to match its parent.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (this._presenter == null)
        return;
      UIElement parent = VisualTreeHelper.GetParent((DependencyObject) this._presenter) as UIElement;
      while (!(parent is ExpanderView))
        parent = VisualTreeHelper.GetParent((DependencyObject) parent) as UIElement;
      Point point = parent.TransformToVisual((UIElement) this._presenter).Transform(new Point(0.0, 0.0));
      ((FrameworkElement) this._presenter).Width = parent.RenderSize.Width + point.X;
    }

    /// <summary>
    /// Recalculates size of canvas based on the size change for the presenter.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    private void OnPresenterSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (this._itemsCanvas == null || this._presenter == null || !this.IsExpanded)
        return;
      ((FrameworkElement) this._itemsCanvas).Height = ((UIElement) this._presenter).DesiredSize.Height;
    }

    /// <summary>Updates the visual state.</summary>
    /// <param name="useTransitions">
    /// Indicates whether visual transitions should be used.
    /// </param>
    internal virtual void UpdateVisualState(bool useTransitions)
    {
      if (this._presenter != null)
      {
        Size desiredSize;
        if (this._expandedStateAnimation != null)
        {
          DoubleAnimation expandedStateAnimation = this._expandedStateAnimation;
          desiredSize = ((UIElement) this._presenter).DesiredSize;
          double? nullable = new double?(desiredSize.Height);
          expandedStateAnimation.To = nullable;
        }
        if (this._collapsedToExpandedFrame != null)
        {
          EasingDoubleKeyFrame collapsedToExpandedFrame = this._collapsedToExpandedFrame;
          desiredSize = ((UIElement) this._presenter).DesiredSize;
          double height = desiredSize.Height;
          ((DoubleKeyFrame) collapsedToExpandedFrame).Value = height;
        }
        if (this._expandedToCollapsedFrame != null)
        {
          EasingDoubleKeyFrame toCollapsedFrame = this._expandedToCollapsedFrame;
          desiredSize = ((UIElement) this._presenter).DesiredSize;
          double height = desiredSize.Height;
          ((DoubleKeyFrame) toCollapsedFrame).Value = height;
        }
      }
      string str;
      if (this.IsExpanded)
      {
        str = "Expanded";
        if (useTransitions)
          this.AnimateContainerDropDown();
      }
      else
        str = "Collapsed";
      VisualStateManager.GoToState((Control) this, str, useTransitions);
      VisualStateManager.GoToState((Control) this, !this.IsNonExpandable ? "Expandable" : "NonExpandable", useTransitions);
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
    /// Provides the feathered animation for items
    /// when the Expander View goes from collapsed to expanded.
    /// </summary>
    internal void AnimateContainerDropDown()
    {
      for (int index = 0; index < ((PresentationFrameworkCollection<object>) this.Items).Count && this.ItemContainerGenerator.ContainerFromIndex(index) is FrameworkElement frameworkElement; ++index)
      {
        Storyboard storyboard = new Storyboard();
        QuadraticEase quadraticEase = new QuadraticEase();
        ((EasingFunctionBase) quadraticEase).EasingMode = (EasingMode) 0;
        IEasingFunction ieasingFunction = (IEasingFunction) quadraticEase;
        int num1 = 225 + 35 * index;
        int num2 = 250 + 35 * index;
        TranslateTransform translateTransform = new TranslateTransform();
        ((UIElement) frameworkElement).RenderTransform = (Transform) translateTransform;
        DoubleAnimationUsingKeyFrames animationUsingKeyFrames1 = new DoubleAnimationUsingKeyFrames();
        EasingDoubleKeyFrame easingDoubleKeyFrame1 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame1.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame1).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds(0.0));
        ((DoubleKeyFrame) easingDoubleKeyFrame1).Value = -150.0;
        EasingDoubleKeyFrame easingDoubleKeyFrame2 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame2.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame2).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds((double) num1));
        ((DoubleKeyFrame) easingDoubleKeyFrame2).Value = 0.0;
        EasingDoubleKeyFrame easingDoubleKeyFrame3 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame3.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame3).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds((double) num2));
        ((DoubleKeyFrame) easingDoubleKeyFrame3).Value = 0.0;
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames1.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame1);
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames1.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame2);
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames1.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame3);
        Storyboard.SetTarget((Timeline) animationUsingKeyFrames1, (DependencyObject) translateTransform);
        Storyboard.SetTargetProperty((Timeline) animationUsingKeyFrames1, new PropertyPath((object) TranslateTransform.YProperty));
        ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) animationUsingKeyFrames1);
        DoubleAnimationUsingKeyFrames animationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
        EasingDoubleKeyFrame easingDoubleKeyFrame4 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame4.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame4).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds(0.0));
        ((DoubleKeyFrame) easingDoubleKeyFrame4).Value = 0.0;
        EasingDoubleKeyFrame easingDoubleKeyFrame5 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame5.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame5).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds((double) (num1 - 150)));
        ((DoubleKeyFrame) easingDoubleKeyFrame5).Value = 0.0;
        EasingDoubleKeyFrame easingDoubleKeyFrame6 = new EasingDoubleKeyFrame();
        easingDoubleKeyFrame6.EasingFunction = ieasingFunction;
        ((DoubleKeyFrame) easingDoubleKeyFrame6).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds((double) num2));
        ((DoubleKeyFrame) easingDoubleKeyFrame6).Value = 1.0;
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames2.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame4);
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames2.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame5);
        ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames2.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame6);
        Storyboard.SetTarget((Timeline) animationUsingKeyFrames2, (DependencyObject) frameworkElement);
        Storyboard.SetTargetProperty((Timeline) animationUsingKeyFrames2, new PropertyPath((object) UIElement.OpacityProperty));
        ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) animationUsingKeyFrames2);
        storyboard.Begin();
      }
    }

    /// <summary>Updates the HasItems property.</summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    {
      base.OnItemsChanged(e);
      this.HasItems = ((PresentationFrameworkCollection<object>) this.Items).Count > 0;
    }

    /// <summary>Toggles the IsExpanded property.</summary>
    /// <param name="sender">The Expander Panel that triggers the event.</param>
    /// <param name="e">The event information.</param>
    private void OnExpanderPanelTap(object sender, GestureEventArgs e)
    {
      if (this.IsNonExpandable)
        return;
      this.IsExpanded = !this.IsExpanded;
    }

    /// <summary>
    /// Called when the value of th Expander property changes.
    /// </summary>
    /// <param name="oldExpander">
    /// The old value of the Expander property.
    /// </param>
    /// <param name="newExpander">
    /// The new value of the Expander property.
    /// </param>
    protected virtual void OnExpanderChanged(object oldExpander, object newExpander)
    {
    }

    /// <summary>
    /// Called when the value of the ExpanderTemplate property changes.
    /// </summary>
    /// <param name="oldTemplate">
    /// The old value of the ExpanderTemplate property.
    /// </param>
    /// <param name="newTemplate">
    /// The new value of the ExpanderTemplate property.
    /// </param>
    protected virtual void OnExpanderTemplateChanged(
      DataTemplate oldTemplate,
      DataTemplate newTemplate)
    {
    }

    /// <summary>
    /// Called when the value of the NonExpandableHeader property changes.
    /// </summary>
    /// <param name="oldHeader">
    /// The old value of the NonExpandableHeader property.
    /// </param>
    /// <param name="newHeader">
    /// The new value of the NonExpandableHeader property.
    /// </param>
    protected virtual void OnNonExpandableHeaderChanged(object oldHeader, object newHeader)
    {
    }

    /// <summary>
    /// Called when the value of the NonExpandableHeaderTemplate
    /// property changes.
    /// </summary>
    /// <param name="oldTemplate">
    /// The old value of the NonExpandableHeaderTemplate property.
    /// </param>
    /// <param name="newTemplate">
    /// The new value of the NonExpandableHeaderTemplate property.
    /// </param>
    protected virtual void OnNonExpandableHeaderTemplateChanged(
      DataTemplate oldTemplate,
      DataTemplate newTemplate)
    {
    }

    /// <summary>
    /// Raises an Expanded event when the IsExpanded property
    /// changes from false to true.
    /// </summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnExpanded(RoutedEventArgs e) => this.RaiseEvent(this.Expanded, e);

    /// <summary>
    /// Raises a Collapsed event when the IsExpanded property
    /// changes from true to false.
    /// </summary>
    /// <param name="e">The event information.</param>
    protected virtual void OnCollapsed(RoutedEventArgs e) => this.RaiseEvent(this.Collapsed, e);
  }
}
