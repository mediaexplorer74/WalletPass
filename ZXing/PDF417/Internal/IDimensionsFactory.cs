// Decompiled with JetBrains decompiler
// Type: ZXing.PDF417.Internal.IDimensionsFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.PDF417.Internal
{
  [Guid(4094546248, 51363, 22895, 65, 53, 62, 20, 65, 112, 86, 50)]
  [Version(917504)]
  [ExclusiveTo(typeof (Dimensions))]
  internal interface IDimensionsFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Dimensions CreateDimensions([In] int minCols, [In] int maxCols, [In] int minRows, [In] int maxRows);
  }
}
