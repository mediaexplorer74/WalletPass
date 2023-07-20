// Nokia.Phone.HereLaunchers.ExploremapsSearchPlacesTask


using System;
// using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class ExploremapsSearchPlacesTask : TaskBase
  {
    public GeoCoordinate Location { get; set; }

    public string SearchTerm { get; set; }

    public void Show()
    {
      if (string.IsNullOrEmpty(this.SearchTerm))
        throw new InvalidOperationException("Please set the search term before calling Show()");
      string uriString = "explore-maps://v2.0/search/places/?term=" + Uri.EscapeDataString(this.SearchTerm);
      if (this.Location != (GeoCoordinate) null)
      {
        string str1 = this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        string str2 = this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        uriString = uriString + "&latlon=" + str1 + "," + str2;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
