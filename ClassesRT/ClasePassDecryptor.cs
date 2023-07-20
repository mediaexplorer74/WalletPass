// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassDecryptor
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI;

namespace Wallet_Pass
{
  public class ClasePassDecryptor
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
      try
      {
        fileStream.Position = 0L;
        StorageFolder dataFolder = await local.CreateFolderAsync("temp", (CreationCollisionOption) 3);
        using (ZipArchive archive = new ZipArchive(fileStream))
        {
          foreach (ZipArchiveEntry entry in archive.Entries)
          {
            if (entry.FullName.LastIndexOf("/") != entry.FullName.Length - 1)
            {
              string filePath = entry.FullName;
              if (filePath.Contains("/"))
                filePath = filePath.Substring(filePath.LastIndexOf("/") + 1);
              StorageFile file = await dataFolder.CreateFileAsync(filePath, (CreationCollisionOption) 1);
              if (entry.FullName.Contains("png") && entry.FullName.Contains("/"))
              {
                Stream stream = await ((IStorageFile) file).OpenStreamForWriteAsync();
                MemoryStream streamEntry = new MemoryStream();
                entry.Open().CopyTo((Stream) streamEntry);
                if (streamEntry.Length != 0L)
                  this.formatPassImg(streamEntry.ToArray(), entry.FullName);
              }
              else if (entry.FullName.Contains("png"))
              {
                Stream stream = await ((IStorageFile) file).OpenStreamForWriteAsync();
                MemoryStream streamEntry = new MemoryStream();
                entry.Open().CopyTo((Stream) streamEntry);
                if (streamEntry.Length > 0L)
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
              else if (entry.FullName.Equals("pass.strings", StringComparison.OrdinalIgnoreCase))
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
        if (this.clsPass.backgroundB != null || this.clsPass.background2B != null)
        {
          this.clsPass.foregroundColor = Colors.White;
          this.clsPass.labelColor = Colors.White;
        }
        await dataFolder.DeleteAsync((StorageDeleteOption) 1);
        return 0;
      }
      catch
      {
        return -1;
      }
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
      this.clsPass.strBackgroundColor = this.extractColor(tempPass.backgroundColor, "#FF000000");
      this.clsPass.strForegroundColor = this.extractColor(tempPass.foregroundColor, tempPass.backgroundColor);
      this.clsPass.strLabelColor = this.extractColor(tempPass.labelColor, tempPass.backgroundColor);
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

    private string extractColor(string strColor, string antColor)
    {
      byte num1 = 0;
      byte num2 = 0;
      byte num3 = 0;
      if (strColor != null)
      {
        if (strColor.Contains("rgba"))
        {
          int num4 = 0;
          byte num5 = Convert.ToByte(strColor.Substring(num4 + 5, strColor.IndexOf(",") - (num4 + 5)));
          int startIndex1 = strColor.IndexOf(",") + 1;
          byte num6 = Convert.ToByte(strColor.Substring(startIndex1, strColor.IndexOf(",", startIndex1) - startIndex1));
          int startIndex2 = strColor.IndexOf(",", startIndex1) + 1;
          byte num7 = Convert.ToByte(strColor.Substring(startIndex2, strColor.IndexOf(",", startIndex2) - startIndex2));
          int startIndex3 = strColor.IndexOf(",", startIndex2) + 1;
          return "#" + Convert.ToByte(Convert.ToInt32(Convert.ToDouble(strColor.Substring(startIndex3, strColor.IndexOf(")", startIndex3) - startIndex3), (IFormatProvider) new CultureInfo("en")) * (double) byte.MaxValue)).ToString("X2") + num5.ToString("X2") + num6.ToString("X2") + num7.ToString("X2");
        }
        if (strColor.Contains("rgb"))
        {
          int num8 = 0;
          byte num9 = Convert.ToByte(strColor.Substring(num8 + 4, strColor.IndexOf(",") - (num8 + 4)));
          int startIndex4 = strColor.IndexOf(",") + 1;
          byte num10 = Convert.ToByte(strColor.Substring(startIndex4, strColor.IndexOf(",", startIndex4) - startIndex4));
          int startIndex5 = strColor.IndexOf(",", startIndex4) + 1;
          byte num11 = Convert.ToByte(strColor.Substring(startIndex5, strColor.IndexOf(")", startIndex5) - startIndex5));
          return "#FF" + num9.ToString("X2") + num10.ToString("X2") + num11.ToString("X2");
        }
        if (strColor.Contains("#"))
          return "#FF" + strColor.Substring(1);
      }
      return num1 > (byte) 210 & num2 > (byte) 210 & num3 > (byte) 210 ? "#FF000000" : "#FFFFFFFF";
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
        foreach (Location location in tempPass.locations)
        {
          ClaseLocations claseLocations = new ClaseLocations(Convert.ToDouble((object) location.latitude, (IFormatProvider) new CultureInfo("en-US")), Convert.ToDouble((object) location.longitude, (IFormatProvider) new CultureInfo("en-US")), location.relevantText, location.altitude);
          locations.Add(claseLocations);
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
        foreach (passField passField in zoneField)
        {
          ClaseField claseField = new ClaseField();
          if (!string.IsNullOrEmpty(passField.dateStyle) | !string.IsNullOrEmpty(passField.timeStyle))
          {
            DateTime dateTime1 = new DateTime();
            string format1 = "";
            string format2 = "";
            DateTime dateTime2 = Convert.ToDateTime(passField.value);
            if (!passField.ignoresTimeZone)
              dateTime2 = dateTime2.ToLocalTime();
            switch (passField.dateStyle)
            {
              case "PKDateStyleNone":
                format1 = "";
                break;
              case "PKDateStyleShort":
                format1 = "d";
                break;
              case "PKDateStyleMedium":
                format1 = "MMM dd, yyyy";
                break;
              case "PKDateStyleLong":
                format1 = "MMMM dd, yyyy";
                break;
              case "PKDateStyleFull":
                format1 = "dddd, MMMM dd, yyyy gg";
                break;
            }
            string str1 = !string.IsNullOrEmpty(format1) ? dateTime2.ToString(format1, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.Date.ToString();
            switch (passField.timeStyle)
            {
              case "PKDateStyleNone":
                format2 = "t";
                break;
              case "PKDateStyleShort":
                format2 = "t";
                break;
              case "PKDateStyleMedium":
                format2 = "hh:mm:ss tt";
                break;
              case "PKDateStyleLong":
                format2 = "hh:mm:ss tt";
                break;
              case "PKDateStyleFull":
                format2 = "hh:mm:ss tt";
                break;
            }
            string str2 = !string.IsNullOrEmpty(format2) ? dateTime2.ToString(format2, (IFormatProvider) CultureInfo.CurrentCulture) : dateTime2.TimeOfDay.ToString();
            claseField.Value = str1 + " " + str2;
            claseField.Label = passField.label;
          }
          else if (!string.IsNullOrEmpty(passField.currencyCode) | !string.IsNullOrEmpty(passField.numberStyle))
          {
            string format = "";
            if (passField.currencyCode != null)
            {
              string currencyCode = passField.currencyCode;
              NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
              ClaseCurrency claseCurrency = new ClaseCurrency();
              provider.CurrencySymbol = claseCurrency.getCurrencySymbol(currencyCode);
              claseField.Value = Convert.ToSingle(Convert.ToDouble(passField.value, (IFormatProvider) new CultureInfo("en-US"))).ToString("C", (IFormatProvider) provider);
            }
            else
            {
              NumberFormatInfo provider = (NumberFormatInfo) CultureInfo.CurrentCulture.NumberFormat.Clone();
              switch (passField.numberStyle)
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
              claseField.Value = Convert.ToSingle(Convert.ToDouble(passField.value, (IFormatProvider) new CultureInfo("en-US"))).ToString(format, (IFormatProvider) provider);
            }
            claseField.Label = passField.label;
          }
          else
          {
            claseField.Label = passField.label;
            claseField.Value = passField.value;
            claseField.Alignment = passField.textAlignment;
          }
          fields.Add(claseField);
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
      for (int startIndex = text.IndexOf("\\u"); startIndex != -1; startIndex = text.IndexOf("\\u"))
      {
        if (text.Substring(startIndex, 6) == "\\u0022")
        {
          text = text.Remove(startIndex, 6);
          text = text.Insert(startIndex, "'");
        }
        else
          text = text.Replace(text.Substring(startIndex - 1, 7), Convert.ToChar(Convert.ToInt32(text.Substring(startIndex + 2, 4), 16)).ToString());
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
  }
}
