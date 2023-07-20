// Decompiled with JetBrains decompiler
// Type: ZXing.DecodeHintType
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using Windows.Foundation.Metadata;

namespace ZXing
{
  [Version(917504)]
  public enum DecodeHintType
  {
    OTHER,
    PURE_BARCODE,
    POSSIBLE_FORMATS,
    TRY_HARDER,
    CHARACTER_SET,
    ALLOWED_LENGTHS,
    ASSUME_CODE_39_CHECK_DIGIT,
    NEED_RESULT_POINT_CALLBACK,
    ASSUME_MSI_CHECK_DIGIT,
    USE_CODE_39_EXTENDED_MODE,
    RELAXED_CODE_39_EXTENDED_MODE,
    TRY_HARDER_WITHOUT_ROTATION,
    ASSUME_GS1,
    RETURN_CODABAR_START_END,
    ALLOWED_EAN_EXTENSIONS,
  }
}
