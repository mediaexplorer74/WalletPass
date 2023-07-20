// Decompiled with JetBrains decompiler
// Type: WalletPass.confCalendarPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace WalletPass
{
  public sealed partial class confCalendarPage : Page
  {
    //internal Grid LayoutRoot;
    //internal Button btnHelp;
    //internal Rectangle imgBtnHelp;
    //internal ToggleSwitch toggleSwitchCalendarAutoSave;
    //internal Button btnCalendarAlarm;
    //internal ListPicker listPickerCalendarState;
    //private bool _contentLoaded;

    public confCalendarPage()
    {
      this.InitializeComponent();
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      if (!App._isTombStoned)
      {
        if (e.NavigationMode == null)
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        else
          ((UIElement) this).Opacity = 1.0;
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        App._isTombStoned = false;
      }
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      ((ContentControl) this.btnCalendarAlarm).Content = (object) new ClaseReminderItems().listPickerCalendarItem(appSettings.calendarReminder);
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      this.showTransitionOutBackward();
      base.OnBackKeyPress(e);
    }

    private void btnCalendarAlarm_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/reminderPage.xaml?calendar", UriKind.Relative))));
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

    private void showTransitionTurnstile()
    {
      TurnstileTransition turnstileTransition = new TurnstileTransition();
      turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      ITransition trans = turnstileTransition.GetTransition((UIElement) element);
      trans.Completed += (EventHandler) ((param0, param1) => trans.Stop());
      trans.Begin();
    }

    private void btnHelp_Tap(object sender, GestureEventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/Tutorials/tutorialConfCalendar.xaml", UriKind.Relative))));
    }

    private void btnHelp_ManipulationStarted(object sender, ManipulationStartedEventArgs e) => ((Shape) this.imgBtnHelp).Fill = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorMain, (Type) null, (object) null, (CultureInfo) null);

    private void btnHelp_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e) => ((Shape) this.imgBtnHelp).Fill = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorForeground, (Type) null, (object) null, (CultureInfo) null);

    private void listPickerCalendarState_SelectionChanged(
      object sender,
      SelectionChangedEventArgs e)
    {
      AppSettings appSettings = new AppSettings();
      if (this.listPickerCalendarState == null)
        return;
      if (e.AddedItems.Count > 0)
        appSettings.calendarState = this.listPickerCalendarState.SelectedIndex;
      else
        this.listPickerCalendarState.SelectedIndex = appSettings.calendarState;
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/confPages/confCalendarPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnHelp = (Button) ((FrameworkElement) this).FindName("btnHelp");
      this.imgBtnHelp = (Rectangle) ((FrameworkElement) this).FindName("imgBtnHelp");
      this.toggleSwitchCalendarAutoSave = (ToggleSwitch) ((FrameworkElement) this).FindName("toggleSwitchCalendarAutoSave");
      this.btnCalendarAlarm = (Button) ((FrameworkElement) this).FindName("btnCalendarAlarm");
      this.listPickerCalendarState = (ListPicker) ((FrameworkElement) this).FindName("listPickerCalendarState");
    }
        */
  }
}
