// Decompiled with JetBrains decompiler
// Type: ZXing.Client.Result.TextParsedResult
// Assembly: zxing.wp8.0, Version=0.14.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DD293DF0-BBAA-4BF0-BAC7-F5FAF5AC94ED
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\zxing.wp8.0.xml

namespace ZXing.Client.Result
{
  /// <summary>
  /// A simple result type encapsulating a string that has no further interpretation.
  /// </summary>
  /// <author>Sean Owen</author>
  public sealed class TextParsedResult : ParsedResult
  {
    public TextParsedResult(string text, string language)
      : base(ParsedResultType.TEXT)
    {
      this.Text = text;
      this.Language = language;
      this.displayResultValue = text;
    }

    public string Text { get; private set; }

    public string Language { get; private set; }
  }
}
