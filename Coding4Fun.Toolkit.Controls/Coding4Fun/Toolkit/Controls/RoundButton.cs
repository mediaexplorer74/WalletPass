// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.RoundButton
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public class RoundButton : ButtonBase, IImageSource, IAppBarButton
  {
    private Grid _hostContainer;
    private FrameworkElement _contentBody;
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (RoundButton), new PropertyMetadata((object) (Stretch) 0, new PropertyChangedCallback(RoundButton.OnUpdate)));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (RoundButton), new PropertyMetadata((object) null, new PropertyChangedCallback(RoundButton.OnUpdate)));
    public static readonly DependencyProperty PressedBrushProperty = DependencyProperty.Register(nameof (PressedBrush), typeof (Brush), typeof (RoundButton), new PropertyMetadata((object) null));
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (RoundButton), new PropertyMetadata((object) (Orientation) 0));
    public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register(nameof (ButtonWidth), typeof (double), typeof (RoundButton), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty ButtonHeightProperty = DependencyProperty.Register(nameof (ButtonHeight), typeof (double), typeof (RoundButton), new PropertyMetadata((object) double.NaN));

    private bool IsContentEmpty(object content) => content == null && this.ImageSource == null;

    private void ApplyingTemplate()
    {
      this._hostContainer = ((Control) this).GetTemplateChild("ContentHost") as Grid;
      this._contentBody = ((Control) this).GetTemplateChild("ContentBody") as FrameworkElement;
      ButtonBaseHelper.UpdateImageSource(this._contentBody, this._hostContainer, this.ImageSource, this.Stretch);
    }

    public Stretch Stretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(RoundButton.StretchProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.StretchProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(RoundButton.ImageSourceProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.ImageSourceProperty, (object) value);
    }

    private static void OnUpdate(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is RoundButton roundButton))
        return;
      roundButton.UpdateImageSource();
    }

    private void UpdateImageSource() => ButtonBaseHelper.UpdateImageSource(this._contentBody, this._hostContainer, this.ImageSource, this.Stretch);

    public RoundButton() => ((Control) this).DefaultStyleKey = (object) typeof (RoundButton);

    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      ((ContentControl) this).OnContentChanged(oldContent, newContent);
      if (oldContent == newContent)
        return;
      this.AppendCheck(((ContentControl) this).Content);
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ButtonBaseHelper.ApplyForegroundToFillBinding(((Control) this).GetTemplateChild("ContentBody") as ContentControl)));
    }

    private void AppendCheck(object content)
    {
      if (!this.IsContentEmpty(content))
        return;
      ((ContentControl) this).Content = (object) ButtonBaseHelper.CreateXamlCheck((FrameworkElement) this);
    }

    public override void OnApplyTemplate()
    {
      this.ApplyingTemplate();
      this.AppendCheck(((ContentControl) this).Content);
      ButtonBaseHelper.ApplyForegroundToFillBinding(((Control) this).GetTemplateChild("ContentBody") as ContentControl);
      ButtonBaseHelper.ApplyTitleOffset(((Control) this).GetTemplateChild("ContentTitle") as ContentControl);
      base.OnApplyTemplate();
    }

    public Brush PressedBrush
    {
      get => (Brush) ((DependencyObject) this).GetValue(RoundButton.PressedBrushProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.PressedBrushProperty, (object) value);
    }

    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(RoundButton.OrientationProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.OrientationProperty, (object) value);
    }

    public double ButtonWidth
    {
      get => (double) ((DependencyObject) this).GetValue(RoundButton.ButtonWidthProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.ButtonWidthProperty, (object) value);
    }

    public double ButtonHeight
    {
      get => (double) ((DependencyObject) this).GetValue(RoundButton.ButtonHeightProperty);
      set => ((DependencyObject) this).SetValue(RoundButton.ButtonHeightProperty, (object) value);
    }
  }
}
