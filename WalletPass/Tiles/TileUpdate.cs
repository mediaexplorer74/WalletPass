// Decompiled with JetBrains decompiler
// Type: WalletPass.TileUpdate
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;

namespace WalletPass
{
  internal class TileUpdate
  {
    public Uri WideImageFront { get; set; }

    public Uri WideImageBack { get; set; }

    public Uri ImageFront { get; set; }

    public Uri ImageBack { get; set; }

    public Uri SmallImage { get; set; }

    public void RenderWideTile()
    {
      WideTileControl wideTileControl1 = new WideTileControl(false);
      WideTileControl wideTileControl2 = new WideTileControl(true);
      wideTileControl1.SaveJpegComplete += (EventHandler<SaveJpegCompleteEventArgs>) ((s, args) =>
      {
        try
        {
          if (!args.success)
            return;
          this.WideImageFront = new Uri("isostore:/" + args.imageFilename, UriKind.Absolute);
        }
        catch
        {
        }
      });
      wideTileControl1.BeginSaveJpeg();
      wideTileControl2.SaveJpegComplete += (EventHandler<SaveJpegCompleteEventArgs>) ((s, args) =>
      {
        try
        {
          if (!args.success)
            return;
          this.WideImageBack = new Uri("isostore:/" + args.imageFilename, UriKind.Absolute);
        }
        catch
        {
        }
      });
      wideTileControl2.BeginSaveJpeg();
    }

    public void RenderMediumTile()
    {
      MediumTileControl mediumTileControl1 = new MediumTileControl(false);
      MediumTileControl mediumTileControl2 = new MediumTileControl(true);
      mediumTileControl1.SaveJpegComplete += (EventHandler<SaveJpegCompleteEventArgs>) ((s, args) =>
      {
        try
        {
          if (!args.success)
            return;
          this.ImageFront = new Uri("isostore:/" + args.imageFilename, UriKind.Absolute);
        }
        catch
        {
        }
      });
      mediumTileControl1.BeginSaveJpeg();
      mediumTileControl2.SaveJpegComplete += (EventHandler<SaveJpegCompleteEventArgs>) ((s, args) =>
      {
        try
        {
          if (!args.success)
            return;
          this.ImageBack = new Uri("isostore:/" + args.imageFilename, UriKind.Absolute);
        }
        catch
        {
        }
      });
      mediumTileControl2.BeginSaveJpeg();
    }

    public void RenderSmallTile()
    {
      SmallTileControl smallTileControl = new SmallTileControl();
      smallTileControl.SaveJpegComplete += (EventHandler<SaveJpegCompleteEventArgs>) ((s, args) =>
      {
        try
        {
          if (!args.success)
            return;
          this.SmallImage = new Uri("isostore:/" + args.imageFilename, UriKind.Absolute);
        }
        catch
        {
        }
      });
      smallTileControl.BeginSaveJpeg();
    }
  }
}
