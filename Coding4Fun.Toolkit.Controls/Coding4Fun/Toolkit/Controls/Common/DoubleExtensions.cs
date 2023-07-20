// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.DoubleExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;

namespace Coding4Fun.Toolkit.Controls.Common
{
  [Obsolete("Moved to Coding4Fun.Toolkit.dll now, Namespace is System, ")]
  public static class DoubleExtensions
  {
    public static double CheckBound(this double value, double maximum) => System.DoubleExtensions.CheckBound(value, maximum);

    public static double CheckBound(this double value, double minimum, double maximum) => System.DoubleExtensions.CheckBound(value, minimum, maximum);

    public static bool AlmostEquals(this double a, double b, double precision = 4.94065645841247E-324) => System.DoubleExtensions.AlmostEquals(a, b, precision);
  }
}
