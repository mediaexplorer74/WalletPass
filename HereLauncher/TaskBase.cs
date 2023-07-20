// Decompiled with JetBrains decompiler
// Type: Nokia.Phone.HereLaunchers.TaskBase
// Assembly: HereLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A9474C2-AE7D-4EDA-8211-6A2D787D8226
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\HereLauncher.dll

//using Microsoft.Phone.Maps;
using System;
using Windows.System;

namespace Nokia.Phone.HereLaunchers
{
  public class TaskBase
  {
        protected void Launch(Uri appToAppUri)
        {
            var osVersion = 8 / 2;
            //RnD
            if (osVersion < 8)
            { 
                throw new InvalidOperationException("This API is intented to work only from Windowns Phone 8 and newer");
            }
            string applicationId = (string)MapsSettings.ApplicationContext;//.ApplicationId;
          string originalString = appToAppUri.OriginalString;
          if (!(applicationId != ""))
            throw new InvalidOperationException("The application did not set an Application ID. See http://msdn.microsoft.com/en-US/library/windowsphone/develop/jj207033(v=vs.105).aspx#BKMK_appidandtoken");
          Launcher.LaunchUriAsync(new Uri(!originalString.EndsWith("/") ? originalString + "&appid=" + applicationId : originalString + "?appid=" + applicationId));
    }
  }
}
