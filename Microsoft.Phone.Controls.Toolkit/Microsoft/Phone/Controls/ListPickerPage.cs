// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ListPickerPage
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Displays the list of items and allows single or multiple selection.
  /// </summary>
  public class ListPickerPage : PhoneApplicationPage
  {
    private const string StateKey_Value = "ListPickerPage_State_Value";
    private PageOrientation _lastOrientation;
    private IList<WeakReference> _itemsToAnimate;
    private static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof (IsOpen), typeof (bool), typeof (ListPickerPage), new PropertyMetadata((object) false, new PropertyChangedCallback(ListPickerPage.OnIsOpenChanged)));
    internal Grid MainGrid;
    internal TextBlock HeaderTitle;
    internal ListBox Picker;
    private bool _contentLoaded;

    /// <summary>
    /// Gets or sets the string of text to display as the header of the page.
    /// </summary>
    public string HeaderText { get; set; }

    /// <summary>Gets or sets the list of items to display.</summary>
    public IList Items { get; private set; }

    /// <summary>Gets or sets the selection mode.</summary>
    public SelectionMode SelectionMode { get; set; }

    /// <summary>Gets or sets the selected item.</summary>
    public object SelectedItem { get; set; }

    /// <summary>Gets or sets the list of items to select.</summary>
    public IList SelectedItems { get; private set; }

    /// <summary>Gets or sets the item template</summary>
    public DataTemplate FullModeItemTemplate { get; set; }

    /// <summary>Whether the picker page is open or not.</summary>
    private bool IsOpen
    {
      get => (bool) ((DependencyObject) this).GetValue(ListPickerPage.IsOpenProperty);
      set => ((DependencyObject) this).SetValue(ListPickerPage.IsOpenProperty, (object) value);
    }

    private static void OnIsOpenChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) => (o as ListPickerPage).OnIsOpenChanged();

    private void OnIsOpenChanged() => this.UpdateVisualState(true);

    /// <summary>Creates a list picker page.</summary>
    public ListPickerPage()
    {
      this.InitializeComponent();
      this.Items = (IList) new List<object>();
      this.SelectedItems = (IList) new List<object>();
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.OnLoaded);
      ((FrameworkElement) this).Unloaded += new RoutedEventHandler(this.OnUnloaded);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.OnOrientationChanged);
      this._lastOrientation = this.Orientation;
      if (null != this.ApplicationBar)
      {
        foreach (object button in (IEnumerable) this.ApplicationBar.Buttons)
        {
          IApplicationBarIconButton iapplicationBarIconButton = button as IApplicationBarIconButton;
          if (null != iapplicationBarIconButton)
          {
            if ("DONE" == ((IApplicationBarMenuItem) iapplicationBarIconButton).Text)
            {
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Text = ControlResources.DateTimePickerDoneText;
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Click += new EventHandler(this.OnDoneButtonClick);
            }
            else if ("CANCEL" == ((IApplicationBarMenuItem) iapplicationBarIconButton).Text)
            {
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Text = ControlResources.DateTimePickerCancelText;
              ((IApplicationBarMenuItem) iapplicationBarIconButton).Click += new EventHandler(this.OnCancelButtonClick);
            }
          }
        }
      }
      this.SetupListItems(-90.0);
      PlaneProjection planeProjection = (PlaneProjection) ((UIElement) this.HeaderTitle).Projection;
      if (null == planeProjection)
      {
        planeProjection = new PlaneProjection();
        ((UIElement) this.HeaderTitle).Projection = (Projection) planeProjection;
      }
      planeProjection.RotationX = -90.0;
      ((UIElement) this.Picker).Opacity = 1.0;
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.IsOpen = true));
    }

    private void OnUnloaded(object sender, RoutedEventArgs e) => this.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.OnOrientationChanged);

    private void SetupListItems(double degree)
    {
      this._itemsToAnimate = ItemsControlExtensions.GetItemsInViewPort((ItemsControl) this.Picker);
      for (int index = 0; index < this._itemsToAnimate.Count; ++index)
      {
        FrameworkElement target = (FrameworkElement) this._itemsToAnimate[index].Target;
        if (null != target)
        {
          PlaneProjection planeProjection = (PlaneProjection) ((UIElement) target).Projection;
          if (null == planeProjection)
          {
            planeProjection = new PlaneProjection();
            ((UIElement) target).Projection = (Projection) planeProjection;
          }
          planeProjection.RotationX = degree;
        }
      }
    }

    /// <summary>
    /// Called when a page becomes the active page in a frame.
    /// </summary>
    /// <param name="e">An object that contains the event data.</param>
    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Page) this).OnNavigatedTo(e);
      if (this.State.ContainsKey("ListPickerPage_State_Value"))
      {
        this.State.Remove("ListPickerPage_State_Value");
        if (((Page) this).NavigationService.CanGoBack)
        {
          ((Page) this).NavigationService.GoBack();
          return;
        }
      }
      if (null != this.HeaderText)
        this.HeaderTitle.Text = this.HeaderText.ToUpper(CultureInfo.CurrentCulture);
      ((FrameworkElement) this.Picker).DataContext = (object) this.Items;
      this.Picker.SelectionMode = this.SelectionMode;
      if (null != this.FullModeItemTemplate)
        ((ItemsControl) this.Picker).ItemTemplate = this.FullModeItemTemplate;
      if (this.SelectionMode == 0)
      {
        this.ApplicationBar.IsVisible = false;
        ((Selector) this.Picker).SelectedItem = this.SelectedItem;
      }
      else
      {
        this.ApplicationBar.IsVisible = true;
        this.Picker.ItemContainerStyle = (Style) ((FrameworkElement) this).Resources[(object) "ListBoxItemCheckedStyle"];
        foreach (object obj in (IEnumerable) this.Items)
        {
          if (this.SelectedItems != null && this.SelectedItems.Contains(obj))
            this.Picker.SelectedItems.Add(obj);
        }
      }
    }

    private void OnDoneButtonClick(object sender, EventArgs e)
    {
      this.SelectedItem = ((Selector) this.Picker).SelectedItem;
      this.SelectedItems = this.Picker.SelectedItems;
      this.ClosePickerPage();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
      this.SelectedItem = (object) null;
      this.SelectedItems = (IList) null;
      this.ClosePickerPage();
    }

    /// <summary>Called when the Back key is pressed.</summary>
    /// <param name="e">Event arguments.</param>
    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      e.Cancel = true;
      this.SelectedItem = (object) null;
      this.SelectedItems = (IList) null;
      this.ClosePickerPage();
    }

    private void ClosePickerPage()
    {
      ((UIElement) this.Picker).IsHitTestVisible = false;
      this.IsOpen = false;
    }

    private void OnClosedStoryboardCompleted(object sender, EventArgs e)
    {
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
    }

    /// <summary>
    /// Called when a page is no longer the active page in a frame.
    /// </summary>
    /// <param name="e">An object that contains the event data.</param>
    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      if (null == e)
        throw new ArgumentNullException(nameof (e));
      ((Page) this).OnNavigatedFrom(e);
      if (!e.Uri.IsExternalNavigation())
        return;
      this.State["ListPickerPage_State_Value"] = (object) "ListPickerPage_State_Value";
    }

    private void OnOrientationChanged(object sender, OrientationChangedEventArgs e)
    {
      PageOrientation orientation = e.Orientation;
      RotateTransition rotateTransition = new RotateTransition();
      if (null != this.MainGrid)
      {
        PageOrientation pageOrientation = orientation;
        switch (pageOrientation - 1)
        {
          case 0:
          case 4:
            ((FrameworkElement) this.HeaderTitle).Margin = new Thickness(24.0, 12.0, 12.0, 12.0);
            ((FrameworkElement) this.Picker).Margin = new Thickness(24.0, 12.0, 0.0, 0.0);
            rotateTransition.Mode = this._lastOrientation == 18 ? RotateTransitionMode.In90Counterclockwise : RotateTransitionMode.In90Clockwise;
            goto case 2;
          case 1:
            ((FrameworkElement) this.HeaderTitle).Margin = new Thickness(24.0, 24.0, 0.0, 0.0);
            ((FrameworkElement) this.Picker).Margin = new Thickness(24.0, 24.0, 0.0, 0.0);
            rotateTransition.Mode = this._lastOrientation == 34 ? RotateTransitionMode.In180Counterclockwise : RotateTransitionMode.In90Clockwise;
            goto case 2;
          case 2:
          case 3:
            break;
          default:
            if (pageOrientation != 18)
            {
              if (pageOrientation == 34)
              {
                ((FrameworkElement) this.HeaderTitle).Margin = new Thickness(24.0, 24.0, 0.0, 0.0);
                ((FrameworkElement) this.Picker).Margin = new Thickness(24.0, 24.0, 0.0, 0.0);
                rotateTransition.Mode = this._lastOrientation == 5 ? RotateTransitionMode.In90Counterclockwise : RotateTransitionMode.In180Clockwise;
                goto case 2;
              }
              else
                goto case 2;
            }
            else
              goto case 1;
        }
      }
      PhoneApplicationPage content = (PhoneApplicationPage) ((ContentControl) Application.Current.RootVisual).Content;
      ITransition transition = rotateTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1) => transition.Stop());
      transition.Begin();
      this._lastOrientation = orientation;
    }

    private void UpdateVisualState(bool useTransitions)
    {
      if (useTransitions)
      {
        ScrollViewer scrollViewer = ((DependencyObject) this.Picker).GetVisualChildren().OfType<ScrollViewer>().FirstOrDefault<ScrollViewer>();
        scrollViewer?.ScrollToVerticalOffset(scrollViewer.VerticalOffset);
        if (!this.IsOpen)
          this.SetupListItems(0.0);
        Storyboard storyboard1 = new Storyboard();
        Storyboard storyboard2 = this.AnimationForElement((FrameworkElement) this.HeaderTitle, 0);
        ((PresentationFrameworkCollection<Timeline>) storyboard1.Children).Add((Timeline) storyboard2);
        for (int index = 0; index < this._itemsToAnimate.Count; ++index)
        {
          Storyboard storyboard3 = this.AnimationForElement((FrameworkElement) this._itemsToAnimate[index].Target, index + 1);
          ((PresentationFrameworkCollection<Timeline>) storyboard1.Children).Add((Timeline) storyboard3);
        }
        if (!this.IsOpen)
          ((Timeline) storyboard1).Completed += new EventHandler(this.OnClosedStoryboardCompleted);
        storyboard1.Begin();
      }
      else
      {
        if (this.IsOpen)
          return;
        this.OnClosedStoryboardCompleted((object) null, (EventArgs) null);
      }
    }

    private Storyboard AnimationForElement(FrameworkElement element, int index)
    {
      double num1 = 30.0;
      double num2 = this.IsOpen ? 350.0 : 250.0;
      double num3 = this.IsOpen ? -45.0 : 0.0;
      double num4 = this.IsOpen ? 0.0 : 90.0;
      ExponentialEase exponentialEase1 = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase1).EasingMode = this.IsOpen ? (EasingMode) 0 : (EasingMode) 1;
      exponentialEase1.Exponent = 5.0;
      ExponentialEase exponentialEase2 = exponentialEase1;
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      ((Timeline) doubleAnimation1).Duration = new Duration(TimeSpan.FromMilliseconds(num2));
      doubleAnimation1.From = new double?(num3);
      doubleAnimation1.To = new double?(num4);
      doubleAnimation1.EasingFunction = (IEasingFunction) exponentialEase2;
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) element);
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationX)", new object[0]));
      Storyboard storyboard = new Storyboard();
      ((Timeline) storyboard).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(num1 * (double) index));
      ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
      return storyboard;
    }

    private void OnPickerTapped(object sender, GestureEventArgs e)
    {
      if (this.SelectionMode != 0)
        return;
      this.SelectedItem = ((Selector) this.Picker).SelectedItem;
      this.ClosePickerPage();
    }

    /// <summary>InitializeComponent</summary>
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Microsoft.Phone.Controls.Toolkit;component/ListPicker/ListPickerPage.xaml", UriKind.Relative));
      this.MainGrid = (Grid) ((FrameworkElement) this).FindName("MainGrid");
      this.HeaderTitle = (TextBlock) ((FrameworkElement) this).FindName("HeaderTitle");
      this.Picker = (ListBox) ((FrameworkElement) this).FindName("Picker");
    }
  }
}
