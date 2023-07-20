// Decompiled with JetBrains decompiler
// Type: WalletPass.splashUpdTilesControl
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WalletPass
{
  public class splashUpdTilesControl : UserControl
  {
    internal Grid LayoutRoot;
    internal SolidColorBrush LayoutColor;
    internal TextBlock txtSplash;
    internal TextBlock txtSplashTemp;
    internal ProgressBar progressBar;
    private bool _contentLoaded;

    public splashUpdTilesControl(
      string text,
      string textTemp = "",
      string BackgroundColor = "",
      string ForegroundColor = "")
    {
      this.InitializeComponent();
      double actualHeight = Application.Current.Host.Content.ActualHeight;
      double actualWidth = Application.Current.Host.Content.ActualWidth;
      ((FrameworkElement) this).Height = actualHeight;
      ((FrameworkElement) this).Width = actualWidth;
      this.txtSplash.Text = text;
      if (!string.IsNullOrEmpty(textTemp))
      {
        ((UIElement) this.txtSplashTemp).Visibility = (Visibility) 0;
        this.txtSplashTemp.Text = textTemp;
      }
      else
        ((UIElement) this.txtSplashTemp).Visibility = (Visibility) 1;
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      if (string.IsNullOrEmpty(BackgroundColor))
        BackgroundColor = appSettings.themeColorCustomMain;
      if (string.IsNullOrEmpty(ForegroundColor))
        ForegroundColor = appSettings.themeColorForeground;
      this.LayoutColor.Color = ((SolidColorBrush) toColorConverter.Convert((object) BackgroundColor, (Type) null, (object) null, (CultureInfo) null)).Color;
      this.txtSplash.Foreground = (Brush) toColorConverter.Convert((object) ForegroundColor, (Type) null, (object) null, (CultureInfo) null);
      ((Control) this.progressBar).Foreground = (Brush) toColorConverter.Convert((object) ForegroundColor, (Type) null, (object) null, (CultureInfo) null);
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/Tiles/splashUpdTilesControl.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.LayoutColor = (SolidColorBrush) ((FrameworkElement) this).FindName("LayoutColor");
      this.txtSplash = (TextBlock) ((FrameworkElement) this).FindName("txtSplash");
      this.txtSplashTemp = (TextBlock) ((FrameworkElement) this).FindName("txtSplashTemp");
      this.progressBar = (ProgressBar) ((FrameworkElement) this).FindName("progressBar");
    }
  }
}
