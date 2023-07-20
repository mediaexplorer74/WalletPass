// Decompiled with JetBrains decompiler
// Type: ZXing.Common.IEncodingOptions
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ZXing.Common
{
  [Guid(3382255846, 13499, 20534, 94, 125, 54, 54, 180, 213, 73, 38)]
  [Version(917504)]
  public interface IEncodingOptions
  {
    IMap<EncodeHintType, object> Hints { get; }
  }
}
