// Nokia.Phone.HereLaunchers.PublicTransitRouteDestinationTask

using System;
//using System.Device.Location;
using System.Globalization;

namespace Nokia.Phone.HereLaunchers
{
  public sealed class PublicTransitRouteDestinationTask : TaskBase
  {
    public GeoCoordinate Destination { get; set; }

    public string DestinationTitle { get; set; }

    public GeoCoordinate Origin { get; set; }

    public string OriginTitle { get; set; }

    public DateTime ArrivalTime { get; set; }

    public DateTime DepartureTime { get; set; }

    public PublicTransitRouteDestinationTask()
    {
      this.ArrivalTime = new DateTime(0L);
      this.DepartureTime = new DateTime(0L);
    }

    public void Show()
    {
      if (!(this.Destination != (GeoCoordinate) null))
        throw new InvalidOperationException("Please set the Destination before calling Show()");
      string uriString = "public-transit://v2.0/route/destination/?latlon=" + this.Destination.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture) + "," + this.Destination.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (!string.IsNullOrEmpty(this.DestinationTitle))
      {
        string str = Uri.EscapeDataString(this.DestinationTitle);
        uriString = uriString + "&title=" + str;
      }
      if (this.Origin != (GeoCoordinate) null)
      {
        string str1 = this.Origin.Latitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        string str2 = this.Origin.Longitude.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        uriString = uriString + "&origin.latlon=" + str1 + "," + str2;
        if (!string.IsNullOrEmpty(this.OriginTitle))
        {
          string str3 = Uri.EscapeDataString(this.OriginTitle);
          uriString = uriString + "&origin.title=" + str3;
        }
      }
      if (this.DepartureTime.CompareTo(new DateTime(0L)) != 0)
        uriString = uriString + "&departureTime=" + this.DepartureTime.ToString("s");
      if (this.ArrivalTime.CompareTo(new DateTime(0L)) != 0)
        uriString = uriString + "&arrivalTime=" + this.ArrivalTime.ToString("s");
      this.Launch(new Uri(uriString));
    }
  }
}
