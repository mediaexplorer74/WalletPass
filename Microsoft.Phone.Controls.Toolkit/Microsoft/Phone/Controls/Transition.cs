// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Transition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Mirrors the
  /// <see cref="T:System.Windows.Media.Animation.Storyboard" />
  /// interface to control an
  /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
  /// for a
  /// <see cref="T:System.Windows.UIElement" />.
  /// Saves and restores the
  /// <see cref="P:System.Windows.UIElement.CacheMode" />
  /// and
  /// <see cref="P:System.Windows.UIElement.IsHitTestVisible" />
  /// values for the
  /// <see cref="T:System.Windows.UIElement" />.
  /// </summary>
  public class Transition : ITransition
  {
    /// <summary>
    /// The original
    /// <see cref="P:System.Windows.UIElement.CacheMode" />
    /// of the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    private CacheMode _cacheMode;
    /// <summary>
    /// The <see cref="T:System.Windows.UIElement" />
    /// that the transition will be applied to.
    /// </summary>
    private UIElement _element;
    /// <summary>
    /// The original
    /// <see cref="P:System.Windows.UIElement.IsHitTestVisible" />
    /// of the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    private bool _isHitTestVisible;
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.Media.Animation.Storyboard" />
    /// for the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    private Storyboard _storyboard;

    /// <summary>
    /// The property that identifies the
    /// <see cref="T:System.Windows.Media.Animation.Storyboard" />
    /// for the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    protected Storyboard Storyboard
    {
      get => this._storyboard;
      set
      {
        if (value == this._storyboard)
          return;
        if (this._storyboard != null)
          ((Timeline) this._storyboard).Completed -= new EventHandler(this.OnCompleted);
        this._storyboard = value;
        if (this._storyboard != null)
          ((Timeline) this._storyboard).Completed += new EventHandler(this.OnCompleted);
      }
    }

    /// <summary>
    /// Mirrors <see cref="E:System.Windows.Media.Animation.Storyboard.Completed" />.
    /// </summary>
    public event EventHandler Completed;

    /// <summary>
    /// Constructs a
    /// <see cref="T:Microsoft.Phone.Controls.Transition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// and a
    /// <see cref="T:System.Windows.Media.Animation.Storyboard" />.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="storyboard">The <see cref="T:System.Windows.Media.Animation.Storyboard" />.</param>
    public Transition(UIElement element, Storyboard storyboard)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (storyboard == null)
        throw new ArgumentNullException(nameof (storyboard));
      this._element = element;
      this.Storyboard = storyboard;
      ((Timeline) this.Storyboard).Completed += new EventHandler(this.OnCompletedRestore);
    }

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.Begin" />.
    /// </summary>
    public virtual void Begin()
    {
      this.Save();
      this.Storyboard.Begin();
    }

    /// <summary>Restores the settings for the transition.</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCompletedRestore(object sender, EventArgs e) => this.Restore();

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.GetCurrentState" />.
    /// </summary>
    public ClockState GetCurrentState() => this.Storyboard.GetCurrentState();

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.GetCurrentTime" />.
    /// </summary>
    public TimeSpan GetCurrentTime() => this.Storyboard.GetCurrentTime();

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.Pause" />.
    /// </summary>
    public void Pause() => this.Storyboard.Pause();

    /// <summary>
    /// Restores the saved
    /// <see cref="P:System.Windows.UIElement.CacheMode" />
    /// and
    /// <see cref="P:System.Windows.UIElement.IsHitTestVisible" />
    /// values for the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    private void Restore()
    {
      if (!(this._cacheMode is BitmapCache))
        this._element.CacheMode = this._cacheMode;
      if (this._isHitTestVisible)
        this._element.IsHitTestVisible = this._isHitTestVisible;
      else
        this._element.IsHitTestVisible = true;
    }

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.Resume" />.
    /// </summary>
    public void Resume() => this.Storyboard.Resume();

    /// <summary>
    /// Saves the
    /// <see cref="P:System.Windows.UIElement.CacheMode" />
    /// and
    /// <see cref="P:System.Windows.UIElement.IsHitTestVisible" />
    /// values for the
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    private void Save()
    {
      this._cacheMode = this._element.CacheMode;
      if (!(this._cacheMode is BitmapCache))
        this._element.CacheMode = TransitionFrame.BitmapCacheMode;
      this._isHitTestVisible = this._element.IsHitTestVisible;
      if (!this._isHitTestVisible)
        return;
      this._element.IsHitTestVisible = false;
    }

    /// <summary>
    /// Mirrors <see cref="E:System.Windows.Media.Animation.Storyboard.Completed" />.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void OnCompleted(object sender, EventArgs e)
    {
      EventHandler completed = this.Completed;
      if (completed == null)
        return;
      completed((object) this, e);
    }

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.Seek" />.
    /// </summary>
    /// <param name="offset">The time offset.</param>
    public void Seek(TimeSpan offset) => this.Storyboard.Seek(offset);

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.SeekAlignedToLastTick" />.
    /// </summary>
    /// <param name="offset">The time offset.</param>
    public void SeekAlignedToLastTick(TimeSpan offset) => this.Storyboard.SeekAlignedToLastTick(offset);

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.SkipToFill" />.
    /// </summary>
    public void SkipToFill() => this.Storyboard.SkipToFill();

    /// <summary>
    /// Mirrors <see cref="M:System.Windows.Media.Animation.Storyboard.Stop" />.
    /// </summary>
    public void Stop()
    {
      this.Storyboard.Stop();
      this.Restore();
    }
  }
}
