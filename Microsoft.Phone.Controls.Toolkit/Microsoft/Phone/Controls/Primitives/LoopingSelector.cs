// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.LoopingSelector
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// An infinitely scrolling, UI- and data-virtualizing selection control.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplatePart(Name = "CenteringTransform", Type = typeof (TranslateTransform))]
  [TemplatePart(Name = "PanningTransform", Type = typeof (TranslateTransform))]
  [TemplatePart(Name = "ItemsPanel", Type = typeof (Panel))]
  public class LoopingSelector : Control
  {
    private const string ItemsPanelName = "ItemsPanel";
    private const string CenteringTransformName = "CenteringTransform";
    private const string PanningTransformName = "PanningTransform";
    private const double DragSensitivity = 12.0;
    private static readonly Duration _selectDuration = new Duration(TimeSpan.FromMilliseconds(500.0));
    private readonly IEasingFunction _selectEase;
    private static readonly Duration _panDuration = new Duration(TimeSpan.FromMilliseconds(100.0));
    private readonly IEasingFunction _panEase;
    private DoubleAnimation _panelAnimation;
    private Storyboard _panelStoryboard;
    private Panel _itemsPanel;
    private TranslateTransform _panningTransform;
    private TranslateTransform _centeringTransform;
    private bool _isSelecting;
    private LoopingSelectorItem _selectedItem;
    private Queue<LoopingSelectorItem> _temporaryItemsPool;
    private double _minimumPanelScroll;
    private double _maximumPanelScroll;
    private int _additionalItemsCount;
    private bool _isAnimating;
    private double _dragTarget;
    private bool _isAllowedToDragVertically;
    private bool _isDragging;
    private bool _changeStateAfterAnimation;
    private LoopingSelector.State _state;
    /// <summary>The DataSource DependencyProperty</summary>
    public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register(nameof (DataSource), typeof (ILoopingSelectorDataSource), typeof (LoopingSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(LoopingSelector.OnDataSourceChanged)));
    private Brush _borderBrushItem;
    private Brush _foregroundItem;
    private Brush _backgroundItem;
    private Brush _foregroundNotSelectedItem;
    private Brush _borderBrushNotSelectedItem;
    /// <summary>The ItemTemplate DependencyProperty</summary>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(nameof (ItemTemplate), typeof (DataTemplate), typeof (LoopingSelector), new PropertyMetadata((object) null, new PropertyChangedCallback(LoopingSelector.OnItemTemplateChanged)));
    /// <summary>The IsExpanded DependencyProperty.</summary>
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof (IsExpanded), typeof (bool), typeof (LoopingSelector), new PropertyMetadata((object) false, new PropertyChangedCallback(LoopingSelector.OnIsExpandedChanged)));

    /// <summary>
    /// The data source that the this control is the view for.
    /// </summary>
    public ILoopingSelectorDataSource DataSource
    {
      get => (ILoopingSelectorDataSource) ((DependencyObject) this).GetValue(LoopingSelector.DataSourceProperty);
      set => ((DependencyObject) this).SetValue(LoopingSelector.DataSourceProperty, (object) value);
    }

    private void OnDataSourceSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (!this.IsReady || this._isSelecting || e.AddedItems.Count != 1)
        return;
      object addedItem = e.AddedItems[0];
      foreach (LoopingSelectorItem child in (PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)
      {
        if (((FrameworkElement) child).DataContext == addedItem)
        {
          this.SelectAndSnapTo(child);
          return;
        }
      }
      this.UpdateData();
    }

    private static void OnDataSourceChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      LoopingSelector loopingSelector = (LoopingSelector) obj;
      if (e.OldValue != null)
        ((ILoopingSelectorDataSource) e.OldValue).SelectionChanged -= new EventHandler<SelectionChangedEventArgs>(loopingSelector.OnDataSourceSelectionChanged);
      if (e.NewValue != null)
        ((ILoopingSelectorDataSource) e.NewValue).SelectionChanged += new EventHandler<SelectionChangedEventArgs>(loopingSelector.OnDataSourceSelectionChanged);
      loopingSelector.UpdateData();
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush BorderBrushItem
    {
      get => this._borderBrushItem;
      set => this._borderBrushItem = value;
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush ForegroundItem
    {
      get => this._foregroundItem;
      set => this._foregroundItem = value;
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush ForegroundNotSelectedItem
    {
      get => this._foregroundNotSelectedItem;
      set => this._foregroundNotSelectedItem = value;
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush BackgroundItem
    {
      get => this._backgroundItem;
      set => this._backgroundItem = value;
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush BorderBrushNotSelectedItem
    {
      get => this._borderBrushNotSelectedItem;
      set => this._borderBrushNotSelectedItem = value;
    }

    /// <summary>The ItemTemplate property</summary>
    public DataTemplate ItemTemplate
    {
      get => (DataTemplate) ((DependencyObject) this).GetValue(LoopingSelector.ItemTemplateProperty);
      set => ((DependencyObject) this).SetValue(LoopingSelector.ItemTemplateProperty, (object) value);
    }

    private static void OnItemTemplateChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      ((LoopingSelector) obj).UpdateItemTemplates();
    }

    /// <summary>The size of the items, excluding the ItemMargin.</summary>
    public Size ItemSize { get; set; }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Thickness ItemMargin { get; set; }

    /// <summary>Creates a new LoopingSelector.</summary>
    public LoopingSelector()
    {
      ExponentialEase exponentialEase = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase).EasingMode = (EasingMode) 2;
      this._selectEase = (IEasingFunction) exponentialEase;
      this._panEase = (IEasingFunction) new ExponentialEase();
      this._minimumPanelScroll = -3.4028234663852886E+38;
      this._maximumPanelScroll = 3.4028234663852886E+38;
      this._additionalItemsCount = 0;
      this._isAllowedToDragVertically = true;
      // ISSUE: explicit constructor call
      base.\u002Ector();
      this.DefaultStyleKey = (object) typeof (LoopingSelector);
      this.CreateEventHandlers();
      this._borderBrushItem = (Brush) new SolidColorBrush(Colors.Transparent);
      this._foregroundItem = (Brush) new SolidColorBrush(Colors.White);
      this._backgroundItem = (Brush) new SolidColorBrush((Color) Application.Current.Resources[(object) "PhoneAccentColor"]);
      this._borderBrushNotSelectedItem = (Brush) new SolidColorBrush((Color) Application.Current.Resources[(object) "PhoneSubtleColor"]);
      this._foregroundNotSelectedItem = (Brush) new SolidColorBrush((Color) Application.Current.Resources[(object) "PhoneSubtleColor"]);
    }

    /// <summary>
    /// The IsExpanded property controls the expanded state of this control.
    /// </summary>
    public bool IsExpanded
    {
      get => (bool) ((DependencyObject) this).GetValue(LoopingSelector.IsExpandedProperty);
      set => ((DependencyObject) this).SetValue(LoopingSelector.IsExpandedProperty, (object) value);
    }

    /// <summary>
    /// The IsExpandedChanged event will be raised whenever the IsExpanded state changes.
    /// </summary>
    public event DependencyPropertyChangedEventHandler IsExpandedChanged;

    private static void OnIsExpandedChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      LoopingSelector loopingSelector = (LoopingSelector) sender;
      if (loopingSelector.IsExpanded)
        loopingSelector.Balance();
      else
        loopingSelector.SelectAndSnapToClosest();
      loopingSelector.UpdateItemState();
      if (loopingSelector._state == LoopingSelector.State.Normal || loopingSelector._state == LoopingSelector.State.Expanded)
        loopingSelector._state = loopingSelector.IsExpanded ? LoopingSelector.State.Expanded : LoopingSelector.State.Normal;
      DependencyPropertyChangedEventHandler isExpandedChanged = loopingSelector.IsExpandedChanged;
      if (isExpandedChanged == null)
        return;
      isExpandedChanged((object) loopingSelector, e);
    }

    /// <summary>
    /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:FrameworkElement.ApplyTemplate()" />.
    /// In simplest terms, this means the method is called just before a UI element displays in an application.
    /// For more information, see Remarks.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this._itemsPanel = (Panel) ((object) (this.GetTemplateChild("ItemsPanel") as Panel) ?? (object) new Canvas());
      if (!(this.GetTemplateChild("CenteringTransform") is TranslateTransform translateTransform1))
        translateTransform1 = new TranslateTransform();
      this._centeringTransform = translateTransform1;
      if (!(this.GetTemplateChild("PanningTransform") is TranslateTransform translateTransform2))
        translateTransform2 = new TranslateTransform();
      this._panningTransform = translateTransform2;
      this.CreateVisuals();
    }

    private void LoopingSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (!this._isAnimating)
        return;
      double y = this._panningTransform.Y;
      this.StopAnimation();
      this._panningTransform.Y = y;
      this._isAnimating = false;
      this._state = LoopingSelector.State.Dragging;
    }

    private void LoopingSelector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      if (this._selectedItem == sender || this._state != LoopingSelector.State.Dragging || this._isAnimating)
        return;
      this.SelectAndSnapToClosest();
      this._state = LoopingSelector.State.Expanded;
    }

    private void OnTap(object sender, GestureEventArgs e)
    {
      if (this._panningTransform == null)
        return;
      if (this.IsExpanded)
        this.SelectAndSnapToClosest();
      else
        this.IsExpanded = true;
      e.Handled = true;
    }

    private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this._isAllowedToDragVertically = true;
      this._isDragging = false;
    }

    private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (this._isDragging)
      {
        Duration panDuration = LoopingSelector._panDuration;
        IEasingFunction panEase = this._panEase;
        LoopingSelector loopingSelector = this;
        double dragTarget = loopingSelector._dragTarget;
        double y = e.DeltaManipulation.Translation.Y;
        double num1;
        double num2 = num1 = dragTarget + y;
        loopingSelector._dragTarget = num1;
        double to = num2;
        this.AnimatePanel(panDuration, panEase, to);
        this._changeStateAfterAnimation = false;
        e.Handled = true;
      }
      else if (Math.Abs(e.CumulativeManipulation.Translation.X) > 12.0)
      {
        this._isAllowedToDragVertically = false;
      }
      else
      {
        if (!this._isAllowedToDragVertically || Math.Abs(e.CumulativeManipulation.Translation.Y) <= 12.0)
          return;
        this._isDragging = true;
        this._state = LoopingSelector.State.Dragging;
        e.Handled = true;
        this._selectedItem = (LoopingSelectorItem) null;
        if (!this.IsExpanded)
          this.IsExpanded = true;
        this._dragTarget = this._panningTransform.Y;
        this.UpdateItemState();
      }
    }

    private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (!this._isDragging)
        return;
      if (e.IsInertial)
      {
        this._state = LoopingSelector.State.Flicking;
        this._selectedItem = (LoopingSelectorItem) null;
        if (!this.IsExpanded)
          this.IsExpanded = true;
        Point initialVelocity = new Point(0.0, e.FinalVelocities.LinearVelocity.Y);
        double stopTime = PhysicsConstants.GetStopTime(initialVelocity);
        Point stopPoint = PhysicsConstants.GetStopPoint(initialVelocity);
        IEasingFunction easingFunction = PhysicsConstants.GetEasingFunction(stopTime);
        this.AnimatePanel(new Duration(TimeSpan.FromSeconds(stopTime)), easingFunction, this._panningTransform.Y + stopPoint.Y);
        this._changeStateAfterAnimation = false;
        e.Handled = true;
        this._selectedItem = (LoopingSelectorItem) null;
        this.UpdateItemState();
      }
      if (this._state == LoopingSelector.State.Dragging)
        this.SelectAndSnapToClosest();
      this._state = LoopingSelector.State.Expanded;
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      this._centeringTransform.Y = Math.Round(e.NewSize.Height / 2.0);
      RectangleGeometry rectangleGeometry1 = new RectangleGeometry();
      RectangleGeometry rectangleGeometry2 = rectangleGeometry1;
      Size newSize = e.NewSize;
      double width = newSize.Width;
      newSize = e.NewSize;
      double height = newSize.Height;
      Rect rect = new Rect(0.0, 0.0, width, height);
      rectangleGeometry2.Rect = rect;
      ((UIElement) this).Clip = (Geometry) rectangleGeometry1;
      this.UpdateData();
    }

    private void OnWrapperClick(object sender, EventArgs e)
    {
      if (this._state == LoopingSelector.State.Normal)
      {
        this._state = LoopingSelector.State.Expanded;
        this.IsExpanded = true;
      }
      else
      {
        if (this._state != LoopingSelector.State.Expanded)
          return;
        if (!this._isAnimating && sender == this._selectedItem)
        {
          this._state = LoopingSelector.State.Normal;
          this.IsExpanded = false;
        }
        else if (sender != this._selectedItem && !this._isAnimating)
        {
          this.SelectAndSnapTo((LoopingSelectorItem) sender);
          this._selectedItem.SetState(LoopingSelectorItem.State.Selected, true);
        }
      }
    }

    private void SelectAndSnapTo(LoopingSelectorItem item)
    {
      if (item == null)
        return;
      if (this._selectedItem != null && this._selectedItem != item)
        this._selectedItem.SetState(this.IsExpanded ? LoopingSelectorItem.State.Expanded : LoopingSelectorItem.State.Normal, true);
      if (this._selectedItem != item)
      {
        this._selectedItem = item;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
        {
          this._isSelecting = true;
          this.DataSource.SelectedItem = ((FrameworkElement) item).DataContext;
          this._isSelecting = false;
        }));
      }
      TranslateTransform transform = item.Transform;
      if (transform != null)
      {
        double to = -transform.Y - Math.Round(((FrameworkElement) item).ActualHeight / 2.0);
        if (this._panningTransform.Y != to)
        {
          this.AnimatePanel(LoopingSelector._selectDuration, this._selectEase, to);
          this._changeStateAfterAnimation = true;
        }
        else
          this._selectedItem.SetState(LoopingSelectorItem.State.Selected, true);
      }
    }

    private void UpdateData()
    {
      if (!this.IsReady)
        return;
      this._temporaryItemsPool = new Queue<LoopingSelectorItem>(((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children).Count);
      foreach (LoopingSelectorItem child in (PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)
      {
        if (child.GetState() == LoopingSelectorItem.State.Selected)
          child.SetState(LoopingSelectorItem.State.Normal, false);
        this._temporaryItemsPool.Enqueue(child);
        child.Remove();
      }
      ((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children).Clear();
      this.StopAnimation();
      this._panningTransform.Y = 0.0;
      this._minimumPanelScroll = -3.4028234663852886E+38;
      this._maximumPanelScroll = 3.4028234663852886E+38;
      this.Balance();
    }

    private void AnimatePanel(Duration duration, IEasingFunction ease, double to)
    {
      double num1 = Math.Max(this._minimumPanelScroll, Math.Min(this._maximumPanelScroll, to));
      if (to != num1)
      {
        double num2 = Math.Abs(this._panningTransform.Y - to);
        double num3 = Math.Abs(this._panningTransform.Y - num1) / num2;
        duration = new Duration(TimeSpan.FromMilliseconds((double) duration.TimeSpan.Milliseconds * num3));
        to = num1;
      }
      double y = this._panningTransform.Y;
      this.StopAnimation();
      CompositionTarget.Rendering += new EventHandler(this.AnimationPerFrameCallback);
      ((Timeline) this._panelAnimation).Duration = duration;
      this._panelAnimation.EasingFunction = ease;
      this._panelAnimation.From = new double?(y);
      this._panelAnimation.To = new double?(to);
      this._panelStoryboard.Begin();
      this._panelStoryboard.SeekAlignedToLastTick(TimeSpan.Zero);
      this._isAnimating = true;
    }

    private void StopAnimation()
    {
      this._panelStoryboard.Stop();
      this._isAnimating = false;
      CompositionTarget.Rendering -= new EventHandler(this.AnimationPerFrameCallback);
    }

    private void Brake(double newStoppingPoint)
    {
      double? nullable = this._panelAnimation.To;
      double num1 = nullable.Value;
      nullable = this._panelAnimation.From;
      double num2 = nullable.Value;
      double num3 = num1 - num2;
      this.AnimatePanel(new Duration(TimeSpan.FromMilliseconds((double) ((Timeline) this._panelAnimation).Duration.TimeSpan.Milliseconds * Math.Abs((newStoppingPoint - this._panningTransform.Y) / num3))), this._panelAnimation.EasingFunction, newStoppingPoint);
      this._changeStateAfterAnimation = false;
    }

    private bool IsReady => ((FrameworkElement) this).ActualHeight > 0.0 && this.DataSource != null && this._itemsPanel != null;

    /// <summary>Balances the items.</summary>
    private void Balance()
    {
      if (!this.IsReady)
        return;
      double actualItemWidth = this.ActualItemWidth;
      double actualItemHeight = this.ActualItemHeight;
      this._additionalItemsCount = (int) Math.Round(((FrameworkElement) this).ActualHeight * 1.5 / actualItemHeight);
      int num = -1;
      LoopingSelectorItem loopingSelectorItem1;
      if (((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children).Count == 0)
      {
        num = 0;
        this._selectedItem = loopingSelectorItem1 = this.CreateAndAddItem(this._itemsPanel, this.DataSource.SelectedItem);
        loopingSelectorItem1.Transform.Y = -actualItemHeight / 2.0;
        loopingSelectorItem1.Transform.X = (((FrameworkElement) this).ActualWidth - actualItemWidth) / 2.0;
        loopingSelectorItem1.SetState(LoopingSelectorItem.State.Selected, false);
      }
      else
        loopingSelectorItem1 = (LoopingSelectorItem) ((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)[this.GetClosestItem()];
      if (!this.IsExpanded)
        return;
      int count1;
      LoopingSelectorItem before = LoopingSelector.GetFirstItem(loopingSelectorItem1, out count1);
      int count2;
      LoopingSelectorItem after = LoopingSelector.GetLastItem(loopingSelectorItem1, out count2);
      LoopingSelectorItem loopingSelectorItem2;
      if (count1 < count2 || count1 < this._additionalItemsCount)
      {
        for (; count1 < this._additionalItemsCount; ++count1)
        {
          object previous = this.DataSource.GetPrevious(((FrameworkElement) before).DataContext);
          if (previous == null)
          {
            this._maximumPanelScroll = -before.Transform.Y - actualItemHeight / 2.0;
            if (this._isAnimating && this._panelAnimation.To.Value > this._maximumPanelScroll)
            {
              this.Brake(this._maximumPanelScroll);
              break;
            }
            break;
          }
          loopingSelectorItem2 = (LoopingSelectorItem) null;
          LoopingSelectorItem loopingSelectorItem3;
          if (count2 > this._additionalItemsCount)
          {
            loopingSelectorItem3 = after;
            after = after.Previous;
            loopingSelectorItem3.Remove();
            loopingSelectorItem3.Content = ((FrameworkElement) loopingSelectorItem3).DataContext = previous;
          }
          else
          {
            loopingSelectorItem3 = this.CreateAndAddItem(this._itemsPanel, previous);
            loopingSelectorItem3.Transform.X = (((FrameworkElement) this).ActualWidth - actualItemWidth) / 2.0;
          }
          loopingSelectorItem3.Transform.Y = before.Transform.Y - actualItemHeight;
          loopingSelectorItem3.InsertBefore(before);
          before = loopingSelectorItem3;
        }
      }
      if (count2 < count1 || count2 < this._additionalItemsCount)
      {
        for (; count2 < this._additionalItemsCount; ++count2)
        {
          object next = this.DataSource.GetNext(((FrameworkElement) after).DataContext);
          if (next == null)
          {
            this._minimumPanelScroll = -after.Transform.Y - actualItemHeight / 2.0;
            if (this._isAnimating && this._panelAnimation.To.Value < this._minimumPanelScroll)
            {
              this.Brake(this._minimumPanelScroll);
              break;
            }
            break;
          }
          loopingSelectorItem2 = (LoopingSelectorItem) null;
          LoopingSelectorItem loopingSelectorItem4;
          if (count1 > this._additionalItemsCount)
          {
            loopingSelectorItem4 = before;
            before = before.Next;
            loopingSelectorItem4.Remove();
            loopingSelectorItem4.Content = ((FrameworkElement) loopingSelectorItem4).DataContext = next;
          }
          else
          {
            loopingSelectorItem4 = this.CreateAndAddItem(this._itemsPanel, next);
            loopingSelectorItem4.Transform.X = (((FrameworkElement) this).ActualWidth - actualItemWidth) / 2.0;
          }
          loopingSelectorItem4.Transform.Y = after.Transform.Y + actualItemHeight;
          loopingSelectorItem4.InsertAfter(after);
          after = loopingSelectorItem4;
        }
      }
      this._temporaryItemsPool = (Queue<LoopingSelectorItem>) null;
    }

    private static LoopingSelectorItem GetFirstItem(LoopingSelectorItem item, out int count)
    {
      count = 0;
      for (; item.Previous != null; item = item.Previous)
        ++count;
      return item;
    }

    private static LoopingSelectorItem GetLastItem(LoopingSelectorItem item, out int count)
    {
      count = 0;
      for (; item.Next != null; item = item.Next)
        ++count;
      return item;
    }

    private void AnimationPerFrameCallback(object sender, EventArgs e) => this.Balance();

    private int GetClosestItem()
    {
      if (!this.IsReady)
        return -1;
      double actualItemHeight = this.ActualItemHeight;
      int count = ((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children).Count;
      double y = this._panningTransform.Y;
      double num1 = actualItemHeight / 2.0;
      int closestItem = -1;
      double num2 = double.MaxValue;
      for (int index = 0; index < count; ++index)
      {
        double num3 = Math.Abs(((LoopingSelectorItem) ((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)[index]).Transform.Y + num1 + y);
        if (num3 <= num1)
        {
          closestItem = index;
          break;
        }
        if (num2 > num3)
        {
          num2 = num3;
          closestItem = index;
        }
      }
      return closestItem;
    }

    private void PanelStoryboardCompleted(object sender, EventArgs e)
    {
      CompositionTarget.Rendering -= new EventHandler(this.AnimationPerFrameCallback);
      this._isAnimating = false;
      if (this._state == LoopingSelector.State.Dragging)
        return;
      if (this._changeStateAfterAnimation)
        this._selectedItem.SetState(LoopingSelectorItem.State.Selected, true);
      this.SelectAndSnapToClosest();
    }

    private void SelectAndSnapToClosest()
    {
      if (!this.IsReady)
        return;
      int closestItem = this.GetClosestItem();
      if (closestItem == -1)
        return;
      this.SelectAndSnapTo((LoopingSelectorItem) ((PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)[closestItem]);
    }

    private void UpdateItemState()
    {
      if (!this.IsReady)
        return;
      bool isExpanded = this.IsExpanded;
      foreach (LoopingSelectorItem child in (PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)
      {
        if (child == this._selectedItem)
          child.SetState(LoopingSelectorItem.State.Selected, true);
        else
          child.SetState(isExpanded ? LoopingSelectorItem.State.Expanded : LoopingSelectorItem.State.Normal, true);
      }
    }

    private void UpdateItemTemplates()
    {
      if (!this.IsReady)
        return;
      foreach (ContentControl child in (PresentationFrameworkCollection<UIElement>) this._itemsPanel.Children)
        child.ContentTemplate = this.ItemTemplate;
    }

    private double ActualItemWidth
    {
      get
      {
        Thickness padding = this.Padding;
        double left = padding.Left;
        padding = this.Padding;
        double right = padding.Right;
        return left + right + this.ItemSize.Width;
      }
    }

    private double ActualItemHeight
    {
      get
      {
        Thickness padding = this.Padding;
        double top = padding.Top;
        padding = this.Padding;
        double bottom = padding.Bottom;
        return top + bottom + this.ItemSize.Height;
      }
    }

    private void CreateVisuals()
    {
      this._panelAnimation = new DoubleAnimation();
      Storyboard.SetTarget((Timeline) this._panelAnimation, (DependencyObject) this._panningTransform);
      Storyboard.SetTargetProperty((Timeline) this._panelAnimation, new PropertyPath("Y", new object[0]));
      this._panelStoryboard = new Storyboard();
      ((PresentationFrameworkCollection<Timeline>) this._panelStoryboard.Children).Add((Timeline) this._panelAnimation);
      ((Timeline) this._panelStoryboard).Completed += new EventHandler(this.PanelStoryboardCompleted);
    }

    private void CreateEventHandlers()
    {
      ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
      ((UIElement) this).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
      ((UIElement) this).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
      ((UIElement) this).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
      ((UIElement) this).Tap += new EventHandler<GestureEventArgs>(this.OnTap);
      ((UIElement) this).AddHandler(UIElement.MouseLeftButtonDownEvent, (Delegate) new MouseButtonEventHandler(this.LoopingSelector_MouseLeftButtonDown), true);
      ((UIElement) this).AddHandler(UIElement.MouseLeftButtonUpEvent, (Delegate) new MouseButtonEventHandler(this.LoopingSelector_MouseLeftButtonUp), true);
    }

    private LoopingSelectorItem CreateAndAddItem(Panel parent, object content)
    {
      bool flag = this._temporaryItemsPool != null && this._temporaryItemsPool.Count > 0;
      LoopingSelectorItem andAddItem = flag ? this._temporaryItemsPool.Dequeue() : new LoopingSelectorItem();
      andAddItem.ForegroundItem = this.ForegroundItem;
      andAddItem.BackgroundItem = this.BackgroundItem;
      andAddItem.BorderBrushItem = this.BorderBrushItem;
      andAddItem.BorderBrushNotSelected = this.BorderBrushNotSelectedItem;
      ((Control) andAddItem).Foreground = this.ForegroundNotSelectedItem;
      if (!flag)
      {
        andAddItem.ContentTemplate = this.ItemTemplate;
        ((FrameworkElement) andAddItem).Width = this.ItemSize.Width;
        ((FrameworkElement) andAddItem).Height = this.ItemSize.Height;
        ((Control) andAddItem).Padding = this.ItemMargin;
        andAddItem.Click += new EventHandler<EventArgs>(this.OnWrapperClick);
      }
      ((FrameworkElement) andAddItem).DataContext = andAddItem.Content = content;
      ((PresentationFrameworkCollection<UIElement>) parent.Children).Add((UIElement) andAddItem);
      if (!flag)
        ((Control) andAddItem).ApplyTemplate();
      if (this.IsExpanded)
        andAddItem.SetState(LoopingSelectorItem.State.Expanded, false);
      return andAddItem;
    }

    private enum State
    {
      Normal,
      Expanded,
      Dragging,
      Snapping,
      Flicking,
    }
  }
}
