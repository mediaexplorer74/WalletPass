// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.LengthConverter
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Controls;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Converts instances of other types to and from instances of a double that
  /// represent an object measurement such as a height or width.
  /// </summary>
  /// <QualityBand>Stable</QualityBand>
  public class LengthConverter : TypeConverter
  {
    /// <summary>Conversions from units to pixels.</summary>
    private static Dictionary<string, double> UnitToPixelConversions = new Dictionary<string, double>()
    {
      {
        "px",
        1.0
      },
      {
        "in",
        96.0
      },
      {
        "cm",
        4800.0 / (double) sbyte.MaxValue
      },
      {
        "pt",
        4.0 / 3.0
      }
    };

    /// <summary>
    /// Determines whether conversion is possible from a specified type to a
    /// <see cref="T:System.Double" /> that represents an object
    /// measurement.
    /// </summary>
    /// <param name="typeDescriptorContext">
    /// An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />
    /// that provides a format context.
    /// </param>
    /// <param name="sourceType">
    /// A <see cref="T:System.Type" /> that represents the type you want to
    /// convert from.
    /// </param>
    /// <returns>
    /// True if this converter can perform the conversion; otherwise, false.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override bool CanConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      Type sourceType)
    {
      switch (Type.GetTypeCode(sourceType))
      {
        case TypeCode.Int16:
        case TypeCode.UInt16:
        case TypeCode.Int32:
        case TypeCode.UInt32:
        case TypeCode.Int64:
        case TypeCode.UInt64:
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Decimal:
        case TypeCode.String:
          return true;
        default:
          return false;
      }
    }

    /// <summary>
    /// Converts from the specified value to values of the
    /// <see cref="T:System.Double" /> type.
    /// </summary>
    /// <param name="typeDescriptorContext">
    /// An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />
    /// that provides a format context.
    /// </param>
    /// <param name="cultureInfo">
    /// The <see cref="T:System.Globalization.CultureInfo" /> to use as the
    /// current culture.
    /// </param>
    /// <param name="source">The value to convert.</param>
    /// <returns>The converted value.</returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "1#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "2#", Justification = "Compat with WPF.")]
    public override object ConvertFrom(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object source)
    {
      if (source == null)
        throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypeConverters_ConvertFrom_CannotConvertFromType, new object[2]
        {
          (object) this.GetType().Name,
          (object) "null"
        }));
      if (!(source is string strA))
        return (object) Convert.ToDouble(source, (IFormatProvider) cultureInfo);
      if (string.Compare(strA, "Auto", StringComparison.OrdinalIgnoreCase) == 0)
        return (object) double.NaN;
      string str = strA;
      double num = 1.0;
      foreach (KeyValuePair<string, double> toPixelConversion in LengthConverter.UnitToPixelConversions)
      {
        if (str.EndsWith(toPixelConversion.Key, StringComparison.Ordinal))
        {
          num = toPixelConversion.Value;
          str = strA.Substring(0, str.Length - toPixelConversion.Key.Length);
          break;
        }
      }
      try
      {
        return (object) (num * Convert.ToDouble(str, (IFormatProvider) cultureInfo));
      }
      catch (FormatException ex)
      {
        throw new FormatException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Resources.TypeConverters_Convert_CannotConvert, new object[3]
        {
          (object) this.GetType().Name,
          (object) strA,
          (object) typeof (double).Name
        }));
      }
    }

    /// <summary>
    /// Returns whether the type converter can convert a measurement to the
    /// specified type.
    /// </summary>
    /// <param name="typeDescriptorContext">
    /// An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />
    /// that provides a format context.
    /// </param>
    /// <param name="destinationType">
    /// A <see cref="T:System.Type" /> that represents the type you want to
    /// convert to.
    /// </param>
    /// <returns>
    /// True if this converter can perform the conversion; otherwise, false.
    /// </returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    public override bool CanConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      Type destinationType)
    {
      return TypeConverters.CanConvertTo<double>(destinationType);
    }

    /// <summary>
    /// Converts the specified measurement to the specified type.
    /// </summary>
    /// <param name="typeDescriptorContext">
    /// An object that provides a format context.
    /// </param>
    /// <param name="cultureInfo">
    /// The <see cref="T:System.Globalization.CultureInfo" /> to use as the
    /// current culture.
    /// </param>
    /// <param name="value">The value to convert.</param>
    /// <param name="destinationType">
    /// A <see cref="T:System.Type" /> that represents the type you want to
    /// convert to.
    /// </param>
    /// <returns>The converted value.</returns>
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "0#", Justification = "Compat with WPF.")]
    [SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId = "1#", Justification = "Compat with WPF.")]
    public override object ConvertTo(
      ITypeDescriptorContext typeDescriptorContext,
      CultureInfo cultureInfo,
      object value,
      Type destinationType)
    {
      return value is double num && (object) destinationType == (object) typeof (string) ? (num.IsNaN() ? (object) "Auto" : (object) Convert.ToString(num, (IFormatProvider) cultureInfo)) : TypeConverters.ConvertTo((TypeConverter) this, value, destinationType);
    }
  }
}
