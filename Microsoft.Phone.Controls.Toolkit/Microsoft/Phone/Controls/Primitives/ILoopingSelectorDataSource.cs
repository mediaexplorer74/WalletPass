// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Primitives.ILoopingSelectorDataSource
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls.Primitives
{
  /// <summary>
  /// Defines how the LoopingSelector communicates with data source.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public interface ILoopingSelectorDataSource
  {
    /// <summary>Get the next datum, relative to an existing datum.</summary>
    /// <param name="relativeTo">The datum the return value will be relative to.</param>
    /// <returns>The next datum.</returns>
    object GetNext(object relativeTo);

    /// <summary>
    /// Get the previous datum, relative to an existing datum.
    /// </summary>
    /// <param name="relativeTo">The datum the return value will be relative to.</param>
    /// <returns>The previous datum.</returns>
    object GetPrevious(object relativeTo);

    /// <summary>The selected item. Should never be null.</summary>
    object SelectedItem { get; set; }

    /// <summary>Raised when the selection changes.</summary>
    event EventHandler<SelectionChangedEventArgs> SelectionChanged;
  }
}
