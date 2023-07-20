// Decompiled with JetBrains decompiler
// Type: WalletPass.infoPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WalletPass.Resources;

namespace WalletPass
{
  public class infoPage : PhoneApplicationPage
  {
    private bool change;
    private bool isTombstoned = true;
    private TileUpdate tileCreat = new TileUpdate();
    private bool isMoving;
    private double manipulationStart;
    private double manipulationEnd;
    internal Style ListBoxStyle1;
    internal Storyboard hideToggleButtons;
    internal Storyboard showToggleButtons;
    internal Storyboard toggleEnabledNotification;
    internal Storyboard toggleEnabledUpdate;
    internal Storyboard toggleDisabledNotification;
    internal Storyboard toggleDisabledUpdate;
    internal Grid LayoutRoot;
    internal PullDownToRefreshPanel refreshPanel;
    internal ListBox infoList;
    internal Grid toggleStack;
    internal TextBlock txtLastUpdate;
    internal ProgressBar progressBarLastUpdate;
    internal Grid toggleNotification;
    internal Button btnToggleSwitchNotification;
    internal Path toggleSwitchNotification;
    internal Grid toggleUpdates;
    internal Button btnToggleSwitchUpdate;
    internal Path toggleSwitchUpdate;
    internal Canvas canvasNotif;
    private bool _contentLoaded;

    public infoPage()
    {
      this.InitializeComponent();
      TouchPanel.EnabledGestures = GestureType.DragComplete;
      TouchPanel.EnabledGestures = GestureType.HorizontalDrag;
      App._infoPage = true;
      if (App._pageEntry || App._archived)
      {
        ((UIElement) this.toggleStack).Visibility = (Visibility) 1;
        ((UIElement) this.refreshPanel).Visibility = (Visibility) 1;
      }
      else
      {
        ((UIElement) this.toggleStack).Visibility = (Visibility) 0;
        ((UIElement) this.refreshPanel).Visibility = (Visibility) 0;
      }
      ((UIElement) this).Opacity = 0.0;
    }

    private void updateUpdText()
    {
      DateTime dateTime = App._groupItemIndex == -1 ? App._tempPassClass.dateModified : App._tempPassGroup[App._groupItemIndex].dateModified;
      if (DateTime.Now - dateTime < TimeSpan.FromMinutes(30.0))
        this.txtLastUpdate.Text = AppResources.updateTextUpdateFewMoments;
      else if (DateTime.Now - dateTime <= TimeSpan.FromDays(1.0))
        this.txtLastUpdate.Text = AppResources.updateTextUpdatedToday;
      else if (DateTime.Now - dateTime < TimeSpan.FromDays(2.0))
        this.txtLastUpdate.Text = AppResources.updateTextUpdatedYesterday;
      else
        this.txtLastUpdate.Text = AppResources.updateTextUpdated + " " + dateTime.ToString("d");
    }

    public void initializePage()
    {
      if (App._groupItemIndex != -1)
      {
        App._tempPassGroup[App._groupItemIndex].getBackPass();
        ((ItemsControl) this.infoList).ItemsSource = (IEnumerable) App._tempPassGroup[App._groupItemIndex].passBackPageFields;
      }
      else
      {
        App._tempPassClass.getBackPass();
        ((ItemsControl) this.infoList).ItemsSource = (IEnumerable) App._tempPassClass.passBackPageFields;
      }
      this.updateUpdText();
    }

