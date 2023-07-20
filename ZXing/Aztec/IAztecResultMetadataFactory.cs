// Decompiled with JetBrains decompiler
// Type: ZXing.Aztec.IAztecResultMetadataFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Aztec
{
  [Guid(3214467048, 30171, 23731, 114, 162, 154, 70, 103, 118, 150, 110)]
  [Version(917504)]
  [ExclusiveTo(typeof (AztecResultMetadata))]
  internal interface IAztecResultMetadataFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    AztecResultMetadata CreateAztecResultMetadata([In] bool compact, [In] int datablocks, [In] int layers);
  }
}
