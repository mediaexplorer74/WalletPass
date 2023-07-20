// Decompiled with JetBrains decompiler
// Type: WPControls.CalendarItem
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPControls
{
  public class CalendarItem : Button
  {
    private readonly Calendar _owningCalendar;
    public static readonly DependencyProperty DayNumberProperty = DependencyProperty.Register(nameof (DayNumber), typeof (int), typeof (CalendarItem), new PropertyMetadata((object) 0, new PropertyChangedCallback(CalendarItem.OnDayNumberChanged)));
    internal static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof (IsSelected), typeof (bool), typeof (CalendarItem), new PropertyMetadata((object) false, new PropertyChangedCallback(CalendarItem.OnIsSelectedChanged)));
    internal static readonly DependencyProperty ItemDateProperty = DependencyProperty.Register(nameof (ItemDate), typeof (DateTime), typeof (CalendarItem), new PropertyMetadata((PropertyChangedCallback) null));

    [Obsolete("Internal use only")]
    public CalendarItem() => ((Control) this).DefaultStyleKey = (object) typeof (CalendarItem);

    public CalendarItem(Calendar owner)
    {
      ((Control) this).DefaultStyleKey = (object) typeof (CalendarItem);
      this._owningCalendar = owner;
    }

    public int DayNumber
    {
      get => (int) ((DependencyObject) this).GetValue(CalendarItem.DayNumberProperty);
      internal set => ((DependencyObject) this).SetValue(CalendarItem.DayNumberProperty, (object) value);
    }

    private static void OnDayNumberChanged(
      DependencyObject source,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(source is CalendarItem calendarItem))
        return;
      calendarItem.SetForecolor();
      calendarItem.SetBackcolor();
    }

    internal bool IsSelected
    {
      get => (bool) ((DependencyObject) this).GetValue(CalendarItem.IsSelectedProperty);
      set => ((DependencyObject) this).SetValue(CalendarItem.IsSelectedProperty, (object) value);
    }

    private static void OnIsSelectedChanged(
      DependencyObject source,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(source is CalendarItem calendarItem))
        return;
      calendarItem.SetBackcolor();
      calendarItem.SetForecolor();
    }

    public DateTime ItemDate
    {
      get => (DateTime) ((DependencyObject) this).GetValue(CalendarItem.ItemDateProperty);
      internal set => ((DependencyObject) this).SetValue(CalendarItem.ItemDateProperty, (object) value);
    }

    public virtual void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      ((Control) this).Background = (Brush) new SolidColorBrush(Colors.Transparent);
      ((Control) this).Foreground = Application.Current.Resources[(object) "PhoneForegroundBrush"] as Brush;
      this.SetBackcolor();
      this.SetForecolor();
    }

    private bool IsConverterNeeded()
    {
      bool flag = true;
      if (this._owningCalendar.DatesSource != null && !this._owningCalendar.DatesAssigned.Contains(this.ItemDate))
        flag = false;
      return flag;
    }

    internal void SetBackcolor()
    {
      Brush resource = Application.Current.Resources[(object) "PhoneAccentBrush"] as Brush;
      if (this._owningCalendar.ColorConverter != null && this.IsConverterNeeded())
        ((Control) this).Background = this._owningCalendar.ColorConverter.Convert(this.ItemDate, this.IsSelected, this.IsSelected ? resource : (Brush) (object) new SolidColorBrush(Colors.Transparent), BrushType.Background);
      else
        ((Control) this).Background = this.IsSelected ? resource : (Brush) (object) new SolidColorBrush(Colors.Transparent);
    }

    internal void SetForecolor()
    {
      Brush resource = Application.Current.Resources[(object) "PhoneForegroundBrush"] as Brush;
      if (this._owningCalendar.ColorConverter != null && this.IsConverterNeeded())
        ((Control) this).Foreground = this._owningCalendar.ColorConverter.Convert(this.ItemDate, this.IsSelected, resource, BrushType.Foreground);
      else
        ((Control) this).Foreground = resource;
    }
  }
}
