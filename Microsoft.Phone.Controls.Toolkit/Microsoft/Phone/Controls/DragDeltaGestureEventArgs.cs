// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.DragDeltaGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>The event args used by the DragDelta event.</summary>
  public class DragDeltaGestureEventArgs : GestureEventArgs
  {
    internal DragDeltaGestureEventArgs(
      Point gestureOrigin,
      Point currentPosition,
      Point change,
      Orientation direction)
      : base(gestureOrigin, currentPosition)
    {
      this.HorizontalChange = change.X;
      this.VerticalChange = change.Y;
      this.Direction = direction;
    }

    /// <summary>The horizontal (X) change for this drag event.</summary>
    public double HorizontalChange { get; private set; }

    /// <summary>The vertical (Y) change for this drag event.</summary>
    public double VerticalChange { get; private set; }

    /// <summary>
    /// The direction of the drag gesture, as determined by the initial drag change.
    /// </summary>
    public Orientation Direction { get; private set; }
  }
}
