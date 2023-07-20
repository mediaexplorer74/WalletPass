// Decompiled with JetBrains decompiler
// Type: WalletPass.setRelevantDatePage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using WalletPass.Resources;

namespace WalletPass
{
  public class setRelevantDatePage : PhoneApplicationPage
  {
    private ApplicationBarIconButton _btnOK;
    private ApplicationBarIconButton _btnCancel;
    private bool isTombstoned = true;
    internal Grid LayoutRoot;
    internal WPControls.Calendar Calendar;
    internal LoopingSelector HoursLoopingSelector;
    internal LoopingSelector MinutesLoopingSelector;
    internal LoopingSelector AMPMLoopingSelector;
    private bool _contentLoaded;

    public setRelevantDatePage()
    {
      this.InitializeComponent();
      List<string> stringList1 = new List<string>()
      {
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23"
      };
      List<string> stringList2 = new List<string>()
      {
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12"
      };
      List<string> stringList3 = new List<string>()
      {
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
        "24",
        "25",
        "26",
        "27",
        "28",
        "29",
        "30",
        "31",
        "32",
        "33",
        "34",
        "35",
        "36",
        "37",
        "38",
        "39",
        "40",
        "41",
        "42",
        "43",
        "44",
        "45",
        "46",
        "47",
        "48",
        "49",
        "50",
        "51",
        "52",
        "53",
        "54",
        "55",
        "56",
        "57",
        "58",
        "59"
      };
      List<string> stringList4 = new List<string>()
      {
        "AM",
        "PM"
      };
      LoopingSelector minutesLoopingSelector = this.MinutesLoopingSelector;
      ListLoopingDataSource<string> loopingDataSource1 = new ListLoopingDataSource<string>();
      loopingDataSource1.Items = (IEnumerable<string>) stringList3;
      loopingDataSource1.SelectedItem = (object) "00";
      ListLoopingDataSource<string> loopingDataSource2 = loopingDataSource1;
      minutesLoopingSelector.DataSource = (ILoopingSelectorDataSource) loopingDataSource2;
      LoopingSelector ampmLoopingSelector = this.AMPMLoopingSelector;
      ListLoopingDataSource<string> loopingDataSource3 = new ListLoopingDataSource<string>();
      loopingDataSource3.Items = (IEnumerable<string>) stringList4;
      loopingDataSource3.SelectedItem = (object) "AM";
      ListLoopingDataSource<string> loopingDataSource4 = loopingDataSource3;
      ampmLoopingSelector.DataSource = (ILoopingSelectorDataSource) loopingDataSource4;
      if (DateTimeFormatInfo.CurrentInfo.ShortTimePattern.Contains("H"))
      {
        LoopingSelector hoursLoopingSelector = this.HoursLoopingSelector;
        ListLoopingDataSource<string> loopingDataSource5 = new ListLoopingDataSource<string>();
        loopingDataSource5.Items = (IEnumerable<string>) stringList1;
        loopingDataSource5.SelectedItem = (object) "00";
        ListLoopingDataSource<string> loopingDataSource6 = loopingDataSource5;
        hoursLoopingSelector.DataSource = (ILoopingSelectorDataSource) loopingDataSource6;
        ((UIElement) this.AMPMLoopingSelector).Visibility = (Visibility) 1;
        ((FrameworkElement) this.HoursLoopingSelector).HorizontalAlignment = (HorizontalAlignment) 2;
        ((FrameworkElement) this.MinutesLoopingSelector).HorizontalAlignment = (HorizontalAlignment) 0;
      }
      else
      {
        LoopingSelector hoursLoopingSelector = this.HoursLoopingSelector;
        ListLoopingDataSource<string> loopingDataSource7 = new ListLoopingDataSource<string>();
        loopingDataSource7.Items = (IEnumerable<string>) stringList2;
        loopingDataSource7.SelectedItem = (object) "01";
        ListLoopingDataSource<string> loopingDataSource8 = loopingDataSource7;
        hoursLoopingSelector.DataSource = (ILoopingSelectorDataSource) loopingDataSource8;
        ((UIElement) this.AMPMLoopingSelector).Visibility = (Visibility) 0;
      }
      this.Calendar.StartingDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
      if (App._groupItemIndex == -1)
      {
        DateTime relevantDate = App._tempPassClass.relevantDate;
        if (relevantDate != new DateTime(1, 1, 1))
        {
          this.Calendar.SelectedDate = relevantDate;
          this.MinutesLoopingSelector.DataSource.SelectedItem = (object) relevantDate.Minute.ToString("d2");
          this.HoursLoopingSelector.DataSource.SelectedItem = !DateTimeFormatInfo.CurrentInfo.ShortTimePattern.Contains("H") ? (object) (relevantDate.Hour % 12).ToString("d2") : (object) relevantDate.Hour.ToString("d2");
        }
      }
      else
      {
        DateTime relevantDate = App._tempPassGroup[App._groupItemIndex].relevantDate;
        if (relevantDate != new DateTime(1, 1, 1))
        {
          this.Calendar.SelectedDate = relevantDate;
          this.MinutesLoopingSelector.DataSource.SelectedItem = (object) relevantDate.Minute.ToString("d2");
          this.HoursLoopingSelector.DataSource.SelectedItem = !DateTimeFormatInfo.CurrentInfo.ShortTimePattern.Contains("H") ? (object) (relevantDate.Hour % 12).ToString("d2") : (object) relevantDate.Hour.ToString("d2");
        }
      }
      this.Calendar.ShowSelectedDate = true;
      this._btnOK = new ApplicationBarIconButton();
      this._btnOK.IconUri = new Uri("/Assets/AppBar/appbar.ok.png", UriKind.Relative);
      this._btnOK.Text = AppResources.AppBarButtonOk;
      this._btnOK.Click += new EventHandler(this.btnOK_Click);
      this._btnCancel = new ApplicationBarIconButton();
      this._btnCancel.IconUri = new Uri("/Assets/AppBar/appbar.cancel.png", UriKind.Relative);
      this._btnCancel.Text = AppResources.AppBarButtonCancel;
      this._btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.ApplicationBar.Buttons.Add((object) this._btnOK);
      this.ApplicationBar.Buttons.Add((object) this._btnCancel);
      ((UIElement) this).Opacity = 0.0;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      this.showTransitionOutBackward();
      this.isTombstoned = false;
      ((Page) this).NavigationService.GoBack();
    }

