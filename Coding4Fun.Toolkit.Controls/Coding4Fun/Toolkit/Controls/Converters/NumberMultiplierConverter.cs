// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.NumberMultiplierConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class NumberMultiplierConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      double result1;
      double.TryParse(value.ToString(), out result1);
      double result2;
      double.TryParse(parameter.ToString(), out result2);
      return (object) (result1 * result2);
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
