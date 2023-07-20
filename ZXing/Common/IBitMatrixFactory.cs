// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitMatrixFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(2671517160, 25851, 24056, 76, 122, 205, 160, 175, 23, 91, 23)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitMatrix))]
  internal interface IBitMatrixFactory
  {
    [Overload("CreateBitMatrix1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitMatrix CreateBitMatrix([In] int dimension);

    [Overload("CreateBitMatrix2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitMatrix CreateBitMatrix([In] int width, [In] int height);
  }
}
