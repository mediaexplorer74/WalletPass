// ZXing.BarcodeReader

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml.Media.Imaging;
using ZXing.Common;

namespace ZXing
{
  //[MarshalingBehavior]
  //[Threading]
  [Version(917504)]
  [Activatable(917504)]
  public sealed class BarcodeReader : IBarcodeReader, /*IBarcodeReaderClass,*/ IStringable
  {
    private /*const*/ object MethodCodeType;

   //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BarcodeReader();

    public extern DecodingOptions Options 
        {
           // [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            get; 
           // [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            [param: In] 
            set;
        }

    //[Deprecated]
    public extern bool TryHarder 
        { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            get;
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            [param: In] 
            set;
        }

    //[Deprecated]
    public extern bool PureBarcode
        {
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            get; 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            [param: In] 
            set;
        }

    //[Deprecated]
    public extern string CharacterSet { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get; 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
            [param: In] 
            set; }

    //[Deprecated]
    public extern BarcodeFormat[] PossibleFormats
    { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get; 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            [param: In] 
            set;
        }

    [Overload("Decode2")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result Decode([In] WriteableBitmap barcodeBitmap);

    [Overload("Decode1")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result Decode([In] byte[] rawRGB, [In] int width,
        [In] int height, [In] BitmapFormat format);

    public extern event EventHandler<Result> ResultFound;

    public extern event EventHandler<ResultPoint> ResultPointFound;

    [Overload("DecodeMultiple1")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result[] DecodeMultiple(
      [In] byte[] rawRGB,
      [In] int width,
      [In] int height,
      [In] BitmapFormat format);

    [Overload("DecodeMultiple2")]
    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result[] DecodeMultiple([In] WriteableBitmap barcodeBitmap);

    public extern bool AutoRotate {
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get;
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            [param: In] 
            set; }

    public extern bool TryInverted { 
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            get;
            //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)] 
            [param: In] 
            set; }

    //[MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
