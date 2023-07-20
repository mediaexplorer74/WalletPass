// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitMatrixClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using ZXing.Rendering;

namespace ZXing.Common
{
  [Guid(3670538179, 4680, 24035, 77, 251, 169, 65, 243, 155, 151, 88)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitMatrix))]
  internal interface IBitMatrixClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void clear();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    object Clone();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool Equals([In] object obj);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void flip([In] int x, [In] int y);

    int Dimension { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Height { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int RowSize { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int[] getBottomRightOnBit();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int[] getEnclosingRectangle();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitArray getRow([In] int y, [In] BitArray row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool get([In] int x, [In] int y);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int[] getTopLeftOnBit();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void rotate180();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void setRegion([In] int left, [In] int top, [In] int width, [In] int height);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void setRow([In] int y, [In] BitArray row);

    [Deprecated]
    [Overload("ToBitmap1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    PixelData ToBitmap();

    [Deprecated]
    [Overload("ToBitmap2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    PixelData ToBitmap([In] BarcodeFormat format, [In] string content);

    [Overload("ToString1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();

    [Overload("ToString2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString([In] string setString, [In] string unsetString);

    [Overload("ToString3")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString([In] string setString, [In] string unsetString, [In] string lineSeparator);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void xor([In] BitMatrix mask);
  }
}
