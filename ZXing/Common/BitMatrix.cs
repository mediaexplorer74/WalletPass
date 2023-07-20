// Decompiled with JetBrains decompiler
// Type: ZXing.Common.BitMatrix
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using ZXing.Rendering;

namespace ZXing.Common
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IBitMatrixFactory), 917504)]
  [Static(typeof (IBitMatrixStatic), 917504)]
  public sealed class BitMatrix : IBitMatrixClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix([In] int dimension);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix([In] int width, [In] int height);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern BitMatrix parse(
      [In] string stringRepresentation,
      [In] string setString,
      [In] string unsetString);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void clear();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern object Clone();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern bool Equals([In] object obj);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void flip([In] int x, [In] int y);

    public extern int Dimension { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Height { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int RowSize { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int[] getBottomRightOnBit();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int[] getEnclosingRectangle();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitArray getRow([In] int y, [In] BitArray row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern bool get([In] int x, [In] int y);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int[] getTopLeftOnBit();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void rotate180();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void setRegion([In] int left, [In] int top, [In] int width, [In] int height);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void setRow([In] int y, [In] BitArray row);

    [Deprecated]
    [Overload("ToBitmap1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PixelData ToBitmap();

    [Deprecated]
    [Overload("ToBitmap2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PixelData ToBitmap([In] BarcodeFormat format, [In] string content);

    [Overload("ToString1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [Overload("ToString2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern string ToString([In] string setString, [In] string unsetString);

    [Overload("ToString3")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern string ToString([In] string setString, [In] string unsetString, [In] string lineSeparator);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void xor([In] BitMatrix mask);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
