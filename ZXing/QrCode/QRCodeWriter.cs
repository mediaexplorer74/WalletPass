// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.QRCodeWriter
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using ZXing.Common;

namespace ZXing.QrCode
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(917504)]
  public sealed class QRCodeWriter : Writer, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern QRCodeWriter();

    [Overload("encode1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix encode([In] string contents, [In] BarcodeFormat format, [In] int width, [In] int height);

    [Overload("encode2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix encode(
      [In] string contents,
      [In] BarcodeFormat format,
      [In] int width,
      [In] int height,
      [In] IMap<EncodeHintType, object> hints);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
