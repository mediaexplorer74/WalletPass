// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.OffOnConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.LocalizedResources;
using Microsoft.Phone.Controls.Properties;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Microsoft.Phone.Controls
{
  /// <summary>Converts bool? values to "Off" and "On" strings.</summary>
  /// <QualityBand>Preview</QualityBand>
  public class OffOnConverter : IValueConverter
  {
    /// <summary>
    /// Converts a boolean to the equivalent On or Off string.
    /// </summary>
    /// <param name="value">The given boolean.</param>
    /// <param name="targetType">
    /// The type corresponding to the binding property, which must be of
    /// <see cref="T:System.String" />.
    /// </param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">(Not used).</param>
    /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if ((object) targetType == null)
        throw new ArgumentNullException(nameof (targetType));
      if ((object) targetType != (object) typeof (object))
        throw new ArgumentException(Resources.UnexpectedType, nameof (targetType));
      bool? nullable = value is bool? || value == null ? (bool?) value : throw new ArgumentException(Resources.UnexpectedType, nameof (value));
      return (!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) != 0 ? (object) ControlResources.On : (object) ControlResources.Off;
    }

    /// <summary>Not implemented.</summary>
    /// <param name="value">(Not used).</param>
    /// <param name="targetType">(Not used).</param>
    /// <param name="parameter">(Not used).</param>
    /// <param name="culture">(Not used).</param>
    /// <returns>null</returns>
    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
