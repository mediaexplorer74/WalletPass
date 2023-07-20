// WalletPass.updatePage

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Navigation;
//using System.Windows.Threading;
using WalletPass.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WalletPass
{
  public sealed partial class updatePage : Page
  {
    private ApplicationBarIconButton _btnSelect;
    private ApplicationBarIconButton _btnUpdate;
    private ApplicationBarIconButton _btnUpdateAll;
    private ApplicationBarIconButton _btnDelete;
    private ClasePassCollectionUpdate _passCollectionUpdatesPending;
    private TileUpdate tileCreat = new TileUpdate();
    private Dictionary<object, updatePage.PivotCallbacks> _callbacks;
    private DispatcherTimer _timer;
    //internal Grid LayoutRoot;
    //internal Grid pendingPivotItem;
    //internal LongListMultiSelector lstUpdatesPending;
    //private bool _contentLoaded;

    public updatePage()
    {
      this.InitializeComponent();
      this._callbacks = new Dictionary<object, updatePage.PivotCallbacks>();
      this._callbacks[(object) this.pendingPivotItem] = new updatePage.PivotCallbacks()
      {
        Init = new Action(this.CreatePassApplicationBarItems),
        OnActivated = new Action(this.OnPassPivotItemActivated),
        OnBackKeyPress = new Action<CancelEventArgs>(this.OnPlanBackKeyPressed)
      };
      foreach (updatePage.PivotCallbacks pivotCallbacks in this._callbacks.Values)
      {
        if (pivotCallbacks.Init != null)
          pivotCallbacks.Init();
      }
      this._timer = new DispatcherTimer();
      this._timer.Tick += new EventHandler(this.OnTimerTick);
      this._timer.Interval = TimeSpan.FromSeconds(2.0);
      this._passCollectionUpdatesPending = new ClasePassCollectionUpdate();
      for (int index = 0; index < App._updatePassCollection.Count; ++index)
        this._passCollectionUpdatesPending.Add(App._passcollection.returnPass(App._updatePassCollection[index].serialNumberGUID));
    }

    protected virtual void OnNavigatedTo(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedTo(e);
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      if (e.NavigationMode != null)
        return;
      SolidColorBrush solidColorBrush1 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorHeader, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush2 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null);
      SolidColorBrush solidColorBrush3 = (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorMain, (Type) null, (object) null, (CultureInfo) null);
      SystemTray.BackgroundColor = solidColorBrush1.Color;
      SystemTray.ForegroundColor = solidColorBrush2.Color;
      this.ApplicationBar.BackgroundColor = solidColorBrush3.Color;
      this.ApplicationBar.ForegroundColor = solidColorBrush2.Color;
      this.lstUpdatesPending.ItemsSource = (IList) this._passCollectionUpdatesPending;
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      base.OnBackKeyPress(e);
      e.Cancel = true;
      updatePage.PivotCallbacks pivotCallbacks = new updatePage.PivotCallbacks();
      if (!(this._callbacks.TryGetValue((object) this.pendingPivotItem, out pivotCallbacks) & pivotCallbacks.OnBackKeyPress != null))
        return;
      pivotCallbacks.OnBackKeyPress(e);
    }

    public void ClearApplicationBar()
    {
      while (this.ApplicationBar.Buttons.Count > 0)
        this.ApplicationBar.Buttons.RemoveAt(0);
      while (this.ApplicationBar.MenuItems.Count > 0)
        this.ApplicationBar.MenuItems.RemoveAt(0);
    }

    private void CreatePassApplicationBarItems()
    {
      this._btnSelect = new ApplicationBarIconButton();
      this._btnSelect.IconUri = new Uri("/Assets/AppBar/appbar.manage.png", UriKind.Relative);
      this._btnSelect.Text = AppResources.AppBarButtonSelect;
      this._btnSelect.Click += new EventHandler(this.btnSelect_Click);
      this._btnUpdate = new ApplicationBarIconButton();
      this._btnUpdate.IconUri = new Uri("/Assets/AppBar/appbar.download.png", UriKind.Relative);
      this._btnUpdate.Text = AppResources.AppBarButtonUpdate;
      this._btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
      this._btnUpdateAll = new ApplicationBarIconButton();
      this._btnUpdateAll.IconUri = new Uri("/Assets/AppBar/appbar.download.png", UriKind.Relative);
      this._btnUpdateAll.Text = AppResources.AppBarButtonUpdateAll;
      this._btnUpdateAll.Click += new EventHandler(this.btnUpdateAll_Click);
      this._btnDelete = new ApplicationBarIconButton();
      this._btnDelete.IconUri = new Uri("/Assets/AppBar/appbar.delete.png", UriKind.Relative);
      this._btnDelete.Text = AppResources.AppBarButtonDelete;
      this._btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.SetupPassApplicationBar();
    }

    public void OnPassPivotItemActivated()
    {
      if (this.lstUpdatesPending.IsSelectionEnabled)
        this.lstUpdatesPending.IsSelectionEnabled = false;
      else
        this.SetupPassApplicationBar();
    }

    private void SetupPassApplicationBar()
    {
      this.ClearApplicationBar();
      if (this.lstUpdatesPending.IsSelectionEnabled)
      {
        this.ApplicationBar.Buttons.Add((object) this._btnUpdate);
        this.ApplicationBar.Buttons.Add((object) this._btnDelete);
        this.UpdatePassApplicationBar();
      }
      else
      {
        this.ApplicationBar.Buttons.Add((object) this._btnSelect);
        this.ApplicationBar.Buttons.Add((object) this._btnUpdateAll);
      }
    }

    private void UpdatePassApplicationBar()
    {
      AppSettings appSettings = new AppSettings();
      if (!this.lstUpdatesPending.IsSelectionEnabled)
        return;
      bool flag = this.lstUpdatesPending.SelectedItems != null & this.lstUpdatesPending.SelectedItems.Count > 0;
      this._btnUpdate.IsEnabled = flag;
      this._btnDelete.IsEnabled = flag;
    }

    protected void OnPlanBackKeyPressed(CancelEventArgs e)
    {
      if (this.lstUpdatesPending.IsSelectionEnabled)
      {
        this.lstUpdatesPending.IsSelectionEnabled = false;
        e.Cancel = true;
      }
      else
        this.backKeyPress();
    }

    private void OnPassListSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.lstUpdatesPending.IsSelectionEnabled = this.lstUpdatesPending.SelectedItems.Count > 0;
      this.UpdatePassApplicationBar();
    }

    private void OnPassListIsSelectionEnabledChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      this.SetupPassApplicationBar();
    }

    private void backKeyPress()
    {
      this.showTransitionOutBackward();
      this._timer.Stop();
      if (!((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
    }

    private void btnSelect_Click(object sender, EventArgs e) => this.lstUpdatesPending.IsSelectionEnabled = true;

    private async void btnUpdate_Click(object sender, EventArgs e)
    {
      if (this.lstUpdatesPending.SelectedItems.Count <= 0)
        return;
      int result = -1;
      ProgressIndicator prog = new ProgressIndicator()
      {
        Text = AppResources.updateTextUpdating + "...",
        IsVisible = true,
        IsIndeterminate = true,
        Value = 0.0
      };
      SystemTray.SetProgressIndicator((DependencyObject) this, prog);
      ClasePassCollectionUpdate _passesToUpdate = new ClasePassCollectionUpdate();
      for (int index = 0; index < this.lstUpdatesPending.SelectedItems.Count; ++index)
        _passesToUpdate.Add((ClasePass) this.lstUpdatesPending.SelectedItems[index]);
      result = await this.updatePassbook(_passesToUpdate);
      if (result != -1)
        prog = new ProgressIndicator()
        {
          Text = AppResources.updateTextUpdated,
          IsVisible = true,
          IsIndeterminate = false,
          Value = 0.0
        };
      else
        prog = new ProgressIndicator()
        {
          Text = AppResources.updateTextNotUpdated,
          IsVisible = true,
          IsIndeterminate = false,
          Value = 0.0
        };
      SystemTray.SetProgressIndicator((DependencyObject) this, prog);
      this._timer.Start();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (this.lstUpdatesPending.SelectedItems.Count <= 0)
        return;
      ClasePassCollectionUpdate collectionUpdate = new ClasePassCollectionUpdate();
      for (int index = 0; index < this.lstUpdatesPending.SelectedItems.Count; ++index)
        collectionUpdate.Add((ClasePass) this.lstUpdatesPending.SelectedItems[index]);
      for (int index = 0; index < collectionUpdate.Count; ++index)
      {
        this._passCollectionUpdatesPending.Remove(collectionUpdate[index]);
        App._updatePassCollection.RemoveAt(App._updatePassCollection.IndexPass(collectionUpdate[index].serialNumberGUID));
      }
      this.saveData(false);
    }

    private async void btnUpdateAll_Click(object sender, EventArgs e)
    {
      int result = -1;
      ProgressIndicator prog = new ProgressIndicator()
      {
        Text = AppResources.updateTextUpdating + "...",
        IsVisible = true,
        IsIndeterminate = true,
        Value = 0.0
      };
      SystemTray.SetProgressIndicator((DependencyObject) this, prog);
      result = await this.updatePassbook();
      if (result != -1)
        prog = new ProgressIndicator()
        {
          Text = AppResources.updateTextUpdated,
          IsVisible = true,
          IsIndeterminate = false,
          Value = 0.0
        };
      else
        prog = new ProgressIndicator()
        {
          Text = AppResources.updateTextNotUpdated,
          IsVisible = true,
          IsIndeterminate = false,
          Value = 0.0
        };
      SystemTray.SetProgressIndicator((DependencyObject) this, prog);
      this._timer.Start();
    }

    private void OnTimerTick(object sender, EventArgs args)
    {
      SystemTray.ProgressIndicator.IsVisible = false;
      this._timer.Stop();
    }

    private void btnCancel_Click(object sender, EventArgs e) => this.backKeyPress();

    private async Task<int> updatePassbook(ClasePassCollectionUpdate updateList = null)
    {
      int result = -1;
      List<ClasePass> itemsToUpdate = updateList != null ? updateList.ToList<ClasePass>() : this._passCollectionUpdatesPending.ToList<ClasePass>();
      foreach (ClasePass item in itemsToUpdate)
      {
        result = await ClasePassbookWebOperations.updatePassbook(item);
        if (result != -1)
        {
          foreach (ShellTile activeTile in ShellTile.ActiveTiles)
          {
            if (activeTile.NavigationUri.ToString().Contains("SecondaryTile") && activeTile.NavigationUri.ToString().Contains(item.serialNumberGUID) && activeTile.NavigationUri.ToString() != "/")
            {
              App._tempPassClass = App._passcollection[result];
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
          this._passCollectionUpdatesPending.Remove(item);
          App._updatePassCollection.RemoveAt(App._updatePassCollection.IndexPass(item.serialNumberGUID));
        }
      }
      this.saveData();
      return result;
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

    private void saveData(bool saveAll = true)
    {
      if (saveAll)
        IO.SaveData(App._passcollection);
      IO.SaveUpdateData(App._updatePassCollection);
    }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/updatePage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.pendingPivotItem = (Grid) ((FrameworkElement) this).FindName("pendingPivotItem");
      this.lstUpdatesPending = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstUpdatesPending");
    }

    public class PivotCallbacks
    {
      public Action Init;
      public Action OnActivated;
      public Action<CancelEventArgs> OnBackKeyPress;
    }
        */
  }
}
