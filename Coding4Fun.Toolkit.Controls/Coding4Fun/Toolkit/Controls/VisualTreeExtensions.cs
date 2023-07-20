// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.VisualTreeExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public static class VisualTreeExtensions
  {
    public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
    {
      int childCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int counter = 0; counter < childCount; ++counter)
        yield return VisualTreeHelper.GetChild(parent, counter);
    }

    public static IEnumerable<FrameworkElement> GetLogicalChildrenBreadthFirst(
      this FrameworkElement parent)
    {
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(((DependencyObject) parent).GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        yield return element;
        foreach (FrameworkElement frameworkElement in ((DependencyObject) element).GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
      }
    }

    public static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
    {
      for (FrameworkElement parent = node.GetVisualParent(); parent != null; parent = parent.GetVisualParent())
        yield return parent;
    }

    public static FrameworkElement GetVisualParent(this FrameworkElement node) => VisualTreeHelper.GetParent((DependencyObject) node) as FrameworkElement;
  }
}
