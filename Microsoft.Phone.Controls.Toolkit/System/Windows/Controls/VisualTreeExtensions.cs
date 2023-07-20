// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.VisualTreeExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;

namespace System.Windows.Controls
{
  /// <summary>
  /// A static class providing methods for working with the visual tree.
  /// </summary>
  internal static class VisualTreeExtensions
  {
    /// <summary>
    /// Retrieves all the visual children of a framework element.
    /// </summary>
    /// <param name="parent">The parent framework element.</param>
    /// <returns>The visual children of the framework element.</returns>
    internal static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
    {
      Debug.Assert(parent != null, "The parent cannot be null.");
      int childCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int counter = 0; counter < childCount; ++counter)
        yield return VisualTreeHelper.GetChild(parent, counter);
    }

    /// <summary>
    /// Retrieves all the logical children of a framework element using a
    /// breadth-first search.  A visual element is assumed to be a logical
    /// child of another visual element if they are in the same namescope.
    /// For performance reasons this method manually manages the queue
    /// instead of using recursion.
    /// </summary>
    /// <param name="parent">The parent framework element.</param>
    /// <returns>The logical children of the framework element.</returns>
    internal static IEnumerable<FrameworkElement> GetLogicalChildrenBreadthFirst(
      this FrameworkElement parent)
    {
      Debug.Assert(parent != null, "The parent cannot be null.");
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(((DependencyObject) parent).GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        yield return element;
        foreach (FrameworkElement frameworkElement in ((DependencyObject) element).GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
      }
    }
  }
}
