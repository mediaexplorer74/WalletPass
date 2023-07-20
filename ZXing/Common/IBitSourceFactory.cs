// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IBitSourceFactory
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(2747391581, 30058, 22610, 102, 76, 166, 211, 207, 245, 180, 252)]
  [Version(917504)]
  [ExclusiveTo(typeof (BitSource))]
  internal interface IBitSourceFactory
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    BitSource CreateBitSource([In] byte[] bytes);
  }
}
