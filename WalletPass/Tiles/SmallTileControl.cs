// Decompiled with JetBrains decompiler
// Type: WalletPass.SmallTileControl
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
  public class SmallTileControl : UserControl
  {
    internal Grid LayoutRoot;
    internal Image IconImage;
    private bool _contentLoaded;

    public SmallTileControl() => this.InitializeComponent();

    public event EventHandler<SaveJpegCompleteEventArgs> SaveJpegComplete;

    public void BeginSaveJpeg()
    {
      new BitmapImage(new Uri("DefaultSmallTile.jpg", UriKind.Relative)).CreateOptions = (BitmapCreateOptions) 0;
      this.IconImage.Source = (ImageSource) App._tempPassClass.iconImage;
      ((UIElement) this).UpdateLayout();
      ((UIElement) this).Measure(new Size(159.0, 159.0));
      ((UIElement) this).Arrange(new Rect(0.0, 0.0, 159.0, 159.0));
      WriteableBitmap image = new WriteableBitmap(159, 159);
      image.Render((UIElement) this.LayoutRoot, (Transform) null);
      image.Invalidate();
      string str = "SmallTileFront_" + App._tempPassClass.serialNumberGUID + ".png";
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

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/Tiles/SmallTileControl.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.IconImage = (Image) ((FrameworkElement) this).FindName("IconImage");
    }
  }
}
