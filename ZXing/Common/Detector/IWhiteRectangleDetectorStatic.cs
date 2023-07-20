// Decompiled with JetBrains decompiler
// Type: ZXing.Common.Detector.IWhiteRectangleDetectorStatic
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common.Detector
{
  [Guid(2337998238, 18131, 21467, 92, 179, 3, 109, 150, 144, 193, 76)]
  [Version(917504)]
  [ExclusiveTo(typeof (WhiteRectangleDetector))]
  internal interface IWhiteRectangleDetectorStatic
  {
    [Overload("Create1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    WhiteRectangleDetector Create([In] BitMatrix image);

    [Overload("Create2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    WhiteRectangleDetector Create([In] BitMatrix image, [In] int initSize, [In] int x, [In] int y);
  }
}
