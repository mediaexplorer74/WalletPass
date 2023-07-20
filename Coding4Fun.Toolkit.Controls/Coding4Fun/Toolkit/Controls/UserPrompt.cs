// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.UserPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Coding4Fun.Toolkit.Controls
{
  public abstract class UserPrompt : ActionPopUp<string, PopUpResult>
  {
    private readonly RoundButton _cancelButton;
    protected internal Action MessageChanged;
    public readonly DependencyProperty IsCancelVisibileProperty = DependencyProperty.Register(nameof (IsCancelVisible), typeof (bool), typeof (UserPrompt), new PropertyMetadata((object) false, new PropertyChangedCallback(UserPrompt.OnCancelButtonVisibilityChanged)));
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (string), typeof (UserPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (UserPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (UserPrompt), new PropertyMetadata((object) "", new PropertyChangedCallback(UserPrompt.OnMesageContentChanged)));

    protected UserPrompt()
    {
      RoundButton roundButton = new RoundButton();
      this._cancelButton = new RoundButton();
      ((ButtonBase) roundButton).Click += new RoutedEventHandler(this.OkClick);
      ((ButtonBase) this._cancelButton).Click += new RoutedEventHandler(this.CancelledClick);
      this.ActionPopUpButtons.Add((Button) roundButton);
      this.ActionPopUpButtons.Add((Button) this._cancelButton);
      this.SetCancelButtonVisibility(this.IsCancelVisible);
    }

    public override void OnApplyTemplate()
    {
      ((ContentControl) this._cancelButton).Content = (object) ButtonBaseHelper.CreateXamlCancel((FrameworkElement) this._cancelButton);
      base.OnApplyTemplate();
    }

    private void OkClick(object sender, RoutedEventArgs e) => this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
    {
      Result = this.Value,
      PopUpResult = PopUpResult.Ok
    });

    private void CancelledClick(object sender, RoutedEventArgs e) => this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
    {
      PopUpResult = PopUpResult.Cancelled
    });

    private void SetCancelButtonVisibility(bool value) => ((UIElement) this._cancelButton).Visibility = value ? (Visibility) 0 : (Visibility) 1;

    private static void OnMesageContentChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      UserPrompt userPrompt = (UserPrompt) o;
      if (userPrompt == null || e.NewValue == e.OldValue || userPrompt.MessageChanged == null)
        return;
      userPrompt.MessageChanged();
    }

    private static void OnCancelButtonVisibilityChanged(
      DependencyObject o,
      DependencyPropertyChangedEventArgs e)
    {
      UserPrompt userPrompt = (UserPrompt) o;
      if (userPrompt == null || e.NewValue == e.OldValue)
        return;
      userPrompt.SetCancelButtonVisibility((bool) e.NewValue);
    }

    public bool IsCancelVisible
    {
      get => (bool) ((DependencyObject) this).GetValue(this.IsCancelVisibileProperty);
      set => ((DependencyObject) this).SetValue(this.IsCancelVisibileProperty, (object) value);
    }

    public string Value
    {
      get => (string) ((DependencyObject) this).GetValue(UserPrompt.ValueProperty);
      set => ((DependencyObject) this).SetValue(UserPrompt.ValueProperty, (object) value);
    }

    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(UserPrompt.TitleProperty);
      set => ((DependencyObject) this).SetValue(UserPrompt.TitleProperty, (object) value);
    }

    public string Message
    {
      get => (string) ((DependencyObject) this).GetValue(UserPrompt.MessageProperty);
      set => ((DependencyObject) this).SetValue(UserPrompt.MessageProperty, (object) value);
    }
  }
}
