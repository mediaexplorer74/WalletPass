// Decompiled with JetBrains decompiler
// Type: ZXing.IBarcodeReader
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media.Imaging;
using ZXing.Common;

namespace ZXing
{
  [Guid(1469831624, 3403, 23124, 109, 113, 239, 15, 105, 57, 220, 200)]
  [Version(917504)]
  public interface IBarcodeReader
  {
    [Deprecated]
    bool TryHarder { get; [param: In] set; }

    [Deprecated]
    bool PureBarcode { get; [param: In] set; }

    [Deprecated]
    string CharacterSet { get; [param: In] set; }

    [Deprecated]
    BarcodeFormat[] PossibleFormats { get; [param: In] set; }

    DecodingOptions Options { get; [param: In] set; }

    [Overload("Decode1")]
    Result Decode([In] byte[] rawRGB, [In] int width, [In] int height, [In] BitmapFormat format);

    [Overload("Decode2")]
    Result Decode([In] WriteableBitmap barcodeBitmap);
  }
}
