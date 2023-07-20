// Type: Nokia.Phone.HereLaunchers.ExploremapsShowPlaceTask

using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class ExploremapsShowPlaceTask : TaskBase
  {
    public GeoCoordinate Location { get; set; }

    public string Title { get; set; }

    public double Zoom { get; set; }

    public void Show()
    {
      if (!(this.Location != (GeoCoordinate) null))
        throw new InvalidOperationException("Please set an location coordinates for the place before calling Show()");
      string uriString = "explore-maps://v2.0/show/place/?latlon=" + this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (this.Zoom > 1.0 && this.Zoom < 21.0)
      {
        string str = this.Zoom.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        uriString = uriString + "&map.zoom=" + str;
      }
      if (!string.IsNullOrEmpty(this.Title))
      {
        string str = Uri.EscapeDataString(this.Title);
        uriString = uriString + "&title=" + str;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
