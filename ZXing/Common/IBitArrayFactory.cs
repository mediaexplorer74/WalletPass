// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitArrayFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(220715044, 4661, 20518, 89, 73, 217, 17, 83, 74, 42, 235)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitArray))]
  internal interface IBitArrayFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitArray CreateBitArray([In] int size);
  }
}
