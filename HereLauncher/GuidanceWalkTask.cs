// Decompiled with JetBrains decompiler
// Type: Nokia.Phone.HereLaunchers.GuidanceWalkTask
// Assembly: HereLauncher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6A9474C2-AE7D-4EDA-8211-6A2D787D8226
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\HereLauncher.dll

using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class GuidanceWalkTask : TaskBase
  {
    public GeoCoordinate Destination { get; set; }

    public string Title { get; set; }

    public void Show()
    {
      if (!(this.Destination != (GeoCoordinate) null))
        throw new InvalidOperationException("Please set an destination coordinates before calling Show()");
      string uriString = "guidance-walk://v2.0/navigate/destination/?latlon=" + this.Destination.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Destination.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (!string.IsNullOrEmpty(this.Title))
      {
        string str = Uri.EscapeDataString(this.Title);
        uriString = uriString + "&title=" + str;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
