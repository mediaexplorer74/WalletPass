// WalletPass.confSavePage

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Navigation;

namespace WalletPass
{
  public sealed partial class confSavePage : Page
  {
    //internal Grid LayoutRoot;
    //internal ToggleSwitch toggleSwitchCalendarSaveSetting;
    //internal ListPicker saveAddWalletList;
    //private bool _contentLoaded;

    public confSavePage()
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
      this.showTransitionOutBackward();
      base.OnBackKeyPress(e);
    }

    private void saveAddWalletList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.saveAddWalletList == null)
        return;
      if (e.AddedItems.Count > 0)
        appSettings.saveAddWalletOption = this.saveAddWalletList.SelectedIndex;
      else
        this.saveAddWalletList.SelectedIndex = appSettings.saveAddWalletOption;
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

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confSavePage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.toggleSwitchCalendarSaveSetting = (ToggleSwitch) ((FrameworkElement) this).FindName("toggleSwitchCalendarSaveSetting");
      this.saveAddWalletList = (ListPicker) ((FrameworkElement) this).FindName("saveAddWalletList");
    }
        */
  }
}
