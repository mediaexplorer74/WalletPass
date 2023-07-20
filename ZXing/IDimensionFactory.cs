// Decompiled with JetBrains decompiler
// Type: ZXing.IDimensionFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(2069852582, 34275, 24116, 79, 213, 117, 24, 228, 252, 215, 133)]
  [Version(917504)]
  [ExclusiveTo(typeof (Dimension))]
  internal interface IDimensionFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    Dimension CreateDimension([In] int width, [In] int height);
  }
}
