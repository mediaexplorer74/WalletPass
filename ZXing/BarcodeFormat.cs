// Decompiled with JetBrains decompiler
// Type: ZXing.BarcodeFormat
// Assembly: ZXing, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null, ContentType=WindowsRuntime
// MVID: C60C7D43-D3C5-4377-9D33-EA97A0E6CD19
// Assembly location: C:\Users\Admin\Desktop\re\ZXing.winmd

using Windows.Foundation.Metadata;

namespace ZXing
{
  [Version(917504)]
  public enum BarcodeFormat : uint
  {
    AZTEC = 1,
    CODABAR = 2,
    CODE_39 = 4,
    CODE_93 = 8,
    CODE_128 = 16, // 0x00000010
    DATA_MATRIX = 32, // 0x00000020
    EAN_8 = 64, // 0x00000040
    EAN_13 = 128, // 0x00000080
    ITF = 256, // 0x00000100
    MAXICODE = 512, // 0x00000200
    PDF_417 = 1024, // 0x00000400
    QR_CODE = 2048, // 0x00000800
    RSS_14 = 4096, // 0x00001000
    RSS_EXPANDED = 8192, // 0x00002000
    UPC_A = 16384, // 0x00004000
    UPC_E = 32768, // 0x00008000
    All_1D = 61918, // 0x0000F1DE
    UPC_EAN_EXTENSION = 65536, // 0x00010000
    MSI = 131072, // 0x00020000
    PLESSEY = 262144, // 0x00040000
    IMB = 524288, // 0x00080000
  }
}
