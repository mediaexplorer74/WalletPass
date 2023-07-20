﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.TemplatedVisualTreeExtensions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public static class TemplatedVisualTreeExtensions
  {
    public static T GetFirstLogicalChildByType<T>(this FrameworkElement parent, bool applyTemplates) where T : FrameworkElement
    {
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

    public static IEnumerable<T> GetLogicalChildrenByType<T>(
      this FrameworkElement parent,
      bool applyTemplates)
      where T : FrameworkElement
    {
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

    public static IEnumerable<T> GetVisualAncestorsByType<T>(
      this FrameworkElement node,
      bool applyTemplates)
      where T : FrameworkElement
    {
      for (FrameworkElement parent = node.GetVisualParent(); parent != null; parent = parent.GetVisualParent())
      {
        if (applyTemplates && parent is Control)
          ((Control) parent).ApplyTemplate();
        if (parent is T)
          yield return parent as T;
      }
    }
  }
}
