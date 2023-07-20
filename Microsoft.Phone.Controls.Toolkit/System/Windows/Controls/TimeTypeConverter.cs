// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.TimeTypeConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Windows.Controls
{
  /// <summary>Allows time to be set from xaml.</summary>
  /// <QualityBand>Preview</QualityBand>
  /// <remarks>This converter is used by xaml and thus uses the
  /// English formats.</remarks>
  public class TimeTypeConverter : TypeConverter
  {
    /// <summary>BackingField for the TimeFormats being used.</summary>
    private static readonly string[] _timeFormats = new string[6]
    {
      "h:mm tt",
      "h:mm:ss tt",
      "HH:mm",
      "HH:mm:ss",
      "H:mm",
      "H:mm:ss"
    };
    /// <summary>BackingField for the DateFormats being used.</summary>
    private static readonly string[] _dateFormats = new string[1]
    {
      "M/d/yyyy"
    };

    /// <summary>
    /// Determines whether this instance can convert from
    /// the specified type descriptor context.
    /// </summary>
    /// <param name="typeDescriptorContext">The type descriptor context.</param>
    /// <param name="sourceType">Type of the source.</param>
    /// <returns>
    /// 	<c>True</c> if this instance can convert from the specified type
    /// descriptor context; otherwise, <c>false</c>.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override bool CanConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      Type sourceType)
    {
      return Type.GetTypeCode(sourceType) == TypeCode.String;
    }

    /// <summary>
    /// Determines whether this instance can convert to the specified
    /// type descriptor context.
    /// </summary>
    /// <param name="typeDescriptorContext">The type descriptor context.</param>
    /// <param name="destinationType">Type of the destination.</param>
    /// <returns>
    /// 	<c>True</c> if this instance can convert to the specified type
    /// descriptor context; otherwise, <c>false</c>.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override bool CanConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      Type destinationType)
    {
      return Type.GetTypeCode(destinationType) == TypeCode.String || TypeConverters.CanConvertTo<DateTime?>(destinationType);
    }

    /// <summary>
    /// Converts instances of other data types into instances of DateTime that
    /// represent a time.
    /// </summary>
    /// <param name="typeDescriptorContext">The type descriptor context.</param>
    /// <param name="cultureInfo">The culture used to convert. This culture
    /// is not used during conversion, but a specific set of formats is used.</param>
    /// <param name="source">The string being converted to the DateTime.</param>
    /// <returns>A DateTime that is the value of the conversion.</returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "1#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "2#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override object ConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object source)
    {
      if (source == null)
        return (object) null;
      if (!(source is string s))
        throw new InvalidCastException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypeConverters_Convert_CannotConvert, new object[3]
        {
          (object) this.GetType().Name,
          source,
          (object) typeof (DateTime).Name
        }));
      if (string.IsNullOrEmpty(s))
        return (object) null;
      DateTime result;
      foreach (string timeFormat in TimeTypeConverter._timeFormats)
      {
        if (DateTime.TryParseExact(s, timeFormat, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out result))
        {
          DateTime dateTime = DateTime.Now;
          dateTime = dateTime.Date;
          return (object) dateTime.Add(result.TimeOfDay);
        }
      }
      foreach (string dateFormat in TimeTypeConverter._dateFormats)
      {
        foreach (string timeFormat in TimeTypeConverter._timeFormats)
        {
          if (DateTime.TryParseExact(s, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
          {
            (object) dateFormat,
            (object) timeFormat
          }), (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            return (object) result;
        }
      }
      foreach (string dateFormat in TimeTypeConverter._dateFormats)
      {
        if (DateTime.TryParseExact(s, dateFormat, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.NoCurrentDateDefault, out result))
          return (object) result;
      }
      throw new FormatException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypeConverters_Convert_CannotConvert, new object[3]
      {
        (object) this.GetType().Name,
        (object) s,
        (object) typeof (DateTime).Name
      }));
    }

    /// <summary>Converts a DateTime into a string.</summary>
    /// <param name="typeDescriptorContext">The type descriptor context.</param>
    /// <param name="cultureInfo">The culture used to convert.</param>
    /// <param name="value">
    /// The value that is being converted to a specified type.
    /// </param>
    /// <param name="destinationType">The type to convert the value to.</param>
    /// <returns>The value of the conversion to the specified type.</returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "1#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override object ConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
      {
        if (value == null)
          return (object) string.Empty;
        if (value is DateTime dateTime)
          return (object) dateTime.ToString("HH:mm:ss", (IFormatProvider) new CultureInfo("en-US"));
      }
      return TypeConverters.ConvertTo((TypeConverter) this, value, destinationType);
    }
  }
}
