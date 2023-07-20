// Decompiled with JetBrains decompiler
// Type: WalletPass.WideTileControl
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WalletPass.ToolStackPNGWriterLib;

namespace WalletPass
{
  public class WideTileControl : UserControl
  {
    private bool back;
    internal Grid LayoutRoot;
    internal Grid FrontTileBoardingPass;
    internal Image boardAppLogoW;
    internal Image boardAppLogoK;
    internal TextBlock boardTXTDay;
    internal TextBlock boardTXTHour;
    internal TextBlock boardTXTPrimarylbl;
    internal TextBlock boardTXTPrimarylbl1;
    internal TextBlock boardTXTPrimary;
    internal TextBlock boardTXTPrimary1;
    internal Image boardimgCalendar;
    internal Image boardimgSwitch;
    internal TextBlock boardTXTOrg;
    internal Grid FrontTileCoupon;
    internal Image couponBackground;
    internal Image couponAppLogoW;
    internal Image couponAppLogoK;
    internal Image couponStrip;
    internal Image couponimgInfo;
    internal TextBlock couponTXTPrimarylbl;
    internal TextBlock couponTXTPrimary;
    internal Image couponimgCalendar;
    internal TextBlock couponTXTDay;
    internal TextBlock couponTXTHour;
    internal Grid BackTile;
    internal Image backBackground;
    internal TextBlock headerLBL;
    internal TextBlock headerLBL1;
    internal TextBlock headerTXT;
    internal TextBlock headerTXT1;
    internal TextBlock BackTileTXTOrg;
    internal ImageBrush BackTileIMGOrg;
    private bool _contentLoaded;

    public WideTileControl(bool _back)
    {
      this.InitializeComponent();
      this.back = _back;
    }

    public event EventHandler<SaveJpegCompleteEventArgs> SaveJpegComplete;

    public void BeginSaveJpeg()
    {
      new BitmapImage(new Uri("DefaultWideTile.jpg", UriKind.Relative)).CreateOptions = (BitmapCreateOptions) 0;
      if (!this.back)
      {
        this.createFrontTile();
        ((UIElement) this).UpdateLayout();
        ((UIElement) this).Measure(new Size(691.0, 336.0));
        ((UIElement) this).Arrange(new Rect(0.0, 0.0, 691.0, 336.0));
        WriteableBitmap image = new WriteableBitmap(691, 336);
        image.Render((UIElement) this.LayoutRoot, (Transform) null);
        image.Invalidate();
        string str = "WideTileFront_" + App._tempPassClass.serialNumberGUID + ".png";
        PNGWriter pngWriter = new PNGWriter();
        IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
        if (!storeForApplication.DirectoryExists("shared/shellcontent"))
          storeForApplication.CreateDirectory("shared/shellcontent");
        using (IsolatedStorageFileStream file = storeForApplication.CreateFile("shared/shellcontent/" + str))
        {
          pngWriter.WritePNG(image, (Stream) file);
          file.Close();
        }
        if (this.SaveJpegComplete == null)
          return;
        this.SaveJpegComplete((object) this, new SaveJpegCompleteEventArgs(true, "shared/shellcontent/" + str));
      }
      else
      {
        this.createBackTile();
        ((UIElement) this).UpdateLayout();
        ((UIElement) this).Measure(new Size(691.0, 336.0));
        ((UIElement) this).Arrange(new Rect(0.0, 0.0, 691.0, 336.0));
        WriteableBitmap image = new WriteableBitmap(691, 336);
        image.Render((UIElement) this.LayoutRoot, (Transform) null);
        image.Invalidate();
        string str = "WideTileBack_" + Guid.NewGuid().ToString() + ".png";
        PNGWriter pngWriter = new PNGWriter();
        IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication();
        if (!storeForApplication.DirectoryExists("shared/shellcontent"))
          storeForApplication.CreateDirectory("shared/shellcontent");
        using (IsolatedStorageFileStream file = storeForApplication.CreateFile("shared/shellcontent/" + str))
        {
          pngWriter.WritePNG(image, (Stream) file);
          file.Close();
        }
        if (this.SaveJpegComplete == null)
          return;
        this.SaveJpegComplete((object) this, new SaveJpegCompleteEventArgs(true, "shared/shellcontent/" + str));
      }
    }

    private void createFrontTile()
    {
      switch (App._tempPassClass.type)
      {
        case "boardingPass":
          this.createFrontBoardingTile();
          break;
        default:
          this.createFrontCouponTile();
          break;
      }
    }

