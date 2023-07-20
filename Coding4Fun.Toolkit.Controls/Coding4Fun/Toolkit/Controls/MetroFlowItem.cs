// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.MetroFlowItem
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  [ContentProperty("Title")]
  public class MetroFlowItem : Control
  {
    private const int DefaultStartIndex = 1;
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (MetroFlowItem), new PropertyMetadata(new PropertyChangedCallback(MetroFlowItem.OnImageSourceChanged)));
    public static readonly DependencyProperty ImageVisibilityProperty = DependencyProperty.Register(nameof (ImageVisibility), typeof (Visibility), typeof (MetroFlowItem), new PropertyMetadata((object) (Visibility) 0));
    public static readonly DependencyProperty ImageOpacityProperty = DependencyProperty.Register(nameof (ImageOpacity), typeof (double), typeof (MetroFlowItem), new PropertyMetadata((object) 1.0));
    public static readonly DependencyProperty ItemIndexStringProperty = DependencyProperty.Register(nameof (ItemIndexString), typeof (string), typeof (MetroFlowItem), new PropertyMetadata((object) 1.ToString()));
    public static readonly DependencyProperty ItemIndexProperty = DependencyProperty.Register(nameof (ItemIndex), typeof (int), typeof (MetroFlowItem), new PropertyMetadata((object) 1));
    public static readonly DependencyProperty ItemIndexVisibilityProperty = DependencyProperty.Register(nameof (ItemIndexVisibility), typeof (Visibility), typeof (MetroFlowItem), new PropertyMetadata((object) (Visibility) 1));
    public static readonly DependencyProperty ItemIndexOpacityProperty = DependencyProperty.Register(nameof (ItemIndexOpacity), typeof (double), typeof (MetroFlowItem), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (MetroFlowItem), new PropertyMetadata((object) "Lorem ipsum dolor sit amet"));
    public static readonly DependencyProperty TitleVisibilityProperty = DependencyProperty.Register(nameof (TitleVisibility), typeof (Visibility), typeof (MetroFlowItem), new PropertyMetadata((object) (Visibility) 0));
    public static readonly DependencyProperty TitleOpacityProperty = DependencyProperty.Register(nameof (TitleOpacity), typeof (double), typeof (MetroFlowItem), new PropertyMetadata((object) 1.0));

    public MetroFlowItem() => this.DefaultStyleKey = (object) typeof (MetroFlowItem);

    public ImageSource ImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(MetroFlowItem.ImageSourceProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.ImageSourceProperty, (object) value);
    }

    private static void OnImageSourceChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is MetroFlowItem metroFlowItem))
        return;
      ((UIElement) metroFlowItem).UpdateLayout();
    }

    public Visibility ImageVisibility
    {
      get => (Visibility) ((DependencyObject) this).GetValue(MetroFlowItem.ImageVisibilityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.ImageVisibilityProperty, (object) value);
    }

    public double ImageOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(MetroFlowItem.ImageOpacityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.ImageOpacityProperty, (object) value);
    }

    public string ItemIndexString
    {
      get => (string) ((DependencyObject) this).GetValue(MetroFlowItem.ItemIndexStringProperty);
      private set => ((DependencyObject) this).SetValue(MetroFlowItem.ItemIndexStringProperty, (object) value);
    }

    public int ItemIndex
    {
      get => (int) ((DependencyObject) this).GetValue(MetroFlowItem.ItemIndexProperty);
      set
      {
        ((DependencyObject) this).SetValue(MetroFlowItem.ItemIndexProperty, (object) value);
        this.ItemIndexString = this.ItemIndex.ToString();
      }
    }

    public Visibility ItemIndexVisibility
    {
      get => (Visibility) ((DependencyObject) this).GetValue(MetroFlowItem.ItemIndexVisibilityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.ItemIndexVisibilityProperty, (object) value);
    }

    public double ItemIndexOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(MetroFlowItem.ItemIndexOpacityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.ItemIndexOpacityProperty, (object) value);
    }

    public string Title
    {
      get => (string) ((DependencyObject) this).GetValue(MetroFlowItem.TitleProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.TitleProperty, (object) value);
    }

    public Visibility TitleVisibility
    {
      get => (Visibility) ((DependencyObject) this).GetValue(MetroFlowItem.TitleVisibilityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.TitleVisibilityProperty, (object) value);
    }

    public double TitleOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(MetroFlowItem.TitleOpacityProperty);
      set => ((DependencyObject) this).SetValue(MetroFlowItem.TitleOpacityProperty, (object) value);
    }
  }
}
