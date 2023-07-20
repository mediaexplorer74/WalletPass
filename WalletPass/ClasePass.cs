// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePass
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;
using ZXing.Rendering;

namespace WalletPass
{
  [DataContract]
  public class ClasePass : INotifyPropertyChanged
  {
    private static Regex RE_URL = new Regex("(?#Protocol)(?:(?:ht|f)tp(?:s?)\\:\\/\\/|~/|/)?(?#Username:Password)(?:\\w+:\\w+@)?(?#Subdomains)(?:(?:[-\\w]+\\.)+(?#TopLevel Domains)(?:com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2}))(?#Port)(?::[\\d]{1,5})?(?#Directories)(?:(?:(?:/(?:[-\\w~!$+|.,=]|%[a-f\\d]{2})+)+|/)+|\\?|#)?(?#Query)(?:(?:\\?(?:[-\\w~!$+|.,*:]|%[a-f\\d{2}])+=(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)(?:&(?:[-\\w~!$+|.,*:]|%[a-f\\d{2}])+=(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)*)*(?#Anchor)(?:#(?:[-\\w~!$+|.,*:=]|%[a-f\\d]{2})*)?");
    private static Regex RE_EMAIL = new Regex("^[\\w!#$%&'*+\\-/=?\\^_`{|}~]+(\\.[\\w!#$%&'*+\\-/=?\\^_`{|}~]+)*@((([\\-\\w]+\\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\\.){3}[0-9]{1,3}))$");
    private Color _labelColor;
    private Color _foregroundColor;
    private Color _backgroundColor;
    [IgnoreDataMember]
    public Grid passPageRender;
    [IgnoreDataMember]
    public ListBox passBackPageRender;
    [IgnoreDataMember]
    public ObservableCollection<infoFields> passBackPageFields;
    [IgnoreDataMember]
    private string _transitType;

    public event PropertyChangedEventHandler PropertyChanged;

    [DataMember]
    public string type { get; set; }

    [IgnoreDataMember]
    public BitmapImage icon { get; set; }

    [IgnoreDataMember]
    public BitmapImage icon2 { get; set; }

    [IgnoreDataMember]
    public BitmapImage logo { get; set; }

    [IgnoreDataMember]
    public BitmapImage logo2 { get; set; }

    [IgnoreDataMember]
    public BitmapImage thumb { get; set; }

    [IgnoreDataMember]
    public BitmapImage thumb2 { get; set; }

    [IgnoreDataMember]
    public BitmapImage strip { get; set; }

    [IgnoreDataMember]
    public BitmapImage strip2 { get; set; }

    [IgnoreDataMember]
    public BitmapImage footer { get; set; }

    [IgnoreDataMember]
    public BitmapImage footer2 { get; set; }

    [IgnoreDataMember]
    public WriteableBitmap background { get; set; }

    [IgnoreDataMember]
    public WriteableBitmap background2 { get; set; }

