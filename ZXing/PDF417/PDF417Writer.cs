﻿// Decompiled with JetBrains decompiler
// Type: ZXing.PDF417.PDF417Writer
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using ZXing.Common;

namespace ZXing.PDF417
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(917504)]
  public sealed class PDF417Writer : Writer, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern PDF417Writer();

    [Overload("encode2")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix encode(
      [In] string contents,
      [In] BarcodeFormat format,
      [In] int width,
      [In] int height,
      [In] IMap<EncodeHintType, object> hints);

    [Overload("encode1")]
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern BitMatrix encode([In] string contents, [In] BarcodeFormat format, [In] int width, [In] int height);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
