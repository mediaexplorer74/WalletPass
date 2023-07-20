// Decompiled with JetBrains decompiler
// Type: ZXing.Rendering.PixelDataRenderer
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ZXing.Common;

namespace ZXing.Rendering
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(917504)]
  public sealed class PixelDataRenderer : IBarcodeRenderer, IPixelDataRendererClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PixelDataRenderer();

    [Overload("Render1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PixelData Render([In] BitMatrix matrix, [In] BarcodeFormat format, [In] string content);

    [Overload("Render2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PixelData Render(
      [In] BitMatrix matrix,
      [In] BarcodeFormat format,
      [In] string content,
      [In] EncodingOptions options);

    public extern Color Background { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern FontFamily FontFamily { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern double FontSize { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    public extern Color Foreground { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] [param: In] set; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
