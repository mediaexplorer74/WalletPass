// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.FloatExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls.Common
{
  [Obsolete("Moved to Coding4Fun.Toolkit.dll now, Namespace is System")]
  public static class FloatExtensions
  {
    public static double CheckBound(this float value, float maximum) => System.FloatExtensions.CheckBound(value, maximum);

    public static double CheckBound(this float value, float minimum, float maximum) => System.FloatExtensions.CheckBound(value, minimum, maximum);

    public static bool AlmostEquals(this float a, float b, double precision = 1.4012984643248171E-45) => System.FloatExtensions.AlmostEquals(a, b, precision);
  }
}
