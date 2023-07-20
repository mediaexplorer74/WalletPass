// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.PasswordInputPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Binding;
using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class PasswordInputPrompt : InputPrompt
  {
    private readonly StringBuilder _inputText = new StringBuilder();
    private DateTime _lastUpdated = DateTime.Now;
    public static readonly DependencyProperty PasswordCharProperty = DependencyProperty.Register(nameof (PasswordChar), typeof (char), typeof (PasswordInputPrompt), new PropertyMetadata((object) '●'));

    public PasswordInputPrompt() => this.DefaultStyleKey = (object) typeof (PasswordInputPrompt);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (this.InputBox == null)
        return;
      ((FrameworkElement) this).SetBinding(UserPrompt.ValueProperty, new System.Windows.Data.Binding()
      {
        Source = (object) this.InputBox,
        Path = new PropertyPath("Text", new object[0])
      });
      TextBinding.SetUpdateSourceOnChange((DependencyObject) this.InputBox, true);
      this.InputBox.TextChanged -= new TextChangedEventHandler(this.InputBoxTextChanged);
      this.InputBox.SelectionChanged -= new RoutedEventHandler(this.InputBoxSelectionChanged);
      this.InputBox.TextChanged += new TextChangedEventHandler(this.InputBoxTextChanged);
      this.InputBox.SelectionChanged += new RoutedEventHandler(this.InputBoxSelectionChanged);
    }

    private void InputBoxSelectionChanged(object sender, RoutedEventArgs e)
    {
      if (this.InputBox.SelectionLength <= 0)
        return;
      this.InputBox.SelectionLength = 0;
    }

    private async void InputBoxTextChanged(object sender, TextChangedEventArgs e)
    {
      int diff = this.InputBox.Text.Length - this._inputText.Length;
      if (diff < 0)
      {
        diff *= -1;
        int startIndex = this.InputBox.SelectionStart + 1 - diff;
        if (startIndex < 0)
          startIndex = 0;
        this._inputText.Remove(startIndex, diff);
        this.Value = this._inputText.ToString();
      }
      else
      {
        if (diff <= 0)
          return;
        int selectionStart = this.InputBox.SelectionStart;
        int selectionIndex = selectionStart - diff;
        string newChars = this.InputBox.Text.Substring(selectionIndex, diff);
        this._inputText.Insert(selectionIndex, newChars);
        this.Value = this._inputText.ToString();
        if (diff > 1)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Insert(0, this.PasswordChar.ToString(), this.InputBox.Text.Length);
          this.InputBox.Text = stringBuilder.ToString();
        }
        else
        {
          if (this.InputBox.Text.Length >= 2)
          {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Insert(0, this.PasswordChar.ToString(), this.InputBox.Text.Length - diff);
            stringBuilder.Insert(selectionIndex, newChars);
            this.InputBox.Text = stringBuilder.ToString();
          }
          await this.ExecuteDelayedOverwrite();
          this._lastUpdated = DateTime.Now;
        }
        this.InputBox.SelectionStart = selectionStart;
      }
    }

    private async Task ExecuteDelayedOverwrite() => await Task.Run((Func<Task>) (async () =>
    {
      TimeSpan delay = TimeSpan.FromMilliseconds(500.0);
      await Task.Delay(delay);
      if (DateTime.Now - this._lastUpdated < TimeSpan.FromMilliseconds(500.0))
        return;
      ApplicationSpace.CurrentDispatcher.BeginInvoke((Action) (() =>
      {
        int selectionStart = this.InputBox.SelectionStart;
        this.InputBox.Text = Regex.Replace(this.InputBox.Text, ".", this.PasswordChar.ToString());
        this.InputBox.SelectionStart = selectionStart;
      }));
    }));

    public char PasswordChar
    {
      get => (char) ((DependencyObject) this).GetValue(PasswordInputPrompt.PasswordCharProperty);
      set => ((DependencyObject) this).SetValue(PasswordInputPrompt.PasswordCharProperty, (object) value);
    }
  }
}
