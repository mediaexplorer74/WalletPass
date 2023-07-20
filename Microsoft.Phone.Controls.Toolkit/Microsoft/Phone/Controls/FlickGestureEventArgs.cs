// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.FlickGestureEventArgs
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>The event args used by the Flick event.</summary>
  public class FlickGestureEventArgs : GestureEventArgs
  {
    private Point _velocity;

    internal FlickGestureEventArgs(Point hostOrigin, Point velocity)
      : base(hostOrigin, hostOrigin)
    {
      this._velocity = velocity;
    }

    /// <summary>The horizontal (X) velocity of the flick.</summary>
    public double HorizontalVelocity => this._velocity.X;

    /// <summary>The vertical (Y) velocity of the flick.</summary>
    public double VerticalVelocity => this._velocity.Y;

    /// <summary>The angle of the flick.</summary>
    public double Angle => MathHelpers.GetAngle(this._velocity.X, this._velocity.Y);

    /// <summary>
    /// The direction of the flick gesture, as determined by the flick velocities.
    /// </summary>
    public Orientation Direction => Math.Abs(this._velocity.X) >= Math.Abs(this._velocity.Y) ? (Orientation) 1 : (Orientation) 0;
  }
}
