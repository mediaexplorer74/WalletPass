// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RotateTransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

namespace Microsoft.Phone.Controls
{
  internal class RotateTransition : TransitionElement
  {
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (RotateTransitionMode), typeof (RotateTransition), (PropertyMetadata) null);

    public RotateTransitionMode Mode
    {
      get => (RotateTransitionMode) this.GetValue(RotateTransition.ModeProperty);
      set => this.SetValue(RotateTransition.ModeProperty, (object) value);
    }

    public override ITransition GetTransition(UIElement element) => Transitions.Rotate(element, this.Mode);
  }
}
