// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Transitions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Provides
  /// <see cref="T:Microsoft.Phone.Controls.ITransition" />s
  /// for transition families and modes.
  /// </summary>
  /// <QualityBand>Preview</QualityBand>
  internal static class Transitions
  {
    /// <summary>The cached XAML read from the Storyboard resources.</summary>
    private static Dictionary<string, string> _storyboardXamlCache;

    /// <summary>
    /// Creates a
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a transition family, transition mode, and
    /// <see cref="T:System.Windows.UIElement" />.
    /// </summary>
    /// <typeparam name="T">The type of the transition mode.</typeparam>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="name">The transition family.</param>
    /// <param name="mode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    private static ITransition GetEnumStoryboard<T>(UIElement element, string name, T mode)
    {
      Storyboard storyboard = Transitions.GetStoryboard(name + Enum.GetName(typeof (T), (object) mode));
      if (storyboard == null)
        return (ITransition) null;
      Storyboard.SetTarget((Timeline) storyboard, (DependencyObject) element);
      return (ITransition) new Transition(element, storyboard);
    }

    /// <summary>
    /// Creates a
    /// <see cref="T:System.Windows.Media.Storyboard" />
    /// for a particular transition family and transition mode.
    /// </summary>
    /// <param name="name">The transition family and transition mode.</param>
    /// <returns>The <see cref="T:System.Windows.Media.Storyboard" />.</returns>
    private static Storyboard GetStoryboard(string name)
    {
      if (Transitions._storyboardXamlCache == null)
        Transitions._storyboardXamlCache = new Dictionary<string, string>();
      string str = (string) null;
      if (Transitions._storyboardXamlCache.ContainsKey(name))
      {
        str = Transitions._storyboardXamlCache[name];
      }
      else
      {
        using (StreamReader streamReader = new StreamReader(Application.GetResourceStream(new Uri("/Microsoft.Phone.Controls.Toolkit;component/Transitions/Storyboards/" + name + ".xaml", UriKind.Relative)).Stream))
        {
          str = streamReader.ReadToEnd();
          Transitions._storyboardXamlCache[name] = str;
        }
      }
      return XamlReader.Load(str) as Storyboard;
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the roll transition.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Roll(UIElement element)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      Storyboard storyboard = Transitions.GetStoryboard(nameof (Roll));
      Storyboard.SetTarget((Timeline) storyboard, (DependencyObject) element);
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.5,
        CenterOfRotationY = 0.5
      };
      return (ITransition) new Transition(element, storyboard);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the rotate transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="rotateTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Rotate(UIElement element, RotateTransitionMode rotateTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (RotateTransitionMode), (object) rotateTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (rotateTransitionMode));
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.5,
        CenterOfRotationY = 0.5
      };
      rotateTransitionMode = Transitions.AdjustRotateTransitionModeForFlowDirection(element, rotateTransitionMode);
      return Transitions.GetEnumStoryboard<RotateTransitionMode>(element, nameof (Rotate), rotateTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the slide transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="slideTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Slide(UIElement element, SlideTransitionMode slideTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (SlideTransitionMode), (object) slideTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (slideTransitionMode));
      element.RenderTransform = (Transform) new TranslateTransform();
      return Transitions.GetEnumStoryboard<SlideTransitionMode>(element, string.Empty, slideTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the swivel transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="swivelTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Swivel(UIElement element, SwivelTransitionMode swivelTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (SwivelTransitionMode), (object) swivelTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (swivelTransitionMode));
      element.Projection = (Projection) new PlaneProjection();
      return Transitions.GetEnumStoryboard<SwivelTransitionMode>(element, nameof (Swivel), swivelTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the turnstile transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="turnstileTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Turnstile(
      UIElement element,
      TurnstileTransitionMode turnstileTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (TurnstileTransitionMode), (object) turnstileTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (turnstileTransitionMode));
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.0
      };
      return Transitions.GetEnumStoryboard<TurnstileTransitionMode>(element, nameof (Turnstile), turnstileTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the turnover transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="turnOverTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition TurnOver(
      UIElement element,
      TurnOverTransitionMode turnOverTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (TurnOverTransitionMode), (object) turnOverTransitionMode))
        throw new ArgumentOutOfRangeException("turnoverTransitionMode");
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.0
      };
      return Transitions.GetEnumStoryboard<TurnOverTransitionMode>(element, nameof (TurnOver), turnOverTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the opacity transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="turnOverTransitionMode">The transition mode.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition Opacity(
      UIElement element,
      OpacityTransitionMode opacityTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (OpacityTransitionMode), (object) opacityTransitionMode))
        throw new ArgumentOutOfRangeException("turnoverTransitionMode");
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.0
      };
      return Transitions.GetEnumStoryboard<OpacityTransitionMode>(element, nameof (Opacity), opacityTransitionMode);
    }

    /// <summary>
    /// Creates an
    /// <see cref="T:Microsoft.Phone.Controls.ITransition" />
    /// for a
    /// <see cref="T:System.Windows.UIElement" />
    /// for the turnstile feather transition family.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="turnstileFeatherTransitionMode">The transition mode.</param>
    /// <param name="beginTime">The time at which the transition should begin.</param>
    /// <returns>The <see cref="T:Microsoft.Phone.Controls.ITransition" />.</returns>
    public static ITransition TurnstileFeather(
      UIElement element,
      TurnstileFeatherTransitionMode turnstileFeatherTransitionMode,
      TimeSpan? beginTime)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (TurnstileFeatherTransitionMode), (object) turnstileFeatherTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (turnstileFeatherTransitionMode));
      element.Projection = (Projection) new PlaneProjection()
      {
        CenterOfRotationX = 0.0
      };
      return (ITransition) new FeatheredTransition(element, new Storyboard(), turnstileFeatherTransitionMode, beginTime);
    }

    /// <summary>
    /// Adjusts the rotate transition mode based on the <paramref name="element" />'s FlowDirection.
    /// </summary>
    /// <param name="element">The <see cref="T:System.Windows.UIElement" />.</param>
    /// <param name="rotateTransitionMode">The transition mode.</param>
    /// <returns>Returns the adjusted rotate transition mode.</returns>
    private static RotateTransitionMode AdjustRotateTransitionModeForFlowDirection(
      UIElement element,
      RotateTransitionMode rotateTransitionMode)
    {
      FrameworkElement frameworkElement = element as FrameworkElement;
      RotateTransitionMode rotateTransitionMode1 = rotateTransitionMode;
      if (frameworkElement != null && frameworkElement.FlowDirection == 1)
      {
        switch (rotateTransitionMode)
        {
          case RotateTransitionMode.In90Clockwise:
            rotateTransitionMode1 = RotateTransitionMode.In90Counterclockwise;
            break;
          case RotateTransitionMode.In90Counterclockwise:
            rotateTransitionMode1 = RotateTransitionMode.In90Clockwise;
            break;
          case RotateTransitionMode.In180Clockwise:
            rotateTransitionMode1 = RotateTransitionMode.In180Counterclockwise;
            break;
          case RotateTransitionMode.In180Counterclockwise:
            rotateTransitionMode1 = RotateTransitionMode.In180Clockwise;
            break;
          case RotateTransitionMode.Out90Clockwise:
            rotateTransitionMode1 = RotateTransitionMode.Out90Counterclockwise;
            break;
          case RotateTransitionMode.Out90Counterclockwise:
            rotateTransitionMode1 = RotateTransitionMode.Out90Clockwise;
            break;
          case RotateTransitionMode.Out180Clockwise:
            rotateTransitionMode1 = RotateTransitionMode.Out180Counterclockwise;
            break;
          case RotateTransitionMode.Out180Counterclockwise:
            rotateTransitionMode1 = RotateTransitionMode.Out180Clockwise;
            break;
        }
      }
      return rotateTransitionMode1;
    }
  }
}
