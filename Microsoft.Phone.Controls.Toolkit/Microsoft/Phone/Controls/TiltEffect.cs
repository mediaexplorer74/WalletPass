// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TiltEffect
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// This code provides attached properties for adding a 'tilt' effect to all
  /// controls within a container.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  [SuppressMessage("Microsoft.Design", "CA1052:StaticHolderTypesShouldBeSealed", Justification = "Cannot be static and derive from DependencyObject.")]
  public class TiltEffect : DependencyObject
  {
    /// <summary>Maximum amount of tilt, in radians.</summary>
    private const double MaxAngle = 0.3;
    /// <summary>Maximum amount of depression, in pixels</summary>
    private const double MaxDepression = 25.0;
    /// <summary>
    /// Cache of previous cache modes. Not using weak references for now.
    /// </summary>
    private static Dictionary<DependencyObject, CacheMode> _originalCacheMode = new Dictionary<DependencyObject, CacheMode>();
    /// <summary>
    /// Delay between releasing an element and the tilt release animation
    /// playing.
    /// </summary>
    private static readonly TimeSpan TiltReturnAnimationDelay = TimeSpan.FromMilliseconds(200.0);
    /// <summary>Duration of tilt release animation.</summary>
    private static readonly TimeSpan TiltReturnAnimationDuration = TimeSpan.FromMilliseconds(100.0);
    /// <summary>The control that is currently being tilted.</summary>
    private static FrameworkElement currentTiltElement;
    /// <summary>
    /// The single instance of a storyboard used for all tilts.
    /// </summary>
    private static Storyboard tiltReturnStoryboard;
    /// <summary>
    /// The single instance of an X rotation used for all tilts.
    /// </summary>
    private static DoubleAnimation tiltReturnXAnimation;
    /// <summary>
    /// The single instance of a Y rotation used for all tilts.
    /// </summary>
    private static DoubleAnimation tiltReturnYAnimation;
    /// <summary>
    /// The single instance of a Z depression used for all tilts.
    /// </summary>
    private static DoubleAnimation tiltReturnZAnimation;
    /// <summary>The center of the tilt element.</summary>
    private static Point currentTiltElementCenter;
    /// <summary>
    /// Whether the animation just completed was for a 'pause' or not.
    /// </summary>
    private static bool wasPauseAnimation = false;
    /// <summary>
    /// Whether the tilt effect is enabled on a container (and all its
    /// children).
    /// </summary>
    public static readonly DependencyProperty IsTiltEnabledProperty = DependencyProperty.RegisterAttached("IsTiltEnabled", typeof (bool), typeof (TiltEffect), new PropertyMetadata(new PropertyChangedCallback(TiltEffect.OnIsTiltEnabledChanged)));
    /// <summary>
    /// Suppresses the tilt effect on a single control that would otherwise
    /// be tilted.
    /// </summary>
    public static readonly DependencyProperty SuppressTiltProperty = DependencyProperty.RegisterAttached("SuppressTilt", typeof (bool), typeof (TiltEffect), (PropertyMetadata) null);

    /// <summary>
    /// Whether to use a slightly more accurate (but slightly slower) tilt
    /// animation easing function.
    /// </summary>
    public static bool UseLogarithmicEase { get; set; }

    /// <summary>Default list of items that are tiltable.</summary>
    [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Keeping it simple.")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Tiltable", Justification = "By design.")]
    public static List<Type> TiltableItems { get; private set; }

    /// <summary>
    /// This is not a constructable class, but it cannot be static because
    /// it derives from DependencyObject.
    /// </summary>
    private TiltEffect()
    {
    }

    /// <summary>Initialize the static properties</summary>
    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Need to initialize the tiltable items property.")]
    static TiltEffect() => TiltEffect.TiltableItems = new List<Type>()
    {
      typeof (ButtonBase),
      typeof (ListBoxItem),
      typeof (ListPicker),
      typeof (MenuItem),
      typeof (LongListSelector)
    };

    /// <summary>
    /// Gets the IsTiltEnabled dependency property from an object.
    /// </summary>
    /// <param name="source">The object to get the property from.</param>
    /// <returns>The property's value.</returns>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static bool GetIsTiltEnabled(DependencyObject source) => (bool) source.GetValue(TiltEffect.IsTiltEnabledProperty);

    /// <summary>
    /// Sets the IsTiltEnabled dependency property on an object.
    /// </summary>
    /// <param name="source">The object to set the property on.</param>
    /// <param name="value">The value to set.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static void SetIsTiltEnabled(DependencyObject source, bool value) => source.SetValue(TiltEffect.IsTiltEnabledProperty, (object) value);

    /// <summary>
    /// Gets the SuppressTilt dependency property from an object.
    /// </summary>
    /// <param name="source">The object to get the property from.</param>
    /// <returns>The property's value.</returns>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static bool GetSuppressTilt(DependencyObject source) => (bool) source.GetValue(TiltEffect.SuppressTiltProperty);

    /// <summary>
    /// Sets the SuppressTilt dependency property from an object.
    /// </summary>
    /// <param name="source">The object to get the property from.</param>
    /// <param name="value">The property's value.</param>
    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static void SetSuppressTilt(DependencyObject source, bool value) => source.SetValue(TiltEffect.SuppressTiltProperty, (object) value);

    /// <summary>
    /// Property change handler for the IsTiltEnabled dependency property.
    /// </summary>
    /// <param name="target">The element that the property is atteched to.</param>
    /// <param name="args">Event arguments.</param>
    /// <remarks>
    /// Adds or removes event handlers from the element that has been
    /// (un)registered for tilting.
    /// </remarks>
    private static void OnIsTiltEnabledChanged(
      DependencyObject target,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(target is FrameworkElement frameworkElement))
        return;
      if ((bool) args.NewValue)
        ((UIElement) frameworkElement).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(TiltEffect.TiltEffect_ManipulationStarted);
      else
        ((UIElement) frameworkElement).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(TiltEffect.TiltEffect_ManipulationStarted);
    }

    /// <summary>Event handler for ManipulationStarted.</summary>
    /// <param name="sender">sender of the event - this will be the tilt
    /// container (eg, entire page).</param>
    /// <param name="e">Event arguments.</param>
    private static void TiltEffect_ManipulationStarted(
      object sender,
      ManipulationStartedEventArgs e)
    {
      TiltEffect.TryStartTiltEffect(sender as FrameworkElement, e);
    }

    /// <summary>Event handler for ManipulationDelta</summary>
    /// <param name="sender">sender of the event - this will be the tilting
    /// object (eg a button).</param>
    /// <param name="e">Event arguments.</param>
    private static void TiltEffect_ManipulationDelta(object sender, ManipulationDeltaEventArgs e) => TiltEffect.ContinueTiltEffect(sender as FrameworkElement, e);

    /// <summary>Event handler for ManipulationCompleted.</summary>
    /// <param name="sender">sender of the event - this will be the tilting
    /// object (eg a button).</param>
    /// <param name="e">Event arguments.</param>
    private static void TiltEffect_ManipulationCompleted(
      object sender,
      ManipulationCompletedEventArgs e)
    {
      TiltEffect.EndTiltEffect(TiltEffect.currentTiltElement);
    }

    /// <summary>
    /// Checks if the manipulation should cause a tilt, and if so starts the
    /// tilt effect.
    /// </summary>
    /// <param name="source">The source of the manipulation (the tilt
    /// container, eg entire page).</param>
    /// <param name="e">The args from the ManipulationStarted event.</param>
    private static void TryStartTiltEffect(FrameworkElement source, ManipulationStartedEventArgs e)
    {
      foreach (FrameworkElement visualAncestor in (((RoutedEventArgs) e).OriginalSource as FrameworkElement).GetVisualAncestors())
      {
        foreach (Type tiltableItem in TiltEffect.TiltableItems)
        {
          if (tiltableItem.IsAssignableFrom(visualAncestor.GetType()))
          {
            FrameworkElement frameworkElement = !(((DependencyObject) visualAncestor).ReadLocalValue(TiltEffect.SuppressTiltProperty) is bool) ? visualAncestor.GetVisualAncestors().FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>) (x => ((DependencyObject) x).ReadLocalValue(TiltEffect.SuppressTiltProperty) is bool)) : visualAncestor;
            if (frameworkElement == null || !(bool) ((DependencyObject) frameworkElement).ReadLocalValue(TiltEffect.SuppressTiltProperty))
            {
              if ((object) tiltableItem == (object) typeof (LongListSelector))
              {
                TiltEffect.StartTiltEffectOnLLS((LongListSelector) visualAncestor, e);
                return;
              }
              FrameworkElement child = VisualTreeHelper.GetChild((DependencyObject) visualAncestor, 0) as FrameworkElement;
              FrameworkElement manipulationContainer = e.ManipulationContainer as FrameworkElement;
              if (child == null || manipulationContainer == null)
                return;
              Point touchPoint = ((UIElement) manipulationContainer).TransformToVisual((UIElement) child).Transform(e.ManipulationOrigin);
              Point centerPoint = new Point(child.ActualWidth / 2.0, child.ActualHeight / 2.0);
              Point centerToCenterDelta = TiltEffect.GetCenterToCenterDelta(child, source);
              TiltEffect.BeginTiltEffect(child, touchPoint, centerPoint, centerToCenterDelta);
              return;
            }
          }
        }
      }
    }

    /// <summary>Starts the tilt effect on LLS items or sticky header.</summary>
    private static void StartTiltEffectOnLLS(LongListSelector lls, ManipulationStartedEventArgs e)
    {
      FrameworkElement node = (FrameworkElement) ((RoutedEventArgs) e).OriginalSource;
      ContentPresenter[] contentPresenterArray = new ContentPresenter[2];
      int num = 0;
      do
      {
        if (node is ContentPresenter contentPresenter)
          contentPresenterArray[num++ % 2] = contentPresenter;
        node = node.GetVisualParent();
      }
      while (node != lls && (object) node.GetType() != (object) typeof (ViewportControl));
      if (node == lls || num < 2 || !(((DependencyObject) contentPresenterArray[num % 2]).GetVisualChildren().FirstOrDefault<DependencyObject>() is FrameworkElement element) || ((DependencyObject) element).ReadLocalValue(TiltEffect.SuppressTiltProperty) is bool flag && flag)
        return;
      Point touchPoint = ((UIElement) (e.ManipulationContainer as FrameworkElement)).TransformToVisual((UIElement) element).Transform(e.ManipulationOrigin);
      Point centerPoint = new Point(element.ActualWidth / 2.0, element.ActualHeight / 2.0);
      TiltEffect.BeginTiltEffect(element, touchPoint, centerPoint, new Point(0.0, 0.0));
    }

    /// <summary>
    /// Computes the delta between the centre of an element and its
    /// container.
    /// </summary>
    /// <param name="element">The element to compare.</param>
    /// <param name="container">The element to compare against.</param>
    /// <returns>A point that represents the delta between the two centers.</returns>
    private static Point GetCenterToCenterDelta(
      FrameworkElement element,
      FrameworkElement container)
    {
      Point point1 = new Point(element.ActualWidth / 2.0, element.ActualHeight / 2.0);
      Point point2 = !(container is PhoneApplicationFrame applicationFrame) ? new Point(container.ActualWidth / 2.0, container.ActualHeight / 2.0) : ((applicationFrame.Orientation & 2) != 2 ? new Point(container.ActualWidth / 2.0, container.ActualHeight / 2.0) : new Point(container.ActualHeight / 2.0, container.ActualWidth / 2.0));
      Point point3 = ((UIElement) element).TransformToVisual((UIElement) container).Transform(point1);
      return new Point(point2.X - point3.X, point2.Y - point3.Y);
    }

    /// <summary>
    /// Begins the tilt effect by preparing the control and doing the
    /// initial animation.
    /// </summary>
    /// <param name="element">The element to tilt.</param>
    /// <param name="touchPoint">The touch point, in element coordinates.</param>
    /// <param name="centerPoint">The center point of the element in element
    /// coordinates.</param>
    /// <param name="centerDelta">The delta between the
    /// <paramref name="element" />'s center and the container's center.</param>
    private static void BeginTiltEffect(
      FrameworkElement element,
      Point touchPoint,
      Point centerPoint,
      Point centerDelta)
    {
      if (TiltEffect.tiltReturnStoryboard != null)
        TiltEffect.StopTiltReturnStoryboardAndCleanup();
      if (!TiltEffect.PrepareControlForTilt(element, centerDelta))
        return;
      TiltEffect.currentTiltElement = element;
      TiltEffect.currentTiltElementCenter = centerPoint;
      TiltEffect.PrepareTiltReturnStoryboard(element);
      TiltEffect.ApplyTiltEffect(TiltEffect.currentTiltElement, touchPoint, TiltEffect.currentTiltElementCenter);
    }

    /// <summary>
    /// Prepares a control to be tilted by setting up a plane projection and
    /// some event handlers.
    /// </summary>
    /// <param name="element">The control that is to be tilted.</param>
    /// <param name="centerDelta">Delta between the element's center and the
    /// tilt container's.</param>
    /// <returns>true if successful; false otherwise.</returns>
    /// <remarks>
    /// This method is conservative; it will fail any attempt to tilt a
    /// control that already has a projection on it.
    /// </remarks>
    private static bool PrepareControlForTilt(FrameworkElement element, Point centerDelta)
    {
      if (((UIElement) element).Projection != null || ((UIElement) element).RenderTransform != null && (object) ((UIElement) element).RenderTransform.GetType() != (object) typeof (MatrixTransform))
        return false;
      TiltEffect._originalCacheMode[(DependencyObject) element] = ((UIElement) element).CacheMode;
      ((UIElement) element).CacheMode = (CacheMode) new BitmapCache();
      ((UIElement) element).RenderTransform = (Transform) new TranslateTransform()
      {
        X = centerDelta.X,
        Y = centerDelta.Y
      };
      ((UIElement) element).Projection = (Projection) new PlaneProjection()
      {
        GlobalOffsetX = (-1.0 * centerDelta.X),
        GlobalOffsetY = (-1.0 * centerDelta.Y)
      };
      ((UIElement) element).ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(TiltEffect.TiltEffect_ManipulationDelta);
      ((UIElement) element).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(TiltEffect.TiltEffect_ManipulationCompleted);
      return true;
    }

    /// <summary>Removes modifications made by PrepareControlForTilt.</summary>
    /// <param name="element">THe control to be un-prepared.</param>
    /// <remarks>
    /// This method is basic; it does not do anything to detect if the
    /// control being un-prepared was previously prepared.
    /// </remarks>
    private static void RevertPrepareControlForTilt(FrameworkElement element)
    {
      ((UIElement) element).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(TiltEffect.TiltEffect_ManipulationDelta);
      ((UIElement) element).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(TiltEffect.TiltEffect_ManipulationCompleted);
      ((UIElement) element).Projection = (Projection) null;
      ((UIElement) element).RenderTransform = (Transform) null;
      CacheMode cacheMode;
      if (TiltEffect._originalCacheMode.TryGetValue((DependencyObject) element, out cacheMode))
      {
        ((UIElement) element).CacheMode = cacheMode;
        TiltEffect._originalCacheMode.Remove((DependencyObject) element);
      }
      else
        ((UIElement) element).CacheMode = (CacheMode) null;
    }

    /// <summary>
    /// Creates the tilt return storyboard (if not already created) and
    /// targets it to the projection.
    /// </summary>
    /// <param name="element">The framework element to prepare for
    /// projection.</param>
    private static void PrepareTiltReturnStoryboard(FrameworkElement element)
    {
      if (TiltEffect.tiltReturnStoryboard == null)
      {
        TiltEffect.tiltReturnStoryboard = new Storyboard();
        ((Timeline) TiltEffect.tiltReturnStoryboard).Completed += new EventHandler(TiltEffect.TiltReturnStoryboard_Completed);
        TiltEffect.tiltReturnXAnimation = new DoubleAnimation();
        Storyboard.SetTargetProperty((Timeline) TiltEffect.tiltReturnXAnimation, new PropertyPath((object) PlaneProjection.RotationXProperty));
        ((Timeline) TiltEffect.tiltReturnXAnimation).BeginTime = new TimeSpan?(TiltEffect.TiltReturnAnimationDelay);
        TiltEffect.tiltReturnXAnimation.To = new double?(0.0);
        ((Timeline) TiltEffect.tiltReturnXAnimation).Duration = Duration.op_Implicit(TiltEffect.TiltReturnAnimationDuration);
        TiltEffect.tiltReturnYAnimation = new DoubleAnimation();
        Storyboard.SetTargetProperty((Timeline) TiltEffect.tiltReturnYAnimation, new PropertyPath((object) PlaneProjection.RotationYProperty));
        ((Timeline) TiltEffect.tiltReturnYAnimation).BeginTime = new TimeSpan?(TiltEffect.TiltReturnAnimationDelay);
        TiltEffect.tiltReturnYAnimation.To = new double?(0.0);
        ((Timeline) TiltEffect.tiltReturnYAnimation).Duration = Duration.op_Implicit(TiltEffect.TiltReturnAnimationDuration);
        TiltEffect.tiltReturnZAnimation = new DoubleAnimation();
        Storyboard.SetTargetProperty((Timeline) TiltEffect.tiltReturnZAnimation, new PropertyPath((object) PlaneProjection.GlobalOffsetZProperty));
        ((Timeline) TiltEffect.tiltReturnZAnimation).BeginTime = new TimeSpan?(TiltEffect.TiltReturnAnimationDelay);
        TiltEffect.tiltReturnZAnimation.To = new double?(0.0);
        ((Timeline) TiltEffect.tiltReturnZAnimation).Duration = Duration.op_Implicit(TiltEffect.TiltReturnAnimationDuration);
        if (TiltEffect.UseLogarithmicEase)
        {
          TiltEffect.tiltReturnXAnimation.EasingFunction = (IEasingFunction) new TiltEffect.LogarithmicEase();
          TiltEffect.tiltReturnYAnimation.EasingFunction = (IEasingFunction) new TiltEffect.LogarithmicEase();
          TiltEffect.tiltReturnZAnimation.EasingFunction = (IEasingFunction) new TiltEffect.LogarithmicEase();
        }
        ((PresentationFrameworkCollection<Timeline>) TiltEffect.tiltReturnStoryboard.Children).Add((Timeline) TiltEffect.tiltReturnXAnimation);
        ((PresentationFrameworkCollection<Timeline>) TiltEffect.tiltReturnStoryboard.Children).Add((Timeline) TiltEffect.tiltReturnYAnimation);
        ((PresentationFrameworkCollection<Timeline>) TiltEffect.tiltReturnStoryboard.Children).Add((Timeline) TiltEffect.tiltReturnZAnimation);
      }
      Storyboard.SetTarget((Timeline) TiltEffect.tiltReturnXAnimation, (DependencyObject) ((UIElement) element).Projection);
      Storyboard.SetTarget((Timeline) TiltEffect.tiltReturnYAnimation, (DependencyObject) ((UIElement) element).Projection);
      Storyboard.SetTarget((Timeline) TiltEffect.tiltReturnZAnimation, (DependencyObject) ((UIElement) element).Projection);
    }

    /// <summary>
    /// Continues a tilt effect that is currently applied to an element,
    /// presumably because the user moved their finger.
    /// </summary>
    /// <param name="element">The element being tilted.</param>
    /// <param name="e">The manipulation event args.</param>
    private static void ContinueTiltEffect(FrameworkElement element, ManipulationDeltaEventArgs e)
    {
      if (!(e.ManipulationContainer is FrameworkElement manipulationContainer) || element == null)
        return;
      Point touchPoint = ((UIElement) manipulationContainer).TransformToVisual((UIElement) element).Transform(e.ManipulationOrigin);
      if (!new Rect(0.0, 0.0, TiltEffect.currentTiltElement.ActualWidth, TiltEffect.currentTiltElement.ActualHeight).Contains(touchPoint))
        TiltEffect.PauseTiltEffect();
      else
        TiltEffect.ApplyTiltEffect(TiltEffect.currentTiltElement, touchPoint, TiltEffect.currentTiltElementCenter);
    }

    /// <summary>Ends the tilt effect by playing the animation.</summary>
    /// <param name="element">The element being tilted.</param>
    private static void EndTiltEffect(FrameworkElement element)
    {
      if (element != null)
      {
        ((UIElement) element).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(TiltEffect.TiltEffect_ManipulationCompleted);
        ((UIElement) element).ManipulationDelta -= new EventHandler<ManipulationDeltaEventArgs>(TiltEffect.TiltEffect_ManipulationDelta);
      }
      if (TiltEffect.tiltReturnStoryboard != null)
      {
        TiltEffect.wasPauseAnimation = false;
        if (TiltEffect.tiltReturnStoryboard.GetCurrentState() == 0)
          return;
        TiltEffect.tiltReturnStoryboard.Begin();
      }
      else
        TiltEffect.StopTiltReturnStoryboardAndCleanup();
    }

    /// <summary>Handler for the storyboard complete event.</summary>
    /// <param name="sender">sender of the event.</param>
    /// <param name="e">event args.</param>
    private static void TiltReturnStoryboard_Completed(object sender, EventArgs e)
    {
      if (TiltEffect.wasPauseAnimation)
        TiltEffect.ResetTiltEffect(TiltEffect.currentTiltElement);
      else
        TiltEffect.StopTiltReturnStoryboardAndCleanup();
    }

    /// <summary>
    /// Resets the tilt effect on the control, making it appear 'normal'
    /// again.
    /// </summary>
    /// <param name="element">The element to reset the tilt on.</param>
    /// <remarks>
    /// This method doesn't turn off the tilt effect or cancel any current
    /// manipulation; it just temporarily cancels the effect.
    /// </remarks>
    private static void ResetTiltEffect(FrameworkElement element)
    {
      PlaneProjection projection = ((UIElement) element).Projection as PlaneProjection;
      projection.RotationY = 0.0;
      projection.RotationX = 0.0;
      projection.GlobalOffsetZ = 0.0;
    }

    /// <summary>
    /// Stops the tilt effect and release resources applied to the currently
    /// tilted control.
    /// </summary>
    private static void StopTiltReturnStoryboardAndCleanup()
    {
      if (TiltEffect.tiltReturnStoryboard != null)
        TiltEffect.tiltReturnStoryboard.Stop();
      if (TiltEffect.currentTiltElement == null)
        return;
      TiltEffect.RevertPrepareControlForTilt(TiltEffect.currentTiltElement);
      TiltEffect.currentTiltElement = (FrameworkElement) null;
    }

    /// <summary>
    /// Pauses the tilt effect so that the control returns to the 'at rest'
    /// position, but doesn't stop the tilt effect (handlers are still
    /// attached).
    /// </summary>
    private static void PauseTiltEffect()
    {
      if (TiltEffect.tiltReturnStoryboard == null || TiltEffect.wasPauseAnimation)
        return;
      TiltEffect.tiltReturnStoryboard.Stop();
      TiltEffect.wasPauseAnimation = true;
      TiltEffect.tiltReturnStoryboard.Begin();
    }

    /// <summary>Resets the storyboard to not running.</summary>
    private static void ResetTiltReturnStoryboard()
    {
      TiltEffect.tiltReturnStoryboard.Stop();
      TiltEffect.wasPauseAnimation = false;
    }

    /// <summary>Applies the tilt effect to the control.</summary>
    /// <param name="element">the control to tilt.</param>
    /// <param name="touchPoint">The touch point, in the container's
    /// coordinates.</param>
    /// <param name="centerPoint">The center point of the container.</param>
    private static void ApplyTiltEffect(
      FrameworkElement element,
      Point touchPoint,
      Point centerPoint)
    {
      TiltEffect.ResetTiltReturnStoryboard();
      Point point = new Point(Math.Min(Math.Max(touchPoint.X / (centerPoint.X * 2.0), 0.0), 1.0), Math.Min(Math.Max(touchPoint.Y / (centerPoint.Y * 2.0), 0.0), 1.0));
      if (double.IsNaN(point.X) || double.IsNaN(point.Y))
        return;
      double num1 = Math.Abs(point.X - 0.5);
      double num2 = Math.Abs(point.Y - 0.5);
      double num3 = (double) -Math.Sign(point.X - 0.5);
      double num4 = (double) Math.Sign(point.Y - 0.5);
      double num5 = num1 + num2;
      double num6 = num1 + num2 > 0.0 ? num1 / (num1 + num2) : 0.0;
      double num7 = num5 * 0.3 * 180.0 / Math.PI;
      double num8 = (1.0 - num5) * 25.0;
      PlaneProjection projection = ((UIElement) element).Projection as PlaneProjection;
      projection.RotationY = num7 * num6 * num3;
      projection.RotationX = num7 * (1.0 - num6) * num4;
      projection.GlobalOffsetZ = -num8;
    }

    /// <summary>Provides an easing function for the tilt return.</summary>
    private class LogarithmicEase : EasingFunctionBase
    {
      /// <summary>Computes the easing function.</summary>
      /// <param name="normalizedTime">The time.</param>
      /// <returns>The eased value.</returns>
      protected virtual double EaseInCore(double normalizedTime) => Math.Log(normalizedTime + 1.0) / 0.693147181;
    }
  }
}
