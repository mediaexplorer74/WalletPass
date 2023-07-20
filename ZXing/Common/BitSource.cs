// Decompiled with JetBrains decompiler
// Type: ZXing.Common.BitSource
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IBitSourceFactory), 917504)]
  public sealed class BitSource : IBitSourceClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitSource([In] byte[] bytes);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int available();

    public extern int BitOffset { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int ByteOffset { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern int readBits([In] int numBits);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
