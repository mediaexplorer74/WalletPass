// Microsoft.WindowsAzure.Messaging.Http.ServiceBusClient

using Microsoft.WindowsAzure.Messaging.Http.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal sealed class ServiceBusClient : IDisposable
  {
    private HttpChannel _channel;

    private ServiceConfiguration ServiceConfig { get; set; }

    public ServiceBusClient(string serviceNamespace, string issuerName, string issuerPassword)
    {
      Validator.ArgumentIsValidPath(nameof (serviceNamespace), serviceNamespace);
      Validator.ArgumentIsNotNullOrEmptyString(nameof (issuerName), issuerName);
      Validator.ArgumentIsNotNull(nameof (issuerPassword), (object) issuerPassword);
      this.ServiceConfig = new ServiceConfiguration(new Uri(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "https://{0}.servicebus.windows.net/", (object) serviceNamespace), UriKind.Absolute));
      this._channel = HttpChannel.CreateWithWrapAuthentication(serviceNamespace, issuerName, issuerPassword);
    }

    public ServiceBusClient(string connectionString, params IHttpHandler[] additionalHandlers)
    {
      Validator.ArgumentIsNotNullOrEmptyString(nameof (connectionString), connectionString);
      Dictionary<string, string> connectionString1 = ServiceBusClient.ParseConnectionString(nameof (connectionString), connectionString);
      string str = connectionString1["Endpoint"];
      UriBuilder uriBuilder = !string.IsNullOrEmpty(str) ? new UriBuilder(str) : throw ServiceBusClient.CreateConnectionStringException(
          nameof (connectionString), "ErrorMissingServiceBusKey", (object) "Endpoint");
      uriBuilder.Scheme = "https";
      if (connectionString1.ContainsKey("SharedAccessKeyName"))
        this._channel = HttpChannel.CreateWithSharedAccessSecretAuthentication(connectionString1["SharedAccessKeyName"],
            connectionString1["SharedAccessKey"], additionalHandlers);

      else if (!connectionString1.ContainsKey("StsEndpoint"))
      {
        string serviceNamespace = ServiceBusClient.GetServiceNamespace(uriBuilder.Uri.AbsoluteUri);
        if (string.IsNullOrEmpty(serviceNamespace))
          throw ServiceBusClient.CreateConnectionStringException(nameof (connectionString), 
              "ErrorInvalidEndpoint", (object) str);

        this._channel = HttpChannel.CreateWithWrapAuthentication(serviceNamespace, 
            connectionString1["SharedSecretIssuer"]
            , connectionString1["SharedSecretValue"], additionalHandlers);
      }
      else
      {
        string stsEndpoint = connectionString1["StsEndpoint"];
        this._channel = HttpChannel.CreateWithWrapAuthentication(str, stsEndpoint, connectionString1["SharedSecretIssuer"], connectionString1["SharedSecretValue"], additionalHandlers);
      }
      this.ServiceConfig = new ServiceConfiguration(uriBuilder.Uri);
    }

    private static Dictionary<string, string> ParseConnectionString(
      string argumentName,
      string connectionString)
    {
      Dictionary<string, string> connectionString1 = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (KeyValuePair<string, string> keyValuePair in ConnectionStringParser.Parse(argumentName, connectionString))
        connectionString1[keyValuePair.Key] = keyValuePair.Value;
      string str = (string) null;
      if (!connectionString1.ContainsKey("Endpoint"))
        str = "Endpoint";
      else if (!connectionString1.ContainsKey("SharedSecretIssuer") && connectionString1.ContainsKey("SharedSecretValue"))
        str = "SharedSecretIssuer";
      else if (connectionString1.ContainsKey("SharedSecretIssuer") && !connectionString1.ContainsKey("SharedSecretValue"))
        str = "SharedSecretValue";
      else if (!connectionString1.ContainsKey("SharedAccessKeyName") && connectionString1.ContainsKey("SharedAccessKey"))
        str = "SharedAccessKeyName";
      else if (connectionString1.ContainsKey("SharedAccessKeyName") && !connectionString1.ContainsKey("SharedAccessKey"))
        str = "SharedAccessKey";
      if (str != null)
        throw ServiceBusClient.CreateConnectionStringException(argumentName, "ErrorMissingServiceBusKey",
            (object) str);
      return connectionString1;
    }

    public ServiceBusClient(Uri endPoint, params IHttpHandler[] handlers)
    {
      Validator.ArgumentIsNotNull(nameof (endPoint), (object) endPoint);
      Validator.ArgumentIsNotNull(nameof (handlers), (object) handlers);
      Uri uri = endPoint;
      if (!endPoint.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
        uri = new UriBuilder(endPoint) { Scheme = "https" }.Uri;
      this.ServiceConfig = new ServiceConfiguration(uri);
      this._channel = new HttpChannel(handlers);
    }

    public ServiceBusClient AddHandlers(params IHttpHandler[] handlers)
    {
      Validator.ArgumentIsNotNull(nameof (handlers), (object) handlers);
      return new ServiceBusClient(this, handlers);
    }

    private ServiceBusClient(ServiceBusClient client, params IHttpHandler[] handlers)
    {
      Validator.ArgumentIsNotNull(nameof (client), (object) client);
      Validator.ArgumentIsNotNull(nameof (handlers), (object) handlers);
      this.ServiceConfig = client.ServiceConfig;
      this._channel = new HttpChannel(client._channel, handlers);
    }

    public IAsyncOperation<string> CreateRegistrationIdAsync(string notificationHubPath) => WindowsRuntimeSystemExtensions.AsAsyncOperation<string>(this._channel.SendAsyncInternal(new HttpRequest("POST", this.ServiceConfig.CreateRegistrationId(notificationHubPath).AddApiVersion())
    {
      ContentType = "application/atom+xml;type=entry;charset=utf-8",
      ContentLength = 0L,
      RaiseExceptionForNotFound = true
    }).ContinueWith<string>((Func<Task<HttpResponse>, string>) (tr => this.GetRegistrationId(tr.Result))));

    public IAsyncOperation<T> CreateOrUpdateRegistrationAsync<T>(T registration) where T : Registration
    {
      if (string.IsNullOrEmpty(registration.RegistrationId))
        throw new ArgumentNullException("RegistrationId");
      Uri registrationUri = this.ServiceConfig.GetRegistrationUri(registration.NotificationHubPath, registration.RegistrationId);
      registration.RegistrationId = (string) null;
      registration.ExpiresAt = new DateTime?();
      registration.ETag = (string) null;
      return this.CreateItemAsync<T, T>(registrationUri, registration);
    }

    public IAsyncOperation<T> UpdateRegistrationAsync<T>(T registration) where T : Registration
    {
      if (string.IsNullOrEmpty(registration.RegistrationId))
        throw new ArgumentNullException("RegistrationId");
      if (string.IsNullOrEmpty(registration.ETag))
        throw new ArgumentNullException("ETag");
      Uri registrationUri = this.ServiceConfig.GetRegistrationUri(registration.NotificationHubPath, registration.RegistrationId);
      registration.RegistrationId = (string) null;
      registration.ExpiresAt = new DateTime?();
      return this.CreateItemAsync<T, T>(registrationUri, registration);
    }

    public async Task<ListWithContinuation<Registration>> ListRegistrationsAsync(
      string notificationHubPath,
      string channelUri,
      int top = 0,
      string continuationToken = null)
    {
      Uri sendLocation = this.ServiceConfig.GetListRegistrationsUri(notificationHubPath, channelUri);
      return await this.GetItemsAsync<Registration>(sendLocation, top, continuationToken);
    }

    public IAsyncOperation<Registration> GetRegistrationAsync(
      string notificationHubPath,
      string registrationId)
    {
      return this.GetItemAsync<Registration>(this.ServiceConfig.GetRegistrationUri(notificationHubPath, registrationId), false);
    }

    public IAsyncAction DeleteRegistrationAsync(
      string notificationHubPath,
      string registrationName,
      string ETag)
    {
      return this.DeleteItemAsync(this.ServiceConfig.GetRegistrationUri(notificationHubPath, registrationName), ETag);
    }

    public IAsyncOperation<IEnumerable<Registration>> UpdateChannelUriAsync(
      string notificationHubPath,
      string originalChannelUri,
      string newChannelUri)
    {
      Uri uri = this.ServiceConfig.GetUpdateChannelUriPath(notificationHubPath).AddApiVersion();
      UpdateChannelUriPayload channelUriPayload = new UpdateChannelUriPayload()
      {
        OriginalChannelUri = originalChannelUri,
        NewChannelUri = newChannelUri
      };
      return WindowsRuntimeSystemExtensions.AsAsyncOperation<IEnumerable<Registration>>(this._channel.SendAsyncInternal(new HttpRequest("POST", uri)
      {
        ContentType = "application/json",
        Content = SerializationHelper.JsonSerialize((object) channelUriPayload, (Type[]) null),
        RaiseExceptionForNotFound = false
      }, (Func<HttpResponse, HttpResponse>[]) null).ContinueWith<IEnumerable<Registration>>((Func<Task<HttpResponse>, IEnumerable<Registration>>) (r => this.GetItems<Registration>(r.Result))));
    }

    private async Task<ListWithContinuation<TInfo>> GetItemsAsync<TInfo>(
      Uri containerUri,
      int top,
      string continuationToken,
      params Type[] extraTypes)
    {
      Uri targetUri = containerUri.AddTop(top).AddContinuationToken(continuationToken).AddApiVersion();
      HttpRequest request = new HttpRequest("GET", targetUri)
      {
        RaiseExceptionForNotFound = false
      };
      HttpResponse response = await this._channel.SendAsyncInternal(request);
      string nextContinuationToken = (string) null;
      response.Headers.TryGetValue("x-ms-continuationtoken", out nextContinuationToken);
      return new ListWithContinuation<TInfo>(this.GetItems<TInfo>(response, extraTypes), nextContinuationToken);
    }

    private IEnumerable<TInfo> GetItems<TInfo>(HttpResponse response, params Type[] extraTypes)
    {
      if (response.IsNotFoundStatusCode && !response.Request.RaiseExceptionForNotFound)
        return (IEnumerable<TInfo>) new List<TInfo>();
      return response.Content == null ? (IEnumerable<TInfo>) new List<TInfo>() : SerializationHelper.DeserializeCollection<TInfo>(XElement.Parse(response.Content), (IEnumerable<Type>) extraTypes);
    }

    private IAsyncOperation<TInfo> GetItemAsync<TInfo>(
      Uri itemUri,
      bool raiseExceptionForNotFound = true,
      params Type[] extraTypes)
    {
      return WindowsRuntimeSystemExtensions.AsAsyncOperation<TInfo>(this._channel.SendAsyncInternal(new HttpRequest("GET", itemUri.AddApiVersion())
      {
        RaiseExceptionForNotFound = raiseExceptionForNotFound
      }, new Func<HttpResponse, HttpResponse>(HttpChannel.CheckNoContent)).ContinueWith<TInfo>((Func<Task<HttpResponse>, TInfo>) (tr => this.GetItem<TInfo>(tr.Result, extraTypes))));
    }

    private IAsyncAction DeleteItemAsync(Uri itemUri, string ETag = "")
    {
      HttpRequest request = new HttpRequest("DELETE", itemUri.AddApiVersion());
      request.ContentType = "application/atom+xml;type=entry;charset=utf-8";
      request.AddEtag(ETag);
      return WindowsRuntimeSystemExtensions.AsAsyncAction((Task) this._channel.SendAsyncInternal(request));
    }

    private string GetRegistrationId(HttpResponse response, params Type[] extraTypes)
    {
      string uriString;
      response.Headers.TryGetValue("Location", out uriString);
      string str = new Uri(uriString).AbsolutePath.TrimEnd('/');
      return str.Substring(str.LastIndexOf('/') + 1);
    }

    private TInfo GetItem<TInfo>(HttpResponse response, params Type[] extraTypes)
    {
      if (response.IsNotFoundStatusCode && !response.Request.RaiseExceptionForNotFound)
        return default (TInfo);
      return response.Content == null ? default (TInfo) : SerializationHelper.DeserializeItem<TInfo>(XElement.Parse(response.Content), (IEnumerable<Type>) null);
    }

    private IAsyncOperation<TInfo> CreateItemAsync<TInfo, TSettings>(
      Uri itemUri,
      TSettings itemSettings)
      where TSettings : class
    {
      HttpRequest request = new HttpRequest("PUT", itemUri.AddApiVersion());
      if (itemSettings is Registration registration && !string.IsNullOrEmpty(registration.ETag))
        request.AddEtag(registration.ETag);
      request.ContentType = "application/atom+xml;type=entry;charset=utf-8";
      request.Content = HttpUtilities.WrapAsAtomItem(SerializationHelper.Serialize((object) itemSettings));
      return WindowsRuntimeSystemExtensions.AsAsyncOperation<TInfo>(this._channel.SendAsyncInternal(request, new Func<HttpResponse, HttpResponse>(HttpChannel.CheckNoContent)).ContinueWith<TInfo>((Func<Task<HttpResponse>, TInfo>) (tr => this.GetItem<TInfo>(tr.Result))));
    }

    public void Dispose()
    {
      this._channel.Dispose();
      GC.SuppressFinalize((object) this);
    }

    internal static string GetServiceNamespace(string endpoint)
    {
      string serviceNamespace = (string) null;
      Uri result;
      if (Uri.TryCreate(endpoint, UriKind.Absolute, out result))
      {
        Match match = Regex.Match(result.Host, "^(.+)\\.servicebus\\.windows\\.net/?$", RegexOptions.IgnoreCase);
        if (match.Success)
          serviceNamespace = match.Groups[1].Value;
      }
      return serviceNamespace;
    }

    private static Exception CreateConnectionStringException(
      string argumentName,
      string errorMessage,
      params object[] args)
    {
      errorMessage = string.Format((IFormatProvider) CultureInfo.InvariantCulture, errorMessage, args);
      errorMessage = string.Format((IFormatProvider) CultureInfo.InvariantCulture, 
          "ErrorInvalidConnectionString", (object) argumentName, (object) errorMessage);
      return (Exception) new ArgumentException(errorMessage);
    }
  }
}
