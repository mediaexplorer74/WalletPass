// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ITransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>Controls the behavior of transitions.</summary>
  public interface ITransition
  {
    /// <summary>
    /// Occurs when the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// has completed playing.
    /// </summary>
    event EventHandler Completed;

    /// <summary>
    /// Gets the
    /// <see cref="T:System.Windows.Media.Animation.ClockState" />
    /// of the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    /// <returns></returns>
    ClockState GetCurrentState();

    /// <summary>
    /// Gets the current time of the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    /// <returns>The current time.</returns>
    TimeSpan GetCurrentTime();

    /// <summary>
    /// Pauses the animation clock associated with the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    void Pause();

    /// <summary>
    /// Resumes the animation clock, or run-time state, associated with the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    void Resume();

    /// <summary>
    /// Moves the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// to the specified animation position. The
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// performs the requested seek when the next clock tick occurs.
    /// </summary>
    /// <param name="offset">The specified animation position.</param>
    void Seek(TimeSpan offset);

    /// <summary>
    /// Moves the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// to the specified animation position immediately (synchronously).
    /// </summary>
    /// <param name="offset">The specified animation position</param>
    void SeekAlignedToLastTick(TimeSpan offset);

    /// <summary>
    /// Advances the current time of the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />'s
    /// clock to the end of its active period.
    /// </summary>
    void SkipToFill();

    /// <summary>
    /// Initiates the set of animations associated with the
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    void Begin();

    /// <summary>
    /// Stops the <see cref="T:Microsoft.Phone.Controls.ITransition" />.
    /// </summary>
    void Stop();
  }
}