    private void createFrontBoardingTile()
    {
      AppSettings appSettings = new AppSettings();
      SolidColorBrush solidColorBrush1 = new SolidColorBrush();
      SolidColorBrush solidColorBrush2 = new SolidColorBrush();
      ((UIElement) this.FrontTileBoardingPass).Visibility = (Visibility) 0;
      ((UIElement) this.FrontTileCoupon).Visibility = (Visibility) 1;
      ((UIElement) this.BackTile).Visibility = (Visibility) 1;
      if (appSettings.tileBackground == 0)
      {
        ((Panel) this.FrontTileBoardingPass).Background = (Brush) new SolidColorBrush(App._tempPassClass.backgroundColor);
        solidColorBrush1 = new SolidColorBrush(App._tempPassClass.foregroundColor);
        solidColorBrush2 = new SolidColorBrush(App._tempPassClass.labelColor);
        if (App._tempPassClass.backgroundColor.R > (byte) 210 & App._tempPassClass.backgroundColor.G > (byte) 210 & App._tempPassClass.backgroundColor.B > (byte) 210)
        {
          ((UIElement) this.boardAppLogoW).Visibility = (Visibility) 1;
          ((UIElement) this.boardAppLogoK).Visibility = (Visibility) 0;
          this.boardimgCalendar.Source = (ImageSource) new BitmapImage(new Uri("/Assets/Tiles/appbar.calendarK.png", UriKind.Relative));
          this.boardimgSwitch.Source = (ImageSource) new BitmapImage(new Uri("/Assets/Tiles/appbar.switchK.png", UriKind.Relative));
        }
        else
        {
          ((UIElement) this.boardAppLogoW).Visibility = (Visibility) 0;
          ((UIElement) this.boardAppLogoK).Visibility = (Visibility) 1;
        }
      }
      else if (appSettings.tileBackground == 1)
      {
        ((Panel) this.FrontTileBoardingPass).Background = (Brush) new SolidColorBrush(Colors.Transparent);
        solidColorBrush1 = new SolidColorBrush(Colors.White);
        solidColorBrush2 = new SolidColorBrush(Colors.White);
        ((UIElement) this.boardAppLogoW).Visibility = (Visibility) 0;
        ((UIElement) this.boardAppLogoK).Visibility = (Visibility) 1;
      }
      this.boardTXTDay.Text = App._tempPassClass.relevantDayDay;
      this.boardTXTDay.Foreground = (Brush) solidColorBrush1;
      this.boardTXTHour.Text = App._tempPassClass.relevantDayHour;
      this.boardTXTHour.Foreground = (Brush) solidColorBrush1;
      this.boardTXTPrimarylbl.Text = App._tempPassClass.checkLabel(App._tempPassClass.PrimaryFields[0].Label);
      this.boardTXTPrimarylbl.Foreground = (Brush) solidColorBrush2;
      this.boardTXTPrimarylbl1.Text = App._tempPassClass.checkLabel(App._tempPassClass.PrimaryFields[1].Label);
      this.boardTXTPrimarylbl1.Foreground = (Brush) solidColorBrush2;
      this.boardTXTPrimary.Text = App._tempPassClass.PrimaryFields[0].Value;
      this.boardTXTPrimary.Foreground = (Brush) solidColorBrush1;
      this.boardTXTPrimary1.Text = App._tempPassClass.PrimaryFields[1].Value;
      this.boardTXTPrimary1.Foreground = (Brush) solidColorBrush1;
      this.boardTXTOrg.Text = this.correctText(App._tempPassClass.organizationName);
      this.boardTXTOrg.Foreground = (Brush) solidColorBrush1;
      if (!string.IsNullOrEmpty(App._tempPassClass.relevantDayDay))
        return;
      ((UIElement) this.boardimgCalendar).Visibility = (Visibility) 1;
    }

