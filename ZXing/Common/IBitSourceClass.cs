// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitSourceClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(2311551262, 33651, 21052, 80, 208, 140, 99, 150, 72, 26, 71)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitSource))]
  internal interface IBitSourceClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int available();

    int BitOffset { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int ByteOffset { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int readBits([In] int numBits);
  }
}
