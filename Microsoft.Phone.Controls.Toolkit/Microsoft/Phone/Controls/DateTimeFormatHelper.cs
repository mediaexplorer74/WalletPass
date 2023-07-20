// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DateTimeFormatHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Helper methods and constants for the date time converters.
  /// </summary>
  internal static class DateTimeFormatHelper
  {
    /// <summary>An hour defined in minutes.</summary>
    private const double Hour = 60.0;
    /// <summary>A day defined in minutes.</summary>
    private const double Day = 1440.0;
    /// <summary>
    /// Format pattern for single-character meridiem designators.
    /// e.g. 4:30p
    /// </summary>
    private const string SingleMeridiemDesignator = "t";
    /// <summary>
    /// Format pattern for double-character meridiem designators.
    /// e.g. 4:30 p.m.
    /// </summary>
    private const string DoubleMeridiemDesignator = "tt";
    /// <summary>
    /// Date and time information used when getting the super short time
    /// pattern. Syncs with the current culture.
    /// </summary>
    private static DateTimeFormatInfo formatInfo_GetSuperShortTime = (DateTimeFormatInfo) null;
    /// <summary>
    /// Date and time information used when getting the month and day
    /// pattern. Syncs with the current culture.
    /// </summary>
    private static DateTimeFormatInfo formatInfo_GetMonthAndDay = (DateTimeFormatInfo) null;
    /// <summary>
    /// Date and time information used when getting the short time
    /// pattern. Syncs with the current culture.
    /// </summary>
    private static DateTimeFormatInfo formatInfo_GetShortTime = (DateTimeFormatInfo) null;
    /// <summary>
    /// Lock object used to delimit a critical region when getting
    /// the super short time pattern.
    /// </summary>
    private static object lock_GetSuperShortTime = new object();
    /// <summary>
    /// Lock object used to delimit a critical region when getting
    /// the month and day pattern.
    /// </summary>
    private static object lock_GetMonthAndDay = new object();
    /// <summary>
    /// Lock object used to delimit a critical region when getting
    /// the short time pattern.
    /// </summary>
    private static object lock_GetShortTime = new object();
    /// <summary>
    /// Regular expression used to get the month and day pattern.
    /// </summary>
    private static readonly Regex rxMonthAndDay = new Regex("(d{1,2}[^A-Za-z]M{1,3})|(M{1,3}[^A-Za-z]d{1,2})");
    /// <summary>
    /// Regular expression used to get the seconds pattern with separator.
    /// </summary>
    private static readonly Regex rxSeconds = new Regex("([^A-Za-z]s{1,2})");

    /// <summary>
    /// Gets the number representing the day of the week from a given
    /// <see cref="T:System.DateTime" />
    /// object, relative to the first day of the week
    /// according to the current culture.
    /// </summary>
    /// <param name="dt">Date information.</param>
    /// <returns>
    /// A number representing the day of the week.
    /// e.g. if Monday is the first day of the week according to the
    /// relative culture, Monday returns 0, Tuesday returns 1, etc.
    /// </returns>
    public static int GetRelativeDayOfWeek(DateTime dt) => (dt.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7;

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object represents a future instance when compared to another
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>
    /// True if given date and time represents a future instance.
    /// </returns>
    public static bool IsFutureDateTime(DateTime relative, DateTime given) => relative < given;

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object represents a past year when compared to another
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>True if given date and time is set to a past year.</returns>
    public static bool IsAnOlderYear(DateTime relative, DateTime given) => relative.Year > given.Year;

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object represents a past week when compared to another
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>True if given date and time is set to a past week.</returns>
    public static bool IsAnOlderWeek(DateTime relative, DateTime given) => DateTimeFormatHelper.IsAtLeastOneWeekOld(relative, given) || DateTimeFormatHelper.GetRelativeDayOfWeek(given) > DateTimeFormatHelper.GetRelativeDayOfWeek(relative);

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object is at least one week behind from another
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>
    /// True if given date and time is at least one week behind.
    /// </returns>
    public static bool IsAtLeastOneWeekOld(DateTime relative, DateTime given) => (double) (int) (relative - given).TotalMinutes >= 10080.0;

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object corresponds to a past day in the same week as another
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>
    /// True if given date and time is a past day in the relative week.
    /// </returns>
    public static bool IsPastDayOfWeek(DateTime relative, DateTime given) => DateTimeFormatHelper.GetRelativeDayOfWeek(relative) > DateTimeFormatHelper.GetRelativeDayOfWeek(given);

    /// <summary>
    /// Indicates if a given
    /// <see cref="T:System.DateTime" />
    /// object corresponds to a past day in the same week as another
    /// <see cref="T:System.DateTime" />
    /// object and at least three hours behind it.
    /// </summary>
    /// <param name="relative">Relative date and time.</param>
    /// <param name="given">Given date and time.</param>
    /// <returns>
    /// True if given date and time is a past day in the relative week
    /// and at least three hours behind the relative time.
    /// </returns>
    public static bool IsPastDayOfWeekWithWindow(DateTime relative, DateTime given) => DateTimeFormatHelper.IsPastDayOfWeek(relative, given) && (double) (int) (relative - given).TotalMinutes > 180.0;

    /// <summary>Identifies if the current culture is set to Japanese.</summary>
    /// <returns>True if current culture is set to Japanese.</returns>
    public static bool IsCurrentCultureJapanese() => CultureInfo.CurrentCulture.Name.StartsWith("ja", StringComparison.OrdinalIgnoreCase);

    /// <summary>Identifies if the current culture is set to Korean.</summary>
    /// <returns>True if current culture is set to Korean.</returns>
    public static bool IsCurrentCultureKorean() => CultureInfo.CurrentCulture.Name.StartsWith("ko", StringComparison.OrdinalIgnoreCase);

    /// <summary>Identifies if the current culture is set to Turkish.</summary>
    /// <returns>True if current culture is set to Turkish.</returns>
    public static bool IsCurrentCultureTurkish() => CultureInfo.CurrentCulture.Name.StartsWith("tr", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Identifies if the current culture is set to Hungarian.
    /// </summary>
    /// <returns>True if current culture is set to Hungarian.</returns>
    public static bool IsCurrentCultureHungarian() => CultureInfo.CurrentCulture.Name.StartsWith("hu", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Identifies if the current UI culture is set to French.
    /// </summary>
    /// <returns>True if current UI culture is set to French.</returns>
    public static bool IsCurrentUICultureFrench() => CultureInfo.CurrentUICulture.Name.Equals("fr-FR", StringComparison.Ordinal);

    /// <summary>
    /// Gets the abbreviated day from a
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="dt">Date information.</param>
    /// <returns>e.g. "Mon" for Monday when en-US.</returns>
    public static string GetAbbreviatedDay(DateTime dt) => DateTimeFormatHelper.IsCurrentCultureJapanese() || DateTimeFormatHelper.IsCurrentCultureKorean() ? "(" + dt.ToString("ddd", (IFormatProvider) CultureInfo.CurrentCulture) + ")" : dt.ToString("ddd", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>
    /// Gets the time from a
    /// <see cref="T:System.DateTime" />
    /// object in short Metro format.
    /// </summary>
    /// <remarks>
    /// Metro design guidelines normalize strings to lowercase.
    /// </remarks>
    /// <param name="dt">Time information.</param>
    /// <returns>"10:20p" for 10:20 p.m. when en-US.</returns>
    [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Metro design guidelines normalize strings to lowercase.")]
    public static string GetSuperShortTime(DateTime dt)
    {
      if (DateTimeFormatHelper.formatInfo_GetSuperShortTime == null)
      {
        lock (DateTimeFormatHelper.lock_GetSuperShortTime)
        {
          StringBuilder stringBuilder = new StringBuilder(string.Empty);
          DateTimeFormatHelper.formatInfo_GetSuperShortTime = (DateTimeFormatInfo) CultureInfo.CurrentCulture.DateTimeFormat.Clone();
          stringBuilder.Append(DateTimeFormatHelper.formatInfo_GetSuperShortTime.LongTimePattern);
          string oldValue = DateTimeFormatHelper.rxSeconds.Match(stringBuilder.ToString()).Value;
          stringBuilder.Replace(" ", string.Empty);
          stringBuilder.Replace(oldValue, string.Empty);
          if (!DateTimeFormatHelper.IsCurrentCultureJapanese() && !DateTimeFormatHelper.IsCurrentCultureKorean() && !DateTimeFormatHelper.IsCurrentCultureHungarian())
            stringBuilder.Replace("tt", "t");
          DateTimeFormatHelper.formatInfo_GetSuperShortTime.ShortTimePattern = stringBuilder.ToString();
        }
      }
      return dt.ToString("t", (IFormatProvider) DateTimeFormatHelper.formatInfo_GetSuperShortTime).ToLowerInvariant();
    }

    /// <summary>
    /// Gets the month and day from a
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="dt">Date information.</param>
    /// <returns>"05/24" for May 24th when en-US.</returns>
    public static string GetMonthAndDay(DateTime dt)
    {
      if (DateTimeFormatHelper.formatInfo_GetMonthAndDay == null)
      {
        lock (DateTimeFormatHelper.lock_GetMonthAndDay)
        {
          StringBuilder stringBuilder = new StringBuilder(string.Empty);
          DateTimeFormatHelper.formatInfo_GetMonthAndDay = (DateTimeFormatInfo) CultureInfo.CurrentCulture.DateTimeFormat.Clone();
          stringBuilder.Append(DateTimeFormatHelper.rxMonthAndDay.Match(DateTimeFormatHelper.formatInfo_GetMonthAndDay.ShortDatePattern).Value);
          if (stringBuilder.ToString().Contains("."))
            stringBuilder.Append(".");
          DateTimeFormatHelper.formatInfo_GetMonthAndDay.ShortDatePattern = stringBuilder.ToString();
        }
      }
      return dt.ToString("d", (IFormatProvider) DateTimeFormatHelper.formatInfo_GetMonthAndDay);
    }

    /// <summary>
    /// Gets the date in short pattern from a
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="dt">Date information.</param>
    /// <returns>
    /// Date in short pattern as defined by the system locale.
    /// </returns>
    public static string GetShortDate(DateTime dt) => dt.ToString("d", (IFormatProvider) CultureInfo.CurrentCulture);

    /// <summary>
    /// Gets the time in short pattern from a
    /// <see cref="T:System.DateTime" />
    /// object.
    /// </summary>
    /// <param name="dt">Date information.</param>
    /// <returns>
    /// Time in short pattern as defined by the system locale.
    /// </returns>
    public static string GetShortTime(DateTime dt)
    {
      if (DateTimeFormatHelper.formatInfo_GetShortTime == null)
      {
        lock (DateTimeFormatHelper.lock_GetShortTime)
        {
          StringBuilder stringBuilder = new StringBuilder(string.Empty);
          DateTimeFormatHelper.formatInfo_GetShortTime = (DateTimeFormatInfo) CultureInfo.CurrentCulture.DateTimeFormat.Clone();
          stringBuilder.Append(DateTimeFormatHelper.formatInfo_GetShortTime.LongTimePattern);
          string oldValue = DateTimeFormatHelper.rxSeconds.Match(stringBuilder.ToString()).Value;
          stringBuilder.Replace(oldValue, string.Empty);
          DateTimeFormatHelper.formatInfo_GetShortTime.ShortTimePattern = stringBuilder.ToString();
        }
      }
      return dt.ToString("t", (IFormatProvider) DateTimeFormatHelper.formatInfo_GetShortTime);
    }
  }
}
