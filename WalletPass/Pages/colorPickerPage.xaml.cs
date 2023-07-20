// Decompiled with JetBrains decompiler
// Type: WalletPass.colorPickerPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WalletPass.Resources;

namespace WalletPass
{
  public class colorPickerPage : PhoneApplicationPage
  {
    private ApplicationBarIconButton _btnAceptar;
    private ApplicationBarIconButton _btnCancel;
    internal Grid LayoutRoot;
    internal TextBlock txtHeader;
    internal ColorPicker colorPicker;
    private bool _contentLoaded;

    public colorPickerPage()
    {
      this.InitializeComponent();
      this._btnAceptar = new ApplicationBarIconButton();
      this._btnAceptar.IconUri = new Uri("/Assets/AppBar/appbar.ok.png", UriKind.Relative);
      this._btnAceptar.Text = AppResources.AppBarButtonOk;
      this._btnAceptar.Click += new EventHandler(this.btnAceptar_Click);
      this._btnCancel = new ApplicationBarIconButton();
      this._btnCancel.IconUri = new Uri("/Assets/AppBar/appbar.cancel.png", UriKind.Relative);
      this._btnCancel.Text = AppResources.AppBarButtonCancel;
      this._btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.ApplicationBar.Buttons.Add((object) this._btnAceptar);
      this.ApplicationBar.Buttons.Add((object) this._btnCancel);
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorMain, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush4 = (SolidColorBrush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      this.ApplicationBar.BackgroundColor = solidColorBrush3.Color;
      this.ApplicationBar.ForegroundColor = solidColorBrush2.Color;
      this.colorPicker.Color = solidColorBrush4.Color;
      switch (App._colorPageType)
      {
        case 0:
          this.txtHeader.Text = AppResources.settingTextThemeHeaderColor;
          break;
        case 1:
          this.txtHeader.Text = AppResources.settingTextThemeMainColor;
          break;
        case 2:
          this.txtHeader.Text = AppResources.settingTextThemeForegroundColor;
          break;
      }
      if (!App._isTombStoned)
      {
        if (e.NavigationMode != null)
          return;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      e.Cancel = true;
      this.backKeyPress();
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      App._colorPage = this.colorPicker.Color.ToString();
      this.backKeyPress();
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.backKeyPress();

    private void backKeyPress()
    {
      this.showTransitionOutBackward();
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
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

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/colorPickerPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.txtHeader = (TextBlock) ((FrameworkElement) this).FindName("txtHeader");
      this.colorPicker = (ColorPicker) ((FrameworkElement) this).FindName("colorPicker");
    }
  }
}
