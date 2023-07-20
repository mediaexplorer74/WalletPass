// Decompiled with JetBrains decompiler
// Type: WalletPass.pkPassGroupPage
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WalletPass.Resources;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;

namespace WalletPass
{
  public class pkPassGroupPage : PhoneApplicationPage
  {
    private ApplicationBarIconButton _btnLocation;
    private ApplicationBarIconButton _btnPin;
    private ApplicationBarIconButton _btnShare;
    private ApplicationBarIconButton _btnInfo;
    private ApplicationBarMenuItem _btnDelete;
    private ApplicationBarMenuItem _btnArchive;
    private ApplicationBarMenuItem _btnArchiveDelete;
    private ApplicationBarMenuItem _btnCalendar;
    private ApplicationBarMenuItem _btnSetRelevantDate;
    private ClaseSaveCalendar _calendarManage = new ClaseSaveCalendar();
    private AppointmentStore _appointmentStore;
    private bool isTombstoned = true;
    internal Grid LayoutRoot;
    internal StackPanel pivotItemIndex;
    internal Pivot pivotGroup;
    private bool _contentLoaded;

    public pkPassGroupPage()
    {
      this.InitializeComponent();
      this._btnDelete = new ApplicationBarMenuItem();
      this._btnDelete.Text = AppResources.AppBarButtonDelete;
      this._btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this._btnCalendar = new ApplicationBarMenuItem();
      this._btnCalendar.Text = AppResources.AppBarButtonCalendar;
      this._btnCalendar.Click += new EventHandler(this.btnCalendar_Click);
      this._btnSetRelevantDate = new ApplicationBarMenuItem();
      this._btnSetRelevantDate.Text = AppResources.AppBarButtonSetRelevantDate;
      this._btnSetRelevantDate.Click += new EventHandler(this.btnSetRelevantDate_Click);
      this._btnArchive = new ApplicationBarMenuItem();
      this._btnArchive.Text = AppResources.AppBarButtonArchiveAdd;
      this._btnArchive.Click += new EventHandler(this._btnArchive_Click);
      this._btnArchiveDelete = new ApplicationBarMenuItem();
      this._btnArchiveDelete.Text = AppResources.AppBarButtonArchiveRemove;
      this._btnArchiveDelete.Click += new EventHandler(this._btnArchiveDelete_Click);
      this._btnLocation = new ApplicationBarIconButton();
      this._btnLocation.IconUri = new Uri("/Assets/AppBar/appbar.globe.png", UriKind.Relative);
      this._btnLocation.Text = AppResources.AppBarButtonLocation;
      this._btnLocation.IsEnabled = false;
      this._btnLocation.Click += new EventHandler(this.btnLocation_Click);
      this._btnInfo = new ApplicationBarIconButton();
      this._btnInfo.IconUri = new Uri("/Assets/AppBar/appbar.information.png", UriKind.Relative);
      this._btnInfo.Text = AppResources.AppBarButtonInfo;
      this._btnInfo.Click += new EventHandler(this.btnInfo_Click);
      this._btnPin = new ApplicationBarIconButton();
      this._btnPin.IconUri = new Uri("/Assets/AppBar/appbar.pin.png", UriKind.Relative);
      this._btnPin.Text = AppResources.AppBarButtonPinToStart;
      this._btnPin.Click += new EventHandler(this.btnPin_Click);
      this._btnShare = new ApplicationBarIconButton();
      this._btnShare.IconUri = new Uri("/Assets/AppBar/appbar.share.png", UriKind.Relative);
      this._btnShare.Text = AppResources.AppBarButtonShare;
      this._btnShare.Click += new EventHandler(this.btnShare_Click);
      ((UIElement) this).Opacity = 0.0;
    }

