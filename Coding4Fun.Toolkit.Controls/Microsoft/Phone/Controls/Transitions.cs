// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.Transitions
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Microsoft.Phone.Controls
{
  internal static class Transitions
  {
    private static Dictionary<string, string> _storyboardXamlCache;

    private static ITransition GetEnumStoryboard<T>(UIElement element, string name, T mode)
    {
      Storyboard storyboard = Transitions.GetStoryboard(name + Enum.GetName(typeof (T), (object) mode));
      if (storyboard == null)
        return (ITransition) null;
      Storyboard.SetTarget((Timeline) storyboard, (DependencyObject) element);
      return (ITransition) new Transition(element, storyboard);
    }

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
      return Transitions.GetEnumStoryboard<RotateTransitionMode>(element, nameof (Rotate), rotateTransitionMode);
    }

    public static ITransition Slide(UIElement element, SlideTransitionMode slideTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (SlideTransitionMode), (object) slideTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (slideTransitionMode));
      element.RenderTransform = (Transform) new TranslateTransform();
      return Transitions.GetEnumStoryboard<SlideTransitionMode>(element, string.Empty, slideTransitionMode);
    }

    public static ITransition Swivel(UIElement element, SwivelTransitionMode swivelTransitionMode)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (!Enum.IsDefined(typeof (SwivelTransitionMode), (object) swivelTransitionMode))
        throw new ArgumentOutOfRangeException(nameof (swivelTransitionMode));
      element.Projection = (Projection) new PlaneProjection();
      return Transitions.GetEnumStoryboard<SwivelTransitionMode>(element, nameof (Swivel), swivelTransitionMode);
    }

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
  }
}
