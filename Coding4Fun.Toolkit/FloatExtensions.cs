// Decompiled with JetBrains decompiler
// Type: System.FloatExtensions
// Assembly: Coding4Fun.Toolkit, Version=2.0.8.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FB93589A-92C1-4C35-B9CB-579624E90C48
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.dll

namespace System
{
  public static class FloatExtensions
  {
    public static double CheckBound(this float value, float maximum) => value.CheckBound(0.0f, maximum);

    public static double CheckBound(this float value, float minimum, float maximum)
    {
      if ((double) value <= (double) minimum)
        value = minimum;
      else if ((double) value >= (double) maximum)
        value = maximum;
      return (double) value;
    }

    public static bool AlmostEquals(this float a, float b, double precision = 1.4012984643248171E-45) => (double) Math.Abs(a - b) <= precision;
  }
}
