// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.AutoCompleteFilterPredicate`1
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents the filter used by the
  /// <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control to
  /// determine whether an item is a possible match for the specified text.
  /// </summary>
  /// <returns>true to indicate <paramref name="item" /> is a possible match
  /// for <paramref name="search" />; otherwise false.</returns>
  /// <param name="search">The string used as the basis for filtering.</param>
  /// <param name="item">The item that is compared with the
  /// <paramref name="search" /> parameter.</param>
  /// <typeparam name="T">The type used for filtering the
  /// <see cref="T:System.Windows.Controls.AutoCompleteBox" />. This type can
  /// be either a string or an object.</typeparam>
  /// <QualityBand>Stable</QualityBand>
  public delegate bool AutoCompleteFilterPredicate<T>(string search, T item);
}
