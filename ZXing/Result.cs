// Decompiled with JetBrains decompiler
// Type: ZXing.Result
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [MarshalingBehavior]
  [Threading]
  [Version(917504)]
  [Activatable(typeof (IResultFactory), 917504)]
  public sealed class Result : IResultClass, IStringable
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result(
      [In] string text,
      [In] byte[] rawBytes,
      [In] ResultPoint[] resultPoints,
      [In] BarcodeFormat format);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern Result(
      [In] string text,
      [In] byte[] rawBytes,
      [In] ResultPoint[] resultPoints,
      [In] BarcodeFormat format,
      [In] long timestamp);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void addResultPoints([In] ResultPoint[] newPoints);

    public extern BarcodeFormat BarcodeFormat { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern byte[] RawBytes { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern IMap<ResultMetadataType, object> ResultMetadata { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern ResultPoint[] ResultPoints { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern string Text { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    public extern long Timestamp { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void putAllMetadata([In] IMap<ResultMetadataType, object> metadata);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public extern void putMetadata([In] ResultMetadataType type, [In] object value);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    public override sealed extern string ToString();

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    extern string IStringable.ToString();
  }
}
