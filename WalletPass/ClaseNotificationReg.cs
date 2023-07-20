// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseNotificationReg
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Info;
using System;
using System.Net.NetworkInformation;
using Windows.ApplicationModel;
using Windows.Networking.PushNotifications;

namespace WalletPass
{
  public class ClaseNotificationReg
  {
    public string notificationHubName { get; set; }

    public string connectionString { get; set; }

    public string notificationURL { get; set; }

    public string registrationID { get; set; }

    public string userAgent { get; set; }

    public ClaseNotificationReg()
    {
      string registrationID = IO.LoadDataNotificationReg();
      if (!string.IsNullOrEmpty(registrationID))
      {
        this.Initialize(registrationID);
      }
      else
      {
        if (App.IsTrial || !NetworkInterface.GetIsNetworkAvailable())
          return;
        this.Initialize("");
        this.InitNotificationsAsync();
      }
    }

    public ClaseNotificationReg(string registrationID) => this.Initialize(registrationID);

    private void Initialize(string registrationID)
    {
      string deviceManufacturer = DeviceStatus.DeviceManufacturer;
      string deviceName = DeviceStatus.DeviceName;
      string str1 = Environment.OSVersion.ToString();
      PackageVersion version = Package.Current.Id.Version;
      string str2 = string.Format("{0}.{1}.{2}.{3}", (object) version.Major, (object) version.Minor, (object) version.Build, (object) version.Revision);
      if (Package.Current.Id.Name == "38890sSamedi.WalletPass")
      {
        this.notificationHubName = "walletpasspushupdate";
        this.connectionString = "Endpoint=sb://walletpasspushupdate-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=X/GaJgIOfL8sZWK1z6q9t/i3zBP8s1Og89rOs9WBIrw=";
        this.notificationURL = "https://walletpass.azure-mobile.net/";
        this.userAgent = string.Format("{0}/{1} ({2}; {3}; {4})", (object) "walletpass", (object) str2, (object) str1, (object) deviceManufacturer, (object) deviceName);
      }
      else
      {
        this.notificationHubName = "walletpasspushupdateb";
        this.connectionString = "Endpoint=sb://walletpasspushupdate-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=hQhquhUzTu4IoRRx4qWd2eXL/86KUSnmhy0RgUM4ECg=";
        this.notificationURL = "https://walletpassbeta.azure-mobile.net/";
        this.userAgent = string.Format("{0}/{1} ({2}; {3}; {4})", (object) "walletpassbeta", (object) str2, (object) str1, (object) deviceManufacturer, (object) deviceName);
      }
      this.registrationID = registrationID;
    }

    private async void InitNotificationsAsync()
    {
      try
      {
        PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
        NotificationHub hub = new NotificationHub(this.notificationHubName, this.connectionString, this.registrationID);
        hub.Register(channel.Uri);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
