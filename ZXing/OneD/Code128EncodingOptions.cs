// Decompiled with JetBrains decompiler
// Type: ZXing.OneD.Code128EncodingOptions
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using ZXing.Common;

namespace ZXing.OneD
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(917504)]
  public sealed class Code128EncodingOptions : 
    IEncodingOptions,
    ICode128EncodingOptionsClass,
    IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Code128EncodingOptions();

    public extern IMap<EncodeHintType, object> Hints { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern bool ForceCodesetB { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern int Height { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern int Margin { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern bool PureBarcode { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
