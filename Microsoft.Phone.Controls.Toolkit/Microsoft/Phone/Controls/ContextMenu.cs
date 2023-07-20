// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ContextMenu
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a pop-up menu that enables a control to expose functionality that is specific to the context of the control.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "Closed")]
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "OpenReversed")]
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "OpenLandscapeReversed")]
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "Open")]
  [TemplateVisualState(GroupName = "VisibilityStates", Name = "OpenLandscape")]
  [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Code flow is reasonably clear.")]
  public class ContextMenu : MenuBase
  {
    /// <summary>Width of the Menu in Landscape</summary>
    private const double LandscapeWidth = 480.0;
    /// <summary>Width of the system tray in Landscape Mode</summary>
    private const double SystemTrayLandscapeWidth = 72.0;
    /// <summary>Width of the application bar in Landscape mode</summary>
    private const double ApplicationBarLandscapeWidth = 72.0;
    /// <summary>Width of the borders around the menu</summary>
    private const double TotalBorderWidth = 8.0;
    /// <summary>Visibility state group.</summary>
    private const string VisibilityGroupName = "VisibilityStates";
    /// <summary>Open visibility state.</summary>
    private const string OpenVisibilityStateName = "Open";
    /// <summary>Open state when the context menu grows upwards.</summary>
    private const string OpenReversedVisibilityStateName = "OpenReversed";
    /// <summary>Closed visibility state.</summary>
    private const string ClosedVisibilityStateName = "Closed";
    /// <summary>Open landscape visibility state.</summary>
    private const string OpenLandscapeVisibilityStateName = "OpenLandscape";
    /// <summary>
    /// Open landscape state when the context menu grows leftwards.
    /// </summary>
    private const string OpenLandscapeReversedVisibilityStateName = "OpenLandscapeReversed";
    /// <summary>The panel that holds all the content</summary>
    private StackPanel _outerPanel;
    /// <summary>The grid that contains the item presenter</summary>
    private Grid _innerGrid;
    /// <summary>
    /// Stores a reference to the PhoneApplicationPage that contains the owning object.
    /// </summary>
    private PhoneApplicationPage _page;
    /// <summary>
    /// Stores a reference to a list of ApplicationBarIconButtons for which the Click event is being handled.
    /// </summary>
    private readonly List<ApplicationBarIconButton> _applicationBarIconButtons = new List<ApplicationBarIconButton>();
    /// <summary>
    /// Stores a reference to the Storyboard used to animate the background resize.
    /// </summary>
    private Storyboard _backgroundResizeStoryboard;
    /// <summary>
    /// Stores a reference to the Storyboard used to animate the ContextMenu open.
    /// </summary>
    private List<Storyboard> _openingStoryboard;
    /// <summary>
    /// Tracks whether the Storyboard used to animate the ContextMenu open is active.
    /// </summary>
    private bool _openingStoryboardPlaying;
    /// <summary>
    /// Tracks the threshold for releasing contact during the ContextMenu open animation.
    /// </summary>
    private DateTime _openingStoryboardReleaseThreshold;
    /// <summary>Stores a reference to the current root visual.</summary>
    private PhoneApplicationFrame _rootVisual;
    /// <summary>
    /// Stores a reference to the object that owns the ContextMenu.
    /// </summary>
    private DependencyObject _owner;
    /// <summary>Stores a reference to the current Popup.</summary>
    private Popup _popup;
    /// <summary>Stores a reference to the current overlay.</summary>
    private Panel _overlay;
    /// <summary>
    /// Stores a reference to the current Popup alignment point.
    /// </summary>
    private Point _popupAlignmentPoint;
    /// <summary>
    /// Stores a value indicating whether the IsOpen property is being updated by ContextMenu.
    /// </summary>
    private bool _settingIsOpen;
    /// <summary>
    /// Whether the opening animation is reversed (bottom to top or right to left).
    /// </summary>
    private bool _reversed;
    /// <summary>Identifies the IsZoomEnabled dependency property.</summary>
    public static readonly DependencyProperty IsZoomEnabledProperty = DependencyProperty.Register(nameof (IsZoomEnabled), typeof (bool), typeof (ContextMenu), new PropertyMetadata((object) true));
    /// <summary>Identifies the IsFadeEnabled dependency property.</summary>
    public static readonly DependencyProperty IsFadeEnabledProperty = DependencyProperty.Register(nameof (IsFadeEnabled), typeof (bool), typeof (ContextMenu), new PropertyMetadata((object) true));
    /// <summary>Identifies the VerticalOffset dependency property.</summary>
    public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register(nameof (VerticalOffset), typeof (double), typeof (ContextMenu), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(ContextMenu.OnVerticalOffsetChanged)));
    /// <summary>Identifies the IsOpen dependency property.</summary>
    public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof (IsOpen), typeof (bool), typeof (ContextMenu), new PropertyMetadata((object) false, new PropertyChangedCallback(ContextMenu.OnIsOpenChanged)));
    /// <summary>Identifies the RegionOfInterest dependency property.</summary>
    public static readonly DependencyProperty RegionOfInterestProperty = DependencyProperty.Register(nameof (RegionOfInterest), typeof (Rect?), typeof (ContextMenu), (PropertyMetadata) null);
    /// <summary>
    /// Identifies the ApplicationBarMirror dependency property.
    /// </summary>
    private static readonly DependencyProperty ApplicationBarMirrorProperty = DependencyProperty.Register("ApplicationBarMirror", typeof (IApplicationBar), typeof (ContextMenu), new PropertyMetadata(new PropertyChangedCallback(ContextMenu.OnApplicationBarMirrorChanged)));

    /// <summary>Gets or sets the owning object for the ContextMenu.</summary>
    public DependencyObject Owner
    {
      get => this._owner;
      internal set
      {
        if (null != this._owner)
        {
          FrameworkElement owner = this._owner as FrameworkElement;
          if (null != owner)
          {
            ((UIElement) owner).Hold -= new EventHandler<GestureEventArgs>(this.OnOwnerHold);
            owner.Loaded -= new RoutedEventHandler(this.OnOwnerLoaded);
            owner.Unloaded -= new RoutedEventHandler(this.OnOwnerUnloaded);
            this.OnOwnerUnloaded((object) null, (RoutedEventArgs) null);
          }
        }
        this._owner = value;
        if (null == this._owner)
          return;
        FrameworkElement owner1 = this._owner as FrameworkElement;
        if (null != owner1)
        {
          ((UIElement) owner1).Hold += new EventHandler<GestureEventArgs>(this.OnOwnerHold);
          owner1.Loaded += new RoutedEventHandler(this.OnOwnerLoaded);
          owner1.Unloaded += new RoutedEventHandler(this.OnOwnerUnloaded);
          DependencyObject dependencyObject = (DependencyObject) owner1;
          while (dependencyObject != null)
          {
            dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            if (dependencyObject != null && dependencyObject == this._rootVisual)
            {
              this.OnOwnerLoaded((object) null, (RoutedEventArgs) null);
              break;
            }
          }
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the background will zoom out when the ContextMenu is open.
    /// </summary>
    public bool IsZoomEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(ContextMenu.IsZoomEnabledProperty);
      set => ((DependencyObject) this).SetValue(ContextMenu.IsZoomEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the background will fade when the ContextMenu is open.
    /// IsZoomEnabled must be true for this value to take effect.
    /// </summary>
    public bool IsFadeEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(ContextMenu.IsFadeEnabledProperty);
      set => ((DependencyObject) this).SetValue(ContextMenu.IsFadeEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the vertical distance between the target origin and the popup alignment point.
    /// </summary>
    [TypeConverter(typeof (LengthConverter))]
    public double VerticalOffset
    {
      get => (double) ((DependencyObject) this).GetValue(ContextMenu.VerticalOffsetProperty);
      set => ((DependencyObject) this).SetValue(ContextMenu.VerticalOffsetProperty, (object) value);
    }

    /// <summary>
    /// Handles changes to the VerticalOffset DependencyProperty.
    /// </summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnVerticalOffsetChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ContextMenu) o).UpdateContextMenuPlacement();
    }

    /// <summary>
    /// Gets or sets a value indicating whether the ContextMenu is visible.
    /// </summary>
    public bool IsOpen
    {
      get => (bool) ((DependencyObject) this).GetValue(ContextMenu.IsOpenProperty);
      set => ((DependencyObject) this).SetValue(ContextMenu.IsOpenProperty, (object) value);
    }

    /// <summary>Handles changes to the IsOpen DependencyProperty.</summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) => ((ContextMenu) o).OnIsOpenChanged((bool) e.NewValue);

    /// <summary>Handles changes to the IsOpen property.</summary>
    /// <param name="newValue">New value.</param>
    private void OnIsOpenChanged(bool newValue)
    {
      if (this._settingIsOpen)
        return;
      if (newValue)
        this.OpenPopup(new Point(-1.0, -1.0));
      else
        this.ClosePopup();
    }

    /// <summary>
    /// Gets or sets the region of interest expressed in the coordinate system of the root visual.
    /// A context menu will try to position itself outside the region of interest.
    /// If null, the owner's bounding box is considered the region of interest.
    /// </summary>
    public Rect? RegionOfInterest
    {
      get => (Rect?) ((DependencyObject) this).GetValue(ContextMenu.RegionOfInterestProperty);
      set => ((DependencyObject) this).SetValue(ContextMenu.RegionOfInterestProperty, (object) value);
    }

    /// <summary>
    /// Occurs when a particular instance of a ContextMenu opens.
    /// </summary>
    public event RoutedEventHandler Opened;

    /// <summary>Called when the Opened event occurs.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnOpened(RoutedEventArgs e)
    {
      this.UpdateContextMenuPlacement();
      this.SetRenderTransform();
      this.UpdateVisualStates(true);
      RoutedEventHandler opened = this.Opened;
      if (null == opened)
        return;
      opened((object) this, e);
    }

    private void SetRenderTransform()
    {
      if (DesignerProperties.IsInDesignTool || this._rootVisual.Orientation.IsPortrait())
      {
        double num = this._popupAlignmentPoint.X / ((FrameworkElement) this).Width;
        if (this._outerPanel != null)
          ((UIElement) this._outerPanel).RenderTransformOrigin = new Point(num, 0.0);
        if (this._innerGrid == null)
          return;
        ((UIElement) this._innerGrid).RenderTransformOrigin = new Point(0.0, this._reversed ? 1.0 : 0.0);
      }
      else
      {
        if (this._outerPanel != null)
          ((UIElement) this._outerPanel).RenderTransformOrigin = new Point(0.0, 0.5);
        if (this._innerGrid != null)
          ((UIElement) this._innerGrid).RenderTransformOrigin = new Point(this._reversed ? 1.0 : 0.0, 0.0);
      }
    }

    /// <summary>
    /// Occurs when a particular instance of a ContextMenu closes.
    /// </summary>
    public event RoutedEventHandler Closed;

    /// <summary>Called when the Closed event occurs.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnClosed(RoutedEventArgs e)
    {
      this.UpdateVisualStates(true);
      RoutedEventHandler closed = this.Closed;
      if (null == closed)
        return;
      closed((object) this, e);
    }

    /// <summary>Initializes a new instance of the ContextMenu class.</summary>
    public ContextMenu()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (ContextMenu);
      this._openingStoryboard = new List<Storyboard>();
      if (null == Application.Current.RootVisual)
        ((FrameworkElement) this).LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
      else
        this.InitializeRootVisual();
    }

    /// <summary>Called when a new Template is applied.</summary>
    public virtual void OnApplyTemplate()
    {
      if (null != this._openingStoryboard)
      {
        foreach (Timeline timeline in this._openingStoryboard)
          timeline.Completed -= new EventHandler(this.OnStoryboardCompleted);
        this._openingStoryboard.Clear();
      }
      this._openingStoryboardPlaying = false;
      ((FrameworkElement) this).OnApplyTemplate();
      this.SetDefaultStyle();
      FrameworkElement child = VisualTreeHelper.GetChild((DependencyObject) this, 0) as FrameworkElement;
      if (null != child)
      {
        foreach (VisualStateGroup visualStateGroup in (IEnumerable) VisualStateManager.GetVisualStateGroups(child))
        {
          if ("VisibilityStates" == visualStateGroup.Name)
          {
            foreach (VisualState state in (IEnumerable) visualStateGroup.States)
            {
              if (("Open" == state.Name || "OpenLandscape" == state.Name || "OpenReversed" == state.Name || "OpenLandscapeReversed" == state.Name) && null != state.Storyboard)
              {
                this._openingStoryboard.Add(state.Storyboard);
                ((Timeline) state.Storyboard).Completed += new EventHandler(this.OnStoryboardCompleted);
              }
            }
          }
        }
      }
      this._outerPanel = ((Control) this).GetTemplateChild("OuterPanel") as StackPanel;
      this._innerGrid = ((Control) this).GetTemplateChild("InnerGrid") as Grid;
      bool flag = DesignerProperties.IsInDesignTool || this._rootVisual.Orientation.IsPortrait();
      this.SetRenderTransform();
      if (!this.IsOpen)
        return;
      if (null != this._innerGrid)
        ((FrameworkElement) this._innerGrid).MinHeight = flag ? 0.0 : ((FrameworkElement) this._rootVisual).ActualWidth;
      this.UpdateVisualStates(true);
    }

    /// <summary>Set up the background and border default styles</summary>
    private void SetDefaultStyle()
    {
      SolidColorBrush solidColorBrush1;
      SolidColorBrush solidColorBrush2;
      if (DesignerProperties.IsInDesignTool || ((FrameworkElement) this).Resources.IsDarkThemeActive())
      {
        solidColorBrush1 = new SolidColorBrush(Colors.White);
        solidColorBrush2 = new SolidColorBrush(Colors.Black);
      }
      else
      {
        solidColorBrush1 = new SolidColorBrush(Colors.Black);
        solidColorBrush2 = new SolidColorBrush(Colors.White);
      }
      Style style = new Style(typeof (ContextMenu));
      Setter setter1 = new Setter(Control.BackgroundProperty, (object) solidColorBrush1);
      Setter setter2 = new Setter(Control.BorderBrushProperty, (object) solidColorBrush2);
      if (null == ((FrameworkElement) this).Style)
      {
        ((PresentationFrameworkCollection<SetterBase>) style.Setters).Add((SetterBase) setter1);
        ((PresentationFrameworkCollection<SetterBase>) style.Setters).Add((SetterBase) setter2);
      }
      else
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (Setter setter3 in (PresentationFrameworkCollection<SetterBase>) ((FrameworkElement) this).Style.Setters)
        {
          if (setter3.Property == Control.BackgroundProperty)
            flag1 = true;
          else if (setter3.Property == Control.BorderBrushProperty)
            flag2 = true;
          ((PresentationFrameworkCollection<SetterBase>) style.Setters).Add((SetterBase) new Setter(setter3.Property, setter3.Value));
        }
        if (!flag1)
          ((PresentationFrameworkCollection<SetterBase>) style.Setters).Add((SetterBase) setter1);
        if (!flag2)
          ((PresentationFrameworkCollection<SetterBase>) style.Setters).Add((SetterBase) setter2);
      }
      ((FrameworkElement) this).Style = style;
    }

    /// <summary>
    /// Handles the Completed event of the opening Storyboard.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnStoryboardCompleted(object sender, EventArgs e) => this._openingStoryboardPlaying = false;

    /// <summary>
    /// Uses VisualStateManager to go to the appropriate visual state.
    /// </summary>
    /// <param name="useTransitions">true to use a System.Windows.VisualTransition to
    /// transition between states; otherwise, false.</param>
    private void UpdateVisualStates(bool useTransitions)
    {
      string str;
      if (this.IsOpen)
      {
        if (null != this._openingStoryboard)
        {
          this._openingStoryboardPlaying = true;
          this._openingStoryboardReleaseThreshold = DateTime.UtcNow.AddSeconds(0.3);
        }
        if (this._rootVisual != null && this._rootVisual.Orientation.IsPortrait())
        {
          if (this._outerPanel != null)
            this._outerPanel.Orientation = (Orientation) 0;
          str = this._reversed ? "OpenReversed" : "Open";
        }
        else
        {
          if (this._outerPanel != null)
            this._outerPanel.Orientation = (Orientation) 1;
          str = this._reversed ? "OpenLandscapeReversed" : "OpenLandscape";
        }
        if (null != this._backgroundResizeStoryboard)
          this._backgroundResizeStoryboard.Begin();
      }
      else
        str = "Closed";
      VisualStateManager.GoToState((Control) this, str, useTransitions);
    }

    /// <summary>
    /// Whether the position is on the right half of the screen.
    /// Only supports landscape mode.
    /// This is used to determine which side of the screen the context menu will display on.
    /// </summary>
    /// <param name="position">Position to check for</param>
    private bool PositionIsOnScreenRight(double position) => 18 == this._rootVisual.Orientation ? position > ((FrameworkElement) this._rootVisual).ActualHeight / 2.0 : position < ((FrameworkElement) this._rootVisual).ActualHeight / 2.0;

    /// <summary>Called when the left mouse button is pressed.</summary>
    /// <param name="e">The event data for the MouseLeftButtonDown event.</param>
    protected virtual void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      e.Handled = true;
      ((Control) this).OnMouseLeftButtonDown(e);
    }

    /// <summary>Responds to the KeyDown event.</summary>
    /// <param name="e">The event data for the KeyDown event.</param>
    protected virtual void OnKeyDown(KeyEventArgs e)
    {
      Key key = e != null ? e.Key : throw new ArgumentNullException(nameof (e));
      if (key != 8)
      {
        switch (key - 15)
        {
          case 0:
            this.FocusNextItem(false);
            e.Handled = true;
            break;
          case 2:
            this.FocusNextItem(true);
            e.Handled = true;
            break;
        }
      }
      else
      {
        this.ClosePopup();
        e.Handled = true;
      }
      ((Control) this).OnKeyDown(e);
    }

    /// <summary>
    /// Handles the LayoutUpdated event to capture Application.Current.RootVisual.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnLayoutUpdated(object sender, EventArgs e)
    {
      if (null == Application.Current.RootVisual)
        return;
      this.InitializeRootVisual();
      ((FrameworkElement) this).LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
    }

    /// <summary>
    /// Handles the ManipulationCompleted event for the RootVisual.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnRootVisualManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (!this._openingStoryboardPlaying || !(DateTime.UtcNow <= this._openingStoryboardReleaseThreshold))
        return;
      this.IsOpen = false;
    }

    /// <summary>Handles the Hold event for the owning element.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnOwnerHold(object sender, GestureEventArgs e)
    {
      if (this.IsOpen)
        return;
      this.OpenPopup(e.GetPosition((UIElement) null));
      e.Handled = true;
    }

    /// <summary>
    /// Handles changes to the ApplicationBarMirror DependencyProperty.
    /// </summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnApplicationBarMirrorChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ((ContextMenu) o).OnApplicationBarMirrorChanged((IApplicationBar) e.OldValue, (IApplicationBar) e.NewValue);
    }

    /// <summary>Handles changes to the ApplicationBarMirror property.</summary>
    /// <param name="oldValue">Old value.</param>
    /// <param name="newValue">New value.</param>
    private void OnApplicationBarMirrorChanged(IApplicationBar oldValue, IApplicationBar newValue)
    {
      if (null != oldValue)
        oldValue.StateChanged -= new EventHandler<ApplicationBarStateChangedEventArgs>(this.OnEventThatClosesContextMenu);
      if (null == newValue)
        return;
      newValue.StateChanged += new EventHandler<ApplicationBarStateChangedEventArgs>(this.OnEventThatClosesContextMenu);
    }

    /// <summary>
    /// Handles an event which should close an open ContextMenu.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnEventThatClosesContextMenu(object sender, EventArgs e) => this.IsOpen = false;

    /// <summary>Handles the Loaded event of the Owner.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnOwnerLoaded(object sender, RoutedEventArgs e)
    {
      if (null != this._page)
        return;
      this.InitializeRootVisual();
      if (null != this._rootVisual)
      {
        this._page = ((ContentControl) this._rootVisual).Content as PhoneApplicationPage;
        if (this._page != null)
        {
          this._page.BackKeyPress += new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
          ((FrameworkElement) this).SetBinding(ContextMenu.ApplicationBarMirrorProperty, new Binding()
          {
            Source = (object) this._page,
            Path = new PropertyPath("ApplicationBar", new object[0])
          });
        }
      }
    }

    /// <summary>Handles the Unloaded event of the Owner.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnOwnerUnloaded(object sender, RoutedEventArgs e)
    {
      if (null != this._rootVisual)
      {
        ((UIElement) this._rootVisual).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnRootVisualManipulationCompleted);
        this._rootVisual.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.OnEventThatClosesContextMenu);
      }
      if (this._page == null)
        return;
      this._page.BackKeyPress -= new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
      ((DependencyObject) this).ClearValue(ContextMenu.ApplicationBarMirrorProperty);
      this._page = (PhoneApplicationPage) null;
    }

    /// <summary>
    /// Handles the BackKeyPress of the containing PhoneApplicationPage.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnPageBackKeyPress(object sender, CancelEventArgs e)
    {
      if (!this.IsOpen)
        return;
      this.IsOpen = false;
      e.Cancel = true;
    }

    /// <summary>
    /// Calls TransformToVisual on the specified element for the specified visual, suppressing the ArgumentException that can occur in some cases.
    /// </summary>
    /// <param name="element">Element on which to call TransformToVisual.</param>
    /// <param name="visual">Visual to pass to the call to TransformToVisual.</param>
    /// <returns>Resulting GeneralTransform object.</returns>
    private static GeneralTransform SafeTransformToVisual(UIElement element, UIElement visual)
    {
      GeneralTransform visual1;
      try
      {
        visual1 = element.TransformToVisual(visual);
      }
      catch (ArgumentException ex)
      {
        visual1 = (GeneralTransform) new TranslateTransform();
      }
      return visual1;
    }

    /// <summary>
    /// Initialize the _rootVisual property (if possible and not already done).
    /// </summary>
    private void InitializeRootVisual()
    {
      if (null != this._rootVisual)
        return;
      this._rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
      if (null != this._rootVisual)
      {
        ((UIElement) this._rootVisual).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.OnRootVisualManipulationCompleted);
        ((UIElement) this._rootVisual).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnRootVisualManipulationCompleted);
        this._rootVisual.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.OnEventThatClosesContextMenu);
        this._rootVisual.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.OnEventThatClosesContextMenu);
      }
    }

    /// <summary>Sets focus to the next item in the ContextMenu.</summary>
    /// <param name="down">True to move the focus down; false to move it up.</param>
    private void FocusNextItem(bool down)
    {
      int count = ((PresentationFrameworkCollection<object>) this.Items).Count;
      int num1 = down ? -1 : count;
      if (FocusManager.GetFocusedElement() is MenuItem focusedElement && this == focusedElement.ParentMenuBase)
        num1 = this.ItemContainerGenerator.IndexFromContainer((DependencyObject) focusedElement);
      int num2 = num1;
      MenuItem menuItem;
      do
      {
        num2 = (num2 + count + (down ? 1 : -1)) % count;
        menuItem = this.ItemContainerGenerator.ContainerFromIndex(num2) as MenuItem;
      }
      while ((null == menuItem || !((Control) menuItem).IsEnabled || !((Control) menuItem).Focus()) && num2 != num1);
    }

    /// <summary>Called when a child MenuItem is clicked.</summary>
    internal void ChildMenuItemClicked() => this.ClosePopup();

    /// <summary>
    /// Handles the SizeChanged event for the ContextMenu or RootVisual.
    /// </summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnContextMenuOrRootVisualSizeChanged(object sender, SizeChangedEventArgs e) => this.UpdateContextMenuPlacement();

    /// <summary>Handles the MouseButtonUp events for the overlay.</summary>
    /// <param name="sender">Source of the event.</param>
    /// <param name="e">Event arguments.</param>
    private void OnOverlayMouseButtonUp(object sender, MouseButtonEventArgs e)
    {
      if (!(VisualTreeHelper.FindElementsInHostCoordinates(((MouseEventArgs) e).GetPosition((UIElement) null), (UIElement) this._rootVisual) as List<UIElement>).Contains((UIElement) this))
        this.ClosePopup();
      e.Handled = true;
    }

    /// <summary>
    /// Adjust the position (Y) of ContextMenu for Portrait Mode.
    /// </summary>
    private double AdjustContextMenuPositionForPortraitMode(
      Rect bounds,
      double roiY,
      double roiHeight,
      ref bool reversed)
    {
      double num1 = 0.0;
      bool flag = false;
      double num2 = bounds.Bottom - ((FrameworkElement) this).ActualHeight;
      double num3 = bounds.Top + ((FrameworkElement) this).ActualHeight;
      if (bounds.Height <= ((FrameworkElement) this).ActualHeight)
        flag = true;
      else if (roiY + roiHeight <= num2)
      {
        num1 = roiY + roiHeight;
        reversed = false;
      }
      else if (roiY >= num3)
      {
        num1 = roiY - ((FrameworkElement) this).ActualHeight;
        reversed = true;
      }
      else if (this._popupAlignmentPoint.Y >= 0.0)
      {
        num1 = this._popupAlignmentPoint.Y;
        if (num1 <= num2)
          reversed = false;
        else if (num1 >= num3)
        {
          num1 -= ((FrameworkElement) this).ActualHeight;
          reversed = true;
        }
        else
          flag = true;
      }
      else
        flag = true;
      if (flag)
      {
        num1 = num2;
        reversed = true;
        if (num1 <= bounds.Top)
        {
          num1 = bounds.Top;
          reversed = false;
        }
      }
      return num1;
    }

    /// <summary>
    /// Updates the location and size of the Popup and overlay.
    /// </summary>
    private void UpdateContextMenuPlacement()
    {
      if (this._rootVisual == null || null == this._overlay)
        return;
      Point point = new Point(this._popupAlignmentPoint.X, this._popupAlignmentPoint.Y);
      bool flag = this._rootVisual.Orientation.IsPortrait();
      double num1 = flag ? ((FrameworkElement) this._rootVisual).ActualWidth : ((FrameworkElement) this._rootVisual).ActualHeight;
      double num2 = flag ? ((FrameworkElement) this._rootVisual).ActualHeight : ((FrameworkElement) this._rootVisual).ActualWidth;
      Rect bounds = new Rect(0.0, 0.0, num1, num2);
      if (this._page != null)
        bounds = ContextMenu.SafeTransformToVisual((UIElement) this._page, (UIElement) this._rootVisual).TransformBounds(new Rect(0.0, 0.0, ((FrameworkElement) this._page).ActualWidth, ((FrameworkElement) this._page).ActualHeight));
      if (flag && null != this._rootVisual)
      {
        Rect? regionOfInterest = this.RegionOfInterest;
        double y;
        double roiHeight;
        if (regionOfInterest.HasValue)
        {
          regionOfInterest = this.RegionOfInterest;
          Rect rect = regionOfInterest.Value;
          y = rect.Y;
          regionOfInterest = this.RegionOfInterest;
          rect = regionOfInterest.Value;
          roiHeight = rect.Height;
        }
        else if (this.Owner is FrameworkElement)
        {
          FrameworkElement owner = (FrameworkElement) this.Owner;
          y = ((UIElement) owner).TransformToVisual((UIElement) this._rootVisual).Transform(new Point(0.0, 0.0)).Y;
          roiHeight = owner.ActualHeight;
        }
        else
        {
          y = this._popupAlignmentPoint.Y;
          roiHeight = 0.0;
        }
        point.Y = this.AdjustContextMenuPositionForPortraitMode(bounds, y, roiHeight, ref this._reversed);
      }
      double x = point.X;
      double position = point.Y + this.VerticalOffset;
      double val1;
      if (flag)
      {
        val1 = bounds.Left;
        ((FrameworkElement) this).Width = bounds.Width;
        if (null != this._innerGrid)
          ((FrameworkElement) this._innerGrid).Width = ((FrameworkElement) this).Width;
      }
      else
      {
        if (this.PositionIsOnScreenRight(position))
        {
          ((FrameworkElement) this).Width = SystemTray.IsVisible ? 408.0 : 480.0;
          val1 = SystemTray.IsVisible ? 72.0 : 0.0;
          this._reversed = true;
        }
        else
        {
          ((FrameworkElement) this).Width = this._page.ApplicationBar == null || !this._page.ApplicationBar.IsVisible ? 480.0 : 408.0;
          val1 = bounds.Width - ((FrameworkElement) this).Width + (SystemTray.IsVisible ? 72.0 : 0.0);
          this._reversed = false;
        }
        if (null != this._innerGrid)
          ((FrameworkElement) this._innerGrid).Width = ((FrameworkElement) this).Width - 8.0;
        position = 0.0;
      }
      Canvas.SetLeft((UIElement) this, Math.Max(val1, 0.0));
      Canvas.SetTop((UIElement) this, position);
      ((FrameworkElement) this._overlay).Width = num1;
      ((FrameworkElement) this._overlay).Height = num2;
    }

    /// <summary>Opens the Popup.</summary>
    /// <param name="position">Position to place the Popup.</param>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Code flow is reasonably clear.")]
    private void OpenPopup(Point position)
    {
      this._popupAlignmentPoint = position;
      this.InitializeRootVisual();
      bool flag = this._rootVisual.Orientation.IsPortrait();
      if (flag)
      {
        if (this._innerGrid != null)
          ((FrameworkElement) this._innerGrid).MinHeight = 0.0;
      }
      else if (this._innerGrid != null)
        ((FrameworkElement) this._innerGrid).MinHeight = ((FrameworkElement) this._rootVisual).ActualWidth;
      Canvas canvas = new Canvas();
      ((Panel) canvas).Background = (Brush) new SolidColorBrush(Colors.Transparent);
      this._overlay = (Panel) canvas;
      ((UIElement) this._overlay).MouseLeftButtonUp += new MouseButtonEventHandler(this.OnOverlayMouseButtonUp);
      if (this.IsZoomEnabled && null != this._rootVisual)
      {
        double num1 = flag ? ((FrameworkElement) this._rootVisual).ActualWidth : ((FrameworkElement) this._rootVisual).ActualHeight;
        double num2 = flag ? ((FrameworkElement) this._rootVisual).ActualHeight : ((FrameworkElement) this._rootVisual).ActualWidth;
        Rectangle rectangle1 = new Rectangle();
        ((FrameworkElement) rectangle1).Width = num1;
        ((FrameworkElement) rectangle1).Height = num2;
        ((Shape) rectangle1).Fill = (Brush) Application.Current.Resources[(object) "PhoneBackgroundBrush"];
        ((UIElement) rectangle1).CacheMode = (CacheMode) new BitmapCache();
        ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Insert(0, (UIElement) rectangle1);
        FrameworkElement owner = this._owner as FrameworkElement;
        if (null != owner)
          ((UIElement) owner).Opacity = 0.0;
        WriteableBitmap writeableBitmap = new WriteableBitmap((int) num1, (int) num2);
        writeableBitmap.Render((UIElement) this._rootVisual, (Transform) null);
        writeableBitmap.Invalidate();
        Transform transform = (Transform) new ScaleTransform()
        {
          CenterX = (num1 / 2.0),
          CenterY = (num2 / 2.0)
        };
        Image image = new Image();
        image.Source = (ImageSource) writeableBitmap;
        ((UIElement) image).RenderTransform = transform;
        ((UIElement) image).CacheMode = (CacheMode) new BitmapCache();
        ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Insert(1, (UIElement) image);
        Rectangle rectangle2 = new Rectangle();
        ((FrameworkElement) rectangle2).Width = num1;
        ((FrameworkElement) rectangle2).Height = num2;
        ((Shape) rectangle2).Fill = (Brush) Application.Current.Resources[(object) "PhoneBackgroundBrush"];
        ((UIElement) rectangle2).Opacity = 0.0;
        ((UIElement) rectangle2).CacheMode = (CacheMode) new BitmapCache();
        UIElement uiElement1 = (UIElement) rectangle2;
        ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Insert(2, uiElement1);
        if (null != owner)
        {
          ((UIElement) this.Owner).Opacity = 1.0;
          Point point = ContextMenu.SafeTransformToVisual((UIElement) owner, (UIElement) this._rootVisual).Transform(new Point(owner.FlowDirection == 1 ? owner.ActualWidth : 0.0, 0.0));
          Rectangle rectangle3 = new Rectangle();
          ((FrameworkElement) rectangle3).Width = owner.ActualWidth;
          ((FrameworkElement) rectangle3).Height = owner.ActualHeight;
          ((Shape) rectangle3).Fill = (Brush) new SolidColorBrush(Colors.Transparent);
          ((UIElement) rectangle3).CacheMode = (CacheMode) new BitmapCache();
          UIElement uiElement2 = (UIElement) rectangle3;
          Canvas.SetLeft(uiElement2, point.X);
          Canvas.SetTop(uiElement2, point.Y);
          ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Insert(3, uiElement2);
          UIElement uiElement3 = (UIElement) new Image()
          {
            Source = (ImageSource) new WriteableBitmap((UIElement) owner, (Transform) null)
          };
          Canvas.SetLeft(uiElement3, point.X);
          Canvas.SetTop(uiElement3, point.Y);
          ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Insert(4, uiElement3);
        }
        double num3 = 1.0;
        double num4 = 0.94;
        TimeSpan timeSpan = TimeSpan.FromSeconds(0.42);
        ExponentialEase exponentialEase = new ExponentialEase();
        ((EasingFunctionBase) exponentialEase).EasingMode = (EasingMode) 2;
        IEasingFunction ieasingFunction = (IEasingFunction) exponentialEase;
        this._backgroundResizeStoryboard = new Storyboard();
        DoubleAnimation doubleAnimation1 = new DoubleAnimation();
        doubleAnimation1.From = new double?(num3);
        doubleAnimation1.To = new double?(num4);
        ((Timeline) doubleAnimation1).Duration = Duration.op_Implicit(timeSpan);
        doubleAnimation1.EasingFunction = ieasingFunction;
        DoubleAnimation doubleAnimation2 = doubleAnimation1;
        Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) transform);
        Storyboard.SetTargetProperty((Timeline) doubleAnimation2, new PropertyPath((object) ScaleTransform.ScaleXProperty));
        ((PresentationFrameworkCollection<Timeline>) this._backgroundResizeStoryboard.Children).Add((Timeline) doubleAnimation2);
        DoubleAnimation doubleAnimation3 = new DoubleAnimation();
        doubleAnimation3.From = new double?(num3);
        doubleAnimation3.To = new double?(num4);
        ((Timeline) doubleAnimation3).Duration = Duration.op_Implicit(timeSpan);
        doubleAnimation3.EasingFunction = ieasingFunction;
        DoubleAnimation doubleAnimation4 = doubleAnimation3;
        Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) transform);
        Storyboard.SetTargetProperty((Timeline) doubleAnimation4, new PropertyPath((object) ScaleTransform.ScaleYProperty));
        ((PresentationFrameworkCollection<Timeline>) this._backgroundResizeStoryboard.Children).Add((Timeline) doubleAnimation4);
        if (this.IsFadeEnabled)
        {
          DoubleAnimation doubleAnimation5 = new DoubleAnimation();
          doubleAnimation5.From = new double?(0.0);
          doubleAnimation5.To = new double?(0.3);
          ((Timeline) doubleAnimation5).Duration = Duration.op_Implicit(timeSpan);
          doubleAnimation5.EasingFunction = ieasingFunction;
          DoubleAnimation doubleAnimation6 = doubleAnimation5;
          Storyboard.SetTarget((Timeline) doubleAnimation6, (DependencyObject) uiElement1);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation6, new PropertyPath((object) UIElement.OpacityProperty));
          ((PresentationFrameworkCollection<Timeline>) this._backgroundResizeStoryboard.Children).Add((Timeline) doubleAnimation6);
        }
      }
      TransformGroup transformGroup = new TransformGroup();
      if (null != this._rootVisual)
      {
        PageOrientation orientation = this._rootVisual.Orientation;
        if (orientation != 18)
        {
          if (orientation == 34)
          {
            ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) new RotateTransform()
            {
              Angle = -90.0
            });
            ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) new TranslateTransform()
            {
              Y = ((FrameworkElement) this._rootVisual).ActualHeight
            });
          }
        }
        else
        {
          ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) new RotateTransform()
          {
            Angle = 90.0
          });
          ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) new TranslateTransform()
          {
            X = ((FrameworkElement) this._rootVisual).ActualWidth
          });
        }
      }
      ((UIElement) this._overlay).RenderTransform = (Transform) transformGroup;
      if (this._page != null && this._page.ApplicationBar != null && null != this._page.ApplicationBar.Buttons)
      {
        foreach (object button in (IEnumerable) this._page.ApplicationBar.Buttons)
        {
          ApplicationBarIconButton applicationBarIconButton = button as ApplicationBarIconButton;
          if (null != applicationBarIconButton)
          {
            applicationBarIconButton.Click += new EventHandler(this.OnEventThatClosesContextMenu);
            this._applicationBarIconButtons.Add(applicationBarIconButton);
          }
        }
      }
      ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Add((UIElement) this);
      this._popup = new Popup()
      {
        Child = (UIElement) this._overlay
      };
      this._popup.Opened += (EventHandler) ((s, e) => this.OnOpened(new RoutedEventArgs()));
      ((FrameworkElement) this).SizeChanged += new SizeChangedEventHandler(this.OnContextMenuOrRootVisualSizeChanged);
      if (null != this._rootVisual)
        ((FrameworkElement) this._rootVisual).SizeChanged += new SizeChangedEventHandler(this.OnContextMenuOrRootVisualSizeChanged);
      this.UpdateContextMenuPlacement();
      if (((DependencyObject) this).ReadLocalValue(FrameworkElement.DataContextProperty) == DependencyProperty.UnsetValue)
      {
        DependencyObject dependencyObject = (DependencyObject) ((object) this.Owner ?? (object) this._rootVisual);
        ((FrameworkElement) this).SetBinding(FrameworkElement.DataContextProperty, new Binding("DataContext")
        {
          Source = (object) dependencyObject
        });
      }
      this._popup.IsOpen = true;
      ((Control) this).Focus();
      this._settingIsOpen = true;
      this.IsOpen = true;
      this._settingIsOpen = false;
    }

    /// <summary>Closes the Popup.</summary>
    private void ClosePopup()
    {
      if (null != this._backgroundResizeStoryboard)
      {
        foreach (DoubleAnimation child in (PresentationFrameworkCollection<Timeline>) this._backgroundResizeStoryboard.Children)
        {
          double num = child.From.Value;
          child.From = child.To;
          child.To = new double?(num);
        }
        Popup popup = this._popup;
        Panel overlay = this._overlay;
        ((Timeline) this._backgroundResizeStoryboard).Completed += (EventHandler) ((param0, param1) =>
        {
          if (null != popup)
          {
            popup.IsOpen = false;
            popup.Child = (UIElement) null;
          }
          if (null == overlay)
            return;
          ((PresentationFrameworkCollection<UIElement>) overlay.Children).Clear();
        });
        this._backgroundResizeStoryboard.Begin();
        this._backgroundResizeStoryboard = (Storyboard) null;
        this._popup = (Popup) null;
        this._overlay = (Panel) null;
      }
      else
      {
        if (null != this._popup)
        {
          this._popup.IsOpen = false;
          this._popup.Child = (UIElement) null;
          this._popup = (Popup) null;
        }
        if (null != this._overlay)
        {
          ((PresentationFrameworkCollection<UIElement>) this._overlay.Children).Clear();
          this._overlay = (Panel) null;
        }
      }
      ((FrameworkElement) this).SizeChanged -= new SizeChangedEventHandler(this.OnContextMenuOrRootVisualSizeChanged);
      if (null != this._rootVisual)
        ((FrameworkElement) this._rootVisual).SizeChanged -= new SizeChangedEventHandler(this.OnContextMenuOrRootVisualSizeChanged);
      foreach (ApplicationBarIconButton applicationBarIconButton in this._applicationBarIconButtons)
        applicationBarIconButton.Click -= new EventHandler(this.OnEventThatClosesContextMenu);
      this._applicationBarIconButtons.Clear();
      this._settingIsOpen = true;
      this.IsOpen = false;
      this._settingIsOpen = false;
      this.OnClosed(new RoutedEventArgs());
    }
  }
}
