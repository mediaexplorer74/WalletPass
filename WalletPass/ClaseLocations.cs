// Decompiled with JetBrains decompiler
// Type: WalletPass.ClaseLocations
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

namespace WalletPass
{
  public class ClaseLocations
  {
    public double locLatitude { get; set; }

    public double locLongitude { get; set; }

    public string locText { get; set; }

    public double locAltitude { get; set; }

    public ClaseLocations()
      : this(0.0, 0.0, "", 0.0)
    {
    }

    public ClaseLocations(double loclatitude, double loclongitude)
    {
      this.locLatitude = loclatitude;
      this.locLongitude = loclongitude;
    }

    public ClaseLocations(double loclatitude, double loclongitude, string loctext)
      : this(loclatitude, loclongitude)
    {
      this.locText = loctext;
    }

    public ClaseLocations(
      double loclatitude,
      double loclongitude,
      string loctext,
      double localtitude)
      : this(loclatitude, loclongitude, loctext)
    {
      this.locAltitude = localtitude;
    }
  }
}