    private void createFrontCouponTile()
    {
      AppSettings appSettings = new AppSettings();
      SolidColorBrush solidColorBrush1 = new SolidColorBrush();
      SolidColorBrush solidColorBrush2 = new SolidColorBrush();
      ((UIElement) this.FrontTileBoardingPass).Visibility = (Visibility) 1;
      ((UIElement) this.FrontTileCoupon).Visibility = (Visibility) 0;
      ((UIElement) this.BackTile).Visibility = (Visibility) 1;
      if (appSettings.tileBackground == 0)
      {
        ((Panel) this.FrontTileCoupon).Background = (Brush) new SolidColorBrush(App._tempPassClass.backgroundColor);
        solidColorBrush1 = new SolidColorBrush(App._tempPassClass.foregroundColor);
        solidColorBrush2 = new SolidColorBrush(App._tempPassClass.labelColor);
        if (App._tempPassClass.backgroundColor.R > (byte) 210 & App._tempPassClass.backgroundColor.G > (byte) 210 & App._tempPassClass.backgroundColor.B > (byte) 210)
        {
          ((UIElement) this.couponAppLogoW).Visibility = (Visibility) 1;
          ((UIElement) this.couponAppLogoK).Visibility = (Visibility) 0;
          this.couponimgCalendar.Source = (ImageSource) new BitmapImage(new Uri("/Assets/Tiles/appbar.calendarK.png", UriKind.Relative));
          this.couponimgInfo.Source = (ImageSource) new BitmapImage(new Uri("/Assets/Tiles/appbar.informationK.png", UriKind.Relative));
        }
        else
        {
          ((UIElement) this.couponAppLogoW).Visibility = (Visibility) 0;
          ((UIElement) this.couponAppLogoK).Visibility = (Visibility) 1;
        }
      }
      else if (appSettings.tileBackground == 1)
      {
        ((Panel) this.FrontTileCoupon).Background = (Brush) new SolidColorBrush(Colors.Transparent);
        solidColorBrush1 = new SolidColorBrush(Colors.White);
        solidColorBrush2 = new SolidColorBrush(Colors.White);
        ((UIElement) this.couponAppLogoW).Visibility = (Visibility) 0;
        ((UIElement) this.couponAppLogoK).Visibility = (Visibility) 1;
        ((UIElement) this.couponBackground).Visibility = (Visibility) 1;
      }
      this.couponTXTDay.Text = App._tempPassClass.relevantDayDay;
      this.couponTXTDay.Foreground = (Brush) solidColorBrush1;
      this.couponTXTHour.Text = App._tempPassClass.relevantDayHour;
      this.couponTXTHour.Foreground = (Brush) solidColorBrush1;
      if (App._tempPassClass.PrimaryFields.Count > 0)
      {
        this.couponTXTPrimarylbl.Text = App._tempPassClass.checkLabel(App._tempPassClass.PrimaryFields[0].Label);
        this.couponTXTPrimarylbl.Foreground = (Brush) solidColorBrush2;
        this.couponTXTPrimary.Text = App._tempPassClass.PrimaryFields[0].Value;
        this.couponTXTPrimary.Foreground = (Brush) solidColorBrush1;
        ((UIElement) this.couponimgInfo).Visibility = (Visibility) 0;
      }
      this.couponStrip.Source = (ImageSource) App._tempPassClass.stripImage;
      this.couponBackground.Source = (ImageSource) App._tempPassClass.backgroundImage;
      if (!string.IsNullOrEmpty(App._tempPassClass.relevantDayDay))
        return;
      ((UIElement) this.couponimgCalendar).Visibility = (Visibility) 1;
    }

    private void createBackTile()
    {
      AppSettings appSettings = new AppSettings();
      SolidColorBrush solidColorBrush1 = new SolidColorBrush();
      SolidColorBrush solidColorBrush2 = new SolidColorBrush();
      ((UIElement) this.FrontTileBoardingPass).Visibility = (Visibility) 1;
      ((UIElement) this.FrontTileCoupon).Visibility = (Visibility) 1;
      ((UIElement) this.BackTile).Visibility = (Visibility) 0;
      if (appSettings.tileBackground == 0)
      {
        ((Panel) this.BackTile).Background = (Brush) new SolidColorBrush(App._tempPassClass.backgroundColor);
        solidColorBrush1 = new SolidColorBrush(App._tempPassClass.foregroundColor);
        solidColorBrush2 = new SolidColorBrush(App._tempPassClass.labelColor);
      }
      else if (appSettings.tileBackground == 1)
      {
        ((Panel) this.BackTile).Background = (Brush) new SolidColorBrush(Colors.Transparent);
        solidColorBrush1 = new SolidColorBrush(Colors.White);
        solidColorBrush2 = new SolidColorBrush(Colors.White);
        ((UIElement) this.backBackground).Visibility = (Visibility) 1;
      }
      if (App._tempPassClass.HeaderFields.Count == 1)
      {
        this.headerLBL1.Text = App._tempPassClass.checkLabel(App._tempPassClass.HeaderFields[0].Label);
        this.headerLBL1.Foreground = (Brush) solidColorBrush2;
        this.headerTXT1.Text = App._tempPassClass.HeaderFields[0].Value;
        this.headerTXT1.Foreground = (Brush) solidColorBrush1;
      }
      else if (App._tempPassClass.HeaderFields.Count > 1)
      {
        this.headerLBL.Text = App._tempPassClass.checkLabel(App._tempPassClass.HeaderFields[0].Label);
        this.headerLBL.Foreground = (Brush) solidColorBrush2;
        this.headerTXT.Text = App._tempPassClass.HeaderFields[0].Value;
        this.headerTXT.Foreground = (Brush) solidColorBrush1;
        this.headerLBL1.Text = App._tempPassClass.checkLabel(App._tempPassClass.HeaderFields[1].Label);
        this.headerLBL1.Foreground = (Brush) solidColorBrush2;
        this.headerTXT1.Text = App._tempPassClass.HeaderFields[1].Value;
        this.headerTXT1.Foreground = (Brush) solidColorBrush1;
      }
      this.BackTileTXTOrg.Text = this.correctText(App._tempPassClass.organizationName);
      this.BackTileTXTOrg.Foreground = (Brush) solidColorBrush1;
      this.BackTileIMGOrg.ImageSource = (ImageSource) App._tempPassClass.iconImage;
      this.backBackground.Source = (ImageSource) App._tempPassClass.backgroundImage;
    }

