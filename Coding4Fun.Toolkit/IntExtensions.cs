// Decompiled with JetBrains decompiler
// Type: System.IntExtensions
// Assembly: Coding4Fun.Toolkit, Version=2.0.8.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FB93589A-92C1-4C35-B9CB-579624E90C48
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.dll

namespace System
{
  public static class IntExtensions
  {
    public static double CheckBound(this int value, int maximum) => value.CheckBound(0, maximum);

    public static double CheckBound(this int value, int minimum, int maximum)
    {
      if (value <= minimum)
        value = minimum;
      else if (value >= maximum)
        value = maximum;
      return (double) value;
    }
  }
}
