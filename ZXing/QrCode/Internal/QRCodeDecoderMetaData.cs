// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.QRCodeDecoderMetaData
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IQRCodeDecoderMetaDataFactory), 917504)]
  public sealed class QRCodeDecoderMetaData : IQRCodeDecoderMetaDataClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern QRCodeDecoderMetaData([In] bool mirrored);

    public extern bool IsMirrored { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
