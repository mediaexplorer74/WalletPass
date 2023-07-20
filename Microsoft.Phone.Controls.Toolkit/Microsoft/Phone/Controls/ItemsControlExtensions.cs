// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ItemsControlExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Microsoft.Phone.Controls
{
  /// <summary>Provides helper methods to work with ItemsControl.</summary>
  internal static class ItemsControlExtensions
  {
    /// <summary>Gets the parent ItemsControl.</summary>
    /// <typeparam name="T">The type of ItemsControl.</typeparam>
    /// <param name="element">The dependency object </param>
    /// <returns>The parent ItemsControl or null if there is not.</returns>
    public static T GetParentItemsControl<T>(DependencyObject element) where T : ItemsControl
    {
      DependencyObject parent = VisualTreeHelper.GetParent(element);
      while (!(parent is T) && parent != null)
        parent = VisualTreeHelper.GetParent(parent);
      return (T) parent;
    }

    /// <summary>
    /// Gets the items that are currently in the view port
    /// of an ItemsControl with a ScrollViewer.
    /// </summary>
    /// <param name="list">The ItemsControl to search on.</param>
    /// <returns>
    /// A list of weak references to the items in the view port.
    /// </returns>
    public static IList<WeakReference> GetItemsInViewPort(ItemsControl list)
    {
      IList<WeakReference> items = (IList<WeakReference>) new List<WeakReference>();
      ItemsControlExtensions.GetItemsInViewPort(list, items);
      return items;
    }

    /// <summary>
    /// Gets the items that are currently in the view port
    /// of an ItemsControl with a ScrollViewer and adds them
    /// into a list of weak references.
    /// </summary>
    /// <param name="list">The items control to search on.</param>
    /// <param name="items">
    /// The list of weak references where the items in
    /// the view port will be added.
    /// </param>
    public static void GetItemsInViewPort(ItemsControl list, IList<WeakReference> items)
    {
      if (VisualTreeHelper.GetChildrenCount((DependencyObject) list) == 0)
        return;
      ScrollViewer child = VisualTreeHelper.GetChild((DependencyObject) list, 0) as ScrollViewer;
      ((UIElement) list).UpdateLayout();
      if (child == null)
        return;
      int num;
      GeneralTransform generalTransform;
      Rect rect;
      for (num = 0; num < ((PresentationFrameworkCollection<object>) list.Items).Count; ++num)
      {
        FrameworkElement target = (FrameworkElement) list.ItemContainerGenerator.ContainerFromIndex(num);
        if (target != null)
        {
          generalTransform = (GeneralTransform) null;
          GeneralTransform visual;
          try
          {
            visual = ((UIElement) target).TransformToVisual((UIElement) child);
          }
          catch (ArgumentException ex)
          {
            return;
          }
          rect = new Rect(visual.Transform(new Point()), visual.Transform(new Point(target.ActualWidth, target.ActualHeight)));
          if (rect.Bottom > 0.0)
          {
            items.Add(new WeakReference((object) target));
            ++num;
            break;
          }
        }
      }
      for (; num < ((PresentationFrameworkCollection<object>) list.Items).Count; ++num)
      {
        FrameworkElement target = (FrameworkElement) list.ItemContainerGenerator.ContainerFromIndex(num);
        generalTransform = (GeneralTransform) null;
        GeneralTransform visual;
        try
        {
          visual = ((UIElement) target).TransformToVisual((UIElement) child);
        }
        catch (ArgumentException ex)
        {
          break;
        }
        rect = new Rect(visual.Transform(new Point()), visual.Transform(new Point(target.ActualWidth, target.ActualHeight)));
        if (rect.Top >= ((FrameworkElement) child).ActualHeight)
          break;
        items.Add(new WeakReference((object) target));
      }
    }
  }
}
