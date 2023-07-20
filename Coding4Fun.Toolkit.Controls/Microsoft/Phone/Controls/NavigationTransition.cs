// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.NavigationTransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

namespace Microsoft.Phone.Controls
{
  internal class NavigationTransition : DependencyObject
  {
    public static readonly DependencyProperty BackwardProperty = DependencyProperty.Register(nameof (Backward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);
    public static readonly DependencyProperty ForwardProperty = DependencyProperty.Register(nameof (Forward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);

    public event RoutedEventHandler BeginTransition;

    public event RoutedEventHandler EndTransition;

    public TransitionElement Backward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.BackwardProperty);
      set => this.SetValue(NavigationTransition.BackwardProperty, (object) value);
    }

    public TransitionElement Forward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.ForwardProperty);
      set => this.SetValue(NavigationTransition.ForwardProperty, (object) value);
    }

    internal void OnBeginTransition()
    {
      if (this.BeginTransition == null)
        return;
      this.BeginTransition((object) this, new RoutedEventArgs());
    }

    internal void OnEndTransition()
    {
      if (this.EndTransition == null)
        return;
      this.EndTransition((object) this, new RoutedEventArgs());
    }
  }
}
