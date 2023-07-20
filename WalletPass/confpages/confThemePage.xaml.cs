// Decompiled with JetBrains decompiler
// Type: WalletPass.confThemePage
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
//using System.Windows.Controls;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
using WalletPass.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WalletPass
{
  public sealed partial class confThemePage : Page
  {
    private ApplicationBarIconButton _btnAceptar;
    private ApplicationBarIconButton _btnCancel;
    private string foregroundColor;
    private string mainColor;
    private string headerColor;
    private string customForegroundColor;
    private string customMainColor;
    private string customHeaderColor;
        /*
    internal Grid LayoutRoot;
    internal ListPicker listPickerThemes;
    internal Border borderCustomHeader;
    internal Border borderCustomMain;
    internal Border borderCustomForeground;
    internal StackPanel stackPanelCustomColors;
    internal Button btnThemeColorHeader;
    internal Border btnThemeColorHeaderBorder;
    internal Button btnThemeColorMain;
    internal Border btnThemeColorMainBorder;
    internal Button btnThemeColorForeground;
    internal Border btnThemeColorForegroundBorder;
    internal Rectangle ejBorder;
    internal Rectangle ejBlockHeader;
    internal TextBlock ejTxtHeader;
    internal Rectangle ejBlockMain;
    internal TextBlock ejTxtMain;
    internal Rectangle ejBlockAppBar;
    internal Button ejBtnAppBar;
    internal Rectangle ejBtnAppBarIcon;
    private bool _contentLoaded;
        */

    public confThemePage()
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
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
        {
          SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
          SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
          SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorMain, (Type) null, (object) null, (CultureInfo) null);
          SolidColorBrush solidColorBrush4 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorCustomHeader, (Type) null, (object) null, (CultureInfo) null);
          SolidColorBrush solidColorBrush5 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorCustomForeground, (Type) null, (object) null, (CultureInfo) null);
          SolidColorBrush solidColorBrush6 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorCustomMain, (Type) null, (object) null, (CultureInfo) null);
          this.foregroundColor = appSettings.themeColorForeground;
          this.headerColor = appSettings.themeColorHeader;
          this.mainColor = appSettings.themeColorMain;
          this.customForegroundColor = appSettings.themeColorCustomForeground;
          this.customHeaderColor = appSettings.themeColorCustomHeader;
          this.customMainColor = appSettings.themeColorCustomMain;
          SystemTray.BackgroundColor = solidColorBrush1.Color;
          SystemTray.ForegroundColor = solidColorBrush2.Color;
          this.ApplicationBar.BackgroundColor = solidColorBrush3.Color;
          this.ApplicationBar.ForegroundColor = solidColorBrush2.Color;
          this.borderCustomHeader.BorderBrush = (Brush) solidColorBrush5;
          this.borderCustomHeader.Background = (Brush) solidColorBrush4;
          this.borderCustomMain.Background = (Brush) solidColorBrush6;
          this.borderCustomMain.BorderBrush = (Brush) solidColorBrush5;
          this.borderCustomForeground.Background = (Brush) solidColorBrush5;
          this.borderCustomForeground.BorderBrush = (Brush) solidColorBrush5;
          this.btnThemeColorHeaderBorder.Background = (Brush) solidColorBrush4;
          this.btnThemeColorMainBorder.Background = (Brush) solidColorBrush6;
          this.btnThemeColorForegroundBorder.Background = (Brush) solidColorBrush5;
          this.btnThemeColorForegroundBorder.BorderBrush = (Brush) solidColorBrush5;
          this.btnThemeColorMainBorder.BorderBrush = (Brush) solidColorBrush5;
          this.btnThemeColorHeaderBorder.BorderBrush = (Brush) solidColorBrush5;
          this.listPickerThemes.SelectedIndex = appSettings.themeListThemeSelection;
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        }
        else
        {
          if (e.NavigationMode != 1)
            return;
          switch (App._colorPageType)
          {
            case 0:
              this.btnThemeColorHeaderBorder.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomHeader.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.headerColor = App._colorPage;
              this.customHeaderColor = App._colorPage;
              break;
            case 1:
              this.btnThemeColorMainBorder.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomMain.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.mainColor = App._colorPage;
              this.customMainColor = App._colorPage;
              break;
            case 2:
              this.btnThemeColorForegroundBorder.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.btnThemeColorForegroundBorder.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.btnThemeColorMainBorder.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.btnThemeColorHeaderBorder.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomForeground.Background = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomHeader.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomMain.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.borderCustomForeground.BorderBrush = (Brush) toColorConverter.Convert((object) App._colorPage, (Type) null, (object) null, (CultureInfo) null);
              this.foregroundColor = App._colorPage;
              this.customForegroundColor = App._colorPage;
              break;
          }
          this.updateSample();
          ((UIElement) this).Opacity = 1.0;
        }
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

    private void backKeyPress()
    {
      this.showTransitionOutBackward();
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(AppResources.msgBoxThemeRestart, AppResources.msgBoxThemeRestartCaption, (MessageBoxButton) 1) != 1)
        return;
      AppSettings appSettings = new AppSettings()
      {
        themeColorMain = this.mainColor,
        themeColorForeground = this.foregroundColor,
        themeColorHeader = this.headerColor,
        themeColorCustomHeader = this.customHeaderColor,
        themeColorCustomMain = this.customMainColor,
        themeColorCustomForeground = this.customForegroundColor,
        themeListThemeSelection = this.listPickerThemes.SelectedIndex
      };
      Application.Current.Terminate();
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.backKeyPress();

    private void listPickerThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (this.listPickerThemes == null)
        return;
      AppSettings appSettings = new AppSettings();
      ((UIElement) this.stackPanelCustomColors).Visibility = (Visibility) 1;
      switch (this.listPickerThemes.SelectedIndex)
      {
        case 0:
          this.mainColor = "#FFF0F7FF";
          this.headerColor = "#FFD2E5FB";
          this.foregroundColor = "#FF006BA8";
          break;
        case 1:
          this.mainColor = "#FF0F2028";
          this.headerColor = "#FF294551";
          this.foregroundColor = "#FF42C7EA";
          break;
        case 2:
          this.mainColor = "#FF000000";
          this.headerColor = "#FF484848";
          this.foregroundColor = "#FFFFFFFF";
          break;
        case 3:
          this.mainColor = "#FFFFFFFF";
          this.headerColor = "#FFEEEEEE";
          this.foregroundColor = "#FF000000";
          break;
        case 4:
          this.mainColor = this.customMainColor;
          this.headerColor = this.customHeaderColor;
          this.foregroundColor = this.customForegroundColor;
          ((UIElement) this.stackPanelCustomColors).Visibility = ((UIElement) this).Visibility;
          break;
      }
      this.updateSample();
    }

    private void updateSample()
    {
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) this.headerColor, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) this.foregroundColor, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) this.mainColor, (Type) null, (object) null, (CultureInfo) null);
      ((Shape) this.ejBorder).Stroke = (Brush) solidColorBrush2;
      ((Shape) this.ejBlockHeader).Fill = (Brush) solidColorBrush1;
      this.ejTxtHeader.Foreground = (Brush) solidColorBrush2;
      ((Shape) this.ejBlockMain).Fill = (Brush) solidColorBrush3;
      this.ejTxtMain.Foreground = (Brush) solidColorBrush2;
      ((Shape) this.ejBlockAppBar).Fill = (Brush) solidColorBrush3;
      ((Control) this.ejBtnAppBar).BorderBrush = (Brush) solidColorBrush2;
      ((Shape) this.ejBtnAppBarIcon).Fill = (Brush) solidColorBrush2;
    }

    private string colorToString(Color color) => "#" + color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");

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

    private void btnThemeColorForeground_Tap(object sender, GestureEventArgs e)
    {
      App._colorPage = ((SolidColorBrush) this.btnThemeColorForegroundBorder.Background).Color.ToString();
      App._colorPageType = 2;
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/colorPickerPage.xaml?notificationAlarm", UriKind.Relative))));
    }

    private void btnThemeColorHeader_Tap(object sender, GestureEventArgs e)
    {
      App._colorPage = ((SolidColorBrush) this.btnThemeColorHeaderBorder.Background).Color.ToString();
      App._colorPageType = 0;
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/colorPickerPage.xaml?notificationAlarm", UriKind.Relative))));
    }

    private void btnThemeColorMain_Tap(object sender, GestureEventArgs e)
    {
      App._colorPage = ((SolidColorBrush) this.btnThemeColorMainBorder.Background).Color.ToString();
      App._colorPageType = 1;
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/colorPickerPage.xaml?notificationAlarm", UriKind.Relative))));
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confThemePage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.listPickerThemes = (ListPicker) ((FrameworkElement) this).FindName("listPickerThemes");
      this.borderCustomHeader = (Border) ((FrameworkElement) this).FindName("borderCustomHeader");
      this.borderCustomMain = (Border) ((FrameworkElement) this).FindName("borderCustomMain");
      this.borderCustomForeground = (Border) ((FrameworkElement) this).FindName("borderCustomForeground");
      this.stackPanelCustomColors = (StackPanel) ((FrameworkElement) this).FindName("stackPanelCustomColors");
      this.btnThemeColorHeader = (Button) ((FrameworkElement) this).FindName("btnThemeColorHeader");
      this.btnThemeColorHeaderBorder = (Border) ((FrameworkElement) this).FindName("btnThemeColorHeaderBorder");
      this.btnThemeColorMain = (Button) ((FrameworkElement) this).FindName("btnThemeColorMain");
      this.btnThemeColorMainBorder = (Border) ((FrameworkElement) this).FindName("btnThemeColorMainBorder");
      this.btnThemeColorForeground = (Button) ((FrameworkElement) this).FindName("btnThemeColorForeground");
      this.btnThemeColorForegroundBorder = (Border) ((FrameworkElement) this).FindName("btnThemeColorForegroundBorder");
      this.ejBorder = (Rectangle) ((FrameworkElement) this).FindName("ejBorder");
      this.ejBlockHeader = (Rectangle) ((FrameworkElement) this).FindName("ejBlockHeader");
      this.ejTxtHeader = (TextBlock) ((FrameworkElement) this).FindName("ejTxtHeader");
      this.ejBlockMain = (Rectangle) ((FrameworkElement) this).FindName("ejBlockMain");
      this.ejTxtMain = (TextBlock) ((FrameworkElement) this).FindName("ejTxtMain");
      this.ejBlockAppBar = (Rectangle) ((FrameworkElement) this).FindName("ejBlockAppBar");
      this.ejBtnAppBar = (Button) ((FrameworkElement) this).FindName("ejBtnAppBar");
      this.ejBtnAppBarIcon = (Rectangle) ((FrameworkElement) this).FindName("ejBtnAppBarIcon");
    }
        */
  }
}
