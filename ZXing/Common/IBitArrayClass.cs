// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitArrayClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(3515368651, 41121, 24269, 114, 223, 78, 221, 231, 52, 80, 166)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitArray))]
  internal interface IBitArrayClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void appendBitArray([In] BitArray other);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void appendBits([In] int value, [In] int numBits);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void appendBit([In] bool bit);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void clear();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    object Clone();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool Equals([In] object o);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void flip([In] int i);

    int[] Array { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Size { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int SizeInBytes { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getNextSet([In] int from);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getNextUnset([In] int from);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool get([In] int i);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool isRange([In] int start, [In] int end, [In] bool val);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void reverse();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void setBulk([In] int i, [In] int newBits);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void setRange([In] int start, [In] int end);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void toBytes([In] int bitOffset, [In] byte[] array, [In] int offset, [In] int numBytes);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void xor([In] BitArray other);
  }
}
