// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseGeofence
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;

namespace WalletPass
{
  public class ClaseGeofence
  {
    private const double radius = 500.0;

    public void createGeofences(ClasePassCollection passcollection)
    {
      if (!new AppSettings().locationEnabled)
        return;
      for (int index1 = 0; index1 <= passcollection.Count - 1; ++index1)
      {
        if (passcollection[index1].showNotifications & passcollection[index1].relevantDate.Year == 1)
        {
          for (int index2 = 0; index2 <= passcollection[index1].Locations.Count - 1; ++index2)
            this.createGeofences(new BasicGeoposition()
            {
              Latitude = passcollection[index1].Locations[index2].locLatitude,
              Longitude = passcollection[index1].Locations[index2].locLongitude,
              Altitude = passcollection[index1].Locations[index2].locAltitude
            }, index2.ToString() + "?" + passcollection[index1].serialNumberGUID);
        }
      }
    }

    public void createGeofences(string id, List<ClaseLocations> locations)
    {
      if (!new AppSettings().locationEnabled)
        return;
      for (int index = 0; index < locations.Count; ++index)
        this.createGeofences(new BasicGeoposition()
        {
          Latitude = locations[index].locLatitude,
          Longitude = locations[index].locLongitude,
          Altitude = locations[index].locAltitude
        }, index.ToString() + "?" + id);
    }

    private void createGeofences(BasicGeoposition position, string id)
    {
      Geocircle geocircle = new Geocircle(position, 500.0);
      MonitoredGeofenceStates monitoredGeofenceStates = (MonitoredGeofenceStates) 1;
      TimeSpan timeSpan = TimeSpan.FromSeconds(1.0);
      Geofence geofence = new Geofence(id, (IGeoshape) geocircle, monitoredGeofenceStates, false, timeSpan);
      try
      {
        GeofenceMonitor.Current.Geofences.Add(geofence);
      }
      catch (Exception ex)
      {
        Console.Write(ex.ToString());
      }
    }

    public void removeGeofences(string id)
    {
      IList<Geofence> geofences = GeofenceMonitor.Current.Geofences;
      while (geofences.FirstOrDefault<Geofence>((Func<Geofence, bool>) (g => g.Id.Contains(id))) != null)
        GeofenceMonitor.Current.Geofences.Remove(geofences.FirstOrDefault<Geofence>((Func<Geofence, bool>) (g => g.Id.Contains(id))));
    }

    public void removeGeofences() => GeofenceMonitor.Current.Geofences.Clear();
  }
}
