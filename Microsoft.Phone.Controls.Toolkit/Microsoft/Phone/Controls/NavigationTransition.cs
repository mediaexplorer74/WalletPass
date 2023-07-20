// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.NavigationTransition
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Windows;

namespace Microsoft.Phone.Controls
{
  /// <summary>
  /// Has
  /// <see cref="T:Microsoft.Phone.Controls.TransitionElement" />s
  /// for the designer experiences.
  /// </summary>
  public class NavigationTransition : DependencyObject
  {
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the backward
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.
    /// </summary>
    public static readonly DependencyProperty BackwardProperty = DependencyProperty.Register(nameof (Backward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);
    /// <summary>
    /// The
    /// <see cref="T:System.Windows.DependencyProperty" />
    /// for the forward
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.
    /// </summary>
    public static readonly DependencyProperty ForwardProperty = DependencyProperty.Register(nameof (Forward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);

    /// <summary>The navigation transition will begin.</summary>
    public event RoutedEventHandler BeginTransition;

    /// <summary>The navigation transition has ended.</summary>
    public event RoutedEventHandler EndTransition;

    /// <summary>
    /// Gets or sets the backward
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.
    /// </summary>
    public TransitionElement Backward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.BackwardProperty);
      set => this.SetValue(NavigationTransition.BackwardProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the forward
    /// <see cref="T:Microsoft.Phone.Controls.NavigationTransition" />.
    /// </summary>
    public TransitionElement Forward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.ForwardProperty);
      set => this.SetValue(NavigationTransition.ForwardProperty, (object) value);
    }

    /// <summary>
    /// Triggers <see cref="E:Microsoft.Phone.Controls.NavigationTransition.BeginTransition" />.
    /// </summary>
    internal void OnBeginTransition()
    {
      if (this.BeginTransition == null)
        return;
      this.BeginTransition((object) this, new RoutedEventArgs());
    }

    /// <summary>
    /// Triggers <see cref="E:Microsoft.Phone.Controls.NavigationTransition.EndTransition" />.
    /// </summary>
    internal void OnEndTransition()
    {
      if (this.EndTransition == null)
        return;
      this.EndTransition((object) this, new RoutedEventArgs());
    }
  }
}
