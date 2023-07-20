// Decompiled with JetBrains decompiler
// Type: WalletPass.pkPassPageEntry
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Coding4Fun.Toolkit.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Wallet;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using WalletPass.Resources;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Email;
using Windows.Phone.Storage.SharedAccess;
using Windows.Phone.System.Analytics;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.System;

namespace WalletPass
{
  public class pkPassPageEntry : PhoneApplicationPage
  {
    private bool errorLoading;
    private ApplicationBarIconButton _btnLocation;
    private ApplicationBarIconButton _btnSave;
    private ApplicationBarIconButton _btnInfo;
    private AppointmentStore _appointmentStore;
    private StorageFile _temp;
    private Popup _popup;
    internal PhoneApplicationPage phoneApplicationPage;
    internal Grid LayoutRoot;
    internal Border passPageBorder;
    internal Grid passPage;
    private bool _contentLoaded;

    public pkPassPageEntry()
    {
      this.InitializeComponent();
      this.InitializeAll();
    }

    private void InitializeAll()
    {
      this._btnSave = new ApplicationBarIconButton();
      this._btnSave.IconUri = new Uri("/Assets/AppBar/appbar.save.png", UriKind.Relative);
      this._btnSave.Text = AppResources.AppBarButtonSave;
      this._btnSave.Click += new EventHandler(this.btnSave_Click);
      this._btnLocation = new ApplicationBarIconButton();
      this._btnLocation.IconUri = new Uri("/Assets/AppBar/appbar.globe.png", UriKind.Relative);
      this._btnLocation.Text = AppResources.AppBarButtonLocation;
      this._btnLocation.IsEnabled = false;
      this._btnLocation.Click += new EventHandler(this.btnLocation_Click);
      this._btnInfo = new ApplicationBarIconButton();
      this._btnInfo.IconUri = new Uri("/Assets/AppBar/appbar.information.png", UriKind.Relative);
      this._btnInfo.Text = AppResources.AppBarButtonInfo;
      this._btnInfo.Click += new EventHandler(this.btnInfo_Click);
      this.ApplicationBar.Buttons.Add((object) this._btnSave);
      this.ApplicationBar.Buttons.Add((object) this._btnLocation);
      this.ApplicationBar.Buttons.Add((object) this._btnInfo);
      TouchPanel.EnabledGestures = GestureType.None;
    }

    protected virtual async void OnNavigatedTo(NavigationEventArgs e)
    {
      // ISSUE: reference to a compiler-generated method
      this.\u003C\u003En__FabricatedMethod9(e);
      App app = Application.Current as App;
      if (app.FilePickerContinuationArgs != null)
        this.ContinueFileSavePicker(app.FilePickerContinuationArgs);
      if (((Page) this).NavigationContext.QueryString.ContainsKey("fileToken") & e.NavigationMode == 0)
      {
        App._passcollection = new ClasePassCollection();
        App._passcollection.AddAllNew(WalletPass.IO.LoadDataPasses());
        App._passcollectionArchived = new ClasePassCollection();
        App._passcollectionArchived.AddAllNew(await WalletPass.IO.LoadDataPassesArchived());
        App._infoPage = false;
        App._pageEntry = true;
        IStorageFile istorageFile = await SharedStorageAccessManager.CopySharedFileAsync(ApplicationData.Current.LocalFolder, "pkpass.file", (NameCollisionOption) 1, ((Page) this).NavigationContext.QueryString["fileToken"]);
        ClasePassDecryptor decryptor = new ClasePassDecryptor();
        int result = -1;
        result = await decryptor.extractCompressedFile("pkpass.file", ApplicationData.Current.LocalFolder);
        switch (result)
        {
          case 0:
            App._tempPassClass = new ClasePass(decryptor.clsPass, true);
            App._tempPassClass.getFrontPass();
            App._tempPassClass.filenamePass = SharedStorageAccessManager.GetSharedFileName(((Page) this).NavigationContext.QueryString["fileToken"]);
            this.Update_Data();
            break;
          case 1:
            this.msgBoxHTML("pkpass.file", SharedStorageAccessManager.GetSharedFileName(((Page) this).NavigationContext.QueryString["fileToken"]));
            break;
          default:
            this.msgBoxErrorLoading("pkpass.file", SharedStorageAccessManager.GetSharedFileName(((Page) this).NavigationContext.QueryString["fileToken"]));
            break;
        }
      }
      else
      {
        if (e.NavigationMode != 1)
          return;
        if (App._isTombStoned)
        {
          App._isTombStoned = false;
          StateManager.LoadStateAll((PhoneApplicationPage) this);
          App._tempPassClass.getFrontPass(true);
          this.Update_Data();
        }
        else
        {
          if (App._infoPage)
          {
            if (!this.errorLoading)
            {
              App._infoPage = false;
              TurnOverTransition turnOverTransition = new TurnOverTransition();
              turnOverTransition.Mode = !App._rightGesture ? TurnOverTransitionMode.F90ToLeft : TurnOverTransitionMode.F90ToRight;
              PhoneApplicationPage content = (PhoneApplicationPage) e.Content;
              ITransition transition = turnOverTransition.GetTransition((UIElement) content);
              transition.Completed += (EventHandler) ((param0, param1) => ((Control) App.RootFrame).Background = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorMain, (Type) null, (object) null, (CultureInfo) null));
              transition.Begin();
            }
            else
              Application.Current.Terminate();
          }
          if (!App._reconstructPages)
            return;
          App._reconstructPages = false;
          App._tempPassClass.getFrontPass(true);
          this.Update_Data();
        }
      }
    }

