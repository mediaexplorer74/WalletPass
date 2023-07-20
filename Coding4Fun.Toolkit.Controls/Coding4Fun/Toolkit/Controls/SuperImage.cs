// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.SuperImage
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  [TemplatePart(Name = "PrimaryImage", Type = typeof (Image))]
  [TemplatePart(Name = "PlaceholderBorder", Type = typeof (Border))]
  public class SuperImage : Control
  {
    public const string PrimaryImage = "PrimaryImage";
    public const string PlaceholderBorder = "PlaceholderBorder";
    private Image _primaryImage;
    private Border _placeholderBorder;
    private bool _isPrimaryImageLoaded;
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (SuperImage), new PropertyMetadata((object) (Stretch) 0));
    public static readonly DependencyProperty SourcesProperty = DependencyProperty.Register(nameof (Sources), typeof (ObservableCollection<SuperImageSource>), typeof (SuperImage), new PropertyMetadata((object) null, new PropertyChangedCallback(SuperImage.OnSourcesChanged)));
    public static readonly DependencyProperty PlaceholderImageSourceProperty = DependencyProperty.Register(nameof (PlaceholderImageSource), typeof (ImageSource), typeof (SuperImage), new PropertyMetadata((object) null));
    public static readonly DependencyProperty PlaceholderOpacityProperty = DependencyProperty.Register(nameof (PlaceholderOpacity), typeof (double), typeof (SuperImage), new PropertyMetadata((object) 1.0));
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof (Source), typeof (ImageSource), typeof (SuperImage), new PropertyMetadata((object) null, new PropertyChangedCallback(SuperImage.OnSourceChanged)));
    public static readonly DependencyProperty PlaceholderBackgroundProperty = DependencyProperty.Register(nameof (PlaceholderBackground), typeof (SolidColorBrush), typeof (SuperImage), new PropertyMetadata((object) null));
    public static readonly DependencyProperty PlaceholderImageStretchProperty = DependencyProperty.Register(nameof (PlaceholderImageStretch), typeof (Stretch), typeof (SuperImage), new PropertyMetadata((object) (Stretch) 0));

    public Stretch Stretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(SuperImage.StretchProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.StretchProperty, (object) value);
    }

    public ObservableCollection<SuperImageSource> Sources
    {
      get => (ObservableCollection<SuperImageSource>) ((DependencyObject) this).GetValue(SuperImage.SourcesProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.SourcesProperty, (object) value);
    }

    public ImageSource PlaceholderImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(SuperImage.PlaceholderImageSourceProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.PlaceholderImageSourceProperty, (object) value);
    }

    public double PlaceholderOpacity
    {
      get => (double) ((DependencyObject) this).GetValue(SuperImage.PlaceholderOpacityProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.PlaceholderOpacityProperty, (object) value);
    }

    public ImageSource Source
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(SuperImage.SourceProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.SourceProperty, (object) value);
    }

    public SolidColorBrush PlaceholderBackground
    {
      get => (SolidColorBrush) ((DependencyObject) this).GetValue(SuperImage.PlaceholderBackgroundProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.PlaceholderBackgroundProperty, (object) value);
    }

    public Stretch PlaceholderImageStretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(SuperImage.PlaceholderImageStretchProperty);
      set => ((DependencyObject) this).SetValue(SuperImage.PlaceholderImageStretchProperty, (object) value);
    }

    private static void OnSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      if (obj == null || e.NewValue == null || e.NewValue == e.OldValue || !(obj is SuperImage superImage) || superImage._primaryImage == null)
        return;
      superImage.OnSourcePropertyChanged();
    }

    private void OnSourcePropertyChanged()
    {
      this._isPrimaryImageLoaded = false;
      this.UpdatePlaceholderImageVisibility();
      this._primaryImage.Source = this.Source.ToBitmapImage();
    }

    private static void OnSourcesChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      if (obj == null || e.NewValue == null || e.NewValue == e.OldValue || !(obj is SuperImage superImage) || superImage._primaryImage == null)
        return;
      superImage.OnSourcesPropertyChanged();
    }

    private void OnSourcesPropertyChanged()
    {
      if (this.Source != null || !this.Sources.Any<SuperImageSource>())
        return;
      int scale = ApplicationSpace.ScaleFactor();
      SuperImageSource[] array = this.Sources.Where<SuperImageSource>((Func<SuperImageSource, bool>) (x =>
      {
        if (scale >= x.MinScale && x.MaxScale == 0 || x.MinScale == 0 && scale <= x.MaxScale)
          return true;
        return scale >= x.MinScale && scale <= x.MaxScale;
      })).ToArray<SuperImageSource>();
      SuperImageSource superImageSource;
      if (((IEnumerable<SuperImageSource>) array).Any<SuperImageSource>())
      {
        superImageSource = ((IEnumerable<SuperImageSource>) array).FirstOrDefault<SuperImageSource>((Func<SuperImageSource, bool>) (x => x.IsDefault)) ?? ((IEnumerable<SuperImageSource>) array).FirstOrDefault<SuperImageSource>();
      }
      else
      {
        superImageSource = this.Sources.FirstOrDefault<SuperImageSource>((Func<SuperImageSource, bool>) (x => x.IsDefault)) ?? this.Sources.FirstOrDefault<SuperImageSource>();
        if (superImageSource == null)
          return;
      }
      if (superImageSource == null)
        return;
      this._primaryImage.Source = superImageSource.Source.ToBitmapImage();
    }

    private void OnPrimaryImageOpened(object sender, RoutedEventArgs routedEventArgs)
    {
      this._isPrimaryImageLoaded = true;
      this.UpdatePlaceholderImageVisibility();
    }

    private void OnPrimaryImageFailed(object sender, RoutedEventArgs routedEventArgs)
    {
      this._isPrimaryImageLoaded = false;
      this.UpdatePlaceholderImageVisibility();
    }

    private void UpdatePlaceholderImageVisibility()
    {
      if (this._placeholderBorder == null)
        return;
      ((UIElement) this._placeholderBorder).Visibility = this._isPrimaryImageLoaded ? (Visibility) 1 : (Visibility) 0;
    }

    public SuperImage()
    {
      this.DefaultStyleKey = (object) typeof (SuperImage);
      this.Sources = new ObservableCollection<SuperImageSource>();
    }

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (this._primaryImage != null)
      {
        this._primaryImage.ImageOpened -= new EventHandler<RoutedEventArgs>(this.OnPrimaryImageOpened);
        this._primaryImage.ImageFailed -= new EventHandler<ExceptionRoutedEventArgs>(this.OnPrimaryImageFailed);
      }
      this._primaryImage = this.GetTemplateChild("PrimaryImage") as Image;
      this._placeholderBorder = this.GetTemplateChild("PlaceholderBorder") as Border;
      this._isPrimaryImageLoaded = false;
      if (this._primaryImage != null)
      {
        this._primaryImage.ImageOpened += new EventHandler<RoutedEventArgs>(this.OnPrimaryImageOpened);
        this._primaryImage.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(this.OnPrimaryImageFailed);
      }
      if (this.Source != null)
        this.OnSourcePropertyChanged();
      else
        this.OnSourcesPropertyChanged();
    }
  }
}
