// Decompiled with JetBrains decompiler
// Type: WalletPass.reminderPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WalletPass
{
  public class reminderPage : PhoneApplicationPage
  {
    private ClaseReminderItems _reminders = new ClaseReminderItems();
    private string tipoReminder;
    private int option;
    internal Grid LayoutRoot;
    internal ListBox listReminders;
    private bool _contentLoaded;

    public reminderPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
        {
          this.tipoReminder = e.Uri.ToString();
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        }
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        StateManager.LoadStateAll((PhoneApplicationPage) this);
        this.tipoReminder = this.LoadState<string>("tipoReminderKey");
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
      if (this.tipoReminder.Contains("calendar"))
      {
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Clear();
        this.option = 0;
        for (int index = 0; index <= this._reminders.listPickerCalendarItems.Count - 1; ++index)
        {
          TextBlock textBlock = new TextBlock();
          ((FrameworkElement) textBlock).Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
          textBlock.Foreground = (Brush) solidColorBrush2;
          textBlock.FontFamily = new FontFamily("Segoe WP SemiLight");
          textBlock.FontSize = 34.0;
          textBlock.Text = this._reminders.listPickerCalendarItems[index];
          ((FrameworkElement) textBlock).Tag = (object) index;
          ((UIElement) textBlock).Tap += new EventHandler<GestureEventArgs>(this.txt_Tap);
          ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Add((object) textBlock);
        }
      }
      else if (this.tipoReminder.Contains("notificationAlarm"))
      {
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Clear();
        for (int index = 0; index <= this._reminders.listPickerNotificationItems.Count - 1; ++index)
        {
          TextBlock textBlock = new TextBlock();
          this.option = 1;
          ((FrameworkElement) textBlock).Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
          textBlock.Foreground = (Brush) solidColorBrush2;
          textBlock.FontFamily = new FontFamily("Segoe WP SemiLight");
          textBlock.FontSize = 34.0;
          textBlock.Text = this._reminders.listPickerNotificationItems[index];
          ((FrameworkElement) textBlock).Tag = (object) index;
          ((UIElement) textBlock).Tap += new EventHandler<GestureEventArgs>(this.txt_Tap);
          ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Add((object) textBlock);
        }
      }
      else
      {
        if (!this.tipoReminder.Contains("notificationExpired"))
          return;
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Clear();
        for (int index = 0; index <= this._reminders.listPickerNotificationItems.Count - 1; ++index)
        {
          TextBlock textBlock = new TextBlock();
          this.option = 2;
          ((FrameworkElement) textBlock).Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
          textBlock.Foreground = (Brush) solidColorBrush2;
          textBlock.FontFamily = new FontFamily("Segoe WP SemiLight");
          textBlock.FontSize = 34.0;
          textBlock.Text = this._reminders.listPickerNotificationItems[index];
          ((FrameworkElement) textBlock).Tag = (object) index;
          ((UIElement) textBlock).Tap += new EventHandler<GestureEventArgs>(this.txt_Tap);
          ((PresentationFrameworkCollection<object>) ((ItemsControl) this.listReminders).Items).Add((object) textBlock);
        }
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      StateManager.SaveStateAll((PhoneApplicationPage) this);
      this.SaveState("tipoReminderKey", (object) this.tipoReminder);
    }

    private void txt_Tap(object sender, GestureEventArgs e)
    {
      TextBlock textBlock = (TextBlock) sender;
      AppSettings appSettings = new AppSettings();
      textBlock.FontWeight = FontWeights.ExtraBold;
      switch (this.option)
      {
        case 0:
          appSettings.calendarReminder = ((Selector) this.listReminders).SelectedIndex;
          break;
        case 1:
          appSettings.notificationReminder = ((Selector) this.listReminders).SelectedIndex;
          break;
        case 2:
          appSettings.notificationReminderExpired = ((Selector) this.listReminders).SelectedIndex;
          break;
      }
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      this.showTransitionOutBackward();
      ((Page) this).NavigationService.GoBack();
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
      transition.Completed += (EventHandler) ((param0, param1) => ((UIElement) this).Opacity = 1.0);
      transition.Begin();
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/reminderPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.listReminders = (ListBox) ((FrameworkElement) this).FindName("listReminders");
    }
  }
}
