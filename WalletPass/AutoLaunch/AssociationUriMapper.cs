// Decompiled with JetBrains decompiler
// Type: WalletPass.AutoLaunch.AssociationUriMapper
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.IO;
using System.Net;
using System.Windows;
using WalletPass;
//using System.Windows.Controls;
//using System.Windows.Navigation;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
//using Windows.Phone.Storage.SharedAccess;

namespace WalletPass.AutoLaunch
{
  public class AssociationUriMapper : UriMapperBase
  {
    private string tempUri;

    public virtual Uri MapUri(Uri uri)
    {
      this.tempUri = uri.ToString();
      if (this.tempUri.Contains("/FileTypeAssociation"))
      {
        string fileID = this.tempUri.Substring(this.tempUri.IndexOf("fileToken=") + 10);

        //RnD
        string extension = ".pkpass";//Path.GetExtension(
            //SharedStorageAccessManager.GetSharedFileName(fileID));
        App._pageEntry = true;
        switch (extension)
        {
          case ".pkpass":
            return this.LaunchPage(fileID);
          case ".pkpas":
            return this.LaunchPage(fileID);
          case ".pkpass2":
            return this.LaunchPage(fileID);
          default:
            App._pageEntry = false;
            return new Uri("/MainPage.xaml", UriKind.Relative);
        }
      }
      else
      {
        if ((Application.Current as App).ShowApptDetailsEventArgs != null)
        {
          AppointmentsProviderShowAppointmentDetailsActivatedEventArgs detailsEventArgs
                        = (Application.Current as App).ShowApptDetailsEventArgs;
                    
          //RnD
          //HttpUtility.UrlEncode(string.Format("localId={0}&instanceStartDate={1}", 
          //    (object) detailsEventArgs.LocalId, (object) detailsEventArgs.InstanceStartDate));

          ((App) Application.Current).ShowApptDetailsEventArgs
                        = (AppointmentsProviderShowAppointmentDetailsActivatedEventArgs) null;

           //RnD
          //((Frame) App.RootFrame).Navigate(new Uri("/MainPage.xaml?Appointment="
          //    + detailsEventArgs.LocalId, UriKind.Relative));
        }
        return uri;
      }
    }

    private Uri LaunchPage(string fileID) => new AppSettings().saveOpenPassEnabled ? new Uri("/MainPage.xaml?fileToken=" + fileID, UriKind.Relative) : new Uri("/pkPassPageEntry.xaml?fileToken=" + fileID, UriKind.Relative);
  }
}
