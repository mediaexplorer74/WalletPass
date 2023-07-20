// App

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace WalletPass
{
    sealed partial class App : Application
    {

        private static LicenseInformation _licenseInfo = default;//new LicenseInformation();
        public static ClasePassCollection _passcollection;
        public static ClasePassCollection _passcollectionArchived;
        public static ClasePass _tempPassClass;
        public static ClasePassBackgroundTaskCollection _updatePassCollection;
        public static ClaseGroup<ClasePass> _tempPassGroup = new ClaseGroup<ClasePass>();
        public static ClasePassMSWalletBackgroundTaskCollection _mswalletPassCollection;
        public static ClasePassMSWalletBackgroundTaskCollection _mswalletDeletePassCollection;
        public static bool _settings;
        public static bool _updates;
        public static bool _archived;
        public static bool _pkPass;
        public static bool _pkPassGroup;
        public static bool _infoPage;
        public static bool _rightGesture;
        public static int _groupItemIndex = -1;
        public static bool _pageEntry = false;
        public static bool _pageArchive = false;
        public static string _colorPage = "#FF000000";
        public static int _colorPageType = 0;
        public static List<ClasePass> _deletePasses = new List<ClasePass>();
        public static List<ClasePass> _archivePasses = new List<ClasePass>();
        public static AppointmentCalendar _currentAppCalendar;
        public static bool _isTombStoned = false;
        public static bool _reconstructPages = false;
        private static bool _IsTrial = true;
        private bool phoneApplicationInitialized;
        //private bool _contentLoaded;

        public static Frame RootFrame { get; private set; }

        public static bool IsTrial => App._IsTrial;

        private void CheckLicense()
        {
            App._IsTrial = false;//App._licenseInfo.IsTrial();
        }

        public AppointmentsProviderShowAppointmentDetailsActivatedEventArgs 
            ShowApptDetailsEventArgs { get; set; }

        public FileSavePickerContinuationEventArgs 
            FilePickerContinuationArgs { get; set; }

        private async void InitNotificationsAsync()
        {
            try
            {
                ClaseNotificationReg notificationReg = new ClaseNotificationReg(
                    IO.LoadDataNotificationReg());
                PushNotificationChannel channel = 
                    await PushNotificationChannelManager
                    .CreatePushNotificationChannelForApplicationAsync();
                NotificationHub hub = new NotificationHub(notificationReg.notificationHubName, 
                    notificationReg.connectionString, notificationReg.registrationID);
                hub.Register(channel.Uri);
            }
            catch (Exception ex)
            {
            }
        }


        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            //   EXPERIMENTAL
            //this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
            
            //this.InitializePhoneApplication();
            //this.InitializeLanguage();
            MapsSettings.ApplicationContext.ApplicationId = "61e56c4f-08e0-4427-af8d-7253603353cb";
            MapsSettings.ApplicationContext.AuthenticationToken = "ljFlW3fy45ZWIIzE9FOPEg";
            
            //if (!Debugger.IsAttached)
            //    return;
            
            //Application.Current.Host.Settings.EnableFrameRateCounter = true;
            //PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode)1;
        }

      
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

           
            if (rootFrame == null)
            {
               
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //
                }

                
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
               
                Window.Current.Activate();
            }
        }

      
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

     
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
           
            deferral.Complete();
        }
    }
}

