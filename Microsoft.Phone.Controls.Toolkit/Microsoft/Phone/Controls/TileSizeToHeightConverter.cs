// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TileSizeToHeightConverter
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
  /// Returns the Hub tile height corresponding to a tile size.
  /// </summary>
  public class TileSizeToHeightConverter : IValueConverter
  {
    /// <summary>
    /// Converts from a tile size to the corresponding height.
    /// </summary>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      double num = 0.0;
      switch ((TileSize) value)
      {
        case TileSize.Default:
          num = 173.0;
          break;
        case TileSize.Small:
          num = 99.0;
          break;
        case TileSize.Medium:
          num = 210.0;
          break;
        case TileSize.Large:
          num = 210.0;
          break;
      }
      double result;
      if (parameter == null || !double.TryParse(parameter.ToString(), out result))
        result = 1.0;
      return (object) (num * result);
    }

    /// <summary>Not used.</summary>
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
