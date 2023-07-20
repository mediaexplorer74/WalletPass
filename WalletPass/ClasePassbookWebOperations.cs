// Decompiled with JetBrains decompiler
// Type: WalletPass.ClasePassbookWebOperations
// Assembly: Wallet Pass, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1E8AC314-47AB-4931-BC36-563B31C55EF5
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Wallet Pass.dll

using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace WalletPass
{
  public static class ClasePassbookWebOperations
  {
    public static async Task<int> updatePassbook(ClasePass clsPass)
    {
      if (NetworkInterface.GetIsNetworkAvailable())
      {
        ClaseNotificationReg reg = new ClaseNotificationReg();
        if (!string.IsNullOrEmpty(clsPass.webServiceURL) && !string.IsNullOrEmpty(reg.registrationID))
        {
          int result = -1;
          ClasePass newClsPass = new ClasePass();
          ClasePassDecryptor clsPassDecrypt = new ClasePassDecryptor();
          MemoryStream updatedPassStream = new MemoryStream();
          try
          {
            HttpClient htp = new HttpClient();
            try
            {
              htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApplePass", clsPass.authenticationToken);
              if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
                clsPass.webServiceURL += "/";
              HttpResponseMessage x = new HttpResponseMessage();
              htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
              x = await htp.GetAsync(string.Format("{0}v1/passes/{1}/{2}", (object) clsPass.webServiceURL, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber));
              if (x.IsSuccessStatusCode)
              {
                Stream httpStream = await x.Content.ReadAsStreamAsync();
                try
                {
                  await httpStream.CopyToAsync((Stream) updatedPassStream);
                  updatedPassStream.Flush();
                }
                finally
                {
                  ((IDisposable) httpStream)?.Dispose();
                }
                result = await clsPassDecrypt.extractCompressedFile((Stream) updatedPassStream, ApplicationData.Current.LocalFolder);
                if (result == 0)
                {
                  newClsPass = new ClasePass(clsPassDecrypt.clsPass, false);
                  try
                  {
                    int index = App._passcollection.swapPass(newClsPass, clsPass.serialNumberGUID);
                    if (index != -1)
                      await ClasePassbookWebOperations.createFile(newClsPass.filenamePass, (Stream) updatedPassStream);
                    ((Collection<ClasePass>) App._passcollection)[App._passcollection.returnIndex(newClsPass.serialNumberGUID)].dateModified = new DateTime(DateTime.Now.Ticks);
                    ((Collection<ClasePass>) App._passcollection)[App._passcollection.returnIndex(newClsPass.serialNumberGUID)].getFrontPass();
                    result = App._passcollection.returnIndex(newClsPass.serialNumberGUID);
                  }
                  catch (Exception ex)
                  {
                    result = -1;
                  }
                }
              }
              else
                result = -1;
            }
            finally
            {
              htp?.Dispose();
            }
          }
          catch (Exception ex)
          {
            result = -1;
          }
          return result;
        }
      }
      return -1;
    }

    public static async Task<ClaseGetSerialsJSON> getSerials(ClasePass clsPass)
    {
      if (NetworkInterface.GetIsNetworkAvailable())
      {
        ClaseNotificationReg reg = new ClaseNotificationReg();
        if (!string.IsNullOrEmpty(clsPass.webServiceURL))
        {
          if (!string.IsNullOrEmpty(reg.registrationID))
          {
            try
            {
              HttpClient htp = new HttpClient();
              HttpResponseMessage x = new HttpResponseMessage();
              if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
                clsPass.webServiceURL += "/";
              htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
              DateTimeToTimestampConverter dateConverter = new DateTimeToTimestampConverter();
              x = await htp.GetAsync(string.Format("{0}v1/devices/{1}/registrations_walletpass/{2}?passesUpdatedSince={3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) Convert.ToString(dateConverter.Convert((object) clsPass.dateModified, (Type) null, (object) null, (CultureInfo) null))));
              if (!x.IsSuccessStatusCode)
                return (ClaseGetSerialsJSON) null;
              MemoryStream serialsJSON = new MemoryStream();
              Stream httpStream = await x.Content.ReadAsStreamAsync();
              try
              {
                await httpStream.CopyToAsync((Stream) serialsJSON);
                serialsJSON.Flush();
              }
              finally
              {
                ((IDisposable) httpStream)?.Dispose();
              }
              StreamReader streamReader = new StreamReader((Stream) serialsJSON);
              string stringSerialsJson = await ((TextReader) streamReader).ReadToEndAsync();
              return JsonConvert.DeserializeObject<ClaseGetSerialsJSON>(stringSerialsJson);
            }
            catch (Exception ex)
            {
              return (ClaseGetSerialsJSON) null;
            }
          }
        }
      }
      return (ClaseGetSerialsJSON) null;
    }

    public static async Task<bool> registerDevice(ClasePass clsPass, bool returnValue = false)
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
        return false;
      ClaseNotificationReg reg = new ClaseNotificationReg();
      if (!string.IsNullOrEmpty(clsPass.webServiceURL) && !string.IsNullOrEmpty(reg.registrationID))
      {
        if (clsPass.webServiceURL.Contains("passcreator"))
        {
          try
          {
            HttpClient htp = new HttpClient();
            htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WalletPass", clsPass.authenticationToken);
            if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
              clsPass.webServiceURL += "/";
            HttpResponseMessage x = new HttpResponseMessage();
            ClaseRegisterDeviceJSON payload = new ClaseRegisterDeviceJSON()
            {
              pushToken = reg.registrationID,
              pushServiceUrl = reg.notificationURL
            };
            string postBody = JsonConvert.SerializeObject((object) payload);
            x = await htp.PostAsync(string.Format("{0}v1/devices/{1}/registrations_walletpass/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
            if (x.IsSuccessStatusCode)
            {
              if (returnValue)
                return true;
              int index = App._passcollection.returnIndex(clsPass.serialNumberGUID);
              if (index != -1)
              {
                ((Collection<ClasePass>) App._passcollection)[index].registered = true;
                WalletPass.IO.SaveData(App._passcollection);
              }
            }
          }
          catch (Exception ex)
          {
            return false;
          }
        }
        else
        {
          try
          {
            HttpClient htp = new HttpClient();
            htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WalletPass", clsPass.authenticationToken);
            htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
            if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
              clsPass.webServiceURL += "/";
            HttpResponseMessage x = new HttpResponseMessage();
            ClaseRegisterDeviceLegacyJSON payload = new ClaseRegisterDeviceLegacyJSON()
            {
              pushToken = reg.registrationID
            };
            string postBody = JsonConvert.SerializeObject((object) payload);
            x = await htp.PostAsync(string.Format("{0}v1/devices/{1}/registrations/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
            if (x.IsSuccessStatusCode)
            {
              if (returnValue)
                return true;
              int index = App._passcollection.returnIndex(clsPass.serialNumberGUID);
              if (index != -1)
              {
                ((Collection<ClasePass>) App._passcollection)[index].registered = true;
                WalletPass.IO.SaveData(App._passcollection);
              }
            }
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    public static async Task<bool> unregisterDevice(ClasePass clsPass)
    {
      if (!NetworkInterface.GetIsNetworkAvailable())
        return false;
      ClaseNotificationReg reg = new ClaseNotificationReg();
      if (!string.IsNullOrEmpty(clsPass.webServiceURL) && !string.IsNullOrEmpty(reg.registrationID))
      {
        if (clsPass.webServiceURL.Contains("passcreator"))
        {
          try
          {
            HttpClient htp = new HttpClient();
            htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WalletPass", clsPass.authenticationToken);
            if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
              clsPass.webServiceURL += "/";
            HttpResponseMessage x = new HttpResponseMessage();
            x = await htp.DeleteAsync(string.Format("{0}v1/devices/{1}/registrations_walletpass/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber));
            if (x.IsSuccessStatusCode)
              return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
        else
        {
          try
          {
            HttpClient htp = new HttpClient();
            htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WalletPass", clsPass.authenticationToken);
            htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
            if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
              clsPass.webServiceURL += "/";
            HttpResponseMessage x = new HttpResponseMessage();
            x = await htp.DeleteAsync(string.Format("{0}v1/devices/{1}/registrations/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber));
            if (x.IsSuccessStatusCode)
              return true;
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return false;
    }

    public static async Task<bool> pushMSWallet(string organizationName, string webServiceURL)
    {
      if (NetworkInterface.GetIsNetworkAvailable())
      {
        ClaseNotificationReg reg = new ClaseNotificationReg();
        try
        {
          HttpClient htp = new HttpClient();
          HttpResponseMessage x = new HttpResponseMessage();
          ClaseMSWalletPushJSON payload = new ClaseMSWalletPushJSON()
          {
            pushToken = reg.registrationID,
            organizationName = organizationName,
            webServiceURL = webServiceURL,
            language = CultureInfo.CurrentCulture.Name
          };
          string postBody = JsonConvert.SerializeObject((object) payload);
          x = await htp.PostAsync(string.Format("{0}api/mswalletupdate", (object) reg.notificationURL), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
          if (x.IsSuccessStatusCode)
            return true;
        }
        catch (Exception ex)
        {
          return false;
        }
      }
      return false;
    }

    private static async Task createFile(string fileName, Stream streamFile)
    {
      try
      {
        StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(ApplicationData.Current.LocalFolder.Path + "\\savedPasses\\");
        StorageFile file = await localFolder.CreateFileAsync(fileName, (CreationCollisionOption) 1);
        streamFile.Position = 0L;
        DataReader reader = new DataReader(streamFile.AsInputStream());
        byte[] buffer = (byte[]) new byte[streamFile.Length];
        int num = (int) await (IAsyncOperation<uint>) reader.LoadAsync((uint) streamFile.Length);
        reader.ReadBytes(buffer);
        await FileIO.WriteBytesAsync((IStorageFile) file, buffer);
        reader.Dispose();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        streamFile.Close();
        streamFile.Dispose();
      }
    }
  }
}
