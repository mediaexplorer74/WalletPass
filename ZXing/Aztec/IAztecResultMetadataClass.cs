// Decompiled with JetBrains decompiler
// Type: ZXing.Aztec.IAztecResultMetadataClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.Aztec
{
  [Guid(2255610120, 39879, 21298, 117, 252, 48, 211, 150, 226, 136, 250)]
  [Version(917504)]
  [ExclusiveTo(typeof (AztecResultMetadata))]
  internal interface IAztecResultMetadataClass
  {
    bool Compact { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Datablocks { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Layers { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
