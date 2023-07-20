// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TransitionService
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;

namespace Microsoft.Phone.Controls
{
  internal static class TransitionService
  {
    public static readonly DependencyProperty NavigationInTransitionProperty = DependencyProperty.RegisterAttached("NavigationInTransition", typeof (NavigationInTransition), typeof (TransitionService), (PropertyMetadata) null);
    public static readonly DependencyProperty NavigationOutTransitionProperty = DependencyProperty.RegisterAttached("NavigationOutTransition", typeof (NavigationOutTransition), typeof (TransitionService), (PropertyMetadata) null);

    public static NavigationInTransition GetNavigationInTransition(UIElement element) => element != null ? (NavigationInTransition) ((DependencyObject) element).GetValue(TransitionService.NavigationInTransitionProperty) : throw new ArgumentNullException(nameof (element));

    public static NavigationOutTransition GetNavigationOutTransition(UIElement element) => element != null ? (NavigationOutTransition) ((DependencyObject) element).GetValue(TransitionService.NavigationOutTransitionProperty) : throw new ArgumentNullException(nameof (element));

    public static void SetNavigationInTransition(UIElement element, NavigationInTransition value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      ((DependencyObject) element).SetValue(TransitionService.NavigationInTransitionProperty, (object) value);
    }

    public static void SetNavigationOutTransition(UIElement element, NavigationOutTransition value)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      ((DependencyObject) element).SetValue(TransitionService.NavigationOutTransitionProperty, (object) value);
    }
  }
}
