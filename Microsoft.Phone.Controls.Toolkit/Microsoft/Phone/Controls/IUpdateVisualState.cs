// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.IUpdateVisualState
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The IUpdateVisualState interface is used to provide the
  /// InteractionHelper with access to the type's UpdateVisualState method.
  /// </summary>
  [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "This is not an exception class.")]
  internal interface IUpdateVisualState
  {
    /// <summary>Update the visual state of the control.</summary>
    /// <param name="useTransitions">
    /// A value indicating whether to automatically generate transitions to
    /// the new state, or instantly transition to the new state.
    /// </param>
    void UpdateVisualState(bool useTransitions);
  }
}