    private void passBackPageRender_MouseMove(object sender, MouseEventArgs e) => e.GetPosition((UIElement) null);

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      if (!App._isTombStoned)
      {
        if (e.NavigationMode != null)
          return;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
        {
          TurnOverTransition turnOverTransition = new TurnOverTransition();
          turnOverTransition.Mode = !App._rightGesture ? TurnOverTransitionMode.F90ToLeft : TurnOverTransitionMode.F90ToRight;
          PhoneApplicationPage content = (PhoneApplicationPage) e.Content;
          ITransition transition = turnOverTransition.GetTransition((UIElement) content);
          transition.Completed += (EventHandler) ((param0, param1) =>
          {
            ((UIElement) this).Opacity = 1.0;
            ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.initializePage()));
            if (App._groupItemIndex != -1)
            {
              ((UIElement) this.toggleSwitchNotification).Opacity = App._tempPassGroup[App._groupItemIndex].showNotifications ? 1.0 : 0.0;
              ((UIElement) this.toggleSwitchUpdate).Opacity = App._tempPassGroup[App._groupItemIndex].registered ? 1.0 : 0.0;
            }
            else
            {
              ((UIElement) this.toggleSwitchNotification).Opacity = App._tempPassClass.showNotifications ? 1.0 : 0.0;
              ((UIElement) this.toggleSwitchUpdate).Opacity = App._tempPassClass.registered ? 1.0 : 0.0;
            }
            this.change = false;
          });
          transition.Begin();
        }));
      }
      else
      {
        StateManager.LoadStateAll((PhoneApplicationPage) this);
        this.change = this.LoadState<bool>("changeKey");
        App._reconstructPages = true;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
        {
          ((UIElement) this).Opacity = 1.0;
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.initializePage()));
          if (App._groupItemIndex != -1)
          {
            ((UIElement) this.toggleSwitchNotification).Opacity = App._tempPassGroup[App._groupItemIndex].showNotifications ? 1.0 : 0.0;
            ((UIElement) this.toggleSwitchUpdate).Opacity = App._tempPassGroup[App._groupItemIndex].registered ? 1.0 : 0.0;
          }
          else
          {
            ((UIElement) this.toggleSwitchNotification).Opacity = App._tempPassClass.showNotifications ? 1.0 : 0.0;
            ((UIElement) this.toggleSwitchUpdate).Opacity = App._tempPassClass.registered ? 1.0 : 0.0;
          }
          this.change = false;
        }));
        App._isTombStoned = false;
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      if (!this.isTombstoned)
        return;
      StateManager.SaveStateAll((PhoneApplicationPage) this);
      this.SaveState("changeKey", (object) this.change);
    }

    private void infoPage_BackKeyPress(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
      App._rightGesture = Convert.ToBoolean(new Random().Next(0, 2));
      this.isTombstoned = false;
      this.infoPageBackKeyPress();
    }

    private void infoPageBackKeyPress()
    {
      if (this.change)
        IO.SaveData(App._passcollection);
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
      {
        TurnOverTransition turnOverTransition = new TurnOverTransition();
        turnOverTransition.Mode = !App._rightGesture ? TurnOverTransitionMode.F0ToLeft : TurnOverTransitionMode.F0ToRight;
        PhoneApplicationPage element = (PhoneApplicationPage) this;
        ITransition transition = turnOverTransition.GetTransition((UIElement) element);
        transition.Completed += (EventHandler) ((param0, param1) =>
        {
          if (!((Page) this).NavigationService.CanGoBack)
            return;
          ((Page) this).NavigationService.GoBack();
        });
        transition.Begin();
      }));
    }

    private void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      double num1 = 0.0;
      int num2 = 0;
      while (TouchPanel.IsGestureAvailable)
      {
        GestureSample gestureSample = TouchPanel.ReadGesture();
        if (gestureSample.GestureType == GestureType.HorizontalDrag && gestureSample.GestureType == GestureType.HorizontalDrag)
        {
          num1 += (double) gestureSample.Delta.X;
          ++num2;
        }
      }
      double num3 = num1 / (double) num2;
      if (Math.Abs(num3) <= 10.0)
        return;
      App._rightGesture = num3 > 0.0;
      this.infoPageBackKeyPress();
    }

    private void showTaskBarText(string text) => SystemTray.SetProgressIndicator((DependencyObject) this, new ProgressIndicator()
    {
      Text = text + "...",
      IsVisible = true,
      IsIndeterminate = true,
      Value = 0.0
    });

    private void hideTaskBarText() => SystemTray.ProgressIndicator.IsVisible = false;

    private async void btnToggleSwitchUpdate_Click(object sender, RoutedEventArgs e)
    {
      ((UIElement) this.txtLastUpdate).Visibility = (Visibility) 1;
      ((UIElement) this.progressBarLastUpdate).Visibility = (Visibility) 0;
      bool registered = App._groupItemIndex != -1 ? App._tempPassGroup[App._groupItemIndex].registered : App._tempPassClass.registered;
      if (!registered)
      {
        this.showTaskBarText(AppResources.updateRegisteringDevice);
        bool resultRegister = false;
        if (App._groupItemIndex != -1)
        {
          resultRegister = await ClasePassbookWebOperations.registerDevice(App._tempPassGroup[App._groupItemIndex], true);
          App._tempPassGroup[App._groupItemIndex].registered = resultRegister;
          if (resultRegister)
            this.toggleEnabledUpdate.Begin();
          this.change = resultRegister;
        }
        else
        {
          resultRegister = await ClasePassbookWebOperations.registerDevice(App._tempPassClass, true);
          App._tempPassClass.registered = resultRegister;
          if (resultRegister)
            this.toggleEnabledUpdate.Begin();
          this.change = resultRegister;
        }
        this.hideTaskBarText();
      }
      else
      {
        bool resultUnregister = false;
        this.showTaskBarText(AppResources.updateUnregisteringDevice);
        if (App._groupItemIndex != -1)
        {
          resultUnregister = await ClasePassbookWebOperations.unregisterDevice(App._tempPassGroup[App._groupItemIndex]);
          App._tempPassGroup[App._groupItemIndex].registered = !resultUnregister;
          if (resultUnregister)
            this.toggleDisabledUpdate.Begin();
          this.change = resultUnregister;
        }
        else
        {
          resultUnregister = await ClasePassbookWebOperations.unregisterDevice(App._tempPassClass);
          App._tempPassClass.registered = !resultUnregister;
          if (resultUnregister)
            this.toggleDisabledUpdate.Begin();
          this.change = resultUnregister;
        }
        this.hideTaskBarText();
      }
      ((UIElement) this.txtLastUpdate).Visibility = (Visibility) 0;
      ((UIElement) this.progressBarLastUpdate).Visibility = (Visibility) 1;
    }

    private void btnToggleSwitchNotification_Click(object sender, RoutedEventArgs e)
    {
      if (App._groupItemIndex != -1 ? App._tempPassGroup[App._groupItemIndex].showNotifications : App._tempPassClass.showNotifications)
      {
        ClaseGeofence claseGeofence = new ClaseGeofence();
        if (App._groupItemIndex != -1)
        {
          App._tempPassGroup[App._groupItemIndex].showNotifications = false;
          claseGeofence.removeGeofences(App._tempPassGroup[App._groupItemIndex].serialNumberGUID);
        }
        else
        {
          App._tempPassClass.showNotifications = false;
          claseGeofence.removeGeofences(App._tempPassClass.serialNumberGUID);
        }
        this.toggleDisabledNotification.Begin();
        this.change = true;
      }
      else
      {
        ClaseGeofence claseGeofence = new ClaseGeofence();
        if (App._groupItemIndex != -1)
        {
          App._tempPassGroup[App._groupItemIndex].showNotifications = true;
          claseGeofence.createGeofences(App._tempPassGroup[App._groupItemIndex].serialNumberGUID, App._tempPassGroup[App._groupItemIndex].Locations);
        }
        else
        {
          App._tempPassClass.showNotifications = true;
          claseGeofence.createGeofences(App._tempPassClass.serialNumberGUID, App._tempPassClass.Locations);
        }
        this.toggleEnabledNotification.Begin();
        this.change = true;
      }
    }

    private async void resfreshPanel_RefreshRequested(object sender, EventArgs e)
    {
      ((UIElement) this.txtLastUpdate).Visibility = (Visibility) 1;
      ((UIElement) this.progressBarLastUpdate).Visibility = (Visibility) 0;
      int result;
      if (App._groupItemIndex != -1)
        result = await ClasePassbookWebOperations.updatePassbook(App._tempPassGroup[App._groupItemIndex]);
      else
        result = await ClasePassbookWebOperations.updatePassbook(App._tempPassClass);
      if (result != -1)
      {
        App._reconstructPages = true;
        IO.SaveData(App._passcollection);
        if (App._groupItemIndex != -1)
        {
          App._tempPassGroup[App._groupItemIndex] = App._passcollection[result];
          foreach (ShellTile activeTile in ShellTile.ActiveTiles)
          {
            if (activeTile.NavigationUri.ToString().Contains("SecondaryTile") && activeTile.NavigationUri.ToString().Contains(App._tempPassGroup[App._groupItemIndex].serialNumberGUID) && activeTile.NavigationUri.ToString() != "/")
            {
              App._tempPassClass = App._tempPassGroup[App._groupItemIndex];
              this.tileCreat.RenderWideTile();
              this.tileCreat.RenderMediumTile();
              this.tileCreat.RenderSmallTile();
              FlipTileData flipTileData1 = new FlipTileData();
              ((ShellTileData) flipTileData1).Title = "";
              ((StandardTileData) flipTileData1).BackgroundImage = this.tileCreat.ImageFront;
              ((StandardTileData) flipTileData1).BackBackgroundImage = this.tileCreat.ImageBack;
              flipTileData1.WideBackgroundImage = this.tileCreat.WideImageFront;
              flipTileData1.WideBackBackgroundImage = this.tileCreat.WideImageBack;
              FlipTileData flipTileData2 = flipTileData1;
              activeTile.Update((ShellTileData) flipTileData2);
            }
          }
        }
        else
        {
          App._tempPassClass = App._passcollection[result];
          foreach (ShellTile activeTile in ShellTile.ActiveTiles)
          {
            if (activeTile.NavigationUri.ToString().Contains("SecondaryTile") && activeTile.NavigationUri.ToString().Contains(App._tempPassClass.serialNumberGUID) && activeTile.NavigationUri.ToString() != "/")
            {
              this.tileCreat.RenderWideTile();
              this.tileCreat.RenderMediumTile();
              this.tileCreat.RenderSmallTile();
              FlipTileData flipTileData3 = new FlipTileData();
              ((ShellTileData) flipTileData3).Title = "";
              ((StandardTileData) flipTileData3).BackgroundImage = this.tileCreat.ImageFront;
              ((StandardTileData) flipTileData3).BackBackgroundImage = this.tileCreat.ImageBack;
              flipTileData3.WideBackgroundImage = this.tileCreat.WideImageFront;
              flipTileData3.WideBackBackgroundImage = this.tileCreat.WideImageBack;
              FlipTileData flipTileData4 = flipTileData3;
              activeTile.Update((ShellTileData) flipTileData4);
            }
          }
        }
        this.updateUpdText();
      }
      ((UIElement) this.txtLastUpdate).Visibility = (Visibility) 0;
      ((UIElement) this.progressBarLastUpdate).Visibility = (Visibility) 1;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/infoPage.xaml", UriKind.Relative));
      this.ListBoxStyle1 = (Style) ((FrameworkElement) this).FindName("ListBoxStyle1");
      this.hideToggleButtons = (Storyboard) ((FrameworkElement) this).FindName("hideToggleButtons");
      this.showToggleButtons = (Storyboard) ((FrameworkElement) this).FindName("showToggleButtons");
      this.toggleEnabledNotification = (Storyboard) ((FrameworkElement) this).FindName("toggleEnabledNotification");
      this.toggleEnabledUpdate = (Storyboard) ((FrameworkElement) this).FindName("toggleEnabledUpdate");
      this.toggleDisabledNotification = (Storyboard) ((FrameworkElement) this).FindName("toggleDisabledNotification");
      this.toggleDisabledUpdate = (Storyboard) ((FrameworkElement) this).FindName("toggleDisabledUpdate");
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.refreshPanel = (PullDownToRefreshPanel) ((FrameworkElement) this).FindName("refreshPanel");
      this.infoList = (ListBox) ((FrameworkElement) this).FindName("infoList");
      this.toggleStack = (Grid) ((FrameworkElement) this).FindName("toggleStack");
      this.txtLastUpdate = (TextBlock) ((FrameworkElement) this).FindName("txtLastUpdate");
      this.progressBarLastUpdate = (ProgressBar) ((FrameworkElement) this).FindName("progressBarLastUpdate");
      this.toggleNotification = (Grid) ((FrameworkElement) this).FindName("toggleNotification");
      this.btnToggleSwitchNotification = (Button) ((FrameworkElement) this).FindName("btnToggleSwitchNotification");
      this.toggleSwitchNotification = (Path) ((FrameworkElement) this).FindName("toggleSwitchNotification");
      this.toggleUpdates = (Grid) ((FrameworkElement) this).FindName("toggleUpdates");
      this.btnToggleSwitchUpdate = (Button) ((FrameworkElement) this).FindName("btnToggleSwitchUpdate");
      this.toggleSwitchUpdate = (Path) ((FrameworkElement) this).FindName("toggleSwitchUpdate");
      this.canvasNotif = (Canvas) ((FrameworkElement) this).FindName("canvasNotif");
    }
  }
}
