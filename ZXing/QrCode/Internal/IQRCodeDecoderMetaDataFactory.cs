// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.IQRCodeDecoderMetaDataFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [Guid(4110825156, 46171, 22896, 97, 50, 255, 185, 128, 56, 11, 179)]
  [Version(917504)]
  [ExclusiveTo(typeof (QRCodeDecoderMetaData))]
  internal interface IQRCodeDecoderMetaDataFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    QRCodeDecoderMetaData CreateQRCodeDecoderMetaData([In] bool mirrored);
  }
}
