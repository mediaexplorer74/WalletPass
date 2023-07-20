// Decompiled with JetBrains decompiler
// Type: ZXing.Common.Detector.WhiteRectangleDetector
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace ZXing.Common.Detector
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Static(typeof (IWhiteRectangleDetectorStatic), 917504)]
  public sealed class WhiteRectangleDetector : IWhiteRectangleDetectorClass, IStringable
  {
    [Overload("Create1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern WhiteRectangleDetector Create([In] BitMatrix image);

    [Overload("Create2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public static extern WhiteRectangleDetector Create(
      [In] BitMatrix image,
      [In] int initSize,
      [In] int x,
      [In] int y);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern ResultPoint[] detect();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
