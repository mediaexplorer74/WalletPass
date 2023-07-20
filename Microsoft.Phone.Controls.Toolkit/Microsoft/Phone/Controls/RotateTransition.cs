// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RotateTransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides rotate <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
  /// </summary>
  public class RotateTransition : TransitionElement
  {
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the
    /// <see cref="T:Microsoft.Phone.Controls.RotateTransitionMode" />.
    /// </summary>
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (RotateTransitionMode), typeof (RotateTransition), (PropertyMetadata) null);

    /// <summary>
    /// The <see cref="T:Microsoft.Phone.Controls.RotateTransitionMode" />.
    /// </summary>
    public RotateTransitionMode Mode
    {
      get => (RotateTransitionMode) this.GetValue(RotateTransition.ModeProperty);
      set => this.SetValue(RotateTransition.ModeProperty, (object) value);
    }

    /// <summary>
    /// Creates a new
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// Saves and clears the existing
    /// <see cref="F:System.Windows.UIElement.RenderTransformProperty" />
    /// value before the start of the transition, then restores it after it is stopped or completed.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public override ITransition GetTransition(UIElement element) => Transitions.Rotate(element, this.Mode);
  }
}
