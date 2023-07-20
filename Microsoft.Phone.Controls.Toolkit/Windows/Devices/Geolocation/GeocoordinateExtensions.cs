// Decompiled with JetBrains decompiler
// Type: Windows.Devices.Geolocation.GeocoordinateExtensions
// Assembly: Microsoft.Phone.Controls.Toolkit, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b772ad94eb9ca604
// MVID: BD71C4CB-0489-4E2A-8436-9568C3D40C61
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.dll
// XML documentation location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.Phone.Controls.Toolkit.xml

using System.Device.Location;
using System.Diagnostics.CodeAnalysis;
using System.Security;

namespace Windows.Devices.Geolocation
{
  /// <summary>Represents a class that extends Geocoordinate.</summary>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Geocoordinate", Justification = "Geocoordinate is a valid word in the Windows.Devices.Geolocation namespace")]
  public static class GeocoordinateExtensions
  {
    /// <summary>
    /// Creates a <see cref="T:System.Device.Location.GeoCoordinate" /> from a <see cref="T:Windows.Devices.Geolocation.Geocoordinate" />.
    /// </summary>
    /// <param name="geocoordinate">A <see cref="T:Windows.Devices.Geolocation.Geocoordinate" /> to create a <see cref="T:System.Device.Location.GeoCoordinate" /> from.</param>
    /// <returns>Returns <see cref="T:System.Device.Location.GeoCoordinate" /></returns>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "geocoordinate", Justification = "Geocoordinate is a valid word in the Windows.Devices.Geolocation namespace")]
    [SecuritySafeCritical]
    public static GeoCoordinate ToGeoCoordinate(this Geocoordinate geocoordinate)
    {
      if (geocoordinate == null)
        return (GeoCoordinate) null;
      GeoCoordinate geoCoordinate1 = new GeoCoordinate();
      GeoCoordinate geoCoordinate2 = geoCoordinate1;
      double? nullable = geocoordinate.Altitude;
      double num1 = nullable ?? double.NaN;
      geoCoordinate2.Altitude = num1;
      GeoCoordinate geoCoordinate3 = geoCoordinate1;
      nullable = geocoordinate.Heading;
      double num2 = nullable ?? double.NaN;
      geoCoordinate3.Course = num2;
      geoCoordinate1.HorizontalAccuracy = geocoordinate.Accuracy;
      geoCoordinate1.Latitude = geocoordinate.Latitude;
      geoCoordinate1.Longitude = geocoordinate.Longitude;
      GeoCoordinate geoCoordinate4 = geoCoordinate1;
      nullable = geocoordinate.Speed;
      double num3 = nullable ?? double.NaN;
      geoCoordinate4.Speed = num3;
      GeoCoordinate geoCoordinate5 = geoCoordinate1;
      nullable = geocoordinate.AltitudeAccuracy;
      double num4 = nullable ?? double.NaN;
      geoCoordinate5.VerticalAccuracy = num4;
      return geoCoordinate1;
    }
  }
}
