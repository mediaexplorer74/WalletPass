// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.LockScreenPreview
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.9.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FE9E68FC-2303-41B7-81A5-6B9678322A1F
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coding4Fun.Toolkit.Controls
{
  [TemplatePart(Name = "TimeText", Type = typeof (TextBlock))]
  [TemplatePart(Name = "DateText", Type = typeof (TextBlock))]
  [TemplatePart(Name = "DayText", Type = typeof (TextBlock))]
  public class LockScreenPreview : ContentControl
  {
    public const string TimeText = "TimeText";
    public const string DayText = "DayText";
    public const string DateText = "DateText";
    public const string LockScreenImage = "LockScreenImage";
    public static readonly DependencyProperty LockScreenImageSourceProperty = DependencyProperty.Register(nameof (LockScreenImageSource), typeof (ImageSource), typeof (LockScreenPreview), new PropertyMetadata((object) null));
    public static readonly DependencyProperty TextLine1Property = DependencyProperty.Register(nameof (TextLine1), typeof (string), typeof (LockScreenPreview), new PropertyMetadata((object) null));
    public static readonly DependencyProperty TextLine2Property = DependencyProperty.Register(nameof (TextLine2), typeof (string), typeof (LockScreenPreview), new PropertyMetadata((object) null));
    public static readonly DependencyProperty TextLine3Property = DependencyProperty.Register(nameof (TextLine3), typeof (string), typeof (LockScreenPreview), new PropertyMetadata((object) null));
    public static readonly DependencyProperty NotificationIconSourceProperty = DependencyProperty.Register(nameof (NotificationIconSource), typeof (ImageSource), typeof (LockScreenPreview), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ShowNotificationCountProperty = DependencyProperty.Register(nameof (ShowNotificationCount), typeof (bool), typeof (LockScreenPreview), new PropertyMetadata((object) true));
    public static readonly DependencyProperty Support720Property = DependencyProperty.Register(nameof (Support720), typeof (bool), typeof (LockScreenPreview), new PropertyMetadata((object) false));

    public ImageSource LockScreenImageSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(LockScreenPreview.LockScreenImageSourceProperty);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.LockScreenImageSourceProperty, (object) value);
    }

    public string TextLine1
    {
      get => (string) ((DependencyObject) this).GetValue(LockScreenPreview.TextLine1Property);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.TextLine1Property, (object) value);
    }

    public string TextLine2
    {
      get => (string) ((DependencyObject) this).GetValue(LockScreenPreview.TextLine2Property);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.TextLine2Property, (object) value);
    }

    public string TextLine3
    {
      get => (string) ((DependencyObject) this).GetValue(LockScreenPreview.TextLine3Property);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.TextLine3Property, (object) value);
    }

    public ImageSource NotificationIconSource
    {
      get => (ImageSource) ((DependencyObject) this).GetValue(LockScreenPreview.NotificationIconSourceProperty);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.NotificationIconSourceProperty, (object) value);
    }

    public bool ShowNotificationCount
    {
      get => (bool) ((DependencyObject) this).GetValue(LockScreenPreview.ShowNotificationCountProperty);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.ShowNotificationCountProperty, (object) value);
    }

    public bool Support720
    {
      get => (bool) ((DependencyObject) this).GetValue(LockScreenPreview.Support720Property);
      set => ((DependencyObject) this).SetValue(LockScreenPreview.Support720Property, (object) value);
    }

    public LockScreenPreview() => ((Control) this).DefaultStyleKey = (object) typeof (LockScreenPreview);

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      DateTime now = DateTime.Now;
      CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
      if (((Control) this).GetTemplateChild("DateText") is TextBlock templateChild1)
        templateChild1.Text = now.ToString(currentUiCulture.DateTimeFormat.MonthDayPattern);
      if (((Control) this).GetTemplateChild("DayText") is TextBlock templateChild2)
        templateChild2.Text = now.DayOfWeek.ToString();
      if (!(((Control) this).GetTemplateChild("TimeText") is TextBlock templateChild3))
        return;
      templateChild3.Text = now.ToString(currentUiCulture.DateTimeFormat.ShortTimePattern);
      if (!string.IsNullOrEmpty(currentUiCulture.DateTimeFormat.AMDesignator))
        templateChild3.Text = templateChild3.Text.Replace(currentUiCulture.DateTimeFormat.AMDesignator, string.Empty);
      if (string.IsNullOrEmpty(currentUiCulture.DateTimeFormat.PMDesignator))
        return;
      templateChild3.Text = templateChild3.Text.Replace(currentUiCulture.DateTimeFormat.PMDesignator, string.Empty);
    }
  }
}
