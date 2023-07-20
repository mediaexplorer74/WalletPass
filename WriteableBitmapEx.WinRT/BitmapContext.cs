// Decompiled with JetBrains decompiler
// Type: Windows.UI.Xaml.Media.Imaging.BitmapContext
// Assembly: WriteableBitmapEx.WinRT, Version=1.5.0.0, Culture=neutral, PublicKeyToken=50375ca6144f1c69
// MVID: 322E7EEB-6AEE-4710-AEEF-B781C66D0422
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WriteableBitmapEx.WinRT.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Windows.UI.Xaml.Media.Imaging
{
  public struct BitmapContext : IDisposable
  {
    private readonly WriteableBitmap _writeableBitmap;
    private readonly ReadWriteMode _mode;
    private readonly int _pixelWidth;
    private readonly int _pixelHeight;
    private static readonly IDictionary<WriteableBitmap, int> UpdateCountByBmp = (IDictionary<WriteableBitmap, int>) new ConcurrentDictionary<WriteableBitmap, int>();
    private static readonly IDictionary<WriteableBitmap, int[]> PixelCacheByBmp = (IDictionary<WriteableBitmap, int[]>) new ConcurrentDictionary<WriteableBitmap, int[]>();
    private int length;
    private int[] pixels;

    public WriteableBitmap WriteableBitmap => this._writeableBitmap;

    public int Width => ((BitmapSource) this._writeableBitmap).PixelWidth;

    public int Height => ((BitmapSource) this._writeableBitmap).PixelHeight;

    public BitmapContext(WriteableBitmap writeableBitmap)
      : this(writeableBitmap, ReadWriteMode.ReadWrite)
    {
    }

    public BitmapContext(WriteableBitmap writeableBitmap, ReadWriteMode mode)
    {
      this._writeableBitmap = writeableBitmap;
      this._mode = mode;
      this._pixelWidth = ((BitmapSource) this._writeableBitmap).PixelWidth;
      this._pixelHeight = ((BitmapSource) this._writeableBitmap).PixelHeight;
      if (!BitmapContext.UpdateCountByBmp.ContainsKey(this._writeableBitmap))
      {
        BitmapContext.UpdateCountByBmp.Add(this._writeableBitmap, 1);
        this.length = ((BitmapSource) this._writeableBitmap).PixelWidth * ((BitmapSource) this._writeableBitmap).PixelHeight;
        this.pixels = new int[this.length];
        this.CopyPixels();
        BitmapContext.PixelCacheByBmp.Add(this._writeableBitmap, this.pixels);
      }
      else
      {
        BitmapContext.IncrementRefCount(this._writeableBitmap);
        this.pixels = BitmapContext.PixelCacheByBmp[this._writeableBitmap];
        this.length = this.pixels.Length;
      }
    }

    private unsafe void CopyPixels()
    {
      fixed (byte* numPtr1 = this._writeableBitmap.PixelBuffer.ToArray())
        fixed (int* numPtr2 = this.pixels)
        {
          for (int index = 0; index < this.length; ++index)
            numPtr2[index] = (int) numPtr1[index * 4 + 3] << 24 | (int) numPtr1[index * 4 + 2] << 16 | (int) numPtr1[index * 4 + 1] << 8 | (int) numPtr1[index * 4];
        }
    }

    public int[] Pixels => this.pixels;

    public int Length => this.length;

    public static void BlockCopy(
      BitmapContext src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy((Array) src.Pixels, srcOffset, (Array) dest.Pixels, destOffset, count);
    }

    public static void BlockCopy(
      Array src,
      int srcOffset,
      BitmapContext dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy(src, srcOffset, (Array) dest.Pixels, destOffset, count);
    }

    public static void BlockCopy(
      BitmapContext src,
      int srcOffset,
      Array dest,
      int destOffset,
      int count)
    {
      Buffer.BlockCopy((Array) src.Pixels, srcOffset, dest, destOffset, count);
    }

    public void Clear()
    {
      int[] pixels = this.Pixels;
      Array.Clear((Array) pixels, 0, pixels.Length);
    }

    public unsafe void Dispose()
    {
      if (BitmapContext.DecrementRefCount(this._writeableBitmap) != 0)
        return;
      BitmapContext.UpdateCountByBmp.Remove(this._writeableBitmap);
      BitmapContext.PixelCacheByBmp.Remove(this._writeableBitmap);
      if (this._mode != ReadWriteMode.ReadWrite)
        return;
      using (Stream stream = this._writeableBitmap.PixelBuffer.AsStream())
      {
        byte[] buffer = new byte[this.length * 4];
        fixed (int* numPtr = this.pixels)
        {
          int index1 = 0;
          int index2 = 0;
          while (index2 < this.length)
          {
            int num = numPtr[index2];
            buffer[index1 + 3] = (byte) (num >> 24 & (int) byte.MaxValue);
            buffer[index1 + 2] = (byte) (num >> 16 & (int) byte.MaxValue);
            buffer[index1 + 1] = (byte) (num >> 8 & (int) byte.MaxValue);
            buffer[index1] = (byte) (num & (int) byte.MaxValue);
            ++index2;
            index1 += 4;
          }
          stream.Write(buffer, 0, this.length * 4);
        }
      }
      this._writeableBitmap.Invalidate();
    }

    private static void IncrementRefCount(WriteableBitmap target)
    {
      IDictionary<WriteableBitmap, int> updateCountByBmp;
      WriteableBitmap key;
      (updateCountByBmp = BitmapContext.UpdateCountByBmp)[key = target] = updateCountByBmp[key] + 1;
    }

    private static int DecrementRefCount(WriteableBitmap target)
    {
      int num = BitmapContext.UpdateCountByBmp[target] - 1;
      BitmapContext.UpdateCountByBmp[target] = num;
      return num;
    }
  }
}
