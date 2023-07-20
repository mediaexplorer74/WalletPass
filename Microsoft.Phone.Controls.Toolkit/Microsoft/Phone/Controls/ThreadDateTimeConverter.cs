// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ThreadDateTimeConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>Date and time converter for threads.</summary>
  /// <QualityBand>Preview</QualityBand>
  public class ThreadDateTimeConverter : IValueConverter
  {
    /// <summary>
    /// Converts a
    /// <see cref="T:System.DateTime" />
    /// object into a string appropriately formatted for threads.
    /// This format can be found in messaging.
    /// </summary>
    /// <remarks>This format never displays the year.</remarks>
    /// <param name="value">The given date and time.</param>
    /// <param name="targetType">
    /// The type corresponding to the binding property, which must be of
    /// <see cref="T:System.String" />.
    /// </param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">(Not used).</param>
    /// <returns>The given date and time as a string.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is DateTime dateTime))
        throw new ArgumentException(Resources.InvalidDateTimeArgument);
      DateTime now = DateTime.Now;
      if (DateTimeFormatHelper.IsFutureDateTime(now, dateTime))
        throw new NotSupportedException(Resources.NonSupportedDateTime);
      return !DateTimeFormatHelper.IsAnOlderWeek(now, dateTime) ? (!DateTimeFormatHelper.IsPastDayOfWeekWithWindow(now, dateTime) ? (object) DateTimeFormatHelper.GetSuperShortTime(dateTime) : (object) DateTimeFormatHelper.GetAbbreviatedDay(dateTime)) : (object) DateTimeFormatHelper.GetMonthAndDay(dateTime);
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
