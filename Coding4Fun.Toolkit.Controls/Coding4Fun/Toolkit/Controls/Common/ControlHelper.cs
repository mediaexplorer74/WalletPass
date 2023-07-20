// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ControlHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public class ControlHelper
  {
    internal static int MagicSpacingNumber = 12;

    [Obsolete("Made into extension, Moved to Coding4Fun.Toolkit.dll now, Namespace is System, ")]
    public static double CheckBound(double value, double max) => value.CheckBound(max);

    [Obsolete("Made into extension, Moved to Coding4Fun.Toolkit.dll now, Namespace is System, ")]
    public static double CheckBound(double value, double min, double max) => value.CheckBound(min, max);

    public static void CreateDoubleAnimations(
      Storyboard sb,
      DependencyObject target,
      string propertyPath,
      double fromValue = 0.0,
      double toValue = 0.0,
      int speed = 500)
    {
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.To = new double?(toValue);
      doubleAnimation1.From = new double?(fromValue);
      ((Timeline) doubleAnimation1).Duration = new Duration(TimeSpan.FromMilliseconds((double) speed));
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTarget((Timeline) doubleAnimation2, target);
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, new PropertyPath(propertyPath, new object[0]));
      ((PresentationFrameworkCollection<Timeline>) sb.Children).Add((Timeline) doubleAnimation2);
    }
  }
}
