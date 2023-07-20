// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RecurringDaysPicker
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents a control that allows the user to choose days of the week.
  /// </summary>
  /// <QualityBand>Experimental</QualityBand>
  public class RecurringDaysPicker : ListPicker
  {
    private const string CommaSpace = ", ";
    private const string EnglishLanguage = "en";
    private string[] DayNames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
    private string[] ShortestDayNames = CultureInfo.CurrentCulture.DateTimeFormat.ShortestDayNames;

    /// <summary>
    /// Initializes a new instance of the RecurringDaysPicker control.
    /// </summary>
    public RecurringDaysPicker()
    {
      if (CultureInfo.CurrentCulture.Name.StartsWith("en", StringComparison.OrdinalIgnoreCase))
        this.ShortestDayNames = new string[7]
        {
          "Sun",
          "Mon",
          "Tue",
          "Wed",
          "Thu",
          "Fri",
          "Sat"
        };
      DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      for (int index = 0; index < ((IEnumerable<string>) this.DayNames).Count<string>(); ++index)
        ((PresentationFrameworkCollection<object>) this.Items).Add((object) this.DayNames[(int) (firstDayOfWeek + index) % ((IEnumerable<string>) this.DayNames).Count<string>()]);
      this.SelectionMode = (SelectionMode) 1;
      this.SummaryForSelectedItemsDelegate = new Func<IList, string>(this.SummarizeDaysOfWeek);
    }

    /// <summary>
    /// Sumarizes a list of days into a shortened string representation.
    /// If all days, all weekdays, or all weekends are in the list, then the string includes
    ///     the corresponding name rather than listing out all of those days separately.
    /// If individual days are listed, they are abreviated.
    /// If the list is null or empty, "only once" is returned.
    /// </summary>
    /// <param name="selection">The list of days. Can be empty or null.</param>
    /// <returns>A string representation of the list of days.</returns>
    protected string SummarizeDaysOfWeek(IList selection)
    {
      string repeatsOnlyOnce = ControlResources.RepeatsOnlyOnce;
      if (null != selection)
      {
        List<string> daysList = new List<string>();
        foreach (object obj in (IEnumerable) selection)
          daysList.Add((string) obj);
        repeatsOnlyOnce = this.DaysOfWeekToString(daysList);
      }
      return repeatsOnlyOnce;
    }

    /// <summary>
    /// Sumarizes a list of days into a shortened string representation.
    /// If all days, all weekdays, or all weekends are in the list, then the string includes
    ///     the corresponding name rather than listing out all of those days separately.
    /// If individual days are listed, they are abreviated.
    /// If the list is empty, "only once" is returned.
    /// </summary>
    /// <param name="daysList">The list of days. Can be empty.</param>
    /// <returns>A string representation of the list of days.</returns>
    private string DaysOfWeekToString(List<string> daysList)
    {
      List<string> days1 = new List<string>();
      foreach (string days2 in daysList)
      {
        if (!days1.Contains(days2))
          days1.Add(days2);
      }
      if (days1.Count == 0)
        return ControlResources.RepeatsOnlyOnce;
      StringBuilder stringBuilder = new StringBuilder();
      IEnumerable<string> unhandledDays;
      stringBuilder.Append(RecurringDaysPicker.HandleGroups(days1, out unhandledDays));
      if (stringBuilder.Length > 0)
        stringBuilder.Append(", ");
      DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      for (int index1 = 0; index1 < ((IEnumerable<string>) this.DayNames).Count<string>(); ++index1)
      {
        int index2 = (int) (firstDayOfWeek + index1) % ((IEnumerable<string>) this.DayNames).Count<string>();
        string dayName = this.DayNames[index2];
        if (unhandledDays.Contains<string>(dayName))
        {
          stringBuilder.Append(this.ShortestDayNames[index2]);
          stringBuilder.Append(", ");
        }
      }
      stringBuilder.Length -= ", ".Length;
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Finds a group (weekends, weekdays, every day) within a list of days and returns a string representing that group.
    /// Days that are not in a group are set in the unhandledDays out parameter.
    /// </summary>
    /// <param name="days">List of days</param>
    /// <param name="unhandledDays">Out parameter which will be written to with the list of days that were not in a group.</param>
    /// <returns>String of any group found.</returns>
    private static string HandleGroups(List<string> days, out IEnumerable<string> unhandledDays)
    {
      if (days.Count == 7)
      {
        unhandledDays = (IEnumerable<string>) new List<string>();
        return ControlResources.RepeatsEveryDay;
      }
      ReadOnlyCollection<string> weekdays = CultureInfo.CurrentCulture.Weekdays();
      ReadOnlyCollection<string> weekends = CultureInfo.CurrentCulture.Weekends();
      if (days.Intersect<string>((IEnumerable<string>) weekdays).Count<string>() == weekdays.Count)
      {
        unhandledDays = days.Where<string>((Func<string, bool>) (day => !weekdays.Contains(day)));
        return ControlResources.RepeatsOnWeekdays;
      }
      if (days.Intersect<string>((IEnumerable<string>) weekends).Count<string>() == weekends.Count)
      {
        unhandledDays = days.Where<string>((Func<string, bool>) (day => !weekends.Contains(day)));
        return ControlResources.RepeatsOnWeekends;
      }
      unhandledDays = (IEnumerable<string>) days;
      return string.Empty;
    }
  }
}
