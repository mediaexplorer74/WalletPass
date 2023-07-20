// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.VisibilityConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// If there is a new notification (value)
  /// Returns a Visible value for the notification block (parameter).
  /// Or a Collapsed value for the message block (parameter).
  /// Returns a opposite values otherwise.
  /// </summary>
  internal class VisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool) value ^ (bool) parameter ? (object) (Visibility) 0 : (object) (Visibility) 1;

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
