// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.IErrorCorrectionLevelStatic
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [Guid(453034509, 60960, 23382, 79, 170, 167, 69, 177, 94, 13, 18)]
  [Version(917504)]
  [ExclusiveTo(typeof (ErrorCorrectionLevel))]
  internal interface IErrorCorrectionLevelStatic
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    ErrorCorrectionLevel forBits([In] int bits);

    ErrorCorrectionLevel H { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    ErrorCorrectionLevel L { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    ErrorCorrectionLevel M { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    ErrorCorrectionLevel Q { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
