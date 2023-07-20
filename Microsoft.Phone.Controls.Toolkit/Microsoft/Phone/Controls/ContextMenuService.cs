// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ContextMenuService
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides the system implementation for displaying a ContextMenu.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public static class ContextMenuService
  {
    /// <summary>Identifies the ContextMenu attached property.</summary>
    public static readonly DependencyProperty ContextMenuProperty = DependencyProperty.RegisterAttached("ContextMenu", typeof (ContextMenu), typeof (ContextMenuService), new PropertyMetadata((object) null, new PropertyChangedCallback(ContextMenuService.OnContextMenuChanged)));

    /// <summary>
    /// Gets the value of the ContextMenu property of the specified object.
    /// </summary>
    /// <param name="element">Object to query concerning the ContextMenu property.</param>
    /// <returns>Value of the ContextMenu property.</returns>
    public static ContextMenu GetContextMenu(DependencyObject element) => element != null ? (ContextMenu) element.GetValue(ContextMenuService.ContextMenuProperty) : throw new ArgumentNullException(nameof (element));

    /// <summary>
    /// Sets the value of the ContextMenu property of the specified object.
    /// </summary>
    /// <param name="element">Object to set the property on.</param>
    /// <param name="value">Value to set.</param>
    public static void SetContextMenu(DependencyObject element, ContextMenu value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      element.SetValue(ContextMenuService.ContextMenuProperty, (object) value);
    }

    /// <summary>
    /// Handles changes to the ContextMenu DependencyProperty.
    /// </summary>
    /// <param name="o">DependencyObject that changed.</param>
    /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
    private static void OnContextMenuChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement frameworkElement = o as FrameworkElement;
      if (null == frameworkElement)
        return;
      ContextMenu oldValue = e.OldValue as ContextMenu;
      if (null != oldValue)
        oldValue.Owner = (DependencyObject) null;
      ContextMenu newValue = e.NewValue as ContextMenu;
      if (null != newValue)
        newValue.Owner = (DependencyObject) frameworkElement;
    }
  }
}
