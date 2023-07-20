// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MathHelpers
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Xna.Framework;
using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  internal static class MathHelpers
  {
    /// <summary>
    /// Return the angle of the hypotenuse of a triangle with
    /// sides defined by deltaX and deltaY.
    /// </summary>
    /// <param name="deltaX">Change in X.</param>
    /// <param name="deltaY">Change in Y.</param>
    /// <returns>The angle (in degrees).</returns>
    public static double GetAngle(double deltaX, double deltaY)
    {
      double num = Math.Atan2(deltaY, deltaX);
      if (num < 0.0)
        num = 2.0 * Math.PI + num;
      return num * 360.0 / (2.0 * Math.PI);
    }

    /// <summary>Return the distance between two points</summary>
    /// <param name="p0">The first point.</param>
    /// <param name="p1">The second point.</param>
    /// <returns>The distance between the two points.</returns>
    public static double GetDistance(Point p0, Point p1)
    {
      double num1 = p0.X - p1.X;
      double num2 = p0.Y - p1.Y;
      return Math.Sqrt(num1 * num1 + num2 * num2);
    }

    /// <summary>
    /// Helper extension method for turning XNA's Vector2 type into a Point
    /// </summary>
    /// <param name="v">The Vector2.</param>
    /// <returns>The point.</returns>
    public static Point ToPoint(this Vector2 v) => new Point((double) v.X, (double) v.Y);
  }
}
