// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Binding.PreventScrollBinding
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls.Binding
{
  public class PreventScrollBinding
  {
    private static FrameworkElement _internalPanningControl;
    private static readonly DependencyProperty IsScrollSuspendedProperty = DependencyProperty.RegisterAttached("IsScrollSuspended", typeof (bool), typeof (PreventScrollBinding), new PropertyMetadata((object) false));
    private static readonly DependencyProperty LastTouchPointProperty = DependencyProperty.RegisterAttached("LastTouchPoint", typeof (Point), typeof (PreventScrollBinding), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty IsEnabled = DependencyProperty.RegisterAttached(nameof (IsEnabled), typeof (bool), typeof (PreventScrollBinding), new PropertyMetadata((object) false, new PropertyChangedCallback(PreventScrollBinding.IsEnabledDependencyPropertyChangedCallback)));

    public static bool GetIsEnabled(DependencyObject obj) => (bool) obj.GetValue(PreventScrollBinding.IsEnabled);

    public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(PreventScrollBinding.IsEnabled, (object) value);

    private static void IsEnabledDependencyPropertyChangedCallback(
      DependencyObject dobj,
      DependencyPropertyChangedEventArgs ea)
    {
      if (!(dobj is FrameworkElement frameworkElement))
        return;
      frameworkElement.UseOptimizedManipulationRouting = false;
      frameworkElement.Unloaded += new RoutedEventHandler(PreventScrollBinding.BlockingElementUnloaded);
      ((UIElement) frameworkElement).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(PreventScrollBinding.SuspendScroll);
      ((UIElement) frameworkElement).MouseLeftButtonDown += new MouseButtonEventHandler(PreventScrollBinding.SuspendScroll);
    }

    private static void BlockingElementUnloaded(object sender, RoutedEventArgs e)
    {
      if (!(sender is FrameworkElement frameworkElement))
        return;
      frameworkElement.Unloaded -= new RoutedEventHandler(PreventScrollBinding.BlockingElementUnloaded);
      ((UIElement) frameworkElement).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(PreventScrollBinding.SuspendScroll);
      ((UIElement) frameworkElement).MouseLeftButtonDown -= new MouseButtonEventHandler(PreventScrollBinding.SuspendScroll);
    }

    private static void SuspendScroll(object sender, RoutedEventArgs e)
    {
      FrameworkElement blockingElement = sender as FrameworkElement;
      if (PreventScrollBinding._internalPanningControl == null)
        PreventScrollBinding._internalPanningControl = PreventScrollBinding.FindAncestor((DependencyObject) blockingElement, (Func<DependencyObject, bool>) (p => p is Pivot || p is Panorama)) as FrameworkElement;
      if (PreventScrollBinding._internalPanningControl != null && (bool) ((DependencyObject) PreventScrollBinding._internalPanningControl).GetValue(PreventScrollBinding.IsScrollSuspendedProperty) || PreventScrollBinding.FindAncestor(e.OriginalSource as DependencyObject, (Func<DependencyObject, bool>) (dobj => dobj == blockingElement)) != blockingElement)
        return;
      if (PreventScrollBinding._internalPanningControl != null)
        ((DependencyObject) PreventScrollBinding._internalPanningControl).SetValue(PreventScrollBinding.IsScrollSuspendedProperty, (object) true);
      Touch.FrameReported += new TouchFrameEventHandler(PreventScrollBinding.TouchFrameReported);
      if (blockingElement != null)
        ((UIElement) blockingElement).IsHitTestVisible = true;
      if (PreventScrollBinding._internalPanningControl == null)
        return;
      ((UIElement) PreventScrollBinding._internalPanningControl).IsHitTestVisible = false;
    }

    private static void TouchFrameReported(object sender, TouchFrameEventArgs e)
    {
      if (PreventScrollBinding._internalPanningControl == null)
        return;
      TouchPoint touchPoint = ((DependencyObject) PreventScrollBinding._internalPanningControl).GetValue(PreventScrollBinding.LastTouchPointProperty) as TouchPoint;
      bool flag = (bool) ((DependencyObject) PreventScrollBinding._internalPanningControl).GetValue(PreventScrollBinding.IsScrollSuspendedProperty);
      TouchPointCollection touchPoints = e.GetTouchPoints((UIElement) ApplicationSpace.RootFrame);
      if (touchPoint == null || touchPoint != ((IEnumerable<TouchPoint>) touchPoints).Last<TouchPoint>())
        touchPoint = ((IEnumerable<TouchPoint>) touchPoints).Last<TouchPoint>();
      if (!flag || touchPoint == null || touchPoint.Action != 3)
        return;
      Touch.FrameReported -= new TouchFrameEventHandler(PreventScrollBinding.TouchFrameReported);
      ((UIElement) PreventScrollBinding._internalPanningControl).IsHitTestVisible = true;
      ((DependencyObject) PreventScrollBinding._internalPanningControl).SetValue(PreventScrollBinding.IsScrollSuspendedProperty, (object) false);
    }

    public static DependencyObject FindAncestor(
      DependencyObject dependencyObject,
      Func<DependencyObject, bool> predicate)
    {
      if (predicate(dependencyObject))
        return dependencyObject;
      DependencyObject dependencyObject1 = (DependencyObject) null;
      if (dependencyObject is FrameworkElement frameworkElement)
        dependencyObject1 = frameworkElement.Parent ?? VisualTreeHelper.GetParent((DependencyObject) frameworkElement);
      return dependencyObject1 == null ? (DependencyObject) null : PreventScrollBinding.FindAncestor(dependencyObject1, predicate);
    }
  }
}
