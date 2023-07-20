// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IDecodingOptionsClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(3346033268, 15259, 22384, 67, 15, 117, 139, 161, 228, 54, 220)]
  [Version(917504)]
  [ExclusiveTo(typeof (DecodingOptions))]
  internal interface IDecodingOptionsClass
  {
    event EventHandler<object> ValueChanged;

    string CharacterSet { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    IMap<DecodeHintType, object> Hints { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    BarcodeFormat[] PossibleFormats { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool PureBarcode { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool ReturnCodabarStartEnd { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool TryHarder { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool UseCode39ExtendedMode { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    bool UseCode39RelaxedExtendedMode { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }
  }
}
