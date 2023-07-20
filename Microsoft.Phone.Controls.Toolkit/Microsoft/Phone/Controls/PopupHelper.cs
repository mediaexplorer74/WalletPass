// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PopupHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// PopupHelper is a simple wrapper type that helps abstract platform
  /// differences out of the Popup.
  /// </summary>
  internal class PopupHelper
  {
    /// <summary>
    /// The distance from the control to the <see cref="T:Popup" /> child.
    /// </summary>
    private const double PopupOffset = 2.0;
    /// <summary>
    /// A value indicating whether Silverlight has loaded at least once,
    /// so that the wrapping canvas is not recreated.
    /// </summary>
    private bool _hasControlLoaded;

    /// <summary>
    /// Gets a value indicating whether a visual popup state is being used
    /// in the current template for the Closed state. Setting this value to
    /// true will delay the actual setting of Popup.IsOpen to false until
    /// after the visual state's transition for Closed is complete.
    /// </summary>
    public bool UsesClosingVisualState { get; private set; }

    /// <summary>Gets or sets the parent control.</summary>
    private Control Parent { get; set; }

    /// <summary>Gets or sets the expansive area outside of the popup.</summary>
    private Canvas OutsidePopupCanvas { get; set; }

    /// <summary>Gets or sets the canvas for the popup child.</summary>
    private Canvas PopupChildCanvas { get; set; }

    /// <summary>Gets or sets the maximum drop down height value.</summary>
    public double MaxDropDownHeight { get; set; }

    /// <summary>Gets the Popup control instance.</summary>
    public Popup Popup { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the actual Popup is open.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Provided for completeness.")]
    public bool IsOpen
    {
      get => this.Popup.IsOpen;
      set => this.Popup.IsOpen = value;
    }

    /// <summary>
    /// Gets or sets the popup child framework element. Can be used if an
    /// assumption is made on the child type.
    /// </summary>
    private FrameworkElement PopupChild { get; set; }

    /// <summary>The Closed event is fired after the Popup closes.</summary>
    public event EventHandler Closed;

    /// <summary>
    /// Fired when the popup children have a focus event change, allows the
    /// parent control to update visual states or react to the focus state.
    /// </summary>
    public event EventHandler FocusChanged;

    /// <summary>
    /// Fired when the popup children intercept an event that may indicate
    /// the need for a visual state update by the parent control.
    /// </summary>
    public event EventHandler UpdateVisualStates;

    /// <summary>Initializes a new instance of the PopupHelper class.</summary>
    /// <param name="parent">The parent control.</param>
    public PopupHelper(Control parent)
    {
      Debug.Assert(parent != null, "Parent should not be null.");
      this.Parent = parent;
    }

    /// <summary>Initializes a new instance of the PopupHelper class.</summary>
    /// <param name="parent">The parent control.</param>
    /// <param name="popup">The Popup template part.</param>
    public PopupHelper(Control parent, Popup popup)
      : this(parent)
    {
      this.Popup = popup;
    }

    /// <summary>
    /// Gets the <see cref="T:MatrixTransform" /> for the control.
    /// </summary>
    /// <returns>The <see cref="T:MatrixTransform" />.</returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This try-catch pattern is used by other popup controls to keep the runtime up.")]
    private MatrixTransform GetControlMatrixTransform()
    {
      try
      {
        return ((UIElement) this.Parent).TransformToVisual((UIElement) null) as MatrixTransform;
      }
      catch
      {
        this.OnClosed(EventArgs.Empty);
        return (MatrixTransform) null;
      }
    }

    /// <summary>
    /// Makes a <see cref="T:Point" /> from a <see cref="T:MatrixTransform" />.
    /// </summary>
    /// <remarks>
    /// The control's margin is counted with the offset to make the <see cref="T:Point" />.
    /// This makes the <see cref="T:Point" /> refer to the visible part of the control.
    /// </remarks>
    /// <param name="matrixTransform">The <see cref="T:MatrixTransform" />.</param>
    /// <param name="margin">The margin.</param>
    /// <returns></returns>
    private static Point MatrixTransformPoint(MatrixTransform matrixTransform, Thickness margin) => new Point(matrixTransform.Matrix.OffsetX + margin.Left, matrixTransform.Matrix.OffsetY + margin.Top);

    /// <summary>
    /// Gets the <see cref="T:Size" /> of the visible part of the control (minus the margin).
    /// </summary>
    /// <remarks>
    /// The Parent <see cref="T:Size" /> is wrong if the orientation changed, so use the ArrangeOverride <see cref="T:Size" /> if it's available.
    /// </remarks>
    /// <param name="margin">The margin.</param>
    /// <param name="finalSize">The <see cref="M:AutoCompleteBox.ArrangeOverride" /> size.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    private Size GetControlSize(Thickness margin, Size? finalSize)
    {
      Size size;
      double num1;
      if (!finalSize.HasValue)
      {
        num1 = ((FrameworkElement) this.Parent).ActualWidth;
      }
      else
      {
        size = finalSize.Value;
        num1 = size.Width;
      }
      double left = margin.Left;
      double num2 = num1 - left - margin.Right;
      double num3;
      if (!finalSize.HasValue)
      {
        num3 = ((FrameworkElement) this.Parent).ActualHeight;
      }
      else
      {
        size = finalSize.Value;
        num3 = size.Height;
      }
      double top = margin.Top;
      double num4 = num3 - top - margin.Bottom;
      return new Size(num2, num4);
    }

    /// <summary>Gets the margin for the control.</summary>
    /// <returns>The margin.</returns>
    private Thickness GetMargin()
    {
      Thickness? resource = ((FrameworkElement) this.Popup).Resources[(object) "PhoneTouchTargetOverhang"] as Thickness?;
      return resource.HasValue ? resource.Value : new Thickness(0.0);
    }

    /// <summary>
    /// Determines whether <see cref="P:Popup.Child" /> is displayed above the control.
    /// </summary>
    /// <param name="displaySize">The <see cref="T:Size" /> not covered by the SIP.</param>
    /// <param name="controlSize">The <see cref="T:Size" /> of the control.</param>
    /// <param name="controlOffset">The position of the control.</param>
    /// <returns></returns>
    private static bool IsChildAbove(Size displaySize, Size controlSize, Point controlOffset)
    {
      double y = controlOffset.Y;
      double num = displaySize.Height - controlSize.Height - y;
      return y > num;
    }

    /// <summary>
    /// Gets the minimum of three numbers and floors it at zero.
    /// </summary>
    /// <param name="x">The first number.</param>
    /// <param name="y">The second number.</param>
    /// <returns>The result.</returns>
    private static double Min0(double x, double y) => Math.Max(Math.Min(x, y), 0.0);

    /// <summary>
    /// Computes the <see cref="T:Size" /> of <see cref="P:Popup.Child" /> if displayed above the control.
    /// </summary>
    /// <param name="controlSize">The <see cref="T:Size" /> of the control.</param>
    /// <param name="controlPoint">The position of the control.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    private Size AboveChildSize(Size controlSize, Point controlPoint) => new Size(controlSize.Width, PopupHelper.Min0(controlPoint.Y - 2.0, this.MaxDropDownHeight));

    /// <summary>
    /// Computes the <see cref="T:Size" /> of <see cref="P:Popup.Child" /> if displayed below the control.
    /// </summary>
    /// <param name="displaySize">The <see cref="T:Size" /> not covered by the SIP.</param>
    /// <param name="controlSize">The <see cref="T:Size" /> of the control.</param>
    /// <param name="controlPoint">The position of the control.</param>
    /// <returns>The <see cref="T:Size" />.</returns>
    private Size BelowChildSize(Size displaySize, Size controlSize, Point controlPoint) => new Size(controlSize.Width, PopupHelper.Min0(displaySize.Height - controlSize.Height - controlPoint.Y - 2.0, this.MaxDropDownHeight));

    /// <summary>
    /// The position of <see cref="P:Popup.Child" /> if displayed above the control.
    /// </summary>
    /// <param name="margin">The control's margin.</param>
    /// <returns>The position.</returns>
    private Point AboveChildPoint(Thickness margin) => new Point(margin.Left, margin.Top - this.PopupChild.ActualHeight - 2.0 + this.Parent.BorderThickness.Bottom * 2.0);

    /// <summary>
    /// The position of <see cref="P:Popup.Child" /> if displayed below the control.
    /// </summary>
    /// <param name="margin">The control's margin.</param>
    /// <param name="controlSize">The <see cref="T:Size" /> of the control.</param>
    /// <returns>The position.</returns>
    private Point BelowChildPoint(Thickness margin, Size controlSize) => new Point(margin.Left, margin.Top + controlSize.Height + 2.0 - this.Parent.BorderThickness.Top * 2.0);

    /// <summary>
    /// Arrange the popup.
    /// <param name="finalSize">The <see cref="T:Size" /> from <see cref="M:AutoCompleteBox.ArrangeOverride" />.</param>
    /// </summary>
    public void Arrange(Size? finalSize)
    {
      PhoneApplicationFrame phoneApplicationFrame;
      if (this.Popup == null || this.PopupChild == null || Application.Current == null || this.OutsidePopupCanvas == null || Application.Current.Host == null || Application.Current.Host.Content == null || !PhoneHelper.TryGetPhoneApplicationFrame(out phoneApplicationFrame))
        return;
      Size usefulSize = phoneApplicationFrame.GetUsefulSize();
      Thickness margin = this.GetMargin();
      Size controlSize = this.GetControlSize(margin, finalSize);
      MatrixTransform controlMatrixTransform = this.GetControlMatrixTransform();
      if (controlMatrixTransform == null)
        return;
      Point point1 = PopupHelper.MatrixTransformPoint(controlMatrixTransform, margin);
      Size sipUncoveredSize = phoneApplicationFrame.GetSipUncoveredSize();
      bool flag = PopupHelper.IsChildAbove(sipUncoveredSize, controlSize, point1);
      Size size = flag ? this.AboveChildSize(controlSize, point1) : this.BelowChildSize(sipUncoveredSize, controlSize, point1);
      if (usefulSize.Width == 0.0 || usefulSize.Height == 0.0 || size.Height == 0.0)
        return;
      Point point2 = flag ? this.AboveChildPoint(margin) : this.BelowChildPoint(margin, controlSize);
      this.Popup.HorizontalOffset = 0.0;
      this.Popup.VerticalOffset = 0.0;
      this.PopupChild.Width = this.PopupChild.MaxWidth = this.PopupChild.MinWidth = controlSize.Width;
      this.PopupChild.MinHeight = 0.0;
      this.PopupChild.MaxHeight = size.Height;
      this.PopupChild.HorizontalAlignment = (HorizontalAlignment) 0;
      this.PopupChild.VerticalAlignment = (VerticalAlignment) 0;
      Canvas.SetLeft((UIElement) this.PopupChild, point2.X);
      Canvas.SetTop((UIElement) this.PopupChild, point2.Y);
      ((FrameworkElement) this.OutsidePopupCanvas).Width = controlSize.Width;
      ((FrameworkElement) this.OutsidePopupCanvas).Height = usefulSize.Height;
      Matrix outputMatrix;
      controlMatrixTransform.Matrix.Invert(out outputMatrix);
      controlMatrixTransform.Matrix = outputMatrix;
      ((UIElement) this.OutsidePopupCanvas).RenderTransform = (Transform) controlMatrixTransform;
    }

    /// <summary>Fires the Closed event.</summary>
    /// <param name="e">The event data.</param>
    private void OnClosed(EventArgs e)
    {
      EventHandler closed = this.Closed;
      if (closed == null)
        return;
      closed((object) this, e);
    }

    /// <summary>
    /// Actually closes the popup after the VSM state animation completes.
    /// </summary>
    /// <param name="sender">Event source.</param>
    /// <param name="e">Event arguments.</param>
    private void OnPopupClosedStateChanged(object sender, VisualStateChangedEventArgs e)
    {
      if (e == null || e.NewState == null || !(e.NewState.Name == "PopupClosed"))
        return;
      if (this.Popup != null)
        this.Popup.IsOpen = false;
      this.OnClosed(EventArgs.Empty);
    }

    /// <summary>
    /// Should be called by the parent control before the base
    /// OnApplyTemplate method is called.
    /// </summary>
    public void BeforeOnApplyTemplate()
    {
      if (this.UsesClosingVisualState)
      {
        VisualStateGroup visualStateGroup = VisualStates.TryGetVisualStateGroup((DependencyObject) this.Parent, "PopupStates");
        if (null != visualStateGroup)
        {
          visualStateGroup.CurrentStateChanged -= new EventHandler<VisualStateChangedEventArgs>(this.OnPopupClosedStateChanged);
          this.UsesClosingVisualState = false;
        }
      }
      if (this.Popup == null)
        return;
      this.Popup.Closed -= new EventHandler(this.Popup_Closed);
    }

    /// <summary>
    /// Should be called by the parent control after the base
    /// OnApplyTemplate method is called.
    /// </summary>
    public void AfterOnApplyTemplate()
    {
      if (this.Popup != null)
        this.Popup.Closed += new EventHandler(this.Popup_Closed);
      VisualStateGroup visualStateGroup = VisualStates.TryGetVisualStateGroup((DependencyObject) this.Parent, "PopupStates");
      if (null != visualStateGroup)
      {
        visualStateGroup.CurrentStateChanged += new EventHandler<VisualStateChangedEventArgs>(this.OnPopupClosedStateChanged);
        this.UsesClosingVisualState = true;
      }
      if (this.Popup == null)
        return;
      this.PopupChild = this.Popup.Child as FrameworkElement;
      if (this.PopupChild != null && !this._hasControlLoaded)
      {
        this._hasControlLoaded = true;
        this.PopupChildCanvas = new Canvas();
        this.Popup.Child = (UIElement) this.PopupChildCanvas;
        this.OutsidePopupCanvas = new Canvas();
        ((Panel) this.OutsidePopupCanvas).Background = (Brush) new SolidColorBrush(Colors.Transparent);
        ((UIElement) this.OutsidePopupCanvas).MouseLeftButtonDown += new MouseButtonEventHandler(this.OutsidePopup_MouseLeftButtonDown);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) this.PopupChildCanvas).Children).Add((UIElement) this.OutsidePopupCanvas);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) this.PopupChildCanvas).Children).Add((UIElement) this.PopupChild);
        ((UIElement) this.PopupChild).GotFocus += new RoutedEventHandler(this.PopupChild_GotFocus);
        ((UIElement) this.PopupChild).LostFocus += new RoutedEventHandler(this.PopupChild_LostFocus);
        ((UIElement) this.PopupChild).MouseEnter += new MouseEventHandler(this.PopupChild_MouseEnter);
        ((UIElement) this.PopupChild).MouseLeave += new MouseEventHandler(this.PopupChild_MouseLeave);
        this.PopupChild.SizeChanged += new SizeChangedEventHandler(this.PopupChild_SizeChanged);
      }
    }

    /// <summary>The size of the popup child has changed.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void PopupChild_SizeChanged(object sender, SizeChangedEventArgs e) => this.Arrange(new Size?());

    /// <summary>The mouse has clicked outside of the popup.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void OutsidePopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (this.Popup == null)
        return;
      this.Popup.IsOpen = false;
    }

    /// <summary>
    /// Connected to the Popup Closed event and fires the Closed event.
    /// </summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void Popup_Closed(object sender, EventArgs e) => this.OnClosed(EventArgs.Empty);

    /// <summary>
    /// Connected to several events that indicate that the FocusChanged
    /// event should bubble up to the parent control.
    /// </summary>
    /// <param name="e">The event data.</param>
    private void OnFocusChanged(EventArgs e)
    {
      EventHandler focusChanged = this.FocusChanged;
      if (focusChanged == null)
        return;
      focusChanged((object) this, e);
    }

    /// <summary>Fires the UpdateVisualStates event.</summary>
    /// <param name="e">The event data.</param>
    private void OnUpdateVisualStates(EventArgs e)
    {
      EventHandler updateVisualStates = this.UpdateVisualStates;
      if (updateVisualStates == null)
        return;
      updateVisualStates((object) this, e);
    }

    /// <summary>The popup child has received focus.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void PopupChild_GotFocus(object sender, RoutedEventArgs e) => this.OnFocusChanged(EventArgs.Empty);

    /// <summary>The popup child has lost focus.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void PopupChild_LostFocus(object sender, RoutedEventArgs e) => this.OnFocusChanged(EventArgs.Empty);

    /// <summary>The popup child has had the mouse enter its bounds.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void PopupChild_MouseEnter(object sender, MouseEventArgs e) => this.OnUpdateVisualStates(EventArgs.Empty);

    /// <summary>The mouse has left the popup child's bounds.</summary>
    /// <param name="sender">The source object.</param>
    /// <param name="e">The event data.</param>
    private void PopupChild_MouseLeave(object sender, MouseEventArgs e) => this.OnUpdateVisualStates(EventArgs.Empty);
  }
}
