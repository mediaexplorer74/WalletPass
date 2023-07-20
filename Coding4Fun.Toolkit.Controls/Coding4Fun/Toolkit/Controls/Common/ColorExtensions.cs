// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ColorExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls.Common
{
  public static class ColorExtensions
  {
    public static float GetHue(this Color color)
    {
      if ((int) color.R == (int) color.G && (int) color.G == (int) color.B)
        return 0.0f;
      float a1 = (float) color.R / (float) byte.MaxValue;
      float a2 = (float) color.G / (float) byte.MaxValue;
      float num1 = (float) color.B / (float) byte.MaxValue;
      float num2 = Coding4Fun.Toolkit.Numbers.Min(new float[3]
      {
        a1,
        a2,
        num1
      });
      float b = Coding4Fun.Toolkit.Numbers.Max(new float[3]
      {
        a1,
        a2,
        num1
      });
      float num3 = b - num2;
      float hue = (!a1.AlmostEquals(b) ? (!a2.AlmostEquals(b) ? (float) (4.0 + ((double) a1 - (double) a2) / (double) num3) : (float) (2.0 + ((double) num1 - (double) a1) / (double) num3)) : (a2 - num1) / num3) * 60f;
      if ((double) hue < 0.0)
        hue += 360f;
      return hue;
    }

    public static HSV GetHSV(this Color color) => new HSV()
    {
      Hue = color.GetHue(),
      Saturation = color.GetSaturation(),
      Value = color.GetValue()
    };

    public static float GetSaturation(this Color color)
    {
      float num1 = (float) color.R / (float) byte.MaxValue;
      float num2 = (float) color.G / (float) byte.MaxValue;
      float num3 = (float) color.B / (float) byte.MaxValue;
      float b = Coding4Fun.Toolkit.Numbers.Min(new float[3]
      {
        num1,
        num2,
        num3
      });
      float a = Coding4Fun.Toolkit.Numbers.Max(new float[3]
      {
        num1,
        num2,
        num3
      });
      return a.AlmostEquals(b) || a.AlmostEquals(0.0f) ? 0.0f : (float) (1.0 - 1.0 * (double) b / (double) a);
    }

    public static float GetValue(this Color color) => Coding4Fun.Toolkit.Numbers.Max(new float[3]
    {
      (float) color.R / (float) byte.MaxValue,
      (float) color.G / (float) byte.MaxValue,
      (float) color.B / (float) byte.MaxValue
    });
  }
}
