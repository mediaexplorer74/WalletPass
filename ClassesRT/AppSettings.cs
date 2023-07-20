// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.AppSettings
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;
using System.ComponentModel;
using Windows.Storage;

namespace Wallet_Pass
{
  public class AppSettings : INotifyPropertyChanged
  {
    private const string listExpiredTransparentSettingKeyName = "listExpiredTransparentSetting";
    private const string listShowDatesSettingKeyName = "listShowDatesSetting";
    private const string listShowGroupKeyName = "listShowGroup";
    private const string listGroupByKeyName = "listGroupBy";
    private const string listGroupOrderKeyName = "listGroupOrder";
    private const string listExpiredListKeyName = "listExpiredList";
    private const string listElementSizeKeyName = "listElementSize";
    private const string listOrderKeyName = "listOrder";
    private const string tileBackgroundKeyName = "tileBackground";
    private const string locationEnabledKeyName = "locationEnabled";
    private const string notificationEnabledKeyName = "notificationEnabled";
    private const string silenceNotificationEnabledKeyName = "silenceNotificationEnabled";
    private const string notificationReminderKeyName = "notificationReminder";
    private const string notificationReminderExpiredKeyName = "notificationReminderExpired";
    private const string notificationDisplayAlwaysKeyName = "notificationDisplayAlways";
    private const string notificationUpdateKeyName = "notificationUpdate";
    private const string calendarAutomaticSaveEnabledKeyName = "calendarAutomaticSave";
    private const string calendarOnSaveEnabledKeyName = "calendarOnSave";
    private const string calendarUserConfirmationEnabledKeyName = "calendarUserConfirmation";
    private const string calendarReminderKeyName = "calendarReminder";
    private const string calendarStateKeyName = "calendarState";
    private const string themeColorHeaderKeyName = "themeColorHeader";
    private const string themeColorMainKeyName = "themeColorMain";
    private const string themeColorForegroundKeyName = "themeColorForeground";
    private const string themeColorCustomHeaderKeyName = "themeColorCustomHeader";
    private const string themeColorCustomMainKeyName = "themeColorCustomMain";
    private const string themeColorCustomForegroundKeyName = "themeColorCustomForeground";
    private const string themeListThemeSelectionKeyName = "themeListThemeSelection";
    private const string saveAddWalletEnabledKeyName = "saveAddWalletEnabled";
    private const string saveOpenPassEnabledKeyName = "saveOpenPassEnabled";
    private const string saveAddWalletOptionKeyName = "saveAddWalletOption";
    private const string updateAutomaticUpdatesKeyName = "updateAutomaticUpdates";
    private const string updateWifiOnlyKeyName = "updateWifiOnly";
    private const bool listExpiredTransparentSettingDefault = false;
    private const bool listShowDatesSettingDefault = false;
    private const bool listShowGroupDefault = false;
    private const int listGroupByDefault = 0;
    private const int listGroupOrderDefault = 0;
    private const bool listExpiredListDefault = false;
    private const int listElementSizeDefault = 0;
    private const int listOrderDefault = 0;
    private const int tileBackgroundDefault = 0;
    private const bool locationEnabledDefault = true;
    private const bool notificationEnabledDefault = true;
    private const bool silenceNotificationEnabledDefault = true;
    private const int notificationReminderDefault = 1;
    private const int notificationReminderExpiredDefault = 0;
    private const bool notificationDisplayAlwaysDefault = false;
    private const bool notificationUpdateDefault = true;
    private const bool calendarAutomaticSaveEnabledDefault = true;
    private const bool calendarOnSaveEnabledDefault = false;
    private const bool calendarUserConfirmationEnabledDefault = false;
    private const int calendarReminderDefault = 4;
    private const int calendarStateDefault = 2;
    private const string themeColorHeaderDefault = "#FFD2E5FB";
    private const string themeColorMainDefault = "#FFF0F7FF";
    private const string themeColorForegroundDefault = "#FF006BA8";
    private const string themeColorCustomHeaderDefault = "#FFD2E5FB";
    private const string themeColorCustomMainDefault = "#FFF0F7FF";
    private const string themeColorCustomForegroundDefault = "#FF006BA8";
    private const int themeListThemeSelectionDefault = 0;
    private const bool saveAddWalletEnabledDefault = true;
    private const bool saveOpenPassEnabledDefault = true;
    private const int saveAddWalletOptionDefault = 1;
    private const bool updateAutomaticUpdatesDefault = true;
    private const bool updateWifiOnlyDefault = false;
    private ApplicationDataContainer localSettings;

