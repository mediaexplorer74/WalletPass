// WalletPass.confListPage

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace WalletPass
{
  public sealed partial class confListPage : Page
  {
    private bool orderChanged;
    //internal Grid LayoutRoot;
    //internal Button btnHelp;
    //internal Rectangle imgBtnHelp;
    //internal ListPicker listPickerElementSize;
    //internal ListPicker listPickerElementOrder;
    //internal ToggleSwitch toggleSwitchGroup;
    //internal ListPicker listPickerGroupBy;
    //internal ListPicker listPickerGroupOrder;
    //private bool _contentLoaded;

    public confListPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      if (this.orderChanged)
      {
        ClasePassCollection source = new ClasePassCollection(App._passcollection.ToList<ClasePass>());
        App._passcollection.Clear();
        App._passcollection.AddAllNew(source.ToList<ClasePass>());
      }
      this.showTransitionOutBackward();
      base.OnBackKeyPress(e);
    }

    private void listPickerGroupBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.listPickerGroupBy == null)
        return;
      if (e.AddedItems.Count > 0)
        appSettings.listGroupBy = this.listPickerGroupBy.SelectedIndex;
      else
        this.listPickerGroupBy.SelectedIndex = appSettings.listGroupBy;
    }

    private void listPickerGroupOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.listPickerGroupOrder == null)
        return;
      if (e.AddedItems.Count > 0)
        appSettings.listGroupOrder = this.listPickerGroupOrder.SelectedIndex;
      else
        this.listPickerGroupOrder.SelectedIndex = appSettings.listGroupOrder;
    }

    private void toggleSwitchGroup_Checked(object sender, RoutedEventArgs e)
    {
      ((UIElement) this.listPickerGroupBy).Visibility = (Visibility) 0;
      ((UIElement) this.listPickerGroupOrder).Visibility = (Visibility) 0;
    }

    private void toggleSwitchGroup_Unchecked(object sender, RoutedEventArgs e)
    {
      ((UIElement) this.listPickerGroupBy).Visibility = (Visibility) 1;
      ((UIElement) this.listPickerGroupOrder).Visibility = (Visibility) 1;
    }

    private void showTransitionOutBackward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      opacityTransition.GetTransition((UIElement) element).Begin();
    }

    private void showTransitionInForward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionIn;
      PhoneApplicationPage content = (PhoneApplicationPage) ((ContentControl) Application.Current.RootVisual).Content;
      ITransition transition = opacityTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1) => ((UIElement) this).Opacity = 1.0);
      transition.Begin();
    }

    private void showTransitionTurnstile()
    {
      TurnstileTransition turnstileTransition = new TurnstileTransition();
      turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      ITransition trans = turnstileTransition.GetTransition((UIElement) element);
      trans.Completed += (EventHandler) ((param0, param1) => trans.Stop());
      trans.Begin();
    }

    private void btnHelp_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/Tutorials/tutorialConfList.xaml", UriKind.Relative))));
    }

    private void btnHelp_ManipulationStarted(object sender, ManipulationStartedEventArgs e) => ((Shape) this.imgBtnHelp).Fill = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorMain, (Type) null, (object) null, (CultureInfo) null);

    private void btnHelp_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e) => ((Shape) this.imgBtnHelp).Fill = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorForeground, (Type) null, (object) null, (CultureInfo) null);

    private void listPickerElementSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.listPickerElementSize == null)
        return;
      if (e.AddedItems.Count > 0)
        appSettings.listElementSize = this.listPickerElementSize.SelectedIndex;
      else
        this.listPickerElementSize.SelectedIndex = appSettings.listElementSize;
    }

    private void listPickerElementOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.listPickerElementOrder == null)
        return;
      if (e.AddedItems.Count > 0)
      {
        appSettings.listOrder = this.listPickerElementOrder.SelectedIndex;
        this.orderChanged = true;
      }
      else
        this.listPickerElementOrder.SelectedIndex = appSettings.listOrder;
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confListPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnHelp = (Button) ((FrameworkElement) this).FindName("btnHelp");
      this.imgBtnHelp = (Rectangle) ((FrameworkElement) this).FindName("imgBtnHelp");
      this.listPickerElementSize = (ListPicker) ((FrameworkElement) this).FindName("listPickerElementSize");
      this.listPickerElementOrder = (ListPicker) ((FrameworkElement) this).FindName("listPickerElementOrder");
      this.toggleSwitchGroup = (ToggleSwitch) ((FrameworkElement) this).FindName("toggleSwitchGroup");
      this.listPickerGroupBy = (ListPicker) ((FrameworkElement) this).FindName("listPickerGroupBy");
      this.listPickerGroupOrder = (ListPicker) ((FrameworkElement) this).FindName("listPickerGroupOrder");
    }
        */
  }
}