    private string correctText(string Text)
    {
      Text = Text.ToLower();
      Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);
      for (int index = Text.IndexOf(" "); index != -1; index = Text.IndexOf(" ", index + 1))
        Text = Text.Substring(0, index + 1) + Text.Substring(index + 1, 1).ToUpper() + Text.Substring(index + 2);
      return Text;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/Tiles/WideTileControl.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.FrontTileBoardingPass = (Grid) ((FrameworkElement) this).FindName("FrontTileBoardingPass");
      this.boardAppLogoW = (Image) ((FrameworkElement) this).FindName("boardAppLogoW");
      this.boardAppLogoK = (Image) ((FrameworkElement) this).FindName("boardAppLogoK");
      this.boardTXTDay = (TextBlock) ((FrameworkElement) this).FindName("boardTXTDay");
      this.boardTXTHour = (TextBlock) ((FrameworkElement) this).FindName("boardTXTHour");
      this.boardTXTPrimarylbl = (TextBlock) ((FrameworkElement) this).FindName("boardTXTPrimarylbl");
      this.boardTXTPrimarylbl1 = (TextBlock) ((FrameworkElement) this).FindName("boardTXTPrimarylbl1");
      this.boardTXTPrimary = (TextBlock) ((FrameworkElement) this).FindName("boardTXTPrimary");
      this.boardTXTPrimary1 = (TextBlock) ((FrameworkElement) this).FindName("boardTXTPrimary1");
      this.boardimgCalendar = (Image) ((FrameworkElement) this).FindName("boardimgCalendar");
      this.boardimgSwitch = (Image) ((FrameworkElement) this).FindName("boardimgSwitch");
      this.boardTXTOrg = (TextBlock) ((FrameworkElement) this).FindName("boardTXTOrg");
      this.FrontTileCoupon = (Grid) ((FrameworkElement) this).FindName("FrontTileCoupon");
      this.couponBackground = (Image) ((FrameworkElement) this).FindName("couponBackground");
      this.couponAppLogoW = (Image) ((FrameworkElement) this).FindName("couponAppLogoW");
      this.couponAppLogoK = (Image) ((FrameworkElement) this).FindName("couponAppLogoK");
      this.couponStrip = (Image) ((FrameworkElement) this).FindName("couponStrip");
      this.couponimgInfo = (Image) ((FrameworkElement) this).FindName("couponimgInfo");
      this.couponTXTPrimarylbl = (TextBlock) ((FrameworkElement) this).FindName("couponTXTPrimarylbl");
      this.couponTXTPrimary = (TextBlock) ((FrameworkElement) this).FindName("couponTXTPrimary");
      this.couponimgCalendar = (Image) ((FrameworkElement) this).FindName("couponimgCalendar");
      this.couponTXTDay = (TextBlock) ((FrameworkElement) this).FindName("couponTXTDay");
      this.couponTXTHour = (TextBlock) ((FrameworkElement) this).FindName("couponTXTHour");
      this.BackTile = (Grid) ((FrameworkElement) this).FindName("BackTile");
      this.backBackground = (Image) ((FrameworkElement) this).FindName("backBackground");
      this.headerLBL = (TextBlock) ((FrameworkElement) this).FindName("headerLBL");
      this.headerLBL1 = (TextBlock) ((FrameworkElement) this).FindName("headerLBL1");
      this.headerTXT = (TextBlock) ((FrameworkElement) this).FindName("headerTXT");
      this.headerTXT1 = (TextBlock) ((FrameworkElement) this).FindName("headerTXT1");
      this.BackTileTXTOrg = (TextBlock) ((FrameworkElement) this).FindName("BackTileTXTOrg");
      this.BackTileIMGOrg = (ImageBrush) ((FrameworkElement) this).FindName("BackTileIMGOrg");
    }
  }
}
