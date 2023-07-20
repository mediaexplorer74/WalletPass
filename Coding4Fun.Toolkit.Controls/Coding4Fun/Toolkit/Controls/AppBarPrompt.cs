// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.AppBarPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Clarity.Phone.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public class AppBarPrompt : PopUp<string, PopUpResult>
  {
    private const string BodyName = "Body";
    protected StackPanel Body;
    private static readonly Color NullColor = Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
    private readonly AppBarPromptAction[] _theActions;

    public AppBarPrompt()
    {
      this.DefaultStyleKey = (object) typeof (AppBarPrompt);
      this.MainBodyDelay = TimeSpan.FromMilliseconds(100.0);
    }

    public AppBarPrompt(params AppBarPromptAction[] actions)
      : this()
    {
      this.AnimationType = DialogService.AnimationTypes.Swivel;
      this._theActions = actions;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.VerifyAppBarBackgroundColor();
      this.Body = this.GetTemplateChild("Body") as StackPanel;
      if (this.Body == null)
        return;
      foreach (AppBarPromptAction theAction in this._theActions)
      {
        theAction.Parent = this;
        AppBarPromptItem appBarPromptItem = new AppBarPromptItem();
        ((ContentControl) appBarPromptItem).Content = theAction.Content;
        ((ButtonBase) appBarPromptItem).Command = theAction.Command;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) this.Body).Children).Add((UIElement) appBarPromptItem);
      }
    }

    private void VerifyAppBarBackgroundColor()
    {
      Color backgroundColor = this.PopUpService.Page.ApplicationBar.BackgroundColor;
      if (!Color.op_Inequality(backgroundColor, AppBarPrompt.NullColor))
        return;
      this.Background = (Brush) new SolidColorBrush(backgroundColor);
    }
  }
}
