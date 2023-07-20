// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RoutedPropertyChangingEventHandler`1
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Represents methods that handle various routed events that track property
  /// values changing.  Typically the events denote a cancellable action.
  /// </summary>
  /// <typeparam name="T">
  /// The type of the value for the dependency property that is changing.
  /// </typeparam>
  /// <param name="sender">
  /// The object where the initiating property is changing.
  /// </param>
  /// <param name="e">Event data for the event.</param>
  /// <QualityBand>Preview</QualityBand>
  [SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances", Justification = "To match pattern of RoutedPropertyChangedEventHandler<T>")]
  public delegate void RoutedPropertyChangingEventHandler<T>(
    object sender,
    RoutedPropertyChangingEventArgs<T> e);
}
