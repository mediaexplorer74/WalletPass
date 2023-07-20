// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePass
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Windows.UI;

namespace Wallet_Pass
{
  [DataContract]
  public class ClasePass
  {
    private Color _labelColor;
    private Color _foregroundColor;
    private Color _backgroundColor;

    [DataMember]
    public string type { get; set; }

    [DataMember]
    public byte[] iconB { get; set; }

    [DataMember]
    public byte[] icon2B { get; set; }

    [DataMember]
    public byte[] logoB { get; set; }

    [DataMember]
    public byte[] logo2B { get; set; }

    [DataMember]
    public byte[] thumbB { get; set; }

    [DataMember]
    public byte[] thumb2B { get; set; }

    [DataMember]
    public byte[] stripB { get; set; }

    [DataMember]
    public byte[] strip2B { get; set; }

    [DataMember]
    public byte[] footerB { get; set; }

    [DataMember]
    public byte[] footer2B { get; set; }

    [DataMember]
    public byte[] backgroundB { get; set; }

    [DataMember]
    public byte[] background2B { get; set; }

    [DataMember]
    public string barCodeCode { get; set; }

    [DataMember]
    public string serialNumber { get; set; }

    [DataMember]
    public string serialNumberGUID { get; set; }

    [DataMember]
    public string idAppointment { get; set; }

    [DataMember]
    public bool showNotifications { get; set; }

    [DataMember]
    public bool allowUpdates { get; set; }

    [DataMember]
    public bool isUpdated { get; set; }

    [DataMember]
    public string filenamePass { get; set; }

    [DataMember]
    public string strLabelColor { get; set; }

    [DataMember]
    public string strForegroundColor { get; set; }

    [DataMember]
    public string strBackgroundColor { get; set; }

    [DataMember]
    public Color labelColor
    {
      get => this._labelColor;
      set
      {
        this._labelColor = value;
        this.strLabelColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (string) null);
      }
    }

