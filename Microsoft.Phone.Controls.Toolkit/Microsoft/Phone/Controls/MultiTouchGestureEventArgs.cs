// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MultiTouchGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The base class for multi-touch gesture event args. Currently used only for
  /// two-finger (pinch) operations.
  /// </summary>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
  public class MultiTouchGestureEventArgs : GestureEventArgs
  {
    /// <summary>The second touch point's initial position</summary>
    protected Point GestureOrigin2 { get; private set; }

    /// <summary>
    /// The second touch point. The first is stored in GestureEventArgs.
    /// </summary>
    protected Point TouchPosition2 { get; private set; }

    internal MultiTouchGestureEventArgs(
      Point gestureOrigin,
      Point gestureOrigin2,
      Point position,
      Point position2)
      : base(gestureOrigin, position)
    {
      this.GestureOrigin2 = gestureOrigin2;
      this.TouchPosition2 = position2;
    }

    /// <summary>
    /// Returns the position of either of the two touch points (0 or 1) relative to
    /// the UIElement provided.
    /// </summary>
    /// <param name="relativeTo">The return value will be relative to this element.</param>
    /// <param name="index">The touchpoint to use (0 or 1).</param>
    /// <returns>The gesture's starting point relative to the given UIElement.</returns>
    public Point GetPosition(UIElement relativeTo, int index)
    {
      if (index == 0)
        return this.GetPosition(relativeTo);
      if (index == 1)
        return GestureEventArgs.GetPosition(relativeTo, this.TouchPosition2);
      throw new ArgumentOutOfRangeException(nameof (index));
    }
  }
}
