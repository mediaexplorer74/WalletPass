// Decompiled with JetBrains decompiler
// Type: ZXing.PDF417.Internal.IDimensionsClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.PDF417.Internal
{
  [Guid(1209020448, 64702, 21160, 77, 142, 50, 132, 215, 249, 186, 170)]
  [Version(917504)]
  [ExclusiveTo(typeof (Dimensions))]
  internal interface IDimensionsClass
  {
    int MaxCols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int MaxRows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int MinCols { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int MinRows { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }
  }
}
