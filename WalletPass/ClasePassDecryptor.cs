// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassDecryptor
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace WalletPass
{
  internal class ClasePassDecryptor
  {
    private string filePass;
    private string filePassString;

    public ClasePass clsPass { get; set; }

    public ClasePassDecryptor()
    {
      this.clsPass = new ClasePass();
      this.filePass = "";
    }

    public async Task<int> extractCompressedFile(Stream fileStream, StorageFolder local)
    {
      StorageFolder dataFolder = await local.CreateFolderAsync("temp", (CreationCollisionOption) 3);
      try
      {
        fileStream.Position = 0L;
        ZipArchive archive = new ZipArchive(fileStream);
        try
        {
          IEnumerator<ZipArchiveEntry> enumerator = archive.Entries.GetEnumerator();
          try
          {
            while (((IEnumerator) enumerator).MoveNext())
            {
              ZipArchiveEntry entry = enumerator.Current;
              if (entry.FullName.LastIndexOf("/") != entry.FullName.Length - 1)
              {
                string filePath = entry.FullName;
                if (filePath.Contains("/"))
                  filePath = filePath.Substring(filePath.LastIndexOf("/") + 1);
                StorageFile file = await dataFolder.CreateFileAsync(filePath, (CreationCollisionOption) 0);
                if (entry.FullName.Contains("png") && entry.FullName.Contains("/"))
                {
                  Stream stream = await ((IStorageFile) file).OpenStreamForWriteAsync();
                  MemoryStream streamEntry = new MemoryStream();
                  BitmapImage bitmapImage = new BitmapImage();
                  entry.Open().CopyTo((Stream) streamEntry);
                  if (streamEntry.Length != 0L)
                    this.formatPassImg(streamEntry.ToArray(), entry.FullName);
                }
                else if (entry.FullName.Contains("png"))
                {
                  Stream stream = await ((IStorageFile) file).OpenStreamForWriteAsync();
                  MemoryStream streamEntry = new MemoryStream();
                  BitmapImage bitmapImage = new BitmapImage();
                  entry.Open().CopyTo((Stream) streamEntry);
                  if (streamEntry.Length != 0L)
                  {
                    switch (entry.FullName.ToLower())
                    {
                      case "icon.png":
                        this.clsPass.iconB = streamEntry.ToArray();
                        continue;
                      case "icon@2x.png":
                        this.clsPass.icon2B = streamEntry.ToArray();
                        continue;
                      case "logo.png":
                        this.clsPass.logoB = streamEntry.ToArray();
                        continue;
                      case "logo@2x.png":
                        this.clsPass.logo2B = streamEntry.ToArray();
                        continue;
                      case "background.png":
                        this.clsPass.backgroundB = streamEntry.ToArray();
                        continue;
                      case "background@2x.png":
                        this.clsPass.background2B = streamEntry.ToArray();
                        continue;
                      case "footer.png":
                        this.clsPass.footerB = streamEntry.ToArray();
                        continue;
                      case "footer@2x.png":
                        this.clsPass.footer2B = streamEntry.ToArray();
                        continue;
                      case "thumbnail.png":
                        this.clsPass.thumbB = streamEntry.ToArray();
                        continue;
                      case "thumbnail@2x.png":
                        this.clsPass.thumb2B = streamEntry.ToArray();
                        continue;
                      case "strip.png":
                        this.clsPass.stripB = streamEntry.ToArray();
                        continue;
                      case "strip@2x.png":
                        this.clsPass.strip2B = streamEntry.ToArray();
                        continue;
                      default:
                        continue;
                    }
                  }
                }
                else if (entry.FullName.Equals("pass.json", StringComparison.OrdinalIgnoreCase))
                {
                  MemoryStream destination = new MemoryStream();
                  entry.Open().CopyTo((Stream) destination);
                  byte[] array = destination.ToArray();
                  if (this.contain3Bytes0(array))
                    this.filePass = this.extractWrongBytes(this.filePass, array);
                  if (string.IsNullOrEmpty(this.filePass))
                  {
                    StreamReader streamReader = new StreamReader((Stream) destination);
                    destination.Position = 0L;
                    this.filePass = streamReader.ReadToEnd();
                  }
                  this.filePass = this.decodeFromUnicode(this.filePass);
                  this.formatPassJSON(JsonConvert.DeserializeObject<Pass>(this.filePass));
                }
                else if (entry.FullName.Contains("pass.strings"))
                {
                  Stream newFileStream = await ((IStorageFile) file).OpenStreamForWriteAsync();
                  MemoryStream streamEntry = new MemoryStream();
                  entry.Open().CopyTo((Stream) streamEntry);
                  byte[] streamEntryArray = streamEntry.ToArray();
                  this.filePassString = Encoding.UTF8.GetString(streamEntryArray, 0, streamEntryArray.Length);
                  newFileStream.Flush();
                  newFileStream.Dispose();
                  await this.formatPassString(this.filePassString, entry.FullName);
                }
              }
            }
          }
          finally
          {
            ((IDisposable) enumerator)?.Dispose();
          }
        }
        finally
        {
          archive?.Dispose();
        }
        if (this.clsPass.backgroundB != null || this.clsPass.background2B != null)
        {
          this.clsPass.foregroundColor = Colors.White;
          this.clsPass.labelColor = Colors.White;
        }
        this.clsPass.newLoadImages();
        if (dataFolder != null)
          await dataFolder.DeleteAsync((StorageDeleteOption) 1);
        return 0;
      }
      catch (Exception ex)
      {
        return !this.checkIfHTML(fileStream) ? -1 : 1;
      }
    }

    public async Task<int> extractCompressedFile(string fileName, StorageFolder local)
    {
      StorageFile sourceFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
      int result = 0;
      try
      {
        Stream fileStream = await ((IStorageFile) sourceFile).OpenStreamForReadAsync();
        try
        {
          result = await this.extractCompressedFile(fileStream, local);
        }
        finally
        {
          ((IDisposable) fileStream)?.Dispose();
        }
        return result;
      }
      catch
      {
        return -1;
      }
    }

    private bool checkIfHTML(Stream fileStream)
    {
      StreamReader streamReader = new StreamReader(fileStream);
      string end;
      try
      {
        end = streamReader.ReadToEnd();
      }
      finally
      {
        ((IDisposable) streamReader)?.Dispose();
      }
      return end.Contains("DOCTYPE html") || end.Contains("html PUBLIC") || end.Contains("</html>");
    }

    private void formatPassJSON(Pass tempPass)
    {
      this.clsPass.serialNumberGUID = Guid.NewGuid().ToString();
      this.clsPass.serialNumber = tempPass.serialNumber;
      this.clsPass.organizationName = tempPass.organizationName;
      this.clsPass.description = tempPass.description;
      this.clsPass.webServiceURL = tempPass.webServiceURL;
      this.clsPass.passTypeIdentifier = tempPass.passTypeIdentifier;
      this.clsPass.teamIdentifier = tempPass.teamIdentifier;
      this.clsPass.authenticationToken = tempPass.authenticationToken;
      this.clsPass.relevantDate = this.convertToDateTime(tempPass.relevantDate);
      this.clsPass.expirationDate = this.convertToDateTime(tempPass.expirationDate);
      this.clsPass.logoText = tempPass.logoText;
      this.clsPass.backgroundColor = this.extractColor(tempPass.backgroundColor, Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
      this.clsPass.foregroundColor = this.extractColor(tempPass.foregroundColor, this.clsPass.backgroundColor);
      this.clsPass.labelColor = this.extractColor(tempPass.labelColor, this.clsPass.backgroundColor);
      this.clsPass.type = this.determineType(tempPass);
      this.clsPass.Locations = this.extractLocations(tempPass);
      if (tempPass.barcode != null)
      {
        this.clsPass.barCodeCode = tempPass.barcode.message;
        this.clsPass.altText = tempPass.barcode.altText;
        this.clsPass.barCodeType = tempPass.barcode.format;
      }
      passType passType = (passType) null;
      if (this.clsPass.type == "boardingPass")
      {
        this.clsPass.transitType = tempPass.boardingPass.transitType;
        passType = tempPass.boardingPass;
      }
      else if (this.clsPass.type == "coupon")
        passType = tempPass.coupon;
      else if (this.clsPass.type == "eventTicket")
        passType = tempPass.eventTicket;
      else if (this.clsPass.type == "storeCard")
        passType = tempPass.storeCard;
      else if (this.clsPass.type == "generic")
        passType = tempPass.generic;
      this.clsPass.HeaderFields = this.extractFields(passType.headerFields);
      this.clsPass.PrimaryFields = this.extractFields(passType.primaryFields);
      this.clsPass.SecondaryFields = this.extractFields(passType.secondaryFields);
      this.clsPass.AuxiliaryFields = this.extractFields(passType.auxiliaryFields);
      this.clsPass.BackFields = this.extractFields(passType.backFields);
    }

    private Color extractColor(string strColor, Color antColor)
    {
      if (strColor != null)
      {
        if (strColor.Contains("rgba"))
        {
          int num1 = 0;
          byte num2 = Convert.ToByte(strColor.Substring(num1 + 5, strColor.IndexOf(",") - (num1 + 5)));
          int startIndex1 = strColor.IndexOf(",") + 1;
          byte num3 = Convert.ToByte(strColor.Substring(startIndex1, strColor.IndexOf(",", startIndex1) - startIndex1));
          int startIndex2 = strColor.IndexOf(",", startIndex1) + 1;
          byte num4 = Convert.ToByte(strColor.Substring(startIndex2, strColor.IndexOf(",", startIndex2) - startIndex2));
          int startIndex3 = strColor.IndexOf(",", startIndex2) + 1;
          return Color.FromArgb(Convert.ToByte(Convert.ToInt32(Convert.ToDouble(strColor.Substring(startIndex3, strColor.IndexOf(")", startIndex3) - startIndex3), (IFormatProvider) new CultureInfo("en-US")) * (double) byte.MaxValue)), num2, num3, num4);
        }
        if (strColor.Contains("rgb"))
        {
          int num5 = 0;
          byte num6 = Convert.ToByte(strColor.Substring(num5 + 4, strColor.IndexOf(",") - (num5 + 4)));
          int startIndex4 = strColor.IndexOf(",") + 1;
          byte num7 = Convert.ToByte(strColor.Substring(startIndex4, strColor.IndexOf(",", startIndex4) - startIndex4));
          int startIndex5 = strColor.IndexOf(",", startIndex4) + 1;
          byte num8 = Convert.ToByte(strColor.Substring(startIndex5, strColor.IndexOf(")", startIndex5) - startIndex5));
          return Color.FromArgb(byte.MaxValue, num6, num7, num8);
        }
        if (strColor.Contains("#"))
          return Color.FromArgb(byte.MaxValue, byte.Parse(strColor.Substring(1, 2), NumberStyles.HexNumber), byte.Parse(strColor.Substring(3, 2), NumberStyles.HexNumber), byte.Parse(strColor.Substring(5, 2), NumberStyles.HexNumber));
      }
      return antColor.R > (byte) 210 & antColor.G > (byte) 210 & antColor.B > (byte) 210 ? Colors.Black : Colors.White;
    }

    private string determineType(Pass tempPass)
    {
      if (tempPass.boardingPass != null)
        return "boardingPass";
      if (tempPass.coupon != null)
        return "coupon";
      if (tempPass.eventTicket != null)
        return "eventTicket";
      if (tempPass.storeCard != null)
        return "storeCard";
      return tempPass.generic != null ? "generic" : "";
    }

    private List<ClaseLocations> extractLocations(Pass tempPass)
    {
      List<ClaseLocations> locations = new List<ClaseLocations>();
      if (tempPass.locations != null)
      {
        List<Location>.Enumerator enumerator = tempPass.locations.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            Location current = enumerator.Current;
            ClaseLocations claseLocations = new ClaseLocations(Convert.ToDouble((object) (double) current.latitude, (IFormatProvider) new CultureInfo("en-US")), Convert.ToDouble((object) (double) current.longitude, (IFormatProvider) new CultureInfo("en-US")), current.relevantText, current.altitude);
            locations.Add(claseLocations);
          }
        }
        finally
        {
          enumerator.Dispose();
        }
      }
      return locations;
    }

    private DateTime convertToDateTime(string date)
    {
      try
      {
        if (!string.IsNullOrEmpty(date) && date.Contains("T"))
        {
          DateTimeOffset result;
          DateTimeOffset.TryParse(date, out result);
          return result.LocalDateTime;
        }
        return string.IsNullOrEmpty(date) ? new DateTime(1, 1, 1) : Convert.ToDateTime(date);
      }
      catch (Exception ex)
      {
        return Convert.ToDateTime(date);
      }
    }

    private List<ClaseField> extractFields(List<passField> zoneField)
    {
      List<ClaseField> fields = new List<ClaseField>();
      if (zoneField != null && zoneField.Count > 0)
      {
        List<passField>.Enumerator enumerator = zoneField.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            passField current = enumerator.Current;
            ClaseField claseField = new ClaseField();
            if (!string.IsNullOrEmpty(current.dateStyle) || !string.IsNullOrEmpty(current.timeStyle))
            {
              DateTime dateTime1 = new DateTime();
              string str1 = "";
              string str2 = "";
              DateTime dateTime2 = Convert.ToDateTime(current.value);
              if (!current.ignoresTimeZone)
                dateTime2 = dateTime2.ToLocalTime();
              switch (current.dateStyle)
              {
                case "PKDateStyleNone":
                  str1 = "";
                  break;
                case "PKDateStyleShort":
                  str1 = "d";
                  break;
                case "PKDateStyleMedium":
                  str1 = "MMM dd, yyyy";
                  break;
                case "PKDateStyleLong":
                  str1 = "MMMM dd, yyyy";
                  break;
                case "PKDateStyleFull":
                  str1 = "dddd, MMMM dd, yyyy gg";
                  break;
              }
              string str3 = !string.IsNullOrEmpty(str1) ? dateTime2.ToString(str1, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.Date.ToString();
              switch (current.timeStyle)
              {
                case "PKDateStyleNone":
                  str2 = "t";
                  break;
                case "PKDateStyleShort":
                  str2 = "t";
                  break;
                case "PKDateStyleMedium":
                  str2 = "hh:mm:ss tt";
                  break;
                case "PKDateStyleLong":
                  str2 = "hh:mm:ss tt";
                  break;
                case "PKDateStyleFull":
                  str2 = "hh:mm:ss tt";
                  break;
              }
              string str4 = !string.IsNullOrEmpty(str2) ? dateTime2.ToString(str2, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.TimeOfDay.ToString();
              claseField.Value = str3 + " " + str4;
              claseField.Label = current.label;
            }
            else if (!string.IsNullOrEmpty(current.currencyCode) || !string.IsNullOrEmpty(current.numberStyle))
            {
              string format = "";
              if (current.currencyCode != null)
              {
                string currencyCode = current.currencyCode;
                NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
                ClaseCurrency claseCurrency = new ClaseCurrency();
                provider.CurrencySymbol = claseCurrency.getCurrencySymbol(currencyCode);
                claseField.Value = Convert.ToSingle(Convert.ToDouble(current.value, (IFormatProvider) new CultureInfo("en-US"))).ToString("C", (IFormatProvider) provider);
              }
              else
              {
                NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
                switch (current.numberStyle)
                {
                  case "PKNumberStyleNone":
                    format = "N";
                    break;
                  case "PKNumberStyleDecimal":
                    format = "00";
                    break;
                  case "PKNumberStylePercent":
                    format = "P2";
                    break;
                  case "PKNumberStyleScientific":
                    format = "E2";
                    break;
                  case "PKNumberStyleSpellOut":
                    format = "N";
                    break;
                }
                claseField.Value = Convert.ToSingle(Convert.ToDouble(current.value, (IFormatProvider) new CultureInfo("en-US"))).ToString(format, (IFormatProvider) provider);
              }
              claseField.Label = current.label;
            }
            else
            {
              claseField.Label = current.label;
              claseField.Value = current.value;
              claseField.Alignment = current.textAlignment;
            }
            fields.Add(claseField);
          }
        }
        finally
        {
          enumerator.Dispose();
        }
      }
      return fields;
    }

    private async Task formatPassString(string filePassStr, string location)
    {
      try
      {
        this.clsPass.hasStrings = true;
        filePassStr = this.decodeFromUnicode(filePassStr);
        this.clsPass.locationStrings.Add(this.extractStrings(filePassStr, new ClaseStrings(location.Substring(0, location.IndexOf(".")))));
      }
      catch
      {
      }
    }

    private void formatPassImg(byte[] imgBytes, string location)
    {
      try
      {
        this.clsPass.hasStringsImg = true;
        ClaseStringsImg claseStringsImg = (ClaseStringsImg) null;
        int index1 = -1;
        for (int index2 = 0; index2 < this.clsPass.locationStringsImg.Count; ++index2)
        {
          if (this.clsPass.locationStringsImg[index2].Language == location.Substring(0, location.IndexOf(".")))
          {
            claseStringsImg = this.clsPass.locationStringsImg[index2];
            index1 = index2;
            break;
          }
        }
        if (index1 == -1)
        {
          claseStringsImg = new ClaseStringsImg(location.Substring(0, location.IndexOf(".")));
          this.clsPass.locationStringsImg.Add(claseStringsImg);
          index1 = this.clsPass.locationStringsImg.Count - 1;
        }
        string label = !location.Contains("logo") ? (!location.Contains("icon") ? (!location.Contains("footer") ? (!location.Contains("thumbnail") ? (!location.Contains("strip") ? "background" : "strip") : "thumbnail") : "footer") : "icon") : "logo";
        if (location.Contains("2x"))
          claseStringsImg.AddImgToField(label, false, imgBytes);
        else
          claseStringsImg.AddImgToField(label, true, imgBytes);
        this.clsPass.locationStringsImg[index1] = claseStringsImg;
      }
      catch
      {
      }
    }

    private ClaseStrings extractStrings(string filePassStr, ClaseStrings locStrings)
    {
      int startIndex = 0;
      while (filePassStr.Contains("/*"))
        filePassStr = filePassStr.Substring(0, filePassStr.IndexOf("/*")) + filePassStr.Substring(filePassStr.IndexOf("*/") + 2);
      for (int index = filePassStr.IndexOf(";"); index != -1; index = filePassStr.IndexOf(";", startIndex))
      {
        string str = filePassStr.Substring(startIndex, index - startIndex + 1);
        string fromComillas1 = this.extractFromComillas(str.Substring(0, str.IndexOf("=")));
        string fromComillas2 = this.extractFromComillas(str.Substring(str.IndexOf("=") + 1));
        locStrings.AddField(fromComillas1, fromComillas2);
        startIndex = index + 1;
      }
      return locStrings;
    }

    private string extractFromComillas(string text, int indexBegin)
    {
      int num1 = text.IndexOf(",", indexBegin);
      int num2 = text.IndexOf("\"", indexBegin);
      if (num1 != -1)
        return num2 < num1 ? text.Substring(num2 + 1, text.IndexOf("\"", num2 + 1) - 1 - num2) : text.Substring(indexBegin + 1, num1 - indexBegin - 1);
      if (num2 != -1)
        return text.Substring(num2 + 1, text.IndexOf("\"", num2 + 1) - 1 - num2);
      int num3 = text.IndexOf("}", indexBegin);
      return text.Substring(indexBegin + 1, num3 - num2 - 1);
    }

    private string extractFromComillas(string text)
    {
      int num = text.IndexOf("\"");
      return text.Substring(num + 1, text.IndexOf("\"", num + 1) - 1 - num);
    }

    private List<string> extractFromCorchetes(string text, int indexBegin)
    {
      List<string> fromCorchetes = new List<string>();
      string str = text.Substring(indexBegin, this.searchCloseCorchete(text, indexBegin) - (indexBegin - 1));
      int startIndex1;
      for (int startIndex2 = str.IndexOf("{"); startIndex2 != -1; startIndex2 = str.IndexOf("{", startIndex1))
      {
        startIndex1 = str.IndexOf("}", startIndex2);
        fromCorchetes.Add(str.Substring(startIndex2 + 1, startIndex1 - startIndex2));
      }
      return fromCorchetes;
    }

    private int searchCloseCorchete(string text, int indexBegin)
    {
      int num = text.IndexOf("]", indexBegin);
      for (int index = text.IndexOf("[", indexBegin + 1); index != -1 & index < num; index = text.IndexOf("[", index + 1))
        num = text.IndexOf("]", num + 1);
      return num;
    }

    private string extractDictionaries(string filePass, string type)
    {
      int startIndex = filePass.IndexOf(type);
      if (startIndex == -1)
        return "";
      int indexBegin = filePass.IndexOf(":", startIndex);
      return this.extractFromComillas(filePass, indexBegin);
    }

    private DateTime extractDictionariesDate(string filePass, string type)
    {
      int startIndex = filePass.IndexOf(type);
      if (startIndex == -1)
        return Convert.ToDateTime((string) null);
      int indexBegin = filePass.IndexOf(":", startIndex);
      return Convert.ToDateTime(this.extractFromComillas(filePass, indexBegin));
    }

    private string extractPassType(string filePass)
    {
      if (filePass.Contains("boardingPass"))
        return "boardingPass";
      if (filePass.Contains("coupon"))
        return "coupon";
      if (filePass.Contains("eventTicket"))
        return "eventTicket";
      if (filePass.Contains("storeCard"))
        return "storeCard";
      return filePass.Contains("generic") ? "generic" : "";
    }

    private void extractBarcode(
      string filePass,
      ref string barCode,
      ref string altText,
      ref string typeBarCode)
    {
      int startIndex1 = filePass.IndexOf("barcode");
      if (startIndex1 == -1)
        return;
      int startIndex2 = filePass.IndexOf(nameof (altText), startIndex1);
      if (startIndex2 != -1)
      {
        int indexBegin = filePass.IndexOf(":", startIndex2);
        altText = this.extractFromComillas(filePass, indexBegin);
      }
      int startIndex3 = filePass.IndexOf("\"message\"", startIndex1);
      int indexBegin1 = filePass.IndexOf(":", startIndex3);
      barCode = this.extractFromComillas(filePass, indexBegin1);
      int startIndex4 = filePass.IndexOf("format", startIndex1);
      int indexBegin2 = filePass.IndexOf(":", startIndex4);
      typeBarCode = this.extractFromComillas(filePass, indexBegin2);
    }

    private List<ClaseLocations> extractLocations(string filePass)
    {
      List<ClaseLocations> locations = new List<ClaseLocations>();
      List<string> stringList = new List<string>();
      string str1 = "";
      int startIndex = filePass.IndexOf("locations");
      if (startIndex != -1)
      {
        int indexBegin1 = filePass.IndexOf("[", startIndex);
        List<string>.Enumerator enumerator = this.extractFromCorchetes(filePass, indexBegin1).GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            ClaseLocations claseLocations = new ClaseLocations();
            for (int index = current.IndexOf(":", current.IndexOf("latitude")) + 1; current[index] != ',' & current[index] != '}'; ++index)
              str1 += (string) (object) (char) current[index];
            claseLocations.locLatitude = Convert.ToDouble(str1, (IFormatProvider) new CultureInfo("en-US"));
            string str2 = "";
            for (int index = current.IndexOf(":", current.IndexOf("longitude")) + 1; current[index] != ',' & current[index] != '}'; ++index)
              str2 += (string) (object) (char) current[index];
            claseLocations.locLongitude = Convert.ToDouble(str2, (IFormatProvider) new CultureInfo("en-US"));
            str1 = "";
            if (current.IndexOf("relevantText") != -1)
            {
              int indexBegin2 = current.IndexOf(":", current.IndexOf("relevantText"));
              claseLocations.locText = this.extractFromComillas(current, indexBegin2);
            }
            locations.Add(claseLocations);
          }
        }
        finally
        {
          enumerator.Dispose();
        }
      }
      return locations;
    }

    private List<ClaseField> extractFields(string filePass, string zoneField)
    {
      List<ClaseField> fields = new List<ClaseField>();
      List<string> stringList = new List<string>();
      int startIndex = filePass.IndexOf(zoneField);
      if (startIndex != -1)
      {
        int indexBegin1 = filePass.IndexOf("[", startIndex);
        List<string>.Enumerator enumerator = this.extractFromCorchetes(filePass, indexBegin1).GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            string current = enumerator.Current;
            ClaseField claseField = new ClaseField();
            if (current.IndexOf("value") != -1)
            {
              if (current.IndexOf("dateStyle") != -1 | current.IndexOf("timeStyle") != -1)
              {
                DateTime dateTime1 = new DateTime();
                bool flag = false;
                int indexBegin2 = current.IndexOf(":", current.IndexOf("value"));
                DateTime dateTime2 = Convert.ToDateTime(this.extractFromComillas(current, indexBegin2));
                if (current.IndexOf("ignoresTimeZone") != -1)
                {
                  int indexBegin3 = current.IndexOf(":", current.IndexOf("ignoresTimeZone"));
                  if (indexBegin3 != -1)
                    flag = Convert.ToBoolean(Convert.ToInt16(this.extractFromComillas(current, indexBegin3)));
                }
                int indexBegin4 = current.IndexOf(":", current.IndexOf("dateStyle"));
                string str1 = this.extractFromComillas(current, indexBegin4);
                if (!flag)
                  dateTime2 = dateTime2.ToLocalTime();
                switch (str1)
                {
                  case "PKDateStyleNone":
                    str1 = "";
                    break;
                  case "PKDateStyleShort":
                    str1 = "d";
                    break;
                  case "PKDateStyleMedium":
                    str1 = "MMM dd, yyyy";
                    break;
                  case "PKDateStyleLong":
                    str1 = "MMMM dd, yyyy";
                    break;
                  case "PKDateStyleFull":
                    str1 = "dddd, MMMM dd, yyyy gg";
                    break;
                }
                string str2 = !string.IsNullOrEmpty(str1) ? dateTime2.ToString(str1, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.Date.ToString();
                int indexBegin5 = current.IndexOf(":", current.IndexOf("timeStyle"));
                string str3 = this.extractFromComillas(current, indexBegin5);
                switch (str3)
                {
                  case "PKDateStyleNone":
                    str3 = "t";
                    break;
                  case "PKDateStyleShort":
                    str3 = "t";
                    break;
                  case "PKDateStyleMedium":
                    str3 = "hh:mm:ss tt";
                    break;
                  case "PKDateStyleLong":
                    str3 = "hh:mm:ss tt";
                    break;
                  case "PKDateStyleFull":
                    str3 = "hh:mm:ss tt";
                    break;
                }
                string str4 = !string.IsNullOrEmpty(str3) ? dateTime2.ToString(str3, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.TimeOfDay.ToString();
                claseField.Value = str2 + " " + str4;
                if (current.IndexOf("label") != -1)
                {
                  int indexBegin6 = current.IndexOf(":", current.IndexOf("label"));
                  claseField.Label = this.extractFromComillas(current, indexBegin6);
                }
              }
              else if (current.IndexOf("currencyCode") != -1 | current.IndexOf("numberStyle") != -1)
              {
                int indexBegin7 = current.IndexOf(":", current.IndexOf("currencyCode"));
                string fromComillas = this.extractFromComillas(current, indexBegin7);
                if (current.IndexOf("label") != -1)
                {
                  int indexBegin8 = current.IndexOf(":", current.IndexOf("label"));
                  claseField.Label = this.extractFromComillas(current, indexBegin8);
                }
                int indexBegin9 = current.IndexOf(":", current.IndexOf("value"));
                NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
                ClaseCurrency claseCurrency = new ClaseCurrency();
                provider.CurrencySymbol = claseCurrency.getCurrencySymbol(fromComillas);
                claseField.Value = Convert.ToSingle(this.extractFromComillas(current, indexBegin9)).ToString("C", (IFormatProvider) provider);
              }
              else
              {
                if (current.IndexOf("label") != -1)
                {
                  int indexBegin10 = current.IndexOf(":", current.IndexOf("label"));
                  claseField.Label = this.extractFromComillas(current, indexBegin10);
                }
                int indexBegin11 = current.IndexOf(":", current.IndexOf("value"));
                claseField.Value = this.extractFromComillas(current, indexBegin11);
                if (current.IndexOf("textAlignment") != -1)
                {
                  int indexBegin12 = current.IndexOf(":", current.IndexOf("textAlignment"));
                  claseField.Alignment = this.extractFromComillas(current, indexBegin12);
                }
              }
              fields.Add(claseField);
            }
          }
        }
        finally
        {
          enumerator.Dispose();
        }
      }
      return fields;
    }

    private string extractTransitType(string filePass, bool boardingPass)
    {
      if (boardingPass)
      {
        if (filePass.Contains("PKTransitTypeAir"))
          return "PKTransitTypeAir";
        if (filePass.Contains("PKTransitTypeBoat"))
          return "PKTransitTypeBoat";
        if (filePass.Contains("PKTransitTypeBus"))
          return "PKTransitTypeBus";
        if (filePass.Contains("PKTransitTypeGeneric"))
          return "PKTransitTypeGeneric";
        if (filePass.Contains("PKTransitTypeTrain"))
          return "PKTransitTypeTrain";
      }
      return "";
    }

    private bool contain3Bytes0(byte[] bArray)
    {
      int num = 0;
      for (int index = 0; index < bArray.Length - 2; ++index)
      {
        if ((int) bArray[index] == (int) Convert.ToByte(0) && (int) bArray[index + 1] == (int) Convert.ToByte(0) && (int) bArray[index + 2] == (int) Convert.ToByte(0))
          ++num;
        if (num == 5)
          break;
      }
      return num == 5;
    }

    private string extractWrongBytes(string str, byte[] bArray)
    {
      for (int index = 0; index < bArray.Length; ++index)
      {
        if ((int) bArray[index] != (int) Convert.ToByte(0) && (int) bArray[index] != (int) Convert.ToByte(1))
          str += Encoding.UTF8.GetString(bArray, index, 1);
      }
      return str;
    }

    private string decodeFromUnicode(string text)
    {
      text = text.Replace("\\\\", "\\");
      for (int startIndex = text.IndexOf("\\u"); startIndex != -1; startIndex = text.IndexOf("\\u"))
      {
        if (text.Substring(startIndex, 6) == "\\u0022")
        {
          text = text.Remove(startIndex, 6);
          text = text.Insert(startIndex, "'");
        }
        else
          text = text.Replace(text.Substring(startIndex, 6), Convert.ToChar(Convert.ToInt32(text.Substring(startIndex + 2, 4), 16)).ToString());
      }
      text = text.Replace("\0", "");
      text = text.Replace("\\r\\n", Environment.NewLine);
      text = text.Replace("\\n", Environment.NewLine);
      text = text.Replace("\\/", "/");
      text = text.Replace("\\\"", "'");
      text = text.Replace("\\'", "'");
      text = text.Replace("�x", "ß");
      text = text.Replace("�", "");
      text = text.Replace("&lt;", "<");
      text = text.Replace("&gt;", ">");
      text = text.Replace("&amp;", "&");
      return text;
    }

    public static void CopyStream(Stream input, Stream output)
    {
      byte[] buffer = (byte[]) new byte[16384];
      int count;
      while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
        output.Write(buffer, 0, count);
    }
  }
}
