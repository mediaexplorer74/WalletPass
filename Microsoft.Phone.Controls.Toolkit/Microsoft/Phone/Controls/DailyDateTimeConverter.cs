// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DailyDateTimeConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>Date and time converter for daily feeds.</summary>
  /// <QualityBand>Preview</QualityBand>
  public class DailyDateTimeConverter : IValueConverter
  {
    /// <summary>
    /// Converts a
    /// <see cref="T:System.DateTime" />
    /// object into a string appropriately formatted for daily feeds.
    /// This format can be found in the call history.
    /// </summary>
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
      StringBuilder stringBuilder = new StringBuilder(string.Empty);
      DateTime now = DateTime.Now;
      if (DateTimeFormatHelper.IsFutureDateTime(now, dateTime))
        throw new NotSupportedException(Resources.NonSupportedDateTime);
      if (DateTimeFormatHelper.IsAtLeastOneWeekOld(now, dateTime))
        stringBuilder.Append(DateTimeFormatHelper.GetShortDate(dateTime));
      else
        stringBuilder.AppendFormat((IFormatProvider) CultureInfo.CurrentCulture, "{0} {1}", new object[2]
        {
          (object) DateTimeFormatHelper.GetAbbreviatedDay(dateTime),
          (object) DateTimeFormatHelper.GetSuperShortTime(dateTime)
        });
      return (object) stringBuilder.ToString();
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
