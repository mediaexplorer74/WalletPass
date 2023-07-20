// Decompiled with JetBrains decompiler
// Type: ZXing.Rendering.IBarcodeRenderer
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using ZXing.Common;

namespace ZXing.Rendering
{
  [Guid(2330613969, 16222, 21919, 67, 142, 83, 1, 239, 160, 100, 106)]
  [Version(917504)]
  public interface IBarcodeRenderer
  {
    [Overload("Render1")]
    PixelData Render([In] BitMatrix matrix, [In] BarcodeFormat format, [In] string content);

    [Overload("Render2")]
    PixelData Render(
      [In] BitMatrix matrix,
      [In] BarcodeFormat format,
      [In] string content,
      [In] EncodingOptions options);
  }
}
