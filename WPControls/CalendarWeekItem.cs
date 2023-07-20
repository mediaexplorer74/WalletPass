// Decompiled with JetBrains decompiler
// Type: WPControls.CalendarWeekItem
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System.Windows;
using System.Windows.Controls;

namespace WPControls
{
  public class CalendarWeekItem : Control
  {
    public static readonly DependencyProperty WeekNumberProperty = DependencyProperty.Register(nameof (WeekNumber), typeof (int), typeof (CalendarWeekItem), new PropertyMetadata((PropertyChangedCallback) null));

    public CalendarWeekItem() => this.DefaultStyleKey = (object) typeof (CalendarWeekItem);

    public int? WeekNumber
    {
      get => new int?((int) ((DependencyObject) this).GetValue(CalendarWeekItem.WeekNumberProperty));
      internal set => ((DependencyObject) this).SetValue(CalendarWeekItem.WeekNumberProperty, (object) value);
    }
  }
}
