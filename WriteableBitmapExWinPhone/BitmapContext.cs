// Decompiled with JetBrains decompiler
// Type: System.Windows.Media.Imaging.BitmapContext
// Assembly: WriteableBitmapExWinPhone, Version=1.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B0ADAD6-8531-4A99-AF69-71B377344AB9
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapExWinPhone.xml

namespace System.Windows.Media.Imaging
{
  /// <summary>
  /// A disposable cross-platform wrapper around a WriteableBitmap, allowing a common API for Silverlight + WPF with locking + unlocking if necessary
  /// </summary>
  /// <remarks>Attempting to put as many preprocessor hacks in this file, to keep the rest of the codebase relatively clean</remarks>
  public struct BitmapContext : IDisposable
  {
    private readonly WriteableBitmap writeableBitmap;
    private readonly ReadWriteMode mode;

    /// <summary>The Bitmap</summary>
    public WriteableBitmap WriteableBitmap => this.writeableBitmap;

    /// <summary>Width of the bitmap</summary>
    public int Width => ((BitmapSource) this.writeableBitmap).PixelWidth;

    /// <summary>Height of the bitmap</summary>
    public int Height => ((BitmapSource) this.writeableBitmap).PixelHeight;

    /// <summary>
    /// Creates an instance of a BitmapContext, with default mode = ReadWrite
    /// </summary>
    /// <param name="writeableBitmap"></param>
    public BitmapContext(WriteableBitmap writeableBitmap)
      : this(writeableBitmap, ReadWriteMode.ReadWrite)
    {
    }

    /// <summary>
    /// Creates an instance of a BitmapContext, with specified ReadWriteMode
    /// </summary>
    /// <param name="writeableBitmap"></param>
    /// <param name="mode"></param>
    public BitmapContext(WriteableBitmap writeableBitmap, ReadWriteMode mode)
    {
      this.writeableBitmap = writeableBitmap;
      this.mode = mode;
    }

    /// <summary>Gets the Pixels array</summary>
    public int[] Pixels => this.writeableBitmap.Pixels;

    /// <summary>Gets the length of the Pixels array</summary>
    public int Length => this.writeableBitmap.Pixels.Length;

    /// <summary>
    /// Performs a Copy operation from source BitmapContext to destination BitmapContext
    /// </summary>
    /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
    public static void BlockCopy(
      BitmapContext src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy((Array) src.Pixels, srcOffset, (Array) dest.Pixels, destOffset, count);
    }

    /// <summary>
    /// Performs a Copy operation from source Array to destination BitmapContext
    /// </summary>
    /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
    public static void BlockCopy(
      Array src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy(src, srcOffset, (Array) dest.Pixels, destOffset, count);
    }

    /// <summary>
    /// Performs a Copy operation from source BitmapContext to destination Array
    /// </summary>
    /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
    public static void BlockCopy(
      BitmapContext src,
      int srcOffset,
      Array dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy((Array) src.Pixels, srcOffset, dest, destOffset, count);
    }

    /// <summary>
    /// Clears the BitmapContext, filling the underlying bitmap with zeros
    /// </summary>
    public void Clear()
    {
      int[] pixels = this.writeableBitmap.Pixels;
      Array.Clear((Array) pixels, 0, pixels.Length);
    }

    /// <summary>
    /// Disposes this instance if the underlying platform needs that.
    /// </summary>
    public void Dispose()
    {
    }
  }
}
