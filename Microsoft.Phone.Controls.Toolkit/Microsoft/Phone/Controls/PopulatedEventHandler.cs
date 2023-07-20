// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PopulatedEventHandler
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents the method that will handle the
  /// <see cref="E:System.Windows.Controls.AutoCompleteBox.Populated" />
  /// event of a <see cref="T:System.Windows.Controls.AutoCompleteBox" />
  /// control.
  /// </summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">A
  /// <see cref="T:System.Windows.Controls.PopulatedEventArgs" /> that
  /// contains the event data.</param>
  /// <QualityBand>Stable</QualityBand>
  [SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Justification = "There is no generic RoutedEventHandler.")]
  public delegate void PopulatedEventHandler(object sender, PopulatedEventArgs e);
}
