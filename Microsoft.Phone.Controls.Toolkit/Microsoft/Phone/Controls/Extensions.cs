// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Extensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// This set of internal extension methods provide general solutions and
  /// utilities in a small enough number to not warrant a dedicated extension
  /// methods class.
  /// </summary>
  internal static class Extensions
  {
    private const string ExternalAddress = "app://external/";

    /// <summary>
    /// Inverts a Matrix. The Invert functionality on the Matrix type is
    /// internal to the framework only. Since Matrix is a struct, an out
    /// parameter must be presented.
    /// </summary>
    /// <param name="m">The Matrix object.</param>
    /// <param name="outputMatrix">The matrix to return by an output
    /// parameter.</param>
    /// <returns>Returns a value indicating whether the type was
    /// successfully inverted. If the determinant is 0.0, then it cannot
    /// be inverted and the original instance will remain untouched.</returns>
    public static bool Invert(this Matrix m, out Matrix outputMatrix)
    {
      double num = m.M11 * m.M22 - m.M12 * m.M21;
      if (num == 0.0)
      {
        outputMatrix = m;
        return false;
      }
      Matrix matrix = m;
      m.M11 = matrix.M22 / num;
      m.M12 = -1.0 * matrix.M12 / num;
      m.M21 = -1.0 * matrix.M21 / num;
      m.M22 = matrix.M11 / num;
      m.OffsetX = (matrix.OffsetY * matrix.M21 - matrix.OffsetX * matrix.M22) / num;
      m.OffsetY = (matrix.OffsetX * matrix.M12 - matrix.OffsetY * matrix.M11) / num;
      outputMatrix = m;
      return true;
    }

    /// <summary>
    /// An implementation of the Contains member of string that takes in a
    /// string comparison. The traditional .NET string Contains member uses
    /// StringComparison.Ordinal.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <param name="value">The string value to search for.</param>
    /// <param name="comparison">The string comparison type.</param>
    /// <returns>Returns true when the substring is found.</returns>
    public static bool Contains(this string s, string value, StringComparison comparison) => s.IndexOf(value, comparison) >= 0;

    /// <summary>Returns whether the page orientation is in portrait.</summary>
    /// <param name="orientation">Page orientation</param>
    /// <returns>If the orientation is portrait</returns>
    public static bool IsPortrait(this PageOrientation orientation) => 1 == (1 & orientation);

    /// <summary>
    /// Returns whether the dark visual theme is currently active.
    /// </summary>
    /// <param name="resources">Resource Dictionary</param>
    public static bool IsDarkThemeActive(this ResourceDictionary resources) => (Visibility) resources[(object) "PhoneDarkThemeVisibility"] == 0;

    /// <summary>Returns whether the uri is from an external source.</summary>
    /// <param name="uri">The uri</param>
    public static bool IsExternalNavigation(this Uri uri) => "app://external/" == uri.ToString();

    /// <summary>
    /// Registers a property changed callback for a given property.
    /// </summary>
    /// <param name="element">The element registering the notification</param>
    /// <param name="propertyName">Property name to register</param>
    /// <param name="callback">Callback function</param>
    /// <remarks>This allows a child to be notified of when a property declared in its parent is changed.</remarks>
    public static void RegisterNotification(
      this FrameworkElement element,
      string propertyName,
      PropertyChangedCallback callback)
    {
      DependencyProperty dependencyProperty = DependencyProperty.RegisterAttached("Notification" + propertyName, typeof (object), typeof (FrameworkElement), new PropertyMetadata(callback));
      element.SetBinding(dependencyProperty, new Binding(propertyName)
      {
        Source = (object) element
      });
    }
  }
}
