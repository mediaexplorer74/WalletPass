// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Encoder.IDefaultPlacementClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Encoder
{
  [Guid(1267068056, 53662, 22867, 106, 98, 82, 237, 141, 26, 84, 120)]
  [Version(917504)]
  [ExclusiveTo(typeof (DefaultPlacement))]
  internal interface IDefaultPlacementClass
  {
    byte[] Bits { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Numcols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Numrows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool getBit([In] int col, [In] int row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    bool hasBit([In] int col, [In] int row);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void place();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void setBit([In] int col, [In] int row, [In] bool bit);
  }
}
