// Decompiled with JetBrains decompiler
// Type: ZXing.IBarcodeWriter
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.InteropServices;
using Windows.Foundation.Metadata;
using ZXing.Common;
using ZXing.Rendering;

namespace ZXing
{
  [Guid(3441176032, 18435, 23924, 71, 105, 136, 213, 176, 191, 187, 34)]
  [Version(917504)]
  public interface IBarcodeWriter
  {
    BitMatrix Encode([In] string contents);

    PixelData Write([In] string contents);
  }
}
