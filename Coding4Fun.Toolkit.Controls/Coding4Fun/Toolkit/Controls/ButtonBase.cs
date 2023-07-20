// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ButtonBase
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public abstract class ButtonBase : Button, IButtonBase
  {
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof (Label), typeof (object), typeof (ButtonBase), new PropertyMetadata((object) string.Empty));

    public virtual void OnApplyTemplate() => base.OnApplyTemplate();

    public object Label
    {
      get => ((DependencyObject) this).GetValue(ButtonBase.LabelProperty);
      set => ((DependencyObject) this).SetValue(ButtonBase.LabelProperty, value);
    }
  }
}
