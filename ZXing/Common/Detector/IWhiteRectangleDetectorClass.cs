// Decompiled with JetBrains decompiler
// Type: ZXing.Common.Detector.IWhiteRectangleDetectorClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common.Detector
{
  [Guid(440372215, 21768, 21290, 107, 45, 248, 182, 209, 40, 130, 181)]
  [Version(917504)]
  [ExclusiveTo(typeof (WhiteRectangleDetector))]
  internal interface IWhiteRectangleDetectorClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    ResultPoint[] detect();
  }
}
