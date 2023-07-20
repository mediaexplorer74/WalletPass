// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TreeHelpers
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>Couple of simple helpers for walking the visual tree.</summary>
  internal static class TreeHelpers
  {
    /// <summary>Gets the ancestors of the element, up to the root.</summary>
    /// <param name="node">The element to start from.</param>
    /// <returns>An enumerator of the ancestors.</returns>
    public static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
    {
      for (FrameworkElement parent = node.GetVisualParent(); parent != null; parent = parent.GetVisualParent())
        yield return parent;
    }

    /// <summary>Gets the visual parent of the element.</summary>
    /// <param name="node">The element to check.</param>
    /// <returns>The visual parent.</returns>
    public static FrameworkElement GetVisualParent(this FrameworkElement node) => VisualTreeHelper.GetParent((DependencyObject) node) as FrameworkElement;
  }
}
