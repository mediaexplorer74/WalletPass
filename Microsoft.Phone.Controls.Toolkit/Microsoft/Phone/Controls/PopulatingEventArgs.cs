// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PopulatingEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides data for the
  /// <see cref="E:System.Windows.Controls.AutoCompleteBox.Populating" />
  /// event.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public class PopulatingEventArgs : RoutedEventArgs
  {
    /// <summary>
    /// Gets the text that is used to determine which items to display in
    /// the <see cref="T:System.Windows.Controls.AutoCompleteBox" />
    /// control.
    /// </summary>
    /// <value>The text that is used to determine which items to display in
    /// the <see cref="T:System.Windows.Controls.AutoCompleteBox" />.</value>
    public string Parameter { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the
    /// <see cref="E:System.Windows.Controls.AutoCompleteBox.Populating" />
    /// event should be canceled.
    /// </summary>
    /// <value>True to cancel the event, otherwise false. The default is
    /// false.</value>
    public bool Cancel { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="T:System.Windows.Controls.PopulatingEventArgs" />.
    /// </summary>
    /// <param name="parameter">The value of the
    /// <see cref="P:System.Windows.Controls.AutoCompleteBox.SearchText" />
    /// property, which is used to filter items for the
    /// <see cref="T:System.Windows.Controls.AutoCompleteBox" /> control.</param>
    public PopulatingEventArgs(string parameter) => this.Parameter = parameter;
  }
}
