// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.BooleanToVisibilityConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class BooleanToVisibilityConverter : ValueConverter
  {
    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      bool flag = System.Convert.ToBoolean(value);
      if (parameter != null)
        flag = !flag;
      return (object) (Visibility) (flag ? 0 : 1);
    }

    public override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return (object) value.Equals((object) (Visibility) 0);
    }
  }
}
