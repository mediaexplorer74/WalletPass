// Decompiled with JetBrains decompiler
// Type: ZXing.ResultMetadataType
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using Windows.Foundation.Metadata;

namespace ZXing
{
  [Version(917504)]
  public enum ResultMetadataType
  {
    OTHER,
    ORIENTATION,
    BYTE_SEGMENTS,
    ERROR_CORRECTION_LEVEL,
    ISSUE_NUMBER,
    SUGGESTED_PRICE,
    POSSIBLE_COUNTRY,
    UPC_EAN_EXTENSION,
    STRUCTURED_APPEND_SEQUENCE,
    STRUCTURED_APPEND_PARITY,
    PDF417_EXTRA_METADATA,
    AZTEC_EXTRA_METADATA,
  }
}
