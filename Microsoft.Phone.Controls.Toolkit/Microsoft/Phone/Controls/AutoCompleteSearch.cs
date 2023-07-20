// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.AutoCompleteSearch
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// A predefined set of filter functions for the known, built-in
  /// AutoCompleteFilterMode enumeration values.
  /// </summary>
  internal static class AutoCompleteSearch
  {
    /// <summary>
    /// Index function that retrieves the filter for the provided
    /// AutoCompleteFilterMode.
    /// </summary>
    /// <param name="FilterMode">The built-in search mode.</param>
    /// <returns>Returns the string-based comparison function.</returns>
    public static AutoCompleteFilterPredicate<string> GetFilter(AutoCompleteFilterMode FilterMode)
    {
      switch (FilterMode)
      {
        case AutoCompleteFilterMode.StartsWith:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.StartsWith);
        case AutoCompleteFilterMode.StartsWithCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.StartsWithCaseSensitive);
        case AutoCompleteFilterMode.StartsWithOrdinal:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.StartsWithOrdinal);
        case AutoCompleteFilterMode.StartsWithOrdinalCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.StartsWithOrdinalCaseSensitive);
        case AutoCompleteFilterMode.Contains:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.Contains);
        case AutoCompleteFilterMode.ContainsCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.ContainsCaseSensitive);
        case AutoCompleteFilterMode.ContainsOrdinal:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.ContainsOrdinal);
        case AutoCompleteFilterMode.ContainsOrdinalCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.ContainsOrdinalCaseSensitive);
        case AutoCompleteFilterMode.Equals:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.Equals);
        case AutoCompleteFilterMode.EqualsCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.EqualsCaseSensitive);
        case AutoCompleteFilterMode.EqualsOrdinal:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.EqualsOrdinal);
        case AutoCompleteFilterMode.EqualsOrdinalCaseSensitive:
          return new AutoCompleteFilterPredicate<string>(AutoCompleteSearch.EqualsOrdinalCaseSensitive);
        default:
          return (AutoCompleteFilterPredicate<string>) null;
      }
    }

    /// <summary>Check if the string value begins with the text.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool StartsWith(string text, string value) => value.StartsWith(text, StringComparison.CurrentCultureIgnoreCase);

    /// <summary>Check if the string value begins with the text.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool StartsWithCaseSensitive(string text, string value) => value.StartsWith(text, StringComparison.CurrentCulture);

    /// <summary>Check if the string value begins with the text.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool StartsWithOrdinal(string text, string value) => value.StartsWith(text, StringComparison.OrdinalIgnoreCase);

    /// <summary>Check if the string value begins with the text.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool StartsWithOrdinalCaseSensitive(string text, string value) => value.StartsWith(text, StringComparison.Ordinal);

    /// <summary>
    /// Check if the prefix is contained in the string value. The current
    /// culture's case insensitive string comparison operator is used.
    /// </summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool Contains(string text, string value) => value.Contains(text, StringComparison.CurrentCultureIgnoreCase);

    /// <summary>Check if the prefix is contained in the string value.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool ContainsCaseSensitive(string text, string value) => value.Contains(text, StringComparison.CurrentCulture);

    /// <summary>Check if the prefix is contained in the string value.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool ContainsOrdinal(string text, string value) => value.Contains(text, StringComparison.OrdinalIgnoreCase);

    /// <summary>Check if the prefix is contained in the string value.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool ContainsOrdinalCaseSensitive(string text, string value) => value.Contains(text, StringComparison.Ordinal);

    /// <summary>Check if the string values are equal.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool Equals(string text, string value) => value.Equals(text, StringComparison.CurrentCultureIgnoreCase);

    /// <summary>Check if the string values are equal.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool EqualsCaseSensitive(string text, string value) => value.Equals(text, StringComparison.CurrentCulture);

    /// <summary>Check if the string values are equal.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool EqualsOrdinal(string text, string value) => value.Equals(text, StringComparison.OrdinalIgnoreCase);

    /// <summary>Check if the string values are equal.</summary>
    /// <param name="text">The AutoCompleteBox prefix text.</param>
    /// <param name="value">The item's string value.</param>
    /// <returns>Returns true if the condition is met.</returns>
    public static bool EqualsOrdinalCaseSensitive(string text, string value) => value.Equals(text, StringComparison.Ordinal);
  }
}
