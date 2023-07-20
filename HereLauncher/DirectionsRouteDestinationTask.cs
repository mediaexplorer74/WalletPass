// Nokia.Phone.HereLaunchers.DirectionsRouteDestinationTask


using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class DirectionsRouteDestinationTask : TaskBase
  {
    public GeoCoordinate Destination { get; set; }

    public GeoCoordinate Origin { get; set; }

    public RouteMode Mode { get; set; }

    public DirectionsRouteDestinationTask() => this.Mode = RouteMode.Smart;

    public void Show()
    {
      if (!(this.Destination != (GeoCoordinate) null))
        throw new InvalidOperationException("Please set coordinate for the Destination before calling Show()");
      string uriString = "directions://v2.0/route/destination/?latlon=" + this.Destination.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Destination.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (this.Origin != (GeoCoordinate) null)
      {
        string str1 = this.Origin.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        string str2 = this.Origin.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        uriString = uriString + "&origin.latlon=" + str1 + "," + str2;
      }
      if (this.Mode != RouteMode.Unknown)
      {
        switch (this.Mode)
        {
          case RouteMode.Pedestrian:
            uriString += "&mode=pedestrian";
            break;
          case RouteMode.Car:
            uriString += "&mode=car";
            break;
          case RouteMode.PublicTransport:
            uriString += "&mode=publicTransport";
            break;
        }
      }
      this.Launch(new Uri(uriString));
    }
  }
}
