// Decompiled with JetBrains decompiler
// Type: ZXing.Aztec.AztecResultMetadata
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Aztec
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IAztecResultMetadataFactory), 917504)]
  public sealed class AztecResultMetadata : IAztecResultMetadataClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern AztecResultMetadata([In] bool compact, [In] int datablocks, [In] int layers);

    public extern bool Compact { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Datablocks { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Layers { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
