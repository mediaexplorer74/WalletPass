// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.IErrorCorrectionLevelClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [Guid(4193616707, 52522, 24400, 112, 50, 217, 254, 147, 226, 123, 176)]
  [Version(917504)]
  [ExclusiveTo(typeof (ErrorCorrectionLevel))]
  internal interface IErrorCorrectionLevelClass
  {
    int Bits { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    string Name { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int ordinal();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
