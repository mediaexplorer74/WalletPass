﻿// Decompiled with JetBrains decompiler
// Type: ZXing.IBarcodeWriterGeneric`1
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.xml

using ZXing.Common;

namespace ZXing
{
  /// <summary>
  /// Interface for a smart class to encode some content into a barcode
  /// </summary>
  public interface IBarcodeWriterGeneric<TOutput>
  {
    /// <summary>Encodes the specified contents.</summary>
    /// <param name="contents">The contents.</param>
    /// <returns></returns>
    BitMatrix Encode(string contents);

    /// <summary>Creates a visual representation of the contents</summary>
    /// <param name="contents">The contents.</param>
    /// <returns></returns>
    TOutput Write(string contents);

    /// <summary>
    /// Returns a rendered instance of the barcode which is given by a BitMatrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns></returns>
    TOutput Write(BitMatrix matrix);
  }
}
