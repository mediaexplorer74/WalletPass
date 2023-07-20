// WalletPass.PullDownToRefreshPanel


using System;
using System.Collections.Generic;
using System.Windows;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;

namespace WalletPass
{
  [TemplateVisualState(Name = "PullingDown", GroupName = "ActivityStates")]
  [TemplateVisualState(Name = "ReadyToRelease", GroupName = "ActivityStates")]
  [TemplateVisualState(Name = "Refreshing", GroupName = "ActivityStates")]
  [TemplateVisualState(Name = "Inactive", GroupName = "ActivityStates")]
  public class PullDownToRefreshPanel : Control
  {
    private const string ActivityVisualStateGroup = "ActivityStates";
    private const string InactiveVisualState = "Inactive";
    private const string PullingDownVisualState = "PullingDown";
    private const string ReadyToReleaseVisualState = "ReadyToRelease";
    private const string RefreshingVisualState = "Refreshing";
    private const string NoVerticalCompression = "NoVerticalCompression";
    private const string CompressionTop = "CompressionTop";
    private string activityState;
    private Point currentPosition;
    private Point initialPoint;
    private bool isCompressed;
    private bool isMeasuring;
    private bool isReadyToRefresh;
    private ScrollViewer targetScrollViewer;
    public static readonly DependencyProperty IsRefreshingProperty 
            = DependencyProperty.Register(nameof (IsRefreshing), 
                typeof (bool), typeof (PullDownToRefreshPanel), 
                new PropertyMetadata((object) false, (PropertyChangedCallback)
                    ((d, e) => ((PullDownToRefreshPanel) d).OnIsRefreshingChanged(e))));

    public static readonly DependencyProperty PullThresholdProperty
            = DependencyProperty.Register(nameof (PullThreshold), 
                typeof (double), typeof (PullDownToRefreshPanel),
                new PropertyMetadata((object) 100.0));

    public static readonly DependencyProperty PullDistanceProperty 
            = DependencyProperty.Register(nameof (PullDistance), 
                typeof (double), typeof (PullDownToRefreshPanel), 
                new PropertyMetadata((object) 0.0));

    public static readonly DependencyProperty PullFractionProperty 
            = DependencyProperty.Register(nameof (PullFraction),
                typeof (double), typeof (PullDownToRefreshPanel), 
                new PropertyMetadata((object) 0.0));

    public static readonly DependencyProperty PullingDownTemplateProperty 
            = DependencyProperty.Register(nameof (PullingDownTemplate), 
                typeof (DataTemplate), typeof (PullDownToRefreshPanel), 
                (PropertyMetadata) null);

    public static readonly DependencyProperty ReadyToReleaseTemplateProperty 
            = DependencyProperty.Register(nameof (ReadyToReleaseTemplate), 
                typeof (DataTemplate), typeof (PullDownToRefreshPanel),
                (PropertyMetadata) null);

    public static readonly DependencyProperty RefreshingTemplateProperty 
            = DependencyProperty.Register(nameof (RefreshingTemplate),
                typeof (DataTemplate), typeof (PullDownToRefreshPanel),
                (PropertyMetadata) null);

    public PullDownToRefreshPanel()
    {
      this.DefaultStyleKey = (object) typeof (PullDownToRefreshPanel);

      //RnD
      //((FrameworkElement) this).Loaded += new RoutedEventHandler(
      //    this.PullDownToRefreshPanelLoaded);
    }

    public event EventHandler RefreshRequested;

    public bool IsRefreshing
    {
      get => (bool) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.IsRefreshingProperty);

      set => ((DependencyObject) this).SetValue(
          PullDownToRefreshPanel.IsRefreshingProperty, (object) value);
    }

    protected void OnIsRefreshingChanged(DependencyPropertyChangedEventArgs e)
    {
        VisualStateManager.GoToState((Control)this, (bool)e.NewValue
            ? "Refreshing"
            : "Inactive", false);
    }

    public double PullThreshold
    {
      get => (double) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.PullThresholdProperty);

