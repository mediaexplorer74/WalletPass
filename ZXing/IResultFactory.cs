// Decompiled with JetBrains decompiler
// Type: ZXing.IResultFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(1421684900, 751, 23798, 92, 52, 248, 186, 234, 6, 191, 17)]
  [Version(917504)]
  [ExclusiveTo(typeof (Result))]
  internal interface IResultFactory
  {
    [Overload("CreateResult1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Result CreateResult(
      [In] string text,
      [In] byte[] rawBytes,
      [In] ResultPoint[] resultPoints,
      [In] BarcodeFormat format);

    [Overload("CreateResult2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Result CreateResult(
      [In] string text,
      [In] byte[] rawBytes,
      [In] ResultPoint[] resultPoints,
      [In] BarcodeFormat format,
      [In] long timestamp);
  }
}