    private async void btnOK_Click(object sender, EventArgs e)
    {
      int hour = int.Parse((string) ((LoopingDataSourceBase) this.HoursLoopingSelector.DataSource).SelectedItem);
      int minutes = int.Parse((string) ((LoopingDataSourceBase) this.MinutesLoopingSelector.DataSource).SelectedItem);
      if (((UIElement) this.AMPMLoopingSelector).Visibility == null && (string) ((LoopingDataSourceBase) this.AMPMLoopingSelector.DataSource).SelectedItem == "PM")
        hour += 12;
      DateTime Value = new DateTime(this.Calendar.SelectedDate.Year, this.Calendar.SelectedDate.Month, this.Calendar.SelectedDate.Day, hour, minutes, 0);
      int index;
      if (App._groupItemIndex == -1)
      {
        index = App._passcollection.IndexOf(App._tempPassClass);
        App._tempPassClass.relevantDate = Value;
        if (!string.IsNullOrEmpty(App._tempPassClass.idAppointment))
        {
          ClaseSaveCalendar _calendar = new ClaseSaveCalendar();
          int num = await _calendar.editAppointment(App._tempPassClass.idAppointment, App._currentAppCalendar, Value) ? 1 : 0;
        }
      }
      else
      {
        index = App._passcollection.passIndex(App._tempPassClass);
        App._tempPassGroup[App._groupItemIndex].relevantDate = Value;
        if (!string.IsNullOrEmpty(App._tempPassGroup[App._groupItemIndex].idAppointment))
        {
          ClaseSaveCalendar _calendar = new ClaseSaveCalendar();
          int num = await _calendar.editAppointment(App._tempPassGroup[App._groupItemIndex].idAppointment, App._currentAppCalendar, Value) ? 1 : 0;
        }
      }
      App._passcollection[index].relevantDate = Value;
      IO.SaveData(App._passcollection);
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorMain, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      this.ApplicationBar.BackgroundColor = solidColorBrush3.Color;
      this.ApplicationBar.ForegroundColor = solidColorBrush2.Color;
      this.HoursLoopingSelector.ForegroundItem = (Brush) solidColorBrush3;
      this.HoursLoopingSelector.BackgroundItem = (Brush) solidColorBrush2;
      this.HoursLoopingSelector.BorderBrushNotSelectedItem = (Brush) solidColorBrush2;
      this.HoursLoopingSelector.ForegroundNotSelectedItem = (Brush) solidColorBrush2;
      this.MinutesLoopingSelector.ForegroundItem = (Brush) solidColorBrush3;
      this.MinutesLoopingSelector.BackgroundItem = (Brush) solidColorBrush2;
      this.MinutesLoopingSelector.BorderBrushNotSelectedItem = (Brush) solidColorBrush2;
      this.MinutesLoopingSelector.ForegroundNotSelectedItem = (Brush) solidColorBrush2;
      this.AMPMLoopingSelector.ForegroundItem = (Brush) solidColorBrush3;
      this.AMPMLoopingSelector.BackgroundItem = (Brush) solidColorBrush2;
      this.AMPMLoopingSelector.BorderBrushNotSelectedItem = (Brush) solidColorBrush2;
      this.AMPMLoopingSelector.ForegroundNotSelectedItem = (Brush) solidColorBrush2;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        StateManager.LoadStateAll((PhoneApplicationPage) this);
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
        App._reconstructPages = true;
        this.ApplicationBar.IsVisible = true;
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      if (!this.isTombstoned)
        return;
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      this.showTransitionOutBackward();
      this.isTombstoned = false;
      base.OnBackKeyPress(e);
    }

