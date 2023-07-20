// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Rating
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A rating control implementation that allows for a parameterized
  /// number of RatingItems, which can be arbitrarily styled using the
  /// FilledItemStyle and UnfilledItemStyle properties, or templated
  /// using an applied ControlTemplate.
  /// </summary>
  [TemplatePart(Name = "FilledClipElement", Type = typeof (UIElement))]
  [TemplatePart(Name = "FilledGridElement", Type = typeof (Grid))]
  [TemplateVisualState(Name = "Visible", GroupName = "DragHelperStates")]
  [TemplateVisualState(Name = "Collapsed", GroupName = "DragHelperStates")]
  [TemplatePart(Name = "UnfilledGridElement", Type = typeof (Grid))]
  [TemplatePart(Name = "DragBorderElement", Type = typeof (Border))]
  [TemplatePart(Name = "DragTextBlockElement", Type = typeof (TextBlock))]
  public class Rating : Control
  {
    private const string FilledClipElement = "FilledClipElement";
    private const string FilledGridElement = "FilledGridElement";
    private const string UnfilledGridElement = "UnfilledGridElement";
    private const string DragBorderElement = "DragBorderElement";
    private const string DragTextBlockElement = "DragTextBlockElement";
    private const string DragHelperStates = "DragHelperStates";
    private const string DragHelperCollapsed = "Collapsed";
    private const string DragHelperVisible = "Visible";
    private UIElement _filledClipElement;
    private Grid _filledGridElement;
    private Grid _unfilledGridElement;
    private Border _dragBorderElement;
    private TextBlock _dragTextBlockElement;
    private Geometry _clippingMask;
    private List<RatingItem> _filledItemCollection = new List<RatingItem>();
    private List<RatingItem> _unfilledItemCollection = new List<RatingItem>();
    /// <summary>Identifies the FilledItemStyle dependency property.</summary>
    public static readonly DependencyProperty FilledItemStyleProperty = DependencyProperty.Register(nameof (FilledItemStyle), typeof (Style), typeof (Rating), new PropertyMetadata(new PropertyChangedCallback(Rating.OnFilledItemStyleChanged)));
    /// <summary>Identifies the UnfilledItemStyle dependency property.</summary>
    public static readonly DependencyProperty UnfilledItemStyleProperty = DependencyProperty.Register(nameof (UnfilledItemStyle), typeof (Style), typeof (Rating), new PropertyMetadata(new PropertyChangedCallback(Rating.OnUnfilledItemStyleChanged)));
    /// <summary>Identifies the RatingItemCount dependency property.</summary>
    public static readonly DependencyProperty RatingItemCountProperty = DependencyProperty.Register(nameof (RatingItemCount), typeof (int), typeof (Rating), new PropertyMetadata((object) 5, new PropertyChangedCallback(Rating.OnRatingItemCountChanged)));
    /// <summary>Identifies the Value dependency property.</summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (double), typeof (Rating), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(Rating.OnValueChanged)));
    /// <summary>Identifies the ReadOnly dependency property.</summary>
    public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register(nameof (ReadOnly), typeof (bool), typeof (Rating), new PropertyMetadata((object) false));
    /// <summary>
    /// Identifies the AllowHalfItemIncrement dependency property.
    /// </summary>
    public static readonly DependencyProperty AllowHalfItemIncrementProperty = DependencyProperty.Register(nameof (AllowHalfItemIncrement), typeof (bool), typeof (Rating), new PropertyMetadata((object) false));
    /// <summary>
    /// Identifies the AllowNoItemsSelected dependency property.
    /// </summary>
    public static readonly DependencyProperty AllowSelectingZeroProperty = DependencyProperty.Register(nameof (AllowSelectingZero), typeof (bool), typeof (Rating), new PropertyMetadata((object) false));
    /// <summary>
    /// Identifies the AllowNoItemsSelected dependency property.
    /// </summary>
    public static readonly DependencyProperty ShowSelectionHelperProperty = DependencyProperty.Register("ShowSelectionHelperItems", typeof (bool), typeof (Rating), new PropertyMetadata((object) false));
    /// <summary>Identifies the Orientation dependency property.</summary>
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (Rating), new PropertyMetadata((object) (Orientation) 1, new PropertyChangedCallback(Rating.OnOrientationChanged)));

    /// <summary>
    /// Occures when the value of this control changes, either from user interaction or by directly setting
    /// the property.
    /// </summary>
    public event EventHandler ValueChanged;

    /// <summary>Initializes a new instance of the Rating type.</summary>
    public Rating()
    {
      this.DefaultStyleKey = (object) typeof (Rating);
      ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
      ((UIElement) this).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
      ((UIElement) this).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
      ((UIElement) this).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
      this.AdjustNumberOfRatingItems();
      this.SynchronizeGrids();
      this.UpdateClippingMask();
    }

    private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (this.ReadOnly)
        return;
      this.PerformValueCalculation(e.ManipulationOrigin, e.ManipulationContainer);
      this.UpdateDragHelper();
      if (this.ShowSelectionHelper)
        this.ChangeDragHelperVisibility(true);
    }

    private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      if (this.ReadOnly)
        return;
      this.PerformValueCalculation(e.ManipulationOrigin, e.ManipulationContainer);
      this.UpdateDragHelper();
    }

    private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (!this.ReadOnly)
        this.PerformValueCalculation(e.ManipulationOrigin, e.ManipulationContainer);
      this.ChangeDragHelperVisibility(false);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) => this.UpdateClippingMask();

    /// <summary>
    /// Applies the template and extracts both a visual state and template
    /// parts.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      this._filledClipElement = this.GetTemplateChild("FilledClipElement") as UIElement;
      this._filledGridElement = this.GetTemplateChild("FilledGridElement") as Grid;
      this._unfilledGridElement = this.GetTemplateChild("UnfilledGridElement") as Grid;
      this._dragBorderElement = this.GetTemplateChild("DragBorderElement") as Border;
      this._dragTextBlockElement = this.GetTemplateChild("DragTextBlockElement") as TextBlock;
      if (this._filledClipElement != null)
        this._filledClipElement.Clip = this._clippingMask;
      if (this._dragBorderElement != null)
        ((UIElement) this._dragBorderElement).RenderTransform = (Transform) new TranslateTransform();
      VisualStateManager.GoToState((Control) this, "Collapsed", false);
      this.SynchronizeGrids();
    }

    /// <summary>
    /// Checks for existence of a drag text helper, and sets the visibility of this element.
    /// </summary>
    /// <param name="isVisible">Indicates whether the UIElement should be visible.</param>
    private void ChangeDragHelperVisibility(bool isVisible)
    {
      if (this._dragBorderElement == null)
        return;
      if (isVisible)
        VisualStateManager.GoToState((Control) this, "Visible", true);
      else
        VisualStateManager.GoToState((Control) this, "Collapsed", true);
    }

    /// <summary>
    /// Updates both the text and the positioning of the drag text helper.
    /// </summary>
    private void UpdateDragHelper()
    {
      if (this.RatingItemCount == 0)
        return;
      string format = !this.AllowHalfItemIncrement ? "F0" : "F1";
      if (this._dragTextBlockElement != null)
        this._dragTextBlockElement.Text = this.Value.ToString(format, (IFormatProvider) CultureInfo.CurrentCulture);
      if (this.Orientation == 1)
      {
        if (this._dragBorderElement == null)
          return;
        double num1 = ((FrameworkElement) this._dragBorderElement).ActualWidth / 2.0;
        double num2 = ((FrameworkElement) this._filledItemCollection[0]).ActualWidth / 2.0;
        TranslateTransform renderTransform = (TranslateTransform) ((UIElement) this._dragBorderElement).RenderTransform;
        int num3 = this.AllowHalfItemIncrement ? 1 : (this.AllowSelectingZero ? 1 : 0);
        renderTransform.X = num3 != 0 ? this.Value / (double) this.RatingItemCount * ((FrameworkElement) this).ActualWidth - num1 : this.Value / (double) this.RatingItemCount * ((FrameworkElement) this).ActualWidth - num1 - num2;
        renderTransform.Y = -(((FrameworkElement) this).ActualHeight / 2.0 + 15.0);
      }
      else if (this._dragBorderElement != null)
      {
        double num4 = ((FrameworkElement) this._dragBorderElement).ActualHeight / 2.0;
        double num5 = ((FrameworkElement) this._filledItemCollection[0]).ActualHeight / 2.0;
        TranslateTransform renderTransform = (TranslateTransform) ((UIElement) this._dragBorderElement).RenderTransform;
        int num6 = this.AllowHalfItemIncrement ? 1 : (this.AllowSelectingZero ? 1 : 0);
        renderTransform.Y = num6 != 0 ? this.Value / (double) this.RatingItemCount * ((FrameworkElement) this).ActualHeight - num4 : this.Value / (double) this.RatingItemCount * ((FrameworkElement) this).ActualHeight - num4 - num5;
        renderTransform.X = -(((FrameworkElement) this).ActualWidth / 2.0 + 15.0);
      }
    }

    /// <summary>
    /// Calculates the new Value of this control using a location and relative source.
    /// The location is translated to the control, and then used to set the value.
    /// </summary>
    /// <param name="location">A point.</param>
    /// <param name="locationRelativeSource">The UIElement that the point originated from.</param>
    private void PerformValueCalculation(Point location, UIElement locationRelativeSource)
    {
      location = locationRelativeSource.TransformToVisual((UIElement) this).Transform(location);
      int count = this._filledItemCollection.Count;
      if (this.AllowHalfItemIncrement)
        count *= 2;
      double num = this.Orientation != 1 ? Math.Ceiling(location.Y / ((FrameworkElement) this).ActualHeight * (double) count) : Math.Ceiling(location.X / ((FrameworkElement) this).ActualWidth * (double) count);
      if (!this.AllowSelectingZero && num <= 0.0)
        num = 1.0;
      this.Value = num;
    }

    /// <summary>
    /// Updates the clipping mask on _filledClipElement, if present, to reflect the current Value of the control.
    /// </summary>
    private void UpdateClippingMask()
    {
      Rect rect = this.Orientation != 1 ? new Rect(0.0, 0.0, ((FrameworkElement) this).ActualWidth, (((FrameworkElement) this).ActualHeight - this.BorderThickness.Top - this.BorderThickness.Bottom) * (this.Value / (double) this.RatingItemCount)) : new Rect(0.0, 0.0, (((FrameworkElement) this).ActualWidth - this.BorderThickness.Right - this.BorderThickness.Left) * (this.Value / (double) this.RatingItemCount), ((FrameworkElement) this).ActualHeight);
      if (this._clippingMask is RectangleGeometry clippingMask)
        clippingMask.Rect = rect;
      else
        this._clippingMask = (Geometry) new RectangleGeometry()
        {
          Rect = rect
        };
    }

    /// <summary>
    /// Builds a new RatingItem, taking in an optional Style to be applied to it.
    /// </summary>
    /// <param name="s">An optional style to apply to the RatingItem</param>
    /// <returns></returns>
    private static RatingItem BuildNewRatingItem(Style s)
    {
      RatingItem ratingItem = new RatingItem();
      if (s != null)
        ((FrameworkElement) ratingItem).Style = s;
      return ratingItem;
    }

    /// <summary>
    /// Adds or removes RatingItem objects in the _filledItemCollection and _unfilledItemCollection lists to
    /// reflect the value of RatingItemCount.
    /// </summary>
    private void AdjustNumberOfRatingItems()
    {
      while (this._filledItemCollection.Count > this.RatingItemCount)
        this._filledItemCollection.RemoveAt(0);
      while (this._unfilledItemCollection.Count > this.RatingItemCount)
        this._unfilledItemCollection.RemoveAt(0);
      while (this._filledItemCollection.Count < this.RatingItemCount)
        this._filledItemCollection.Add(Rating.BuildNewRatingItem(this.FilledItemStyle));
      while (this._unfilledItemCollection.Count < this.RatingItemCount)
        this._unfilledItemCollection.Add(Rating.BuildNewRatingItem(this.UnfilledItemStyle));
    }

    /// <summary>
    /// Adjusts the RowDefinition and ColumnDefinition collections on a Grid to be consistent with the
    /// number of RatingItems present in the collections, and finally adds these RatingItems to the
    /// grid's Children, setting their Row and Column dependency properties.
    /// </summary>
    /// <param name="grid">The grid to fix up.</param>
    /// <param name="ratingItemList">A rating list source to add to the grid.</param>
    private void SynchronizeGrid(Grid grid, IList<RatingItem> ratingItemList)
    {
      if (grid == null)
        return;
      ((PresentationFrameworkCollection<RowDefinition>) grid.RowDefinitions).Clear();
      ((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions).Clear();
      if (this.Orientation == 1)
      {
        while (((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions).Count < ratingItemList.Count)
          ((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions).Add(new ColumnDefinition()
          {
            Width = new GridLength(1.0, (GridUnitType) 2)
          });
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Clear();
        for (int index = 0; index < ratingItemList.Count; ++index)
        {
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Add((UIElement) ratingItemList[index]);
          Grid.SetColumn((FrameworkElement) ratingItemList[index], index);
          Grid.SetRow((FrameworkElement) ratingItemList[index], 0);
        }
      }
      else
      {
        while (((PresentationFrameworkCollection<RowDefinition>) grid.RowDefinitions).Count < ratingItemList.Count)
          ((PresentationFrameworkCollection<RowDefinition>) grid.RowDefinitions).Add(new RowDefinition()
          {
            Height = new GridLength(1.0, (GridUnitType) 2)
          });
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Clear();
        for (int index = 0; index < ratingItemList.Count; ++index)
        {
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Add((UIElement) ratingItemList[index]);
          Grid.SetRow((FrameworkElement) ratingItemList[index], index);
          Grid.SetColumn((FrameworkElement) ratingItemList[index], 0);
        }
      }
    }

    /// <summary>
    /// Helper function to call SynchronizeGrid on both Grids captured from the ControlTemplate.
    /// </summary>
    private void SynchronizeGrids()
    {
      this.SynchronizeGrid(this._unfilledGridElement, (IList<RatingItem>) this._unfilledItemCollection);
      this.SynchronizeGrid(this._filledGridElement, (IList<RatingItem>) this._filledItemCollection);
    }

    /// <summary>
    /// Gets or sets the style that will be applied to filled RatingItems.
    /// This style can only be applied once to a RatingItem.
    /// </summary>
    public Style FilledItemStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(Rating.FilledItemStyleProperty);
      set => ((DependencyObject) this).SetValue(Rating.FilledItemStyleProperty, (object) value);
    }

    private static void OnFilledItemStyleChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      ((Rating) dependencyObject).OnFilledItemStyleChanged();
    }

    private void OnFilledItemStyleChanged()
    {
      foreach (FrameworkElement filledItem in this._filledItemCollection)
        filledItem.Style = this.FilledItemStyle;
    }

    /// <summary>
    /// Gets or sets the style that will be applied to unfilled RatingItems.
    /// This style can only be applied once to a RatingItem.
    /// </summary>
    public Style UnfilledItemStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(Rating.UnfilledItemStyleProperty);
      set => ((DependencyObject) this).SetValue(Rating.UnfilledItemStyleProperty, (object) value);
    }

    private static void OnUnfilledItemStyleChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      ((Rating) dependencyObject).OnUnfilledItemStyleChanged();
    }

    private void OnUnfilledItemStyleChanged()
    {
      foreach (FrameworkElement unfilledItem in this._unfilledItemCollection)
        unfilledItem.Style = this.UnfilledItemStyle;
    }

    /// <summary>
    /// Gets or sets the number of RatingItems that will be displayed
    /// by the control. Changing this property will cause elements to be
    /// added or removed from the FilledItemsCollection
    /// and UnfilledItemsCollection.
    /// </summary>
    public int RatingItemCount
    {
      get => (int) ((DependencyObject) this).GetValue(Rating.RatingItemCountProperty);
      set => ((DependencyObject) this).SetValue(Rating.RatingItemCountProperty, (object) value);
    }

    private static void OnRatingItemCountChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      ((Rating) dependencyObject).OnRatingItemCountChanged();
    }

    private void OnRatingItemCountChanged()
    {
      if (this.RatingItemCount <= 0)
        this.RatingItemCount = 0;
      this.AdjustNumberOfRatingItems();
      this.SynchronizeGrids();
    }

    /// <summary>
    /// Gets or sets the current value of the control. This value
    /// corresponds to the percentage of filled RatingItems displayed,
    /// as determiend by a Clipping mask.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Property traditionally named value")]
    public double Value
    {
      get => (double) ((DependencyObject) this).GetValue(Rating.ValueProperty);
      set
      {
        ((DependencyObject) this).SetValue(Rating.ValueProperty, (object) value);
        if (this.ValueChanged == null)
          return;
        this.ValueChanged((object) this, EventArgs.Empty);
      }
    }

    private static void OnValueChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      ((Rating) dependencyObject).OnValueChanged();
    }

    private void OnValueChanged()
    {
      if (this.Value > (double) this.RatingItemCount || this.Value < 0.0)
        this.Value = Math.Max(0.0, Math.Min((double) this.RatingItemCount, this.Value));
      this.UpdateClippingMask();
    }

    /// <summary>
    /// Gets or sets the value indicating whether this control will
    /// process user input and update the Value property.
    /// </summary>
    public bool ReadOnly
    {
      get => (bool) ((DependencyObject) this).GetValue(Rating.ReadOnlyProperty);
      set => ((DependencyObject) this).SetValue(Rating.ReadOnlyProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the value indicating whether this control will
    /// allow users to select half items when dragging or touching.
    /// </summary>
    public bool AllowHalfItemIncrement
    {
      get => (bool) ((DependencyObject) this).GetValue(Rating.AllowHalfItemIncrementProperty);
      set => ((DependencyObject) this).SetValue(Rating.AllowHalfItemIncrementProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the value indicating whether this control will
    /// allow users to drag the selected items to zero.
    /// </summary>
    public bool AllowSelectingZero
    {
      get => (bool) ((DependencyObject) this).GetValue(Rating.AllowSelectingZeroProperty);
      set => ((DependencyObject) this).SetValue(Rating.AllowSelectingZeroProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the value indicating whether this control will
    /// display the drag selection helper.
    /// </summary>
    public bool ShowSelectionHelper
    {
      get => (bool) ((DependencyObject) this).GetValue(Rating.ShowSelectionHelperProperty);
      set => ((DependencyObject) this).SetValue(Rating.ShowSelectionHelperProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the value indicating the current orientation of the
    /// control.
    /// </summary>
    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(Rating.OrientationProperty);
      set => ((DependencyObject) this).SetValue(Rating.OrientationProperty, (object) value);
    }

    private static void OnOrientationChanged(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs e)
    {
      ((Rating) dependencyObject).OnOrientationChanged();
    }

    private void OnOrientationChanged()
    {
      this.SynchronizeGrids();
      this.UpdateClippingMask();
    }
  }
}
