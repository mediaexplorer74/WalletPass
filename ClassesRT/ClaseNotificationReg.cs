// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseNotificationReg
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using Windows.ApplicationModel;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace Wallet_Pass
{
  public class ClaseNotificationReg
  {
    public string notificationHubName { get; set; }

    public string connectionString { get; set; }

    public string notificationURL { get; set; }

    public string registrationID { get; set; }

    public string userAgent { get; set; }

    public ClaseNotificationReg()
      : this(IO.LoadDataNotificationReg())
    {
    }

    public ClaseNotificationReg(string registrationID)
    {
      EasClientDeviceInformation deviceInformation = new EasClientDeviceInformation();
      string systemManufacturer1 = deviceInformation.SystemManufacturer;
      string systemManufacturer2 = deviceInformation.SystemManufacturer;
      string systemProductName = deviceInformation.SystemProductName;
      string operatingSystem = deviceInformation.OperatingSystem;
      PackageVersion version = Package.Current.Id.Version;
      string str = string.Format("{0}.{1}.{2}.{3}", (object) version.Major, (object) version.Minor, (object) version.Build, (object) version.Revision);
      if (Package.Current.Id.Name == "38890sSamedi.WalletPass")
      {
        this.notificationHubName = "walletpasspushupdate";
        this.connectionString = "Endpoint=sb://walletpasspushupdate-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=X/GaJgIOfL8sZWK1z6q9t/i3zBP8s1Og89rOs9WBIrw=";
        this.notificationURL = "https://walletpass.azure-mobile.net/";
        this.userAgent = string.Format("{0}/{1} ({2}; {3}; {4})", (object) "walletpass", (object) str, (object) operatingSystem, (object) systemManufacturer2, (object) systemProductName);
      }
      else
      {
        this.notificationHubName = "walletpasspushupdateb";
        this.connectionString = "Endpoint=sb://walletpasspushupdate-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=hQhquhUzTu4IoRRx4qWd2eXL/86KUSnmhy0RgUM4ECg=";
        this.notificationURL = "https://walletpassbeta.azure-mobile.net/";
        this.userAgent = string.Format("{0}/{1} ({2}; {3}; {4})", (object) "walletpassbeta", (object) str, (object) operatingSystem, (object) systemManufacturer2, (object) systemProductName);
      }
      this.registrationID = registrationID;
    }
  }
}
