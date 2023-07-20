// Decompiled with JetBrains decompiler
// Type: ZXing.IMultipleBarcodeReader
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media.Imaging;
using ZXing.Common;

namespace ZXing
{
  [Guid(3306819083, 21811, 20715, 70, 146, 138, 251, 58, 175, 129, 193)]
  [Version(917504)]
  public interface IMultipleBarcodeReader
  {
    event EventHandler<Result> ResultFound;

    [Deprecated]
    bool TryHarder { get; [param: In] set; }

    [Deprecated]
    bool PureBarcode { get; [param: In] set; }

    [Deprecated]
    string CharacterSet { get; [param: In] set; }

    [Deprecated]
    IVector<BarcodeFormat> PossibleFormats { get; [param: In] set; }

    DecodingOptions Options { get; [param: In] set; }

    [Overload("DecodeMultiple1")]
    Result[] DecodeMultiple([In] byte[] rawRGB, [In] int width, [In] int height, [In] BitmapFormat format);

    [Overload("DecodeMultiple2")]
    Result[] DecodeMultiple([In] WriteableBitmap barcodeBitmap);
  }
}
