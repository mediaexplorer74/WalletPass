// Nokia.Phone.HereLaunchers.ExploremapsShowMapTask


//using Microsoft.Phone.Maps.Controls;
using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class ExploremapsShowMapTask : TaskBase
  {
    public LocationRectangle ViewPort { get; set; }

    public GeoCoordinate Location { get; set; }

    public double Zoom { get; set; }

    public void Show()
    {
      if (this.Location != (GeoCoordinate) null)
      {
        string uriString = "explore-maps://v2.0/show/map/?latlon=" 
                    + this.Location.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Location.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        if (this.Zoom > 1.0 && this.Zoom < 21.0)
        {
          string str = this.Zoom.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          uriString = uriString + "&zoom=" + str;
        }
        this.Launch(new Uri(uriString));
      }
      else
      {
        if (this.ViewPort == null)
          throw new InvalidOperationException("Please set an location/viewport coordinates before calling Show()");
       
        //RnD
            //this.Launch(new Uri("explore-maps://v2.0/show/map/?viewport=" + 
            //this.ViewPort.Northeast.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.ViewPort.Northeast.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.ViewPort.Southwest.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.ViewPort.Southwest.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture)));
      }
    }
  }
}
