// Decompiled with JetBrains decompiler
// Type: WPControls.BooleanToVisibilityConverter
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPControls
{
  public class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is bool flag && flag ? (object) (Visibility) 0 : (object) (Visibility) 1;

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) null;
    }
  }
}
