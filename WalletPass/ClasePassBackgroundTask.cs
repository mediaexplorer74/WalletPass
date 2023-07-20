// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassBackgroundTask
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WalletPass
{
  [DataContract]
  public class ClasePassBackgroundTask
  {
    [DataMember]
    public string type { get; set; }

    [DataMember]
    public string serialNumberGUID { get; set; }

    [DataMember]
    public string serialNumber { get; set; }

    [DataMember]
    public bool showNotifications { get; set; }

    [DataMember]
    public bool allowUpdates { get; set; }

    [DataMember]
    public bool isUpdated { get; set; }

    [DataMember]
    public DateTime relevantDate { get; set; }

    [DataMember]
    public DateTime expirationDate { get; set; }

    [DataMember]
    public string organizationName { get; set; }

    [DataMember]
    public string transitType { get; set; }

    [DataMember]
    public string webServiceURL { get; set; }

    [DataMember]
    public string passTypeIdentifier { get; set; }

    [DataMember]
    public string authenticationToken { get; set; }

    [DataMember]
    public bool toastDisplayed { get; set; }

    [DataMember]
    public bool firstDisplayTime { get; set; }

    [DataMember]
    public List<ClaseField> PrimaryFields { get; set; }

    [DataMember]
    public DateTime dateModified { get; set; }

    [DataMember]
    public string sinceUpdate { get; set; }

    [DataMember]
    public string idAppointment { get; set; }

    public ClasePassBackgroundTask()
    {
      this.PrimaryFields = new List<ClaseField>();
      this.type = "";
      this.serialNumberGUID = "";
      this.serialNumber = "";
      this.showNotifications = true;
      this.allowUpdates = true;
      this.isUpdated = false;
      this.organizationName = "";
      this.relevantDate = new DateTime(1, 1, 1);
      this.expirationDate = new DateTime(1, 1, 1);
      this.transitType = "";
      this.webServiceURL = "";
      this.passTypeIdentifier = "";
      this.authenticationToken = "";
      this.toastDisplayed = false;
      this.firstDisplayTime = true;
      this.dateModified = DateTime.Now;
      this.sinceUpdate = "";
      this.idAppointment = "";
    }
  }
}
