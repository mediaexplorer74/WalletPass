// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PinchGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// The event args used by the PinchDelta and PinchCompleted events.
  /// </summary>
  public class PinchGestureEventArgs : MultiTouchGestureEventArgs
  {
    internal PinchGestureEventArgs(
      Point gestureOrigin,
      Point gestureOrigin2,
      Point position,
      Point position2)
      : base(gestureOrigin, gestureOrigin2, position, position2)
    {
    }

    /// <summary>
    /// Returns the ratio of the current distance between touchpoints / the original distance
    /// between the touchpoints.
    /// </summary>
    public double DistanceRatio
    {
      get
      {
        double num = Math.Max(MathHelpers.GetDistance(this.GestureOrigin, this.GestureOrigin2), 1.0);
        return Math.Max(MathHelpers.GetDistance(this.TouchPosition, this.TouchPosition2), 1.0) / num;
      }
    }

    /// <summary>
    /// Returns the difference in angle between the current touch positions and the original
    /// touch positions.
    /// </summary>
    public double TotalAngleDelta
    {
      get
      {
        Point point1 = this.GestureOrigin2;
        double x1 = point1.X;
        point1 = this.GestureOrigin;
        double x2 = point1.X;
        double deltaX1 = x1 - x2;
        point1 = this.GestureOrigin2;
        double y1 = point1.Y;
        point1 = this.GestureOrigin;
        double y2 = point1.Y;
        double deltaY1 = y1 - y2;
        double angle = MathHelpers.GetAngle(deltaX1, deltaY1);
        Point point2 = this.TouchPosition2;
        double x3 = point2.X;
        point2 = this.TouchPosition;
        double x4 = point2.X;
        double deltaX2 = x3 - x4;
        point2 = this.TouchPosition2;
        double y3 = point2.Y;
        point2 = this.TouchPosition;
        double y4 = point2.Y;
        double deltaY2 = y3 - y4;
        return MathHelpers.GetAngle(deltaX2, deltaY2) - angle;
      }
    }
  }
}