    [DataMember]
    public Color foregroundColor
    {
      get => this._foregroundColor;
      set
      {
        this._foregroundColor = value;
        this.strForegroundColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (string) null);
      }
    }

    [IgnoreDataMember]
    public Color backgroundColorTop { get; set; }

    [DataMember]
    public Color backgroundColor
    {
      get => this._backgroundColor;
      set
      {
        this._backgroundColor = value;
        this.backgroundColorTop = Color.FromArgb(byte.MaxValue, (byte) Math.Min((int) byte.MaxValue, (int) value.R + 30), (byte) Math.Min((int) byte.MaxValue, (int) value.G + 30), (byte) Math.Min((int) byte.MaxValue, (int) value.B + 30));
        this.strBackgroundColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (string) null);
      }
    }

    [DataMember]
    public DateTime relevantDate { get; set; }

    [DataMember]
    public DateTime expirationDate { get; set; }

    [DataMember]
    public string organizationName { get; set; }

    [DataMember]
    public string description { get; set; }

    [DataMember]
    public string webServiceURL { get; set; }

    [DataMember]
    public string teamIdentifier { get; set; }

    [DataMember]
    public string authenticationToken { get; set; }

    [DataMember]
    public string passTypeIdentifier { get; set; }

    [DataMember]
    public string logoText { get; set; }

    [DataMember]
    public string altText { get; set; }

    [DataMember]
    public string barCodeType { get; set; }

    [DataMember]
    public bool hasStrings { get; set; }

    [DataMember]
    public bool hasStringsImg { get; set; }

    [IgnoreDataMember]
    private int stringsIndex { get; set; }

    [DataMember]
    public List<ClaseStrings> locationStrings { get; set; }

    [DataMember]
    public List<ClaseStringsImg> locationStringsImg { get; set; }

    [DataMember]
    public DateTime dateModified { get; set; }

    [DataMember]
    public bool registered { get; set; }

    [DataMember]
    public string sinceUpdate { get; set; }

    [DataMember]
    public List<ClaseField> HeaderFields { get; set; }

    [DataMember]
    public List<ClaseField> PrimaryFields { get; set; }

    [DataMember]
    public List<ClaseField> SecondaryFields { get; set; }

    [DataMember]
    public List<ClaseField> AuxiliaryFields { get; set; }

    [DataMember]
    public List<ClaseField> BackFields { get; set; }

    [DataMember]
    public List<ClaseLocations> Locations { get; set; }

    [DataMember]
    public string transitType { get; set; }

    public ClasePass()
    {
      this.type = "";
      this.iconB = (byte[]) null;
      this.icon2B = (byte[]) null;
      this.logoB = (byte[]) null;
      this.logo2B = (byte[]) null;
      this.thumbB = (byte[]) null;
      this.thumb2B = (byte[]) null;
      this.stripB = (byte[]) null;
      this.strip2B = (byte[]) null;
      this.footerB = (byte[]) null;
      this.footer2B = (byte[]) null;
      this.backgroundB = (byte[]) null;
      this.background2B = (byte[]) null;
      this.HeaderFields = new List<ClaseField>();
      this.PrimaryFields = new List<ClaseField>();
      this.SecondaryFields = new List<ClaseField>();
      this.AuxiliaryFields = new List<ClaseField>();
      this.BackFields = new List<ClaseField>();
      this.Locations = new List<ClaseLocations>();
      this.locationStrings = new List<ClaseStrings>();
      this.locationStringsImg = new List<ClaseStringsImg>();
      this.serialNumberGUID = "";
      this.showNotifications = true;
      this.allowUpdates = true;
      this.isUpdated = false;
      this.serialNumber = "";
      this.idAppointment = "";
      this.organizationName = "";
      this.description = "";
      this.webServiceURL = "";
      this.passTypeIdentifier = "";
      this.teamIdentifier = "";
      this.authenticationToken = "";
      this.labelColor = Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      this.foregroundColor = Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      this.backgroundColor = Color.FromArgb((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      this.relevantDate = new DateTime(1, 1, 1);
      this.expirationDate = new DateTime(1, 1, 1);
      this.filenamePass = "";
      this.logoText = "";
      this.altText = "";
      this.barCodeType = "";
      this.barCodeCode = "";
      this.hasStrings = false;
      this.hasStringsImg = false;
      this.stringsIndex = -1;
      this.transitType = "";
      this.dateModified = DateTime.Now;
      this.registered = false;
      this.sinceUpdate = "";
    }

    public ClasePass(ClasePass clsPass)
    {
      this.type = clsPass.type;
      this.serialNumberGUID = string.IsNullOrEmpty(clsPass.serialNumberGUID) ? Guid.NewGuid().ToString() : clsPass.serialNumberGUID;
      this.serialNumber = clsPass.serialNumber;
      this.idAppointment = clsPass.idAppointment;
      this.showNotifications = clsPass.showNotifications;
      this.allowUpdates = clsPass.allowUpdates;
      this.isUpdated = clsPass.isUpdated;
      this.organizationName = clsPass.organizationName;
      this.description = clsPass.description;
      this.webServiceURL = clsPass.webServiceURL;
      this.passTypeIdentifier = clsPass.passTypeIdentifier;
      this.teamIdentifier = clsPass.teamIdentifier;
      this.authenticationToken = clsPass.authenticationToken;
      this.relevantDate = clsPass.relevantDate;
      this.expirationDate = clsPass.expirationDate;
      this.logoText = clsPass.logoText;
      this.filenamePass = clsPass.filenamePass;
      this.altText = clsPass.altText;
      this.barCodeType = clsPass.barCodeType;
      this.barCodeCode = clsPass.barCodeCode;
      this.transitType = clsPass.transitType;
      this.hasStrings = clsPass.hasStrings;
      this.hasStringsImg = clsPass.hasStringsImg;
      this.dateModified = clsPass.dateModified;
      this.registered = clsPass.registered;
      this.sinceUpdate = clsPass.sinceUpdate;
      this.stringsIndex = -1;
      if (string.IsNullOrEmpty(clsPass.strLabelColor) || string.IsNullOrEmpty(clsPass.strForegroundColor) || string.IsNullOrEmpty(clsPass.strBackgroundColor))
      {
        this.labelColor = clsPass.labelColor;
        this.foregroundColor = clsPass.foregroundColor;
        this.backgroundColor = clsPass.backgroundColor;
      }
      else
      {
        StringToColorConverter toColorConverter = new StringToColorConverter();
        this.labelColor = (Color) toColorConverter.Convert((object) clsPass.strLabelColor, (Type) null, (object) null, (string) null);
        this.foregroundColor = (Color) toColorConverter.Convert((object) clsPass.strForegroundColor, (Type) null, (object) null, (string) null);
        this.backgroundColor = (Color) toColorConverter.Convert((object) clsPass.strBackgroundColor, (Type) null, (object) null, (string) null);
      }
      this.iconB = clsPass.iconB;
      this.icon2B = clsPass.icon2B;
      this.logoB = clsPass.logoB;
      this.logo2B = clsPass.logo2B;
      this.thumbB = clsPass.thumbB;
      this.thumb2B = clsPass.thumb2B;
      this.stripB = clsPass.stripB;
      this.strip2B = clsPass.strip2B;
      this.footerB = clsPass.footerB;
      this.footer2B = clsPass.footer2B;
      this.backgroundB = clsPass.backgroundB;
      this.background2B = clsPass.background2B;
      this.HeaderFields = new List<ClaseField>();
      this.PrimaryFields = new List<ClaseField>();
      this.SecondaryFields = new List<ClaseField>();
      this.AuxiliaryFields = new List<ClaseField>();
      this.BackFields = new List<ClaseField>();
      this.Locations = new List<ClaseLocations>();
      this.locationStrings = new List<ClaseStrings>();
      this.locationStringsImg = new List<ClaseStringsImg>();
      this.HeaderFields = clsPass.HeaderFields;
      this.PrimaryFields = clsPass.PrimaryFields;
      this.SecondaryFields = clsPass.SecondaryFields;
      this.AuxiliaryFields = clsPass.AuxiliaryFields;
      this.BackFields = clsPass.BackFields;
      this.Locations = clsPass.Locations;
      this.locationStrings = clsPass.locationStrings;
      this.locationStringsImg = clsPass.locationStringsImg;
    }
  }
}
