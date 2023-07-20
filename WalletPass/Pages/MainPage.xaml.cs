// WalletPass.MainPage

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

using WalletPass.Resources;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using WalletPass;

//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Windows.Threading;

//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Shell;
//using Microsoft.Phone.Tasks;
//using Microsoft.Phone.Wallet;
//using Windows.Phone.System.Analytics;



namespace WalletPass
{
    public sealed partial class MainPage : Page
    {
        private ApplicationBarIconButton _selectPlan;
        private ApplicationBarIconButton _deletePlan;
        private ApplicationBarIconButton _sharePlan;
        private ApplicationBarIconButton _archivePlan;
        private ApplicationBarIconButton _archivedButton;
        private ApplicationBarIconButton _settingsButton;
        private ApplicationBarMenuItem _downloadSample;
        private ApplicationBarMenuItem _rateReview;
        private ApplicationBarMenuItem _contact;
        private string _groupToFind = "";
        private AppointmentStore _appointmentStore;
        private Popup _popup;
        private bool tutoHelp;
        private bool shareContext;
        private bool isTombstoned = true;
        private DispatcherTimer _timer;
        private TileUpdate tileCreat = new TileUpdate();
        private Dictionary<object, MainPage.PivotCallbacks> _callbacks;
        private Dictionary<object, MainPage.PivotCallbacks> _callbacksExpired;
        private Dictionary<object, MainPage.PivotCallbacks> _callbacksExpiredExp;
        /*
        internal Storyboard FlipDownload;
        internal Storyboard FlipHelp;
        internal Storyboard TextShowDownload;
        internal Storyboard TextShowHelp;
        internal Grid LayoutRoot;
        internal StackPanel btnUpdates;
        internal TextBlock txtUpdates;
        internal Pivot AppPivot;
        internal PivotItem PassPanel;
        internal LongListMultiSelector lstPass;
        internal LongListMultiSelector lstPassGroup;
        internal Pivot AppPivotExpired;
        internal PivotItem PassPanelOptExpired;
        internal LongListMultiSelector lstPassOptExpired;
        internal LongListMultiSelector lstPassGroupOptExpired;
        internal PivotItem PassPanelOptExpiredExp;
        internal LongListMultiSelector lstPassOptExpiredExp;
        internal LongListMultiSelector lstPassGroupOptExpiredExp;
        internal Grid Sample;
        internal Button btnDownHelp;
        internal Rectangle ImageDownload;
        internal Rectangle ImageHelp;
        internal TextBlock TextHelp;
        internal TextBlock TextDownload;
        internal Grid gridSplash;
        internal SolidColorBrush LayoutColor;
        internal TextBlock txtSplash;
        internal TextBlock txtSplashTemp;
        internal ProgressBar progressBar;
        private bool _contentLoaded;
        */

        public MainPage()
        {
            this.InitializeComponent();
            this._callbacks = new Dictionary<object, MainPage.PivotCallbacks>();
            this._callbacksExpired = new Dictionary<object, MainPage.PivotCallbacks>();
            this._callbacksExpiredExp = new Dictionary<object, MainPage.PivotCallbacks>();
            this._callbacks[(object)this.PassPanel] = new MainPage.PivotCallbacks()
            {
                Init = new Action(this.CreatePassApplicationBarItems),
                OnActivated = new Action(this.OnPassPivotItemActivated),
                OnBackKeyPress = new Action<CancelEventArgs>(this.OnPlanBackKeyPressed)
            };
            this._callbacksExpired[(object)this.PassPanelOptExpired] = new MainPage.PivotCallbacks()
            {
                Init = new Action(this.CreatePassApplicationBarItems),
                OnActivated = new Action(this.OnPassPivotItemActivated),
                OnBackKeyPress = new Action<CancelEventArgs>(this.OnPlanBackKeyPressed)
            };
            this._callbacksExpiredExp[(object)this.PassPanelOptExpiredExp] = new MainPage.PivotCallbacks()
            {
                Init = new Action(this.CreatePassApplicationBarItems),
                OnActivated = new Action(this.OnPassPivotItemActivated),
                OnBackKeyPress = new Action<CancelEventArgs>(this.OnPlanBackKeyPressed)
            };
            foreach (MainPage.PivotCallbacks pivotCallbacks in this._callbacks.Values)
            {
                if (pivotCallbacks.Init != null)
                    pivotCallbacks.Init();
            }
            this._timer = new DispatcherTimer();
            this._timer.Tick += new EventHandler(this.OnTimerTick);
            this._timer.Interval = TimeSpan.FromSeconds(5.0);
            App._passcollectionArchived = new ClasePassCollection();
            App._updatePassCollection = new ClasePassBackgroundTaskCollection(WalletPass.IO.LoadDataPassesUpdateBackgroundTask());
            App._mswalletPassCollection = new ClasePassMSWalletBackgroundTaskCollection(WalletPass.IO.LoadDataMSWalletPasses());
            App._mswalletDeletePassCollection = new ClasePassMSWalletBackgroundTaskCollection(WalletPass.IO.LoadDataMSWalletPassesDelete());
            if (App._updatePassCollection.Count <= 0)
                return;
            ((UIElement)this.btnUpdates).Visibility = (Visibility)0;
            this.txtUpdates.Text = App._updatePassCollection.Count.ToString();
        }

