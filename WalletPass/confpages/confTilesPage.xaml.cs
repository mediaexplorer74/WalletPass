// Decompiled with JetBrains decompiler
// Type: WalletPass.confTilesPage
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
//using System.Windows.Controls.Primitives;
//using System.Windows.Media;
//using System.Windows.Navigation;
using WalletPass.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace WalletPass
{
  public sealed partial class confTilesPage : Page
  {
    private int changeTileColor;
    private Popup _popup;
    private TileUpdate tileCreat;

    //internal Grid LayoutRoot;
    //internal RadioButton btnTileColorPassbook;
    //internal RadioButton btnTileColorTransp;
    //private bool _contentLoaded;

    public confTilesPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
      this.changeTileColor = new AppSettings().tileBackground;
      if (this.changeTileColor == 0)
        ((ToggleButton) this.btnTileColorPassbook).IsChecked = new bool?(true);
      else
        ((ToggleButton) this.btnTileColorTransp).IsChecked = new bool?(true);
      this.tileCreat = new TileUpdate();
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

    private void btnTileColorPassbook_Checked(object sender, RoutedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (!((ToggleButton) this.btnTileColorPassbook).IsChecked.Value)
        return;
      appSettings.tileBackground = 0;
    }

    private void btnTileColorTransp_Checked(object sender, RoutedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (!((ToggleButton) this.btnTileColorTransp).IsChecked.Value)
        return;
      appSettings.tileBackground = 1;
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      base.OnBackKeyPress(e);
      if (this.changeTileColor == new AppSettings().tileBackground)
        return;
      this.showSplash();
    }

    private void showSplash()
    {
      this._popup = new Popup();
      this._popup.Child = (UIElement) new splashUpdTilesControl(AppResources.splashUpdateTile);
      this._popup.IsOpen = true;
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.StartLoadingData()));
    }

    public void StartLoadingData()
    {
      foreach (ShellTile activeTile in ShellTile.ActiveTiles)
      {
        if (activeTile.NavigationUri.ToString().Contains("SecondaryTile") && activeTile.NavigationUri.ToString() != "/")
        {
          App._tempPassClass = App._passcollection.returnPass(activeTile.NavigationUri.ToString().Substring(activeTile.NavigationUri.ToString().IndexOf("=") + 1));
          this.tileCreat.RenderWideTile();
          this.tileCreat.RenderMediumTile();
          this.tileCreat.RenderSmallTile();
          FlipTileData flipTileData1 = new FlipTileData();
          ((ShellTileData) flipTileData1).Title = "";
          ((StandardTileData) flipTileData1).BackgroundImage = this.tileCreat.ImageFront;
          ((StandardTileData) flipTileData1).BackBackgroundImage = this.tileCreat.ImageBack;
          flipTileData1.WideBackgroundImage = this.tileCreat.WideImageFront;
          flipTileData1.WideBackBackgroundImage = this.tileCreat.WideImageBack;
          FlipTileData flipTileData2 = flipTileData1;
          activeTile.Update((ShellTileData) flipTileData2);
        }
      }
      this._popup.IsOpen = false;
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      this.showTransitionOutBackward();
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

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confTilesPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnTileColorPassbook = (RadioButton) ((FrameworkElement) this).FindName("btnTileColorPassbook");
      this.btnTileColorTransp = (RadioButton) ((FrameworkElement) this).FindName("btnTileColorTransp");
    }
        */
  }
}
