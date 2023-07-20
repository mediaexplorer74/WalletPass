// Nokia.Phone.HereLaunchers.ExploremapsExplorePlacesTask


using System;
using System.Collections.Generic;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class ExploremapsExplorePlacesTask : TaskBase
  {
    public GeoCoordinate Location { get; set; }

    public List<string> Category { get; set; }

    public ExploremapsExplorePlacesTask() => this.Category = new List<string>();

    public void Show()
    {
      string uriString = "explore-maps://v2.0/explore/places/";
      if (this.Category != null && this.Category.Count > 0)
      {
        uriString = uriString + "?category=" + this.Category[0];
        if (this.Category.Count > 1)
        {
          for (int index = 1; index < this.Category.Count; ++index)
            uriString = uriString + "," + Uri.EscapeDataString(this.Category[index]);
        }
        if (this.Location != (GeoCoordinate) null)
        {
          string str1 = this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          string str2 = this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          uriString = uriString + "&latlon=" + str1 + "," + str2;
        }
      }
      else if (this.Location != (GeoCoordinate) null)
      {
        string str3 = this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        string str4 = this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        uriString = uriString + "?latlon=" + str3 + "," + str4;
      }
      this.Launch(new Uri(uriString));
    }
  }
}
