// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.TemplatedVisualTreeExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Media;

namespace System.Windows.Controls
{
  /// <summary>
  /// A static class providing methods for working with the visual tree using generics.
  /// </summary>
  public static class TemplatedVisualTreeExtensions
  {
    /// <summary>
    /// Retrieves the first logical child of a specified type using a
    /// breadth-first search.  A visual element is assumed to be a logical
    /// child of another visual element if they are in the same namescope.
    /// For performance reasons this method manually manages the queue
    /// instead of using recursion.
    /// </summary>
    /// <param name="parent">The parent framework element.</param>
    /// <param name="applyTemplates">Specifies whether to apply templates on the traversed framework elements</param>
    /// <returns>The first logical child of the framework element of the specified type.</returns>
    internal static T GetFirstLogicalChildByType<T>(
      this FrameworkElement parent,
      bool applyTemplates)
      where T : FrameworkElement
    {
      Debug.Assert(parent != null, "The parent cannot be null.");
      Queue<FrameworkElement> frameworkElementQueue = new Queue<FrameworkElement>();
      frameworkElementQueue.Enqueue(parent);
      while (frameworkElementQueue.Count > 0)
      {
        FrameworkElement parent1 = frameworkElementQueue.Dequeue();
        Control control = parent1 as Control;
        if (applyTemplates && control != null)
          control.ApplyTemplate();
        if (parent1 is T && parent1 != parent)
          return (T) parent1;
        foreach (FrameworkElement frameworkElement in ((DependencyObject) parent1).GetVisualChildren().OfType<FrameworkElement>())
          frameworkElementQueue.Enqueue(frameworkElement);
      }
      return default (T);
    }

    /// <summary>
    /// Retrieves all the logical children of a specified type using a
    /// breadth-first search.  A visual element is assumed to be a logical
    /// child of another visual element if they are in the same namescope.
    /// For performance reasons this method manually manages the queue
    /// instead of using recursion.
    /// </summary>
    /// <param name="parent">The parent framework element.</param>
    /// <param name="applyTemplates">Specifies whether to apply templates on the traversed framework elements</param>
    /// <returns>The logical children of the framework element of the specified type.</returns>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "File is linked to projects that target previous platforms that require this method.")]
    internal static IEnumerable<T> GetLogicalChildrenByType<T>(
      this FrameworkElement parent,
      bool applyTemplates)
      where T : FrameworkElement
    {
      Debug.Assert(parent != null, "The parent cannot be null.");
      if (applyTemplates && parent is Control)
        ((Control) parent).ApplyTemplate();
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(((DependencyObject) parent).GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        if (applyTemplates && element is Control)
          ((Control) element).ApplyTemplate();
        if (element is T obj)
          yield return obj;
        foreach (FrameworkElement frameworkElement in ((DependencyObject) element).GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
      }
    }

    /// <summary>
    /// The first parent of the framework element of the specified type
    /// that is found while traversing the visual tree upwards.
    /// </summary>
    /// <typeparam name="T">
    /// The element type of the dependency object.
    /// </typeparam>
    /// <param name="element">The framework element.</param>
    /// <returns>
    /// The first parent of the framework element of the specified type.
    /// </returns>
    internal static T GetParentByType<T>(this FrameworkElement element) where T : FrameworkElement
    {
      Debug.Assert(element != null, "The element cannot be null.");
      T obj = default (T);
      for (DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject) element); parent != null; parent = VisualTreeHelper.GetParent(parent))
      {
        if (parent is T parentByType)
          return parentByType;
      }
      return default (T);
    }
  }
}