    public event PropertyChangedEventHandler PropertyChanged;

    public AppSettings() => this.localSettings = ApplicationData.Current.LocalSettings;

    private bool AddOrUpdateValue(string key, object value)
    {
      bool flag = false;
      if (((IDictionary<string, object>) this.localSettings.Values)[key] != null)
      {
        if (((IDictionary<string, object>) this.localSettings.Values)[key] != value)
        {
          ((IDictionary<string, object>) this.localSettings.Values)[key] = value;
          flag = true;
        }
      }
      else
      {
        ((IDictionary<string, object>) this.localSettings.Values)[key] = value;
        flag = true;
      }
      return flag;
    }

    private T GetValueOrDefault<T>(string key, T defaultValue) => ((IDictionary<string, object>) this.localSettings.Values)[key] == null ? defaultValue : (T) ((IDictionary<string, object>) this.localSettings.Values)[key];

    public bool listExpiredTransparentSetting
    {
      get => this.GetValueOrDefault<bool>(nameof (listExpiredTransparentSetting), false);
      set => this.AddOrUpdateValue(nameof (listExpiredTransparentSetting), (object) value);
    }

    public bool listExpiredList
    {
      get => this.GetValueOrDefault<bool>(nameof (listExpiredList), false);
      set => this.AddOrUpdateValue(nameof (listExpiredList), (object) value);
    }

    public bool listShowDatesSetting
    {
      get
      {
        this.NotifyProperties();
        return this.GetValueOrDefault<bool>(nameof (listShowDatesSetting), false);
      }
      set
      {
        this.AddOrUpdateValue(nameof (listShowDatesSetting), (object) value);
        this.NotifyProperties();
      }
    }

    public bool listShowGroup
    {
      get => this.GetValueOrDefault<bool>(nameof (listShowGroup), false);
      set => this.AddOrUpdateValue(nameof (listShowGroup), (object) value);
    }

    public int listGroupBy
    {
      get => this.GetValueOrDefault<int>(nameof (listGroupBy), 0);
      set => this.AddOrUpdateValue(nameof (listGroupBy), (object) value);
    }

    public int listGroupOrder
    {
      get => this.GetValueOrDefault<int>(nameof (listGroupOrder), 0);
      set => this.AddOrUpdateValue(nameof (listGroupOrder), (object) value);
    }

    public int listElementSize
    {
      get => this.GetValueOrDefault<int>(nameof (listElementSize), 0);
      set => this.AddOrUpdateValue(nameof (listElementSize), (object) value);
    }

    public int listOrder
    {
      get => this.GetValueOrDefault<int>(nameof (listOrder), 0);
      set => this.AddOrUpdateValue(nameof (listOrder), (object) value);
    }

    public int tileBackground
    {
      get => this.GetValueOrDefault<int>(nameof (tileBackground), 0);
      set => this.AddOrUpdateValue(nameof (tileBackground), (object) value);
    }

    public bool locationEnabled
    {
      get => this.GetValueOrDefault<bool>(nameof (locationEnabled), true);
      set => this.AddOrUpdateValue(nameof (locationEnabled), (object) value);
    }

    public bool notificationEnabled
    {
      get => this.GetValueOrDefault<bool>(nameof (notificationEnabled), true);
      set => this.AddOrUpdateValue(nameof (notificationEnabled), (object) value);
    }

    public bool silenceNotificationEnabled
    {
      get => this.GetValueOrDefault<bool>(nameof (silenceNotificationEnabled), true);
      set => this.AddOrUpdateValue(nameof (silenceNotificationEnabled), (object) value);
    }

    public bool notificationDisplayAlways
    {
      get => this.GetValueOrDefault<bool>(nameof (notificationDisplayAlways), false);
      set => this.AddOrUpdateValue(nameof (notificationDisplayAlways), (object) value);
    }

    public bool notificationUpdate
    {
      get => this.GetValueOrDefault<bool>(nameof (notificationUpdate), true);
      set => this.AddOrUpdateValue(nameof (notificationUpdate), (object) value);
    }

    public bool calendarAutomaticSave
    {
      get => this.GetValueOrDefault<bool>(nameof (calendarAutomaticSave), true);
      set => this.AddOrUpdateValue(nameof (calendarAutomaticSave), (object) value);
    }

