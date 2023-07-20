// Decompiled with JetBrains decompiler
// Type: ZXing.IBarcodeWriterClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using ZXing.Common;
using ZXing.Rendering;

namespace ZXing
{
  [Guid(4149713961, 56596, 24034, 116, 134, 10, 218, 2, 87, 240, 250)]
  [Version(917504)]
  [ExclusiveTo(typeof (BarcodeWriter))]
  internal interface IBarcodeWriterClass
  {
    Writer Encoder { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    BarcodeFormat Format { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    EncodingOptions Options { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    IBarcodeRenderer Renderer { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }
  }
}
