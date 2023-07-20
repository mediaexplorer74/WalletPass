// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.Pass
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System.Collections.Generic;

namespace Wallet_Pass
{
  public class Pass
  {
    public string description { get; set; }

    public string organizationName { get; set; }

    public string passTypeIdentifier { get; set; }

    public string serialNumber { get; set; }

    public string teamIdentifier { get; set; }

    public Barcode barcode { get; set; }

    public string backgroundColor { get; set; }

    public string foregroundColor { get; set; }

    public string labelColor { get; set; }

    public string logoText { get; set; }

    public string webServiceURL { get; set; }

    public string authenticationToken { get; set; }

    public List<Location> locations { get; set; }

    public string relevantDate { get; set; }

    public string expirationDate { get; set; }

    public int formatVersion { get; set; }

    public passType boardingPass { get; set; }

    public passType eventTicket { get; set; }

    public passType coupon { get; set; }

    public passType storeCard { get; set; }

    public passType generic { get; set; }
  }
}
