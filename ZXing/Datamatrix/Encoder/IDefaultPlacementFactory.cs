// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Encoder.IDefaultPlacementFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Encoder
{
  [Guid(1081316758, 15882, 22944, 121, 195, 189, 36, 93, 155, 78, 191)]
  [Version(917504)]
  [ExclusiveTo(typeof (DefaultPlacement))]
  internal interface IDefaultPlacementFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    DefaultPlacement CreateDefaultPlacement([In] string codewords, [In] int numcols, [In] int numrows);
  }
}
