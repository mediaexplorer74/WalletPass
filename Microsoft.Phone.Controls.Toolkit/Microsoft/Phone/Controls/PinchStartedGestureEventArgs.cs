// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.PinchStartedGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>The event args used by the PinchStarted event.</summary>
  public class PinchStartedGestureEventArgs : MultiTouchGestureEventArgs
  {
    internal PinchStartedGestureEventArgs(
      Point gestureOrigin,
      Point gestureOrigin2,
      Point pinch,
      Point pinch2)
      : base(gestureOrigin, gestureOrigin2, pinch, pinch2)
    {
    }

    /// <summary>The distance between the two touch points.</summary>
    public double Distance => MathHelpers.GetDistance(this.TouchPosition, this.TouchPosition2);

    /// <summary>The angle defined by the two touch points.</summary>
    public double Angle
    {
      get
      {
        Point point = this.TouchPosition2;
        double x1 = point.X;
        point = this.TouchPosition;
        double x2 = point.X;
        double deltaX = x1 - x2;
        point = this.TouchPosition2;
        double y1 = point.Y;
        point = this.TouchPosition;
        double y2 = point.Y;
        double deltaY = y1 - y2;
        return MathHelpers.GetAngle(deltaX, deltaY);
      }
    }
  }
}
