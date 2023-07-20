// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseReminderItems
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using WalletPass.Resources;

namespace WalletPass
{
  public class ClaseReminderItems
  {
    private List<string> _listPickerCalendarItems { get; set; }

    private List<string> _listPickerNotificationItems { get; set; }

    public ClaseReminderItems()
    {
      this.listPickerCalendarItemsInit();
      this.listPickerNotificationsItemsInit();
    }

    private void listPickerCalendarItemsInit()
    {
      this._listPickerCalendarItems = new List<string>();
      this._listPickerCalendarItems.Add(AppResources.calendarReminderNo);
      this._listPickerCalendarItems.Add(AppResources.calendarReminderInit);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder5min);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder10min);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder15min);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder30min);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder1hour);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder4hour);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder18hour);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder1day);
      this._listPickerCalendarItems.Add(AppResources.calendarReminder1week);
    }

    private void listPickerNotificationsItemsInit()
    {
      this._listPickerNotificationItems = new List<string>();
      this._listPickerNotificationItems.Add(AppResources.calendarReminder20min);
      this._listPickerNotificationItems.Add(AppResources.calendarReminder45min);
      this._listPickerNotificationItems.Add(AppResources.calendarReminder1hour);
      this._listPickerNotificationItems.Add(AppResources.calendarReminder4hour);
      this._listPickerNotificationItems.Add(AppResources.calendarReminder18hour);
      this._listPickerNotificationItems.Add(AppResources.calendarReminder1day);
    }

    public TimeSpan listPickerCalendarItemTimeSpan(int id)
    {
      switch (id)
      {
        case 0:
          return TimeSpan.Zero;
        case 1:
          return TimeSpan.FromSeconds(0.0);
        case 2:
          return TimeSpan.FromMinutes(5.0);
        case 3:
          return TimeSpan.FromMinutes(10.0);
        case 4:
          return TimeSpan.FromMinutes(15.0);
        case 5:
          return TimeSpan.FromMinutes(30.0);
        case 6:
          return TimeSpan.FromHours(1.0);
        case 7:
          return TimeSpan.FromHours(4.0);
        case 8:
          return TimeSpan.FromHours(18.0);
        case 9:
          return TimeSpan.FromDays(1.0);
        case 10:
          return TimeSpan.FromDays(7.0);
        default:
          return TimeSpan.Zero;
      }
    }

    public TimeSpan listPickerNotificationItemTimeSpan(int id)
    {
      switch (id)
      {
        case 0:
          return TimeSpan.FromMinutes(20.0);
        case 1:
          return TimeSpan.FromMinutes(45.0);
        case 2:
          return TimeSpan.FromHours(1.0);
        case 3:
          return TimeSpan.FromHours(4.0);
        case 4:
          return TimeSpan.FromHours(18.0);
        case 5:
          return TimeSpan.FromDays(1.0);
        default:
          return TimeSpan.Zero;
      }
    }

    public string listPickerCalendarItem(int id) => this._listPickerCalendarItems[id];

    public string listPickerNotificationItem(int id) => this._listPickerNotificationItems[id];

    public List<string> listPickerCalendarItems
    {
      get => this._listPickerCalendarItems;
      set => this._listPickerCalendarItems = value;
    }

    public List<string> listPickerNotificationItems
    {
      get => this._listPickerNotificationItems;
      set => this._listPickerNotificationItems = value;
    }
  }
}
