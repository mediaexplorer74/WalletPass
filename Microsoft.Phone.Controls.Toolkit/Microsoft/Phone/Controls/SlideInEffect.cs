// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.SlideInEffect
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  public class SlideInEffect : DependencyObject
  {
    private const double ProportionalOffset = 50.0;
    private const double ExponentialInterpolationWeight = 0.9;
    private const double BeginTime = 350.0;
    private const double BreakTime = 420.0;
    private const double EndTime = 1050.0;
    private static readonly ExponentialEase SlideInExponentialEase;
    private static readonly PropertyPath XPropertyPath;
    private static bool _selectionChanged;
    private static bool _manipulatedStarted;
    private static Dictionary<Pivot, int> _pivotsToElementCounters;
    private static Dictionary<PivotItem, List<FrameworkElement>> _pivotItemsToElements;
    public static readonly DependencyProperty LineIndexProperty;
    private static readonly DependencyProperty ParentPivotProperty;
    private static readonly DependencyProperty ParentPivotItemProperty;
    private static readonly DependencyProperty AttachedTransformProperty;
    private static readonly DependencyProperty IsSubscribedProperty;
    private static readonly DependencyProperty HasEventsAttachedProperty;

    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static int GetLineIndex(DependencyObject obj) => (int) obj.GetValue(SlideInEffect.LineIndexProperty);

    [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Standard pattern.")]
    public static void SetLineIndex(DependencyObject obj, int value) => obj.SetValue(SlideInEffect.LineIndexProperty, (object) value);

    private static void OnLineIndexPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(obj is FrameworkElement target))
        throw new InvalidOperationException("The dependency object must be a framework element.");
      if ((int) e.NewValue < 0)
      {
        if (SlideInEffect.GetHasEventsAttached((DependencyObject) target))
        {
          target.Loaded -= new RoutedEventHandler(SlideInEffect.Target_Loaded);
          target.Unloaded -= new RoutedEventHandler(SlideInEffect.Target_Unloaded);
          SlideInEffect.SetHasEventsAttached((DependencyObject) target, false);
        }
        SlideInEffect.UnsubscribeFrameworkElement(target);
      }
      else
      {
        if (!SlideInEffect.GetHasEventsAttached((DependencyObject) target))
        {
          target.Loaded += new RoutedEventHandler(SlideInEffect.Target_Loaded);
          target.Unloaded += new RoutedEventHandler(SlideInEffect.Target_Unloaded);
          SlideInEffect.SetHasEventsAttached((DependencyObject) target, true);
        }
        SlideInEffect.SubscribeFrameworkElement(target);
      }
    }

    private static Pivot GetParentPivot(DependencyObject obj) => (Pivot) obj.GetValue(SlideInEffect.ParentPivotProperty);

    private static void SetParentPivot(DependencyObject obj, Pivot value) => obj.SetValue(SlideInEffect.ParentPivotProperty, (object) value);

    private static void OnParentPivotPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      Pivot oldValue = (Pivot) e.OldValue;
      Pivot newValue = (Pivot) e.NewValue;
      if (newValue != null)
      {
        if (!SlideInEffect._pivotsToElementCounters.ContainsKey(newValue))
        {
          newValue.SelectionChanged += new SelectionChangedEventHandler(SlideInEffect.Pivot_SelectionChanged);
          ((UIElement) newValue).ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(SlideInEffect.Pivot_ManipulationStarted);
          ((UIElement) newValue).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(SlideInEffect.Pivot_ManipulationCompleted);
          SlideInEffect._pivotsToElementCounters.Add(newValue, 1);
        }
        else
        {
          Dictionary<Pivot, int> toElementCounters;
          Pivot key;
          (toElementCounters = SlideInEffect._pivotsToElementCounters)[key = newValue] = toElementCounters[key] + 1;
        }
      }
      else
      {
        Dictionary<Pivot, int> toElementCounters;
        Pivot key;
        if (SlideInEffect._pivotsToElementCounters.ContainsKey(oldValue) && ((toElementCounters = SlideInEffect._pivotsToElementCounters)[key = oldValue] = toElementCounters[key] - 1) == 0)
        {
          oldValue.SelectionChanged -= new SelectionChangedEventHandler(SlideInEffect.Pivot_SelectionChanged);
          ((UIElement) oldValue).ManipulationStarted -= new EventHandler<ManipulationStartedEventArgs>(SlideInEffect.Pivot_ManipulationStarted);
          ((UIElement) oldValue).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(SlideInEffect.Pivot_ManipulationCompleted);
          SlideInEffect._pivotsToElementCounters.Remove(oldValue);
        }
      }
    }

    private static PivotItem GetParentPivotItem(DependencyObject obj) => (PivotItem) obj.GetValue(SlideInEffect.ParentPivotItemProperty);

    private static void SetParentPivotItem(DependencyObject obj, PivotItem value) => obj.SetValue(SlideInEffect.ParentPivotItemProperty, (object) value);

    private static void OnParentPivotItemPropertyChanged(
      DependencyObject obj,
      DependencyPropertyChangedEventArgs e)
    {
      FrameworkElement frameworkElement = (FrameworkElement) obj;
      PivotItem oldValue = (PivotItem) e.OldValue;
      PivotItem newValue = (PivotItem) e.NewValue;
      if (newValue != null)
      {
        List<FrameworkElement> frameworkElementList;
        if (!SlideInEffect._pivotItemsToElements.TryGetValue(newValue, out frameworkElementList))
        {
          frameworkElementList = new List<FrameworkElement>();
          SlideInEffect._pivotItemsToElements.Add(newValue, frameworkElementList);
        }
        frameworkElementList.Add(frameworkElement);
      }
      else
      {
        List<FrameworkElement> frameworkElementList;
        if (SlideInEffect._pivotItemsToElements.TryGetValue(oldValue, out frameworkElementList))
        {
          if (frameworkElementList.Contains(frameworkElement))
            frameworkElementList.Remove(frameworkElement);
          if (frameworkElementList.Count == 0)
            SlideInEffect._pivotItemsToElements.Remove(oldValue);
        }
      }
    }

    private static TranslateTransform GetAttachedTransform(DependencyObject obj) => (TranslateTransform) obj.GetValue(SlideInEffect.AttachedTransformProperty);

    private static void SetAttachedTransform(DependencyObject obj, TranslateTransform value) => obj.SetValue(SlideInEffect.AttachedTransformProperty, (object) value);

    private static bool GetIsSubscribed(DependencyObject obj) => (bool) obj.GetValue(SlideInEffect.IsSubscribedProperty);

    private static void SetIsSubscribed(DependencyObject obj, bool value) => obj.SetValue(SlideInEffect.IsSubscribedProperty, (object) value);

    private static bool GetHasEventsAttached(DependencyObject obj) => (bool) obj.GetValue(SlideInEffect.HasEventsAttachedProperty);

    private static void SetHasEventsAttached(DependencyObject obj, bool value) => obj.SetValue(SlideInEffect.HasEventsAttachedProperty, (object) value);

    private static void Target_Loaded(object sender, RoutedEventArgs e) => SlideInEffect.SubscribeFrameworkElement((FrameworkElement) sender);

    private static void Target_Unloaded(object sender, RoutedEventArgs e) => SlideInEffect.UnsubscribeFrameworkElement((FrameworkElement) sender);

    private static void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e) => SlideInEffect._selectionChanged = true;

    private static void Pivot_ManipulationStarted(object sender, ManipulationStartedEventArgs e) => SlideInEffect._manipulatedStarted = true;

    private static void Pivot_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (SlideInEffect._selectionChanged && SlideInEffect._manipulatedStarted)
      {
        Pivot pivot = (Pivot) sender;
        List<FrameworkElement> frameworkElementList;
        if (!(((ItemsControl) pivot).ItemContainerGenerator.ContainerFromItem(pivot.SelectedItem) is PivotItem key) || !SlideInEffect._pivotItemsToElements.TryGetValue(key, out frameworkElementList))
          return;
        Storyboard storyboard = new Storyboard();
        foreach (FrameworkElement element in frameworkElementList)
        {
          if (element != null && SlideInEffect.IsOnScreen(element))
          {
            bool leftToRight = e.TotalManipulation.Translation.X <= 0.0;
            SlideInEffect.ComposeStoryboard(element, leftToRight, ref storyboard);
          }
        }
        ((Timeline) storyboard).Completed += (EventHandler) ((s1, e1) => storyboard.Stop());
        storyboard.Begin();
      }
      SlideInEffect._selectionChanged = SlideInEffect._manipulatedStarted = false;
    }

    private static void SubscribeFrameworkElement(FrameworkElement target)
    {
      if (SlideInEffect.GetIsSubscribed((DependencyObject) target))
        return;
      DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject) target);
      Pivot pivot1 = (Pivot) null;
      PivotItem pivotItem1;
      PivotItem pivotItem2 = pivotItem1 = (PivotItem) null;
      for (; pivot1 == null && parent != null; parent = VisualTreeHelper.GetParent(parent))
      {
        pivot1 = parent as Pivot;
        if (parent is PivotItem pivotItem3)
          pivotItem2 = pivotItem3;
      }
      if (parent == null || pivotItem2 == null)
        return;
      Pivot pivot2 = pivot1;
      SlideInEffect.AttachTransform(target);
      SlideInEffect.SetParentPivot((DependencyObject) target, pivot2);
      SlideInEffect.SetParentPivotItem((DependencyObject) target, pivotItem2);
      SlideInEffect.SetIsSubscribed((DependencyObject) target, true);
    }

    private static void UnsubscribeFrameworkElement(FrameworkElement target)
    {
      if (!SlideInEffect.GetIsSubscribed((DependencyObject) target))
        return;
      SlideInEffect.SetParentPivot((DependencyObject) target, (Pivot) null);
      SlideInEffect.SetParentPivotItem((DependencyObject) target, (PivotItem) null);
      SlideInEffect.SetIsSubscribed((DependencyObject) target, false);
    }

    private static void ComposeStoryboard(
      FrameworkElement element,
      bool leftToRight,
      ref Storyboard storyboard)
    {
      double num1 = (double) SlideInEffect.GetLineIndex((DependencyObject) element) * 50.0;
      double num2 = leftToRight ? num1 : -num1;
      TranslateTransform attachedTransform = SlideInEffect.GetAttachedTransform((DependencyObject) element);
      DoubleAnimationUsingKeyFrames animationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
      LinearDoubleKeyFrame linearDoubleKeyFrame1 = new LinearDoubleKeyFrame();
      ((DoubleKeyFrame) linearDoubleKeyFrame1).KeyTime = KeyTime.op_Implicit(TimeSpan.Zero);
      ((DoubleKeyFrame) linearDoubleKeyFrame1).Value = num2;
      ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames.KeyFrames).Add((DoubleKeyFrame) linearDoubleKeyFrame1);
      LinearDoubleKeyFrame linearDoubleKeyFrame2 = new LinearDoubleKeyFrame();
      ((DoubleKeyFrame) linearDoubleKeyFrame2).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds(350.0));
      ((DoubleKeyFrame) linearDoubleKeyFrame2).Value = num2;
      ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames.KeyFrames).Add((DoubleKeyFrame) linearDoubleKeyFrame2);
      LinearDoubleKeyFrame linearDoubleKeyFrame3 = new LinearDoubleKeyFrame();
      ((DoubleKeyFrame) linearDoubleKeyFrame3).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds(420.0));
      ((DoubleKeyFrame) linearDoubleKeyFrame3).Value = num2 * 0.9;
      ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames.KeyFrames).Add((DoubleKeyFrame) linearDoubleKeyFrame3);
      EasingDoubleKeyFrame easingDoubleKeyFrame = new EasingDoubleKeyFrame();
      ((DoubleKeyFrame) easingDoubleKeyFrame).KeyTime = KeyTime.op_Implicit(TimeSpan.FromMilliseconds(1050.0));
      ((DoubleKeyFrame) easingDoubleKeyFrame).Value = 0.0;
      easingDoubleKeyFrame.EasingFunction = (IEasingFunction) SlideInEffect.SlideInExponentialEase;
      ((PresentationFrameworkCollection<DoubleKeyFrame>) animationUsingKeyFrames.KeyFrames).Add((DoubleKeyFrame) easingDoubleKeyFrame);
      Storyboard.SetTarget((Timeline) animationUsingKeyFrames, (DependencyObject) attachedTransform);
      Storyboard.SetTargetProperty((Timeline) animationUsingKeyFrames, SlideInEffect.XPropertyPath);
      ((PresentationFrameworkCollection<Timeline>) storyboard.Children).Add((Timeline) animationUsingKeyFrames);
    }

    private static bool IsOnScreen(FrameworkElement element)
    {
      if (!(Application.Current.RootVisual is PhoneApplicationFrame rootVisual))
        return false;
      double actualHeight = ((FrameworkElement) rootVisual).ActualHeight;
      GeneralTransform visual;
      try
      {
        visual = ((UIElement) element).TransformToVisual((UIElement) rootVisual);
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      Rect rect = new Rect(visual.Transform(new Point(0.0, 0.0)), visual.Transform(new Point(element.ActualWidth, element.ActualHeight)));
      return rect.Bottom > 0.0 && rect.Top < actualHeight;
    }

    private static void AttachTransform(FrameworkElement element)
    {
      Transform renderTransform = ((UIElement) element).RenderTransform;
      if (SlideInEffect.GetAttachedTransform((DependencyObject) element) != null)
        return;
      TranslateTransform translateTransform = new TranslateTransform()
      {
        X = 0.0
      };
      if (renderTransform == null)
      {
        ((UIElement) element).RenderTransform = (Transform) translateTransform;
      }
      else
      {
        TransformGroup transformGroup = new TransformGroup();
        ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add(renderTransform);
        ((PresentationFrameworkCollection<Transform>) transformGroup.Children).Add((Transform) translateTransform);
        ((UIElement) element).RenderTransform = (Transform) transformGroup;
      }
      SlideInEffect.SetAttachedTransform((DependencyObject) element, translateTransform);
    }

    static SlideInEffect()
    {
      ExponentialEase exponentialEase = new ExponentialEase();
      ((EasingFunctionBase) exponentialEase).EasingMode = (EasingMode) 0;
      exponentialEase.Exponent = 5.0;
      SlideInEffect.SlideInExponentialEase = exponentialEase;
      SlideInEffect.XPropertyPath = new PropertyPath("X", new object[0]);
      SlideInEffect._pivotsToElementCounters = new Dictionary<Pivot, int>();
      SlideInEffect._pivotItemsToElements = new Dictionary<PivotItem, List<FrameworkElement>>();
      SlideInEffect.LineIndexProperty = DependencyProperty.RegisterAttached("LineIndex", typeof (int), typeof (SlideInEffect), new PropertyMetadata((object) -1, new PropertyChangedCallback(SlideInEffect.OnLineIndexPropertyChanged)));
      SlideInEffect.ParentPivotProperty = DependencyProperty.RegisterAttached("ParentPivot", typeof (Pivot), typeof (SlideInEffect), new PropertyMetadata((object) null, new PropertyChangedCallback(SlideInEffect.OnParentPivotPropertyChanged)));
      SlideInEffect.ParentPivotItemProperty = DependencyProperty.RegisterAttached("ParentPivotItem", typeof (PivotItem), typeof (SlideInEffect), new PropertyMetadata((object) null, new PropertyChangedCallback(SlideInEffect.OnParentPivotItemPropertyChanged)));
      SlideInEffect.AttachedTransformProperty = DependencyProperty.RegisterAttached("AttachedTransform", typeof (TranslateTransform), typeof (SlideInEffect), new PropertyMetadata((PropertyChangedCallback) null));
      SlideInEffect.IsSubscribedProperty = DependencyProperty.RegisterAttached("IsSubscribed", typeof (bool), typeof (SlideInEffect), new PropertyMetadata((object) false, (PropertyChangedCallback) null));
      SlideInEffect.HasEventsAttachedProperty = DependencyProperty.RegisterAttached("HasEventsAttached", typeof (bool), typeof (SlideInEffect), new PropertyMetadata((object) false));
    }
  }
}
