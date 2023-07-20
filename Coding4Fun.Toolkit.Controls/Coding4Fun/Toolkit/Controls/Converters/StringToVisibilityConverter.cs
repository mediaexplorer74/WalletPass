// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Converters.StringToVisibilityConverter
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;

namespace Coding4Fun.Toolkit.Controls.Converters
{
  public class StringToVisibilityConverter : ValueConverter
  {
    public bool Inverted { get; set; }

    public override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture,
      string language)
    {
      return this.Inverted ? (object) (Visibility) (string.IsNullOrEmpty(value as string) ? 0 : 1) : (object) (Visibility) (string.IsNullOrEmpty(value as string) ? 1 : 0);
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
