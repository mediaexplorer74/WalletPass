// Decompiled with JetBrains decompiler
// Type: ZXing.Writer
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using ZXing.Common;

namespace ZXing
{
  [Guid(4037510718, 14041, 21942, 82, 198, 28, 242, 57, 217, 44, 122)]
  [Version(917504)]
  public interface Writer
  {
    [Overload("encode1")]
    BitMatrix encode([In] string contents, [In] BarcodeFormat format, [In] int width, [In] int height);

    [Overload("encode2")]
    BitMatrix encode(
      [In] string contents,
      [In] BarcodeFormat format,
      [In] int width,
      [In] int height,
      [In] IMap<EncodeHintType, object> hints);
  }
}
