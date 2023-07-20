// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.CustomMessageBox
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Microsoft.Phone.Controls
{
  /// <summary>Represents a popup dialog with one or two buttons.</summary>
  /// <QualityBand>Preview</QualityBand>
  [TemplatePart(Name = "CaptionTextBlock", Type = typeof (TextBlock))]
  [TemplatePart(Name = "LeftButton", Type = typeof (ButtonBase))]
  [TemplatePart(Name = "RightButton", Type = typeof (ButtonBase))]
  [TemplatePart(Name = "TitleTextBlock", Type = typeof (TextBlock))]
  [TemplatePart(Name = "MessageTextBlock", Type = typeof (TextBlock))]
  public class CustomMessageBox : ContentControl
  {
    /// <summary>
    /// The height of the system tray in pixels when the page
    /// is in portrait mode.
    /// </summary>
    private const double _systemTrayHeightInPortrait = 32.0;
    /// <summary>
    /// The width of the system tray in pixels when the page
    /// is in landscape mode.
    /// </summary>
    private const double _systemTrayWidthInLandscape = 72.0;
    /// <summary>Title text block template part name.</summary>
    private const string TitleTextBlock = "TitleTextBlock";
    /// <summary>Caption text block template part name.</summary>
    private const string CaptionTextBlock = "CaptionTextBlock";
    /// <summary>Message text block template part name.</summary>
    private const string MessageTextBlock = "MessageTextBlock";
    /// <summary>Left button template part name.</summary>
    private const string LeftButton = "LeftButton";
    /// <summary>Right button template part name.</summary>
    private const string RightButton = "RightButton";
    /// <summary>
    /// Holds a weak reference to the message box that is currently displayed.
    /// </summary>
    private static WeakReference _currentInstance;
    /// <summary>The current screen width.</summary>
    private static readonly double _screenWidth = Application.Current.Host.Content.ActualWidth;
    /// <summary>The current screen height.</summary>
    private static readonly double _screenHeight = Application.Current.Host.Content.ActualHeight;
    /// <summary>
    /// Identifies whether the application bar and the system tray
    /// must be restored after the message box is dismissed.
    /// </summary>
    private static bool _mustRestore = true;
    /// <summary>Title text block template part.</summary>
    private TextBlock _titleTextBlock;
    /// <summary>Caption text block template part.</summary>
    private TextBlock _captionTextBlock;
    /// <summary>Message text block template part.</summary>
    private TextBlock _messageTextBlock;
    /// <summary>Left button template part.</summary>
    private Button _leftButton;
    /// <summary>Right button template part.</summary>
    private Button _rightButton;
    /// <summary>The popup used to display the message box.</summary>
    private Popup _popup;
    /// <summary>The child container of the popup.</summary>
    private Grid _container;
    /// <summary>The root visual of the application.</summary>
    private PhoneApplicationFrame _frame;
    /// <summary>The current application page.</summary>
    private PhoneApplicationPage _page;
    /// <summary>
    /// Identifies whether the application bar is visible or not before
    /// opening the message box.
    /// </summary>
    private bool _hasApplicationBar;
    /// <summary>The current color of the system tray.</summary>
    private Color _systemTrayColor;
    /// <summary>Identifies the Title dependency property.</summary>
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (CustomMessageBox), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(CustomMessageBox.OnTitlePropertyChanged)));
    /// <summary>Identifies the Caption dependency property.</summary>
    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(nameof (Caption), typeof (string), typeof (CustomMessageBox), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(CustomMessageBox.OnCaptionPropertyChanged)));
    /// <summary>Identifies the Message dependency property.</summary>
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (CustomMessageBox), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(CustomMessageBox.OnMessagePropertyChanged)));
    /// <summary>Identifies the LeftButtonContent dependency property.</summary>
    public static readonly DependencyProperty LeftButtonContentProperty = DependencyProperty.Register(nameof (LeftButtonContent), typeof (object), typeof (CustomMessageBox), new PropertyMetadata((object) null, new PropertyChangedCallback(CustomMessageBox.OnLeftButtonContentPropertyChanged)));
    /// <summary>
    /// Identifies the RightButtonContent dependency property.
    /// </summary>
    public static readonly DependencyProperty RightButtonContentProperty = DependencyProperty.Register(nameof (RightButtonContent), typeof (object), typeof (CustomMessageBox), new PropertyMetadata((object) null, new PropertyChangedCallback(CustomMessageBox.OnRightButtonContentPropertyChanged)));
    /// <summary>
    /// Identifies the IsLeftButtonEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty IsLeftButtonEnabledProperty = DependencyProperty.Register(nameof (IsLeftButtonEnabled), typeof (bool), typeof (CustomMessageBox), new PropertyMetadata((object) true));
    /// <summary>
    /// Identifies the IsRightButtonEnabled dependency property.
    /// </summary>
    public static readonly DependencyProperty IsRightButtonEnabledProperty = DependencyProperty.Register(nameof (IsRightButtonEnabled), typeof (bool), typeof (CustomMessageBox), new PropertyMetadata((object) true));
    /// <summary>Identifies the IsFullScreen dependency property.</summary>
    public static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.Register(nameof (IsFullScreen), typeof (bool), typeof (CustomMessageBox), new PropertyMetadata((object) false, new PropertyChangedCallback(CustomMessageBox.OnIsFullScreenPropertyChanged)));

    /// <summary>Called when the message is being dismissing.</summary>
    public event EventHandler<DismissingEventArgs> Dismissing;

    /// <summary>Called when the message box is dismissed.</summary>
    public event EventHandler<DismissedEventArgs> Dismissed;

    /// <summary>Gets or sets the title.</summary>
    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(CustomMessageBox.TitleProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.TitleProperty, (object) value);
    }

    /// <summary>
    /// Changes the visibility of the title text block based on its content.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnTitlePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if (customMessageBox._titleTextBlock == null)
        return;
      string newValue = (string) e.NewValue;
      ((UIElement) customMessageBox._titleTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(newValue);
    }

    /// <summary>Gets or sets the caption.</summary>
    public string Caption
    {
      get => (string) ((DependencyObject) this).GetValue(CustomMessageBox.CaptionProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.CaptionProperty, (object) value);
    }

    /// <summary>
    /// Changes the visibility of the caption text block based on its content.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnCaptionPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if (customMessageBox._captionTextBlock == null)
        return;
      string newValue = (string) e.NewValue;
      ((UIElement) customMessageBox._captionTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(newValue);
    }

    /// <summary>Gets or sets the message.</summary>
    public string Message
    {
      get => (string) ((DependencyObject) this).GetValue(CustomMessageBox.MessageProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.MessageProperty, (object) value);
    }

    /// <summary>
    /// Changes the visibility of the message text block based on its content.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnMessagePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if (customMessageBox._messageTextBlock == null)
        return;
      string newValue = (string) e.NewValue;
      ((UIElement) customMessageBox._messageTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(newValue);
    }

    /// <summary>Gets or sets the left button content.</summary>
    public object LeftButtonContent
    {
      get => ((DependencyObject) this).GetValue(CustomMessageBox.LeftButtonContentProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.LeftButtonContentProperty, value);
    }

    /// <summary>
    /// Changes the visibility of the left button based on its content.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnLeftButtonContentPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if (customMessageBox._leftButton == null)
        return;
      ((UIElement) customMessageBox._leftButton).Visibility = CustomMessageBox.GetVisibilityFromObject(e.NewValue);
    }

    /// <summary>Gets or sets the right button content.</summary>
    public object RightButtonContent
    {
      get => ((DependencyObject) this).GetValue(CustomMessageBox.RightButtonContentProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.RightButtonContentProperty, value);
    }

    /// <summary>
    /// Changes the visibility of the right button based on its content.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event information.</param>
    private static void OnRightButtonContentPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if (customMessageBox._rightButton == null)
        return;
      ((UIElement) customMessageBox._rightButton).Visibility = CustomMessageBox.GetVisibilityFromObject(e.NewValue);
    }

    /// <summary>Gets or sets whether the left button is enabled.</summary>
    public bool IsLeftButtonEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(CustomMessageBox.IsLeftButtonEnabledProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.IsLeftButtonEnabledProperty, (object) value);
    }

    /// <summary>Gets or sets whether the right button is enabled.</summary>
    public bool IsRightButtonEnabled
    {
      get => (bool) ((DependencyObject) this).GetValue(CustomMessageBox.IsRightButtonEnabledProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.IsRightButtonEnabledProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets whether the message box occupies the whole screen.
    /// </summary>
    public bool IsFullScreen
    {
      get => (bool) ((DependencyObject) this).GetValue(CustomMessageBox.IsFullScreenProperty);
      set => ((DependencyObject) this).SetValue(CustomMessageBox.IsFullScreenProperty, (object) value);
    }

    /// <summary>
    /// Modifies the vertical alignment of the message box depending
    /// on whether it should occupy the full screen or not.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="e"></param>
    private static void OnIsFullScreenPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      CustomMessageBox customMessageBox = (CustomMessageBox) obj;
      if ((bool) e.NewValue)
        ((FrameworkElement) customMessageBox).VerticalAlignment = (VerticalAlignment) 3;
      else
        ((FrameworkElement) customMessageBox).VerticalAlignment = (VerticalAlignment) 0;
    }

    /// <summary>
    /// Called when the back key is pressed. This event handler cancels
    /// the backward navigation and dismisses the message box.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnBackKeyPress(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
      this.Dismiss(CustomMessageBoxResult.None, true);
    }

    /// <summary>
    /// Called when the application frame is navigating.
    /// This event handler dismisses the message box.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnNavigating(object sender, NavigatingCancelEventArgs e)
    {
    }

    /// <summary>
    /// Gets the template parts and attaches event handlers.
    /// Animates the message box when the template is applied to it.
    /// </summary>
    public virtual void OnApplyTemplate()
    {
      if (this._leftButton != null)
        ((ButtonBase) this._leftButton).Click -= new RoutedEventHandler(this.LeftButton_Click);
      if (this._rightButton != null)
        ((ButtonBase) this._rightButton).Click -= new RoutedEventHandler(this.RightButton_Click);
      ((FrameworkElement) this).OnApplyTemplate();
      this._titleTextBlock = ((Control) this).GetTemplateChild("TitleTextBlock") as TextBlock;
      this._captionTextBlock = ((Control) this).GetTemplateChild("CaptionTextBlock") as TextBlock;
      this._messageTextBlock = ((Control) this).GetTemplateChild("MessageTextBlock") as TextBlock;
      this._leftButton = ((Control) this).GetTemplateChild("LeftButton") as Button;
      this._rightButton = ((Control) this).GetTemplateChild("RightButton") as Button;
      if (this._titleTextBlock != null)
        ((UIElement) this._titleTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(this.Title);
      if (this._captionTextBlock != null)
        ((UIElement) this._captionTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(this.Caption);
      if (this._messageTextBlock != null)
        ((UIElement) this._messageTextBlock).Visibility = CustomMessageBox.GetVisibilityFromString(this.Message);
      if (this._leftButton != null)
      {
        ((ButtonBase) this._leftButton).Click += new RoutedEventHandler(this.LeftButton_Click);
        ((UIElement) this._leftButton).Visibility = CustomMessageBox.GetVisibilityFromObject(this.LeftButtonContent);
      }
      if (this._rightButton == null)
        return;
      ((ButtonBase) this._rightButton).Click += new RoutedEventHandler(this.RightButton_Click);
      ((UIElement) this._rightButton).Visibility = CustomMessageBox.GetVisibilityFromObject(this.RightButtonContent);
    }

    /// <summary>
    /// Initializes a new instance of the CustomMessageBox class.
    /// </summary>
    public CustomMessageBox() => ((Control) this).DefaultStyleKey = (object) typeof (CustomMessageBox);

    /// <summary>
    /// Reveals the message box by inserting it into a popup and opening it.
    /// </summary>
    public void Show()
    {
      if (this._popup != null && this._popup.IsOpen)
        return;
      ((FrameworkElement) this).LayoutUpdated += new EventHandler(this.CustomMessageBox_LayoutUpdated);
      this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
      this._page = ((ContentControl) this._frame).Content as PhoneApplicationPage;
      if (SystemTray.IsVisible)
      {
        this._systemTrayColor = SystemTray.BackgroundColor;
        SystemTray.BackgroundColor = !(((Control) this).Background is SolidColorBrush) ? (Color) Application.Current.Resources[(object) "PhoneChromeColor"] : ((SolidColorBrush) ((Control) this).Background).Color;
      }
      if (this._page.ApplicationBar != null)
      {
        this._hasApplicationBar = this._page.ApplicationBar.IsVisible;
        if (this._hasApplicationBar)
          this._page.ApplicationBar.IsVisible = false;
      }
      else
        this._hasApplicationBar = false;
      if (CustomMessageBox._currentInstance != null)
      {
        CustomMessageBox._mustRestore = false;
        if (CustomMessageBox._currentInstance.Target is CustomMessageBox target)
        {
          this._systemTrayColor = target._systemTrayColor;
          this._hasApplicationBar = target._hasApplicationBar;
          target.Dismiss();
        }
      }
      CustomMessageBox._mustRestore = true;
      Rectangle rectangle = new Rectangle();
      Color resource = (Color) Application.Current.Resources[(object) "PhoneBackgroundColor"];
      ((Shape) rectangle).Fill = (Brush) new SolidColorBrush(Color.FromArgb((byte) 153, resource.R, resource.G, resource.B));
      this._container = new Grid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) this._container).Children).Add((UIElement) rectangle);
      ((PresentationFrameworkCollection<UIElement>) ((Panel) this._container).Children).Add((UIElement) this);
      this._popup = new Popup();
      this._popup.Child = (UIElement) this._container;
      this.SetSizeAndOffset();
      this._popup.IsOpen = true;
      CustomMessageBox._currentInstance = new WeakReference((object) this);
      if (this._page != null)
      {
        this._page.BackKeyPress += new EventHandler<CancelEventArgs>(this.OnBackKeyPress);
        this._page.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.OnOrientationChanged);
      }
      if (this._frame == null)
        return;
      ((Frame) this._frame).Navigating += new NavigatingCancelEventHandler(this.OnNavigating);
    }

    /// <summary>Dismisses the message box.</summary>
    public void Dismiss() => this.Dismiss(CustomMessageBoxResult.None, true);

    /// <summary>Dismisses the message box.</summary>
    /// <param name="source">The source that caused the dismission.</param>
    /// <param name="useTransition">
    /// If true, the message box is dismissed after swiveling
    /// backward and out.
    /// </param>
    private void Dismiss(CustomMessageBoxResult source, bool useTransition)
    {
      EventHandler<DismissingEventArgs> dismissing = this.Dismissing;
      if (dismissing != null)
      {
        DismissingEventArgs e = new DismissingEventArgs(source);
        dismissing((object) this, e);
        if (e.Cancel)
          return;
      }
      EventHandler<DismissedEventArgs> dismissed = this.Dismissed;
      if (dismissed != null)
      {
        DismissedEventArgs e = new DismissedEventArgs(source);
        dismissed((object) this, e);
      }
      CustomMessageBox._currentInstance = (WeakReference) null;
      bool restoreOriginalValues = CustomMessageBox._mustRestore;
      if (useTransition)
      {
        ITransition swivelTransition = new SwivelTransition()
        {
          Mode = SwivelTransitionMode.BackwardOut
        }.GetTransition((UIElement) this);
        swivelTransition.Completed += (EventHandler) ((s, e) =>
        {
          swivelTransition.Stop();
          this.ClosePopup(restoreOriginalValues);
        });
        swivelTransition.Begin();
      }
      else
        this.ClosePopup(restoreOriginalValues);
    }

    /// <summary>Closes the pop up.</summary>
    private void ClosePopup(bool restoreOriginalValues)
    {
      this._popup.IsOpen = false;
      this._popup = (Popup) null;
      if (restoreOriginalValues)
      {
        if (SystemTray.IsVisible)
          SystemTray.BackgroundColor = this._systemTrayColor;
        if (this._hasApplicationBar)
        {
          this._hasApplicationBar = false;
          this._page.ApplicationBar.IsVisible = true;
        }
      }
      if (this._page != null)
      {
        this._page.BackKeyPress -= new EventHandler<CancelEventArgs>(this.OnBackKeyPress);
        this._page.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.OnOrientationChanged);
        this._page = (PhoneApplicationPage) null;
      }
      if (this._frame == null)
        return;
      ((Frame) this._frame).Navigating -= new NavigatingCancelEventHandler(this.OnNavigating);
      this._frame = (PhoneApplicationFrame) null;
    }

    /// <summary>Called when the visual layout changes.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void CustomMessageBox_LayoutUpdated(object sender, EventArgs e)
    {
      ITransition swivelTransition = new SwivelTransition()
      {
        Mode = SwivelTransitionMode.BackwardIn
      }.GetTransition((UIElement) this);
      swivelTransition.Completed += (EventHandler) ((s1, e1) => swivelTransition.Stop());
      swivelTransition.Begin();
      ((FrameworkElement) this).LayoutUpdated -= new EventHandler(this.CustomMessageBox_LayoutUpdated);
    }

    /// <summary>Dismisses the message box with the left button.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void LeftButton_Click(object sender, RoutedEventArgs e) => this.Dismiss(CustomMessageBoxResult.LeftButton, true);

    /// <summary>Dismisses the message box with the right button.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void RightButton_Click(object sender, RoutedEventArgs e) => this.Dismiss(CustomMessageBoxResult.RightButton, true);

    /// <summary>Called when the current page changes orientation.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private void OnOrientationChanged(object sender, OrientationChangedEventArgs e) => this.SetSizeAndOffset();

    /// <summary>
    /// Sets The vertical and horizontal offset of the popup,
    /// as well as the size of its child container.
    /// </summary>
    private void SetSizeAndOffset()
    {
      Rect transformedRect = CustomMessageBox.GetTransformedRect();
      if (this._container != null)
      {
        ((UIElement) this._container).RenderTransform = CustomMessageBox.GetTransform();
        ((FrameworkElement) this._container).Width = transformedRect.Width;
        ((FrameworkElement) this._container).Height = transformedRect.Height;
      }
      if (!SystemTray.IsVisible || this._popup == null)
        return;
      PageOrientation pageOrientation = CustomMessageBox.GetPageOrientation();
      if (pageOrientation != 5)
      {
        if (pageOrientation != 18)
        {
          if (pageOrientation == 34)
          {
            this._popup.HorizontalOffset = 0.0;
            this._popup.VerticalOffset = 0.0;
          }
        }
        else
        {
          this._popup.HorizontalOffset = 0.0;
          this._popup.VerticalOffset = 72.0;
        }
      }
      else
      {
        this._popup.HorizontalOffset = 0.0;
        this._popup.VerticalOffset = 32.0;
        Grid container = this._container;
        ((FrameworkElement) container).Height = ((FrameworkElement) container).Height - 32.0;
      }
    }

    /// <summary>Gets a rectangle that occupies the entire page.</summary>
    /// <returns>The width, height and location of the rectangle.</returns>
    private static Rect GetTransformedRect()
    {
      bool flag = CustomMessageBox.IsLandscape(CustomMessageBox.GetPageOrientation());
      return new Rect(0.0, 0.0, flag ? CustomMessageBox._screenHeight : CustomMessageBox._screenWidth, flag ? CustomMessageBox._screenWidth : CustomMessageBox._screenHeight);
    }

    /// <summary>Determines whether the orientation is landscape.</summary>
    /// <param name="orientation">The orientation.</param>
    /// <returns>True if the orientation is landscape.</returns>
    private static bool IsLandscape(PageOrientation orientation) => orientation == 2 || orientation == 18 || orientation == 34;

    /// <summary>
    /// Gets a transform for popup elements based
    /// on the current page orientation.
    /// </summary>
    /// <returns>A composite transform determined by page orientation.</returns>
    private static Transform GetTransform()
    {
      PageOrientation pageOrientation = CustomMessageBox.GetPageOrientation();
      if (pageOrientation != 2 && pageOrientation != 18)
      {
        if (pageOrientation != 34)
          return (Transform) null;
        return (Transform) new CompositeTransform()
        {
          Rotation = -90.0,
          TranslateY = CustomMessageBox._screenHeight
        };
      }
      return (Transform) new CompositeTransform()
      {
        Rotation = 90.0,
        TranslateX = CustomMessageBox._screenWidth
      };
    }

    /// <summary>Gets the current page orientation.</summary>
    /// <returns>The current page orientation.</returns>
    private static PageOrientation GetPageOrientation() => Application.Current.RootVisual is PhoneApplicationFrame rootVisual && ((ContentControl) rootVisual).Content is PhoneApplicationPage content ? content.Orientation : (PageOrientation) 0;

    /// <summary>
    /// Returns a visibility value based on the value of a string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>
    /// Visibility.Collapsed if the string is null or empty.
    /// Visibility.Visible otherwise.
    /// </returns>
    private static Visibility GetVisibilityFromString(string str) => string.IsNullOrEmpty(str) ? (Visibility) 1 : (Visibility) 0;

    /// <summary>
    /// Returns a visibility value based on the value of an object.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>
    /// Visibility.Collapsed if the object is null.
    /// Visibility.Visible otherwise.
    /// </returns>
    private static Visibility GetVisibilityFromObject(object obj) => obj == null ? (Visibility) 1 : (Visibility) 0;
  }
}
