// Decompiled with JetBrains decompiler
// Type: ZXing.Datamatrix.Internal.IVersionClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.Datamatrix.Internal
{
  [Guid(3065974496, 16300, 23339, 114, 7, 54, 114, 192, 132, 17, 22)]
  [Version(917504)]
  [ExclusiveTo(typeof (Version))]
  internal interface IVersionClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getDataRegionSizeColumns();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getDataRegionSizeRows();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getSymbolSizeColumns();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getSymbolSizeRows();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getTotalCodewords();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    int getVersionNumber();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
