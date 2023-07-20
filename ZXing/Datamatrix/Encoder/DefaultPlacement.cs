// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Encoder.DefaultPlacement
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Encoder
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IDefaultPlacementFactory), 917504)]
  public sealed class DefaultPlacement : IDefaultPlacementClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern DefaultPlacement([In] string codewords, [In] int numcols, [In] int numrows);

    public extern byte[] Bits { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Numcols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Numrows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern bool getBit([In] int col, [In] int row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern bool hasBit([In] int col, [In] int row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void place();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void setBit([In] int col, [In] int row, [In] bool bit);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