    public bool calendarOnSave
    {
      get => this.GetValueOrDefault<bool>(nameof (calendarOnSave), false);
      set => this.AddOrUpdateValue(nameof (calendarOnSave), (object) value);
    }

    public bool calendarUserConfirmation
    {
      get => this.GetValueOrDefault<bool>(nameof (calendarUserConfirmation), false);
      set => this.AddOrUpdateValue(nameof (calendarUserConfirmation), (object) value);
    }

    public int calendarReminder
    {
      get => this.GetValueOrDefault<int>(nameof (calendarReminder), 4);
      set => this.AddOrUpdateValue(nameof (calendarReminder), (object) value);
    }

    public int calendarState
    {
      get => this.GetValueOrDefault<int>(nameof (calendarState), 2);
      set => this.AddOrUpdateValue(nameof (calendarState), (object) value);
    }

    public int notificationReminder
    {
      get => this.GetValueOrDefault<int>(nameof (notificationReminder), 1);
      set => this.AddOrUpdateValue(nameof (notificationReminder), (object) value);
    }

    public int notificationReminderExpired
    {
      get => this.GetValueOrDefault<int>(nameof (notificationReminderExpired), 0);
      set => this.AddOrUpdateValue(nameof (notificationReminderExpired), (object) value);
    }

    public string themeColorHeader
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorHeader), "#FFD2E5FB");
      set => this.AddOrUpdateValue(nameof (themeColorHeader), (object) value);
    }

    public string themeColorMain
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorMain), "#FFF0F7FF");
      set => this.AddOrUpdateValue(nameof (themeColorMain), (object) value);
    }

    public string themeColorForeground
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorForeground), "#FF006BA8");
      set => this.AddOrUpdateValue(nameof (themeColorForeground), (object) value);
    }

    public string themeColorCustomHeader
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorCustomHeader), "#FFD2E5FB");
      set => this.AddOrUpdateValue(nameof (themeColorCustomHeader), (object) value);
    }

    public string themeColorCustomMain
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorCustomMain), "#FFF0F7FF");
      set => this.AddOrUpdateValue(nameof (themeColorCustomMain), (object) value);
    }

    public string themeColorCustomForeground
    {
      get => this.GetValueOrDefault<string>(nameof (themeColorCustomForeground), "#FF006BA8");
      set => this.AddOrUpdateValue(nameof (themeColorCustomForeground), (object) value);
    }

    public string themeColorForegroundSemi => "#55" + this.themeColorForeground.Substring(3);

    public string themeColorForegroundSemiBold => "#AA" + this.themeColorForeground.Substring(3);

    public int themeListThemeSelection
    {
      get => this.GetValueOrDefault<int>(nameof (themeListThemeSelection), 0);
      set => this.AddOrUpdateValue(nameof (themeListThemeSelection), (object) value);
    }

    public bool saveAddWalletEnabled
    {
      get => this.GetValueOrDefault<bool>(nameof (saveAddWalletEnabled), true);
      set => this.AddOrUpdateValue(nameof (saveAddWalletEnabled), (object) value);
    }

    public bool saveOpenPassEnabled
    {
      get => this.GetValueOrDefault<bool>(nameof (saveOpenPassEnabled), true);
      set => this.AddOrUpdateValue(nameof (saveOpenPassEnabled), (object) value);
    }

    public int saveAddWalletOption
    {
      get => this.GetValueOrDefault<int>(nameof (saveAddWalletOption), 1);
      set => this.AddOrUpdateValue(nameof (saveAddWalletOption), (object) value);
    }

    public bool updateAutomaticUpdates
    {
      get => this.GetValueOrDefault<bool>(nameof (updateAutomaticUpdates), true);
      set => this.AddOrUpdateValue(nameof (updateAutomaticUpdates), (object) value);
    }

    public bool updateWifiOnly
    {
      get => this.GetValueOrDefault<bool>(nameof (updateWifiOnly), false);
      set => this.AddOrUpdateValue(nameof (updateWifiOnly), (object) value);
    }

    private void NotifyProperties()
    {
      this.OnPropertyChanged("dateVisibility");
      this.OnPropertyChanged("listItemHeight");
    }

    public bool dateVisibility => this.GetValueOrDefault<bool>("listShowDatesSetting", false);

    public int listItemHeight => !this.GetValueOrDefault<bool>("listShowDatesSetting", false) ? 115 : 150;

    protected void OnPropertyChanged(string name) => this.PropertyChanged((object) this, new PropertyChangedEventArgs(name));
  }
}
