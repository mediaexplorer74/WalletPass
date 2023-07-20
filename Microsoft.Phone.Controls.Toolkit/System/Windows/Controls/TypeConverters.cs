// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.TypeConverters
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace System.Windows.Controls
{
  /// <summary>Common TypeConverter functionality.</summary>
  internal static class TypeConverters
  {
    /// <summary>
    /// Determines whether conversion is possible to a specified type.
    /// </summary>
    /// <typeparam name="T">Expected type of the converter.</typeparam>
    /// <param name="destinationType">
    /// Identifies the data type to evaluate for conversion.
    /// </param>
    /// <returns>A value indicating whether conversion is possible.</returns>
    internal static bool CanConvertTo<T>(Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      return (object) destinationType == (object) typeof (string) || destinationType.IsAssignableFrom(typeof (T));
    }

    /// <summary>
    /// Attempts to convert a specified object to an instance of the
    /// desired type.
    /// </summary>
    /// <param name="converter">TypeConverter instance.</param>
    /// <param name="value">The object being converted.</param>
    /// <param name="destinationType">The type to convert the value to.</param>
    /// <returns>The value of the conversion to the specified type.</returns>
    internal static object ConvertTo(TypeConverter converter, object value, Type destinationType)
    {
      Debug.Assert(converter != null, "converter should not be null!");
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value == null && !destinationType.IsValueType)
        return (object) null;
      return value != null && destinationType.IsAssignableFrom(value.GetType()) ? value : throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypeConverters_Convert_CannotConvert, new object[3]
      {
        (object) converter.GetType().Name,
        value != null ? (object) value.GetType().FullName : (object) "(null)",
        (object) destinationType.GetType().Name
      }));
    }
  }
}
