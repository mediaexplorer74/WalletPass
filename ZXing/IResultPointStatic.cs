// Decompiled with JetBrains decompiler
// Type: ZXing.IResultPointStatic
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(2375914258, 31652, 24429, 117, 103, 220, 108, 2, 223, 52, 15)]
  [Version(917504)]
  [ExclusiveTo(typeof (ResultPoint))]
  internal interface IResultPointStatic
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    float distance([In] ResultPoint pattern1, [In] ResultPoint pattern2);
  }
}