/*

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using WalletPass.AutoLaunch;
using WalletPass.Resources;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Appointments;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;
using Windows.Networking.PushNotifications;

namespace WalletPass
{
  public class App : Application
  {
    private static LicenseInformation _licenseInfo = new LicenseInformation();
    public static ClasePassCollection _passcollection;
    public static ClasePassCollection _passcollectionArchived;
    public static ClasePass _tempPassClass;
    public static ClasePassBackgroundTaskCollection _updatePassCollection;
    public static ClaseGroup<ClasePass> _tempPassGroup = new ClaseGroup<ClasePass>();
    public static ClasePassMSWalletBackgroundTaskCollection _mswalletPassCollection;
    public static ClasePassMSWalletBackgroundTaskCollection _mswalletDeletePassCollection;
    public static bool _settings;
    public static bool _updates;
    public static bool _archived;
    public static bool _pkPass;
    public static bool _pkPassGroup;
    public static bool _infoPage;
    public static bool _rightGesture;
    public static int _groupItemIndex = -1;
    public static bool _pageEntry = false;
    public static bool _pageArchive = false;
    public static string _colorPage = "#FF000000";
    public static int _colorPageType = 0;
    public static List<ClasePass> _deletePasses = new List<ClasePass>();
    public static List<ClasePass> _archivePasses = new List<ClasePass>();
    public static AppointmentCalendar _currentAppCalendar;
    public static bool _isTombStoned = false;
    public static bool _reconstructPages = false;
    private static bool _IsTrial = true;
    private bool phoneApplicationInitialized;
    private bool _contentLoaded;

    public static PhoneApplicationFrame RootFrame { get; private set; }

    public static bool IsTrial => App._IsTrial;

    private void CheckLicense() => App._IsTrial = App._licenseInfo.IsTrial();

    public AppointmentsProviderShowAppointmentDetailsActivatedEventArgs ShowApptDetailsEventArgs { get; set; }

    public FileSavePickerContinuationEventArgs FilePickerContinuationArgs { get; set; }

    private async void InitNotificationsAsync()
    {
      try
      {
        ClaseNotificationReg notificationReg = new ClaseNotificationReg(IO.LoadDataNotificationReg());
        PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
        NotificationHub hub = new NotificationHub(notificationReg.notificationHubName, notificationReg.connectionString, notificationReg.registrationID);
        hub.Register(channel.Uri);
      }
      catch (Exception ex)
      {
      }
    }

    public App()
    {
      this.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
      this.InitializeComponent();
      this.InitializePhoneApplication();
      this.InitializeLanguage();
      MapsSettings.ApplicationContext.ApplicationId = "61e56c4f-08e0-4427-af8d-7253603353cb";
      MapsSettings.ApplicationContext.AuthenticationToken = "ljFlW3fy45ZWIIzE9FOPEg";
      if (!Debugger.IsAttached)
        return;
      Application.Current.Host.Settings.EnableFrameRateCounter = true;
      PhoneApplicationService.Current.UserIdleDetectionMode = (IdleDetectionMode) 1;
    }

    private void Application_ContractActivated(object sender, IActivatedEventArgs e)
    {
      if (!(e is FileSavePickerContinuationEventArgs continuationEventArgs))
        return;
      this.FilePickerContinuationArgs = continuationEventArgs;
    }

    private void Application_Launching(object sender, LaunchingEventArgs e)
    {
      this.CheckLicense();
      if (!App._IsTrial && NetworkInterface.GetIsNetworkAvailable())
        this.InitNotificationsAsync();
      if (!(e is PhoneAppointmentsProviderLaunchingEventArgs))
        return;
      PhoneAppointmentsProviderLaunchingEventArgs launchingEventArgs = e as PhoneAppointmentsProviderLaunchingEventArgs;
      if (launchingEventArgs.AppointmentsProviderActivatedEventArgs.Verb == AppointmentsProviderLaunchActionVerbs.ShowAppointmentDetails)
        this.ShowApptDetailsEventArgs = launchingEventArgs.AppointmentsProviderActivatedEventArgs as AppointmentsProviderShowAppointmentDetailsActivatedEventArgs;
      else
        this.Terminate();
    }

    private async void Application_Activated(object sender, ActivatedEventArgs e)
    {
      this.CheckLicense();
      if (e.IsApplicationInstancePreserved)
        return;
      App._isTombStoned = true;
      App._passcollection = new ClasePassCollection();
      App._passcollection.AddAllNew(IO.LoadDataPasses());
      App._passcollection.getFrontPasses(true);
      App._passcollectionArchived = new ClasePassCollection();
      App._passcollectionArchived.AddAllNew(await IO.LoadDataPassesArchived());
    }

    private void Application_Deactivated(object sender, DeactivatedEventArgs e)
    {
    }

    private void Application_Closing(object sender, ClosingEventArgs e)
    {
    }

    private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.Break();
    }

    private void Application_UnhandledException(
      object sender,
      ApplicationUnhandledExceptionEventArgs e)
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.Break();
    }

    private void InitializePhoneApplication()
    {
      if (this.phoneApplicationInitialized)
        return;
      App.RootFrame = new PhoneApplicationFrame();
      ((Frame) App.RootFrame).Navigated += new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
      ((Control) App.RootFrame).Background = (Brush) new StringToColorConverter().Convert((object) new AppSettings().themeColorMain, (Type) null, (object) null, (CultureInfo) null);
      ((Frame) App.RootFrame).UriMapper = (UriMapperBase) new AssociationUriMapper();
      ((Frame) App.RootFrame).NavigationFailed += new NavigationFailedEventHandler(this.RootFrame_NavigationFailed);
      ((Frame) App.RootFrame).Navigated += new NavigatedEventHandler(this.CheckForResetNavigation);
      PhoneApplicationService.Current.ContractActivated += new EventHandler<IActivatedEventArgs>(this.Application_ContractActivated);
      this.phoneApplicationInitialized = true;
    }

    private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
    {
      if (this.RootVisual != App.RootFrame)
        this.RootVisual = (UIElement) App.RootFrame;
      ((Frame) App.RootFrame).Navigated -= new NavigatedEventHandler(this.CompleteInitializePhoneApplication);
    }

    private void CheckForResetNavigation(object sender, NavigationEventArgs e)
    {
      if (e.NavigationMode != 4)
        return;
      ((Frame) App.RootFrame).Navigated += new NavigatedEventHandler(this.ClearBackStackAfterReset);
    }

    private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
    {
      ((Frame) App.RootFrame).Navigated -= new NavigatedEventHandler(this.ClearBackStackAfterReset);
      if (e.NavigationMode != null && e.NavigationMode != 3)
        return;
      do
        ;
      while (App.RootFrame.RemoveBackEntry() != null);
    }

    private void InitializeLanguage()
    {
      try
      {
        ((FrameworkElement) App.RootFrame).Language = XmlLanguage.GetLanguage(AppResources.ResourceLanguage);
        ((FrameworkElement) App.RootFrame).FlowDirection = (FlowDirection) Enum.Parse(typeof (FlowDirection), AppResources.ResourceFlowDirection);
      }
      catch
      {
        if (Debugger.IsAttached)
          Debugger.Break();
        throw;
      }
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Wallet%20Pass;component/App.xaml", UriKind.Relative));
    }
  }
}
*/

