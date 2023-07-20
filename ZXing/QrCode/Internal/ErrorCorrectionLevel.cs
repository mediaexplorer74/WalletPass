// Decompiled with JetBrains decompiler
// Type: ZXing.QrCode.Internal.ErrorCorrectionLevel
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.QrCode.Internal
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Static(typeof (IErrorCorrectionLevelStatic), 917504)]
  public sealed class ErrorCorrectionLevel : IErrorCorrectionLevelClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern ErrorCorrectionLevel forBits([In] int bits);

    public static extern ErrorCorrectionLevel H { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public static extern ErrorCorrectionLevel L { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public static extern ErrorCorrectionLevel M { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public static extern ErrorCorrectionLevel Q { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Bits { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern string Name { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int ordinal();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
