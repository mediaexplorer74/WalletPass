// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.SolidColorBrushToColorConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class SolidColorBrushToColorConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      SolidColorBrush solidColorBrush = value as SolidColorBrush;
      Color color = Colors.Transparent;
      if (solidColorBrush != null)
        color = solidColorBrush.Color;
      byte result;
      if (parameter != null && byte.TryParse((string) parameter, out result))
        color.A = result;
      return (object) color;
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      throw new NotImplementedException();
    }
  }
}
