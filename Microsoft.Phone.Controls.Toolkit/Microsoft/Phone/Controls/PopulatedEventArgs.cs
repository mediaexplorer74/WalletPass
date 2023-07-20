// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PopulatedEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides data for the
  /// <see cref="E:System.Windows.Controls.AutoCompleteBox.Populated" />
  /// event.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public class PopulatedEventArgs : RoutedEventArgs
  {
    /// <summary>
    /// Gets the list of possible matches added to the drop-down portion of
    /// the <see cref="T:System.Windows.Controls.AutoCompleteBox" />
    /// control.
    /// </summary>
    /// <value>The list of possible matches added to the
    /// <see cref="T:System.Windows.Controls.AutoCompleteBox" />.</value>
    public IEnumerable Data { get; private set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:System.Windows.Controls.PopulatedEventArgs" />.
    /// </summary>
    /// <param name="data">The list of possible matches added to the
    /// drop-down portion of the
    /// <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.</param>
    public PopulatedEventArgs(IEnumerable data) => this.Data = data;
  }
}
