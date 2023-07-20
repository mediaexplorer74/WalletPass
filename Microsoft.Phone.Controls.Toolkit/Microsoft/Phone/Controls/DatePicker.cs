// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DatePicker
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Globalization;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a control that allows the user to choose a date (day/month/year).
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public class DatePicker : DateTimePickerBase
  {
    private string _fallbackValueStringFormat;

    /// <summary>Initializes a new instance of the DatePicker control.</summary>
    public DatePicker()
    {
      this.DefaultStyleKey = (object) typeof (DatePicker);
      this.Value = new DateTime?(DateTime.Now.Date);
    }

    /// <summary>
    /// Gets the fallback value for the ValueStringFormat property.
    /// </summary>
    protected override string ValueStringFormatFallback
    {
      get
      {
        if (this._fallbackValueStringFormat == null)
        {
          string str = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
          if (DateTimePickerBase.DateShouldFlowRTL())
          {
            char[] charArray = str.ToCharArray();
            Array.Reverse((Array) charArray);
            str = new string(charArray);
          }
          this._fallbackValueStringFormat = "{0:" + str + "}";
        }
        return this._fallbackValueStringFormat;
      }
    }
  }
}
