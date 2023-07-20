// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ToggleButtonBase
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  public abstract class ToggleButtonBase : CheckBox, IImageSource, IButtonBase, IAppBarButton
  {
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (ToggleButtonBase), new PropertyMetadata((object) (Stretch) 0, new PropertyChangedCallback(ToggleButtonBase.OnUpdate)));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ToggleButtonBase), new PropertyMetadata((object) null, new PropertyChangedCallback(ToggleButtonBase.OnUpdate)));
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof (Label), typeof (object), typeof (ToggleButtonBase), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty CheckedBrushProperty = DependencyProperty.Register(nameof (CheckedBrush), typeof (Brush), typeof (ToggleButtonBase), new PropertyMetadata((object) null));
    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (ToggleButtonBase), new PropertyMetadata((object) (Orientation) 0));
    public static readonly DependencyProperty ButtonWidthProperty = DependencyProperty.Register(nameof (ButtonWidth), typeof (double), typeof (ToggleButtonBase), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty ButtonHeightProperty = DependencyProperty.Register(nameof (ButtonHeight), typeof (double), typeof (ToggleButtonBase), new PropertyMetadata((object) double.NaN));

    private bool IsContentEmpty(object content) => content == null && this.ImageSource == null;

    private void ApplyingTemplate() => this.UpdateImageSource();

    public Stretch Stretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(ToggleButtonBase.StretchProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.StretchProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(ToggleButtonBase.ImageSourceProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.ImageSourceProperty, (object) value);
    }

    private static void OnUpdate(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is ToggleButtonBase toggleButtonBase))
        return;
      toggleButtonBase.UpdateImageSource();
    }

    private void UpdateImageSource()
    {
      Grid templateChild1 = ((Control) this).GetTemplateChild("ContentHost") as Grid;
      Grid templateChild2 = ((Control) this).GetTemplateChild("DisabledContentHost") as Grid;
      FrameworkElement templateChild3 = ((Control) this).GetTemplateChild("EnabledContent") as FrameworkElement;
      FrameworkElement templateChild4 = ((Control) this).GetTemplateChild("DisabledContent") as FrameworkElement;
      ButtonBaseHelper.UpdateImageSource(templateChild3, templateChild1, this.ImageSource, this.Stretch);
      ButtonBaseHelper.UpdateImageSource(templateChild4, templateChild2, this.ImageSource, this.Stretch);
    }

    protected ToggleButtonBase() => ((Control) this).IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.IsEnabledStateChanged);

    private void IsEnabledStateChanged(object sender, DependencyPropertyChangedEventArgs e) => this.IsEnabledStateChanged();

    private void IsEnabledStateChanged()
    {
      ContentControl templateChild1 = ((Control) this).GetTemplateChild("ContentBody") as ContentControl;
      Grid templateChild2 = ((Control) this).GetTemplateChild("EnabledHolder") as Grid;
      Grid templateChild3 = ((Control) this).GetTemplateChild("DisabledHolder") as Grid;
      if (templateChild1 != null && templateChild3 != null && templateChild2 != null)
      {
        if (!((Control) this).IsEnabled)
          ((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild2).Children).Remove((UIElement) templateChild1);
        else
          ((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild3).Children).Remove((UIElement) templateChild1);
        if (((Control) this).IsEnabled)
        {
          if (!((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild2).Children).Contains((UIElement) templateChild1))
            ((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild2).Children).Insert(0, (UIElement) templateChild1);
        }
        else if (!((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild3).Children).Contains((UIElement) templateChild1))
          ((PresentationFrameworkCollection<UIElement>) ((Panel) templateChild3).Children).Insert(0, (UIElement) templateChild1);
      }
      ((UIElement) this).UpdateLayout();
      ButtonBaseHelper.ApplyForegroundToFillBinding(templateChild1);
    }

    protected virtual void OnContentChanged(object oldContent, object newContent)
    {
      ((ToggleButton) this).OnContentChanged(oldContent, newContent);
      if (oldContent == newContent)
        return;
      this.AppendCheck(((ContentControl) this).Content);
      this.IsEnabledStateChanged();
    }

    private void AppendCheck(object content)
    {
      if (!this.IsContentEmpty(content))
        return;
      ((ContentControl) this).Content = (object) ButtonBaseHelper.CreateXamlCheck((FrameworkElement) this);
    }

    public virtual void OnApplyTemplate()
    {
      ((ToggleButton) this).OnApplyTemplate();
      this.ApplyingTemplate();
      this.AppendCheck(((ContentControl) this).Content);
      this.IsEnabledStateChanged();
      ButtonBaseHelper.ApplyTitleOffset(((Control) this).GetTemplateChild("ContentTitle") as ContentControl);
    }

    public object Label
    {
      get => ((DependencyObject) this).GetValue(ToggleButtonBase.LabelProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.LabelProperty, value);
    }

    public Brush CheckedBrush
    {
      get => (Brush) ((DependencyObject) this).GetValue(ToggleButtonBase.CheckedBrushProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.CheckedBrushProperty, (object) value);
    }

    public Orientation Orientation
    {
      get => (Orientation) ((DependencyObject) this).GetValue(ToggleButtonBase.OrientationProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.OrientationProperty, (object) value);
    }

    public double ButtonWidth
    {
      get => (double) ((DependencyObject) this).GetValue(ToggleButtonBase.ButtonWidthProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.ButtonWidthProperty, (object) value);
    }

    public double ButtonHeight
    {
      get => (double) ((DependencyObject) this).GetValue(ToggleButtonBase.ButtonHeightProperty);
      set => ((DependencyObject) this).SetValue(ToggleButtonBase.ButtonHeightProperty, (object) value);
    }
  }
}