    protected virtual async void OnNavigatedTo(NavigationEventArgs e)
    {
      // ISSUE: reference to a compiler-generated method
      this.\u003C\u003En__FabricatedMethodb(e);
      this.addApplicationBarButtons();
      try
      {
        if (!App._isTombStoned)
        {
          if (e.NavigationMode == null)
          {
            ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => this.showTransitionInForward()));
            if (App._currentAppCalendar == null)
              await this.CreateAppointmentCalendar();
          }
          else if (e.NavigationMode == 1)
          {
            if (App._infoPage)
              ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
              {
                TurnOverTransition turnOverTransition = new TurnOverTransition();
                turnOverTransition.Mode = !App._rightGesture ? TurnOverTransitionMode.F90ToLeft : TurnOverTransitionMode.F90ToRight;
                PhoneApplicationPage content = (PhoneApplicationPage) e.Content;
                ITransition transition = turnOverTransition.GetTransition((UIElement) content);
                transition.Completed += (EventHandler) ((param0, param1) => ((Control) App.RootFrame).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 240, (byte) 247, byte.MaxValue)));
                transition.Begin();
              }));
            if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\" + App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass))
              File.Delete(ApplicationData.Current.LocalFolder.Path + "\\" + App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass);
            if (App._reconstructPages)
            {
              App._reconstructPages = false;
              for (int index = 0; index <= App._tempPassGroup.Count - 1; ++index)
                App._tempPassGroup[index].getFrontPass(true);
            }
          }
        }
        else
        {
          StateManager.LoadStateAll((PhoneApplicationPage) this);
          this.pivotGroup.SelectedIndex = App._groupItemIndex;
          App._isTombStoned = false;
          for (int index = 0; index <= App._tempPassGroup.Count - 1; ++index)
            App._tempPassGroup[index].getFrontPass(true);
          ((Control) App.RootFrame).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 240, (byte) 247, byte.MaxValue));
          if (App._currentAppCalendar == null)
            await this.CreateAppointmentCalendar();
        }
        App._deletePasses.Clear();
        App._infoPage = false;
        App._pageEntry = false;
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.pivotGroup).Items).Clear();
        PivotItem pvt = (PivotItem) null;
        for (int index = 0; index <= App._tempPassGroup.Count - 1; ++index)
        {
          pvt = new PivotItem();
          ((FrameworkElement) pvt).Margin = new Thickness(0.0, -10.0, 0.0, 0.0);
          ((ContentControl) pvt).Content = (object) this.Update_Data(index);
          ((PresentationFrameworkCollection<object>) ((ItemsControl) this.pivotGroup).Items).Add((object) pvt);
          pvt = (PivotItem) null;
        }
        this.loadPivotItemIndexCircles();
        this.pivotGroup.SelectedIndex = App._groupItemIndex;
        this.ApplicationBar.IsVisible = true;
        ((UIElement) this).Opacity = 1.0;
      }
      catch
      {
        if (!((Page) this).NavigationService.CanGoBack)
          return;
        ((Page) this).NavigationService.GoBack();
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      App._groupItemIndex = this.pivotGroup.SelectedIndex;
      if (!this.isTombstoned)
        return;
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    private Grid Update_Data(int index)
    {
      if (App._archived)
        App._tempPassGroup[index].getFrontPass();
      Grid grid = new Grid();
      Grid parent = (Grid) ((FrameworkElement) App._tempPassGroup[index].passPageRender).Parent;
      if (parent != null && ((PresentationFrameworkCollection<UIElement>) ((Panel) parent).Children).IndexOf((UIElement) App._tempPassGroup[index].passPageRender) != -1)
        ((PresentationFrameworkCollection<UIElement>) ((Panel) parent).Children).Remove((UIElement) App._tempPassGroup[index].passPageRender);
      if (((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Count > 0)
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).RemoveAt(0);
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children).Add((UIElement) App._tempPassGroup[index].passPageRender);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
      ((Panel) this.LayoutRoot).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 15, (byte) 32, (byte) 40));
      if (App._tempPassGroup[index].background == null)
      {
        GradientStop gradientStop1 = new GradientStop();
        GradientStop gradientStop2 = new GradientStop();
        linearGradientBrush.StartPoint = new System.Windows.Point(0.5, 0.0);
        linearGradientBrush.EndPoint = new System.Windows.Point(0.5, 1.0);
        gradientStop1.Color = App._tempPassGroup[index].backgroundColorTop;
        gradientStop2.Color = App._tempPassGroup[index].backgroundColor;
        gradientStop1.Offset = 0.0;
        gradientStop2.Offset = 1.0;
        ((PresentationFrameworkCollection<GradientStop>) ((GradientBrush) linearGradientBrush).GradientStops).Add(gradientStop1);
        ((PresentationFrameworkCollection<GradientStop>) ((GradientBrush) linearGradientBrush).GradientStops).Add(gradientStop2);
        ((Panel) grid).Background = (Brush) linearGradientBrush;
      }
      else
        ((Panel) grid).Background = (Brush) new ImageBrush()
        {
          ImageSource = (ImageSource) App._tempPassGroup[index].backgroundImage
        };
      ImageBrush imageBrush = new ImageBrush();
      imageBrush.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/PassHeadersList/" + App._tempPassGroup[index].type + "T.png", UriKind.Relative));
      ((TileBrush) imageBrush).Stretch = (Stretch) 1;
      ((UIElement) grid).OpacityMask = (Brush) imageBrush;
      return grid;
    }

    public void ClearApplicationBar()
    {
      while (this.ApplicationBar.Buttons.Count > 0)
        this.ApplicationBar.Buttons.RemoveAt(0);
      while (this.ApplicationBar.MenuItems.Count > 0)
        this.ApplicationBar.MenuItems.RemoveAt(0);
    }

    private void addApplicationBarButtons()
    {
      this.ClearApplicationBar();
      if (App._archived)
      {
        this.ApplicationBar.Buttons.Add((object) this._btnLocation);
        this.ApplicationBar.Buttons.Add((object) this._btnInfo);
        this.ApplicationBar.MenuItems.Add((object) this._btnDelete);
        this.ApplicationBar.MenuItems.Add((object) this._btnArchiveDelete);
      }
      else
      {
        this.ApplicationBar.Buttons.Add((object) this._btnPin);
        this.ApplicationBar.Buttons.Add((object) this._btnShare);
        this.ApplicationBar.Buttons.Add((object) this._btnLocation);
        this.ApplicationBar.Buttons.Add((object) this._btnInfo);
        this.ApplicationBar.MenuItems.Add((object) this._btnCalendar);
        this.ApplicationBar.MenuItems.Add((object) this._btnDelete);
        this.ApplicationBar.MenuItems.Add((object) this._btnSetRelevantDate);
        this.ApplicationBar.MenuItems.Add((object) this._btnArchive);
      }
    }

    private void btnInfo_Click(object sender, EventArgs e)
    {
      App._rightGesture = Convert.ToBoolean(new Random().Next(0, 2));
      this.btnInfoClick();
    }

    private void btnInfoClick()
    {
      ((Control) App.RootFrame).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 0, (byte) 0));
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() =>
      {
        TurnOverTransition turnOverTransition = new TurnOverTransition();
        turnOverTransition.Mode = !App._rightGesture ? TurnOverTransitionMode.F0ToLeft : TurnOverTransitionMode.F0ToRight;
        PhoneApplicationPage element = (PhoneApplicationPage) this;
        ITransition transition = turnOverTransition.GetTransition((UIElement) element);
        transition.Completed += (EventHandler) ((param0, param1) =>
        {
          App._groupItemIndex = this.pivotGroup.SelectedIndex;
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/infoPage.xaml", UriKind.Relative))));
        });
        transition.Begin();
      }));
    }

    private void btnLocation_Click(object sender, EventArgs e)
    {
      App._groupItemIndex = this.pivotGroup.SelectedIndex;
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative))));
    }

    private void btnSetRelevantDate_Click(object sender, EventArgs e)
    {
      App._groupItemIndex = this.pivotGroup.SelectedIndex;
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/setRelevantDatePage.xaml", UriKind.Relative))));
    }

    private void btnPin_Click(object sender, EventArgs e)
    {
      App._tempPassClass = App._tempPassGroup[this.pivotGroup.SelectedIndex];
      TileUpdate tileUpdate = new TileUpdate();
      tileUpdate.RenderWideTile();
      tileUpdate.RenderMediumTile();
      tileUpdate.RenderSmallTile();
      FlipTileData flipTileData1 = new FlipTileData();
      ((ShellTileData) flipTileData1).Title = "";
      flipTileData1.SmallBackgroundImage = tileUpdate.SmallImage;
      ((StandardTileData) flipTileData1).BackgroundImage = tileUpdate.ImageFront;
      ((StandardTileData) flipTileData1).BackBackgroundImage = tileUpdate.ImageBack;
      flipTileData1.WideBackgroundImage = tileUpdate.WideImageFront;
      flipTileData1.WideBackBackgroundImage = tileUpdate.WideImageBack;
      FlipTileData flipTileData2 = flipTileData1;
      ShellTile.Create(new Uri("/MainPage.xaml?SecondaryTile=" + App._tempPassClass.serialNumberGUID, UriKind.Relative), (ShellTileData) flipTileData2, true);
      App._tempPassClass.updateSettings();
      this._btnPin.IsEnabled = false;
    }

    private bool isPinned(string serialNumber)
    {
      Uri _uri = new Uri("/MainPage.xaml?SecondaryTile=" + serialNumber, UriKind.Relative);
      return ShellTile.ActiveTiles.Where<ShellTile>((Func<ShellTile, bool>) (t => t.NavigationUri == _uri)).FirstOrDefault<ShellTile>() != null;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(AppResources.msgBoxDelete, AppResources.msgBoxDeleteCaption, (MessageBoxButton) 1) != 1)
        return;
      App._deletePasses.Add(App._tempPassGroup[this.pivotGroup.SelectedIndex]);
      if (App._tempPassGroup.Count > 1)
      {
        App._tempPassGroup.RemoveAt(this.pivotGroup.SelectedIndex);
        this.removePivotItemIndexCircles(this.pivotGroup.SelectedIndex);
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.pivotGroup).Items).RemoveAt(this.pivotGroup.SelectedIndex);
      }
      else
      {
        this.showTransitionOutBackward();
        ((Page) this).NavigationService.GoBack();
      }
    }

    private void _btnArchive_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(AppResources.msgBoxArchive, AppResources.msgBoxArchiveCaption, (MessageBoxButton) 1) != 1)
        return;
      App._archivePasses.Add(App._tempPassGroup[this.pivotGroup.SelectedIndex]);
      if (App._tempPassGroup.Count > 1)
      {
        App._tempPassGroup.RemoveAt(this.pivotGroup.SelectedIndex);
        this.removePivotItemIndexCircles(this.pivotGroup.SelectedIndex);
        ((PresentationFrameworkCollection<object>) ((ItemsControl) this.pivotGroup).Items).RemoveAt(this.pivotGroup.SelectedIndex);
      }
      else
      {
        this.showTransitionOutBackward();
        ((Page) this).NavigationService.GoBack();
      }
    }

    private async void btnCalendar_Click(object sender, EventArgs e)
    {
      App._tempPassClass = App._tempPassGroup[this.pivotGroup.SelectedIndex];
      AppSettings appSettings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      if (!await this._calendarManage.saveAppointment(App._tempPassClass, App._currentAppCalendar))
        return;
      WalletPass.IO.SaveData(App._passcollection);
      ToastPrompt toastPrompt = new ToastPrompt();
      Color resource = (Color) Application.Current.Resources[(object) "PhoneAccentColor"];
      toastPrompt.Background = (Brush) new SolidColorBrush(resource);
      toastPrompt.Foreground = (Brush) new SolidColorBrush(Colors.White);
      toastPrompt.FontSize = 24.0;
      ((FrameworkElement) toastPrompt).Height = 70.0;
      toastPrompt.ImageHeight = 60.0;
      toastPrompt.FontFamily = new FontFamily("Segoe WP Light");
      toastPrompt.Title = " ";
      toastPrompt.Message = AppResources.ToastSaveCalendarEvent;
      toastPrompt.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/Tiles/appbar.calendar.png", UriKind.Relative));
      toastPrompt.Show();
      this._btnCalendar.IsEnabled = false;
    }

    private void _btnArchiveDelete_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(AppResources.msgBoxArchiveRemove, AppResources.msgBoxArchiveCaption, (MessageBoxButton) 1) != 1 || !((Page) this).NavigationService.CanGoBack)
        return;
      ((Page) this).NavigationService.GoBack();
      App._archivePasses.Add(App._tempPassGroup[this.pivotGroup.SelectedIndex]);
    }

    protected virtual void OnBackKeyPress(CancelEventArgs e)
    {
      this.showTransitionOutBackward();
      this.isTombstoned = false;
      base.OnBackKeyPress(e);
    }

    private void loadPivotItemIndexCircles()
    {
      ((PresentationFrameworkCollection<UIElement>) ((Panel) this.pivotItemIndex).Children).Clear();
      for (int index = 0; index <= App._tempPassGroup.Count - 1; ++index)
      {
        Ellipse ellipse = new Ellipse();
        ((FrameworkElement) ellipse).Width = 12.0;
        ((FrameworkElement) ellipse).Height = 12.0;
        ((FrameworkElement) ellipse).Margin = new Thickness(7.0, 0.0, 7.0, 0.0);
        ((Shape) ellipse).Stroke = (Brush) new SolidColorBrush(App._tempPassGroup[index].backgroundColor);
        ((Shape) ellipse).Fill = (Brush) new SolidColorBrush(App._tempPassGroup[index].backgroundColor);
        ((Shape) ellipse).StrokeThickness = 2.0;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) this.pivotItemIndex).Children).Add((UIElement) ellipse);
      }
      if (this.pivotGroup == null || this.pivotGroup.SelectedIndex == -1)
        return;
      this.loadPivotItemIndexCircles(this.pivotGroup.SelectedIndex);
    }

    private void loadPivotItemIndexCircles(int index)
    {
      for (int index1 = 0; index1 <= ((PresentationFrameworkCollection<UIElement>) ((Panel) this.pivotItemIndex).Children).Count - 1; ++index1)
      {
        Ellipse child = (Ellipse) ((PresentationFrameworkCollection<UIElement>) ((Panel) this.pivotItemIndex).Children)[index1];
        ((Shape) child).Stroke = (Brush) new SolidColorBrush(App._tempPassGroup[index1].backgroundColor);
        ((Shape) child).Fill = (Brush) new SolidColorBrush(App._tempPassGroup[index1].backgroundColor);
        if (index == index1)
          ((Shape) child).Fill = (Brush) new SolidColorBrush(Colors.Transparent);
      }
    }

    private void removePivotItemIndexCircles(int index) => ((PresentationFrameworkCollection<UIElement>) ((Panel) this.pivotItemIndex).Children).RemoveAt(index);

    private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (!App._archived)
      {
        this.ApplicationBar.IsVisible = true;
        this._btnPin.IsEnabled = !this.isPinned(App._tempPassGroup[this.pivotGroup.SelectedIndex].serialNumberGUID);
        this._btnShare.IsEnabled = File.Exists(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass);
        this._btnCalendar.IsEnabled = (!await this._calendarManage.existAppointment(App._tempPassGroup[this.pivotGroup.SelectedIndex].idAppointment, App._currentAppCalendar) ? 1 : 0) != 0;
        this._btnSetRelevantDate.IsEnabled = App._tempPassGroup[this.pivotGroup.SelectedIndex].hasRelevantDate;
        if (App._passcollection[App._passcollection.passIndex(App._tempPassGroup[this.pivotGroup.SelectedIndex])].isUpdated)
        {
          App._passcollection[App._passcollection.passIndex(App._tempPassGroup[this.pivotGroup.SelectedIndex])].isUpdated = false;
          WalletPass.IO.SaveData(App._passcollection);
        }
      }
      this._btnLocation.IsEnabled = App._tempPassGroup[this.pivotGroup.SelectedIndex].Locations.Count > 0;
      if (App._tempPassGroup[this.pivotGroup.SelectedIndex].background == null)
      {
        this.ApplicationBar.BackgroundColor = App._tempPassGroup[this.pivotGroup.SelectedIndex].backgroundColor;
        if (App._tempPassGroup[this.pivotGroup.SelectedIndex].backgroundColor.B > (byte) 210 & App._tempPassGroup[this.pivotGroup.SelectedIndex].backgroundColor.R > (byte) 210 & App._tempPassGroup[this.pivotGroup.SelectedIndex].backgroundColor.G > (byte) 210)
          this.ApplicationBar.ForegroundColor = Colors.Black;
        else
          this.ApplicationBar.ForegroundColor = Colors.White;
      }
      else
      {
        this.ApplicationBar.BackgroundColor = Color.FromArgb(byte.MaxValue, (byte) 15, (byte) 32, (byte) 40);
        this.ApplicationBar.ForegroundColor = Colors.White;
      }
      this.loadPivotItemIndexCircles(this.pivotGroup.SelectedIndex);
    }

    public async Task CreateAppointmentCalendar()
    {
      this._appointmentStore = await AppointmentManager.RequestStoreAsync((AppointmentStoreAccessType) 0);
      if (!((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values).ContainsKey("FirstRun"))
      {
        await this.CheckForAndCreateAppointmentCalendars();
        this._appointmentStore.ChangeTracker.Enable();
        this._appointmentStore.ChangeTracker.Reset();
        ((IDictionary<string, object>) ApplicationData.Current.LocalSettings.Values)["FirstRun"] = (object) false;
      }
      else
        await this.CheckForAndCreateAppointmentCalendars();
    }

    public async Task CheckForAndCreateAppointmentCalendars()
    {
      IReadOnlyList<AppointmentCalendar> appCalendars = await this._appointmentStore.FindAppointmentCalendarsAsync((FindAppointmentCalendarsOptions) 1);
      AppointmentCalendar appCalendar = (AppointmentCalendar) null;
      AppointmentCalendar appointmentCalendarAsync;
      if (appCalendars.Count != 0)
        appointmentCalendarAsync = appCalendars[0];
      else
        appointmentCalendarAsync = await this._appointmentStore.CreateAppointmentCalendarAsync("Wallet Pass Calendar");
      appCalendar = appointmentCalendarAsync;
      appCalendar.put_OtherAppReadAccess((AppointmentCalendarOtherAppReadAccess) 2);
      appCalendar.put_OtherAppWriteAccess((AppointmentCalendarOtherAppWriteAccess) 1);
      appCalendar.put_SummaryCardView((AppointmentSummaryCardView) 1);
      await appCalendar.SaveAsync();
      App._currentAppCalendar = appCalendar;
    }

    private void btnShare_Click(object sender, EventArgs e)
    {
      this.registerForShare();
      DataTransferManager.ShowShareUI();
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
      DataRequest request = e.Request;
      request.Data.Properties.put_Title(App._tempPassGroup[this.pivotGroup.SelectedIndex].organizationName + " passbook");
      request.Data.Properties.put_Description("");
      DataRequestDeferral deferral = request.GetDeferral();
      try
      {
        File.Copy(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass, ApplicationData.Current.LocalFolder.Path + "\\" + App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass, true);
        StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(App._tempPassGroup[this.pivotGroup.SelectedIndex].filenamePass);
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
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/pkPassGroupPage.xaml", UriKind.Relative));
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.pivotItemIndex = (StackPanel) ((FrameworkElement) this).FindName("pivotItemIndex");
      this.pivotGroup = (Pivot) ((FrameworkElement) this).FindName("pivotGroup");
    }
  }
}
