// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.PhysicsConstants
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Coding4Fun.Toolkit.Controls
{
  public static class PhysicsConstants
  {
    public static double GetStopTime(Point initialVelocity) => PhysicsConstants.GetStopTime(initialVelocity, 0.2, 4000.0, 80.0);

    public static double GetStopTime(
      Point initialVelocity,
      double friction,
      double maximumSpeed,
      double parkingSpeed)
    {
      double num = Math.Min(Math.Sqrt(initialVelocity.X * initialVelocity.X + initialVelocity.Y * initialVelocity.Y), maximumSpeed);
      return parkingSpeed >= num ? 0.0 : Math.Log(parkingSpeed / num) / Math.Log(friction);
    }

    public static Point GetStopPoint(Point initialVelocity) => PhysicsConstants.GetStopPoint(initialVelocity, 0.2, 4000.0, 80.0);

    public static Point GetStopPoint(
      Point initialVelocity,
      double friction,
      double maximumSpeed,
      double parkingSpeed)
    {
      double num1 = Math.Sqrt(initialVelocity.X * initialVelocity.X + initialVelocity.Y * initialVelocity.Y);
      Point initialVelocity1 = initialVelocity;
      if (num1 > maximumSpeed && num1 > 0.0)
      {
        initialVelocity1.X *= maximumSpeed / num1;
        initialVelocity1.Y *= maximumSpeed / num1;
      }
      double num2 = (Math.Pow(friction, PhysicsConstants.GetStopTime(initialVelocity1, friction, maximumSpeed, parkingSpeed)) - 1.0) / Math.Log(friction);
      return new Point(initialVelocity1.X * num2, initialVelocity1.Y * num2);
    }

    public static IEasingFunction GetEasingFunction(double stopTime) => PhysicsConstants.GetEasingFunction(stopTime, 0.2);

    public static IEasingFunction GetEasingFunction(double stopTime, double friction)
    {
      ExponentialEase easingFunction = new ExponentialEase();
      easingFunction.Exponent = stopTime * Math.Log(friction);
      ((EasingFunctionBase) easingFunction).EasingMode = (EasingMode) 1;
      return (IEasingFunction) easingFunction;
    }
  }
}
