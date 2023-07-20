// Decompiled with JetBrains decompiler
// Type: ZXing.Rendering.IPixelDataClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.Rendering
{
  [Guid(3027138300, 60023, 21325, 117, 208, 238, 65, 25, 25, 39, 125)]
  [Version(917504)]
  [ExclusiveTo(typeof (PixelData))]
  internal interface IPixelDataClass
  {
    int Heigth { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    byte[] Pixel { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    object ToBitmap();
  }
}
