// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.ValueConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows.Data;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public abstract class ValueConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => this.Convert(value, targetType, parameter, culture, (string) null);

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return this.ConvertBack(value, targetType, parameter, culture, (string) null);
    }

    public object Convert(object value, Type targetType, object parameter, string language) => this.Convert(value, targetType, parameter, (CultureInfo) null, language);

    public object ConvertBack(object value, Type targetType, object parameter, string language) => this.ConvertBack(value, targetType, parameter, (CultureInfo) null, language);

    public virtual object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      throw new NotImplementedException();
    }

    public virtual object ConvertBack(
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
