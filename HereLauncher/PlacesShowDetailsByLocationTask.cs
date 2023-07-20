// // Nokia.Phone.HereLaunchers.PlacesShowDetailsByLocationTask


using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class PlacesShowDetailsByLocationTask : TaskBase
  {
    public GeoCoordinate Location { get; set; }

    public string Title { get; set; }

    public void Show()
    {
      if (!(this.Location != (GeoCoordinate) null))
        throw new InvalidOperationException("Please set an location coordinates for the place before calling Show()");
      string uriString = "places://v2.0/show/details/?latlon=" + this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (!string.IsNullOrEmpty(this.Title))
      {
        string str = Uri.EscapeDataString(this.Title);
        uriString = uriString + "&title=" + str;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
