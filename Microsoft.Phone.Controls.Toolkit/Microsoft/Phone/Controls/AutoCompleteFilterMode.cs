// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.AutoCompleteFilterMode
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Specifies how text in the text box portion of the
  /// <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control is used
  /// to filter items specified by the
  /// <see cref="P:System.Windows.Controls.AutoCompleteBox.ItemsSource" />
  /// property for display in the drop-down.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public enum AutoCompleteFilterMode
  {
    /// <summary>
    /// Specifies that no filter is used. All items are returned.
    /// </summary>
    None,
    /// <summary>
    /// Specifies a culture-sensitive, case-insensitive filter where the
    /// returned items start with the specified text. The filter uses the
    /// <see cref="M:System.String.StartsWith(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.CurrentCultureIgnoreCase" /> as
    /// the string comparison criteria.
    /// </summary>
    StartsWith,
    /// <summary>
    /// Specifies a culture-sensitive, case-sensitive filter where the
    /// returned items start with the specified text. The filter uses the
    /// <see cref="M:System.String.StartsWith(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.CurrentCulture" /> as the string
    /// comparison criteria.
    /// </summary>
    StartsWithCaseSensitive,
    /// <summary>
    /// Specifies an ordinal, case-insensitive filter where the returned
    /// items start with the specified text. The filter uses the
    /// <see cref="M:System.String.StartsWith(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.OrdinalIgnoreCase" /> as the
    /// string comparison criteria.
    /// </summary>
    StartsWithOrdinal,
    /// <summary>
    /// Specifies an ordinal, case-sensitive filter where the returned items
    /// start with the specified text. The filter uses the
    /// <see cref="M:System.String.StartsWith(System.String,System.StringComparison)" />
    /// method, specifying <see cref="P:System.StringComparer.Ordinal" /> as
    /// the string comparison criteria.
    /// </summary>
    StartsWithOrdinalCaseSensitive,
    /// <summary>
    /// Specifies a culture-sensitive, case-insensitive filter where the
    /// returned items contain the specified text.
    /// </summary>
    Contains,
    /// <summary>
    /// Specifies a culture-sensitive, case-sensitive filter where the
    /// returned items contain the specified text.
    /// </summary>
    ContainsCaseSensitive,
    /// <summary>
    /// Specifies an ordinal, case-insensitive filter where the returned
    /// items contain the specified text.
    /// </summary>
    ContainsOrdinal,
    /// <summary>
    /// Specifies an ordinal, case-sensitive filter where the returned items
    /// contain the specified text.
    /// </summary>
    ContainsOrdinalCaseSensitive,
    /// <summary>
    /// Specifies a culture-sensitive, case-insensitive filter where the
    /// returned items equal the specified text. The filter uses the
    /// <see cref="M:System.String.Equals(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.CurrentCultureIgnoreCase" /> as
    /// the search comparison criteria.
    /// </summary>
    Equals,
    /// <summary>
    /// Specifies a culture-sensitive, case-sensitive filter where the
    /// returned items equal the specified text. The filter uses the
    /// <see cref="M:System.String.Equals(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.CurrentCulture" /> as the string
    /// comparison criteria.
    /// </summary>
    EqualsCaseSensitive,
    /// <summary>
    /// Specifies an ordinal, case-insensitive filter where the returned
    /// items equal the specified text. The filter uses the
    /// <see cref="M:System.String.Equals(System.String,System.StringComparison)" />
    /// method, specifying
    /// <see cref="P:System.StringComparer.OrdinalIgnoreCase" /> as the
    /// string comparison criteria.
    /// </summary>
    EqualsOrdinal,
    /// <summary>
    /// Specifies an ordinal, case-sensitive filter where the returned items
    /// equal the specified text. The filter uses the
    /// <see cref="M:System.String.Equals(System.String,System.StringComparison)" />
    /// method, specifying <see cref="P:System.StringComparer.Ordinal" /> as
    /// the string comparison criteria.
    /// </summary>
    EqualsOrdinalCaseSensitive,
    /// <summary>
    /// Specifies that a custom filter is used. This mode is used when the
    /// <see cref="P:System.Windows.Controls.AutoCompleteBox.TextFilter" />
    /// or
    /// <see cref="P:System.Windows.Controls.AutoCompleteBox.ItemFilter" />
    /// properties are set.
    /// </summary>
    Custom,
  }
}
