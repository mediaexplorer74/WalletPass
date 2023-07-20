// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TimePicker
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Globalization;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a control that allows the user to choose a time (hour/minute/am/pm).
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public class TimePicker : DateTimePickerBase
  {
    private string _fallbackValueStringFormat;

    /// <summary>Initializes a new instance of the TimePicker control.</summary>
    public TimePicker()
    {
      this.DefaultStyleKey = (object) typeof (TimePicker);
      this.Value = new DateTime?(DateTime.Now);
    }

    /// <summary>
    /// Gets the fallback value for the ValueStringFormat property.
    /// </summary>
    protected override string ValueStringFormatFallback
    {
      get
      {
        if (null == this._fallbackValueStringFormat)
        {
          string str = CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern.Replace(":ss", "");
          string letterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
          this._fallbackValueStringFormat = "{0:" + (!(letterIsoLanguageName == "ar") && !(letterIsoLanguageName == "fa") ? "\u200E" + str : "\u200F" + str) + "}";
        }
        return this._fallbackValueStringFormat;
      }
    }
  }
}
