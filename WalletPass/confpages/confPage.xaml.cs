// Decompiled with JetBrains decompiler
// Type: WalletPass.confPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

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
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Navigation;

namespace WalletPass
{
  public sealed partial class confPage : Page
  {
   // internal Grid LayoutRoot;
   // internal StackPanel confSavePassbook;
   // internal StackPanel confListPassbook;
   // internal StackPanel confNotification;
   // internal StackPanel confCalendar;
   // internal StackPanel confUpdate;
   // internal StackPanel confTiles;
   // internal StackPanel confThemes;
   // internal StackPanel confAbout;
   // private bool _contentLoaded;

    public confPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 =
                (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = 
                (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
              => this.showTransitionInForward()));
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

    private void confSavePassbook_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
          => ((Page) this).NavigationService.Navigate(
              new Uri("/confPages/confSavePage.xaml", UriKind.Relative))));
    }

    private void confListPassbook_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => 
      ((Page) this).NavigationService.Navigate(
          new Uri("/confPages/confListPage.xaml", UriKind.Relative))));
    }

    private void confNotification_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
          => ((Page) this).NavigationService.Navigate(
              new Uri("/confPages/confNotificationPage.xaml", UriKind.Relative))));
    }

    private void confTiles_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => 
      ((Page) this).NavigationService.Navigate(
          new Uri("/confPages/confTilesPage.xaml", UriKind.Relative))));
    }

    private void confCalendar_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
      ((Page) this).NavigationService.Navigate(
          new Uri("/confPages/confCalendarPage.xaml", UriKind.Relative))));
    }

    private void confAbout_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => 
      ((Page) this).NavigationService.Navigate(new Uri(
          "/confPages/confAboutPage.xaml", UriKind.Relative))));
    }

    private void confTheme_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
          => ((Page) this).NavigationService.Navigate(
              new Uri("/confPages/confThemePage.xaml", UriKind.Relative))));
    }

    private void confUpdate_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
          => ((Page) this).NavigationService.Navigate(
              new Uri("/confPages/confUpdatePage.xaml", UriKind.Relative))));
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
      PhoneApplicationPage content = (PhoneApplicationPage)
                ((ContentControl) Application.Current.RootVisual).Content;

      ITransition transition = opacityTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1)
                => ((UIElement) this).Opacity = 1.0);

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
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.confSavePassbook = (StackPanel) ((FrameworkElement) this).FindName("confSavePassbook");
      this.confListPassbook = (StackPanel) ((FrameworkElement) this).FindName("confListPassbook");
      this.confNotification = (StackPanel) ((FrameworkElement) this).FindName("confNotification");
      this.confCalendar = (StackPanel) ((FrameworkElement) this).FindName("confCalendar");
      this.confUpdate = (StackPanel) ((FrameworkElement) this).FindName("confUpdate");
      this.confTiles = (StackPanel) ((FrameworkElement) this).FindName("confTiles");
      this.confThemes = (StackPanel) ((FrameworkElement) this).FindName("confThemes");
      this.confAbout = (StackPanel) ((FrameworkElement) this).FindName("confAbout");
    }
        */
  }
}
