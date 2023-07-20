// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TurnstileFeatherTransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides turnstile feather <see cref="T:Microsoft.Phone.Controls.ITransition" />s.
  /// </summary>
  public class TurnstileFeatherTransition : TransitionElement
  {
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the
    /// <see cref="T:Microsoft.Phone.Controls.TurnstileTransitionMode" />.
    /// </summary>
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (TurnstileFeatherTransitionMode), typeof (TurnstileFeatherTransition), (PropertyMetadata) null);
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the time at which the transition should begin.
    /// </summary>
    public static readonly DependencyProperty BeginTimeProperty = DependencyProperty.Register(nameof (BeginTime), typeof (TimeSpan?), typeof (TurnstileFeatherTransition), new PropertyMetadata((object) TimeSpan.Zero));

    /// <summary>
    /// The <see cref="T:Microsoft.Phone.Controls.TurnstileTransitionMode" />.
    /// </summary>
    public TurnstileFeatherTransitionMode Mode
    {
      get => (TurnstileFeatherTransitionMode) this.GetValue(TurnstileFeatherTransition.ModeProperty);
      set => this.SetValue(TurnstileFeatherTransition.ModeProperty, (object) value);
    }

    /// <summary>The time at which the transition should begin.</summary>
    public TimeSpan? BeginTime
    {
      get => (TimeSpan?) this.GetValue(TurnstileFeatherTransition.BeginTimeProperty);
      set => this.SetValue(TurnstileFeatherTransition.BeginTimeProperty, (object) value);
    }

    /// <summary>
    /// Creates a new
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />.
    /// Saves and clears the existing
    /// <see cref="F:System.Windows.UIElement.ProjectionProperty" />
    /// value before the start of the transition, then restores it after it is stopped or completed.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public override ITransition GetTransition(UIElement element) => Transitions.TurnstileFeather(element, this.Mode, this.BeginTime);
  }
}
