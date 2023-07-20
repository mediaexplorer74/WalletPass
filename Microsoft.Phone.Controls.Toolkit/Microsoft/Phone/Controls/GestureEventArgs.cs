// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.GestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The base class for all gesture events. Also used by Tap, DoubleTap and Hold.
  /// </summary>
  public class GestureEventArgs : EventArgs
  {
    /// <summary>
    /// The point, in unrotated screen coordinates, where the gesture occurred.
    /// </summary>
    protected Point GestureOrigin { get; private set; }

    /// <summary>
    /// The point, in unrotated screen coordinates, where the first touchpoint is now.
    /// </summary>
    protected Point TouchPosition { get; private set; }

    internal GestureEventArgs(Point gestureOrigin, Point position)
    {
      this.GestureOrigin = gestureOrigin;
      this.TouchPosition = position;
    }

    /// <summary>
    /// The first hit-testable item under the touch point. Determined by a combination of order in the tree and
    /// Z-order.
    /// </summary>
    public object OriginalSource { get; internal set; }

    /// <summary>
    /// If an event handler sets this to true, it stops event bubbling.
    /// </summary>
    public bool Handled { get; set; }

    /// <summary>
    /// Returns the position of the gesture's starting point relative to a given UIElement.
    /// </summary>
    /// <param name="relativeTo">The return value will be relative to this element.</param>
    /// <returns>The gesture's starting point relative to the given UIElement.</returns>
    public Point GetPosition(UIElement relativeTo) => GestureEventArgs.GetPosition(relativeTo, this.TouchPosition);

    /// <summary>
    /// Returns the position of a given point relative to a given UIElement.
    /// </summary>
    /// <param name="relativeTo">The return value will be relative to this element.</param>
    /// <param name="point">The point to translate.</param>
    /// <returns>The given point relative to the given UIElement.</returns>
    protected static Point GetPosition(UIElement relativeTo, Point point)
    {
      if (relativeTo == null)
        relativeTo = Application.Current.RootVisual;
      return relativeTo != null ? relativeTo.TransformToVisual((UIElement) null).Inverse.Transform(point) : point;
    }
  }
}
