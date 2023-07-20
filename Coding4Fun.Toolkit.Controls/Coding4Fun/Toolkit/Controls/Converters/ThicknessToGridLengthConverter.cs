// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.ThicknessToGridLengthConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class ThicknessToGridLengthConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      if (value == null || parameter == null)
        return (object) new GridLength();
      object obj = Enum.Parse(typeof (ThicknessProperties), parameter.ToString(), true);
      Thickness thickness = (Thickness) value;
      if (obj == null)
        return (object) new GridLength();
      double num;
      switch ((ThicknessProperties) obj)
      {
        case ThicknessProperties.Left:
          num = thickness.Left;
          break;
        case ThicknessProperties.Right:
          num = thickness.Right;
          break;
        case ThicknessProperties.Bottom:
          num = thickness.Bottom;
          break;
        default:
          num = thickness.Top;
          break;
      }
      return (object) new GridLength(num);
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return (object) null;
    }
  }
}
