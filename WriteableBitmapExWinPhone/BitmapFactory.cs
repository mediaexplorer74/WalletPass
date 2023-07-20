// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.BitmapFactory
// Assembly: WriteableBitmapExWinPhone, Version=1.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B0ADAD6-8531-4A99-AF69-71B377344AB9
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.xml

namespace System.Windows.Media.Imaging
{
  /// <summary>Cross-platform factory for WriteableBitmaps</summary>
  public static class BitmapFactory
  {
    /// <summary>
    /// Creates a new WriteableBitmap of the specified width and height
    /// </summary>
    /// <remarks>For WPF the default DPI is 96x96 and PixelFormat is Pbgra32</remarks>
    /// <param name="pixelWidth"></param>
    /// <param name="pixelHeight"></param>
    /// <returns></returns>
    public static WriteableBitmap New(int pixelWidth, int pixelHeight) => new WriteableBitmap(pixelWidth, pixelHeight);
  }
}
