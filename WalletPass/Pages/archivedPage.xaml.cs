// Decompiled with JetBrains decompiler
// Type: WalletPass.archivedPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
//using Microsoft.Phone.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
using WalletPass.Resources;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace WalletPass
{
  public sealed partial class archivedPage : Page
  {
    private ApplicationBarIconButton _selectPlan;
    private ApplicationBarIconButton _deletePlan;
    private ApplicationBarIconButton _archivePlan;
    private ApplicationBarIconButton _sharePlan;
    private bool shareContext;
    private string _groupToFind = "";
    private bool isTombstoned = true;
    private Popup _popup;
    private Dictionary<object, archivedPage.PivotCallbacks> _callbacks;
    internal Grid LayoutRoot;
    internal Grid PassPanel;
    internal LongListMultiSelector lstPass;
    internal LongListMultiSelector lstPassGroup;
    private bool _contentLoaded;

    public archivedPage()
    {
      this.InitializeComponent();
      this._callbacks = new Dictionary<object, archivedPage.PivotCallbacks>();
      this._callbacks[(object) this.PassPanel] = new archivedPage.PivotCallbacks()
      {
        Init = new Action(this.CreatePassApplicationBarItems),
        OnActivated = new Action(this.OnPassPivotItemActivated),
        OnBackKeyPress = new Action<CancelEventArgs>(this.OnPlanBackKeyPressed)
      };
      foreach (archivedPage.PivotCallbacks pivotCallbacks in this._callbacks.Values)
      {
        if (pivotCallbacks.Init != null)
          pivotCallbacks.Init();
      }
      ((UIElement) this).Opacity = 0.0;
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
      if (App._isTombStoned)
        StateManager.LoadStateAll((PhoneApplicationPage) this);
      if (!App._isTombStoned)
      {
        this.distributePassBySettings();
        if (e.NavigationMode == null)
        {
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
        }
        else
        {
          if (App._deletePasses.Count > 0)
            this.DeletePasses();
          if (App._archivePasses.Count > 0)
            this.ArchivePasses();
          ((UIElement) this).Opacity = 1.0;
        }
        this.shareContext = false;
      }
      else
      {
        ((UIElement) this).Opacity = 1.0;
        this.distributePassBySettings();
        App._isTombStoned = false;
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      if (this.isTombstoned)
        StateManager.SaveStateAll((PhoneApplicationPage) this);
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.unRegisterForShare()));
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      base.OnBackKeyPress(e);
      archivedPage.PivotCallbacks pivotCallbacks = new archivedPage.PivotCallbacks();
      if (!(this._callbacks.TryGetValue((object) this.PassPanel, out pivotCallbacks) & pivotCallbacks.OnBackKeyPress != null))
        return;
      pivotCallbacks.OnBackKeyPress(e);
    }

    private void OnItemContentTap(object sender, GestureEventArgs e) => this.loadItem(((FrameworkElement) sender).DataContext as ClasePass);

    private void OnPassListIsSelectionEnabledChanged(
      object sender,
      DependencyPropertyChangedEventArgs e)
    {
      this.SetupPassApplicationBar();
    }

    private void OnPassListSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      this.lstPass.IsSelectionEnabled = this.lstPass.SelectedItems.Count > 0;
      this.lstPassGroup.IsSelectionEnabled = this.lstPassGroup.SelectedItems.Count > 0;
      this.UpdatePassApplicationBar();
    }

    private void DeletePasses()
    {
      this._popup = new Popup();
      this._popup.Child = (UIElement) new splashUpdTilesControl(AppResources.splashDeletePass);
      this._popup.IsOpen = true;
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.DeletePasses(App._deletePasses)));
    }

    private void DeletePasses(List<ClasePass> deletePasses)
    {
      foreach (ClasePass deletePass in deletePasses)
      {
        if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + deletePass.filenamePass))
          File.Delete(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + deletePass.filenamePass);
        App._passcollectionArchived.Remove(App._passcollectionArchived.returnPass(deletePass.serialNumberGUID));
      }
      deletePasses.Clear();
      this.distributePassBySettings();
      WalletPass.IO.SaveDataArchived(App._passcollectionArchived);
      this.lstPass.IsSelectionEnabled = false;
      this.lstPassGroup.IsSelectionEnabled = false;
      this._popup.IsOpen = false;
    }

    private void ArchivePasses()
    {
      this._popup = new Popup();
      this._popup.Child = (UIElement) new splashUpdTilesControl(AppResources.splashActivePass);
      this._popup.IsOpen = true;
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => await this.ArchivePasses(App._archivePasses)));
    }

    private async Task ArchivePasses(List<ClasePass> archivePasses)
    {
      ClaseSaveCalendar claseSaveCalendar = new ClaseSaveCalendar();
      AppSettings settings = new AppSettings();
      App._passcollectionArchived = new ClasePassCollection();
      App._passcollectionArchived.AddAllNew(await WalletPass.IO.LoadDataPassesArchived());
      foreach (ClasePass archivePass in archivePasses)
      {
        ClasePass item = archivePass;
        if (!App._passcollection.passExists(item))
        {
          if (settings.saveAddWalletEnabled)
          {
            if (settings.saveAddWalletOption == 0)
            {
              this.saveWalletItem(item);
            }
            else
            {
              App._mswalletPassCollection.Add(item.serialNumberGUID);
              WalletPass.IO.SaveDataMSWallet(App._mswalletPassCollection);
              int num;
              ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => num = await ClasePassbookWebOperations.pushMSWallet(item.organizationName, item.webServiceURL) ? 1 : 0));
            }
          }
          ClaseSaveCalendar _calendarManage = new ClaseSaveCalendar();
          if (settings.calendarOnSave)
          {
            int num1 = await _calendarManage.saveAppointment(item, App._currentAppCalendar) ? 1 : 0;
          }
          App._passcollection.AddNew(item);
          int num2;
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => num2 = await ClasePassbookWebOperations.registerDevice(item) ? 1 : 0));
          App._passcollectionArchived.Remove(App._passcollectionArchived.returnPass(item.serialNumberGUID));
        }
      }
      archivePasses.Clear();
      this.distributePassBySettings();
      WalletPass.IO.SaveData(App._passcollection);
      WalletPass.IO.SaveDataArchived(App._passcollectionArchived);
      this.lstPass.IsSelectionEnabled = false;
      this.lstPassGroup.IsSelectionEnabled = false;
      this._popup.IsOpen = false;
    }

    private async void saveWalletItem(ClasePass pass)
    {
      try
      {
        WalletItem wItem = Microsoft.Phone.Wallet.Wallet.FindItem(pass.serialNumberGUID);
        if (wItem != null)
          Microsoft.Phone.Wallet.Wallet.Remove(wItem);
        Deal item = new Deal(pass.serialNumberGUID);
        ((WalletItem) item).BarcodeImage = (BitmapSource) pass.barCode;
        ((WalletItem) item).DisplayName = pass.organizationName;
        ((WalletItem) item).Logo99x99 = (BitmapSource) pass.iconImage;
        ((WalletItem) item).Logo159x159 = (BitmapSource) pass.iconImage;
        ((WalletItem) item).Logo336x336 = (BitmapSource) pass.iconImage;
        item.MerchantName = pass.organizationName;
        ((WalletItem) item).NavigationUri = new Uri("/MainPage.xaml?WalletItem=" + pass.serialNumberGUID, UriKind.Relative);
        if (pass.relevantDate.Year != 1)
          item.ExpirationDate = new DateTime?(pass.relevantDate);
        await ((WalletItem) item).SaveAsync();
      }
      catch
      {
      }
    }

    private void loadItem(ClasePass item, bool loadedFromOutside = false)
    {
      try
      {
        if (!new AppSettings().listShowGroup)
        {
          App._tempPassClass = item;
          App._pkPass = true;
        }
        else if (this.groupCount(item) == 1)
        {
          App._tempPassClass = item;
          App._pkPass = true;
        }
        else
        {
          App._tempPassGroup = this.loadEntireGroup(item);
          App._groupItemIndex = this.groupItemIndex(item);
          App._pkPassGroup = true;
        }
        this.NavigateToNextScreen(loadedFromOutside);
      }
      catch (Exception ex)
      {
      }
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
      this._selectPlan = new ApplicationBarIconButton();
      this._selectPlan.IconUri = new Uri("/Assets/Appbar/appbar.manage.png", UriKind.Relative);
      this._selectPlan.Text = AppResources.AppBarButtonSelect;
      this._selectPlan.Click += new EventHandler(this.ApplicationBarIconButtonSelectPass_Click);
      this._deletePlan = new ApplicationBarIconButton();
      this._deletePlan.IconUri = new Uri("/Assets/Appbar/appbar.delete.png", UriKind.Relative);
      this._deletePlan.Text = AppResources.AppBarButtonDelete;
      this._deletePlan.Click += new EventHandler(this.ApplicationBarIconButtonDeletePass_Click);
      this._archivePlan = new ApplicationBarIconButton();
      this._archivePlan.IconUri = new Uri("/Assets/AppBar/appbar.cabinet.remove.png", UriKind.Relative);
      this._archivePlan.Text = AppResources.AppBarButtonArchiveRemove;
      this._archivePlan.Click += new EventHandler(this.ApplicationBarIconButtonArchivePlan_Click);
      this._sharePlan = new ApplicationBarIconButton();
      this._sharePlan.IconUri = new Uri("/Assets/AppBar/appbar.share.png", UriKind.Relative);
      this._sharePlan.Text = AppResources.AppBarButtonShare;
      this._sharePlan.Click += new EventHandler(this.ApplicationBarIconButtonSharePlan_Click);
      this.SetupPassApplicationBar();
    }

    private void ApplicationBarIconButtonSharePlan_Click(object sender, EventArgs e)
    {
      this.registerForShare();
      DataTransferManager.ShowShareUI();
    }

    public void OnPassPivotItemActivated()
    {
      if (this.lstPass.IsSelectionEnabled)
      {
        this.lstPass.IsSelectionEnabled = false;
        this.lstPassGroup.IsSelectionEnabled = false;
      }
      else
        this.SetupPassApplicationBar();
    }

    private void SetupPassApplicationBar()
    {
      this.ClearApplicationBar();
      if (this.lstPass.IsSelectionEnabled | this.lstPassGroup.IsSelectionEnabled)
      {
        this.ApplicationBar.Buttons.Add((object) this._sharePlan);
        this.ApplicationBar.Buttons.Add((object) this._archivePlan);
        this.ApplicationBar.Buttons.Add((object) this._deletePlan);
        this.UpdatePassApplicationBar();
      }
      else
        this.ApplicationBar.Buttons.Add((object) this._selectPlan);
    }

    private void UpdatePassApplicationBar()
    {
      AppSettings appSettings = new AppSettings();
      if (!this.lstPass.IsSelectionEnabled && !this.lstPassGroup.IsSelectionEnabled)
        return;
      bool flag = this.lstPass.SelectedItems != null & this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems != null & this.lstPassGroup.SelectedItems.Count > 0;
      this._deletePlan.IsEnabled = flag;
      this._archivePlan.IsEnabled = flag;
      this._sharePlan.IsEnabled = flag;
    }

    protected void OnPlanBackKeyPressed(CancelEventArgs e)
    {
      if (this.lstPass.IsSelectionEnabled | this.lstPassGroup.IsSelectionEnabled)
      {
        this.lstPass.IsSelectionEnabled = false;
        this.lstPassGroup.IsSelectionEnabled = false;
        e.Cancel = true;
      }
      else
      {
        this.isTombstoned = false;
        this.showTransitionOutBackward();
      }
    }

    private void ApplicationBarIconButtonSelectPass_Click(object sender, EventArgs e)
    {
      this.lstPass.IsSelectionEnabled = true;
      this.lstPassGroup.IsSelectionEnabled = true;
    }

    private void ApplicationBarIconButtonDeletePass_Click(object sender, EventArgs e)
    {
      string str = AppResources.msgBoxDelete;
      AppSettings appSettings = new AppSettings();
      if (this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0)
        str = AppResources.msgBoxDeleteMulti;
      if (MessageBox.Show(str, AppResources.msgBoxDeleteCaption, (MessageBoxButton) 1) != 1)
        return;
      App._deletePasses.Clear();
      if (!appSettings.listShowGroup)
      {
        for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
          App._deletePasses.Add((ClasePass) this.lstPass.SelectedItems[index]);
      }
      else
      {
        for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
          App._deletePasses.Add((ClasePass) this.lstPassGroup.SelectedItems[index]);
      }
      this.DeletePasses();
    }

    private void ApplicationBarIconButtonArchivePlan_Click(object sender, EventArgs e)
    {
      string str = AppResources.msgBoxArchiveRemove;
      AppSettings appSettings = new AppSettings();
      if (this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0)
        str = AppResources.msgBoxArchivesRemove;
      if (MessageBox.Show(str, AppResources.msgBoxDeleteCaption, (MessageBoxButton) 1) != 1)
        return;
      App._archivePasses.Clear();
      if (!appSettings.listShowGroup)
      {
        for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
          App._archivePasses.Add((ClasePass) this.lstPass.SelectedItems[index]);
      }
      else
      {
        for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
          App._archivePasses.Add((ClasePass) this.lstPassGroup.SelectedItems[index]);
      }
      this.ArchivePasses();
    }

    private void itemContextMenuRemoveArchived_Click(object sender, RoutedEventArgs e)
    {
      ClasePass dataContext = (ClasePass) ((FrameworkElement) sender).DataContext;
      string boxArchiveRemove = AppResources.msgBoxArchiveRemove;
      AppSettings appSettings = new AppSettings();
      if (MessageBox.Show(boxArchiveRemove, AppResources.msgBoxDeleteCaption, (MessageBoxButton) 1) != 1)
        return;
      App._archivePasses.Clear();
      App._archivePasses.Add(dataContext);
      this.ArchivePasses();
    }

    private void itemContextMenuShare_Click(object sender, RoutedEventArgs e)
    {
      this.registerForShare();
      this.shareContext = true;
      App._tempPassClass = (ClasePass) ((FrameworkElement) sender).DataContext;
      DataTransferManager.ShowShareUI();
    }

    private void itemContextMenuDelete_Click(object sender, RoutedEventArgs e)
    {
      object dataContext = ((FrameworkElement) sender).DataContext;
      string msgBoxDelete = AppResources.msgBoxDelete;
      AppSettings appSettings = new AppSettings();
      if (MessageBox.Show(msgBoxDelete, AppResources.msgBoxDeleteCaption, (MessageBoxButton) 1) != 1)
        return;
      ClasePass clasePass = (ClasePass) dataContext;
      App._deletePasses.Clear();
      App._deletePasses.Add(clasePass);
      this.DeletePasses();
    }

    private void registerForShare()
    {
      DataTransferManager forCurrentView = DataTransferManager.GetForCurrentView();
      // ISSUE: method pointer
      WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Func<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, EventRegistrationToken>(forCurrentView.add_DataRequested), new Action<EventRegistrationToken>(forCurrentView.remove_DataRequested), new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object) this, __methodptr(ShareStorageItemsHandler)));
    }

    private void unRegisterForShare() => WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Action<EventRegistrationToken>(DataTransferManager.GetForCurrentView().remove_DataRequested), new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object) this, __methodptr(ShareStorageItemsHandler)));

    private async void ShareStorageItemsHandler(
      DataTransferManager sender,
      DataRequestedEventArgs e)
    {
      if (this.shareContext)
      {
        DataRequest request = e.Request;
        request.Data.Properties.put_Title(App._tempPassClass.organizationName + " passbook");
        request.Data.Properties.put_Description("Passbook share");
        DataRequestDeferral deferral = request.GetDeferral();
        try
        {
          File.Copy(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + App._tempPassClass.filenamePass, ApplicationData.Current.LocalFolder.Path + "\\" + App._tempPassClass.filenamePass, true);
          StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(App._tempPassClass.filenamePass);
          request.Data.SetStorageItems((IEnumerable<IStorageItem>) new List<StorageFile>()
          {
            sourceFile
          });
        }
        finally
        {
          deferral.Complete();
          this.unRegisterForShare();
        }
      }
      else
      {
        if (!(this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0))
          return;
        AppSettings _settings = new AppSettings();
        DataRequest request = e.Request;
        request.Data.Properties.put_Title("Passbooks");
        request.Data.Properties.put_Description("Passbook share");
        DataRequestDeferral deferral = request.GetDeferral();
        try
        {
          List<StorageFile> storageItems = new List<StorageFile>();
          if (_settings.listShowGroup)
          {
            for (int i = 0; i < this.lstPassGroup.SelectedItems.Count; ++i)
            {
              ClasePass clsPass = (ClasePass) this.lstPassGroup.SelectedItems[i];
              try
              {
                File.Copy(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + clsPass.filenamePass, ApplicationData.Current.LocalFolder.Path + "\\" + clsPass.filenamePass, true);
                StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(clsPass.filenamePass);
                storageItems.Add(sourceFile);
              }
              catch
              {
              }
            }
          }
          else
          {
            for (int i = 0; i < this.lstPass.SelectedItems.Count; ++i)
            {
              ClasePass clsPass = (ClasePass) this.lstPass.SelectedItems[i];
              try
              {
                File.Copy(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + clsPass.filenamePass, ApplicationData.Current.LocalFolder.Path + "\\" + clsPass.filenamePass, true);
                StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(clsPass.filenamePass);
                storageItems.Add(sourceFile);
              }
              catch
              {
              }
            }
          }
          request.Data.SetStorageItems((IEnumerable<IStorageItem>) storageItems);
        }
        finally
        {
          deferral.Complete();
          this.unRegisterForShare();
        }
      }
    }

    private void deleteTempFiles()
    {
      List<string> list = ((IEnumerable<string>) Directory.GetFiles(ApplicationData.Current.LocalFolder.Path, "*.*")).Where<string>((Func<string, bool>) (file => file.ToLower().EndsWith("pkpass"))).ToList<string>();
      for (int index = 0; index < list.Count; ++index)
        File.Delete(list[index]);
    }

    private bool canShare()
    {
      if (!(this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0))
        return false;
      if (!new AppSettings().listShowGroup)
      {
        for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
        {
          if (string.IsNullOrEmpty(((ClasePass) this.lstPass.SelectedItems[index]).filenamePass))
            return false;
        }
        return true;
      }
      for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
      {
        if (string.IsNullOrEmpty(((ClasePass) this.lstPassGroup.SelectedItems[index]).filenamePass))
          return false;
      }
      return true;
    }

    private void distributePassBySettings()
    {
      AppSettings appSettings = new AppSettings();
      if (!appSettings.listShowGroup)
      {
        ((UIElement) this.lstPass).Visibility = (Visibility) 0;
        ((UIElement) this.lstPassGroup).Visibility = (Visibility) 1;
        App._groupItemIndex = -1;
        App._passcollectionArchived.updateSettings();
        this.lstPass.ItemsSource = (IList) App._passcollectionArchived;
      }
      else
      {
        List<ClaseGroup<ClasePass>> claseGroupList = new List<ClaseGroup<ClasePass>>();
        App._passcollectionArchived.updateSettings();
        for (int index1 = 0; index1 <= App._passcollectionArchived.Count - 1; ++index1)
        {
          switch (appSettings.listGroupBy)
          {
            case 0:
              this._groupToFind = App._passcollectionArchived[index1].organizationName;
              break;
            case 1:
              this._groupToFind = App._passcollectionArchived[index1].type;
              break;
          }
          if (claseGroupList.Find(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName)) == null)
          {
            ClaseGroup<ClasePass> claseGroup;
            if (appSettings.listGroupBy == 0)
            {
              claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, App._passcollectionArchived[index1].iconImage);
            }
            else
            {
              StringToColorConverter toColorConverter = new StringToColorConverter();
              claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, App._passcollectionArchived[index1].imageType, (SolidColorBrush) toColorConverter.Convert((object) appSettings.themeColorForeground, (Type) null, (object) null, (CultureInfo) null));
            }
            claseGroup.Add(App._passcollectionArchived[index1]);
            switch (appSettings.listGroupOrder)
            {
              case 0:
                claseGroupList.Add(claseGroup);
                continue;
              case 1:
                bool flag = false;
                for (int index2 = 0; index2 <= claseGroupList.Count - 1; ++index2)
                {
                  if (string.Compare(claseGroup.Title, claseGroupList[index2].Title) < 0)
                  {
                    claseGroupList.Insert(index2, claseGroup);
                    index2 = claseGroupList.Count;
                    flag = true;
                  }
                }
                if (!flag)
                {
                  claseGroupList.Add(claseGroup);
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
          else
            claseGroupList[claseGroupList.FindIndex(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName))].Add(App._passcollectionArchived[index1]);
        }
        ((UIElement) this.lstPass).Visibility = (Visibility) 1;
        ((UIElement) this.lstPassGroup).Visibility = (Visibility) 0;
        this.lstPassGroup.ItemsSource = (IList) claseGroupList;
      }
    }

    private bool findGroupName(ClaseGroup<ClasePass> gr) => gr.Title.ToString().ToLower() == this._groupToFind.ToLower();

    private ClaseGroup<ClasePass> loadEntireGroup(ClasePass item)
    {
      List<ClaseGroup<ClasePass>> itemsSource = (List<ClaseGroup<ClasePass>>) this.lstPassGroup.ItemsSource;
      for (int index = 0; index <= itemsSource.Count - 1; ++index)
      {
        if (itemsSource[index].Contains(item))
          return itemsSource[index];
      }
      return (ClaseGroup<ClasePass>) null;
    }

    private int groupCount(ClasePass item)
    {
      List<ClaseGroup<ClasePass>> itemsSource = (List<ClaseGroup<ClasePass>>) this.lstPassGroup.ItemsSource;
      for (int index = 0; index <= itemsSource.Count - 1; ++index)
      {
        if (itemsSource[index].Contains(item))
          return itemsSource[index].Count;
      }
      return -1;
    }

    private int groupItemIndex(ClasePass item)
    {
      List<ClaseGroup<ClasePass>> itemsSource = (List<ClaseGroup<ClasePass>>) this.lstPassGroup.ItemsSource;
      for (int index = 0; index <= itemsSource.Count - 1; ++index)
      {
        if (itemsSource[index].Contains(item))
          return itemsSource[index].IndexOf(item);
      }
      return -1;
    }

    private void NavigateToNextScreen(bool loadedFromOutside)
    {
      this.showTransitionTurnstile();
      if (App._pkPass)
      {
        if (loadedFromOutside)
          ((Page) this).NavigationService.Navigate(new Uri("/pkPassPage.xaml", UriKind.Relative));
        else
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/pkPassPage.xaml", UriKind.Relative))));
      }
      else
      {
        if (!App._pkPassGroup)
          return;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/pkPassGroupPage.xaml", UriKind.Relative))));
      }
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

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/archivedPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.PassPanel = (Grid) ((FrameworkElement) this).FindName("PassPanel");
      this.lstPass = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPass");
      this.lstPassGroup = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassGroup");
    }

    public class PivotCallbacks
    {
      public Action Init;
      public Action OnActivated;
      public Action<CancelEventArgs> OnBackKeyPress;
    }
  }
}
