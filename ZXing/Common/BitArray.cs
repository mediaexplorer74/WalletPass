// Decompiled with JetBrains decompiler
// Type: ZXing.Common.BitArray
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(917504)]
  [Activatable(typeof (IBitArrayFactory), 917504)]
  public sealed class BitArray : IBitArrayClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitArray();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitArray([In] int size);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void appendBitArray([In] BitArray other);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void appendBits([In] int value, [In] int numBits);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void appendBit([In] bool bit);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void clear();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern object Clone();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern bool Equals([In] object o);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void flip([In] int i);

    public extern int[] Array { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Size { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int SizeInBytes { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern int GetHashCode();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getNextSet([In] int from);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int getNextUnset([In] int from);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern bool get([In] int i);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern bool isRange([In] int start, [In] int end, [In] bool val);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void reverse();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void setBulk([In] int i, [In] int newBits);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void setRange([In] int start, [In] int end);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void toBytes([In] int bitOffset, [In] byte[] array, [In] int offset, [In] int numBytes);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void xor([In] BitArray other);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