    protected virtual void OnNavigatedFrom(NavigationEventArgs e)
    {
      ((Page) this).OnNavigatedFrom(e);
      StateManager.SaveStateAll((PhoneApplicationPage) this);
    }

    private void Update_Data()
    {
      bool flag = App._passcollection.passExists(App._tempPassClass) || App._passcollectionArchived.passExists(App._tempPassClass);
      App._tempPassClass.getBackPass();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPage).Children).Add((UIElement) App._tempPassClass.passPageRender);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
      this.ApplicationBar.IsVisible = true;
      this._btnLocation.IsEnabled = App._tempPassClass.Locations.Count > 0;
      this._btnSave.IsEnabled = !flag;
      ((Panel) this.LayoutRoot).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 15, (byte) 32, (byte) 40));
      if (App._tempPassClass.background == null)
      {
        GradientStop gradientStop1 = new GradientStop();
        GradientStop gradientStop2 = new GradientStop();
        linearGradientBrush.StartPoint = new Point(0.5, 0.0);
        linearGradientBrush.EndPoint = new Point(0.5, 1.0);
        gradientStop1.Color = App._tempPassClass.backgroundColorTop;
        gradientStop2.Color = App._tempPassClass.backgroundColor;
        gradientStop1.Offset = 0.0;
        gradientStop2.Offset = 1.0;
        ((PresentationFrameworkCollection<GradientStop>) ((GradientBrush) linearGradientBrush).GradientStops).Add(gradientStop1);
        ((PresentationFrameworkCollection<GradientStop>) ((GradientBrush) linearGradientBrush).GradientStops).Add(gradientStop2);
        ((Panel) this.passPage).Background = (Brush) linearGradientBrush;
        this.ApplicationBar.BackgroundColor = App._tempPassClass.backgroundColor;
        if (App._tempPassClass.backgroundColor.B > (byte) 210 & App._tempPassClass.backgroundColor.R > (byte) 210 & App._tempPassClass.backgroundColor.G > (byte) 210)
          this.ApplicationBar.ForegroundColor = Colors.Black;
        else
          this.ApplicationBar.ForegroundColor = Colors.White;
      }
      else
        ((Panel) this.passPage).Background = (Brush) new ImageBrush()
        {
          ImageSource = (ImageSource) App._tempPassClass.backgroundImage
        };
      ImageBrush imageBrush = new ImageBrush();
      imageBrush.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/PassHeadersList/" + App._tempPassClass.type + "T.png", UriKind.Relative));
      ((TileBrush) imageBrush).Stretch = (Stretch) 1;
      ((UIElement) this.passPageBorder).OpacityMask = (Brush) imageBrush;
    }

    private async void saveWalletItem()
    {
      try
      {
        WalletItem wItem = Microsoft.Phone.Wallet.Wallet.FindItem(App._tempPassClass.serialNumberGUID);
        if (wItem != null)
          Microsoft.Phone.Wallet.Wallet.Remove(wItem);
        Deal item = new Deal(App._tempPassClass.serialNumberGUID);
        ((WalletItem) item).BarcodeImage = (BitmapSource) App._tempPassClass.barCode;
        ((WalletItem) item).DisplayName = App._tempPassClass.organizationName;
        ((WalletItem) item).Logo99x99 = (BitmapSource) App._tempPassClass.iconImage;
        ((WalletItem) item).Logo159x159 = (BitmapSource) App._tempPassClass.iconImage;
        ((WalletItem) item).Logo336x336 = (BitmapSource) App._tempPassClass.iconImage;
        item.MerchantName = App._tempPassClass.organizationName;
        ((WalletItem) item).NavigationUri = new Uri("/MainPage.xaml?WalletItem=" + App._tempPassClass.serialNumberGUID, UriKind.Relative);
        if (App._tempPassClass.relevantDate.Year != 1)
          item.ExpirationDate = new DateTime?(App._tempPassClass.relevantDate);
        await ((WalletItem) item).SaveAsync();
      }
      catch
      {
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (!App.IsTrial | App._tempPassClass.serialNumber == "5342eac6ef664" & App._tempPassClass.organizationName == "Passcreator")
      {
        int num;
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => num = await ClasePassbookWebOperations.registerDevice(App._tempPassClass) ? 1 : 0));
        ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => await this.savePass()));
      }
      else
      {
        if (MessageBox.Show(AppResources.msgBoxTrial, AppResources.msgBoxTrialCaption, (MessageBoxButton) 1) != 1)
          return;
        new MarketplaceDetailTask()
        {
          ContentIdentifier = "61e56c4f-08e0-4427-af8d-7253603353cb",
          ContentType = ((MarketplaceContentType) 1)
        }.Show();
      }
    }

    private async Task savePass()
    {
      AppSettings settings = new AppSettings();
      StringToColorConverter toColorConverter = new StringToColorConverter();
      if (App._passcollection.passExists(App._tempPassClass))
        return;
      if (settings.saveAddWalletEnabled)
      {
        if (settings.saveAddWalletOption == 0)
        {
          this.saveWalletItem();
        }
        else
        {
          App._mswalletPassCollection.Add(App._tempPassClass.serialNumberGUID);
          WalletPass.IO.SaveDataMSWallet(App._mswalletPassCollection);
          int num;
          ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (async () => num = await ClasePassbookWebOperations.pushMSWallet(App._tempPassClass.organizationName, App._tempPassClass.webServiceURL) ? 1 : 0));
        }
      }
      ClaseSaveCalendar _calendarManage = new ClaseSaveCalendar();
      if (settings.calendarOnSave)
      {
        await this.CreateAppointmentCalendar();
        int num = await _calendarManage.saveAppointment(App._tempPassClass, App._currentAppCalendar) ? 1 : 0;
      }
      if (!Directory.Exists(ApplicationData.Current.LocalFolder.Path + "\\savedPasses"))
        Directory.CreateDirectory(ApplicationData.Current.LocalFolder.Path + "\\savedPasses");
      string fileName = App._tempPassClass.filenamePass.Substring(0, App._tempPassClass.filenamePass.LastIndexOf("."));
      string fileNameExt = App._tempPassClass.filenamePass.Substring(App._tempPassClass.filenamePass.LastIndexOf("."));
      string fileNameCount = "";
      int i = 0;
      while (true)
      {
        if (File.Exists(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + fileName + fileNameCount + fileNameExt))
        {
          ++i;
          fileNameCount = "(" + Convert.ToString(i) + ")";
        }
        else
          break;
      }
      App._tempPassClass.filenamePass = fileName + fileNameCount + fileNameExt;
      File.Copy(ApplicationData.Current.LocalFolder.Path + "\\pkpass.file", ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\" + App._tempPassClass.filenamePass, true);
      App._tempPassClass.dateModified = File.GetCreationTime(ApplicationData.Current.LocalFolder.Path + "\\pkpass.file");
      this.showTaskBarText(AppResources.updateRegisteringDevice);
      App._passcollection.AddNew(App._tempPassClass);
      WalletPass.IO.SaveData(App._passcollection);
      this._btnSave.IsEnabled = false;
      TouchPanel.EnabledGestures = GestureType.None;
      ToastPrompt toast = new ToastPrompt();
      Color accentColor = (Color) Application.Current.Resources[(object) "PhoneAccentColor"];
      toast.Background = (Brush) new SolidColorBrush(accentColor);
      toast.Foreground = (Brush) new SolidColorBrush(Colors.White);
      toast.FontSize = 24.0;
      ((FrameworkElement) toast).Height = 70.0;
      toast.ImageHeight = 60.0;
      toast.FontFamily = new FontFamily("Segoe WP Light");
      toast.Title = " ";
      toast.Message = AppResources.ToastSave;
      switch (App._tempPassClass.type)
      {
        case "boardingPass":
          toast.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/Toast/WP-Toast-bP.png", UriKind.Relative));
          break;
        case "eventTicket":
          toast.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/Toast/WP-Toast-eT.png", UriKind.Relative));
          break;
        case "coupon":
          toast.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/Toast/WP-Toast-C.png", UriKind.Relative));
          break;
        default:
          toast.ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/Toast/WP-Toast-G.png", UriKind.Relative));
          break;
      }
      toast.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(this.toast_Completed);
      toast.Show();
    }

    private void toast_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
    {
      TouchPanel.EnabledGestures = GestureType.DragComplete;
      TouchPanel.EnabledGestures = GestureType.HorizontalDrag;
    }

    private void btnLocation_Click(object sender, EventArgs e)
    {
      this.showTransitionTurnstile();
      ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/MapPage.xaml", UriKind.Relative))));
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
        transition.Completed += (EventHandler) ((param0, param1) => ((DependencyObject) this).Dispatcher.BeginInvoke((Action) (() => ((Page) this).NavigationService.Navigate(new Uri("/infoPage.xaml", UriKind.Relative)))));
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
      if (Math.Abs(num3) <= 8.0)
        return;
      App._rightGesture = num3 > 0.0;
      this.btnInfoClick();
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

    private void showTransitionTurnstile()
    {
      TurnstileTransition turnstileTransition = new TurnstileTransition();
      turnstileTransition.Mode = TurnstileTransitionMode.ForwardOut;
      PhoneApplicationPage element = (PhoneApplicationPage) this;
      ITransition trans = turnstileTransition.GetTransition((UIElement) element);
      trans.Completed += (EventHandler) ((param0, param1) => trans.Stop());
      trans.Begin();
    }

    private void msgBoxErrorLoading(string pkPassFile, string filename)
    {
      HyperlinkButton hyperlinkButton = new HyperlinkButton();
      ((ContentControl) hyperlinkButton).Content = (object) "wallet_pass_support@outlook.com";
      ((FrameworkElement) hyperlinkButton).Margin = new Thickness(0.0, 28.0, 0.0, 8.0);
      ((FrameworkElement) hyperlinkButton).HorizontalAlignment = (HorizontalAlignment) 0;
      HyperlinkButton source = hyperlinkButton;
      ControlTiltEffect.TiltEffect.SetIsTiltEnabled((DependencyObject) source, true);
      CustomMessageBox customMessageBox1 = new CustomMessageBox();
      customMessageBox1.Caption = AppResources.msgBoxLoadErrorCaption;
      customMessageBox1.Message = AppResources.msgBoxLoadError;
      customMessageBox1.Content = (object) source;
      customMessageBox1.LeftButtonContent = (object) AppResources.msgBoxAnswerYes;
      customMessageBox1.RightButtonContent = (object) AppResources.msgBoxAnswerNo;
      customMessageBox1.IsFullScreen = false;
      CustomMessageBox customMessageBox2 = customMessageBox1;
      customMessageBox2.Dismissed += (EventHandler<DismissedEventArgs>) (async (s1, e1) =>
      {
        switch (e1.Result)
        {
          case CustomMessageBoxResult.LeftButton:
            await this.composeEmail(pkPassFile, filename);
            this.errorLoading = true;
            break;
          case CustomMessageBoxResult.RightButton:
            Application.Current.Terminate();
            break;
          default:
            Application.Current.Terminate();
            break;
        }
      });
      customMessageBox2.Show();
    }

    private async Task composeEmail(string pkPassFile, string filename)
    {
      EmailMessage objEmail = new EmailMessage();
      objEmail.put_Subject("Passbook loading issue");
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(pkPassFile);
      objEmail.To.Add(new EmailRecipient("wallet_pass_support@outlook.com"));
      objEmail.Attachments.Add(new EmailAttachment(filename, (IRandomAccessStreamReference) sourceFile));
      objEmail.put_Body(Environment.NewLine + Environment.NewLine + Environment.NewLine + "------------------------------------" + Environment.NewLine + "Wallet Pass " + this.GetVersionNumber() + Environment.NewLine + "DeviceID: " + HostInformation.PublisherHostId + Environment.NewLine + "Culture: " + CultureInfo.CurrentCulture.Name + Environment.NewLine + "------------------------------------");
      await EmailManager.ShowComposeNewEmailAsync(objEmail);
    }

    private string GetVersionNumber()
    {
      PackageVersion version = Package.Current.Id.Version;
      return string.Format("{0}.{1}.{2}.{3}", (object) version.Major, (object) version.Minor, (object) version.Build, (object) version.Revision);
    }

    private void msgBoxHTML(string pkPassFile, string filename)
    {
      CustomMessageBox customMessageBox = new CustomMessageBox()
      {
        Caption = AppResources.msgBoxHTMLErrorCaption,
        Message = AppResources.msgBoxHTMLError,
        LeftButtonContent = (object) AppResources.msgBoxAnswerYes,
        RightButtonContent = (object) AppResources.msgBoxAnswerNo,
        IsFullScreen = false
      };
      customMessageBox.Dismissed += (EventHandler<DismissedEventArgs>) (async (s1, e1) =>
      {
        switch (e1.Result)
        {
          case CustomMessageBoxResult.LeftButton:
            await this.saveHTMLFile(pkPassFile, filename);
            this.errorLoading = true;
            break;
          case CustomMessageBoxResult.RightButton:
            Application.Current.Terminate();
            break;
          default:
            Application.Current.Terminate();
            break;
        }
      });
      customMessageBox.Show();
    }

    private async Task saveHTMLFile(string pkPassFile, string filename)
    {
      StorageFolder localFolder = ApplicationData.Current.LocalFolder;
      StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(pkPassFile);
      FileSavePicker savePicker = new FileSavePicker();
      this._temp = sourceFile;
      savePicker.put_SuggestedFileName(filename);
      savePicker.FileTypeChoices.Add("HTML", (IList<string>) new List<string>()
      {
        ".html"
      });
      savePicker.PickSaveFileAndContinue();
    }

    public async void ContinueFileSavePicker(FileSavePickerContinuationEventArgs args)
    {
      if (args == null)
        return;
      StorageFile file = args.File;
      if (file == null)
        return;
      CachedFileManager.DeferUpdates((IStorageFile) file);
      Stream stream = await ((IStorageFile) this._temp).OpenStreamForReadAsync();
      byte[] buffer = new byte[stream.Length];
      int num1 = await stream.ReadAsync(buffer, 0, (int) stream.Length);
      await FileIO.WriteBytesAsync((IStorageFile) file, buffer);
      this._temp = (StorageFile) null;
      FileUpdateStatus fileUpdateStatus = await CachedFileManager.CompleteUpdatesAsync((IStorageFile) file);
      int num2 = await Launcher.LaunchFileAsync((IStorageFile) file) ? 1 : 0;
      Application.Current.Terminate();
    }

    private void showTaskBarText(string text) => SystemTray.SetProgressIndicator((DependencyObject) this, new ProgressIndicator()
    {
      Text = text + "...",
      IsVisible = true,
      IsIndeterminate = true,
      Value = 0.0
    });

    private void hideTaskBarText() => SystemTray.ProgressIndicator.IsVisible = false;

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/pkPassPageEntry.xaml", UriKind.Relative));
      this.phoneApplicationPage = (PhoneApplicationPage) ((FrameworkElement) this).FindName("phoneApplicationPage");
      this.LayoutRoot = (Grid) ((FrameworkElement) this).FindName("LayoutRoot");
      this.passPageBorder = (Border) ((FrameworkElement) this).FindName("passPageBorder");
      this.passPage = (Grid) ((FrameworkElement) this).FindName("passPage");
    }
  }
}
