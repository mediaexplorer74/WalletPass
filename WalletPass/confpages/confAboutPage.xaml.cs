// WalletPass.confAboutPage

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Navigation;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WalletPass
{
  public sealed partial class confAboutPage : Page
  {
    //internal Grid LayoutRoot;
    //internal TextBlock aboutLBLVersion;
    //internal TextBlock txtpkPassIssue;
    //internal Button btnChanges;
    //private bool _contentLoaded;

    public confAboutPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
      this.aboutLBLVersion.Text = "Wallet Pass v " + this.GetVersionNumber();
    }

    private string GetVersionNumber()
    {
      PackageVersion version = Package.Current.Id.Version;
      return string.Format("{0}.{1}.{2}.{3}", (object) version.Major,
          (object) version.Minor, (object) version.Build, (object) version.Revision);
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = 
                (SolidColorBrush) toColorConverter.Convert(
                    (object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = 
                (SolidColorBrush) toColorConverter.Convert(
                    (object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (()
            => this.showTransitionInForward()));
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      this.showTransitionOutBackward();
      base.OnBackKeyPress(e);
    }

    private void btnChanges_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() 
          => ((Page) this).NavigationService.Navigate(
              new Uri("/changesPage.xaml", UriKind.Relative))));
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

      PhoneApplicationPage content =
                (PhoneApplicationPage) (
                (ContentControl) Application.Current.RootVisual).Content;

      ITransition transition = opacityTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1) =>
      ((UIElement) this).Opacity = 1.0);
      transition.Begin();
    }

    private void showTransitionTurnstile()
    {
      TurnstileTransition turnstileTransition = new TurnstileTransition();
      turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      turnstileTransition.GetTransition((UIElement) element).Begin();
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confAboutPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.aboutLBLVersion = (TextBlock) ((FrameworkElement) this).FindName("aboutLBLVersion");
      this.txtpkPassIssue = (TextBlock) ((FrameworkElement) this).FindName("txtpkPassIssue");
      this.btnChanges = (Button) ((FrameworkElement) this).FindName("btnChanges");
    }
        */
  }
}
