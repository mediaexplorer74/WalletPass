// Nokia.Phone.HereLaunchers.GuidanceDriveTask

using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class GuidanceDriveTask : TaskBase
  {
    public GeoCoordinate Destination { get; set; }

    public string Title { get; set; }

    public void Show()
    {
      if (!(this.Destination != (GeoCoordinate) null))
        throw new InvalidOperationException(
            "Please set an destination coordinates before calling Show()");

      string uriString = "guidance-drive://v2.0/navigate/destination/?latlon="
                + this.Destination.Latitude.ToString(
                    (IFormatProvider) CultureInfo.InvariantCulture) +
                    "," + this.Destination.Longitude.ToString(
                        (IFormatProvider) CultureInfo.InvariantCulture);
      
            if (!string.IsNullOrEmpty(this.Title))
      {
        string str = Uri.EscapeDataString(this.Title);
        uriString = uriString + "&title=" + str;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
