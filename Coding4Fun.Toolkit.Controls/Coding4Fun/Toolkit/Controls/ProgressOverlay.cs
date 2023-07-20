// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ProgressOverlay
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Coding4Fun.Toolkit.Controls
{
  public class ProgressOverlay : ContentControl
  {
    private const string FadeInName = "FadeInStoryboard";
    private const string FadeOutName = "FadeOutStoryboard";
    private const string LayoutGridName = "LayoutGrid";
    private Storyboard _fadeIn;
    private Storyboard _fadeOut;
    private Grid _layoutGrid;
    public static readonly DependencyProperty ProgressControlProperty = DependencyProperty.Register(nameof (ProgressControl), typeof (object), typeof (ProgressOverlay), new PropertyMetadata((PropertyChangedCallback) null));

    public ProgressOverlay() => ((Control) this).DefaultStyleKey = (object) typeof (ProgressOverlay);

    public object ProgressControl
    {
      get => ((DependencyObject) this).GetValue(ProgressOverlay.ProgressControlProperty);
      set => ((DependencyObject) this).SetValue(ProgressOverlay.ProgressControlProperty, value);
    }

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      this._fadeIn = ((Control) this).GetTemplateChild("FadeInStoryboard") as Storyboard;
      this._fadeOut = ((Control) this).GetTemplateChild("FadeOutStoryboard") as Storyboard;
      this._layoutGrid = ((Control) this).GetTemplateChild("LayoutGrid") as Grid;
      if (this._fadeOut == null)
        return;
      ((Timeline) this._fadeOut).Completed += new EventHandler(this.FadeOutCompleted);
    }

    private void FadeOutCompleted(object sender, EventArgs e)
    {
      ((UIElement) this._layoutGrid).Opacity = 1.0;
      ((UIElement) this).Visibility = (Visibility) 1;
    }

    public void Show()
    {
      if (this._fadeIn == null)
        ((Control) this).ApplyTemplate();
      ((UIElement) this).Visibility = (Visibility) 0;
      if (this._fadeOut != null)
        this._fadeOut.Stop();
      if (this._fadeIn == null)
        return;
      this._fadeIn.Begin();
    }

    public void Hide()
    {
      if (this._fadeOut == null)
        ((Control) this).ApplyTemplate();
      if (this._fadeIn != null)
        this._fadeIn.Stop();
      if (this._fadeOut == null)
        return;
      this._fadeOut.Begin();
    }
  }
}
