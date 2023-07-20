// Decompiled with JetBrains decompiler
// Type: ZXing.IResultPointFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(2772718419, 35254, 23461, 75, 32, 222, 133, 206, 17, 240, 231)]
  [Version(917504)]
  [ExclusiveTo(typeof (ResultPoint))]
  internal interface IResultPointFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    ResultPoint CreateResultPoint([In] float x, [In] float y);
  }
}