    [IgnoreDataMember]
    public WriteableBitmap barCode { get; set; }

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
        this.strLabelColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (CultureInfo) null);
      }
    }

    [DataMember]
    public Color foregroundColor
    {
      get => this._foregroundColor;
      set
      {
        this._foregroundColor = value;
        this.strForegroundColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (CultureInfo) null);
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
        this.strBackgroundColor = (string) new StringToColorConverter().ConvertBack((object) value, (Type) null, (object) null, (CultureInfo) null);
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
    public string transitType
    {
      get => this._transitType;
      set
      {
        this._transitType = value;
        this.OnPropertyChanged("logoImage");
        this.OnPropertyChanged("logoText");
        this.OnPropertyChanged("iconImage");
        this.OnPropertyChanged("textLabel1");
        this.OnPropertyChanged("textKey1");
        this.OnPropertyChanged("textLabel2");
        this.OnPropertyChanged("textKey2");
        this.OnPropertyChanged("backgroundColorBrush");
        this.OnPropertyChanged("foregroundColorBrush");
        this.OnPropertyChanged("labelColorBrush");
        this.OnPropertyChanged("relevantDay");
        this.OnPropertyChanged("relevantDateOpacity");
        this.OnPropertyChanged("headerTopImage");
        this.OnPropertyChanged("logoWidth");
        this.OnPropertyChanged("backgroundImage");
        this.OnPropertyChanged("imageType");
      }
    }

    public ClasePass()
    {
      this.type = "";
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
      this.icon = (BitmapImage) null;
      this.icon2 = (BitmapImage) null;
      this.logo = (BitmapImage) null;
      this.logo2 = (BitmapImage) null;
      this.thumb = (BitmapImage) null;
      this.thumb2 = (BitmapImage) null;
      this.strip = (BitmapImage) null;
      this.strip2 = (BitmapImage) null;
      this.footer = (BitmapImage) null;
      this.footer2 = (BitmapImage) null;
      this.background = (WriteableBitmap) null;
      this.background2 = (WriteableBitmap) null;
      this.barCode = (WriteableBitmap) null;
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
    }

    public ClasePass(ClasePass clsPass, bool loadedFromPkPass)
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
        SolidColorBrush solidColorBrush = new SolidColorBrush();
        this.labelColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strLabelColor, (Type) null, (object) null, (CultureInfo) null)).Color;
        this.foregroundColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strForegroundColor, (Type) null, (object) null, (CultureInfo) null)).Color;
        this.backgroundColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strBackgroundColor, (Type) null, (object) null, (CultureInfo) null)).Color;
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
      if (loadedFromPkPass)
      {
        this.newLoadImages(true);
        this.icon = clsPass.icon;
        this.icon2 = clsPass.icon2;
        this.thumb = clsPass.thumb;
        this.thumb2 = clsPass.thumb2;
        this.strip = clsPass.strip;
        this.strip2 = clsPass.strip2;
        this.footer = clsPass.footer;
        this.footer2 = clsPass.footer2;
      }
      else
        this.newLoadImages(true);
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
      this.passPageRender = new Grid();
      if (loadedFromPkPass)
        return;
      this.newLoadImages(false);
    }

    public void swapPass(ClasePass clsPass)
    {
      this.type = clsPass.type;
      this.serialNumber = clsPass.serialNumber;
      this.organizationName = clsPass.organizationName;
      this.description = clsPass.description;
      this.webServiceURL = clsPass.webServiceURL;
      this.passTypeIdentifier = clsPass.passTypeIdentifier;
      this.teamIdentifier = clsPass.teamIdentifier;
      this.authenticationToken = clsPass.authenticationToken;
      this.relevantDate = clsPass.relevantDate;
      this.expirationDate = clsPass.expirationDate;
      this.logoText = clsPass.logoText;
      this.altText = clsPass.altText;
      this.barCodeType = clsPass.barCodeType;
      this.barCodeCode = clsPass.barCodeCode;
      this.transitType = clsPass.transitType;
      this.hasStrings = clsPass.hasStrings;
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
        SolidColorBrush solidColorBrush = new SolidColorBrush();
        this.labelColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strLabelColor, (Type) null, (object) null, (CultureInfo) null)).Color;
        this.foregroundColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strForegroundColor, (Type) null, (object) null, (CultureInfo) null)).Color;
        this.backgroundColor = ((SolidColorBrush) toColorConverter.Convert((object) clsPass.strBackgroundColor, (Type) null, (object) null, (CultureInfo) null)).Color;
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
      this.newLoadImages(true);
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

    [IgnoreDataMember]
    public BitmapImage logoImage
    {
      get
      {
        if (this.logo == null && this.logo2 == null)
          return (BitmapImage) null;
        return this.logo2 != null ? this.logo2 : this.logo;
      }
    }

    [IgnoreDataMember]
    public BitmapImage iconImage
    {
      get
      {
        if (this.icon == null && this.icon2 == null)
          return (BitmapImage) null;
        return this.icon2 != null ? this.icon2 : this.icon;
      }
    }

    [IgnoreDataMember]
    public BitmapImage stripImage
    {
      get
      {
        if (this.strip == null && this.strip2 == null)
          return (BitmapImage) null;
        return this.strip2 != null ? this.strip2 : this.strip;
      }
    }

    [IgnoreDataMember]
    public string textLabel1
    {
      get
      {
        if (this.HeaderFields.Count == 1)
          return this.checkLabel(this.HeaderFields[0].Label);
        return this.HeaderFields.Count > 1 ? this.checkLabel(this.HeaderFields[1].Label) : "";
      }
    }

    [IgnoreDataMember]
    public string textKey1
    {
      get
      {
        if (this.HeaderFields.Count == 1)
          return this.HeaderFields[0].Value;
        return this.HeaderFields.Count > 1 ? this.HeaderFields[1].Value : "";
      }
    }

    [IgnoreDataMember]
    public string textLabel2 => this.HeaderFields.Count > 1 ? this.HeaderFields[0].Label : "";

    [IgnoreDataMember]
    public string textKey2 => this.HeaderFields.Count > 1 ? this.HeaderFields[0].Value : "";

    [IgnoreDataMember]
    public BitmapImage headerTopImage => new BitmapImage(new Uri("/Assets/PassHeadersList/" + this.type + ".png", UriKind.Relative));

    [IgnoreDataMember]
    public BitmapImage imageType => new BitmapImage(new Uri("/Assets/GroupType/" + this.type + ".png", UriKind.Relative));

    [IgnoreDataMember]
    public Brush backgroundColorBrush => this.background != null ? (Brush) new SolidColorBrush(Colors.Transparent) : (Brush) new SolidColorBrush(this.backgroundColor);

    [IgnoreDataMember]
    public Brush foregroundColorBrush => (Brush) new SolidColorBrush(this.foregroundColor);

    [IgnoreDataMember]
    public Brush labelColorBrush => (Brush) new SolidColorBrush(this.labelColor);

    [IgnoreDataMember]
    public WriteableBitmap backgroundImage => this.background2 != null ? this.background2 : this.background;

    [IgnoreDataMember]
    public double relevantDateOpacity => new AppSettings().listExpiredTransparentSetting && DateTime.Now > this.relevantDate && this.relevantDate.Year != 1 ? 0.5 : 1.0;

    [IgnoreDataMember]
    public string relevantDay => this.relevantDate.Year == 1 ? "-" : this.relevantDate.ToString("d") + " - " + this.relevantDate.ToString("t");

    [IgnoreDataMember]
    public string relevantDayDay => this.relevantDate.Year == 1 ? "" : this.relevantDate.ToString("d");

    [IgnoreDataMember]
    public string relevantDayHour => this.relevantDate.Year == 1 ? "" : this.relevantDate.ToString("t");

    [IgnoreDataMember]
    public int logoWidth
    {
      get
      {
        if (this.logo != null)
          return (double) ((BitmapSource) this.logo).PixelHeight / (double) ((BitmapSource) this.logo).PixelWidth < 0.9 ? 240 : 80;
        if (this.logo2 == null)
          return 0;
        return (double) ((BitmapSource) this.logo2).PixelHeight / (double) ((BitmapSource) this.logo2).PixelWidth < 0.9 ? 240 : 80;
      }
    }

    [IgnoreDataMember]
    public bool isNotPinned
    {
      get
      {
        Uri uri = new Uri("/MainPage.xaml?SecondaryTile=" + this.serialNumberGUID, UriKind.Relative);
        return ShellTile.ActiveTiles.Where<ShellTile>((Func<ShellTile, bool>) (t => t.NavigationUri == uri)).FirstOrDefault<ShellTile>() == null;
      }
    }

    [IgnoreDataMember]
    public bool hasRelevantDate => true;

    [IgnoreDataMember]
    public bool visibleUpdate => this.isUpdated;

    [IgnoreDataMember]
    public string orgName => this.correctText(this.organizationName);

    [IgnoreDataMember]
    public string primFields
    {
      get
      {
        if (this.PrimaryFields.Count <= 0)
          return "";
        switch (this.type)
        {
          case "boardingPass":
            return this.PrimaryFields.Count > 1 ? this.PrimaryFields[0].Value + "->" + this.PrimaryFields[1].Value : this.PrimaryFields[0].Value;
          default:
            return this.PrimaryFields[0].Value;
        }
      }
    }

    [IgnoreDataMember]
    public bool canShare => !string.IsNullOrEmpty(this.filenamePass);

    public void updateSettings()
    {
      this.OnPropertyChanged("relevantDateOpacity");
      this.OnPropertyChanged("isNotPinned");
      this.OnPropertyChanged("hasRelevantDate");
      this.OnPropertyChanged("relevantDay");
    }

    protected void OnPropertyChanged(string name)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    public void setBarCode(string code, string type, string altText)
    {
      BarcodeWriter barcodeWriter = new BarcodeWriter();
      barcodeWriter.Renderer = (IBarcodeRenderer<WriteableBitmap>) new WriteableBitmapRenderer()
      {
        Foreground = Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 0, (byte) 0)
      };
      switch (type)
      {
        case "PKBarcodeFormatQR":
          barcodeWriter.Format = BarcodeFormat.QR_CODE;
          break;
        case "PKBarcodeFormatPDF417":
          barcodeWriter.Format = BarcodeFormat.PDF_417;
          break;
        case "PKBarcodeFormatAztec":
          barcodeWriter.Format = BarcodeFormat.AZTEC;
          break;
      }
      if (type != "PKBarcodeFormatPDF417")
      {
        barcodeWriter.Options.Width = 400;
        barcodeWriter.Options.Height = 400;
        barcodeWriter.Options.Margin = 1;
      }
      else
      {
        barcodeWriter.Options.Height = 350;
        barcodeWriter.Options.Width = 1280;
      }
      WriteableBitmap image = barcodeWriter.Write(code);
      this.barCode = !(type != "PKBarcodeFormatPDF417") ? this.PDF417MarginCorrection(image) : this.SquareBarcodeMarginCorrection(image);
      this.altText = altText;
      this.barCodeType = type;
      this.barCodeCode = code;
    }

    private WriteableBitmap PDF417MarginCorrection(WriteableBitmap image)
    {
      double num1 = (double) Array.IndexOf<int>(image.Pixels, -16777216);
      double num2 = (double) Array.LastIndexOf<int>(image.Pixels, -16777216);
      int int32_1 = Convert.ToInt32(Math.Truncate(num1 / (double) ((BitmapSource) image).PixelWidth));
      int int32_2 = Convert.ToInt32((double) ((BitmapSource) image).PixelHeight - (Math.Truncate(num2 / (double) ((BitmapSource) image).PixelWidth) + 1.0));
      int int32_3 = Convert.ToInt32(Math.Truncate(num1 - (double) (int32_1 * ((BitmapSource) image).PixelWidth)));
      int int32_4 = Convert.ToInt32(Math.Truncate((double) ((((BitmapSource) image).PixelHeight - int32_2) * ((BitmapSource) image).PixelWidth) - num2) - 5.0);
      WriteableBitmap bmp = new WriteableBitmap(((BitmapSource) image).PixelWidth - (int32_3 + int32_4) - 2, ((BitmapSource) image).PixelHeight - (int32_1 + int32_2) - 2);
      int[] pixels = image.Pixels;
      for (int y = 0; y <= ((BitmapSource) bmp).PixelHeight - 1; ++y)
      {
        int index = ((BitmapSource) image).PixelWidth * (int32_1 - 1 + y) + (int32_3 - 1);
        for (int x = 0; x <= ((BitmapSource) bmp).PixelWidth - 1; ++x)
        {
          if (pixels[index] == -1)
            bmp.SetPixel(x, y, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
          else
            bmp.SetPixel(x, y, byte.MaxValue, (byte) 0, (byte) 0, (byte) 0);
          ++index;
        }
      }
      return bmp;
    }

    private WriteableBitmap SquareBarcodeMarginCorrection(WriteableBitmap image)
    {
      int int32 = Convert.ToInt32(Math.Truncate((double) Array.IndexOf<int>(image.Pixels, -16777216) / (double) ((BitmapSource) image).PixelWidth));
      int num1 = int32 == 0 ? 1 : int32;
      int num2 = num1;
      int num3 = num1;
      int num4 = num3;
      WriteableBitmap bmp = new WriteableBitmap(((BitmapSource) image).PixelWidth - (num3 + num4) + 1, ((BitmapSource) image).PixelHeight - (num1 + num2) + 1);
      int[] pixels = image.Pixels;
      try
      {
        for (int y = 0; y <= ((BitmapSource) bmp).PixelHeight - 1; ++y)
        {
          int index = ((BitmapSource) image).PixelWidth * (num1 - 1 + y) + (num3 - 1);
          for (int x = 0; x <= ((BitmapSource) bmp).PixelWidth - 1; ++x)
          {
            if (pixels[index] == -1)
              bmp.SetPixel(x, y, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
            else
              bmp.SetPixel(x, y, byte.MaxValue, (byte) 0, (byte) 0, (byte) 0);
            ++index;
          }
        }
      }
      catch (Exception ex)
      {
      }
      return bmp;
    }

    public void getFrontPass(bool fromTombStone = false)
    {
      this.passPageRender = new Grid();
      if (fromTombStone)
        this.newLoadImages();
      switch (this.type)
      {
        case "boardingPass":
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPageRender).Children).Add((UIElement) this.makeBoardingPassGrid());
          break;
        case "eventTicket":
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPageRender).Children).Add((UIElement) this.makeEventGrid());
          break;
        case "storeCard":
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPageRender).Children).Add((UIElement) this.makeStoreCardGrid());
          break;
        case "generic":
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPageRender).Children).Add((UIElement) this.makeGenericGrid());
          break;
        case "coupon":
          ((PresentationFrameworkCollection<UIElement>) ((Panel) this.passPageRender).Children).Add((UIElement) this.makeCouponGrid());
          break;
      }
    }

    public void getBackPass()
    {
      this.passBackPageRender = new ListBox();
      ((Control) this.passBackPageRender).Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
      this.createBackPanel();
    }

    private Grid makeBoardingPassGrid()
    {
      Grid grid1 = new Grid();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      RowDefinition rowDefinition3 = new RowDefinition();
      RowDefinition rowDefinition4 = new RowDefinition();
      RowDefinition rowDefinition5 = new RowDefinition();
      RowDefinition rowDefinition6 = new RowDefinition();
      rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition3.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition4.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition5.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition6.Height = new GridLength(1.0, (GridUnitType) 2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition5);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition6);
      Grid grid2 = new Grid();
      Grid headerGrid = this.createHeaderGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) headerGrid);
      Grid.SetRow((FrameworkElement) headerGrid, 0);
      Canvas canvas = new Canvas();
      ((Panel) canvas).Background = (Brush) new SolidColorBrush(Color.FromArgb((byte) 153, (byte) 0, (byte) 0, (byte) 0));
      ((FrameworkElement) canvas).Height = 1.0;
      ((FrameworkElement) canvas).Margin = new Thickness(20.0, 10.0, 20.0, 0.0);
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) canvas);
      Grid.SetRow((FrameworkElement) canvas, 1);
      Grid grid3 = new Grid();
      ((FrameworkElement) grid3).Margin = new Thickness(20.0, 10.0, 20.0, 10.0);
      ColumnDefinition columnDefinition1 = new ColumnDefinition();
      ColumnDefinition columnDefinition2 = new ColumnDefinition();
      ColumnDefinition columnDefinition3 = new ColumnDefinition();
      RowDefinition rowDefinition7 = new RowDefinition();
      RowDefinition rowDefinition8 = new RowDefinition();
      columnDefinition1.Width = new GridLength(1.0, (GridUnitType) 2);
      columnDefinition2.Width = new GridLength(1.0, (GridUnitType) 0);
      columnDefinition3.Width = new GridLength(1.0, (GridUnitType) 2);
      rowDefinition7.Height = new GridLength(40.0);
      rowDefinition8.Height = new GridLength(60.0);
      ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition1);
      ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition2);
      ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition7);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition8);
      Rectangle rectangle = new Rectangle();
      ((FrameworkElement) rectangle).Height = 70.0;
      ((FrameworkElement) rectangle).Width = 70.0;
      ((FrameworkElement) rectangle).Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
      ((Shape) rectangle).Fill = (Brush) new SolidColorBrush(this.labelColor);
      ((UIElement) rectangle).OpacityMask = (Brush) new ImageBrush()
      {
        ImageSource = (ImageSource) new BitmapImage(new Uri("/Assets/" + this.transitType + ".png", UriKind.Relative))
      };
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) rectangle);
      Grid.SetColumn((FrameworkElement) rectangle, 1);
      Grid.SetRowSpan((FrameworkElement) rectangle, 2);
      TextBlock tb1 = new TextBlock();
      TextBlock tb2 = new TextBlock();
      TextBlock tb3 = new TextBlock();
      TextBlock tb4 = new TextBlock();
      ((FrameworkElement) tb1).HorizontalAlignment = (HorizontalAlignment) 0;
      ((FrameworkElement) tb1).VerticalAlignment = (VerticalAlignment) 1;
      tb1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
      tb1.FontWeight = FontWeights.Medium;
      tb1.FontFamily = new FontFamily("Segoe WP Light");
      tb1.Text = this.checkLabel(this.PrimaryFields[0].Label);
      tb3.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
      tb3.FontSize = 66.0;
      tb3.FontFamily = new FontFamily("Segoe WP Light");
      tb3.Text = this.checkLabel(this.PrimaryFields[0].Value);
      ((FrameworkElement) tb3).HorizontalAlignment = (HorizontalAlignment) 0;
      ((FrameworkElement) tb3).VerticalAlignment = (VerticalAlignment) 1;
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) tb3);
      Grid.SetColumn((FrameworkElement) tb3, 0);
      Grid.SetRow((FrameworkElement) tb3, 1);
      if (this.isTextTrimming(tb3, 210.0))
      {
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Remove((UIElement) tb3);
        Viewbox viewbox = new Viewbox();
        viewbox.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox).VerticalAlignment = (VerticalAlignment) 1;
        viewbox.Child = (UIElement) tb3;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox);
        Grid.SetColumn((FrameworkElement) viewbox, 0);
        Grid.SetRow((FrameworkElement) viewbox, 1);
      }
      if (this.PrimaryFields.Count > 1)
      {
        ((FrameworkElement) tb2).HorizontalAlignment = (HorizontalAlignment) 2;
        ((FrameworkElement) tb2).VerticalAlignment = (VerticalAlignment) 1;
        tb2.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        tb2.FontWeight = FontWeights.Medium;
        tb2.FontFamily = new FontFamily("Segoe WP Light");
        tb2.Text = this.checkLabel(this.PrimaryFields[1].Label);
        tb4.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        tb4.FontFamily = new FontFamily("Segoe WP Light");
        tb4.FontSize = 66.0;
        tb4.Text = this.checkLabel(this.PrimaryFields[1].Value);
        ((FrameworkElement) tb4).HorizontalAlignment = (HorizontalAlignment) 2;
        ((FrameworkElement) tb4).VerticalAlignment = (VerticalAlignment) 1;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) tb4);
        Grid.SetColumn((FrameworkElement) tb4, 2);
        Grid.SetRow((FrameworkElement) tb4, 1);
        if (this.isTextTrimming(tb4, 210.0))
        {
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Remove((UIElement) tb4);
          Viewbox viewbox = new Viewbox();
          viewbox.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox).HorizontalAlignment = (HorizontalAlignment) 2;
          ((FrameworkElement) viewbox).VerticalAlignment = (VerticalAlignment) 1;
          viewbox.Child = (UIElement) tb4;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox);
          Grid.SetColumn((FrameworkElement) viewbox, 2);
          Grid.SetRow((FrameworkElement) viewbox, 1);
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) tb2);
        Grid.SetColumn((FrameworkElement) tb2, 2);
        Grid.SetRow((FrameworkElement) tb2, 0);
        tb2.Text = this.textTrimming(tb2, 85.0);
      }
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) tb1);
      Grid.SetColumn((FrameworkElement) tb1, 0);
      Grid.SetRow((FrameworkElement) tb1, 0);
      tb1.Text = this.textTrimming(tb1, 85.0);
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
      Grid.SetRow((FrameworkElement) grid3, 2);
      if (this.AuxiliaryFields.Count > 0)
      {
        Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
        Grid.SetRow((FrameworkElement) secAuxGrid, 3);
      }
      if (this.SecondaryFields.Count > 0)
      {
        Grid secAuxGrid = this.createSecAuxGrid(this.SecondaryFields);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
        Grid.SetRow((FrameworkElement) secAuxGrid, 4);
      }
      Grid barCodeGrid = this.createBarCodeGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) barCodeGrid);
      Grid.SetRow((FrameworkElement) barCodeGrid, 5);
      return grid1;
    }

    private Grid makeEventGrid()
    {
      Grid grid1 = new Grid();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      RowDefinition rowDefinition3 = new RowDefinition();
      RowDefinition rowDefinition4 = new RowDefinition();
      RowDefinition rowDefinition5 = new RowDefinition();
      rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition3.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition4.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition5.Height = new GridLength(1.0, (GridUnitType) 2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition5);
      Grid grid2 = new Grid();
      Grid headerGrid = this.createHeaderGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) headerGrid);
      Grid.SetRow((FrameworkElement) headerGrid, 0);
      Grid grid3 = new Grid();
      ((FrameworkElement) grid3).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
      if (this.strip == null)
      {
        ColumnDefinition columnDefinition1 = new ColumnDefinition();
        ColumnDefinition columnDefinition2 = new ColumnDefinition();
        RowDefinition rowDefinition6 = new RowDefinition();
        RowDefinition rowDefinition7 = new RowDefinition();
        RowDefinition rowDefinition8 = new RowDefinition();
        RowDefinition rowDefinition9 = new RowDefinition();
        columnDefinition1.Width = new GridLength(1.0, (GridUnitType) 2);
        columnDefinition2.Width = new GridLength(140.0);
        rowDefinition6.Height = new GridLength(40.0);
        rowDefinition7.Height = new GridLength(60.0);
        rowDefinition8.Height = new GridLength(40.0);
        rowDefinition9.Height = new GridLength(40.0);
        ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition1);
        ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition2);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition6);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition7);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition8);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition9);
        if (this.thumb != null)
        {
          Image image = new Image();
          image.Stretch = (Stretch) 2;
          ((FrameworkElement) image).VerticalAlignment = (VerticalAlignment) 0;
          ((FrameworkElement) image).HorizontalAlignment = (HorizontalAlignment) 2;
          ((FrameworkElement) image).Height = 200.0;
          ((FrameworkElement) image).Width = 140.0;
          image.Source = this.thumb2 != null ? (ImageSource) this.thumb2 : (ImageSource) this.thumb;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) image);
          Grid.SetColumn((FrameworkElement) image, 1);
          Grid.SetRowSpan((FrameworkElement) image, 4);
        }
        if (this.PrimaryFields.Count > 0)
        {
          TextBlock textBlock1 = new TextBlock();
          TextBlock textBlock2 = new TextBlock();
          Viewbox viewbox1 = new Viewbox();
          Viewbox viewbox2 = new Viewbox();
          viewbox1.StretchDirection = (StretchDirection) 1;
          viewbox2.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 2;
          textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
          textBlock1.FontWeight = FontWeights.Medium;
          textBlock1.FontFamily = new FontFamily("Segoe WP Light");
          textBlock1.Text = this.checkLabel(this.PrimaryFields[0].Label);
          ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 0;
          ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 0;
          textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
          textBlock2.FontFamily = new FontFamily("Segoe WP Light");
          textBlock2.FontSize = 30.0;
          textBlock2.TextWrapping = (TextWrapping) 2;
          textBlock2.Text = this.checkLabel(this.PrimaryFields[0].Value);
          viewbox1.Child = (UIElement) textBlock1;
          viewbox2.Child = (UIElement) textBlock2;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox1);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox2);
          Grid.SetRow((FrameworkElement) viewbox1, 0);
          Grid.SetRow((FrameworkElement) viewbox2, 1);
          if (this.thumb == null)
          {
            Grid.SetColumnSpan((FrameworkElement) viewbox1, 2);
            Grid.SetColumnSpan((FrameworkElement) viewbox2, 2);
          }
          else
          {
            Grid.SetColumn((FrameworkElement) viewbox1, 0);
            Grid.SetColumn((FrameworkElement) viewbox2, 0);
          }
        }
        if (this.SecondaryFields.Count > 0)
        {
          TextBlock textBlock3 = new TextBlock();
          TextBlock textBlock4 = new TextBlock();
          Viewbox viewbox3 = new Viewbox();
          Viewbox viewbox4 = new Viewbox();
          viewbox3.StretchDirection = (StretchDirection) 1;
          viewbox4.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox3).Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
          ((FrameworkElement) viewbox3).VerticalAlignment = (VerticalAlignment) 2;
          ((FrameworkElement) viewbox3).HorizontalAlignment = (HorizontalAlignment) 0;
          textBlock3.Foreground = (Brush) new SolidColorBrush(this.labelColor);
          textBlock3.FontWeight = FontWeights.Medium;
          textBlock3.Text = this.checkLabel(this.SecondaryFields[0].Label);
          textBlock3.FontFamily = new FontFamily("Segoe WP Light");
          ((FrameworkElement) viewbox4).VerticalAlignment = (VerticalAlignment) 1;
          ((FrameworkElement) viewbox4).HorizontalAlignment = (HorizontalAlignment) 0;
          textBlock4.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
          textBlock4.FontFamily = new FontFamily("Segoe WP Light");
          textBlock4.FontSize = 24.0;
          textBlock4.Text = this.checkLabel(this.SecondaryFields[0].Value);
          viewbox3.Child = (UIElement) textBlock3;
          viewbox4.Child = (UIElement) textBlock4;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox3);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox4);
          Grid.SetRow((FrameworkElement) viewbox3, 2);
          Grid.SetRow((FrameworkElement) viewbox4, 3);
          if (this.thumb == null)
          {
            Grid.SetColumnSpan((FrameworkElement) viewbox3, 2);
            Grid.SetColumnSpan((FrameworkElement) viewbox4, 2);
          }
          else
          {
            Grid.SetColumn((FrameworkElement) viewbox3, 0);
            Grid.SetColumn((FrameworkElement) viewbox4, 0);
          }
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
        Grid.SetRow((FrameworkElement) grid3, 1);
        if (this.AuxiliaryFields.Count > 0)
        {
          Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
          Grid.SetRow((FrameworkElement) secAuxGrid, 2);
        }
      }
      else
      {
        ((FrameworkElement) grid3).Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
        RowDefinition rowDefinition10 = new RowDefinition();
        RowDefinition rowDefinition11 = new RowDefinition();
        ColumnDefinition columnDefinition = new ColumnDefinition();
        rowDefinition10.Height = new GridLength(140.0);
        rowDefinition11.Height = new GridLength(70.0);
        columnDefinition.Width = new GridLength(1.0, (GridUnitType) 2);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition10);
        ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition11);
        ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition);
        Image image = new Image();
        ((FrameworkElement) image).VerticalAlignment = (VerticalAlignment) 1;
        if (this.strip2 == null)
        {
          image.Source = (ImageSource) this.strip;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip).PixelWidth, (double) ((BitmapSource) this.strip).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        else
        {
          image.Source = (ImageSource) this.strip2;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip2).PixelWidth, (double) ((BitmapSource) this.strip2).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) image);
        Grid.SetRow((FrameworkElement) image, 0);
        Grid.SetRowSpan((FrameworkElement) image, 2);
        if (this.PrimaryFields.Count > 0)
        {
          TextBlock textBlock5 = new TextBlock();
          TextBlock textBlock6 = new TextBlock();
          Viewbox viewbox5 = new Viewbox();
          Viewbox viewbox6 = new Viewbox();
          viewbox5.StretchDirection = (StretchDirection) 1;
          viewbox6.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox5).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
          ((FrameworkElement) viewbox5).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) viewbox5).VerticalAlignment = (VerticalAlignment) 0;
          textBlock5.Foreground = (Brush) new SolidColorBrush(this.labelColor);
          textBlock5.FontWeight = FontWeights.Medium;
          textBlock5.FontSize = 30.0;
          textBlock5.FontFamily = new FontFamily("Segoe WP Light");
          textBlock5.Text = this.checkLabel(this.PrimaryFields[0].Label);
          ((FrameworkElement) viewbox6).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
          ((FrameworkElement) viewbox6).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) viewbox6).VerticalAlignment = (VerticalAlignment) 1;
          textBlock6.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
          textBlock6.FontSize = 100.0;
          textBlock6.FontFamily = new FontFamily("Segoe WP Light");
          textBlock6.Text = this.checkLabel(this.PrimaryFields[0].Value);
          viewbox5.Child = (UIElement) textBlock5;
          viewbox6.Child = (UIElement) textBlock6;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox6);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox5);
          Grid.SetRow((FrameworkElement) viewbox5, 1);
          Grid.SetRow((FrameworkElement) viewbox6, 0);
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
        Grid.SetRow((FrameworkElement) grid3, 1);
        if (this.SecondaryFields.Count > 0)
        {
          Grid secAuxGrid = this.createSecAuxGrid(this.SecondaryFields);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
          Grid.SetRow((FrameworkElement) secAuxGrid, 2);
        }
        if (this.AuxiliaryFields.Count > 0)
        {
          Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
          Grid.SetRow((FrameworkElement) secAuxGrid, 3);
        }
      }
      Grid barCodeGrid = this.createBarCodeGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) barCodeGrid);
      Grid.SetRow((FrameworkElement) barCodeGrid, 4);
      return grid1;
    }

    private Grid makeStoreCardGrid()
    {
      Grid grid1 = new Grid();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      RowDefinition rowDefinition3 = new RowDefinition();
      RowDefinition rowDefinition4 = new RowDefinition();
      RowDefinition rowDefinition5 = new RowDefinition();
      rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition3.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition4.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition5.Height = new GridLength(1.0, (GridUnitType) 2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition5);
      Grid grid2 = new Grid();
      Grid headerGrid = this.createHeaderGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) headerGrid);
      Grid.SetRow((FrameworkElement) headerGrid, 0);
      Grid grid3 = new Grid();
      RowDefinition rowDefinition6 = new RowDefinition();
      RowDefinition rowDefinition7 = new RowDefinition();
      rowDefinition6.Height = new GridLength(140.0);
      rowDefinition7.Height = new GridLength(70.0);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition6);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition7);
      if (this.strip != null | this.strip2 != null)
      {
        Image image = new Image();
        ((FrameworkElement) image).VerticalAlignment = (VerticalAlignment) 1;
        if (this.strip2 == null)
        {
          image.Source = (ImageSource) this.strip;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip).PixelWidth, (double) ((BitmapSource) this.strip).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        else
        {
          image.Source = (ImageSource) this.strip2;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip2).PixelWidth, (double) ((BitmapSource) this.strip2).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) image);
        Grid.SetRowSpan((FrameworkElement) image, 2);
      }
      if (this.PrimaryFields.Count > 0)
      {
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox1 = new Viewbox();
        Viewbox viewbox2 = new Viewbox();
        viewbox2.StretchDirection = (StretchDirection) 1;
        viewbox1.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox1).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
        ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 0;
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        textBlock1.FontSize = 30.0;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.Text = this.checkLabel(this.PrimaryFields[0].Label);
        ((FrameworkElement) viewbox2).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
        ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 1;
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 100.0;
        textBlock2.Text = this.checkLabel(this.PrimaryFields[0].Value);
        viewbox1.Child = (UIElement) textBlock1;
        viewbox2.Child = (UIElement) textBlock2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox2);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox1);
        Grid.SetRow((FrameworkElement) viewbox1, 1);
        Grid.SetRow((FrameworkElement) viewbox2, 0);
      }
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
      Grid.SetRow((FrameworkElement) grid3, 1);
      if (this.SecondaryFields.Count > 0 | this.AuxiliaryFields.Count > 0)
      {
        Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields, this.SecondaryFields);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
        Grid.SetRow((FrameworkElement) secAuxGrid, 2);
      }
      Grid barCodeGrid = this.createBarCodeGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) barCodeGrid);
      Grid.SetRow((FrameworkElement) barCodeGrid, 4);
      return grid1;
    }

    private Grid makeCouponGrid()
    {
      Grid grid1 = new Grid();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      RowDefinition rowDefinition3 = new RowDefinition();
      RowDefinition rowDefinition4 = new RowDefinition();
      RowDefinition rowDefinition5 = new RowDefinition();
      rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition3.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition4.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition5.Height = new GridLength(1.0, (GridUnitType) 2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition5);
      Grid grid2 = new Grid();
      Grid headerGrid = this.createHeaderGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) headerGrid);
      Grid.SetRow((FrameworkElement) headerGrid, 0);
      Grid grid3 = new Grid();
      RowDefinition rowDefinition6 = new RowDefinition();
      RowDefinition rowDefinition7 = new RowDefinition();
      rowDefinition6.Height = new GridLength(140.0);
      rowDefinition7.Height = new GridLength(70.0);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition6);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition7);
      if (this.strip != null)
      {
        Image image = new Image();
        ((FrameworkElement) image).VerticalAlignment = (VerticalAlignment) 1;
        if (this.strip2 == null)
        {
          image.Source = (ImageSource) this.strip;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip).PixelWidth, (double) ((BitmapSource) this.strip).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        else
        {
          image.Source = (ImageSource) this.strip2;
          image.Stretch = this.uniformImage((double) ((BitmapSource) this.strip2).PixelWidth, (double) ((BitmapSource) this.strip2).PixelHeight) ? (Stretch) 2 : (Stretch) 3;
        }
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) image);
        Grid.SetRowSpan((FrameworkElement) image, 2);
      }
      if (this.PrimaryFields != null && this.PrimaryFields.Count > 0)
      {
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox1 = new Viewbox();
        Viewbox viewbox2 = new Viewbox();
        viewbox1.StretchDirection = (StretchDirection) 1;
        viewbox2.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox1).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
        ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 0;
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        textBlock1.FontWeight = FontWeights.Medium;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.FontSize = 30.0;
        textBlock1.Text = this.checkLabel(this.PrimaryFields[0].Label);
        ((FrameworkElement) viewbox2).Margin = new Thickness(20.0, 0.0, 20.0, 0.0);
        ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 1;
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 100.0;
        textBlock2.Text = this.checkLabel(this.PrimaryFields[0].Value);
        viewbox1.Child = (UIElement) textBlock1;
        viewbox2.Child = (UIElement) textBlock2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox2);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox1);
        Grid.SetRow((FrameworkElement) viewbox1, 1);
        Grid.SetRow((FrameworkElement) viewbox2, 0);
      }
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
      Grid.SetRow((FrameworkElement) grid3, 1);
      if (this.SecondaryFields.Count > 0 | this.AuxiliaryFields.Count > 0)
      {
        Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields, this.SecondaryFields);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
        Grid.SetRow((FrameworkElement) secAuxGrid, 2);
      }
      Grid barCodeGrid = this.createBarCodeGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) barCodeGrid);
      Grid.SetRow((FrameworkElement) barCodeGrid, 4);
      return grid1;
    }

    private Grid makeGenericGrid()
    {
      Grid grid1 = new Grid();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      RowDefinition rowDefinition3 = new RowDefinition();
      RowDefinition rowDefinition4 = new RowDefinition();
      RowDefinition rowDefinition5 = new RowDefinition();
      RowDefinition rowDefinition6 = new RowDefinition();
      rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition3.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition4.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition5.Height = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition6.Height = new GridLength(1.0, (GridUnitType) 2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition3);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition5);
      ((PresentationFrameworkCollection<RowDefinition>) grid1.RowDefinitions).Add(rowDefinition6);
      Grid grid2 = new Grid();
      Grid headerGrid = this.createHeaderGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) headerGrid);
      Grid.SetRow((FrameworkElement) headerGrid, 0);
      Canvas canvas = new Canvas();
      ((Panel) canvas).Background = (Brush) new SolidColorBrush(Color.FromArgb((byte) 153, (byte) 0, (byte) 0, (byte) 0));
      ((FrameworkElement) canvas).Height = 1.0;
      ((FrameworkElement) canvas).Margin = new Thickness(20.0, 10.0, 20.0, 0.0);
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) canvas);
      Grid.SetRow((FrameworkElement) canvas, 1);
      Grid grid3 = new Grid();
      ((FrameworkElement) grid3).Margin = new Thickness(20.0, 10.0, 20.0, 10.0);
      ColumnDefinition columnDefinition1 = new ColumnDefinition();
      ColumnDefinition columnDefinition2 = new ColumnDefinition();
      RowDefinition rowDefinition7 = new RowDefinition();
      RowDefinition rowDefinition8 = new RowDefinition();
      RowDefinition rowDefinition9 = new RowDefinition();
      RowDefinition rowDefinition10 = new RowDefinition();
      columnDefinition1.Width = new GridLength(1.0, (GridUnitType) 2);
      columnDefinition2.Width = new GridLength(140.0);
      rowDefinition7.Height = new GridLength(40.0);
      rowDefinition8.Height = new GridLength(60.0);
      rowDefinition9.Height = new GridLength(40.0);
      rowDefinition10.Height = new GridLength(40.0);
      ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition1);
      ((PresentationFrameworkCollection<ColumnDefinition>) grid3.ColumnDefinitions).Add(columnDefinition2);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition7);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition8);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition9);
      ((PresentationFrameworkCollection<RowDefinition>) grid3.RowDefinitions).Add(rowDefinition10);
      if (this.thumb != null)
      {
        Border border = new Border();
        Image image = new Image();
        border.BorderThickness = new Thickness(8.0);
        border.BorderBrush = (Brush) new SolidColorBrush(Colors.White);
        ((FrameworkElement) border).VerticalAlignment = (VerticalAlignment) 0;
        ((FrameworkElement) border).HorizontalAlignment = (HorizontalAlignment) 2;
        image.Stretch = (Stretch) 2;
        image.Source = this.thumb2 != null ? (ImageSource) this.thumb2 : (ImageSource) this.thumb;
        border.Child = (UIElement) image;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) border);
        Grid.SetColumn((FrameworkElement) border, 1);
        Grid.SetRowSpan((FrameworkElement) border, 4);
      }
      if (this.PrimaryFields.Count > 0)
      {
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox = new Viewbox();
        viewbox.StretchDirection = (StretchDirection) 1;
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        textBlock1.FontWeight = FontWeights.Medium;
        ((FrameworkElement) textBlock1).VerticalAlignment = (VerticalAlignment) 2;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.Text = this.checkLabel(this.PrimaryFields[0].Label);
        ((FrameworkElement) textBlock1).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) viewbox).VerticalAlignment = (VerticalAlignment) 0;
        ((FrameworkElement) viewbox).HorizontalAlignment = (HorizontalAlignment) 0;
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 100.0;
        textBlock2.TextWrapping = (TextWrapping) 2;
        textBlock2.Text = this.checkLabel(this.PrimaryFields[0].Value);
        viewbox.Child = (UIElement) textBlock2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) textBlock1);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid3).Children).Add((UIElement) viewbox);
        if (this.thumb == null)
        {
          Grid.SetColumnSpan((FrameworkElement) textBlock1, 2);
          Grid.SetColumnSpan((FrameworkElement) viewbox, 2);
        }
        else
        {
          Grid.SetColumn((FrameworkElement) textBlock1, 0);
          Grid.SetColumn((FrameworkElement) viewbox, 0);
        }
        Grid.SetRow((FrameworkElement) textBlock1, 0);
        Grid.SetRow((FrameworkElement) viewbox, 1);
      }
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) grid3);
      Grid.SetRow((FrameworkElement) grid3, 2);
      if (this.barCodeType == "PKBarcodeFormatPDF417" | string.IsNullOrEmpty(this.barCodeType))
      {
        if (this.SecondaryFields.Count > 0)
        {
          Grid secAuxGrid = this.createSecAuxGrid(this.SecondaryFields);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
          Grid.SetRow((FrameworkElement) secAuxGrid, 3);
        }
        if (this.AuxiliaryFields.Count > 0)
        {
          Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
          Grid.SetRow((FrameworkElement) secAuxGrid, 4);
        }
      }
      else if (this.SecondaryFields.Count > 0 | this.AuxiliaryFields.Count > 0)
      {
        Grid secAuxGrid = this.createSecAuxGrid(this.AuxiliaryFields, this.SecondaryFields);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) secAuxGrid);
        Grid.SetRow((FrameworkElement) secAuxGrid, 3);
      }
      Grid barCodeGrid = this.createBarCodeGrid();
      ((PresentationFrameworkCollection<UIElement>) ((Panel) grid1).Children).Add((UIElement) barCodeGrid);
      Grid.SetRow((FrameworkElement) barCodeGrid, 5);
      return grid1;
    }

    private Grid createHeaderGrid()
    {
      Grid headerGrid = new Grid();
      ((FrameworkElement) headerGrid).Margin = new Thickness(20.0, 10.0, 20.0, 0.0);
      ColumnDefinition columnDefinition1 = new ColumnDefinition();
      ColumnDefinition columnDefinition2 = new ColumnDefinition();
      ColumnDefinition columnDefinition3 = new ColumnDefinition();
      ColumnDefinition columnDefinition4 = new ColumnDefinition();
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      columnDefinition1.Width = new GridLength(1.0, (GridUnitType) 0);
      columnDefinition2.Width = new GridLength(1.0, (GridUnitType) 0);
      columnDefinition3.Width = new GridLength(1.0, (GridUnitType) 0);
      columnDefinition4.Width = new GridLength(1.0, (GridUnitType) 0);
      rowDefinition1.Height = new GridLength(45.0);
      rowDefinition2.Height = new GridLength(45.0);
      ((PresentationFrameworkCollection<ColumnDefinition>) headerGrid.ColumnDefinitions).Add(columnDefinition1);
      ((PresentationFrameworkCollection<ColumnDefinition>) headerGrid.ColumnDefinitions).Add(columnDefinition2);
      ((PresentationFrameworkCollection<ColumnDefinition>) headerGrid.ColumnDefinitions).Add(columnDefinition3);
      ((PresentationFrameworkCollection<ColumnDefinition>) headerGrid.ColumnDefinitions).Add(columnDefinition4);
      ((PresentationFrameworkCollection<RowDefinition>) headerGrid.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) headerGrid.RowDefinitions).Add(rowDefinition2);
      if (this.logo != null | this.logo2 != null)
      {
        Image image = new Image();
        ((FrameworkElement) image).Height = 80.0;
        ((FrameworkElement) image).HorizontalAlignment = (HorizontalAlignment) 0;
        ((FrameworkElement) image).VerticalAlignment = (VerticalAlignment) 1;
        if (this.logo != null)
        {
          if ((double) ((BitmapSource) this.logo).PixelHeight / (double) ((BitmapSource) this.logo).PixelWidth >= 0.9)
            ((FrameworkElement) image).Width = 80.0;
          else
            ((FrameworkElement) image).Width = 256.0;
        }
        else if ((double) ((BitmapSource) this.logo2).PixelHeight / (double) ((BitmapSource) this.logo2).PixelWidth >= 0.9)
          ((FrameworkElement) image).Width = 80.0;
        else
          ((FrameworkElement) image).Width = 256.0;
        image.Source = this.logo2 == null ? (ImageSource) this.logo : (ImageSource) this.logo2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) image);
        Grid.SetColumn((FrameworkElement) image, 0);
        Grid.SetRowSpan((FrameworkElement) image, 2);
      }
      if (!string.IsNullOrEmpty(this.logoText))
      {
        TextBlock textBlock = new TextBlock();
        ((FrameworkElement) textBlock).Margin = new Thickness(5.0, 0.0, 0.0, 0.0);
        ((FrameworkElement) textBlock).VerticalAlignment = (VerticalAlignment) 1;
        ((FrameworkElement) textBlock).HorizontalAlignment = (HorizontalAlignment) 0;
        textBlock.Text = this.logoText;
        textBlock.FontFamily = new FontFamily("Segoe WP Light");
        textBlock.FontSize = 34.0;
        textBlock.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) textBlock);
        Grid.SetColumn((FrameworkElement) textBlock, 1);
        Grid.SetRowSpan((FrameworkElement) textBlock, 2);
      }
      if (this.HeaderFields.Count > 0)
      {
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox1 = new Viewbox();
        Viewbox viewbox2 = new Viewbox();
        viewbox1.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 2;
        ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 2;
        ((FrameworkElement) viewbox1).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
        textBlock1.FontWeight = FontWeights.Medium;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        viewbox2.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 2;
        ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 1;
        ((FrameworkElement) viewbox2).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 36.0;
        if (this.HeaderFields.Count == 1)
        {
          columnDefinition4.Width = new GridLength(1.0, (GridUnitType) 2);
          textBlock1.Text = this.checkLabel(this.HeaderFields[0].Label);
          textBlock2.Text = this.checkLabel(this.HeaderFields[0].Value);
          viewbox1.Child = (UIElement) textBlock1;
          viewbox2.Child = (UIElement) textBlock2;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox1);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox2);
          Grid.SetColumn((FrameworkElement) viewbox1, 3);
          Grid.SetColumn((FrameworkElement) viewbox2, 3);
          Grid.SetRow((FrameworkElement) viewbox1, 0);
          Grid.SetRow((FrameworkElement) viewbox2, 1);
        }
        else if (this.HeaderFields.Count > 1)
        {
          columnDefinition3.Width = new GridLength(1.0, (GridUnitType) 2);
          columnDefinition4.Width = new GridLength(1.0, (GridUnitType) 2);
          TextBlock textBlock3 = new TextBlock();
          TextBlock textBlock4 = new TextBlock();
          Viewbox viewbox3 = new Viewbox();
          Viewbox viewbox4 = new Viewbox();
          viewbox3.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox3).HorizontalAlignment = (HorizontalAlignment) 2;
          ((FrameworkElement) viewbox3).VerticalAlignment = (VerticalAlignment) 2;
          ((FrameworkElement) viewbox3).Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
          textBlock3.FontWeight = FontWeights.Medium;
          textBlock3.FontFamily = new FontFamily("Segoe WP Light");
          textBlock3.Foreground = (Brush) new SolidColorBrush(this.labelColor);
          viewbox4.StretchDirection = (StretchDirection) 1;
          ((FrameworkElement) viewbox4).HorizontalAlignment = (HorizontalAlignment) 2;
          ((FrameworkElement) viewbox4).VerticalAlignment = (VerticalAlignment) 1;
          ((FrameworkElement) viewbox4).Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
          textBlock4.FontSize = 36.0;
          textBlock4.FontFamily = new FontFamily("Segoe WP Light");
          textBlock4.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
          textBlock3.Text = this.checkLabel(this.HeaderFields[0].Label);
          textBlock4.Text = this.checkLabel(this.HeaderFields[0].Value);
          textBlock1.Text = this.HeaderFields[1].Label;
          textBlock2.Text = this.HeaderFields[1].Value;
          viewbox1.Child = (UIElement) textBlock1;
          viewbox2.Child = (UIElement) textBlock2;
          viewbox3.Child = (UIElement) textBlock3;
          viewbox4.Child = (UIElement) textBlock4;
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox1);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox2);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox3);
          ((PresentationFrameworkCollection<UIElement>) ((Panel) headerGrid).Children).Add((UIElement) viewbox4);
          Grid.SetColumn((FrameworkElement) viewbox1, 3);
          Grid.SetColumn((FrameworkElement) viewbox2, 3);
          Grid.SetRow((FrameworkElement) viewbox1, 0);
          Grid.SetRow((FrameworkElement) viewbox2, 1);
          Grid.SetColumn((FrameworkElement) viewbox3, 2);
          Grid.SetColumn((FrameworkElement) viewbox4, 2);
          Grid.SetRow((FrameworkElement) viewbox3, 0);
          Grid.SetRow((FrameworkElement) viewbox4, 1);
        }
      }
      return headerGrid;
    }

    private Grid createSecAuxGrid(List<ClaseField> fields)
    {
      Grid secAuxGrid = new Grid();
      ((FrameworkElement) secAuxGrid).Margin = new Thickness(20.0, -5.0, 20.0, -5.0);
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      rowDefinition1.Height = new GridLength(40.0);
      rowDefinition2.Height = new GridLength(40.0);
      ((PresentationFrameworkCollection<RowDefinition>) secAuxGrid.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) secAuxGrid.RowDefinitions).Add(rowDefinition2);
      int num = Math.Min(4, fields.Count - 1);
      for (int index = 0; index <= num; ++index)
      {
        ColumnDefinition columnDefinition = new ColumnDefinition();
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox1 = new Viewbox();
        Viewbox viewbox2 = new Viewbox();
        viewbox1.StretchDirection = (StretchDirection) 1;
        viewbox2.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 2;
        ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 1;
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        textBlock1.FontWeight = FontWeights.Medium;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.Text = this.checkLabel(fields[index].Label);
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 24.0;
        textBlock2.Text = this.checkLabel(fields[index].Value);
        if (index == 0)
        {
          columnDefinition.Width = new GridLength(1.0, (GridUnitType) 2);
          ((PresentationFrameworkCollection<ColumnDefinition>) secAuxGrid.ColumnDefinitions).Add(columnDefinition);
          ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 0;
        }
        else
        {
          if (index == num)
          {
            columnDefinition.Width = new GridLength(1.0, (GridUnitType) 0);
            ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 2;
            ((FrameworkElement) viewbox1).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
            ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 2;
            ((FrameworkElement) viewbox2).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
          }
          else
          {
            columnDefinition.Width = new GridLength(1.0, (GridUnitType) 0);
            ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 1;
            ((FrameworkElement) viewbox1).Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
            ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 1;
            ((FrameworkElement) viewbox2).Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
          }
          ((PresentationFrameworkCollection<ColumnDefinition>) secAuxGrid.ColumnDefinitions).Add(columnDefinition);
        }
        viewbox1.Child = (UIElement) textBlock1;
        viewbox2.Child = (UIElement) textBlock2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) secAuxGrid).Children).Add((UIElement) viewbox1);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) secAuxGrid).Children).Add((UIElement) viewbox2);
        Grid.SetColumn((FrameworkElement) viewbox1, index);
        Grid.SetColumn((FrameworkElement) viewbox2, index);
        Grid.SetRow((FrameworkElement) viewbox1, 0);
        Grid.SetRow((FrameworkElement) viewbox2, 1);
      }
      return secAuxGrid;
    }

    private Grid createSecAuxGrid(List<ClaseField> auxFields, List<ClaseField> secFields)
    {
      Grid secAuxGrid = new Grid();
      List<ClaseField> claseFieldList = new List<ClaseField>();
      foreach (ClaseField auxField in auxFields)
        claseFieldList.Add(auxField);
      foreach (ClaseField secField in secFields)
        claseFieldList.Add(secField);
      ((FrameworkElement) secAuxGrid).Margin = new Thickness(20.0, -5.0, 20.0, -5.0);
      RowDefinition rowDefinition1 = new RowDefinition();
      RowDefinition rowDefinition2 = new RowDefinition();
      rowDefinition1.Height = new GridLength(40.0);
      rowDefinition2.Height = new GridLength(40.0);
      ((PresentationFrameworkCollection<RowDefinition>) secAuxGrid.RowDefinitions).Add(rowDefinition1);
      ((PresentationFrameworkCollection<RowDefinition>) secAuxGrid.RowDefinitions).Add(rowDefinition2);
      int num = Math.Min(4, claseFieldList.Count - 1);
      for (int index = 0; index <= num; ++index)
      {
        ColumnDefinition columnDefinition = new ColumnDefinition();
        TextBlock textBlock1 = new TextBlock();
        TextBlock textBlock2 = new TextBlock();
        Viewbox viewbox1 = new Viewbox();
        Viewbox viewbox2 = new Viewbox();
        viewbox1.StretchDirection = (StretchDirection) 1;
        viewbox2.StretchDirection = (StretchDirection) 1;
        ((FrameworkElement) viewbox1).VerticalAlignment = (VerticalAlignment) 2;
        ((FrameworkElement) viewbox2).VerticalAlignment = (VerticalAlignment) 1;
        textBlock1.Foreground = (Brush) new SolidColorBrush(this.labelColor);
        textBlock1.FontWeight = FontWeights.Medium;
        textBlock1.FontFamily = new FontFamily("Segoe WP Light");
        textBlock1.Text = this.checkLabel(claseFieldList[index].Label);
        textBlock2.Foreground = (Brush) new SolidColorBrush(this.foregroundColor);
        textBlock2.FontFamily = new FontFamily("Segoe WP Light");
        textBlock2.FontSize = 24.0;
        textBlock2.Text = this.checkLabel(claseFieldList[index].Value);
        if (index == 0)
        {
          columnDefinition.Width = new GridLength(1.0, (GridUnitType) 2);
          ((PresentationFrameworkCollection<ColumnDefinition>) secAuxGrid.ColumnDefinitions).Add(columnDefinition);
          ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 0;
        }
        else
        {
          if (index == num)
          {
            columnDefinition.Width = new GridLength(1.0, (GridUnitType) 0);
            ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 2;
            ((FrameworkElement) viewbox1).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
            ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 2;
            ((FrameworkElement) viewbox2).Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
          }
          else
          {
            columnDefinition.Width = new GridLength(1.0, (GridUnitType) 0);
            ((FrameworkElement) viewbox1).HorizontalAlignment = (HorizontalAlignment) 1;
            ((FrameworkElement) viewbox1).Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
            ((FrameworkElement) viewbox2).HorizontalAlignment = (HorizontalAlignment) 1;
            ((FrameworkElement) viewbox2).Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
          }
          ((PresentationFrameworkCollection<ColumnDefinition>) secAuxGrid.ColumnDefinitions).Add(columnDefinition);
        }
        viewbox1.Child = (UIElement) textBlock1;
        viewbox2.Child = (UIElement) textBlock2;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) secAuxGrid).Children).Add((UIElement) viewbox1);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) secAuxGrid).Children).Add((UIElement) viewbox2);
        Grid.SetColumn((FrameworkElement) viewbox1, index);
        Grid.SetColumn((FrameworkElement) viewbox2, index);
        Grid.SetRow((FrameworkElement) viewbox1, 0);
        Grid.SetRow((FrameworkElement) viewbox2, 1);
      }
      return secAuxGrid;
    }

    private Grid createBarCodeGrid()
    {
      Grid barCodeGrid = new Grid();
      if (!string.IsNullOrEmpty(this.barCodeCode))
      {
        ((FrameworkElement) barCodeGrid).Margin = new Thickness(0.0, 10.0, 0.0, 10.0);
        ColumnDefinition columnDefinition1 = new ColumnDefinition();
        ColumnDefinition columnDefinition2 = new ColumnDefinition();
        ColumnDefinition columnDefinition3 = new ColumnDefinition();
        columnDefinition1.Width = new GridLength(1.0, (GridUnitType) 2);
        columnDefinition2.Width = new GridLength(1.0, (GridUnitType) 0);
        columnDefinition3.Width = new GridLength(1.0, (GridUnitType) 2);
        ((PresentationFrameworkCollection<ColumnDefinition>) barCodeGrid.ColumnDefinitions).Add(columnDefinition1);
        ((PresentationFrameworkCollection<ColumnDefinition>) barCodeGrid.ColumnDefinitions).Add(columnDefinition2);
        ((PresentationFrameworkCollection<ColumnDefinition>) barCodeGrid.ColumnDefinitions).Add(columnDefinition3);
        Border border = new Border();
        Image image1 = new Image();
        Image image2 = new Image();
        TextBlock textBlock = new TextBlock();
        Viewbox viewbox = new Viewbox();
        border.Background = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
        border.CornerRadius = new CornerRadius(8.0);
        ((FrameworkElement) image1).Width = 210.0;
        ((FrameworkElement) image1).Height = 20.0;
        image1.Stretch = (Stretch) 1;
        if (this.footer != null)
        {
          image1.Source = this.footer2 != null ? (ImageSource) this.footer2 : (ImageSource) this.footer;
          border.CornerRadius = new CornerRadius(0.0, 0.0, 5.0, 5.0);
        }
        else
          ((FrameworkElement) image1).Height = 0.0;
        this.setBarCode(this.barCodeCode, this.barCodeType, this.altText);
        if (this.barCodeType == "PKBarcodeFormatPDF417")
        {
          ((UIElement) image2).UseLayoutRounding = true;
          ((FrameworkElement) border).Width = 400.0;
          ((FrameworkElement) image2).Width = 380.0;
          ((FrameworkElement) image2).Height = 105.0;
          ((FrameworkElement) image2).Margin = new Thickness(10.0, 10.0, 10.0, 0.0);
          image2.Stretch = (Stretch) 1;
          image2.Source = (ImageSource) this.barCode;
          ((FrameworkElement) image2).HorizontalAlignment = (HorizontalAlignment) 0;
          ((FrameworkElement) image2).VerticalAlignment = (VerticalAlignment) 0;
          RowDefinition rowDefinition1 = new RowDefinition();
          RowDefinition rowDefinition2 = new RowDefinition();
          RowDefinition rowDefinition3 = new RowDefinition();
          RowDefinition rowDefinition4 = new RowDefinition();
          RowDefinition rowDefinition5 = new RowDefinition();
          RowDefinition rowDefinition6 = new RowDefinition();
          RowDefinition rowDefinition7 = new RowDefinition();
          rowDefinition1.Height = new GridLength(1.0, (GridUnitType) 2);
          rowDefinition2.Height = new GridLength(1.0, (GridUnitType) 0);
          rowDefinition3.Height = new GridLength(25.0);
          rowDefinition4.Height = new GridLength(115.0);
          rowDefinition5.Height = new GridLength(30.0);
          rowDefinition6.Height = new GridLength(25.0);
          rowDefinition7.Height = new GridLength(1.0, (GridUnitType) 2);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition1);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition2);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition3);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition4);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition5);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition6);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition7);
          Grid.SetRow((FrameworkElement) image2, 3);
          Grid.SetRow((FrameworkElement) border, 3);
          Grid.SetRowSpan((FrameworkElement) border, 2);
          Grid.SetRow((FrameworkElement) viewbox, 4);
        }
        else
        {
          ((FrameworkElement) image2).Width = 180.0;
          ((FrameworkElement) image2).Height = 180.0;
          ((FrameworkElement) border).Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
          image2.Stretch = (Stretch) 1;
          ((FrameworkElement) image2).Margin = string.IsNullOrEmpty(this.altText) ? new Thickness(0.0, 15.0, 0.0, -15.0) : new Thickness(0.0, 15.0, 0.0, 0.0);
          image2.Source = (ImageSource) this.barCode;
          ((FrameworkElement) image2).HorizontalAlignment = (HorizontalAlignment) 1;
          ((FrameworkElement) image2).VerticalAlignment = (VerticalAlignment) 1;
          RowDefinition rowDefinition8 = new RowDefinition();
          RowDefinition rowDefinition9 = new RowDefinition();
          RowDefinition rowDefinition10 = new RowDefinition();
          RowDefinition rowDefinition11 = new RowDefinition();
          RowDefinition rowDefinition12 = new RowDefinition();
          rowDefinition8.Height = new GridLength(1.0, (GridUnitType) 2);
          rowDefinition9.Height = new GridLength(1.0, (GridUnitType) 0);
          rowDefinition10.Height = new GridLength(1.0, (GridUnitType) 0);
          rowDefinition11.Height = new GridLength(30.0);
          rowDefinition12.Height = new GridLength(1.0, (GridUnitType) 2);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition8);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition9);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition10);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition11);
          ((PresentationFrameworkCollection<RowDefinition>) barCodeGrid.RowDefinitions).Add(rowDefinition12);
          ((FrameworkElement) barCodeGrid).Tag = (object) false;
          ((UIElement) barCodeGrid).Tap += new EventHandler<GestureEventArgs>(this.barCodeGrid_OnTap);
          Grid.SetRow((FrameworkElement) image2, 2);
          Grid.SetRow((FrameworkElement) border, 2);
          Grid.SetRowSpan((FrameworkElement) border, 2);
          Grid.SetRow((FrameworkElement) viewbox, 3);
        }
        textBlock.Foreground = (Brush) new SolidColorBrush(Colors.Black);
        textBlock.Text = this.altText;
        ((FrameworkElement) viewbox).HorizontalAlignment = (HorizontalAlignment) 1;
        ((FrameworkElement) viewbox).VerticalAlignment = (VerticalAlignment) 0;
        viewbox.StretchDirection = (StretchDirection) 1;
        viewbox.Child = (UIElement) textBlock;
        ((PresentationFrameworkCollection<UIElement>) ((Panel) barCodeGrid).Children).Add((UIElement) border);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) barCodeGrid).Children).Add((UIElement) image1);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) barCodeGrid).Children).Add((UIElement) image2);
        ((PresentationFrameworkCollection<UIElement>) ((Panel) barCodeGrid).Children).Add((UIElement) viewbox);
        Grid.SetColumn((FrameworkElement) border, 1);
        Grid.SetColumn((FrameworkElement) image1, 1);
        Grid.SetColumn((FrameworkElement) image2, 1);
        Grid.SetColumn((FrameworkElement) viewbox, 1);
        Grid.SetRow((FrameworkElement) image1, 1);
      }
      return barCodeGrid;
    }

    private void barCodeGrid_OnTap(object sender, GestureEventArgs e)
    {
      Grid grid = (Grid) sender;
      Image child = (Image) ((PresentationFrameworkCollection<UIElement>) ((Panel) grid).Children)[2];
      double num = 1.28;
      if (!Convert.ToBoolean(((FrameworkElement) grid).Tag))
      {
        Image image1 = child;
        ((FrameworkElement) image1).Width = ((FrameworkElement) image1).Width * num;
        Image image2 = child;
        ((FrameworkElement) image2).Height = ((FrameworkElement) image2).Height * num;
        ((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions)[1].Width = new GridLength(((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions)[1].ActualWidth * num, (GridUnitType) 1);
      }
      else
      {
        Image image3 = child;
        ((FrameworkElement) image3).Width = ((FrameworkElement) image3).Width / num;
        Image image4 = child;
        ((FrameworkElement) image4).Height = ((FrameworkElement) image4).Height / num;
        ((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions)[1].Width = new GridLength(((PresentationFrameworkCollection<ColumnDefinition>) grid.ColumnDefinitions)[1].ActualWidth / num, (GridUnitType) 1);
      }
      ((FrameworkElement) grid).Tag = (object) !Convert.ToBoolean(((FrameworkElement) grid).Tag);
    }

    private void createBackPanel()
    {
      if (!Application.Current.Resources.Contains((object) "screenWidth"))
        Application.Current.Resources.Add("screenWidth", (object) Application.Current.RootVisual.RenderSize.Width);
      this.passBackPageFields = new ObservableCollection<infoFields>();
      for (int index = 0; index <= this.BackFields.Count - 1; ++index)
        this.passBackPageFields.Add(new infoFields(this.checkLabel(this.BackFields[index].Label), this.checkLabel(this.BackFields[index].Value)));
    }

    public string checkLabel(string label)
    {
      bool flag = false;
      int index = 0;
      if (this.hasStrings)
      {
        try
        {
          if (this.stringsIndex == -1)
          {
            for (; index < this.locationStrings.Count & !flag; ++index)
            {
              if (CultureInfo.CurrentCulture.Name.Contains(this.locationStrings[index].Language))
              {
                flag = true;
                this.stringsIndex = index;
              }
              else if (this.locationStrings[index].Language.Contains("en") & !flag)
                this.stringsIndex = index;
            }
          }
          foreach (ClaseField field in this.locationStrings[this.stringsIndex].Fields)
          {
            if (label == field.Label)
              return field.Value;
          }
        }
        catch (Exception ex)
        {
          return label;
        }
      }
      return label;
    }

    public string textTrimming(TextBlock tb, double desiredWidth)
    {
      string str = tb.Text;
      ((UIElement) tb).Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      while (((UIElement) tb).DesiredSize.Width > desiredWidth)
      {
        str = str.Remove(str.Length - 1);
        tb.Text = str + "...";
        ((UIElement) tb).Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      }
      return tb.Text;
    }

    public bool isTextTrimming(TextBlock tb, double desiredWidth)
    {
      string text = tb.Text;
      ((UIElement) tb).Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
      return ((UIElement) tb).DesiredSize.Width > desiredWidth;
    }

    private string correctText(string Text)
    {
      Text = Text.ToLower();
      Text = Text.Substring(0, 1).ToUpper() + Text.Substring(1);
      for (int index = Text.IndexOf(" "); index != -1; index = Text.IndexOf(" ", index + 1))
        Text = Text.Substring(0, index + 1) + Text.Substring(index + 1, 1).ToUpper() + Text.Substring(index + 2);
      return Text;
    }

    private RichTextBox formatURL(object sender, string e)
    {
      RichTextBox richTextBox = (RichTextBox) sender;
      int startIndex1 = 0;
      bool flag1 = false;
      Paragraph paragraph1 = new Paragraph();
      Paragraph paragraph2 = new Paragraph();
      bool flag2 = false;
      foreach (Match match in ClasePass.RE_URL.Matches(e))
      {
        flag1 = true;
        if (match.Index != startIndex1)
        {
          string str = e.Substring(startIndex1, match.Index - startIndex1);
          ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) new Run()
          {
            Text = str
          });
        }
        string uriString = match.Value;
        Uri result;
        if (!Uri.TryCreate(uriString, UriKind.Absolute, out result) && !uriString.StartsWith("http://"))
          Uri.TryCreate("http://" + uriString, UriKind.Absolute, out result);
        if (result != (Uri) null)
        {
          Hyperlink hyperlink = new Hyperlink();
          hyperlink.NavigateUri = result;
          ((Span) hyperlink).Inlines.Add(uriString);
          hyperlink.TargetName = "_blank";
          hyperlink.MouseOverForeground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 132, (byte) 193, byte.MaxValue));
          ((TextElement) hyperlink).Foreground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 123, (byte) 215));
          ((TextElement) hyperlink).FontSize = 25.0;
          ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) hyperlink);
        }
        else
          paragraph1.Inlines.Add(uriString);
        startIndex1 = match.Index + match.Length;
      }
      int startIndex2 = 0;
      foreach (Match match in ClasePass.RE_EMAIL.Matches(e))
      {
        flag1 = true;
        flag2 = true;
        if (match.Index != startIndex2)
        {
          string str = e.Substring(startIndex2, match.Index - startIndex2);
          ((PresentationFrameworkCollection<Inline>) paragraph2.Inlines).Add((Inline) new Run()
          {
            Text = str
          });
        }
        string str1 = match.Value;
        Uri result = (Uri) null;
        Uri.TryCreate("mailto:" + str1, UriKind.Absolute, out result);
        if (result != (Uri) null)
        {
          Hyperlink hyperlink = new Hyperlink();
          hyperlink.NavigateUri = result;
          ((Span) hyperlink).Inlines.Add(str1);
          hyperlink.TargetName = "_blank";
          hyperlink.MouseOverForeground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 132, (byte) 193, byte.MaxValue));
          ((TextElement) hyperlink).Foreground = (Brush) new SolidColorBrush(Color.FromArgb(byte.MaxValue, (byte) 0, (byte) 123, (byte) 215));
          ((TextElement) hyperlink).FontSize = 25.0;
          ((PresentationFrameworkCollection<Inline>) paragraph2.Inlines).Add((Inline) hyperlink);
        }
        else
          paragraph2.Inlines.Add(str1);
        startIndex2 = match.Index + match.Length;
      }
      if (!flag1)
        ((PresentationFrameworkCollection<Inline>) paragraph1.Inlines).Add((Inline) new Run()
        {
          Text = e
        });
      if (flag2)
        paragraph1 = paragraph2;
      ((PresentationFrameworkCollection<Block>) richTextBox.Blocks).Add((Block) paragraph1);
      return richTextBox;
    }

    public async void newLoadImages()
    {
      this.assignLocationImg();
      await this.loadImages(true);
      await this.loadImages(false);
    }

    public async void newLoadImages(bool loaded)
    {
      this.assignLocationImg();
      await this.loadImages(loaded);
    }

    public void assignLocationImg()
    {
      bool flag = false;
      int index1 = 0;
      int index2 = -1;
      if (!this.hasStringsImg)
        return;
      try
      {
        for (; index1 < this.locationStringsImg.Count & !flag; ++index1)
        {
          if (CultureInfo.CurrentCulture.Name.Contains(this.locationStringsImg[index1].Language))
          {
            flag = true;
            index2 = index1;
          }
          else if (this.locationStringsImg[index1].Language.Contains("en") & !flag)
            index2 = index1;
        }
        for (int index3 = 0; index3 < this.locationStringsImg[index2].Fields.Count; ++index3)
        {
          switch (this.locationStringsImg[index2].Fields[index3].label.ToLower())
          {
            case "icon":
              this.iconB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.icon2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
            case "logo":
              this.logoB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.logo2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
            case "background":
              this.backgroundB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.background2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
            case "footer":
              this.footerB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.footer2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
            case "thumbnail":
              this.thumbB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.thumb2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
            case "strip":
              this.stripB = this.locationStringsImg[index2].Fields[index3].imgLow;
              this.strip2B = this.locationStringsImg[index2].Fields[index3].imgHigh;
              break;
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    public async Task loadImages(bool hasPriority)
    {
      if (hasPriority)
      {
        this.logo = this.ToImage(this.logoB);
        this.logo2 = this.ToImage(this.logo2B);
        if (this.backgroundB != null)
        {
          MemoryStream memoryStream = new MemoryStream(this.backgroundB);
          BitmapImage bitmapImage = new BitmapImage();
          ((BitmapSource) bitmapImage).SetSource((Stream) memoryStream);
          WriteableBitmap wb = new WriteableBitmap((BitmapSource) bitmapImage);
          WriteableBitmap wb1 = new WriteableBitmap(((BitmapSource) bitmapImage).PixelWidth, ((BitmapSource) bitmapImage).PixelHeight);
          Task<WriteableBitmap>[] taskArray = new Task<WriteableBitmap>[1]
          {
            Task.Run<WriteableBitmap>((Func<Task<WriteableBitmap>>) (async () => await this.ToImageBlur(this.backgroundB, wb, wb1)))
          };
          Task.WaitAny((Task[]) taskArray);
          wb = taskArray[0].Result;
          this.background = wb;
        }
        this.background2 = await this.ToImageBlur(this.background2B);
      }
      else
      {
        this.icon = this.ToImage(this.iconB);
        this.icon2 = this.ToImage(this.icon2B);
        this.thumb = this.ToImage(this.thumbB);
        this.thumb2 = this.ToImage(this.thumb2B);
        this.strip = this.ToImage(this.stripB);
        this.strip2 = this.ToImage(this.strip2B);
        this.footer = this.ToImage(this.footerB);
        this.footer2 = this.ToImage(this.footer2B);
      }
    }

    private BitmapImage ToImage(byte[] bArray)
    {
      if (bArray == null)
        return (BitmapImage) null;
      MemoryStream memoryStream = new MemoryStream(bArray);
      BitmapImage image = new BitmapImage();
      ((BitmapSource) image).SetSource((Stream) memoryStream);
      return image;
    }

    private async Task<WriteableBitmap> ToImageBlur(byte[] bArray)
    {
      if (bArray == null)
        return (WriteableBitmap) null;
      MemoryStream memoryStream = new MemoryStream(bArray);
      BitmapImage bitmapImage = new BitmapImage();
      ((BitmapSource) bitmapImage).SetSource((Stream) memoryStream);
      return this.BoxBlur(this.SetBrightness(new WriteableBitmap((BitmapSource) bitmapImage), this.determineBrightnessFromColor(this.foregroundColor)), 15);
    }

    private async Task<WriteableBitmap> ToImageBlur(
      byte[] bArray,
      WriteableBitmap wb,
      WriteableBitmap wb1)
    {
      if (bArray == null)
        return (WriteableBitmap) null;
      wb1 = this.SetBrightness(wb, this.determineBrightnessFromColor(this.foregroundColor));
      wb1 = this.BoxBlur(wb1, 15);
      return wb1;
    }

    private bool uniformImage(double imgWidth, double imgHeight)
    {
      double actualWidth = Application.Current.Host.Content.ActualWidth;
      double num = 210.0 / imgHeight;
      return imgWidth * num >= actualWidth;
    }

    private int determineBrightnessFromColor(Color color)
    {
      if (color.B < (byte) 20 && color.G < (byte) 20 && color.R < (byte) 20)
        return 60;
      return color.B > (byte) 230 && color.G > (byte) 230 && color.R > (byte) 230 ? -10 : 0;
    }

    private bool isEqualColors(string strColor, Color color)
    {
      byte num1 = byte.Parse(strColor.Substring(3, 2), NumberStyles.HexNumber);
      byte num2 = byte.Parse(strColor.Substring(5, 2), NumberStyles.HexNumber);
      byte num3 = byte.Parse(strColor.Substring(7, 2), NumberStyles.HexNumber);
      byte num4 = byte.Parse(strColor.Substring(1, 2), NumberStyles.HexNumber);
      return (int) color.R == (int) num1 && (int) color.G == (int) num2 && (int) color.B == (int) num3 && (int) color.A == (int) num4;
    }

    public WriteableBitmap SetBrightness(WriteableBitmap wb, int brightness)
    {
      if (brightness < -255)
        brightness = -255;
      if (brightness > (int) byte.MaxValue)
        brightness = (int) byte.MaxValue;
      byte[] byteArray = wb.ToByteArray();
      int length = byteArray.Length;
      for (int index = 0; index < length; ++index)
      {
        int num1 = (int) byteArray[index];
        int num2 = num1 & (int) byte.MaxValue;
        int num3 = num1 >> 8;
        int num4 = num3 & (int) byte.MaxValue;
        int num5 = num3 >> 8;
        int num6 = num5 & (int) byte.MaxValue;
        int num7 = num5 >> 8;
        if (num7 == 0)
          num7 = 1;
        int num8 = num2 + brightness;
        int num9 = num6 + brightness;
        int num10 = num4 + brightness;
        if (num9 > (int) byte.MaxValue)
          num9 = (int) byte.MaxValue;
        if (num10 > (int) byte.MaxValue)
          num10 = (int) byte.MaxValue;
        if (num8 > (int) byte.MaxValue)
          num8 = (int) byte.MaxValue;
        if (num9 < 0)
          num9 = 1;
        if (num10 < 0)
          num10 = 1;
        if (num8 < 0)
          num8 = 1;
        byte num11 = (byte) num9;
        byte num12 = (byte) num10;
        byte num13 = (byte) num8;
        byte num14 = (byte) num7;
        byteArray[index] = (byte) ((uint) ((int) num14 << 24 | (int) num11 << 16 | (int) num12 << 8) | (uint) num13);
      }
      wb = wb.FromByteArray(byteArray);
      return wb;
    }

    public WriteableBitmap BoxBlur(WriteableBitmap bmp, int range)
    {
      bmp = (range & 1) != 0 ? this.BoxBlurHorizontal(bmp, range) : throw new InvalidOperationException("Range must be odd!");
      bmp = this.BoxBlurVertical(bmp, range);
      return bmp;
    }

    public WriteableBitmap BoxBlurHorizontal(WriteableBitmap wb, int range)
    {
      int pixelWidth = ((BitmapSource) wb).PixelWidth;
      int pixelHeight = ((BitmapSource) wb).PixelHeight;
      int num1 = range / 2;
      int num2 = 0;
      int[] numArray = new int[pixelWidth];
      for (int index1 = 0; index1 < pixelHeight; ++index1)
      {
        int num3 = 0;
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        for (int index2 = -num1; index2 < pixelWidth; ++index2)
        {
          int num7 = index2 - num1 - 1;
          if (num7 >= 0)
          {
            int pixel = wb.Pixels[num2 + num7];
            if (pixel != 0)
            {
              num4 -= (int) (byte) (pixel >> 16);
              num5 -= (int) (byte) (pixel >> 8);
              num6 -= (int) (byte) pixel;
            }
            --num3;
          }
          int num8 = index2 + num1;
          if (num8 < pixelWidth)
          {
            int pixel = wb.Pixels[num2 + num8];
            if (pixel != 0)
            {
              num4 += (int) (byte) (pixel >> 16);
              num5 += (int) (byte) (pixel >> 8);
              num6 += (int) (byte) pixel;
            }
            ++num3;
          }
          if (index2 >= 0)
          {
            int num9 = -16777216 | (int) (byte) (num4 / num3) << 16 | (int) (byte) (num5 / num3) << 8 | (int) (byte) (num6 / num3);
            numArray[index2] = num9;
          }
        }
        for (int index3 = 0; index3 < pixelWidth; ++index3)
          wb.SetPixeli(num2 + index3, numArray[index3]);
        num2 += pixelWidth;
      }
      return wb;
    }

    public WriteableBitmap BoxBlurVertical(WriteableBitmap wb, int range)
    {
      int pixelWidth = ((BitmapSource) wb).PixelWidth;
      int pixelHeight = ((BitmapSource) wb).PixelHeight;
      int num1 = range / 2;
      int[] numArray = new int[pixelHeight];
      int num2 = -(num1 + 1) * pixelWidth;
      int num3 = num1 * pixelWidth;
      for (int index1 = 0; index1 < pixelWidth; ++index1)
      {
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        int num7 = 0;
        int num8 = -num1 * pixelWidth + index1;
        for (int index2 = -num1; index2 < pixelHeight; ++index2)
        {
          if (index2 - num1 - 1 >= 0)
          {
            int pixel = wb.Pixels[num8 + num2];
            if (pixel != 0)
            {
              num5 -= (int) (byte) (pixel >> 16);
              num6 -= (int) (byte) (pixel >> 8);
              num7 -= (int) (byte) pixel;
            }
            --num4;
          }
          if (index2 + num1 < pixelHeight)
          {
            int pixel = wb.Pixels[num8 + num3];
            if (pixel != 0)
            {
              num5 += (int) (byte) (pixel >> 16);
              num6 += (int) (byte) (pixel >> 8);
              num7 += (int) (byte) pixel;
            }
            ++num4;
          }
          if (index2 >= 0)
          {
            int num9 = -16777216 | (int) (byte) (num5 / num4) << 16 | (int) (byte) (num6 / num4) << 8 | (int) (byte) (num7 / num4);
            numArray[index2] = num9;
          }
          num8 += pixelWidth;
        }
        for (int index3 = 0; index3 < pixelHeight; ++index3)
          wb.SetPixeli(index3 * pixelWidth + index1, numArray[index3]);
      }
      return wb;
    }
  }
}
