// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ActionPopUp`2
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class ActionPopUp<T, TPopUpResult> : PopUp<T, TPopUpResult>
  {
    private const string ActionButtonAreaName = "actionButtonArea";
    protected Panel ActionButtonArea;
    public readonly DependencyProperty ActionPopUpButtonsProperty = DependencyProperty.Register(nameof (ActionPopUpButtons), typeof (List<Button>), typeof (ActionPopUp<T, TPopUpResult>), new PropertyMetadata((object) new List<Button>(), new PropertyChangedCallback(ActionPopUp<T, TPopUpResult>.OnActionPopUpButtonsChanged)));

    public override void OnApplyTemplate()
    {
      this.Focus();
      base.OnApplyTemplate();
      this.ActionButtonArea = this.GetTemplateChild("actionButtonArea") as Panel;
      this.SetButtons();
    }

    private void SetButtons()
    {
      if (this.ActionButtonArea == null)
        return;
      ((PresentationFrameworkCollection<UIElement>) this.ActionButtonArea.Children).Clear();
      bool flag = false;
      foreach (Button actionPopUpButton in this.ActionPopUpButtons)
      {
        ((PresentationFrameworkCollection<UIElement>) this.ActionButtonArea.Children).Add((UIElement) actionPopUpButton);
        flag |= ((ContentControl) actionPopUpButton).Content != null;
      }
      if (!flag)
        return;
      ((FrameworkElement) this.ActionButtonArea).Margin = new Thickness();
    }

    private static void OnActionPopUpButtonsChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      ActionPopUp<T, TPopUpResult> actionPopUp = (ActionPopUp<T, TPopUpResult>) o;
      if (actionPopUp == null || e.NewValue == e.OldValue)
        return;
      actionPopUp.SetButtons();
    }

    public List<Button> ActionPopUpButtons
    {
      get => (List<Button>) ((DependencyObject) this).GetValue(this.ActionPopUpButtonsProperty);
      set => ((DependencyObject) this).SetValue(this.ActionPopUpButtonsProperty, (object) value);
    }
  }
}
