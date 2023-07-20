// WalletPass.NotificationHub

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace WalletPass
{
  internal class NotificationHub
  {
    private const string ApiVersion = "?api-version=2014-09";
    private const string AuthHeader = "Authorization";
    private const string ContentType = "application/atom+xml;type=entry;charset=utf-8";

    private static string HubName { get; set; }

    private static string ConnectionString { get; set; }

    private static string RegistrationID { get; set; }

    private static string Endpoint { get; set; }

    private static string SasKeyName { get; set; }

    private static string SasKeyValue { get; set; }

    private static string Payload { get; set; }

    private static string PushChannel { get; set; }

    private static string Tag { get; set; }

    public NotificationHub(string hubName, string connectionString, string registrationID)
    {
      NotificationHub.HubName = hubName;
      NotificationHub.ConnectionString = connectionString;
      NotificationHub.RegistrationID = registrationID;
    }

    public void Register(string pushChannel)
    {
      NotificationHub.PushChannel = pushChannel;
      NotificationHub.ParseConnectionInfo();
      if (string.IsNullOrEmpty(NotificationHub.RegistrationID))
        NotificationHub.SendNHRegistrationIDRequest(NotificationHub.PushChannel);
      else
        NotificationHub.SendNHRegistrationRequest(NotificationHub.PushChannel, 
            NotificationHub.RegistrationID);
    }

    private static void ParseConnectionInfo()
    {
      if (string.IsNullOrWhiteSpace(NotificationHub.HubName))
        throw new InvalidOperationException("Hub name is empty");
      string[] strArray = NotificationHub.ConnectionString.Split(new string[1]
      {
        ";"
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length != 3)
        throw new InvalidOperationException("Error parsing connection string: " 
            + NotificationHub.ConnectionString);
      foreach (string str in strArray)
      {
        if (str.StartsWith("Endpoint"))
          NotificationHub.Endpoint = "https" + str.Substring(11);
        else if (str.StartsWith("SharedAccessKeyName"))
          NotificationHub.SasKeyName = str.Substring(20);
        else if (str.StartsWith("SharedAccessKey"))
          NotificationHub.SasKeyValue = str.Substring(16);
      }
    }

    private static string GenerateSaSToken(Uri uri)
    {
      string lower = WebUtility.UrlEncode(uri.ToString().ToLower()).ToLower();
      long num = Convert.ToInt64(DateTime.UtcNow.Subtract(
          new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds) + 3600L;
      string s = lower + "\n" + (object) num;

      HMACSHA256 hmacshA256 = new HMACSHA256(Encoding.UTF8.GetBytes(
          NotificationHub.SasKeyValue));
      hmacshA256.Initialize();
      string str = WebUtility.UrlEncode(
          Convert.ToBase64String(hmacshA256.ComputeHash(Encoding.UTF8.GetBytes(s))));
      return "SharedAccessSignature sr=" + lower + "&sig=" + str +
                "&se=" + (object) num + "&skn=" + NotificationHub.SasKeyName;
    }

    private static void SendNHRegistrationIDRequest(string pushChannel)
    {
      Uri uri = new Uri(NotificationHub.Endpoint + NotificationHub.HubName +
          "/registrationIDs/?api-version=2014-09");
      HttpWebRequest http = WebRequest.CreateHttp(uri);
      http.Method = "POST";
      http.ContentType = "application/atom+xml;type=entry;charset=utf-8";
      http.Headers["Authorization"] = NotificationHub.GenerateSaSToken(uri);
      http.BeginGetResponse(new AsyncCallback(NotificationHub.GetRegistrationIDCallback),
          (object) http);
    }

    private static void GetRegistrationIDCallback(IAsyncResult result)
    {
      if (!(result.AsyncState is HttpWebRequest asyncState))
        return;
      try
      {
        HttpWebResponse response = (HttpWebResponse) asyncState.EndGetResponse(result);
        if (response.StatusCode != HttpStatusCode.Created)
          return;
        WebHeaderCollection headers = response.Headers;
        foreach (string name in (IEnumerable) headers)
        {
          if (name == "Location")
          {
            string str = headers[name];
            NotificationHub.RegistrationID = str.Substring(str.IndexOf("registrationIDs/") + 16, str.IndexOf("?api") - (str.IndexOf("registrationIDs/") + 16));
            NotificationHub.SendNHRegistrationRequest(NotificationHub.PushChannel, NotificationHub.RegistrationID);
            WalletPass.IO.SaveDataNotificationReg(NotificationHub.RegistrationID);
          }
        }
      }
      catch (WebException ex)
      {
         Debug.WriteLine("[ex] GetRegistrationIDCallback Exception: " + ex.Message);
      }
    }

    private static void SendNHRegistrationRequest(string pushChannel, string registrationID)
    {
      try
      {
        NotificationHub.Payload = 
        "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
        "  <entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n" +
        "  <content type=\"application/xml\">\r\n  " +
        "  <WindowsRegistrationDescription xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" " +
        "xmlns=\"http://schemas.microsoft.com/netservices/2010/10/servicebus/connect\">\r\n " +
        "<Tags>{DeviceTag}</Tags>\r\n   " +
        "  <ChannelUri>{WindowsPushChannel}</ChannelUri>\r\n " +
        " </WindowsRegistrationDescription >\r\n        " +
        "  </content>\r\n       " +
        " </entry>";
       
        NotificationHub.Payload =
            NotificationHub.Payload.Replace("{WindowsPushChannel}", pushChannel);
        NotificationHub.Payload = 
                    NotificationHub.Payload.Replace("{DeviceTag}", registrationID);

        Uri uri = new Uri(NotificationHub.Endpoint + NotificationHub.HubName + "/registrations/" + registrationID + "?api-version=2014-09");
        HttpWebRequest http = WebRequest.CreateHttp(uri);
        http.Method = "PUT";
        http.ContentType = "application/atom+xml;type=entry;charset=utf-8";
        http.Headers["Authorization"] = NotificationHub.GenerateSaSToken(uri);
        http.BeginGetRequestStream(new AsyncCallback(NotificationHub.GetRequestStreamCallback), (object) http);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("[ex] SendNHRegistrationRequest Exception: " + ex.Message);
      }
    }

    private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
    {
      HttpWebRequest asyncState = (HttpWebRequest) asynchronousResult.AsyncState;
      Stream requestStream = asyncState.EndGetRequestStream(asynchronousResult);
      byte[] bytes = Encoding.UTF8.GetBytes(NotificationHub.Payload);
      requestStream.Write(bytes, 0, bytes.Length);

            requestStream.Dispose();//.Close();
      asyncState.BeginGetResponse(new AsyncCallback(
          NotificationHub.GetResponceStreamCallback), (object) asyncState);
    }

    private static void GetResponceStreamCallback(IAsyncResult callbackResult)
    {
      try
      {
        using (StreamReader streamReader = new StreamReader(
            ((WebRequest) callbackResult.AsyncState).EndGetResponse(callbackResult)
            .GetResponseStream()))
          streamReader.ReadToEnd();
      }
      catch (Exception ex)
      {
         Debug.WriteLine("[ex] Exception : " + ex.Message);
      }
    }
  }
}
