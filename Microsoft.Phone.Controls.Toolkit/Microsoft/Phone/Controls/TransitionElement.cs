// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TransitionElement
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Transition factory for a particular transition family.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public abstract class TransitionElement : DependencyObject
  {
    /// <summary>
    /// Creates a new
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// Existing
    /// <see cref="F:System.Windows.UIElement.RenderTransformProperty" />
    /// or
    /// <see cref="F:System.Windows.UIElement.ProjectionProperty" />
    /// values may be saved and cleared before the start of the transition, then restored it after it is stopped or completed.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public abstract ITransition GetTransition(UIElement element);
  }
}
