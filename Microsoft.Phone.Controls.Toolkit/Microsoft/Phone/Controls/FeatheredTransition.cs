// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.FeatheredTransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Controls an
  /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
  /// in order to produce a feathered animation on a set of
  /// <see cref="T:System.Windows.UIElement" />.
  /// </summary>
  public class FeatheredTransition : Transition
  {
    /// <summary>
    /// The <see cref="T:Microsoft.Phone.Controls.TurnstileFeatherTransitionMode" />.
    /// </summary>
    private TurnstileFeatherTransitionMode _mode;
    /// <summary>The time at which the transition should begin.</summary>
    private TimeSpan? _beginTime;

    /// <summary>
    /// Constructs a
    /// <see cref="T:Microsoft.Phone.Controls.FeatheredTransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// and a
    /// <see cref="T:System.Windows.Media.Animation.Storyboard" />,
    /// based on a
    /// <see cref="T:Microsoft.Phone.Controls.TurnstileFeatherTransitionMode" />
    /// </summary>
    /// <param name="element">
    /// The <see cref="T:System.Windows.UIElement" />.
    /// </param>
    /// <param name="storyboard">
    /// The <see cref="T:System.Windows.Media.Animation.Storyboard" />.
    /// </param>
    /// <param name="mode">
    /// The <see cref="T:Microsoft.Phone.Controls.TurnstileFeatherTransitionMode" />.
    /// </param>
    /// <param name="beginTime">
    /// The time at which the transition should begin.
    /// </param>
    public FeatheredTransition(
      UIElement element,
      Storyboard storyboard,
      TurnstileFeatherTransitionMode mode,
      TimeSpan? beginTime)
      : base(element, storyboard)
    {
      this._mode = mode;
      this._beginTime = beginTime;
    }

    /// <summary>
    /// Composes the
    /// <see cref="M:System.Windows.Media.Animation.Storyboard" />
    /// and mirrors
    /// <see cref="M:System.Windows.Media.Animation.Storyboard.Begin" />.
    /// </summary>
    public override void Begin()
    {
      TurnstileFeatherEffect.ComposeStoryboard(this.Storyboard, this._beginTime, this._mode);
      base.Begin();
    }
  }
}
