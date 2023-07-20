// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.MultipleToSingleLineStringConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Converts a multi-line string into a single line string.
  /// </summary>
  internal class MultipleToSingleLineStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string str = value as string;
      return string.IsNullOrEmpty(str) ? (object) string.Empty : (object) str.Replace(Environment.NewLine, " ");
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotSupportedException();
    }
  }
}