      set => ((DependencyObject) this).SetValue
                (PullDownToRefreshPanel.PullThresholdProperty, (object) value);
    }

    public double PullDistance
    {
      get => (double) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.PullDistanceProperty);

      private set => ((DependencyObject) this).SetValue
                (PullDownToRefreshPanel.PullDistanceProperty, (object) value);
    }

    public double PullFraction
    {
      get => (double) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.PullFractionProperty);

      private set => ((DependencyObject) this).SetValue(
          PullDownToRefreshPanel.PullFractionProperty, (object) value);
    }

    public DataTemplate PullingDownTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.PullingDownTemplateProperty);

      set => ((DependencyObject) this).SetValue(
          PullDownToRefreshPanel.PullingDownTemplateProperty, (object) value);
    }

    public DataTemplate ReadyToReleaseTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.ReadyToReleaseTemplateProperty);

      set => ((DependencyObject) this).SetValue(
          PullDownToRefreshPanel.ReadyToReleaseTemplateProperty, (object) value);
    }

    public DataTemplate RefreshingTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(
          PullDownToRefreshPanel.RefreshingTemplateProperty);

      set => ((DependencyObject) this).SetValue(
          PullDownToRefreshPanel.RefreshingTemplateProperty, (object) value);
    }

    private void PullDownToRefreshPanelLoaded(object sender, EventArgs e)
    {
            //RnD
            /*
      ((FrameworkElement) this).Loaded -= 
                new RoutedEventHandler(this.PullDownToRefreshPanelLoaded);
      ((FrameworkElement) this).Unloaded += 
                new RoutedEventHandler(this.PullDownToRefreshPanelUnloaded);
      Touch.FrameReported += new TouchFrameEventHandler(this.TouchFrameReported);

      this.targetScrollViewer = 
                PullDownToRefreshPanel.FindVisualElement<ScrollViewer>(
                    ((FrameworkElement) this).Parent);

      ((VisualStateGroup) VisualStateManager.GetVisualStateGroups
                (VisualTreeHelper.GetChild((DependencyObject) this.targetScrollViewer, 0)
                as FrameworkElement)[1]).CurrentStateChanging += 
                new EventHandler<VisualStateChangedEventArgs>(
                    this.OnVisualStateGroupOnCurrentStateChanging);
            */
    }

    private void PullDownToRefreshPanelUnloaded(object sender, RoutedEventArgs e)
    {
      ((FrameworkElement) this).Unloaded -= 
                new RoutedEventHandler(this.PullDownToRefreshPanelUnloaded);
      //Touch.FrameReported -=
      //          new TouchFrameEventHandler(this.TouchFrameReported);
      //((VisualStateGroup) VisualStateManager.GetVisualStateGroups(
      //    VisualTreeHelper.GetChild((DependencyObject) this.targetScrollViewer, 0) 
      //    as FrameworkElement)[1]).CurrentStateChanging -= 
      //    new EventHandler<VisualStateChangedEventArgs>(
      //        this.OnVisualStateGroupOnCurrentStateChanging);
    }

    private void OnVisualStateGroupOnCurrentStateChanging(
      object sender,
      VisualStateChangedEventArgs e)
    {
      switch (e.NewState.Name)
      {
        case "CompressionTop":
          this.isCompressed = true;
          this.StartMeasuring();
          break;
        case "NoVerticalCompression":
          this.isCompressed = false;
          this.StopMeasuring();
          break;
      }
    }

    private void TouchFrameReported(object sender, TouchFrameEventArgs e)
    {
      TouchPoint primaryTouchPoint = e.GetPrimaryTouchPoint((UIElement) this);
      switch (primaryTouchPoint.Action - 1)
      {
        case 0:
          this.initialPoint = primaryTouchPoint.Position;
          if (!this.isCompressed)
            break;
          this.StartMeasuring();
          break;
        case 1:
          if (this.isMeasuring)
          {
            if (Math.Abs(this.currentPosition.Y - primaryTouchPoint.Position.Y) <= 0.0001)
              break;
            this.currentPosition = primaryTouchPoint.Position;
            this.UpdateControl();
            break;
          }
          this.initialPoint = primaryTouchPoint.Position;
          break;
        case 2:
          this.StopMeasuring();
          break;
      }
    }

    private void UpdateControl()
    {
      double num = this.currentPosition.Y - this.initialPoint.Y;
      if (num > this.PullThreshold)
      {
        if (this.isReadyToRefresh && !(this.activityState != "ReadyToRelease"))
          return;
        this.PullDistance = this.PullThreshold;
        this.PullFraction = 1.0;
        this.activityState = "ReadyToRelease";
        VisualStateManager.GoToState((Control) this, this.activityState, false);
        this.isReadyToRefresh = true;
      }
      else if (num > 0.0)
      {
        this.PullDistance = num;
        double pullThreshold = this.PullThreshold;
        this.PullFraction = Math.Abs(pullThreshold) < 0.0001 ? 1.0 
                    : Math.Min(1.0, num / pullThreshold);
        this.activityState = "PullingDown";
        VisualStateManager.GoToState((Control) this, this.activityState, false);
        this.isReadyToRefresh = false;
      }
      else
      {
        this.PullDistance = 0.0;
        this.PullFraction = 0.0;
        this.activityState = "Inactive";
        VisualStateManager.GoToState((Control) this, this.activityState, false);
        this.isReadyToRefresh = false;
      }
    }

    private void StartMeasuring() => this.isMeasuring = true;

    private void StopMeasuring()
    {
      if (!this.isMeasuring)
        return;
      this.isMeasuring = false;
      VisualStateManager.GoToState((Control) this, "Inactive", false);
      this.PullDistance = 0.0;
      this.PullFraction = 0.0;
      if (!this.isReadyToRefresh)
        return;
      EventHandler refreshRequested = this.RefreshRequested;
      if (refreshRequested != null)
        refreshRequested((object) this, EventArgs.Empty);
      this.isReadyToRefresh = false;
    }

    private static T FindVisualElement<T>(DependencyObject container) where T 
        : DependencyObject
    {
      Queue<DependencyObject> dependencyObjectQueue = new Queue<DependencyObject>();
      dependencyObjectQueue.Enqueue(container);
      while (dependencyObjectQueue.Count > 0)
      {
        DependencyObject dependencyObject = dependencyObjectQueue.Dequeue();
        if (dependencyObject is T visualElement && (object) visualElement != container)
          return visualElement;
        int childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
        for (int index = 0; index < childrenCount; ++index)
          dependencyObjectQueue.Enqueue(VisualTreeHelper.GetChild(dependencyObject, index));
      }
      return default (T);
    }
  }
}
