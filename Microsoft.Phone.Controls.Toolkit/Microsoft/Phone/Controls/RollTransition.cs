// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RollTransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides roll <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
  /// </summary>
  public class RollTransition : TransitionElement
  {
    /// <summary>
    /// Creates a new
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public override ITransition GetTransition(UIElement element) => Transitions.Roll(element);
  }
}
