// Decompiled with JetBrains decompiler
// Type: WPControls.Calendar
// Assembly: WPControls, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C24F0B77-9983-4985-A68F-A065B9B08C6B
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\WPControls.dll

using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace WPControls
{
  public class Calendar : Control
  {
    private const short RowCount = 6;
    private const short ColumnCount = 8;
    private const short Factor = 1000;
    private Grid _itemsGrid;
    private CalendarItem _lastItem;
    private bool _addedItems;
    private int _month = DateTime.Today.Month;
    private int _year = DateTime.Today.Year;
    internal List<DateTime> DatesAssigned;
    internal static readonly DependencyProperty PrivateDataContextPropertyProperty = DependencyProperty.Register(nameof (PrivateDataContextProperty), typeof (object), typeof (Calendar), new PropertyMetadata((object) null, new PropertyChangedCallback(Calendar.OnPrivateDataContextChanged)));
    public static readonly DependencyProperty DatesSourceProperty = DependencyProperty.Register(nameof (DatesSource), typeof (IEnumerable<ISupportCalendarItem>), typeof (Calendar), new PropertyMetadata((object) null, new PropertyChangedCallback(Calendar.OnDatesSourceChanged)));
    public static readonly DependencyProperty DatePropertyNameForDatesSourceProperty = DependencyProperty.Register("DatePropertyNameForDatesSource", typeof (string), typeof (Calendar), new PropertyMetadata((object) string.Empty, new PropertyChangedCallback(Calendar.OnDatesSourceChanged)));
    public static readonly DependencyProperty CalendarItemStyleProperty = DependencyProperty.Register(nameof (CalendarItemStyle), typeof (Style), typeof (Calendar), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CalendarWeekItemStyleStyleProperty = DependencyProperty.Register(nameof (CalendarWeekItemStyle), typeof (Style), typeof (Calendar), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty YearMonthLabelProperty = DependencyProperty.Register(nameof (YearMonthLabel), typeof (string), typeof (Calendar), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register(nameof (SelectedDate), typeof (DateTime), typeof (Calendar), new PropertyMetadata((object) DateTime.MinValue, new PropertyChangedCallback(Calendar.OnSelectedDateChanged)));
    public static readonly DependencyProperty ColorConverterProperty = DependencyProperty.Register(nameof (ColorConverter), typeof (IDateToBrushConverter), typeof (Calendar), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty SelectedYearProperty = DependencyProperty.Register(nameof (SelectedYear), typeof (int), typeof (Calendar), new PropertyMetadata((object) DateTime.Today.Year, new PropertyChangedCallback(Calendar.OnSelectedYearMonthChanged)));
    public static readonly DependencyProperty SelectedMonthProperty = DependencyProperty.Register(nameof (SelectedMonth), typeof (int), typeof (Calendar), new PropertyMetadata((object) DateTime.Today.Month, new PropertyChangedCallback(Calendar.OnSelectedYearMonthChanged)));
    public static readonly DependencyProperty ShowNavigationButtonsProperty = DependencyProperty.Register(nameof (ShowNavigationButtons), typeof (bool), typeof (Calendar), new PropertyMetadata((object) true));
    public static readonly DependencyProperty EnableGesturesProperty = DependencyProperty.Register(nameof (EnableGestures), typeof (bool), typeof (Calendar), new PropertyMetadata((object) false, new PropertyChangedCallback(Calendar.OnEnableGesturesChanged)));
    public static readonly DependencyProperty ShowSelectedDateProperty = DependencyProperty.Register(nameof (ShowSelectedDate), typeof (bool), typeof (Calendar), new PropertyMetadata((object) true));
    public static readonly DependencyProperty WeekNumberDisplayProperty = DependencyProperty.Register(nameof (WeekNumberDisplay), typeof (WeekNumberDisplayOption), typeof (Calendar), new PropertyMetadata((object) WeekNumberDisplayOption.None, new PropertyChangedCallback(Calendar.OnWeekNumberDisplayChanged)));
    public static readonly DependencyProperty SundayProperty = DependencyProperty.Register(nameof (Sunday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Su"));
    public static readonly DependencyProperty MondayProperty = DependencyProperty.Register(nameof (Monday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Mo"));
    public static readonly DependencyProperty TuesdayProperty = DependencyProperty.Register(nameof (Tuesday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Tu"));
    public static readonly DependencyProperty WednesdayProperty = DependencyProperty.Register(nameof (Wednesday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "We"));
    public static readonly DependencyProperty ThursdayProperty = DependencyProperty.Register(nameof (Thursday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Th"));
    public static readonly DependencyProperty FridayProperty = DependencyProperty.Register(nameof (Friday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Fr"));
    public static readonly DependencyProperty SaturdayProperty = DependencyProperty.Register(nameof (Saturday), typeof (string), typeof (Calendar), new PropertyMetadata((object) "Sa"));
    public static readonly DependencyProperty MinimumDateProperty = DependencyProperty.Register(nameof (MinimumDate), typeof (DateTime), typeof (Calendar), new PropertyMetadata((object) new DateTime(1753, 1, 1)));
    public static readonly DependencyProperty MaximumDateProperty = DependencyProperty.Register(nameof (MaximumDate), typeof (DateTime), typeof (Calendar), new PropertyMetadata((object) new DateTime(2499, 12, 31)));
    public static readonly DependencyProperty StartingDayOfWeekProperty = DependencyProperty.Register(nameof (StartingDayOfWeek), typeof (DayOfWeek), typeof (Calendar), new PropertyMetadata((object) DayOfWeek.Sunday, new PropertyChangedCallback(Calendar.OnStartDayOfWeekChanged)));
    private bool _ignoreMonthChange;
    private readonly DateTimeFormatInfo _dateTimeFormatInfo;

    public Calendar()
    {
      this.DefaultStyleKey = (object) typeof (Calendar);
      this.DatesAssigned = new List<DateTime>();
      Binding binding = new Binding();
      ((FrameworkElement) this).Loaded += new RoutedEventHandler(this.CalendarLoaded);
      ((FrameworkElement) this).SetBinding(Calendar.PrivateDataContextPropertyProperty, binding);
      this.WireUpDataSource(((FrameworkElement) this).DataContext, ((FrameworkElement) this).DataContext);
      this._dateTimeFormatInfo = !CultureInfo.CurrentCulture.IsNeutralCulture ? CultureInfo.CurrentCulture.DateTimeFormat : new CultureInfo("en-US").DateTimeFormat;
      this.SetupDaysOfWeekLabels();
    }

    private void CalendarLoaded(object sender, RoutedEventArgs e)
    {
      if (!this.EnableGestures)
        return;
      this.EnableGesturesSupport();
    }

    private void EnableGesturesSupport()
    {
      this.DisableGesturesSupport();
      TouchPanel.EnabledGestures = GestureType.Flick;
      ((UIElement) this).ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.CalendarManipulationCompleted);
    }

    private void DisableGesturesSupport()
    {
      TouchPanel.EnabledGestures = GestureType.None;
      ((UIElement) this).ManipulationCompleted -= new EventHandler<ManipulationCompletedEventArgs>(this.CalendarManipulationCompleted);
    }

    private void CalendarManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      while (TouchPanel.IsGestureAvailable)
      {
        GestureSample gestureSample = TouchPanel.ReadGesture();
        if (gestureSample.GestureType == GestureType.Flick)
        {
          double num1 = (double) gestureSample.Delta.X / 1000.0;
          double num2 = (double) gestureSample.Delta.Y / 1000.0;
          if (Math.Abs(num1) > Math.Abs(num2))
          {
            if ((int) num1 > 0)
              this.DecrementMonth();
            else
              this.IncrementMonth();
          }
          else if ((int) num2 > 0)
            this.DecrementYear();
          else
            this.IncrementYear();
        }
      }
    }

    public event EventHandler<MonthChangedEventArgs> MonthChanging;

    public event EventHandler<MonthChangedEventArgs> MonthChanged;

    public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

    public event EventHandler<SelectionChangedEventArgs> DateClicked;

    protected void OnMonthChanging(int year, int month)
    {
      if (this.MonthChanging == null)
        return;
      this.MonthChanging((object) this, new MonthChangedEventArgs(year, month));
    }

    protected void OnMonthChanged(int year, int month)
    {
      if (this.MonthChanged == null)
        return;
      this.MonthChanged((object) this, new MonthChangedEventArgs(year, month));
    }

    protected void OnSelectionChanged(DateTime dateTime)
    {
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, new SelectionChangedEventArgs(dateTime));
    }

    protected void OnDateClicked(DateTime dateTime)
    {
      if (this.DateClicked == null)
        return;
      this.DateClicked((object) this, new SelectionChangedEventArgs(dateTime));
    }

    internal object PrivateDataContextProperty
    {
      get => ((DependencyObject) this).GetValue(Calendar.PrivateDataContextPropertyProperty);
      set => ((DependencyObject) this).SetValue(Calendar.PrivateDataContextPropertyProperty, value);
    }

    private static void OnPrivateDataContextChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is Calendar calendar))
        return;
      calendar.WireUpDataSource(e.OldValue, e.NewValue);
      calendar.Refresh();
    }

    private void WireUpDataSource(object oldValue, object newValue)
    {
      if (newValue != null && newValue is INotifyPropertyChanged notifyPropertyChanged1)
        notifyPropertyChanged1.PropertyChanged += new PropertyChangedEventHandler(this.SourcePropertyChanged);
      if (oldValue == null || !(newValue is INotifyPropertyChanged notifyPropertyChanged2))
        return;
      notifyPropertyChanged2.PropertyChanged -= new PropertyChangedEventHandler(this.SourcePropertyChanged);
    }

    public void Refresh()
    {
      this.BuildDates();
      this.BuildItems();
    }

    private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      BindingExpression bindingExpression = ((FrameworkElement) this).GetBindingExpression(Calendar.DatesSourceProperty);
      if (bindingExpression == null || !bindingExpression.ParentBinding.Path.Path.EndsWith(e.PropertyName))
        return;
      this.Refresh();
    }

    public IEnumerable<ISupportCalendarItem> DatesSource
    {
      get => (IEnumerable<ISupportCalendarItem>) ((DependencyObject) this).GetValue(Calendar.DatesSourceProperty);
      set => ((DependencyObject) this).SetValue(Calendar.DatesSourceProperty, (object) value);
    }

    private static void OnDatesSourceChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is Calendar calendar))
        return;
      calendar.BuildDates();
      calendar.BuildItems();
      if (e.OldValue is INotifyCollectionChanged)
        ((INotifyCollectionChanged) e.NewValue).CollectionChanged -= new NotifyCollectionChangedEventHandler(calendar.DatesSourceChanged);
      if (!(e.NewValue is INotifyCollectionChanged))
        return;
      (e.NewValue as INotifyCollectionChanged).CollectionChanged += new NotifyCollectionChangedEventHandler(calendar.DatesSourceChanged);
    }

    private void DatesSourceChanged(object sender, NotifyCollectionChangedEventArgs e) => this.Refresh();

    public Style CalendarItemStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(Calendar.CalendarItemStyleProperty);
      set => ((DependencyObject) this).SetValue(Calendar.CalendarItemStyleProperty, (object) value);
    }

    public Style CalendarWeekItemStyle
    {
      get => (Style) ((DependencyObject) this).GetValue(Calendar.CalendarWeekItemStyleStyleProperty);
      set => ((DependencyObject) this).SetValue(Calendar.CalendarWeekItemStyleStyleProperty, (object) value);
    }

    public string YearMonthLabel
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.YearMonthLabelProperty);
      internal set => ((DependencyObject) this).SetValue(Calendar.YearMonthLabelProperty, (object) value);
    }

    public DateTime SelectedDate
    {
      get => (DateTime) ((DependencyObject) this).GetValue(Calendar.SelectedDateProperty);
      set => ((DependencyObject) this).SetValue(Calendar.SelectedDateProperty, (object) value);
    }

    private static void OnSelectedDateChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is Calendar calendar))
        return;
      DateTime newValue = (DateTime) e.NewValue;
      if (calendar._itemsGrid != null)
        ((IEnumerable<UIElement>) ((Panel) calendar._itemsGrid).Children).Where<UIElement>((Func<UIElement, bool>) (oneChild => oneChild is CalendarItem && ((CalendarItem) oneChild).IsSelected && ((CalendarItem) oneChild).ItemDate != newValue)).Select<UIElement, CalendarItem>((Func<UIElement, CalendarItem>) (oneChild => (CalendarItem) oneChild)).ToList<CalendarItem>().ForEach((Action<CalendarItem>) (one => one.IsSelected = false));
      calendar.OnSelectionChanged(newValue);
    }

    public IDateToBrushConverter ColorConverter
    {
      get => (IDateToBrushConverter) ((DependencyObject) this).GetValue(Calendar.ColorConverterProperty);
      set => ((DependencyObject) this).SetValue(Calendar.ColorConverterProperty, (object) value);
    }

    public int SelectedYear
    {
      get => (int) ((DependencyObject) this).GetValue(Calendar.SelectedYearProperty);
      set => ((DependencyObject) this).SetValue(Calendar.SelectedYearProperty, (object) value);
    }

    private static void OnSelectedYearMonthChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is Calendar calendar) || calendar._year == calendar.SelectedYear && calendar._month == calendar.SelectedMonth || calendar._ignoreMonthChange)
        return;
      calendar._year = calendar.SelectedYear;
      calendar._month = calendar.SelectedMonth;
      calendar.SetYearMonthLabel();
    }

    public int SelectedMonth
    {
      get => (int) ((DependencyObject) this).GetValue(Calendar.SelectedMonthProperty);
      set => ((DependencyObject) this).SetValue(Calendar.SelectedMonthProperty, (object) value);
    }

    public bool ShowNavigationButtons
    {
      get => (bool) ((DependencyObject) this).GetValue(Calendar.ShowNavigationButtonsProperty);
      set => ((DependencyObject) this).SetValue(Calendar.ShowNavigationButtonsProperty, (object) value);
    }

    public bool EnableGestures
    {
      get => (bool) ((DependencyObject) this).GetValue(Calendar.EnableGesturesProperty);
      set => ((DependencyObject) this).SetValue(Calendar.EnableGesturesProperty, (object) value);
    }

    public static void OnEnableGesturesChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      Calendar calendar = (Calendar) sender;
      if (calendar.EnableGestures)
        calendar.EnableGesturesSupport();
      else
        calendar.DisableGesturesSupport();
    }

    public bool ShowSelectedDate
    {
      get => (bool) ((DependencyObject) this).GetValue(Calendar.ShowSelectedDateProperty);
      set => ((DependencyObject) this).SetValue(Calendar.ShowSelectedDateProperty, (object) value);
    }

    public WeekNumberDisplayOption WeekNumberDisplay
    {
      get => (WeekNumberDisplayOption) ((DependencyObject) this).GetValue(Calendar.WeekNumberDisplayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.WeekNumberDisplayProperty, (object) value);
    }

    public static void OnWeekNumberDisplayChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      ((Calendar) sender).BuildItems();
    }

    public string Sunday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.SundayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.SundayProperty, (object) value);
    }

    public string Monday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.MondayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.MondayProperty, (object) value);
    }

    public string Tuesday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.TuesdayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.TuesdayProperty, (object) value);
    }

    public string Wednesday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.WednesdayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.WednesdayProperty, (object) value);
    }

    public string Thursday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.ThursdayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.ThursdayProperty, (object) value);
    }

    public string Friday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.FridayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.FridayProperty, (object) value);
    }

    public string Saturday
    {
      get => (string) ((DependencyObject) this).GetValue(Calendar.SaturdayProperty);
      set => ((DependencyObject) this).SetValue(Calendar.SaturdayProperty, (object) value);
    }

    public DateTime MinimumDate
    {
      get => (DateTime) ((DependencyObject) this).GetValue(Calendar.MinimumDateProperty);
      set => ((DependencyObject) this).SetValue(Calendar.MinimumDateProperty, (object) value);
    }

    public DateTime MaximumDate
    {
      get => (DateTime) ((DependencyObject) this).GetValue(Calendar.MaximumDateProperty);
      set => ((DependencyObject) this).SetValue(Calendar.MaximumDateProperty, (object) value);
    }

    public DayOfWeek StartingDayOfWeek
    {
      get => (DayOfWeek) ((DependencyObject) this).GetValue(Calendar.StartingDayOfWeekProperty);
      set => ((DependencyObject) this).SetValue(Calendar.StartingDayOfWeekProperty, (object) value);
    }

    private static void OnStartDayOfWeekChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      ((Calendar) sender).SetupDayLabels();
      ((Calendar) sender).BuildItems();
    }

    public virtual void OnApplyTemplate()
    {
      ((FrameworkElement) this).OnApplyTemplate();
      if (this.GetTemplateChild("PreviousMonthButton") is Button templateChild1)
        ((ButtonBase) templateChild1).Click += new RoutedEventHandler(this.PreviousButtonClick);
      if (this.GetTemplateChild("NextMonthButton") is Button templateChild2)
        ((ButtonBase) templateChild2).Click += new RoutedEventHandler(this.NextButtonClick);
      this._itemsGrid = this.GetTemplateChild("ItemsGrid") as Grid;
      this.SetupDayLabels();
      this.BuildDates();
      this.SetYearMonthLabel();
    }

    private void NextButtonClick(object sender, RoutedEventArgs e) => this.IncrementMonth();

    private void IncrementMonth()
    {
      if (!this.CanMoveToMonthYear(this._year, this._month + 1))
        return;
      ++this._month;
      if (this._month == 13)
      {
        this._month = 1;
        ++this._year;
      }
      this.SetYearMonthLabel();
    }

    private void PreviousButtonClick(object sender, RoutedEventArgs e) => this.DecrementMonth();

    private void DecrementMonth()
    {
      if (!this.CanMoveToMonthYear(this._year, this._month - 1))
        return;
      --this._month;
      if (this._month == 0)
      {
        this._month = 12;
        --this._year;
      }
      this.SetYearMonthLabel();
    }

    private void IncrementYear()
    {
      if (!this.CanMoveToMonthYear(this._year + 1, this._month))
        return;
      ++this._year;
      this.SetYearMonthLabel();
    }

    private void DecrementYear()
    {
      if (!this.CanMoveToMonthYear(this._year - 1, this._month))
        return;
      --this._year;
      this.SetYearMonthLabel();
    }

    private bool CanMoveToMonthYear(int year, int month)
    {
      bool monthYear = false;
      switch (month)
      {
        case 0:
          --year;
          month = 12;
          break;
        case 13:
          month = 1;
          ++year;
          break;
      }
      DateTime dateTime = new DateTime(year, month, 1);
      if (dateTime >= this.MinimumDate && dateTime <= this.MaximumDate)
        monthYear = true;
      return monthYear;
    }

    private void ItemClick(object sender, RoutedEventArgs e)
    {
      if (this._lastItem != null)
        this._lastItem.IsSelected = false;
      this._lastItem = sender as CalendarItem;
      if (this._lastItem == null)
        return;
      if (this.ShowSelectedDate)
        this._lastItem.IsSelected = true;
      this.SelectedDate = this._lastItem.ItemDate;
      this.OnDateClicked(this._lastItem.ItemDate);
    }

    private void SetupDayLabels()
    {
      if (this.StartingDayOfWeek == DayOfWeek.Sunday || this._itemsGrid == null)
        return;
      int num1 = this.DayColumnOffsetFromSunday();
      foreach (UIElement uiElement in ((IEnumerable<UIElement>) ((Panel) this._itemsGrid).Children).Where<UIElement>((Func<UIElement, bool>) (one => ((DependencyObject) one).GetValue(Grid.RowProperty).Equals((object) 0))))
      {
        if (uiElement is TextBlock textBlock && !string.IsNullOrEmpty(textBlock.Text))
        {
          int num2 = 0;
          if (textBlock.Text == this.Sunday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Sunday) + num1;
          if (textBlock.Text == this.Monday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Monday) + num1;
          if (textBlock.Text == this.Tuesday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Tuesday) + num1;
          if (textBlock.Text == this.Wednesday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Wednesday) + num1;
          if (textBlock.Text == this.Thursday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Thursday) + num1;
          if (textBlock.Text == this.Friday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Friday) + num1;
          if (textBlock.Text == this.Saturday)
            num2 = this.DefaultDayColumnIndex(DayOfWeek.Saturday) + num1;
          if (num2 <= 0)
            num2 += 7;
          ((DependencyObject) textBlock).SetValue(Grid.ColumnProperty, (object) num2);
        }
      }
    }

    private int DayColumnOffsetFromSunday()
    {
      switch (this.StartingDayOfWeek)
      {
        case DayOfWeek.Sunday:
          return 0;
        case DayOfWeek.Monday:
          return -1;
        case DayOfWeek.Tuesday:
          return -2;
        case DayOfWeek.Wednesday:
          return -3;
        case DayOfWeek.Thursday:
          return -4;
        case DayOfWeek.Friday:
          return -5;
        case DayOfWeek.Saturday:
          return -6;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private int DefaultDayColumnIndex(DayOfWeek dayOfWeek)
    {
      switch (dayOfWeek)
      {
        case DayOfWeek.Sunday:
          return 1;
        case DayOfWeek.Monday:
          return 2;
        case DayOfWeek.Tuesday:
          return 3;
        case DayOfWeek.Wednesday:
          return 4;
        case DayOfWeek.Thursday:
          return 5;
        case DayOfWeek.Friday:
          return 6;
        case DayOfWeek.Saturday:
          return 7;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private void SetYearMonthLabel()
    {
      this.OnMonthChanging(this._year, this._month);
      this.YearMonthLabel = this.GetMonthName() + " " + this._year.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      this._ignoreMonthChange = true;
      this.SelectedMonth = this._month;
      this.SelectedYear = this._year;
      this._ignoreMonthChange = false;
      this.BuildItems();
      this.OnMonthChanged(this._year, this._month);
    }

    private string GetMonthName() => this._dateTimeFormatInfo.MonthNames[this._month - 1];

    private void SetupDaysOfWeekLabels()
    {
      this.Sunday = this._dateTimeFormatInfo.AbbreviatedDayNames[0];
      this.Monday = this._dateTimeFormatInfo.AbbreviatedDayNames[1];
      this.Tuesday = this._dateTimeFormatInfo.AbbreviatedDayNames[2];
      this.Wednesday = this._dateTimeFormatInfo.AbbreviatedDayNames[3];
      this.Thursday = this._dateTimeFormatInfo.AbbreviatedDayNames[4];
      this.Friday = this._dateTimeFormatInfo.AbbreviatedDayNames[5];
      this.Saturday = this._dateTimeFormatInfo.AbbreviatedDayNames[6];
    }

    private void BuildItems()
    {
      if (this._itemsGrid == null)
        return;
      this.AddDefaultItems();
      DateTime dateTime = new DateTime(this._year, this._month, 1);
      int num1 = this.DefaultDayColumnIndex(dateTime.DayOfWeek);
      if (this.StartingDayOfWeek != DayOfWeek.Sunday)
      {
        num1 += this.DayColumnOffsetFromSunday();
        if (num1 <= 0)
          num1 += 7;
      }
      int num2 = (int) Math.Floor(dateTime.AddMonths(1).Subtract(dateTime).TotalDays);
      int num3 = 0;
      int num4 = 0;
      for (int rowCount = 1; rowCount <= 6; ++rowCount)
      {
        for (int columnCount = 1; columnCount < 8; ++columnCount)
        {
          CalendarItem calendarItem = (CalendarItem) ((IEnumerable<UIElement>) ((Panel) this._itemsGrid).Children).Where<UIElement>((Func<UIElement, bool>) (oneChild => oneChild is CalendarItem && ((FrameworkElement) oneChild).Tag.ToString() == rowCount.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ":" + columnCount.ToString((IFormatProvider) CultureInfo.InvariantCulture))).First<UIElement>();
          if (rowCount == 1 && columnCount < num1)
            ((UIElement) calendarItem).Visibility = (Visibility) 1;
          else if (num3 < num2)
            ((UIElement) calendarItem).Visibility = (Visibility) 0;
          else
            ((UIElement) calendarItem).Visibility = (Visibility) 1;
          CalendarWeekItem calendarWeekItem = (CalendarWeekItem) ((IEnumerable<UIElement>) ((Panel) this._itemsGrid).Children).Where<UIElement>((Func<UIElement, bool>) (oneChild => oneChild is CalendarWeekItem && ((FrameworkElement) oneChild).Tag.ToString() == rowCount.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ":0")).FirstOrDefault<UIElement>();
          if (((UIElement) calendarItem).Visibility == null)
          {
            calendarItem.ItemDate = dateTime.AddDays((double) num3);
            if (this.SelectedDate == DateTime.MinValue && calendarItem.ItemDate == DateTime.Today)
            {
              this.SelectedDate = calendarItem.ItemDate;
              if (this.ShowSelectedDate)
                calendarItem.IsSelected = true;
              this._lastItem = calendarItem;
            }
            else if (calendarItem.ItemDate == this.SelectedDate)
            {
              if (this.ShowSelectedDate)
                calendarItem.IsSelected = true;
            }
            else
              calendarItem.IsSelected = false;
            ++num3;
            calendarItem.DayNumber = num3;
            calendarItem.SetBackcolor();
            calendarItem.SetForecolor();
            if (this.WeekNumberDisplay != WeekNumberDisplayOption.None)
            {
              int num5 = this.WeekNumberDisplay != WeekNumberDisplayOption.WeekOfYear ? rowCount : Thread.CurrentThread.CurrentCulture.Calendar.GetWeekOfYear(calendarItem.ItemDate, Thread.CurrentThread.CurrentCulture.DateTimeFormat.CalendarWeekRule, Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
              if (calendarWeekItem != null)
              {
                calendarWeekItem.WeekNumber = new int?(num5);
                num4 = num5;
                ((UIElement) calendarWeekItem).Visibility = (Visibility) 0;
              }
            }
          }
          else if (this.WeekNumberDisplay != WeekNumberDisplayOption.None && calendarWeekItem != null)
          {
            int? weekNumber = calendarWeekItem.WeekNumber;
            int num6 = num4;
            if ((weekNumber.GetValueOrDefault() != num6 ? 1 : (!weekNumber.HasValue ? 1 : 0)) != 0)
              ((UIElement) calendarWeekItem).Visibility = (Visibility) 1;
          }
        }
      }
    }

    private void AddDefaultItems()
    {
      if (this._addedItems || this._itemsGrid == null)
        return;
      for (int index1 = 1; index1 <= 6; ++index1)
      {
        for (int index2 = 1; index2 < 8; ++index2)
        {
          CalendarItem calendarItem = new CalendarItem(this);
          ((DependencyObject) calendarItem).SetValue(Grid.RowProperty, (object) index1);
          ((DependencyObject) calendarItem).SetValue(Grid.ColumnProperty, (object) index2);
          ((UIElement) calendarItem).Visibility = (Visibility) 1;
          ((FrameworkElement) calendarItem).Tag = (object) (index1.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ":" + index2.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          ((ButtonBase) calendarItem).Click += new RoutedEventHandler(this.ItemClick);
          if (this.CalendarItemStyle != null)
            ((FrameworkElement) calendarItem).Style = this.CalendarItemStyle;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this._itemsGrid).Children).Add((UIElement) calendarItem);
        }
        if (this.WeekNumberDisplay != WeekNumberDisplayOption.None)
        {
          CalendarWeekItem calendarWeekItem = new CalendarWeekItem();
          ((DependencyObject) calendarWeekItem).SetValue(Grid.RowProperty, (object) index1);
          ((DependencyObject) calendarWeekItem).SetValue(Grid.ColumnProperty, (object) 0);
          ((UIElement) calendarWeekItem).Visibility = (Visibility) 1;
          ((FrameworkElement) calendarWeekItem).Tag = (object) (index1.ToString((IFormatProvider) CultureInfo.InvariantCulture) + ":" + 0.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          if (this.CalendarWeekItemStyle != null)
            ((FrameworkElement) calendarWeekItem).Style = this.CalendarWeekItemStyle;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this._itemsGrid).Children).Add((UIElement) calendarWeekItem);
        }
      }
      this._addedItems = true;
    }

    private void BuildDates()
    {
      if (this.DatesSource == null)
        return;
      this.DatesAssigned.Clear();
      this.DatesSource.ToList<ISupportCalendarItem>().ForEach((Action<ISupportCalendarItem>) (one => this.DatesAssigned.Add(one.CalendarItemDate)));
    }
  }
}
