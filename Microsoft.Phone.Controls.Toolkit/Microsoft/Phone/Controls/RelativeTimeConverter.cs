// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RelativeTimeConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using Microsoft.Phone.Controls.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Time converter to display elapsed time relatively to the present.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public class RelativeTimeConverter : IValueConverter
  {
    /// <summary>A minute defined in seconds.</summary>
    private const double Minute = 60.0;
    /// <summary>An hour defined in seconds.</summary>
    private const double Hour = 3600.0;
    /// <summary>A day defined in seconds.</summary>
    private const double Day = 86400.0;
    /// <summary>A week defined in seconds.</summary>
    private const double Week = 604800.0;
    /// <summary>A month defined in seconds.</summary>
    private const double Month = 2635200.0;
    /// <summary>A year defined in seconds.</summary>
    private const double Year = 31536000.0;
    /// <summary>
    /// Abbreviation for the default culture used by resources files.
    /// </summary>
    private const string DefaultCulture = "en-US";
    /// <summary>Four different strings to express hours in plural.</summary>
    private string[] PluralHourStrings;
    /// <summary>Four different strings to express minutes in plural.</summary>
    private string[] PluralMinuteStrings;
    /// <summary>Four different strings to express seconds in plural.</summary>
    private string[] PluralSecondStrings;

    /// <summary>
    /// Resources use the culture in the system locale by default.
    /// The converter must use the culture specified the ConverterCulture.
    /// The ConverterCulture defaults to en-US when not specified.
    /// Thus, change the resources culture only if ConverterCulture is set.
    /// </summary>
    /// <param name="culture">The culture to use in the converter.</param>
    private void SetLocalizationCulture(CultureInfo culture)
    {
      if (!culture.Name.Equals("en-US", StringComparison.Ordinal))
        ControlResources.Culture = culture;
      this.PluralHourStrings = new string[4]
      {
        ControlResources.XHoursAgo_2To4,
        ControlResources.XHoursAgo_EndsIn1Not11,
        ControlResources.XHoursAgo_EndsIn2To4Not12To14,
        ControlResources.XHoursAgo_Other
      };
      this.PluralMinuteStrings = new string[4]
      {
        ControlResources.XMinutesAgo_2To4,
        ControlResources.XMinutesAgo_EndsIn1Not11,
        ControlResources.XMinutesAgo_EndsIn2To4Not12To14,
        ControlResources.XMinutesAgo_Other
      };
      this.PluralSecondStrings = new string[4]
      {
        ControlResources.XSecondsAgo_2To4,
        ControlResources.XSecondsAgo_EndsIn1Not11,
        ControlResources.XSecondsAgo_EndsIn2To4Not12To14,
        ControlResources.XSecondsAgo_Other
      };
    }

    /// <summary>
    /// Returns a localized text string to express months in plural.
    /// </summary>
    /// <param name="month">Number of months.</param>
    /// <returns>Localized text string.</returns>
    private static string GetPluralMonth(int month)
    {
      if (month >= 2 && month <= 4)
        return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.XMonthsAgo_2To4, new object[1]
        {
          (object) month.ToString((IFormatProvider) ControlResources.Culture)
        });
      if (month < 5 || month > 12)
        throw new ArgumentException(Resources.InvalidNumberOfMonths);
      return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.XMonthsAgo_5To12, new object[1]
      {
        (object) month.ToString((IFormatProvider) ControlResources.Culture)
      });
    }

    /// <summary>
    /// Returns a localized text string to express time units in plural.
    /// </summary>
    /// <param name="units">
    /// Number of time units, e.g. 5 for five months.
    /// </param>
    /// <param name="resources">
    /// Resources related to the specified time unit.
    /// </param>
    /// <returns>Localized text string.</returns>
    private static string GetPluralTimeUnits(int units, string[] resources)
    {
      int num1 = units % 10;
      int num2 = units % 100;
      if (units <= 1)
        throw new ArgumentException(Resources.InvalidNumberOfTimeUnits);
      return units >= 2 && units <= 4 ? string.Format((IFormatProvider) CultureInfo.CurrentUICulture, resources[0], new object[1]
      {
        (object) units.ToString((IFormatProvider) ControlResources.Culture)
      }) : (num1 == 1 && num2 != 11 ? string.Format((IFormatProvider) CultureInfo.CurrentUICulture, resources[1], new object[1]
      {
        (object) units.ToString((IFormatProvider) ControlResources.Culture)
      }) : (num1 >= 2 && num1 <= 4 && (num2 < 12 || num2 > 14) ? string.Format((IFormatProvider) CultureInfo.CurrentUICulture, resources[2], new object[1]
      {
        (object) units.ToString((IFormatProvider) ControlResources.Culture)
      }) : string.Format((IFormatProvider) CultureInfo.CurrentUICulture, resources[3], new object[1]
      {
        (object) units.ToString((IFormatProvider) ControlResources.Culture)
      })));
    }

    /// <summary>Returns a localized text string for the day of week.</summary>
    /// <param name="dow">Day of week.</param>
    /// <returns>Localized text string.</returns>
    private static string GetDayOfWeek(DayOfWeek dow)
    {
      string dayOfWeek;
      switch (dow)
      {
        case DayOfWeek.Sunday:
          dayOfWeek = ControlResources.Sunday;
          break;
        case DayOfWeek.Monday:
          dayOfWeek = ControlResources.Monday;
          break;
        case DayOfWeek.Tuesday:
          dayOfWeek = ControlResources.Tuesday;
          break;
        case DayOfWeek.Wednesday:
          dayOfWeek = ControlResources.Wednesday;
          break;
        case DayOfWeek.Thursday:
          dayOfWeek = ControlResources.Thursday;
          break;
        case DayOfWeek.Friday:
          dayOfWeek = ControlResources.Friday;
          break;
        case DayOfWeek.Saturday:
          dayOfWeek = ControlResources.Saturday;
          break;
        default:
          dayOfWeek = ControlResources.Sunday;
          break;
      }
      return dayOfWeek;
    }

    /// <summary>
    /// Returns a localized text string to express "on {0}"
    /// where {0} is a day of the week, e.g. Sunday.
    /// </summary>
    /// <param name="dow">Day of week.</param>
    /// <returns>Localized text string.</returns>
    private static string GetOnDayOfWeek(DayOfWeek dow) => dow == DayOfWeek.Tuesday ? string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.OnDayOfWeek_Tuesday, new object[1]
    {
      (object) RelativeTimeConverter.GetDayOfWeek(dow)
    }) : string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.OnDayOfWeek_Other, new object[1]
    {
      (object) RelativeTimeConverter.GetDayOfWeek(dow)
    });

    /// <summary>
    /// Converts a
    /// <see cref="T:System.DateTime" />
    /// object into a string the represents the elapsed time
    /// relatively to the present.
    /// </summary>
    /// <param name="value">The given date and time.</param>
    /// <param name="targetType">
    /// The type corresponding to the binding property, which must be of
    /// <see cref="T:System.String" />.
    /// </param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">
    /// The culture to use in the converter.
    /// When not specified, the converter uses the current culture
    /// as specified by the system locale.
    /// </param>
    /// <returns>The given date and time as a string.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      DateTime given = value is DateTime dateTime ? dateTime.ToLocalTime() : throw new ArgumentException(Resources.InvalidDateTimeArgument);
      DateTime now = DateTime.Now;
      TimeSpan timeSpan = now - given;
      this.SetLocalizationCulture(culture);
      if (DateTimeFormatHelper.IsFutureDateTime(now, given))
        RelativeTimeConverter.GetPluralTimeUnits(2, this.PluralSecondStrings);
      string str;
      if (timeSpan.TotalSeconds > 31536000.0)
        str = ControlResources.OverAYearAgo;
      else if (timeSpan.TotalSeconds > 3952800.0)
        str = RelativeTimeConverter.GetPluralMonth((int) ((timeSpan.TotalSeconds + 1317600.0) / 2635200.0));
      else if (timeSpan.TotalSeconds >= 2116800.0)
        str = ControlResources.AboutAMonthAgo;
      else if (timeSpan.TotalSeconds >= 604800.0)
      {
        int num = (int) (timeSpan.TotalSeconds / 604800.0);
        if (num > 1)
          str = string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.XWeeksAgo_2To4, new object[1]
          {
            (object) num.ToString((IFormatProvider) ControlResources.Culture)
          });
        else
          str = ControlResources.AboutAWeekAgo;
      }
      else if (timeSpan.TotalSeconds >= 432000.0)
        str = string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ControlResources.LastDayOfWeek, new object[1]
        {
          (object) RelativeTimeConverter.GetDayOfWeek(given.DayOfWeek)
        });
      else
        str = timeSpan.TotalSeconds < 86400.0 ? (timeSpan.TotalSeconds < 7200.0 ? (timeSpan.TotalSeconds < 3600.0 ? (timeSpan.TotalSeconds < 120.0 ? (timeSpan.TotalSeconds < 60.0 ? RelativeTimeConverter.GetPluralTimeUnits((double) (int) timeSpan.TotalSeconds > 1.0 ? (int) timeSpan.TotalSeconds : 2, this.PluralSecondStrings) : ControlResources.AboutAMinuteAgo) : RelativeTimeConverter.GetPluralTimeUnits((int) (timeSpan.TotalSeconds / 60.0), this.PluralMinuteStrings)) : ControlResources.AboutAnHourAgo) : RelativeTimeConverter.GetPluralTimeUnits((int) (timeSpan.TotalSeconds / 3600.0), this.PluralHourStrings)) : RelativeTimeConverter.GetOnDayOfWeek(given.DayOfWeek);
      return (object) str;
    }

    /// <summary>Not implemented.</summary>
    /// <param name="value">(Not used).</param>
    /// <param name="targetType">(Not used).</param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">(Not used).</param>
    /// <returns>null</returns>
    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
