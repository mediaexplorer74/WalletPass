﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ChatBubbleTextBox
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class ChatBubbleTextBox : TextBox
  {
    private const string HintContentElementName = "HintContentElement";
    protected ContentControl HintContentElement;
    private bool _hasFocus;
    public static readonly DependencyProperty ChatBubbleDirectionProperty = DependencyProperty.Register(nameof (ChatBubbleDirection), typeof (ChatBubbleDirection), typeof (ChatBubbleTextBox), new PropertyMetadata((object) ChatBubbleDirection.UpperRight, new PropertyChangedCallback(ChatBubbleTextBox.OnChatBubbleDirectionChanged)));
    public static readonly DependencyProperty HintProperty = DependencyProperty.Register(nameof (Hint), typeof (string), typeof (ChatBubbleTextBox), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty HintStyleProperty = DependencyProperty.Register(nameof (HintStyle), typeof (Style), typeof (ChatBubbleTextBox), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty IsEquallySpacedProperty = DependencyProperty.Register(nameof (IsEquallySpaced), typeof (bool), typeof (ChatBubbleTextBox), new PropertyMetadata((object) true, new PropertyChangedCallback(ChatBubbleTextBox.OnIsEquallySpacedChanged)));
    private static bool _triggered = false;

    public ChatBubbleTextBox()
    {
      ((Control) this).DefaultStyleKey = (object) typeof (ChatBubbleTextBox);
      this.TextChanged += new TextChangedEventHandler(this.ChatBubbleTextBoxTextChanged);
    }

    public ChatBubbleDirection ChatBubbleDirection
    {
      get => (ChatBubbleDirection) ((DependencyObject) this).GetValue(ChatBubbleTextBox.ChatBubbleDirectionProperty);
      set => ((DependencyObject) this).SetValue(ChatBubbleTextBox.ChatBubbleDirectionProperty, (object) value);
    }

    public string Hint
    {
      get => (string) ((DependencyObject) this).GetValue(ChatBubbleTextBox.HintProperty);
      set => ((DependencyObject) this).SetValue(ChatBubbleTextBox.HintProperty, (object) value);
    }

    public Style HintStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(ChatBubbleTextBox.HintStyleProperty);
      set => ((DependencyObject) this).SetValue(ChatBubbleTextBox.HintStyleProperty, (object) value);
    }

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this.HintContentElement = ((Control) this).GetTemplateChild("HintContentElement") as ContentControl;
      this.UpdateHintVisibility();
      this.UpdateChatBubbleDirection();
      this.UpdateIsEquallySpaced();
    }

    protected virtual void OnGotFocus(RoutedEventArgs e)
    {
      this._hasFocus = true;
      this.SetHintVisibility((Visibility) 1);
      base.OnGotFocus(e);
    }

    protected virtual void OnLostFocus(RoutedEventArgs e)
    {
      this._hasFocus = false;
      this.UpdateHintVisibility();
      base.OnLostFocus(e);
    }

    private void UpdateHintVisibility()
    {
      if (this._hasFocus)
        return;
      this.SetHintVisibility(string.IsNullOrEmpty(this.Text) ? (Visibility) 0 : (Visibility) 1);
    }

    private void SetHintVisibility(Visibility value)
    {
      if (this.HintContentElement == null)
        return;
      ((UIElement) this.HintContentElement).Visibility = value;
    }

    private static void OnChatBubbleDirectionChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ChatBubbleTextBox chatBubbleTextBox))
        return;
      chatBubbleTextBox.UpdateChatBubbleDirection();
    }

    private void UpdateChatBubbleDirection() => VisualStateManager.GoToState((Control) this, this.ChatBubbleDirection.ToString(), true);

    private void ChatBubbleTextBoxTextChanged(object sender, TextChangedEventArgs e) => this.UpdateHintVisibility();

    public bool IsEquallySpaced
    {
      get => (bool) ((DependencyObject) this).GetValue(ChatBubbleTextBox.IsEquallySpacedProperty);
      set => ((DependencyObject) this).SetValue(ChatBubbleTextBox.IsEquallySpacedProperty, (object) value);
    }

    private static void OnIsEquallySpacedChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ChatBubbleTextBox chatBubbleTextBox))
        return;
      ChatBubbleTextBox._triggered = true;
      chatBubbleTextBox.UpdateIsEquallySpaced();
    }

    private void UpdateIsEquallySpaced()
    {
      int num = this.IsEquallySpaced ? ControlHelper.MagicSpacingNumber : (ChatBubbleTextBox._triggered ? -1 * ControlHelper.MagicSpacingNumber : 0);
      Thickness margin = ((FrameworkElement) this).Margin;
      switch (this.ChatBubbleDirection)
      {
        case ChatBubbleDirection.UpperRight:
        case ChatBubbleDirection.UpperLeft:
          margin.Bottom += (double) num;
          break;
        case ChatBubbleDirection.LowerRight:
        case ChatBubbleDirection.LowerLeft:
          margin.Top += (double) num;
          break;
      }
      ((FrameworkElement) this).Margin = margin;
    }
  }
}
