// Decompiled with JetBrains decompiler
// Type: ZXing.IResultClass
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;

namespace ZXing
{
  [Guid(784523010, 43751, 20857, 105, 116, 248, 190, 61, 242, 166, 202)]
  [Version(917504)]
  [ExclusiveTo(typeof (Result))]
  internal interface IResultClass
  {
    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void addResultPoints([In] ResultPoint[] newPoints);

    BarcodeFormat BarcodeFormat { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    byte[] RawBytes { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    IMap<ResultMetadataType, object> ResultMetadata { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    ResultPoint[] ResultPoints { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    string Text { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    long Timestamp { [MethodImpl(MethodCodeType = MethodCodeType.Runtime)] get; }

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void putAllMetadata([In] IMap<ResultMetadataType, object> metadata);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    void putMetadata([In] ResultMetadataType type, [In] object value);

    [MethodImpl(MethodCodeType = MethodCodeType.Runtime)]
    string ToString();
  }
}
