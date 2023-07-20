// Decompiled with JetBrains decompiler
// Type: Wallet_Pass.ClasePassbookWebOperations
// Assembly: ClassesRT, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4763AD48-8D96-449E-A89B-9A47C90F0CDD
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\ClassesRT.dll

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Wallet_Pass
{
  public static class ClasePassbookWebOperations
  {
    public static async Task<ClasePassBackgroundTask> updatePassbook(
      ClasePassBackgroundTask clsPass,
      ClasePassCollection passCollection)
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
            htp.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApplePass", clsPass.authenticationToken);
            if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
              clsPass.webServiceURL += "/";
            htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
            HttpResponseMessage x = new HttpResponseMessage();
            x = AsyncHelpers.RunSync<HttpResponseMessage>((Func<Task<HttpResponseMessage>>) (async () => await htp.GetAsync(string.Format("{0}v1/passes/{1}/{2}", new object[3]
            {
              (object) clsPass.webServiceURL,
              (object) clsPass.passTypeIdentifier,
              (object) clsPass.serialNumber
            }), HttpCompletionOption.ResponseHeadersRead)));
            if (x.IsSuccessStatusCode)
            {
              using (Stream httpStream = await x.Content.ReadAsStreamAsync())
              {
                AsyncHelpers.RunSync((Func<Task>) (async () => await httpStream.CopyToAsync((Stream) updatedPassStream)));
                updatedPassStream.Flush();
              }
              result = AsyncHelpers.RunSync<int>((Func<Task<int>>) (async () => await clsPassDecrypt.extractCompressedFile((Stream) updatedPassStream, ApplicationData.Current.LocalFolder)));
              if (result == 0)
              {
                newClsPass = new ClasePass(clsPassDecrypt.clsPass);
                try
                {
                  newClsPass.isUpdated = true;
                  Func<Task<int>> task = (Func<Task<int>>) (async () => await passCollection.swapPass(newClsPass, clsPass.serialNumberGUID));
                  if (AsyncHelpers.RunSync<int>(task) != -1)
                    AsyncHelpers.RunSync((Func<Task>) (async () => await ClasePassbookWebOperations.createFile(newClsPass.filenamePass, (Stream) updatedPassStream)));
                  Wallet_Pass.IO.SaveData(passCollection);
                  clsPass.dateModified = new DateTime(DateTime.Now.Ticks);
                  clsPass.isUpdated = true;
                  return clsPass;
                }
                catch (Exception ex)
                {
                }
              }
            }
          }
          catch (Exception ex)
          {
          }
          return (ClasePassBackgroundTask) null;
        }
      }
      return (ClasePassBackgroundTask) null;
    }

    public static async Task<ClaseGetSerialsJSON> getSerials(ClasePassBackgroundTask clsPass)
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
              using (HttpClient htp = new HttpClient())
              {
                HttpResponseMessage x = new HttpResponseMessage();
                if (clsPass.webServiceURL.Substring(clsPass.webServiceURL.Length - 1, 1) != "/")
                  clsPass.webServiceURL += "/";
                DateTimeToTimestampConverter timestampConverter = new DateTimeToTimestampConverter();
                string timestamp = "";
                htp.DefaultRequestHeaders.UserAgent.ParseAdd(reg.userAgent);
                if (!string.IsNullOrEmpty(clsPass.sinceUpdate))
                  timestamp = "?passesUpdatedSince=" + clsPass.sinceUpdate;
                x = AsyncHelpers.RunSync<HttpResponseMessage>((Func<Task<HttpResponseMessage>>) (async () => await htp.GetAsync(string.Format("{0}v1/devices/{1}/registrations/{2}{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) timestamp), HttpCompletionOption.ResponseHeadersRead)));
                if (x.IsSuccessStatusCode)
                {
                  MemoryStream serialsJSON = new MemoryStream();
                  using (Stream httpStream = await x.Content.ReadAsStreamAsync())
                  {
                    await httpStream.CopyToAsync((Stream) serialsJSON);
                    serialsJSON.Flush();
                  }
                  byte[] serialsByteJSON = serialsJSON.ToArray();
                  string stringSerialsJson = Encoding.UTF8.GetString(serialsByteJSON, 0, serialsByteJSON.Length);
                  return JsonConvert.DeserializeObject<ClaseGetSerialsJSON>(stringSerialsJson);
                }
                if (x.StatusCode == HttpStatusCode.NoContent)
                  return (ClaseGetSerialsJSON) null;
                return new ClaseGetSerialsJSON()
                {
                  lastUpdated = "Not implemented"
                };
              }
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

    public static async Task<bool> registerDevice(ClasePass clsPass)
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
              pushServiceURL = reg.notificationURL
            };
            string postBody = JsonConvert.SerializeObject((object) payload);
            x = await htp.PostAsync(string.Format("{0}v1/devices/{1}/registrations_walletpass/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
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
            ClaseRegisterDeviceLegacyJSON payload = new ClaseRegisterDeviceLegacyJSON()
            {
              pushToken = reg.registrationID
            };
            string postBody = JsonConvert.SerializeObject((object) payload);
            x = await htp.PostAsync(string.Format("{0}v1/devices/{1}/registrations/{2}/{3}", (object) clsPass.webServiceURL, (object) reg.registrationID, (object) clsPass.passTypeIdentifier, (object) clsPass.serialNumber), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
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
            webServiceURL = webServiceURL
          };
          string postBody = JsonConvert.SerializeObject((object) payload);
          x = await htp.PostAsync(string.Format("{0}api/mswalletupdate", new object[1]
          {
            (object) reg.notificationURL
          }), (HttpContent) new StringContent(postBody, Encoding.UTF8, "application/json"));
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
        byte[] buffer = new byte[streamFile.Length];
        int num = (int) await (IAsyncOperation<uint>) reader.LoadAsync((uint) streamFile.Length);
        reader.ReadBytes(buffer);
        AsyncHelpers.RunSync((Func<Task>) (async () => await FileIO.WriteBytesAsync((IStorageFile) file, buffer)));
        reader.Dispose();
      }
      catch (Exception ex)
      {
      }
      finally
      {
        streamFile.Dispose();
      }
    }
  }
}
