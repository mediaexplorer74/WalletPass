// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.WriteableBitmapContextExtensions
// Assembly: WriteableBitmapExWinPhone, Version=1.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B0ADAD6-8531-4A99-AF69-71B377344AB9
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.xml

namespace System.Windows.Media.Imaging
{
  /// <summary>Provides the WriteableBitmap context pixel data</summary>
  public static class WriteableBitmapContextExtensions
  {
    /// <summary>
    /// Gets a BitmapContext within which to perform nested IO operations on the bitmap
    /// </summary>
    /// <remarks>For WPF the BitmapContext will lock the bitmap. Call Dispose on the context to unlock</remarks>
    /// <param name="bmp"></param>
    /// <returns></returns>
    public static BitmapContext GetBitmapContext(this WriteableBitmap bmp) => new BitmapContext(bmp);

    /// <summary>
    /// Gets a BitmapContext within which to perform nested IO operations on the bitmap
    /// </summary>
    /// <remarks>For WPF the BitmapContext will lock the bitmap. Call Dispose on the context to unlock</remarks>
    /// <param name="bmp">The bitmap.</param>
    /// <param name="mode">The ReadWriteMode. If set to ReadOnly, the bitmap will not be invalidated on dispose of the context, else it will</param>
    /// <returns></returns>
    public static BitmapContext GetBitmapContext(this WriteableBitmap bmp, ReadWriteMode mode) => new BitmapContext(bmp, mode);
  }
}
