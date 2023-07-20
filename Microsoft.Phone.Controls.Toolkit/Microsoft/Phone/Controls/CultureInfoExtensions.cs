// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.CultureInfoExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Extends the CultureInfo class to add Weekdays and Weekends methods.
  /// </summary>
  public static class CultureInfoExtensions
  {
    private static string[] CulturesWithTFWeekends = new string[1]
    {
      "ar-SA"
    };
    private static string[] CulturesWithFSWeekends = new string[2]
    {
      "he-IL",
      "ar-EG"
    };

    /// <summary>
    /// Returns a list of days that are weekdays in the given culture.
    /// </summary>
    /// <param name="culture">The culture to lookup.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension method.")]
    public static ReadOnlyCollection<string> Weekdays(this CultureInfo culture)
    {
      DayOfWeek[] dayOfWeekArray;
      if (((IEnumerable<string>) CultureInfoExtensions.CulturesWithTFWeekends).Contains<string>(culture.Name))
        dayOfWeekArray = new DayOfWeek[5]
        {
          DayOfWeek.Monday,
          DayOfWeek.Tuesday,
          DayOfWeek.Wednesday,
          DayOfWeek.Saturday,
          DayOfWeek.Sunday
        };
      else if (((IEnumerable<string>) CultureInfoExtensions.CulturesWithFSWeekends).Contains<string>(culture.Name))
        dayOfWeekArray = new DayOfWeek[5]
        {
          DayOfWeek.Monday,
          DayOfWeek.Tuesday,
          DayOfWeek.Wednesday,
          DayOfWeek.Thursday,
          DayOfWeek.Sunday
        };
      else
        dayOfWeekArray = new DayOfWeek[5]
        {
          DayOfWeek.Monday,
          DayOfWeek.Tuesday,
          DayOfWeek.Wednesday,
          DayOfWeek.Thursday,
          DayOfWeek.Friday
        };
      List<string> list = new List<string>();
      foreach (DayOfWeek dayofweek in dayOfWeekArray)
        list.Add(culture.DateTimeFormat.GetDayName(dayofweek));
      return new ReadOnlyCollection<string>((IList<string>) list);
    }

    /// <summary>
    /// Returns a list of days that are weekends in the given culture.
    /// </summary>
    /// <param name="culture">The culture to lookup.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension method.")]
    public static ReadOnlyCollection<string> Weekends(this CultureInfo culture)
    {
      DayOfWeek[] dayOfWeekArray;
      if (((IEnumerable<string>) CultureInfoExtensions.CulturesWithTFWeekends).Contains<string>(culture.Name))
        dayOfWeekArray = new DayOfWeek[2]
        {
          DayOfWeek.Thursday,
          DayOfWeek.Friday
        };
      else if (((IEnumerable<string>) CultureInfoExtensions.CulturesWithFSWeekends).Contains<string>(culture.Name))
        dayOfWeekArray = new DayOfWeek[2]
        {
          DayOfWeek.Friday,
          DayOfWeek.Saturday
        };
      else
        dayOfWeekArray = new DayOfWeek[2]
        {
          DayOfWeek.Saturday,
          DayOfWeek.Sunday
        };
      List<string> list = new List<string>();
      foreach (DayOfWeek dayofweek in dayOfWeekArray)
        list.Add(culture.DateTimeFormat.GetDayName(dayofweek));
      return new ReadOnlyCollection<string>((IList<string>) list);
    }
  }
}
