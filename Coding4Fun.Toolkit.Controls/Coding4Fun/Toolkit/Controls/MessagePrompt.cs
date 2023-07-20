// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MessagePrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace Coding4Fun.Toolkit.Controls
{
  public class MessagePrompt : UserPrompt
  {
    public static readonly DependencyProperty BodyProperty = DependencyProperty.Register(nameof (Body), typeof (object), typeof (MessagePrompt), new PropertyMetadata((PropertyChangedCallback) null));

    public MessagePrompt()
    {
      this.DefaultStyleKey = (object) typeof (MessagePrompt);
      this.MessageChanged = new Action(this.SetBodyMessage);
    }

    protected internal void SetBodyMessage() => this.Body = (object) new TextBlock()
    {
      Text = this.Message,
      TextWrapping = (TextWrapping) 2
    };

    public object Body
    {
      get => ((DependencyObject) this).GetValue(MessagePrompt.BodyProperty);
      set => ((DependencyObject) this).SetValue(MessagePrompt.BodyProperty, value);
    }
  }
}
