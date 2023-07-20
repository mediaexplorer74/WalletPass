// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.IQRCodeDecoderMetaDataClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [Guid(4290021369, 58311, 22424, 99, 66, 105, 230, 77, 221, 240, 60)]
  [Version(917504)]
  [ExclusiveTo(typeof (QRCodeDecoderMetaData))]
  internal interface IQRCodeDecoderMetaDataClass
  {
    bool IsMirrored { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
