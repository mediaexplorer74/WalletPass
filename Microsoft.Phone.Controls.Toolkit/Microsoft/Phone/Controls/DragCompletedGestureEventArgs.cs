// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DragCompletedGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>The event args used by the DragCompleted event.</summary>
  public class DragCompletedGestureEventArgs : GestureEventArgs
  {
    internal DragCompletedGestureEventArgs(
      Point gestureOrigin,
      Point currentPosition,
      Point change,
      Orientation direction,
      Point finalVelocity)
      : base(gestureOrigin, currentPosition)
    {
      this.HorizontalChange = change.X;
      this.VerticalChange = change.Y;
      this.Direction = direction;
      this.HorizontalVelocity = finalVelocity.X;
      this.VerticalVelocity = finalVelocity.Y;
    }

    /// <summary>The total horizontal (X) change of the drag event.</summary>
    public double HorizontalChange { get; private set; }

    /// <summary>The total vertical (Y) change of the drag event.</summary>
    public double VerticalChange { get; private set; }

    /// <summary>
    /// The direction of the drag gesture, as determined by the initial drag change.
    /// </summary>
    public Orientation Direction { get; private set; }

    /// <summary>
    /// The final horizontal (X) velocity of the drag, if the drag was inertial.
    /// </summary>
    public double HorizontalVelocity { get; private set; }

    /// <summary>
    /// The final vertical (Y) velocity of the drag, if the drag was inertial.
    /// </summary>
    public double VerticalVelocity { get; private set; }
  }
}