    private void HoursLoopingSelector_IsExpandedChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      this.MinutesLoopingSelector.IsExpanded = !this.HoursLoopingSelector.IsExpanded;
    }

    private void MinutesLoopingSelector_IsExpandedChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      this.HoursLoopingSelector.IsExpanded = !this.MinutesLoopingSelector.IsExpanded;
    }

    private void showTransitionOutBackward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      opacityTransition.GetTransition((UIElement) element).Begin();
    }

    private void showTransitionInForward()
    {
      OpacityTransition opacityTransition = new OpacityTransition();
      opacityTransition.Mode = OpacityTransitionMode.TransitionIn;
      PhoneApplicationPage content = (PhoneApplicationPage) ((ContentControl) Application.Current.RootVisual).Content;
      ITransition transition = opacityTransition.GetTransition((UIElement) content);
      transition.Completed += (EventHandler) ((param0, param1) =>
      {
        ((UIElement) this).Opacity = 1.0;
        this.ApplicationBar.IsVisible = true;
      });
      transition.Begin();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/setRelevantDatePage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.Calendar = (WPControls.Calendar) ((FrameworkElement) this).FindName("Calendar");
      this.HoursLoopingSelector = (LoopingSelector) ((FrameworkElement) this).FindName("HoursLoopingSelector");
      this.MinutesLoopingSelector = (LoopingSelector) ((FrameworkElement) this).FindName("MinutesLoopingSelector");
      this.AMPMLoopingSelector = (LoopingSelector) ((FrameworkElement) this).FindName("AMPMLoopingSelector");
    }
  }
}
