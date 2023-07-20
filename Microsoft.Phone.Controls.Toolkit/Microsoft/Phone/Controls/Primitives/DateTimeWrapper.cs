// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.DateTimeWrapper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Implements a wrapper for DateTime that provides formatted strings for DatePicker.
  /// </summary>
  public class DateTimeWrapper
  {
    /// <summary>Gets the DateTime being wrapped.</summary>
    public DateTime DateTime { get; private set; }

    /// <summary>Gets the 4-digit year as a string.</summary>
    public string YearNumber => this.DateTime.ToString("yyyy", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the 2-digit month as a string.</summary>
    public string MonthNumber => this.DateTime.ToString("MM", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the month name as a string.</summary>
    public string MonthName => this.DateTime.ToString("MMMM", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the 2-digit day as a string.</summary>
    public string DayNumber => this.DateTime.ToString("dd", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the day name as a string.</summary>
    public string DayName => this.DateTime.ToString("dddd", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the hour as a string.</summary>
    public string HourNumber => this.DateTime.ToString(DateTimeWrapper.CurrentCultureUsesTwentyFourHourClock() ? "%H" : "%h", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the 2-digit minute as a string.</summary>
    public string MinuteNumber => this.DateTime.ToString("mm", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>Gets the AM/PM designator as a string.</summary>
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Pm", Justification = "Clearest way of expressing the concept.")]
    public string AmPmString => this.DateTime.ToString("tt", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>
    /// Initializes a new instance of the DateTimeWrapper class.
    /// </summary>
    /// <param name="dateTime">DateTime to wrap.</param>
    public DateTimeWrapper(DateTime dateTime) => this.DateTime = dateTime;

    /// <summary>
    /// Returns a value indicating whether the current culture uses a 24-hour clock.
    /// </summary>
    /// <returns>True if it uses a 24-hour clock; false otherwise.</returns>
    public static bool CurrentCultureUsesTwentyFourHourClock() => !CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern.Contains("t");
  }
}
