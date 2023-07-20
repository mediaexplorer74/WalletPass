// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TurnstileFeatherEffect
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using Microsoft.Phone.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides attached properties to feather FrameworkElements in
  /// and out during page transitions. The result is a 'turnstile feather' effect
  /// added to the select elements.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  public sealed class TurnstileFeatherEffect : DependencyObject
  {
    /// <summary>
    /// The center of rotation on X for elements that are feathered.
    /// </summary>
    private const double FeatheringCenterOfRotationX = -0.2;
    /// <summary>
    /// The duration in milliseconds that each element takes
    /// to feather forward in.
    /// </summary>
    private const double ForwardInFeatheringDuration = 350.0;
    /// <summary>
    /// The initial angle position for an element
    /// that feathers forward in.
    /// </summary>
    private const double ForwardInFeatheringAngle = -80.0;
    /// <summary>
    /// The delay in milliseconds between each element that
    /// feathers forward in.
    /// </summary>
    private const double ForwardInFeatheringDelay = 40.0;
    /// <summary>
    /// The duration in milliseconds that each element takes
    /// to feather forward out.
    /// </summary>
    private const double ForwardOutFeatheringDuration = 250.0;
    /// <summary>
    /// The final angle position for an element
    /// that feathers forward out.
    /// </summary>
    private const double ForwardOutFeatheringAngle = 50.0;
    /// <summary>
    /// The delay in milliseconds between each element that
    /// feathers forward out.
    /// </summary>
    private const double ForwardOutFeatheringDelay = 50.0;
    /// <summary>
    /// The duration in milliseconds that each element takes
    /// to feather backward in.
    /// </summary>
    private const double BackwardInFeatheringDuration = 350.0;
    /// <summary>
    /// The initial angle position for an element
    /// that feathers backward in.
    /// </summary>
    private const double BackwardInFeatheringAngle = 50.0;
    /// <summary>
    /// The delay in milliseconds between each element that
    /// feathers backward in.
    /// </summary>
    private const double BackwardInFeatheringDelay = 50.0;
    /// <summary>
    /// The duration in milliseconds that each element takes
    /// to feather backward out.
    /// </summary>
    private const double BackwardOutFeatheringDuration = 250.0;
    /// <summary>
    /// The initial angle position for an element
    /// that feathers backward out.
    /// </summary>
    private const double BackwardOutFeatheringAngle = -80.0;
    /// <summary>
    /// The delay in milliseconds between each element that
    /// feathers backward out.
    /// </summary>
    private const double BackwardOutFeatheringDelay = 40.0;
    /// <summary>
    /// The easing function that defines the exponential inwards
    /// interpolation of the storyboards.
    /// </summary>
    private static readonly ExponentialEase TurnstileFeatheringExponentialEaseIn;
    /// <summary>
    /// The easing function that defines the exponential outwards
    /// interpolation of the storyboards.
    /// </summary>
    private static readonly ExponentialEase TurnstileFeatheringExponentialEaseOut;
    /// <summary>
    /// The property path used to map the animation's target property
    /// to the RotationY property of the plane projection of a UI element.
    /// </summary>
    private static readonly PropertyPath RotationYPropertyPath;
    /// <summary>
    /// The property path used to map the animation's target property
    /// to the Opacity property of a UI element.
    /// </summary>
    private static readonly PropertyPath OpacityPropertyPath;
    /// <summary>A point with coordinate (0, 0).</summary>
    private static readonly Point Origin;
    /// <summary>
    /// Private manager that represents a correlation between pages
    /// and the indexed elements it contains.
    /// </summary>
    private static Dictionary<PhoneApplicationPage, List<WeakReference>> _pagesToReferences;
    /// <summary>
    /// Identifies the set of framework elements that are targeted
    /// to be feathered.
    /// </summary>
    private static IList<WeakReference> _featheringTargets;
    /// <summary>
    /// Indicates whether the targeted framework elements need their
    /// projections and transforms to be restored.
    /// </summary>
    private static bool _pendingRestore;
    /// <summary>Default list of types that cannot be feathered.</summary>
    private static IList<Type> _nonPermittedTypes;
    /// <summary>
    /// Identifies the feathering index of the current element,
    /// which represents its place in the feathering order sequence.
    /// </summary>
    public static readonly DependencyProperty FeatheringIndexProperty;
    /// <summary>Identifies the ParentPage dependency property.</summary>
    private static readonly DependencyProperty ParentPageProperty;
    /// <summary>Identifies the IsSubscribed dependency property.</summary>
    private static readonly DependencyProperty IsSubscribedProperty;
    /// <summary>Identifies the HasEventsAttached dependency property.</summary>
    private static readonly DependencyProperty HasEventsAttachedProperty;
    /// <summary>
    /// Identifies the OriginalProjection dependency property.
    /// </summary>
    private static readonly DependencyProperty OriginalProjectionProperty;
    /// <summary>
    /// Identifies the OriginalRenderTransform dependency property.
    /// </summary>
    private static readonly DependencyProperty OriginalRenderTransformProperty;
    /// <summary>Identifies the OriginalOpacity dependency property.</summary>
    private static readonly DependencyProperty OriginalOpacityProperty;

    /// <summary>Default list of types that cannot be feathered.</summary>
    public static IList<Type> NonPermittedTypes => TurnstileFeatherEffect._nonPermittedTypes;

    /// <summary>
    /// Gets the feathering index of the specified dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The feathering index.</returns>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static int GetFeatheringIndex(DependencyObject obj) => (int) obj.GetValue(TurnstileFeatherEffect.FeatheringIndexProperty);

    /// <summary>
    /// Sets the feathering index of the specified dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The feathering index.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static void SetFeatheringIndex(DependencyObject obj, int value) => obj.SetValue(TurnstileFeatherEffect.FeatheringIndexProperty, (object) value);

    /// <summary>Subscribes an element to the private manager.</summary>
    /// <param name="obj">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private static void OnFeatheringIndexPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(obj is FrameworkElement target))
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The dependency object must be of the type {0}.", new object[1]
        {
          (object) typeof (FrameworkElement)
        }));
      TurnstileFeatherEffect.CheckForTypePermission((object) target);
      if ((int) e.NewValue < 0)
      {
        if (TurnstileFeatherEffect.GetHasEventsAttached((DependencyObject) target))
        {
          target.SizeChanged -= new SizeChangedEventHandler(TurnstileFeatherEffect.Target_SizeChanged);
          target.Unloaded -= new RoutedEventHandler(TurnstileFeatherEffect.Target_Unloaded);
          TurnstileFeatherEffect.SetHasEventsAttached((DependencyObject) target, false);
        }
        TurnstileFeatherEffect.UnsubscribeFrameworkElement(target);
      }
      else
      {
        if (!TurnstileFeatherEffect.GetHasEventsAttached((DependencyObject) target))
        {
          target.SizeChanged += new SizeChangedEventHandler(TurnstileFeatherEffect.Target_SizeChanged);
          target.Unloaded += new RoutedEventHandler(TurnstileFeatherEffect.Target_Unloaded);
          TurnstileFeatherEffect.SetHasEventsAttached((DependencyObject) target, true);
        }
        TurnstileFeatherEffect.SubscribeFrameworkElement(target);
      }
    }

    /// <summary>
    /// Gets the parent page of the specified dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The page.</returns>
    private static PhoneApplicationPage GetParentPage(DependencyObject obj) => (PhoneApplicationPage) obj.GetValue(TurnstileFeatherEffect.ParentPageProperty);

    /// <summary>
    /// Sets the parent page of the specified dependency object.
    /// </summary>
    /// <param name="obj">The depedency object.</param>
    /// <param name="value">The page.</param>
    private static void SetParentPage(DependencyObject obj, PhoneApplicationPage value) => obj.SetValue(TurnstileFeatherEffect.ParentPageProperty, (object) value);

    /// <summary>Manages subscription to a page.</summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="e">The event arguments.</param>
    private static void OnParentPagePropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement target = (FrameworkElement) obj;
      PhoneApplicationPage oldValue = (PhoneApplicationPage) e.OldValue;
      PhoneApplicationPage newValue = (PhoneApplicationPage) e.NewValue;
      if (newValue != null)
      {
        List<WeakReference> references;
        if (!TurnstileFeatherEffect._pagesToReferences.TryGetValue(newValue, out references))
        {
          references = new List<WeakReference>();
          TurnstileFeatherEffect._pagesToReferences.Add(newValue, references);
        }
        else
          WeakReferenceHelper.RemoveNullTargetReferences((IList<WeakReference>) references);
        if (!WeakReferenceHelper.ContainsTarget((IEnumerable<WeakReference>) references, (object) target))
          references.Add(new WeakReference((object) target));
        references.Sort(new Comparison<WeakReference>(TurnstileFeatherEffect.SortReferencesByIndex));
      }
      else
      {
        List<WeakReference> references;
        if (TurnstileFeatherEffect._pagesToReferences.TryGetValue(oldValue, out references))
        {
          WeakReferenceHelper.TryRemoveTarget((IList<WeakReference>) references, (object) target);
          if (references.Count == 0)
            TurnstileFeatherEffect._pagesToReferences.Remove(oldValue);
        }
      }
    }

    /// <summary>
    /// Gets whether the specified dependency object
    /// is subscribed to the private manager or not.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The value.</returns>
    private static bool GetIsSubscribed(DependencyObject obj) => (bool) obj.GetValue(TurnstileFeatherEffect.IsSubscribedProperty);

    /// <summary>
    /// Sets whether the specified dependency object
    /// is subscribed to the private manager or not.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The value.</param>
    private static void SetIsSubscribed(DependencyObject obj, bool value) => obj.SetValue(TurnstileFeatherEffect.IsSubscribedProperty, (object) value);

    /// <summary>
    /// Gets whether the specified dependency object
    /// has events attached to it or not.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The value.</returns>
    private static bool GetHasEventsAttached(DependencyObject obj) => (bool) obj.GetValue(TurnstileFeatherEffect.HasEventsAttachedProperty);

    /// <summary>
    /// Sets whether the specified dependency object
    /// has events attached to it or not.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The value.</param>
    private static void SetHasEventsAttached(DependencyObject obj, bool value) => obj.SetValue(TurnstileFeatherEffect.HasEventsAttachedProperty, (object) value);

    /// <summary>
    /// Gets the original projection of the specified dependency object
    /// after the projection needed to apply the turnstile feather effect
    /// has been attached to it.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The original projection.</returns>
    private static Projection GetOriginalProjection(DependencyObject obj) => (Projection) obj.GetValue(TurnstileFeatherEffect.OriginalProjectionProperty);

    /// <summary>
    /// Sets the original projection of the specified dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The original projection.</param>
    private static void SetOriginalProjection(DependencyObject obj, Projection value) => obj.SetValue(TurnstileFeatherEffect.OriginalProjectionProperty, (object) value);

    /// <summary>
    /// Gets the original render transform of the specified dependency
    /// object after the transform needed to apply the turnstile feather
    /// effect has been attached to it.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The original render transform.</returns>
    private static Transform GetOriginalRenderTransform(DependencyObject obj) => (Transform) obj.GetValue(TurnstileFeatherEffect.OriginalRenderTransformProperty);

    /// <summary>
    /// Sets the original render transform of the specified
    /// dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The original render transform.</param>
    private static void SetOriginalRenderTransform(DependencyObject obj, Transform value) => obj.SetValue(TurnstileFeatherEffect.OriginalRenderTransformProperty, (object) value);

    /// <summary>
    /// Gets the original opacity of the specified dependency
    /// object before the turnstile feather effect is applied to it.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <returns>The original opacity.</returns>
    private static double GetOriginalOpacity(DependencyObject obj) => (double) obj.GetValue(TurnstileFeatherEffect.OriginalOpacityProperty);

    /// <summary>
    /// Sets the original opacity of the specified
    /// dependency object.
    /// </summary>
    /// <param name="obj">The dependency object.</param>
    /// <param name="value">The original opacity.</param>
    private static void SetOriginalOpacity(DependencyObject obj, double value) => obj.SetValue(TurnstileFeatherEffect.OriginalOpacityProperty, (object) value);

    /// <summary>Called when an element gets resized.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    /// <remarks>
    /// Ideally, the Loaded event should be handled instead of
    /// the SizeChanged event. However, the Loaded event does not occur
    /// by the time the TransitionFrame tries to animate a forward in transition.
    /// Handling the SizeChanged event instead guarantees that
    /// the newly created FrameworkElements can be subscribed in time
    /// before the transition begins.
    /// </remarks>
    private static void Target_SizeChanged(object sender, SizeChangedEventArgs e) => TurnstileFeatherEffect.SubscribeFrameworkElement((FrameworkElement) sender);

    /// <summary>Called when an element gets unloaded.</summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event information.</param>
    private static void Target_Unloaded(object sender, RoutedEventArgs e) => TurnstileFeatherEffect.UnsubscribeFrameworkElement((FrameworkElement) sender);

    /// <summary>
    /// Throws an exception if the object sent as parameter is of a type
    /// that is included in the list of non-permitted types.
    /// </summary>
    /// <param name="obj">The object.</param>
    private static void CheckForTypePermission(object obj)
    {
      Type type = obj.GetType();
      if (TurnstileFeatherEffect.NonPermittedTypes.Contains(type))
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Objects of the type {0} cannot be feathered.", new object[1]
        {
          (object) type
        }));
    }

    /// <summary>
    /// Compares two weak references targeting dependency objects
    /// to sort them based on their feathering index.
    /// </summary>
    /// <param name="x">The first weak reference.</param>
    /// <param name="y">The second weak reference.</param>
    /// <returns>
    /// 0 if both weak references target dependency objects with
    /// the same feathering index.
    /// 1 if the first reference targets a dependency
    /// object with a greater feathering index.
    /// -1 if the second reference targets a dependency
    /// object with a greater feathering index.
    /// </returns>
    private static int SortReferencesByIndex(WeakReference x, WeakReference y)
    {
      DependencyObject target1 = x.Target as DependencyObject;
      DependencyObject target2 = y.Target as DependencyObject;
      return target1 == null ? (target2 == null ? 0 : -1) : (target2 == null ? 1 : TurnstileFeatherEffect.GetFeatheringIndex(target1).CompareTo(TurnstileFeatherEffect.GetFeatheringIndex(target2)));
    }

    /// <summary>
    /// Returns the set of weak references to the items
    /// that must be animated.
    /// </summary>
    /// <returns>
    /// A set of weak references to items sorted by their feathering index.
    /// </returns>
    private static IList<WeakReference> GetTargetsToAnimate()
    {
      List<WeakReference> items = new List<WeakReference>();
      PhoneApplicationPage key = (PhoneApplicationPage) null;
      if (Application.Current.RootVisual is PhoneApplicationFrame rootVisual)
        key = ((ContentControl) rootVisual).Content as PhoneApplicationPage;
      List<WeakReference> weakReferenceList;
      if (key == null || !TurnstileFeatherEffect._pagesToReferences.TryGetValue(key, out weakReferenceList))
        return (IList<WeakReference>) null;
      foreach (WeakReference weakReference in weakReferenceList)
      {
        if (weakReference.Target is FrameworkElement target && TurnstileFeatherEffect.IsOnScreen(target))
        {
          ListBox target1 = weakReference.Target as ListBox;
          LongListSelector target2 = weakReference.Target as LongListSelector;
          Pivot target3 = weakReference.Target as Pivot;
          if (target1 != null)
            ItemsControlExtensions.GetItemsInViewPort((ItemsControl) target1, (IList<WeakReference>) items);
          else if (target2 != null)
          {
            ListBox logicalChildByType = ((FrameworkElement) target2).GetFirstLogicalChildByType<ListBox>(false);
            if (logicalChildByType != null)
              ItemsControlExtensions.GetItemsInViewPort((ItemsControl) logicalChildByType, (IList<WeakReference>) items);
          }
          else if (target3 != null)
          {
            ContentPresenter logicalChildByType1 = ((FrameworkElement) target3).GetFirstLogicalChildByType<ContentPresenter>(false);
            if (logicalChildByType1 != null)
              items.Add(new WeakReference((object) logicalChildByType1));
            PivotHeadersControl logicalChildByType2 = ((FrameworkElement) target3).GetFirstLogicalChildByType<PivotHeadersControl>(false);
            if (logicalChildByType2 != null)
              items.Add(new WeakReference((object) logicalChildByType2));
          }
          else
            items.Add(weakReference);
        }
      }
      return (IList<WeakReference>) items;
    }

    /// <summary>Subscribes an element to the private managers.</summary>
    /// <param name="target">The framework element.</param>
    private static void SubscribeFrameworkElement(FrameworkElement target)
    {
      if (TurnstileFeatherEffect.GetIsSubscribed((DependencyObject) target))
        return;
      PhoneApplicationPage parentByType = target.GetParentByType<PhoneApplicationPage>();
      if (parentByType == null)
        return;
      TurnstileFeatherEffect.SetParentPage((DependencyObject) target, parentByType);
      TurnstileFeatherEffect.SetIsSubscribed((DependencyObject) target, true);
    }

    /// <summary>Unsubscribes an element from the private manager.</summary>
    /// <param name="target">The framework element.</param>
    private static void UnsubscribeFrameworkElement(FrameworkElement target)
    {
      if (!TurnstileFeatherEffect.GetIsSubscribed((DependencyObject) target))
        return;
      TurnstileFeatherEffect.SetParentPage((DependencyObject) target, (PhoneApplicationPage) null);
      TurnstileFeatherEffect.SetIsSubscribed((DependencyObject) target, false);
    }

    /// <summary>
    /// Prepares a framework element to be feathered by adding a plane projection
    /// and a composite transform to it.
    /// </summary>
    /// <param name="root">The root visual.</param>
    /// <param name="element">The framework element.</param>
    private static bool TryAttachProjectionAndTransform(
      PhoneApplicationFrame root,
      FrameworkElement element)
    {
      GeneralTransform visual;
      try
      {
        visual = ((UIElement) element).TransformToVisual((UIElement) root);
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      double num1 = visual.Transform(TurnstileFeatherEffect.Origin).Y + element.ActualHeight / 2.0;
      double num2 = ((FrameworkElement) root).ActualHeight / 2.0 - num1;
      TurnstileFeatherEffect.SetOriginalProjection((DependencyObject) element, ((UIElement) element).Projection);
      TurnstileFeatherEffect.SetOriginalRenderTransform((DependencyObject) element, ((UIElement) element).RenderTransform);
      ((UIElement) element).Projection = (Projection) new PlaneProjection()
      {
        GlobalOffsetY = (num2 * -1.0),
        CenterOfRotationX = -0.2
      };
      Transform renderTransform = ((UIElement) element).RenderTransform;
      TranslateTransform translateTransform = new TranslateTransform();
      translateTransform.Y = num2;
      TransformGroup transformGroup = new TransformGroup();
      ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add(renderTransform);
      ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) translateTransform);
      ((UIElement) element).RenderTransform = (Transform) transformGroup;
      return true;
    }

    /// <summary>
    /// Restores the original projection and render transform of
    /// the targeted framework elements.
    /// </summary>
    private static void RestoreProjectionsAndTransforms()
    {
      if (TurnstileFeatherEffect._featheringTargets == null || !TurnstileFeatherEffect._pendingRestore)
        return;
      foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
      {
        if (featheringTarget.Target is FrameworkElement target)
        {
          Projection originalProjection = TurnstileFeatherEffect.GetOriginalProjection((DependencyObject) target);
          Transform originalRenderTransform = TurnstileFeatherEffect.GetOriginalRenderTransform((DependencyObject) target);
          ((UIElement) target).Projection = originalProjection;
          ((UIElement) target).RenderTransform = originalRenderTransform;
        }
      }
      TurnstileFeatherEffect._pendingRestore = false;
    }

    /// <summary>
    /// Indicates whether the specified framework element
    /// is within the bounds of the application's root visual.
    /// </summary>
    /// <param name="element">The framework element.</param>
    /// <returns>
    /// True if the rectangular bounds of the framework element
    /// are completely outside the bounds of the application's root visual.
    /// </returns>
    private static bool IsOnScreen(FrameworkElement element)
    {
      if (!(Application.Current.RootVisual is PhoneApplicationFrame rootVisual))
        return false;
      double actualHeight = ((FrameworkElement) rootVisual).ActualHeight;
      double actualWidth = ((FrameworkElement) rootVisual).ActualWidth;
      GeneralTransform visual;
      try
      {
        visual = ((UIElement) element).TransformToVisual((UIElement) rootVisual);
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      Rect rect = new Rect(visual.Transform(TurnstileFeatherEffect.Origin), visual.Transform(new Point(element.ActualWidth, element.ActualHeight)));
      bool flag = false;
      IList<FrameworkElement> list = (IList<FrameworkElement>) element.GetVisualAncestors().ToList<FrameworkElement>();
      if (list != null)
      {
        for (int index = 0; index < list.Count; ++index)
        {
          if (((UIElement) list[index]).Opacity <= 0.001)
          {
            flag = true;
            break;
          }
        }
      }
      return rect.Bottom > 0.0 && rect.Top < actualHeight && rect.Right > 0.0 && rect.Left < actualWidth && !flag;
    }

    /// <summary>
    /// Adds a set of animations corresponding to the
    /// turnstile feather forward in effect.
    /// </summary>
    /// <param name="storyboard">
    /// The storyboard where the animations
    /// will be added.
    /// </param>
    private static void ComposeForwardInStoryboard(Storyboard storyboard)
    {
      int num = 0;
      PhoneApplicationFrame rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
      foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
      {
        FrameworkElement target = (FrameworkElement) featheringTarget.Target;
        double opacity = ((UIElement) target).Opacity;
        TurnstileFeatherEffect.SetOriginalOpacity((DependencyObject) target, opacity);
        ((UIElement) target).Opacity = 0.0;
        if (TurnstileFeatherEffect.TryAttachProjectionAndTransform(rootVisual, target))
        {
          DoubleAnimation doubleAnimation1 = new DoubleAnimation();
          ((Timeline) doubleAnimation1).Duration = Duration.op_Implicit(TimeSpan.FromMilliseconds(350.0));
          doubleAnimation1.From = new double?(-80.0);
          doubleAnimation1.To = new double?(0.0);
          ((Timeline) doubleAnimation1).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(40.0 * (double) num));
          doubleAnimation1.EasingFunction = (IEasingFunction) TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseOut;
          DoubleAnimation doubleAnimation2 = doubleAnimation1;
          Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation2, TurnstileFeatherEffect.RotationYPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
          DoubleAnimation doubleAnimation3 = new DoubleAnimation();
          ((Timeline) doubleAnimation3).Duration = Duration.op_Implicit(TimeSpan.Zero);
          doubleAnimation3.From = new double?(0.0);
          doubleAnimation3.To = new double?(TurnstileFeatherEffect.GetOriginalOpacity((DependencyObject) target));
          ((Timeline) doubleAnimation3).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(40.0 * (double) num));
          DoubleAnimation doubleAnimation4 = doubleAnimation3;
          Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation4, TurnstileFeatherEffect.OpacityPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation4);
          ++num;
        }
      }
    }

    /// <summary>
    /// Adds a set of animations corresponding to the
    /// turnstile feather forward out effect.
    /// </summary>
    /// <param name="storyboard">
    /// The storyboard where the animations
    /// will be added.
    /// </param>
    private static void ComposeForwardOutStoryboard(Storyboard storyboard)
    {
      int num = 0;
      PhoneApplicationFrame rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
      foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
      {
        FrameworkElement target = (FrameworkElement) featheringTarget.Target;
        double opacity = ((UIElement) target).Opacity;
        TurnstileFeatherEffect.SetOriginalOpacity((DependencyObject) target, opacity);
        if (TurnstileFeatherEffect.TryAttachProjectionAndTransform(rootVisual, target))
        {
          DoubleAnimation doubleAnimation1 = new DoubleAnimation();
          ((Timeline) doubleAnimation1).Duration = Duration.op_Implicit(TimeSpan.FromMilliseconds(250.0));
          doubleAnimation1.From = new double?(0.0);
          doubleAnimation1.To = new double?(50.0);
          ((Timeline) doubleAnimation1).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(50.0 * (double) num));
          doubleAnimation1.EasingFunction = (IEasingFunction) TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseIn;
          DoubleAnimation doubleAnimation2 = doubleAnimation1;
          Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation2, TurnstileFeatherEffect.RotationYPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
          DoubleAnimation doubleAnimation3 = new DoubleAnimation();
          ((Timeline) doubleAnimation3).Duration = Duration.op_Implicit(TimeSpan.Zero);
          doubleAnimation3.From = new double?(opacity);
          doubleAnimation3.To = new double?(0.0);
          ((Timeline) doubleAnimation3).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(50.0 * (double) num + 250.0));
          DoubleAnimation doubleAnimation4 = doubleAnimation3;
          Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation4, TurnstileFeatherEffect.OpacityPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation4);
          ++num;
        }
      }
    }

    /// <summary>
    /// Adds a set of animations corresponding to the
    /// turnstile feather backward in effect.
    /// </summary>
    /// <param name="storyboard">
    /// The storyboard where the animations
    /// will be added.
    /// </param>
    private static void ComposeBackwardInStoryboard(Storyboard storyboard)
    {
      int num = 0;
      PhoneApplicationFrame rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
      foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
      {
        FrameworkElement target = (FrameworkElement) featheringTarget.Target;
        double opacity = ((UIElement) target).Opacity;
        TurnstileFeatherEffect.SetOriginalOpacity((DependencyObject) target, opacity);
        ((UIElement) target).Opacity = 0.0;
        if (TurnstileFeatherEffect.TryAttachProjectionAndTransform(rootVisual, target))
        {
          DoubleAnimation doubleAnimation1 = new DoubleAnimation();
          ((Timeline) doubleAnimation1).Duration = Duration.op_Implicit(TimeSpan.FromMilliseconds(350.0));
          doubleAnimation1.From = new double?(50.0);
          doubleAnimation1.To = new double?(0.0);
          ((Timeline) doubleAnimation1).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(50.0 * (double) num));
          doubleAnimation1.EasingFunction = (IEasingFunction) TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseOut;
          DoubleAnimation doubleAnimation2 = doubleAnimation1;
          Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation2, TurnstileFeatherEffect.RotationYPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
          DoubleAnimation doubleAnimation3 = new DoubleAnimation();
          ((Timeline) doubleAnimation3).Duration = Duration.op_Implicit(TimeSpan.Zero);
          doubleAnimation3.From = new double?(0.0);
          doubleAnimation3.To = new double?(opacity);
          ((Timeline) doubleAnimation3).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(50.0 * (double) num));
          DoubleAnimation doubleAnimation4 = doubleAnimation3;
          Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation4, TurnstileFeatherEffect.OpacityPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation4);
          ++num;
        }
      }
    }

    /// <summary>
    /// Adds a set of animations corresponding to the
    /// turnstile feather backward out effect.
    /// </summary>
    /// <param name="storyboard">
    /// The storyboard where the animations
    /// will be added.
    /// </param>
    private static void ComposeBackwardOutStoryboard(Storyboard storyboard)
    {
      int num = 0;
      PhoneApplicationFrame rootVisual = Application.Current.RootVisual as PhoneApplicationFrame;
      foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
      {
        FrameworkElement target = (FrameworkElement) featheringTarget.Target;
        double opacity = ((UIElement) target).Opacity;
        TurnstileFeatherEffect.SetOriginalOpacity((DependencyObject) target, opacity);
        if (TurnstileFeatherEffect.TryAttachProjectionAndTransform(rootVisual, target))
        {
          DoubleAnimation doubleAnimation1 = new DoubleAnimation();
          ((Timeline) doubleAnimation1).Duration = Duration.op_Implicit(TimeSpan.FromMilliseconds(250.0));
          doubleAnimation1.From = new double?(0.0);
          doubleAnimation1.To = new double?(-80.0);
          ((Timeline) doubleAnimation1).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(40.0 * (double) num));
          doubleAnimation1.EasingFunction = (IEasingFunction) TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseIn;
          DoubleAnimation doubleAnimation2 = doubleAnimation1;
          Storyboard.SetTarget((Timeline) doubleAnimation2, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation2, TurnstileFeatherEffect.RotationYPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation2);
          DoubleAnimation doubleAnimation3 = new DoubleAnimation();
          ((Timeline) doubleAnimation3).Duration = Duration.op_Implicit(TimeSpan.Zero);
          doubleAnimation3.From = new double?(opacity);
          doubleAnimation3.To = new double?(0.0);
          ((Timeline) doubleAnimation3).BeginTime = new TimeSpan?(TimeSpan.FromMilliseconds(40.0 * (double) num + 250.0));
          DoubleAnimation doubleAnimation4 = doubleAnimation3;
          Storyboard.SetTarget((Timeline) doubleAnimation4, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) doubleAnimation4, TurnstileFeatherEffect.OpacityPropertyPath);
          ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) doubleAnimation4);
          ++num;
        }
      }
    }

    /// <summary>
    /// Adds a set of animations corresponding to the
    /// turnstile feather effect.
    /// </summary>
    /// <param name="storyboard">
    /// The storyboard where the animations
    /// will be added.</param>
    /// <param name="beginTime">The time at which the storyboard should begin.</param>
    /// <param name="mode">The mode of the turnstile feather effect.</param>
    internal static void ComposeStoryboard(
      Storyboard storyboard,
      TimeSpan? beginTime,
      TurnstileFeatherTransitionMode mode)
    {
      TurnstileFeatherEffect.RestoreProjectionsAndTransforms();
      TurnstileFeatherEffect._featheringTargets = TurnstileFeatherEffect.GetTargetsToAnimate();
      if (TurnstileFeatherEffect._featheringTargets == null)
        return;
      TurnstileFeatherEffect._pendingRestore = true;
      switch (mode)
      {
        case TurnstileFeatherTransitionMode.ForwardIn:
          TurnstileFeatherEffect.ComposeForwardInStoryboard(storyboard);
          break;
        case TurnstileFeatherTransitionMode.ForwardOut:
          TurnstileFeatherEffect.ComposeForwardOutStoryboard(storyboard);
          break;
        case TurnstileFeatherTransitionMode.BackwardIn:
          TurnstileFeatherEffect.ComposeBackwardInStoryboard(storyboard);
          break;
        case TurnstileFeatherTransitionMode.BackwardOut:
          TurnstileFeatherEffect.ComposeBackwardOutStoryboard(storyboard);
          break;
      }
      ((Timeline) storyboard).BeginTime = beginTime;
      ((Timeline) storyboard).Completed += (EventHandler) ((s, e) =>
      {
        foreach (WeakReference featheringTarget in (IEnumerable<WeakReference>) TurnstileFeatherEffect._featheringTargets)
        {
          FrameworkElement target = (FrameworkElement) featheringTarget.Target;
          double originalOpacity = TurnstileFeatherEffect.GetOriginalOpacity((DependencyObject) target);
          ((UIElement) target).Opacity = originalOpacity;
        }
        TurnstileFeatherEffect.RestoreProjectionsAndTransforms();
      });
    }

    static TurnstileFeatherEffect()
    {
      ExponentialEase exponentialEase1 = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase1).EasingMode = (EasingMode) 1;
      exponentialEase1.Exponent = 6.0;
      TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseIn = exponentialEase1;
      ExponentialEase exponentialEase2 = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase2).EasingMode = (EasingMode) 0;
      exponentialEase2.Exponent = 6.0;
      TurnstileFeatherEffect.TurnstileFeatheringExponentialEaseOut = exponentialEase2;
      TurnstileFeatherEffect.RotationYPropertyPath = new PropertyPath("(UIElement.Projection).(PlaneProjection.RotationY)", new object[0]);
      TurnstileFeatherEffect.OpacityPropertyPath = new PropertyPath("(UIElement.Opacity)", new object[0]);
      TurnstileFeatherEffect.Origin = new Point(0.0, 0.0);
      TurnstileFeatherEffect._pagesToReferences = new Dictionary<PhoneApplicationPage, List<WeakReference>>();
      TurnstileFeatherEffect._nonPermittedTypes = (IList<Type>) new List<Type>()
      {
        typeof (PhoneApplicationFrame),
        typeof (PhoneApplicationPage),
        typeof (PivotItem),
        typeof (Panorama),
        typeof (PanoramaItem)
      };
      TurnstileFeatherEffect.FeatheringIndexProperty = DependencyProperty.RegisterAttached("FeatheringIndex", typeof (int), typeof (TurnstileFeatherEffect), new PropertyMetadata((object) -1, new PropertyChangedCallback(TurnstileFeatherEffect.OnFeatheringIndexPropertyChanged)));
      TurnstileFeatherEffect.ParentPageProperty = DependencyProperty.RegisterAttached("ParentPage", typeof (PhoneApplicationPage), typeof (TurnstileFeatherEffect), new PropertyMetadata((object) null, new PropertyChangedCallback(TurnstileFeatherEffect.OnParentPagePropertyChanged)));
      TurnstileFeatherEffect.IsSubscribedProperty = DependencyProperty.RegisterAttached("IsSubscribed", typeof (bool), typeof (TurnstileFeatherEffect), new PropertyMetadata((object) false));
      TurnstileFeatherEffect.HasEventsAttachedProperty = DependencyProperty.RegisterAttached("HasEventsAttached", typeof (bool), typeof (TurnstileFeatherEffect), new PropertyMetadata((object) false));
      TurnstileFeatherEffect.OriginalProjectionProperty = DependencyProperty.RegisterAttached("OriginalProjection", typeof (Projection), typeof (TurnstileFeatherEffect), new PropertyMetadata((PropertyChangedCallback) null));
      TurnstileFeatherEffect.OriginalRenderTransformProperty = DependencyProperty.RegisterAttached("OriginalRenderTransform", typeof (Transform), typeof (TurnstileFeatherEffect), new PropertyMetadata((PropertyChangedCallback) null));
      TurnstileFeatherEffect.OriginalOpacityProperty = DependencyProperty.RegisterAttached("OriginalOpacity", typeof (double), typeof (TurnstileFeatherEffect), new PropertyMetadata((object) 0.0));
    }
  }
}
