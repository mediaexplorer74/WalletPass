// Decompiled with JetBrains decompiler
// Type: ZXing.Rendering.PixelData
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Rendering
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  public sealed class PixelData : IPixelDataClass, IStringable
  {
    public extern int Heigth { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern byte[] Pixel { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern int Width { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern object ToBitmap();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
