// Decompiled with JetBrains decompiler
// Type: ZXing.IBarcodeReaderClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media.Imaging;

namespace ZXing
{
  [Guid(675405110, 13897, 23897, 121, 231, 175, 103, 210, 175, 85, 7)]
  [Version(917504)]
  [ExclusiveTo(typeof (BarcodeReader))]
  internal interface IBarcodeReaderClass
  {
    event EventHandler<Result> ResultFound;

    event EventHandler<ResultPoint> ResultPointFound;

    [Overload("DecodeMultiple1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Result[] DecodeMultiple([In] byte[] rawRGB, [In] int width, [In] int height, [In] BitmapFormat format);

    [Overload("DecodeMultiple2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Result[] DecodeMultiple([In] WriteableBitmap barcodeBitmap);

    bool AutoRotate { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool TryInverted { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }
  }
}
