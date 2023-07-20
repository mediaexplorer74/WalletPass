// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClaseLocations
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

namespace Wallet_Pass
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
