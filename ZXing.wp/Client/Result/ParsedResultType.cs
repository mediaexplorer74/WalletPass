// Decompiled with JetBrains decompiler
// Type: ZXing.Client.Result.ParsedResultType
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.xml

namespace ZXing.Client.Result
{
  /// <summary>
  /// Represents the type of data encoded by a barcode -- from plain text, to a
  /// URI, to an e-mail address, etc.
  /// </summary>
  /// <author>Sean Owen</author>
  public enum ParsedResultType
  {
    ADDRESSBOOK,
    EMAIL_ADDRESS,
    PRODUCT,
    URI,
    TEXT,
    GEO,
    TEL,
    SMS,
    CALENDAR,
    WIFI,
    ISBN,
    VIN,
  }
}
