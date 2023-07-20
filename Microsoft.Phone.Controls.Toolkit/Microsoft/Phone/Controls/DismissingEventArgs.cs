// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DismissingEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.ComponentModel;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides data for the CustomMessageBox's Dismissing event.
  /// </summary>
  public class DismissingEventArgs : CancelEventArgs
  {
    /// <summary>
    /// Initializes a new instance of the DismissingEventArgs class.
    /// </summary>
    /// <param name="result">The result value.</param>
    public DismissingEventArgs(CustomMessageBoxResult result) => this.Result = result;

    /// <summary>Gets or sets the result value.</summary>
    public CustomMessageBoxResult Result { get; private set; }
  }
}
