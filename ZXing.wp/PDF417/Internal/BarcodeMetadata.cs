// Decompiled with JetBrains decompiler
// Type: ZXing.PDF417.Internal.BarcodeMetadata
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.xml

namespace ZXing.PDF417.Internal
{
  /// <summary>Metadata about a PDF417 Barcode</summary>
  /// <author>Guenther Grau</author>
  public sealed class BarcodeMetadata
  {
    public int ColumnCount { get; private set; }

    public int ErrorCorrectionLevel { get; private set; }

    public int RowCountUpper { get; private set; }

    public int RowCountLower { get; private set; }

    public int RowCount { get; private set; }

    public BarcodeMetadata(
      int columnCount,
      int rowCountUpperPart,
      int rowCountLowerPart,
      int errorCorrectionLevel)
    {
      this.ColumnCount = columnCount;
      this.ErrorCorrectionLevel = errorCorrectionLevel;
      this.RowCountUpper = rowCountUpperPart;
      this.RowCountLower = rowCountLowerPart;
      this.RowCount = rowCountLowerPart + rowCountUpperPart;
    }
  }
}
