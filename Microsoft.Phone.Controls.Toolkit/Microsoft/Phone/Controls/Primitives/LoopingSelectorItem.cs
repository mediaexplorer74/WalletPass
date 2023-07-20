// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.LoopingSelectorItem
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// The items that will be contained in the LoopingSelector.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplatePart(Name = "Transform", Type = typeof (TranslateTransform))]
  [TemplateVisualState(GroupName = "Common", Name = "Normal")]
  [TemplateVisualState(GroupName = "Common", Name = "Selected")]
  [TemplateVisualState(GroupName = "Common", Name = "Expanded")]
  public class LoopingSelectorItem : ContentControl
  {
    private const string TransformPartName = "Transform";
    private const string CommonGroupName = "Common";
    private const string NormalStateName = "Normal";
    private const string ExpandedStateName = "Expanded";
    private const string SelectedStateName = "Selected";
    private bool _shouldClick;
    private LoopingSelectorItem.State _state;
    private Brush _borderBrushItem;
    private Brush _foregroundItem;
    private Brush _backgroundItem;
    private Brush _borderBrushNotSelectedItem;

    /// <summary>Create a new LoopingSelectorItem.</summary>
    public LoopingSelectorItem()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (LoopingSelectorItem);
      ((UIElement) this).MouseLeftButtonDown += new MouseButtonEventHandler(this.LoopingSelectorItem_MouseLeftButtonDown);
      ((UIElement) this).MouseLeftButtonUp += new MouseButtonEventHandler(this.LoopingSelectorItem_MouseLeftButtonUp);
      ((UIElement) this).LostMouseCapture += new MouseEventHandler(this.LoopingSelectorItem_LostMouseCapture);
      ((UIElement) this).Tap += new EventHandler<GestureEventArgs>(this.LoopingSelectorItem_Tap);
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
    public Brush BackgroundItem
    {
      get => this._backgroundItem;
      set => this._backgroundItem = value;
    }

    /// <summary>
    /// The margin around the items, to be a part of the touchable area.
    /// </summary>
    public Brush BorderBrushNotSelected
    {
      get => this._borderBrushNotSelectedItem;
      set => this._borderBrushNotSelectedItem = value;
    }

    /// <summary>Put this item into a new state.</summary>
    /// <param name="newState">The new state.</param>
    /// <param name="useTransitions">Flag indicating that transitions should be used when going to the new state.</param>
    internal void SetState(LoopingSelectorItem.State newState, bool useTransitions)
    {
      if (this._state == newState)
        return;
      this._state = newState;
      switch (this._state)
      {
        case LoopingSelectorItem.State.Normal:
          VisualStateManager.GoToState((Control) this, "Normal", useTransitions);
          break;
        case LoopingSelectorItem.State.Expanded:
          VisualStateManager.GoToState((Control) this, "Expanded", useTransitions);
          break;
        case LoopingSelectorItem.State.Selected:
          VisualStateManager.GoToState((Control) this, "Selected", useTransitions);
          break;
      }
    }

    /// <summary>Returns the current state.</summary>
    /// <returns>The current state.</returns>
    internal LoopingSelectorItem.State GetState() => this._state;

    private void LoopingSelectorItem_Tap(object sender, GestureEventArgs e) => e.Handled = true;

    private void LoopingSelectorItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      ((UIElement) this).CaptureMouse();
      this._shouldClick = true;
    }

    private void LoopingSelectorItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      ((UIElement) this).ReleaseMouseCapture();
      if (!this._shouldClick)
        return;
      this._shouldClick = false;
      SafeRaise.Raise(this.Click, (object) this);
    }

    private void LoopingSelectorItem_LostMouseCapture(object sender, MouseEventArgs e) => this._shouldClick = false;

    /// <summary>
    /// The Click event. This is needed because there is no gesture for touch-down, pause
    /// longer than the Hold time, and touch-up. Tap will not be raise, and Hold is not
    /// adequate.
    /// </summary>
    public event EventHandler<EventArgs> Click;

    /// <summary>Override of OnApplyTemplate</summary>
    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (!(((Control) this).GetTemplateChild("Transform") is TranslateTransform translateTransform))
        translateTransform = new TranslateTransform();
      this.Transform = translateTransform;
    }

    internal LoopingSelectorItem Previous { get; private set; }

    internal LoopingSelectorItem Next { get; private set; }

    internal void Remove()
    {
      if (this.Previous != null)
        this.Previous.Next = this.Next;
      if (this.Next != null)
        this.Next.Previous = this.Previous;
      this.Next = this.Previous = (LoopingSelectorItem) null;
    }

    internal void InsertAfter(LoopingSelectorItem after)
    {
      this.Next = after.Next;
      this.Previous = after;
      if (after.Next != null)
        after.Next.Previous = this;
      after.Next = this;
    }

    internal void InsertBefore(LoopingSelectorItem before)
    {
      this.Next = before;
      this.Previous = before.Previous;
      if (before.Previous != null)
        before.Previous.Next = this;
      before.Previous = this;
    }

    internal TranslateTransform Transform { get; private set; }

    /// <summary>The states that this can be in.</summary>
    internal enum State
    {
      /// <summary>Not visible</summary>
      Normal,
      /// <summary>Visible</summary>
      Expanded,
      /// <summary>Selected</summary>
      Selected,
    }
  }
}