        private async void InitializeAll()
        {
            IO_Ant _IOAnt = new IO_Ant();
            ClaseGeofence claseGeofence = new ClaseGeofence();
            VersionInfo _antVersion = WalletPass.IO.LoadDataShowTutorial();
            AppSettings settings = new AppSettings();
            if (_antVersion.mayorVersion() == 1 & _antVersion.minorVersion() < 9)
            {
                App._passcollection = new ClasePassCollection();
                App._passcollection.AddAllNew(_IOAnt.LoadDataPasses());
                WalletPass.IO.SaveData(App._passcollection);
            }
            else if (_antVersion.mayorVersion() == 2 & _antVersion.minorVersion() < 4)
            {
                ((UIElement)this.gridSplash).Visibility = (Visibility)0;
                this.txtSplash.Text = AppResources.updateRegisteringDevice;
                for (int i = 0; i < App._passcollection.Count; ++i)
                {
                    this.txtSplashTemp.Text = App._passcollection[i].organizationName + " passbook  (" + (object)(i + 1) + "/" + (object)App._passcollection.Count + ")";
                    int num = await ClasePassbookWebOperations.registerDevice(App._passcollection[i], true) ? 1 : 0;
                }
              ((UIElement)this.gridSplash).Visibility = (Visibility)1;
                _antVersion.version = this.GetVersionNumber();
                WalletPass.IO.SaveDataShowTutorial(_antVersion);
            }
          ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => App._passcollection.getFrontPasses()));
            this.distributePassBySettings();
            if (App._passcollection.Count == 0)
            {
                ((UIElement)this.Sample).Visibility = (Visibility)0;
                this.tutoHelp = true;
                this._timer.Start();
                _antVersion.version = this.GetVersionNumber();
                WalletPass.IO.SaveDataShowTutorial(_antVersion);
            }
            for (int index = 0; index < App._passcollection.Count; ++index)
            {
                if (App._passcollection[index].isUpdated)
                {
                    foreach (ShellTile activeTile in ShellTile.ActiveTiles)
                    {
                        if (activeTile.NavigationUri.ToString().Contains("SecondaryTile") && activeTile.NavigationUri.ToString().Contains(App._passcollection[index].serialNumberGUID) && activeTile.NavigationUri.ToString() != "/")
                        {
                            App._tempPassClass = App._passcollection[index];
                            this.tileCreat.RenderWideTile();
                            this.tileCreat.RenderMediumTile();
                            this.tileCreat.RenderSmallTile();
                            FlipTileData flipTileData1 = new FlipTileData();
                            ((ShellTileData)flipTileData1).Title = "";
                            ((StandardTileData)flipTileData1).BackgroundImage = this.tileCreat.ImageFront;
                            ((StandardTileData)flipTileData1).BackBackgroundImage = this.tileCreat.ImageBack;
                            flipTileData1.WideBackgroundImage = this.tileCreat.WideImageFront;
                            flipTileData1.WideBackBackgroundImage = this.tileCreat.WideImageBack;
                            FlipTileData flipTileData2 = flipTileData1;
                            activeTile.Update((ShellTileData)flipTileData2);
                        }
                    }
                }
            }
            if (settings.notificationEnabled)
            {
                BackgroundTasks backgroundTasks = new BackgroundTasks();
                backgroundTasks.RegisterRelevantDateTask();
                backgroundTasks.RegisterMSWalletTask();
                backgroundTasks.RegisterPushNotificationTask();
                int num = settings.locationEnabled ? 1 : 0;
            }
            this.deleteTempFiles();
        }

        private string GetVersionNumber()
        {
            PackageVersion version = Package.Current.Id.Version;
            return string.Format("{0}.{1}.{2}.{3}", (object)version.Major, (object)version.Minor, (object)version.Build, (object)version.Revision);
        }

        private void AppPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainPage.PivotCallbacks pivotCallbacks = new MainPage.PivotCallbacks();
            if (!new AppSettings().listExpiredList)
            {
                if (!(this._callbacks.TryGetValue(this.AppPivot.SelectedItem, out pivotCallbacks) & pivotCallbacks.OnActivated != null))
                    return;
                pivotCallbacks.OnActivated();
            }
            else
            {
                if (!(this._callbacks.TryGetValue(this.AppPivotExpired.SelectedItem, out pivotCallbacks) & pivotCallbacks.OnActivated != null))
                    return;
                pivotCallbacks.OnActivated();
            }
        }

        protected virtual void OnBackKeyPress(CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            MainPage.PivotCallbacks pivotCallbacks = new MainPage.PivotCallbacks();
            if (!new AppSettings().listExpiredList)
            {
                if (!(this._callbacks.TryGetValue(this.AppPivot.SelectedItem, out pivotCallbacks) & pivotCallbacks.OnBackKeyPress != null))
                    return;
                pivotCallbacks.OnBackKeyPress(e);
            }
            else if (this.AppPivotExpired.SelectedIndex == 0)
            {
                if (!(this._callbacksExpired.TryGetValue(this.AppPivotExpired.SelectedItem, out pivotCallbacks) & pivotCallbacks.OnBackKeyPress != null))
                    return;
                pivotCallbacks.OnBackKeyPress(e);
            }
            else
            {
                if (!(this._callbacksExpiredExp.TryGetValue(this.AppPivotExpired.SelectedItem, out pivotCallbacks) & pivotCallbacks.OnBackKeyPress != null))
                    return;
                pivotCallbacks.OnBackKeyPress(e);
            }
        }

        private void OnItemContentTap(object sender, GestureEventArgs e) => this.loadItem(((FrameworkElement)sender).DataContext as ClasePass);

        protected virtual async void OnNavigatedTo(NavigationEventArgs e)
        {
            // ISSUE: reference to a compiler-generated method
            this.\u003C\u003En__FabricatedMethod1a(e);
            AppSettings settings = new AppSettings();
            StringToColorConverter strColor = new StringToColorConverter();
            SolidColorBrush headerColor = (SolidColorBrush)strColor.Convert((object)settings.themeColorHeader, (Type)null, (object)null, (CultureInfo)null);
            SolidColorBrush foreColor = (SolidColorBrush)strColor.Convert((object)settings.themeColorForeground, (Type)null, (object)null, (CultureInfo)null);
            SolidColorBrush mainColor = (SolidColorBrush)strColor.Convert((object)settings.themeColorMain, (Type)null, (object)null, (CultureInfo)null);
            SystemTray.BackgroundColor = headerColor.Color;
            SystemTray.ForegroundColor = foreColor.Color;
            this.ApplicationBar.BackgroundColor = mainColor.Color;
            this.ApplicationBar.ForegroundColor = foreColor.Color;
            ((UIElement)this.btnUpdates).Visibility = App._updatePassCollection.Count > 0 ? (Visibility)0 : (Visibility)1;
            this.txtUpdates.Text = App._updatePassCollection.Count.ToString();
            if (e.NavigationMode == null || e.NavigationMode == 4)
            {
                App._passcollection = new ClasePassCollection();
                App._passcollection.AddAllNew(WalletPass.IO.LoadDataPasses());
                App._settings = false;
                App._pkPass = false;
                App._pkPassGroup = false;
                this.InitializeAll();
                if (e.Uri.ToString().Contains("fileToken"))
                {
                    this.showTransitionTurnstile();
                    ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/pkPassPage.xaml?fileToken=" + e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("=") + 1), UriKind.Relative))));
                }
                else if (e.Uri.ToString().Contains("SecondaryTile") | e.Uri.ToString().Contains("WalletItem"))
                {
                    ClasePass clasePass = App._passcollection.returnPass(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("=") + 1));
                    if (clasePass != null)
                        this.loadItem(clasePass, true);
                }
                else if (e.Uri.ToString().Contains("Appointment"))
                {
                    ClasePass clasePass = App._passcollection.returnPass(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("=") + 1), false);
                    if (clasePass != null)
                        this.loadItem(clasePass, true);
                }
                else if (e.Uri.ToString().Contains("Notification"))
                {
                    ClasePass clasePass = App._passcollection.returnPass(e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("=") + 1));
                    if (clasePass != null)
                    {
                        ClasePassBackgroundTaskCollection passCollection = new ClasePassBackgroundTaskCollection(WalletPass.IO.LoadDataPassesBackgroundTask());
                        int index = passCollection.IndexPass(clasePass.serialNumberGUID);
                        if (index != -1)
                        {
                            ClaseReminderItems claseReminderItems = new ClaseReminderItems();
                            DateTime dateTime = passCollection[index].expirationDate.Year == 1 ? passCollection[index].relevantDate.Add(claseReminderItems.listPickerNotificationItemTimeSpan(settings.notificationReminderExpired)) : passCollection[index].expirationDate;
                            if (settings.notificationDisplayAlways && passCollection[index].showNotifications)
                            {
                                ClaseToastNotifText claseToastNotifText = new ClaseToastNotifText(passCollection);
                                XmlDocument templateContent = ToastNotificationManager.GetTemplateContent((ToastTemplateType)5);
                                XmlNodeList elementsByTagName = templateContent.GetElementsByTagName("text");
                                ((IReadOnlyList<IXmlNode>)elementsByTagName)[0].AppendChild((IXmlNode)templateContent.CreateTextNode(claseToastNotifText.returnHeaderText(passCollection[index].serialNumberGUID)));
                                ((IReadOnlyList<IXmlNode>)elementsByTagName)[1].AppendChild((IXmlNode)templateContent.CreateTextNode(claseToastNotifText.returnBodyText(passCollection[index].serialNumberGUID)));
                                string str = "#/MainPage.xaml?Notification=" + passCollection[index].serialNumberGUID;
                                ((XmlElement)templateContent.SelectSingleNode("/toast")).SetAttribute("launch", str);
                                ToastNotification toastNotification = new ToastNotification(templateContent);
                                toastNotification.put_ExpirationTime(new DateTimeOffset?((DateTimeOffset)dateTime));
                                toastNotification.put_SuppressPopup(settings.silenceNotificationEnabled);
                                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
                            }
                        }
                        this.loadItem(clasePass, true);
                    }
                }
                await this.CreateAppointmentCalendar();
            }
            else
            {
                if (e.NavigationMode != 1)
                    return;
                if (App._isTombStoned)
                {
                    StateManager.LoadStateAll((PhoneApplicationPage)this);
                    App._isTombStoned = false;
                    this.InitializeAll();
                    await this.CreateAppointmentCalendar();
                    ((UIElement)this).Opacity = 1.0;
                }
                else
                {
                    ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => App._passcollection.getFrontPasses()));
                    App._passcollection.updateSettings();
                    App._groupItemIndex = -1;
                    App._pageEntry = false;
                    this.distributePassBySettings();
                    if (App._settings)
                        App._settings = false;
                    else if (App._updates)
                        App._updates = false;
                    else if (App._archived)
                    {
                        App._archived = false;
                    }
                    else
                    {
                        App._pkPass = false;
                        App._pkPassGroup = false;
                        if (App._deletePasses.Count > 0)
                            this.DeletePasses();
                        if (App._archivePasses.Count > 0)
                            this.ArchivePasses();
                    }
                    this.deleteTempFiles();
                    this.shareContext = false;
                    if (App._passcollection.Count <= 0)
                        return;
                    ((UIElement)this.Sample).Visibility = (Visibility)1;
                    this.tutoHelp = false;
                    this._timer.Stop();
                }
            }
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            ((Page)this).OnNavigatedFrom(e);
            if (this.isTombstoned)
                StateManager.SaveStateAll((PhoneApplicationPage)this);
            ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => this.unRegisterForShare()));
        }

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
            this.lstPassOptExpired.IsSelectionEnabled = this.lstPassOptExpired.SelectedItems.Count > 0;
            this.lstPassOptExpiredExp.IsSelectionEnabled = this.lstPassOptExpiredExp.SelectedItems.Count > 0;
            this.lstPassGroupOptExpired.IsSelectionEnabled = this.lstPassGroupOptExpired.SelectedItems.Count > 0;
            this.lstPassGroupOptExpiredExp.IsSelectionEnabled = this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0;
            this.UpdatePassApplicationBar();
        }

        private void DeletePasses()
        {
            this._popup = new Popup();
            this._popup.Child = (UIElement)new splashUpdTilesControl(AppResources.splashDeletePass);
            this._popup.IsOpen = true;
            ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(async () => await this.DeletePasses(App._deletePasses)));
        }

        private async Task DeletePasses(List<ClasePass> deletePasses)
        {
            ClaseSaveCalendar calendarManage = new ClaseSaveCalendar();
            foreach (ClasePass deletePass in deletePasses)
            {
                ClasePass item = deletePass;
                try
                {
                    WalletItem walletItem = Microsoft.Phone.Wallet.Wallet.FindItem(item.serialNumberGUID);
                    if (walletItem != null)
                        Microsoft.Phone.Wallet.Wallet.Remove(walletItem);
                    if (App._mswalletPassCollection.IndexOf(item.serialNumberGUID) != -1)
                    {
                        App._mswalletPassCollection.Remove(item.serialNumberGUID);
                        WalletPass.IO.SaveDataMSWallet(App._mswalletPassCollection);
                    }
                    App._mswalletDeletePassCollection.Add(item.serialNumberGUID);
                    WalletPass.IO.SaveDataMSWalletDelete(App._mswalletDeletePassCollection);
                }
                catch
                {
                }
                if (await calendarManage.existAppointment(item.idAppointment, App._currentAppCalendar))
                    await calendarManage.removeAppointment(item.idAppointment, App._currentAppCalendar);
                if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + item.filenamePass))
                    File.Delete(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + item.filenamePass);
                int num;
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(async () => num = await ClasePassbookWebOperations.unregisterDevice(item) ? 1 : 0));
                this.DeleteTile(item.serialNumberGUID);
                App._passcollection.Remove(App._passcollection.returnPass(item.serialNumberGUID));
            }
            deletePasses.Clear();
            this.distributePassBySettings();
            WalletPass.IO.SaveData(App._passcollection);
            this.lstPass.IsSelectionEnabled = false;
            this.lstPassGroup.IsSelectionEnabled = false;
            this.lstPassOptExpired.IsSelectionEnabled = false;
            this.lstPassOptExpiredExp.IsSelectionEnabled = false;
            this.lstPassGroupOptExpired.IsSelectionEnabled = false;
            this.lstPassGroupOptExpiredExp.IsSelectionEnabled = false;
            this._popup.IsOpen = false;
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
            this._sharePlan = new ApplicationBarIconButton();
            this._sharePlan.IconUri = new Uri("/Assets/AppBar/appbar.share.png", UriKind.Relative);
            this._sharePlan.Text = AppResources.AppBarButtonShare;
            this._sharePlan.Click += new EventHandler(this.ApplicationBarIconButtonSharePass_Click);
            this._archivePlan = new ApplicationBarIconButton();
            this._archivePlan.IconUri = new Uri("/Assets/AppBar/appbar.cabinet.add.png", UriKind.Relative);
            this._archivePlan.Text = AppResources.AppBarButtonArchiveAdd;
            this._archivePlan.Click += new EventHandler(this.ApplicationBarIconButtonArchivePlan_Click);
            this._archivedButton = new ApplicationBarIconButton();
            this._archivedButton.IconUri = new Uri("/Assets/AppBar/appbar.cabinet.variant.png", UriKind.Relative);
            this._archivedButton.Text = AppResources.AppBarButtonArchive;
            this._archivedButton.Click += new EventHandler(this.ApplicationBarIconButtonArchivedButton_Click);
            this._settingsButton = new ApplicationBarIconButton();
            this._settingsButton.IconUri = new Uri("/Assets/Appbar/appbar.settings.png", UriKind.Relative);
            this._settingsButton.Text = AppResources.AppBarButtonSettings;
            this._settingsButton.Click += new EventHandler(this.OnSettingsClick);
            this._downloadSample = new ApplicationBarMenuItem();
            this._downloadSample.Text = AppResources.AppBarButtonDownloadSample;
            this._downloadSample.Click += new EventHandler(this.OnDownloadSampleClick);
            this._rateReview = new ApplicationBarMenuItem();
            this._rateReview.Text = AppResources.AppBarButtonRate;
            this._rateReview.Click += new EventHandler(this.OnRateReviewClick);
            this._contact = new ApplicationBarMenuItem();
            this._contact.Text = AppResources.AppBarButtonEMail;
            this._contact.Click += new EventHandler(this.OnContactClick);
            this.SetupPassApplicationBar();
        }

        public void OnPassPivotItemActivated()
        {
            if (this.lstPass.IsSelectionEnabled)
            {
                this.lstPass.IsSelectionEnabled = false;
                this.lstPassGroup.IsSelectionEnabled = false;
                this.lstPassOptExpired.IsSelectionEnabled = false;
                this.lstPassOptExpiredExp.IsSelectionEnabled = false;
                this.lstPassGroupOptExpired.IsSelectionEnabled = false;
                this.lstPassGroupOptExpiredExp.IsSelectionEnabled = false;
            }
            else
                this.SetupPassApplicationBar();
        }

        private void SetupPassApplicationBar()
        {
            this.ClearApplicationBar();
            if (this.lstPass.IsSelectionEnabled | this.lstPassGroup.IsSelectionEnabled | this.lstPassOptExpired.IsSelectionEnabled | this.lstPassOptExpiredExp.IsSelectionEnabled | this.lstPassGroupOptExpired.IsSelectionEnabled | this.lstPassGroupOptExpiredExp.IsSelectionEnabled)
            {
                this.ApplicationBar.Buttons.Add((object)this._sharePlan);
                this.ApplicationBar.Buttons.Add((object)this._archivePlan);
                this.ApplicationBar.Buttons.Add((object)this._deletePlan);
                this.UpdatePassApplicationBar();
            }
            else
            {
                this.ApplicationBar.Buttons.Add((object)this._selectPlan);
                this.ApplicationBar.Buttons.Add((object)this._archivedButton);
                this.ApplicationBar.Buttons.Add((object)this._settingsButton);
                this.ApplicationBar.MenuItems.Add((object)this._downloadSample);
                this.ApplicationBar.MenuItems.Add((object)this._rateReview);
                this.ApplicationBar.MenuItems.Add((object)this._contact);
            }
        }

        private void UpdatePassApplicationBar()
        {
            AppSettings appSettings = new AppSettings();
            if (!this.lstPass.IsSelectionEnabled && !this.lstPassGroup.IsSelectionEnabled && !this.lstPassOptExpired.IsSelectionEnabled && !this.lstPassOptExpiredExp.IsSelectionEnabled && !this.lstPassGroupOptExpired.IsSelectionEnabled && !this.lstPassGroupOptExpiredExp.IsSelectionEnabled)
                return;
            bool flag = this.lstPass.SelectedItems != null & this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems != null & this.lstPassGroup.SelectedItems.Count > 0 | this.lstPassOptExpired.SelectedItems != null & this.lstPassOptExpired.SelectedItems.Count > 0 | this.lstPassGroupOptExpiredExp.SelectedItems != null & this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0 | this.lstPassOptExpiredExp != null & this.lstPassOptExpiredExp.SelectedItems.Count > 0 | this.lstPassGroupOptExpired.SelectedItems != null & this.lstPassGroupOptExpired.SelectedItems.Count > 0;
            this._deletePlan.IsEnabled = flag;
            this._sharePlan.IsEnabled = this.canShare();
            this._archivePlan.IsEnabled = flag;
        }

        protected void OnPlanBackKeyPressed(CancelEventArgs e)
        {
            if (this.lstPass.IsSelectionEnabled | this.lstPassGroup.IsSelectionEnabled | this.lstPassOptExpired.IsSelectionEnabled | this.lstPassOptExpiredExp.IsSelectionEnabled | this.lstPassGroupOptExpired.IsSelectionEnabled | this.lstPassGroupOptExpiredExp.IsSelectionEnabled)
            {
                this.lstPass.IsSelectionEnabled = false;
                this.lstPassGroup.IsSelectionEnabled = false;
                this.lstPassOptExpired.IsSelectionEnabled = false;
                this.lstPassOptExpiredExp.IsSelectionEnabled = false;
                this.lstPassGroupOptExpired.IsSelectionEnabled = false;
                this.lstPassGroupOptExpiredExp.IsSelectionEnabled = false;
                e.Cancel = true;
            }
            else
                this.isTombstoned = false;
        }

        private void ApplicationBarIconButtonSelectPass_Click(object sender, EventArgs e)
        {
            this.lstPass.IsSelectionEnabled = true;
            this.lstPassGroup.IsSelectionEnabled = true;
            this.lstPassOptExpired.IsSelectionEnabled = true;
            this.lstPassOptExpiredExp.IsSelectionEnabled = true;
            this.lstPassGroupOptExpired.IsSelectionEnabled = true;
            this.lstPassGroupOptExpiredExp.IsSelectionEnabled = true;
        }

        private void ApplicationBarIconButtonDeletePass_Click(object sender, EventArgs e)
        {
            string str = AppResources.msgBoxDelete;
            AppSettings appSettings = new AppSettings();
            if (this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0 | this.lstPassOptExpired.SelectedItems.Count > 0 | this.lstPassOptExpiredExp.SelectedItems.Count > 0 | this.lstPassGroupOptExpired.SelectedItems.Count > 0 | this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0)
                str = AppResources.msgBoxDeleteMulti;
            if (MessageBox.Show(str, AppResources.msgBoxDeleteCaption, (MessageBoxButton)1) != 1)
                return;
            App._deletePasses.Clear();
            if (!appSettings.listShowGroup)
            {
                for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPass.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassOptExpired.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPassOptExpired.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassOptExpiredExp.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPassOptExpiredExp.SelectedItems[index]);
            }
            else
            {
                for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPassGroup.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassGroupOptExpired.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPassGroupOptExpired.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassGroupOptExpiredExp.SelectedItems.Count - 1; ++index)
                    App._deletePasses.Add((ClasePass)this.lstPassGroupOptExpiredExp.SelectedItems[index]);
            }
            this.DeletePasses();
        }

        private void ApplicationBarIconButtonSharePass_Click(object sender, EventArgs e)
        {
            this.registerForShare();
            DataTransferManager.ShowShareUI();
        }

        private async void ApplicationBarIconButtonArchivedButton_Click(object sender, EventArgs e)
        {
            App._passcollectionArchived = new ClasePassCollection();
            App._passcollectionArchived.AddAllNew(await WalletPass.IO.LoadDataPassesArchived());
            App._archived = true;
            this.NavigateToNextScreen(false);
        }

        private void ApplicationBarIconButtonArchivePlan_Click(object sender, EventArgs e)
        {
            string str = AppResources.msgBoxArchive;
            AppSettings appSettings = new AppSettings();
            if (this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0 | this.lstPassOptExpired.SelectedItems.Count > 0 | this.lstPassOptExpiredExp.SelectedItems.Count > 0 | this.lstPassGroupOptExpired.SelectedItems.Count > 0 | this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0)
                str = AppResources.msgBoxArchives;
            if (MessageBox.Show(str, AppResources.msgBoxArchiveCaption, (MessageBoxButton)1) != 1)
                return;
            App._archivePasses.Clear();
            if (!appSettings.listShowGroup)
            {
                for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPass.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassOptExpired.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPassOptExpired.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassOptExpiredExp.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPassOptExpiredExp.SelectedItems[index]);
            }
            else
            {
                for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPassGroup.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassGroupOptExpired.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPassGroupOptExpired.SelectedItems[index]);
                for (int index = 0; index <= this.lstPassGroupOptExpiredExp.SelectedItems.Count - 1; ++index)
                    App._archivePasses.Add((ClasePass)this.lstPassGroupOptExpiredExp.SelectedItems[index]);
            }
            this.ArchivePasses();
        }

        private void OnSettingsClick(object sender, EventArgs e)
        {
            App._settings = true;
            this.NavigateToNextScreen(false);
        }

        private void OnDownloadSampleClick(object sender, EventArgs e) => this.sampleDownload();

        private void OnRateReviewClick(object sender, EventArgs e) => new MarketplaceReviewTask().Show();

        private void OnContactClick(object sender, EventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();
            string versionNumber = this.GetVersionNumber();
            emailComposeTask.To = "wallet_pass_support@outlook.com";
            emailComposeTask.Body = Environment.NewLine + Environment.NewLine + Environment.NewLine + "------------------------------------" + Environment.NewLine + "Wallet Pass " + versionNumber + Environment.NewLine + "DeviceID: " + HostInformation.PublisherHostId + Environment.NewLine + "Culture: " + CultureInfo.CurrentCulture.Name + Environment.NewLine + "------------------------------------";
            emailComposeTask.Show();
        }

        private void btnUpdates_Tap(object sender, GestureEventArgs e)
        {
            App._updates = true;
            this.NavigateToNextScreen(false);
        }

        private void itemContextMenuDelete_Click(object sender, RoutedEventArgs e)
        {
            object dataContext = ((FrameworkElement)sender).DataContext;
            string msgBoxDelete = AppResources.msgBoxDelete;
            AppSettings appSettings = new AppSettings();
            if (MessageBox.Show(msgBoxDelete, AppResources.msgBoxDeleteCaption, (MessageBoxButton)1) != 1)
                return;
            ClasePass clasePass = (ClasePass)dataContext;
            App._deletePasses.Clear();
            App._deletePasses.Add(clasePass);
            this.DeletePasses();
        }

        private void itemContextMenuPinStart_Click(object sender, RoutedEventArgs e)
        {
            ClasePass dataContext = (ClasePass)((FrameworkElement)sender).DataContext;
            App._tempPassClass = dataContext;
            TileUpdate tileUpdate = new TileUpdate();
            tileUpdate.RenderWideTile();
            tileUpdate.RenderMediumTile();
            tileUpdate.RenderSmallTile();
            FlipTileData flipTileData1 = new FlipTileData();
            ((ShellTileData)flipTileData1).Title = "";
            flipTileData1.SmallBackgroundImage = tileUpdate.SmallImage;
            ((StandardTileData)flipTileData1).BackgroundImage = tileUpdate.ImageFront;
            ((StandardTileData)flipTileData1).BackBackgroundImage = tileUpdate.ImageBack;
            flipTileData1.WideBackgroundImage = tileUpdate.WideImageFront;
            flipTileData1.WideBackBackgroundImage = tileUpdate.WideImageBack;
            FlipTileData flipTileData2 = flipTileData1;
            ShellTile.Create(new Uri("/MainPage.xaml?SecondaryTile=" + dataContext.serialNumberGUID, UriKind.Relative), (ShellTileData)flipTileData2, true);
            App._passcollection.updateSettings();
        }

        private void itemContextMenuShare_Click(object sender, RoutedEventArgs e)
        {
            this.registerForShare();
            this.shareContext = true;
            App._tempPassClass = (ClasePass)((FrameworkElement)sender).DataContext;
            DataTransferManager.ShowShareUI();
        }

        private void itemContextMenuSetRelevantDate_Click(object sender, RoutedEventArgs e)
        {
            App._tempPassClass = (ClasePass)((FrameworkElement)sender).DataContext;
            ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/setRelevantDatePage.xaml", UriKind.Relative))));
        }

        private void itemContextMenuAddToArchived_Click(object sender, RoutedEventArgs e)
        {
            ClasePass dataContext = (ClasePass)((FrameworkElement)sender).DataContext;
            string msgBoxArchive = AppResources.msgBoxArchive;
            AppSettings appSettings = new AppSettings();
            if (MessageBox.Show(msgBoxArchive, AppResources.msgBoxArchiveCaption, (MessageBoxButton)1) != 1)
                return;
            App._archivePasses.Clear();
            App._archivePasses.Add(dataContext);
            this.ArchivePasses();
        }

        private void distributePassBySettings()
        {
            AppSettings appSettings = new AppSettings();
            DataTemplate resource1;
            DataTemplate resource2;
            if (appSettings.listElementSize == 0)
            {
                resource1 = ((FrameworkElement)this).Resources[(object)"PassItemTemplateInfo"] as DataTemplate;
                resource2 = ((FrameworkElement)this).Resources[(object)"groupHeaderTemplate"] as DataTemplate;
            }
            else
            {
                resource1 = ((FrameworkElement)this).Resources[(object)"PassItemTemplateInfoSmall"] as DataTemplate;
                resource2 = ((FrameworkElement)this).Resources[(object)"groupHeaderTemplateSmall"] as DataTemplate;
            }
            this.lstPass.ItemInfoTemplate = resource1;
            this.lstPassGroup.ItemInfoTemplate = resource1;
            this.lstPassGroup.GroupHeaderTemplate = resource2;
            this.lstPassOptExpired.ItemInfoTemplate = resource1;
            this.lstPassOptExpiredExp.ItemInfoTemplate = resource1;
            this.lstPassGroupOptExpired.ItemInfoTemplate = resource1;
            this.lstPassGroupOptExpired.GroupHeaderTemplate = resource2;
            this.lstPassGroupOptExpiredExp.ItemInfoTemplate = resource1;
            this.lstPassGroupOptExpiredExp.GroupHeaderTemplate = resource2;
            if (!appSettings.listExpiredList)
            {
                ((UIElement)this.AppPivot).Visibility = (Visibility)0;
                ((UIElement)this.AppPivotExpired).Visibility = (Visibility)1;
                if (!appSettings.listShowGroup)
                {
                    ((UIElement)this.lstPass).Visibility = (Visibility)0;
                    ((UIElement)this.lstPassGroup).Visibility = (Visibility)1;
                    App._groupItemIndex = -1;
                    App._passcollection.updateSettings();
                    this.lstPass.ItemsSource = (IList)App._passcollection;
                }
                else
                {
                    List<ClaseGroup<ClasePass>> claseGroupList = new List<ClaseGroup<ClasePass>>();
                    App._passcollection.updateSettings();
                    for (int index1 = 0; index1 <= App._passcollection.Count - 1; ++index1)
                    {
                        switch (appSettings.listGroupBy)
                        {
                            case 0:
                                this._groupToFind = App._passcollection[index1].organizationName;
                                break;
                            case 1:
                                this._groupToFind = App._passcollection[index1].type;
                                break;
                        }
                        if (claseGroupList.Find(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName)) == null)
                        {
                            ClaseGroup<ClasePass> claseGroup;
                            if (appSettings.listGroupBy == 0)
                            {
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, App._passcollection[index1].iconImage);
                            }
                            else
                            {
                                StringToColorConverter toColorConverter = new StringToColorConverter();
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, App._passcollection[index1].imageType, (SolidColorBrush)toColorConverter.Convert((object)appSettings.themeColorForeground, (Type)null, (object)null, (CultureInfo)null));
                            }
                            claseGroup.Add(App._passcollection[index1]);
                            switch (appSettings.listGroupOrder)
                            {
                                case 0:
                                    claseGroupList.Add(claseGroup);
                                    continue;
                                case 1:
                                    bool flag = false;
                                    for (int index2 = 0; index2 <= claseGroupList.Count - 1; ++index2)
                                    {
                                        if (appSettings.listOrder == 0)
                                        {
                                            if (string.Compare(claseGroup.Title, claseGroupList[index2].Title) < 0)
                                            {
                                                claseGroupList.Insert(index2, claseGroup);
                                                index2 = claseGroupList.Count;
                                                flag = true;
                                            }
                                        }
                                        else if (string.Compare(claseGroup.Title, claseGroupList[index2].Title) > 0)
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
                            claseGroupList[claseGroupList.FindIndex(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName))].Add(App._passcollection[index1]);
                    }
                  ((UIElement)this.lstPass).Visibility = (Visibility)1;
                    ((UIElement)this.lstPassGroup).Visibility = (Visibility)0;
                    this.lstPassGroup.ItemsSource = (IList)claseGroupList;
                }
            }
            else
            {
                List<ClasePass> clasePassList1 = new List<ClasePass>();
                List<ClasePass> clasePassList2 = new List<ClasePass>();
                ((UIElement)this.AppPivot).Visibility = (Visibility)1;
                ((UIElement)this.AppPivotExpired).Visibility = (Visibility)0;
                if (!appSettings.listShowGroup)
                {
                    ((UIElement)this.lstPassOptExpired).Visibility = (Visibility)0;
                    ((UIElement)this.lstPassOptExpiredExp).Visibility = (Visibility)0;
                    ((UIElement)this.lstPassGroupOptExpired).Visibility = (Visibility)1;
                    ((UIElement)this.lstPassGroupOptExpiredExp).Visibility = (Visibility)1;
                    App._groupItemIndex = -1;
                    App._passcollection.updateSettings();
                    for (int index = 0; index < App._passcollection.Count; ++index)
                    {
                        if (App._passcollection[index].relevantDate == new DateTime(1, 1, 1) || App._passcollection[index].relevantDate > DateTime.Now)
                            clasePassList1.Add(App._passcollection[index]);
                        else
                            clasePassList2.Add(App._passcollection[index]);
                    }
                    this.lstPassOptExpired.ItemsSource = (IList)clasePassList1;
                    this.lstPassOptExpiredExp.ItemsSource = (IList)clasePassList2;
                }
                else
                {
                    List<ClaseGroup<ClasePass>> claseGroupList1 = new List<ClaseGroup<ClasePass>>();
                    List<ClaseGroup<ClasePass>> claseGroupList2 = new List<ClaseGroup<ClasePass>>();
                    App._passcollection.updateSettings();
                    for (int index = 0; index < App._passcollection.Count; ++index)
                    {
                        if (App._passcollection[index].relevantDate == new DateTime(1, 1, 1) || App._passcollection[index].relevantDate > DateTime.Now)
                            clasePassList1.Add(App._passcollection[index]);
                        else
                            clasePassList2.Add(App._passcollection[index]);
                    }
                    for (int index3 = 0; index3 <= clasePassList1.Count - 1; ++index3)
                    {
                        switch (appSettings.listGroupBy)
                        {
                            case 0:
                                this._groupToFind = clasePassList1[index3].organizationName;
                                break;
                            case 1:
                                this._groupToFind = clasePassList1[index3].type;
                                break;
                        }
                        if (claseGroupList1.Find(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName)) == null)
                        {
                            ClaseGroup<ClasePass> claseGroup;
                            if (appSettings.listGroupBy == 0)
                            {
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, clasePassList1[index3].iconImage);
                            }
                            else
                            {
                                StringToColorConverter toColorConverter = new StringToColorConverter();
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, clasePassList1[index3].imageType, (SolidColorBrush)toColorConverter.Convert((object)appSettings.themeColorForeground, (Type)null, (object)null, (CultureInfo)null));
                            }
                            claseGroup.Add(clasePassList1[index3]);
                            switch (appSettings.listGroupOrder)
                            {
                                case 0:
                                    claseGroupList1.Add(claseGroup);
                                    continue;
                                case 1:
                                    bool flag = false;
                                    for (int index4 = 0; index4 <= claseGroupList1.Count - 1; ++index4)
                                    {
                                        if (string.Compare(claseGroup.Title, claseGroupList1[index4].Title) < 0)
                                        {
                                            claseGroupList1.Insert(index4, claseGroup);
                                            index4 = claseGroupList1.Count;
                                            flag = true;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        claseGroupList1.Add(claseGroup);
                                        continue;
                                    }
                                    continue;
                                default:
                                    continue;
                            }
                        }
                        else
                            claseGroupList1[claseGroupList1.FindIndex(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName))].Add(clasePassList1[index3]);
                    }
                    for (int index5 = 0; index5 <= clasePassList2.Count - 1; ++index5)
                    {
                        switch (appSettings.listGroupBy)
                        {
                            case 0:
                                this._groupToFind = clasePassList2[index5].organizationName;
                                break;
                            case 1:
                                this._groupToFind = clasePassList2[index5].type;
                                break;
                        }
                        if (claseGroupList2.Find(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName)) == null)
                        {
                            ClaseGroup<ClasePass> claseGroup;
                            if (appSettings.listGroupBy == 0)
                            {
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, clasePassList2[index5].iconImage);
                            }
                            else
                            {
                                StringToColorConverter toColorConverter = new StringToColorConverter();
                                claseGroup = new ClaseGroup<ClasePass>(this._groupToFind, clasePassList2[index5].imageType, (SolidColorBrush)toColorConverter.Convert((object)appSettings.themeColorForeground, (Type)null, (object)null, (CultureInfo)null));
                            }
                            claseGroup.Add(clasePassList2[index5]);
                            switch (appSettings.listGroupOrder)
                            {
                                case 0:
                                    claseGroupList2.Add(claseGroup);
                                    continue;
                                case 1:
                                    bool flag = false;
                                    for (int index6 = 0; index6 <= claseGroupList2.Count - 1; ++index6)
                                    {
                                        if (string.Compare(claseGroup.Title, claseGroupList2[index6].Title) < 0)
                                        {
                                            claseGroupList2.Insert(index6, claseGroup);
                                            index6 = claseGroupList2.Count;
                                            flag = true;
                                        }
                                    }
                                    if (!flag)
                                    {
                                        claseGroupList2.Add(claseGroup);
                                        continue;
                                    }
                                    continue;
                                default:
                                    continue;
                            }
                        }
                        else
                            claseGroupList2[claseGroupList2.FindIndex(new Predicate<ClaseGroup<ClasePass>>(this.findGroupName))].Add(clasePassList2[index5]);
                    }
                  ((UIElement)this.lstPassOptExpired).Visibility = (Visibility)1;
                    ((UIElement)this.lstPassOptExpiredExp).Visibility = (Visibility)1;
                    ((UIElement)this.lstPassGroupOptExpired).Visibility = (Visibility)0;
                    ((UIElement)this.lstPassGroupOptExpiredExp).Visibility = (Visibility)0;
                    this.lstPassGroupOptExpired.ItemsSource = (IList)claseGroupList1;
                    this.lstPassGroupOptExpiredExp.ItemsSource = (IList)claseGroupList2;
                }
            }
        }

        private bool findGroupName(ClaseGroup<ClasePass> gr) => gr.Title.ToString().ToLower() == this._groupToFind.ToLower();

        private ClaseGroup<ClasePass> loadEntireGroup(ClasePass item)
        {
            List<ClaseGroup<ClasePass>> claseGroupList = new AppSettings().listExpiredList ? (this.AppPivotExpired.SelectedIndex != 0 ? (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpiredExp.ItemsSource : (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpired.ItemsSource) : (List<ClaseGroup<ClasePass>>)this.lstPassGroup.ItemsSource;
            for (int index = 0; index <= claseGroupList.Count - 1; ++index)
            {
                if (claseGroupList[index].Contains(item))
                    return claseGroupList[index];
            }
            return (ClaseGroup<ClasePass>)null;
        }

        private int groupCount(ClasePass item)
        {
            List<ClaseGroup<ClasePass>> claseGroupList = new AppSettings().listExpiredList ? (this.AppPivotExpired.SelectedIndex != 0 ? (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpiredExp.ItemsSource : (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpired.ItemsSource) : (List<ClaseGroup<ClasePass>>)this.lstPassGroup.ItemsSource;
            for (int index = 0; index <= claseGroupList.Count - 1; ++index)
            {
                if (claseGroupList[index].Contains(item))
                    return claseGroupList[index].Count;
            }
            return -1;
        }

        private int groupItemIndex(ClasePass item)
        {
            List<ClaseGroup<ClasePass>> claseGroupList = new AppSettings().listExpiredList ? (this.AppPivotExpired.SelectedIndex != 0 ? (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpiredExp.ItemsSource : (List<ClaseGroup<ClasePass>>)this.lstPassGroupOptExpired.ItemsSource) : (List<ClaseGroup<ClasePass>>)this.lstPassGroup.ItemsSource;
            for (int index = 0; index <= claseGroupList.Count - 1; ++index)
            {
                if (claseGroupList[index].Contains(item))
                    return claseGroupList[index].IndexOf(item);
            }
            return -1;
        }

        private void DeleteTile(string serialNumber)
        {
            Uri _uri = new Uri("/MainPage.xaml?SecondaryTile=" + serialNumber, UriKind.Relative);
            ShellTile.ActiveTiles.Where<ShellTile>((Func<ShellTile, bool>)(t => t.NavigationUri == _uri)).FirstOrDefault<ShellTile>()?.Delete();
        }

        private bool isPinned(string serialNumber)
        {
            Uri _uri = new Uri("/MainPage.xaml?SecondaryTile=" + serialNumber, UriKind.Relative);
            return ShellTile.ActiveTiles.Where<ShellTile>((Func<ShellTile, bool>)(t => t.NavigationUri == _uri)).FirstOrDefault<ShellTile>() != null;
        }

        public async Task CreateAppointmentCalendar()
        {
            this._appointmentStore = await AppointmentManager.RequestStoreAsync((AppointmentStoreAccessType)0);
            if (!((IDictionary<string, object>)ApplicationData.Current.LocalSettings.Values).ContainsKey("FirstRun"))
            {
                await this.CheckForAndCreateAppointmentCalendars();
                this._appointmentStore.ChangeTracker.Enable();
                this._appointmentStore.ChangeTracker.Reset();
                ((IDictionary<string, object>)ApplicationData.Current.LocalSettings.Values)["FirstRun"] = (object)false;
            }
            else
                await this.CheckForAndCreateAppointmentCalendars();
        }

        public async Task CheckForAndCreateAppointmentCalendars()
        {
            IReadOnlyList<AppointmentCalendar> appCalendars = await this._appointmentStore.FindAppointmentCalendarsAsync((FindAppointmentCalendarsOptions)1);
            AppointmentCalendar appCalendar = (AppointmentCalendar)null;
            AppointmentCalendar appointmentCalendarAsync;
            if (appCalendars.Count != 0)
                appointmentCalendarAsync = appCalendars[0];
            else
                appointmentCalendarAsync = await this._appointmentStore.CreateAppointmentCalendarAsync("Wallet Pass Calendar");
            appCalendar = appointmentCalendarAsync;
            appCalendar.put_OtherAppReadAccess((AppointmentCalendarOtherAppReadAccess)2);
            appCalendar.put_OtherAppWriteAccess((AppointmentCalendarOtherAppWriteAccess)1);
            appCalendar.put_SummaryCardView((AppointmentSummaryCardView)1);
            await appCalendar.SaveAsync();
            App._currentAppCalendar = appCalendar;
        }

        private void btnDownHelp_Tap(object sender, GestureEventArgs e)
        {
            if (this.tutoHelp)
            {
                this.showTransitionTurnstile();
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/Tutorials/tutorialMain.xaml", UriKind.Relative))));
            }
            else
                this.sampleDownload();
        }

        private void OnTimerTick(object sender, EventArgs args)
        {
            if (this.tutoHelp)
            {
                this.FlipDownload.Begin();
                this.TextShowDownload.Begin();
            }
            else
            {
                this.FlipHelp.Begin();
                this.TextShowHelp.Begin();
            }
            this.tutoHelp = !this.tutoHelp;
        }

        private void sampleDownload() => new WebBrowserTask()
        {
            Uri = new Uri("http://www.passcreator.de/fileadmin/user_upload/passcreator/wallet_pass_demo/demo.pkpass", UriKind.Absolute)
        }.Show();

        private void ArchivePasses()
        {
            this._popup = new Popup();
            this._popup.Child = (UIElement)new splashUpdTilesControl(AppResources.splashArchivePass);
            this._popup.IsOpen = true;
            ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(async () => await this.ArchivePasses(App._archivePasses)));
        }

        private async Task ArchivePasses(List<ClasePass> archivePasses)
        {
            ClaseSaveCalendar calendarManage = new ClaseSaveCalendar();
            App._passcollectionArchived = new ClasePassCollection();
            App._passcollectionArchived.AddAllNew(await WalletPass.IO.LoadDataPassesArchived());
            foreach (ClasePass archivePass in archivePasses)
            {
                ClasePass item = archivePass;
                try
                {
                    WalletItem walletItem = Microsoft.Phone.Wallet.Wallet.FindItem(item.serialNumberGUID);
                    if (walletItem != null)
                        Microsoft.Phone.Wallet.Wallet.Remove(walletItem);
                    if (App._mswalletPassCollection.IndexOf(item.serialNumberGUID) != -1)
                    {
                        App._mswalletPassCollection.Remove(item.serialNumberGUID);
                        WalletPass.IO.SaveDataMSWallet(App._mswalletPassCollection);
                    }
                    App._mswalletDeletePassCollection.Add(item.serialNumberGUID);
                    WalletPass.IO.SaveDataMSWalletDelete(App._mswalletDeletePassCollection);
                }
                catch
                {
                }
                if (await calendarManage.existAppointment(item.idAppointment, App._currentAppCalendar))
                    await calendarManage.removeAppointment(item.idAppointment, App._currentAppCalendar);
                int num;
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(async () => num = await ClasePassbookWebOperations.unregisterDevice(item) ? 1 : 0));
                this.DeleteTile(item.serialNumberGUID);
                App._passcollectionArchived.AddNew(item);
                App._passcollection.Remove(App._passcollection.returnPass(item.serialNumberGUID));
            }
            archivePasses.Clear();
            this.distributePassBySettings();
            WalletPass.IO.SaveData(App._passcollection);
            WalletPass.IO.SaveDataArchived(App._passcollectionArchived);
            this.lstPass.IsSelectionEnabled = false;
            this.lstPassGroup.IsSelectionEnabled = false;
            this.lstPassOptExpired.IsSelectionEnabled = false;
            this.lstPassOptExpiredExp.IsSelectionEnabled = false;
            this.lstPassGroupOptExpired.IsSelectionEnabled = false;
            this.lstPassGroupOptExpiredExp.IsSelectionEnabled = false;
            this._popup.IsOpen = false;
        }

        private void registerForShare()
        {
            DataTransferManager forCurrentView = DataTransferManager.GetForCurrentView();
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Func<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, EventRegistrationToken>(forCurrentView.add_DataRequested), new Action<EventRegistrationToken>(forCurrentView.remove_DataRequested), new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object)this, __methodptr(ShareStorageItemsHandler)));
        }

        private void unRegisterForShare() => WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>>(new Action<EventRegistrationToken>(DataTransferManager.GetForCurrentView().remove_DataRequested), new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>((object)this, __methodptr(ShareStorageItemsHandler)));

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
                    request.Data.SetStorageItems((IEnumerable<IStorageItem>)new List<StorageFile>()
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
                if (!(this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0 | this.lstPassOptExpired.SelectedItems.Count > 0 | this.lstPassOptExpiredExp.SelectedItems.Count > 0 | this.lstPassGroupOptExpired.SelectedItems.Count > 0 | this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0))
                    return;
                AppSettings _settings = new AppSettings();
                DataRequest request = e.Request;
                request.Data.Properties.put_Title("Passbooks");
                request.Data.Properties.put_Description("Passbook share");
                DataRequestDeferral deferral = request.GetDeferral();
                try
                {
                    if (!_settings.listExpiredList)
                    {
                        List<StorageFile> storageItems = new List<StorageFile>();
                        if (_settings.listShowGroup)
                        {
                            for (int i = 0; i < this.lstPassGroup.SelectedItems.Count; ++i)
                            {
                                ClasePass clsPass = (ClasePass)this.lstPassGroup.SelectedItems[i];
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
                                ClasePass clsPass = (ClasePass)this.lstPass.SelectedItems[i];
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
                        request.Data.SetStorageItems((IEnumerable<IStorageItem>)storageItems);
                    }
                    else
                    {
                        List<StorageFile> storageItems = new List<StorageFile>();
                        if (_settings.listShowGroup)
                        {
                            for (int i = 0; i < this.lstPassGroupOptExpired.SelectedItems.Count; ++i)
                            {
                                ClasePass clsPass = (ClasePass)this.lstPassGroupOptExpired.SelectedItems[i];
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
                            for (int i = 0; i < this.lstPassGroupOptExpiredExp.SelectedItems.Count; ++i)
                            {
                                ClasePass clsPass = (ClasePass)this.lstPassGroupOptExpiredExp.SelectedItems[i];
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
                            for (int i = 0; i < this.lstPassOptExpired.SelectedItems.Count; ++i)
                            {
                                ClasePass clsPass = (ClasePass)this.lstPassOptExpired.SelectedItems[i];
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
                            for (int i = 0; i < this.lstPassOptExpiredExp.SelectedItems.Count; ++i)
                            {
                                ClasePass clsPass = (ClasePass)this.lstPassOptExpiredExp.SelectedItems[i];
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
                        request.Data.SetStorageItems((IEnumerable<IStorageItem>)storageItems);
                    }
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
            List<string> list = ((IEnumerable<string>)Directory.GetFiles(ApplicationData.Current.LocalFolder.Path, "*.*")).Where<string>((Func<string, bool>)(file => file.ToLower().EndsWith("pkpass"))).ToList<string>();
            for (int index = 0; index < list.Count; ++index)
                File.Delete(list[index]);
        }

        private bool canShare()
        {
            if (!(this.lstPass.SelectedItems.Count > 0 | this.lstPassGroup.SelectedItems.Count > 0 | this.lstPassOptExpired.SelectedItems.Count > 0 | this.lstPassOptExpiredExp.SelectedItems.Count > 0 | this.lstPassGroupOptExpired.SelectedItems.Count > 0 | this.lstPassGroupOptExpiredExp.SelectedItems.Count > 0))
                return false;
            AppSettings appSettings = new AppSettings();
            if (!appSettings.listExpiredList)
            {
                if (!appSettings.listShowGroup)
                {
                    for (int index = 0; index <= this.lstPass.SelectedItems.Count - 1; ++index)
                    {
                        if (string.IsNullOrEmpty(((ClasePass)this.lstPass.SelectedItems[index]).filenamePass))
                            return false;
                    }
                    return true;
                }
                for (int index = 0; index <= this.lstPassGroup.SelectedItems.Count - 1; ++index)
                {
                    if (string.IsNullOrEmpty(((ClasePass)this.lstPassGroup.SelectedItems[index]).filenamePass))
                        return false;
                }
                return true;
            }
            if (!appSettings.listShowGroup)
            {
                for (int index = 0; index <= this.lstPassOptExpired.SelectedItems.Count - 1; ++index)
                {
                    if (string.IsNullOrEmpty(((ClasePass)this.lstPassOptExpired.SelectedItems[index]).filenamePass))
                        return false;
                }
                for (int index = 0; index <= this.lstPassOptExpiredExp.SelectedItems.Count - 1; ++index)
                {
                    if (string.IsNullOrEmpty(((ClasePass)this.lstPassOptExpired.SelectedItems[index]).filenamePass))
                        return false;
                }
                return true;
            }
            for (int index = 0; index <= this.lstPassGroupOptExpired.SelectedItems.Count - 1; ++index)
            {
                if (string.IsNullOrEmpty(((ClasePass)this.lstPassGroupOptExpired.SelectedItems[index]).filenamePass))
                    return false;
            }
            for (int index = 0; index <= this.lstPassGroupOptExpiredExp.SelectedItems.Count - 1; ++index)
            {
                if (string.IsNullOrEmpty(((ClasePass)this.lstPassGroupOptExpiredExp.SelectedItems[index]).filenamePass))
                    return false;
            }
            return true;
        }

        private void NavigateToNextScreen(bool loadedFromOutside)
        {
            this.showTransitionTurnstile();
            if (App._settings)
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/confPage.xaml", UriKind.Relative))));
            else if (App._updates)
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/updatePage.xaml", UriKind.Relative))));
            else if (App._archived)
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/archivedPage.xaml", UriKind.Relative))));
            else if (App._pkPass)
            {
                if (loadedFromOutside)
                    ((Page)this).NavigationService.Navigate(new Uri("/pkPassPage.xaml", UriKind.Relative));
                else
                    ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/pkPassPage.xaml", UriKind.Relative))));
            }
            else
            {
                if (!App._pkPassGroup)
                    return;
                ((DependencyObject)this).Dispatcher.BeginInvoke((Action)(() => ((Page)this).NavigationService.Navigate(new Uri("/pkPassGroupPage.xaml", UriKind.Relative))));
            }
        }

        private void showTransitionTurnstile()
        {
            TurnstileTransition turnstileTransition = new TurnstileTransition();
            turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
            PhoneApplicationPage element = (PhoneApplicationPage)this;
            ITransition trans = turnstileTransition.GetTransition((UIElement)element);
            trans.Completed += (EventHandler)((param0, param1) => trans.Stop());
            trans.Begin();
        }

        private void btnDownHelp_ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            SolidColorBrush solidColorBrush = (SolidColorBrush)new StringToColorConverter().Convert((object)new AppSettings().themeColorMain, (Type)null, (object)null, (CultureInfo)null);
            ((Shape)this.ImageHelp).Fill = (Brush)solidColorBrush;
            ((Shape)this.ImageDownload).Fill = (Brush)solidColorBrush;
        }

        private void btnDownHelp_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            SolidColorBrush solidColorBrush = (SolidColorBrush)new StringToColorConverter().Convert((object)new AppSettings().themeColorForeground, (Type)null, (object)null, (CultureInfo)null);
            ((Shape)this.ImageHelp).Fill = (Brush)solidColorBrush;
            ((Shape)this.ImageDownload).Fill = (Brush)solidColorBrush;
        }

        /*
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/MainPage.xaml", UriKind.Relative));
      this.FlipDownload = (Storyboard) ((FrameworkElement) this).FindName("FlipDownload");
      this.FlipHelp = (Storyboard) ((FrameworkElement) this).FindName("FlipHelp");
      this.TextShowDownload = (Storyboard) ((FrameworkElement) this).FindName("TextShowDownload");
      this.TextShowHelp = (Storyboard) ((FrameworkElement) this).FindName("TextShowHelp");
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.btnUpdates = (StackPanel) ((FrameworkElement) this).FindName("btnUpdates");
      this.txtUpdates = (TextBlock) ((FrameworkElement) this).FindName("txtUpdates");
      this.AppPivot = (Pivot) ((FrameworkElement) this).FindName("AppPivot");
      this.PassPanel = (PivotItem) ((FrameworkElement) this).FindName("PassPanel");
      this.lstPass = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPass");
      this.lstPassGroup = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassGroup");
      this.AppPivotExpired = (Pivot) ((FrameworkElement) this).FindName("AppPivotExpired");
      this.PassPanelOptExpired = (PivotItem) ((FrameworkElement) this).FindName("PassPanelOptExpired");
      this.lstPassOptExpired = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassOptExpired");
      this.lstPassGroupOptExpired = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassGroupOptExpired");
      this.PassPanelOptExpiredExp = (PivotItem) ((FrameworkElement) this).FindName("PassPanelOptExpiredExp");
      this.lstPassOptExpiredExp = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassOptExpiredExp");
      this.lstPassGroupOptExpiredExp = (LongListMultiSelector) ((FrameworkElement) this).FindName("lstPassGroupOptExpiredExp");
      this.Sample = (Grid) ((FrameworkElement) this).FindName("Sample");
      this.btnDownHelp = (Button) ((FrameworkElement) this).FindName("btnDownHelp");
      this.ImageDownload = (Rectangle) ((FrameworkElement) this).FindName("ImageDownload");
      this.ImageHelp = (Rectangle) ((FrameworkElement) this).FindName("ImageHelp");
      this.TextHelp = (TextBlock) ((FrameworkElement) this).FindName("TextHelp");
      this.TextDownload = (TextBlock) ((FrameworkElement) this).FindName("TextDownload");
      this.gridSplash = (Grid) ((FrameworkElement) this).FindName("gridSplash");
      this.LayoutColor = (SolidColorBrush) ((FrameworkElement) this).FindName("LayoutColor");
      this.txtSplash = (TextBlock) ((FrameworkElement) this).FindName("txtSplash");
      this.txtSplashTemp = (TextBlock) ((FrameworkElement) this).FindName("txtSplashTemp");
      this.progressBar = (ProgressBar) ((FrameworkElement) this).FindName("progressBar");
    }
        */

        public class PivotCallbacks
        {
            public Action Init;
            public Action OnActivated;
            public Action<CancelEventArgs> OnBackKeyPress;
        }
    }
}

