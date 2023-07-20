// Decompiled with JetBrains decompiler
// Type: Windows.UI.Xaml.Media.Imaging.BitmapFactory
// Assembly: WriteableBitmapEx.WinRT, Version=1.5.0.0, Culture=neutral, PublicKeyToken=50375ca6144f1c69
// MVID: 322E7EEB-6AEE-4710-AEEF-B781C66D0422
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapEx.WinRT.dll

namespace Windows.UI.Xaml.Media.Imaging
{
  public static class BitmapFactory
  {
    public static WriteableBitmap New(int pixelWidth, int pixelHeight)
    {
      if (pixelHeight < 1)
        pixelHeight = 1;
      if (pixelWidth < 1)
        pixelWidth = 1;
      return new WriteableBitmap(pixelWidth, pixelHeight);
    }
  }
}
