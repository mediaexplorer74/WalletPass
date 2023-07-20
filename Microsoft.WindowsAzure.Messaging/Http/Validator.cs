// Microsoft.WindowsAzure.Messaging.Http.Validator

using System;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal static class Validator
  {
    internal static void ArgumentIsNotNull(string argumentName, object value)
    {
      if (value == null)
        throw new ArgumentNullException(argumentName);
    }

    internal static void ArgumentIsNotNullOrEmptyString(string argumentName, string value)
    {
      if (value == null)
        throw new ArgumentNullException(argumentName);
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException(
            string.Format((IFormatProvider) CultureInfo.InvariantCulture,
            "ErrorArgumentMustBeNonEmpty", (object) argumentName));
    }

    internal static void ArgumentIsValidPath(string argumentName, string value)
    {
      if (value == null)
        throw new ArgumentNullException(argumentName);
      if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
            "ErrorArgumentMustBeNonEmpty", (object) argumentName));
    }

    internal static void ArgumentIsValidEnumValue<T>(string argumentName, object value) where T : struct
    {
      if (!Enum.IsDefined(typeof (T), value))
        throw new ArgumentOutOfRangeException(argumentName);
    }

    internal static void ArgumentIsNonNegative(string argumentName, int value)
    {
      if (value < 0)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
            "ErrorArgumentMustBeZeroOrPositive", (object) argumentName, (object) value));
    }

    internal static void ArgumentIsPositive(string argumentName, int value)
    {
      if (value <= 0)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.InvariantCulture,
            "ErrorArgumentMustBePositive", (object) argumentName, (object) value));
    }
  }
}
