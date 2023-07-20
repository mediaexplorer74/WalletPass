// Microsoft.WindowsAzure.Messaging.Http.Internal.HttpChannel

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal sealed class HttpChannel : IDisposable
  {
    private HttpChannel _nextChannel;
    private List<IHttpHandler> _handlers;

    public HttpChannel(params IHttpHandler[] handlers)
    {
      Validator.ArgumentIsNotNull(nameof (handlers), (object) handlers);
      this._handlers = new List<IHttpHandler>((IEnumerable<IHttpHandler>) handlers);
    }

    public HttpChannel(HttpChannel originalChannel, params IHttpHandler[] handlers)
    {
      Validator.ArgumentIsNotNull(nameof (originalChannel), (object) originalChannel);
      Validator.ArgumentIsNotNull(nameof (handlers), (object) handlers);
      this._handlers = new List<IHttpHandler>((IEnumerable<IHttpHandler>) handlers);
      this._nextChannel = originalChannel;
    }

    public static HttpChannel CreateWithWrapAuthentication(
      string serviceNamespace,
      string issuerName,
      string secretValue,
      params IHttpHandler[] additionalHandlers)
    {
      Validator.ArgumentIsValidPath(nameof (serviceNamespace), serviceNamespace);
      Validator.ArgumentIsNotNull(nameof (issuerName), (object) issuerName);
      Validator.ArgumentIsNotNull(nameof (secretValue), (object) secretValue);
      return new HttpChannel(((IEnumerable<IHttpHandler>) new WrapAuthenticationHandler[1]
      {
        new WrapAuthenticationHandler(serviceNamespace, issuerName, secretValue)
      }).Concat<IHttpHandler>((IEnumerable<IHttpHandler>) additionalHandlers).ToArray<IHttpHandler>());
    }

    public static HttpChannel CreateWithWrapAuthentication(
      string endpoint,
      string stsEndpoint,
      string issuerName,
      string secretValue,
      params IHttpHandler[] additionalHandlers)
    {
      Validator.ArgumentIsValidPath(nameof (endpoint), endpoint);
      Validator.ArgumentIsValidPath(nameof (stsEndpoint), stsEndpoint);
      Validator.ArgumentIsNotNull(nameof (issuerName), (object) issuerName);
      Validator.ArgumentIsNotNull(nameof (secretValue), (object) secretValue);
      return new HttpChannel(((IEnumerable<IHttpHandler>) new WrapAuthenticationHandler[1]
      {
        new WrapAuthenticationHandler(endpoint, stsEndpoint, issuerName, secretValue)
      }).Concat<IHttpHandler>((IEnumerable<IHttpHandler>) additionalHandlers).ToArray<IHttpHandler>());
    }

    public static HttpChannel CreateWithSharedAccessSecretAuthentication(
      string shareAccessSecretKeyName,
      string shareAccessSecret,
      params IHttpHandler[] additionalHandlers)
    {
      Validator.ArgumentIsNotNullOrEmptyString(nameof (shareAccessSecretKeyName), shareAccessSecretKeyName);
      Validator.ArgumentIsNotNull(nameof (shareAccessSecret), (object) shareAccessSecret);
      return new HttpChannel(((IEnumerable<IHttpHandler>) new SharedAccessSecretAuthenticationHandler[1]
      {
        new SharedAccessSecretAuthenticationHandler(shareAccessSecretKeyName, shareAccessSecret)
      }).Concat<IHttpHandler>((IEnumerable<IHttpHandler>) additionalHandlers).ToArray<IHttpHandler>());
    }

    public void Dispose()
    {
      foreach (IDisposable handler in this._handlers)
        handler.Dispose();
      GC.SuppressFinalize((object) this);
    }

    public IAsyncOperation<HttpResponse> SendAsync(HttpRequest request)
    {
      Validator.ArgumentIsNotNull(nameof (request), (object) request);
      return WindowsRuntimeSystemExtensions.AsAsyncOperation<HttpResponse>(this.SendRequest(request));
    }

    internal async Task<HttpResponse> SendRequest(HttpRequest request)
    {
      for (int index = 0; index < this._handlers.Count; ++index)
        request = this._handlers[index].ProcessRequest(request);
      await request.SendContentAsync();
      HttpResponse response = await request.GetResponseAsync();
      for (int index = this._handlers.Count - 1; index >= 0; --index)
        response = this._handlers[index].ProcessResponse(response);
      return response;
    }

    internal Task<HttpResponse> SendAsyncInternal(
      HttpRequest request,
      params Func<HttpResponse, HttpResponse>[] handlers)
    {
      return WindowsRuntimeSystemExtensions.AsTask<HttpResponse>(this.SendAsync(request)).ContinueWith<HttpResponse>((Func<Task<HttpResponse>, HttpResponse>) (t => this.ProcessResponse(t.Result, handlers)));
    }

    private HttpResponse ProcessResponse(
      HttpResponse response,
      Func<HttpResponse, HttpResponse>[] handlers)
    {
      if (!response.IsSuccessStatusCode && (response.Request.RaiseExceptionForNotFound 
                || !response.IsNotFoundStatusCode))
        throw new WindowsAzureHttpException("ErrorFailedRequest", response);

      if (handlers != null)
      {
        for (int index = 0; index < handlers.Length; ++index)
          response = handlers[index](response);
      }
      return response;
    }

    internal static HttpResponse CheckNoContent(HttpResponse response)
    {
        return response.StatusCode != 204 && response.StatusCode != 205 ? response
            : throw new WindowsAzureHttpException("ErrorNoContent", response);
    }
  }
}
