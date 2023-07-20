// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.NumericExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace System.Windows.Controls
{
  /// <summary>
  /// Numeric utility methods used by controls.  These methods are similar in
  /// scope to the WPF DoubleUtil class.
  /// </summary>
  internal static class NumericExtensions
  {
    /// <summary>Check if a number isn't really a number.</summary>
    /// <param name="value">The number to check.</param>
    /// <returns>
    /// True if the number is not a number, false if it is a number.
    /// </returns>
    public static bool IsNaN(this double value)
    {
      NumericExtensions.NanUnion nanUnion = new NumericExtensions.NanUnion()
      {
        FloatingValue = value
      };
      ulong num = nanUnion.IntegerValue & 18442240474082181120UL;
      return (num == 9218868437227405312UL || num == 18442240474082181120UL) && (nanUnion.IntegerValue & 4503599627370495UL) != 0UL;
    }

    /// <summary>Determine if one number is greater than another.</summary>
    /// <param name="left">First number.</param>
    /// <param name="right">Second number.</param>
    /// <returns>
    /// True if the first number is greater than the second, false
    /// otherwise.
    /// </returns>
    public static bool IsGreaterThan(double left, double right) => left > right && !NumericExtensions.AreClose(left, right);

    /// <summary>Determine if two numbers are close in value.</summary>
    /// <param name="left">First number.</param>
    /// <param name="right">Second number.</param>
    /// <returns>
    /// True if the first number is close in value to the second, false
    /// otherwise.
    /// </returns>
    public static bool AreClose(double left, double right)
    {
      if (left == right)
        return true;
      double num1 = (Math.Abs(left) + Math.Abs(right) + 10.0) * 2.2204460492503131E-16;
      double num2 = left - right;
      return -num1 < num2 && num1 > num2;
    }

    /// <summary>
    /// NanUnion is a C++ style type union used for efficiently converting
    /// a double into an unsigned long, whose bits can be easily
    /// manipulated.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    private struct NanUnion
    {
      /// <summary>Floating point representation of the union.</summary>
      [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "It is accessed through the other member of the union")]
      [FieldOffset(0)]
      internal double FloatingValue;
      /// <summary>Integer representation of the union.</summary>
      [FieldOffset(0)]
      internal ulong IntegerValue;
    }
  }
}
