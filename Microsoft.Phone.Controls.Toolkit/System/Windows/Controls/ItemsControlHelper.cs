// Decompiled with JetBrains decompiler
// Type: System.Windows.Controls.ItemsControlHelper
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

namespace System.Windows.Controls
{
  /// <summary>
  /// The ItemContainerGenerator provides useful utilities for ItemsControls.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  internal sealed class ItemsControlHelper
  {
    /// <summary>
    /// A Panel that is used as the ItemsHost of the ItemsControl.  This
    /// property will only be valid when the ItemsControl is live in the
    /// tree and has generated containers for some of its items.
    /// </summary>
    private Panel _itemsHost;
    /// <summary>
    /// A ScrollViewer that is used to scroll the items in the ItemsHost.
    /// </summary>
    private ScrollViewer _scrollHost;

    /// <summary>
    /// Gets or sets the ItemsControl being tracked by the
    /// ItemContainerGenerator.
    /// </summary>
    private ItemsControl ItemsControl { get; set; }

    /// <summary>
    /// Gets a Panel that is used as the ItemsHost of the ItemsControl.
    /// This property will only be valid when the ItemsControl is live in
    /// the tree and has generated containers for some of its items.
    /// </summary>
    internal Panel ItemsHost
    {
      get
      {
        if (this._itemsHost == null && this.ItemsControl != null && this.ItemsControl.ItemContainerGenerator != null)
        {
          DependencyObject dependencyObject = this.ItemsControl.ItemContainerGenerator.ContainerFromIndex(0);
          if (dependencyObject != null)
            this._itemsHost = VisualTreeHelper.GetParent(dependencyObject) as Panel;
        }
        return this._itemsHost;
      }
    }

    /// <summary>
    /// Gets a ScrollViewer that is used to scroll the items in the
    /// ItemsHost.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is linked into multiple projects.")]
    internal ScrollViewer ScrollHost
    {
      get
      {
        if (this._scrollHost == null)
        {
          Panel itemsHost = this.ItemsHost;
          if (itemsHost != null)
          {
            for (DependencyObject dependencyObject = (DependencyObject) itemsHost; dependencyObject != this.ItemsControl && dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
            {
              if (dependencyObject is ScrollViewer scrollViewer)
              {
                this._scrollHost = scrollViewer;
                break;
              }
            }
          }
        }
        return this._scrollHost;
      }
    }

    /// <summary>
    /// Initializes a new instance of the ItemContainerGenerator.
    /// </summary>
    /// <param name="control">
    /// The ItemsControl being tracked by the ItemContainerGenerator.
    /// </param>
    internal ItemsControlHelper(ItemsControl control)
    {
      Debug.Assert(control != null, "control cannot be null!");
      this.ItemsControl = control;
    }

    /// <summary>Apply a control template to the ItemsControl.</summary>
    internal void OnApplyTemplate()
    {
      this._itemsHost = (Panel) null;
      this._scrollHost = (ScrollViewer) null;
    }

    /// <summary>
    /// Prepares the specified container to display the specified item.
    /// </summary>
    /// <param name="element">
    /// Container element used to display the specified item.
    /// </param>
    /// <param name="parentItemContainerStyle">
    /// The ItemContainerStyle for the parent ItemsControl.
    /// </param>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is linked into multiple projects.")]
    internal static void PrepareContainerForItemOverride(
      DependencyObject element,
      Style parentItemContainerStyle)
    {
      Control control = element as Control;
      if (parentItemContainerStyle == null || control == null || ((FrameworkElement) control).Style != null)
        return;
      ((DependencyObject) control).SetValue(FrameworkElement.StyleProperty, (object) parentItemContainerStyle);
    }

    /// <summary>
    /// Update the style of any generated items when the ItemContainerStyle
    /// has been changed.
    /// </summary>
    /// <param name="itemContainerStyle">The ItemContainerStyle.</param>
    /// <remarks>
    /// XAML does not support setting a Style multiple times, so we
    /// only attempt to set styles on elements whose style hasn't already
    /// been set.
    /// </remarks>
    internal void UpdateItemContainerStyle(Style itemContainerStyle)
    {
      if (itemContainerStyle == null)
        return;
      Panel itemsHost = this.ItemsHost;
      if (itemsHost == null || itemsHost.Children == null)
        return;
      foreach (UIElement child in (PresentationFrameworkCollection<UIElement>) itemsHost.Children)
      {
        FrameworkElement frameworkElement = child as FrameworkElement;
        if (frameworkElement.Style == null)
          frameworkElement.Style = itemContainerStyle;
      }
    }

    /// <summary>
    /// Scroll the desired element into the ScrollHost's viewport.
    /// </summary>
    /// <param name="element">Element to scroll into view.</param>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "File is linked across multiple projects and this method is used in some but not others.")]
    internal void ScrollIntoView(FrameworkElement element)
    {
      ScrollViewer scrollHost = this.ScrollHost;
      if (scrollHost == null)
        return;
      GeneralTransform visual;
      try
      {
        visual = ((UIElement) element).TransformToVisual((UIElement) scrollHost);
      }
      catch (ArgumentException ex)
      {
        return;
      }
      Rect rect = new Rect(visual.Transform(new Point()), visual.Transform(new Point(element.ActualWidth, element.ActualHeight)));
      double verticalOffset = scrollHost.VerticalOffset;
      double num1 = 0.0;
      double viewportHeight = scrollHost.ViewportHeight;
      double bottom = rect.Bottom;
      if (viewportHeight < bottom)
      {
        num1 = bottom - viewportHeight;
        verticalOffset += num1;
      }
      double top = rect.Top;
      if (top - num1 < 0.0)
        verticalOffset -= num1 - top;
      scrollHost.ScrollToVerticalOffset(verticalOffset);
      double horizontalOffset = scrollHost.HorizontalOffset;
      double num2 = 0.0;
      double viewportWidth = scrollHost.ViewportWidth;
      double right = rect.Right;
      if (viewportWidth < right)
      {
        num2 = right - viewportWidth;
        horizontalOffset += num2;
      }
      double left = rect.Left;
      if (left - num2 < 0.0)
        horizontalOffset -= num2 - left;
      scrollHost.ScrollToHorizontalOffset(horizontalOffset);
    }
  }
}
