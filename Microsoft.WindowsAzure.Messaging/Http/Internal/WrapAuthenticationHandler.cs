// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.Internal.WrapAuthenticationHandler
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal sealed class WrapAuthenticationHandler : IHttpHandler, IDisposable
  {
    private HttpChannel _channel;
    private Dictionary<string, WrapToken> _tokens;
    private object _syncObject;
    private string _issuerName;
    private string _issuerPassword;
    private Uri _authenticationUri;
    private Uri _serviceHostUri;

    private WrapAuthenticationHandler(string issuerName, string issuerPassword)
    {
      Validator.ArgumentIsNotNullOrEmptyString(nameof (issuerName), issuerName);
      Validator.ArgumentIsNotNull(nameof (issuerPassword), (object) issuerPassword);
      this._channel = new HttpChannel(new IHttpHandler[0]);
      this._issuerName = issuerName;
      this._issuerPassword = issuerPassword;
      this._channel = new HttpChannel(new IHttpHandler[0]);
      this._syncObject = new object();
      this._tokens = new Dictionary<string, WrapToken>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    public WrapAuthenticationHandler(
      string serviceNamespace,
      string issuerName,
      string issuerPassword)
      : this(issuerName, issuerPassword)
    {
      Validator.ArgumentIsValidPath(nameof (serviceNamespace), serviceNamespace);
      this._authenticationUri = new Uri(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "https://{0}-sb.accesscontrol.windows.net/wrapv0.9/", (object) serviceNamespace), UriKind.Absolute);
      this._serviceHostUri = new Uri(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "http://{0}.servicebus.windows.net/", (object) serviceNamespace), UriKind.Absolute);
    }

    public WrapAuthenticationHandler(
      string endpoint,
      string stsEndpoint,
      string issuerName,
      string issuerPassword)
      : this(issuerName, issuerPassword)
    {
      Validator.ArgumentIsValidPath(nameof (endpoint), endpoint);
      Validator.ArgumentIsValidPath(nameof (stsEndpoint), stsEndpoint);
      UriBuilder uriBuilder = new UriBuilder(stsEndpoint);
      uriBuilder.Scheme = "https";
      uriBuilder.Path += "wrapv0.9/";
      uriBuilder.Port = -1;
      this._authenticationUri = uriBuilder.Uri;
      this._serviceHostUri = new UriBuilder(endpoint)
      {
        Scheme = "http",
        Port = -1,
        Path = string.Empty
      }.Uri;
    }

    private static string CreateSignedAssertion(string issuerName, string issuerSecret)
    {
      string str = "Issuer=" + Uri.EscapeDataString(issuerName);
      byte[] keyBuffer = Convert.FromBase64String(issuerSecret);
      string stringToEscape = WrapAuthenticationHandler.Sign(str, keyBuffer);
      return str + "&HMACSHA256=" + Uri.EscapeDataString(stringToEscape);
    }

    private static string Sign(string value, byte[] keyBuffer) => Convert.ToBase64String(((HashAlgorithm) new HMACSHA256(keyBuffer)).ComputeHash((Stream) new MemoryStream(Encoding.UTF8.GetBytes(value))));

    public void Dispose()
    {
      this._channel.Dispose();
      GC.SuppressFinalize((object) this);
    }

    HttpRequest IHttpHandler.ProcessRequest(HttpRequest request)
    {
      Validator.ArgumentIsNotNull(nameof (request), (object) request);
      this.GetToken(request.Uri.AbsolutePath).Authorize(request);
      return request;
    }

    HttpResponse IHttpHandler.ProcessResponse(HttpResponse response)
    {
      Validator.ArgumentIsNotNull(nameof (response), (object) response);
      return response;
    }

    internal WrapToken GetToken(string resourcePath)
    {
      WrapToken token;
      lock (this._syncObject)
      {
        this._tokens.TryGetValue(resourcePath, out token);
        if (token != null)
        {
          if (token.IsExpired)
            this._tokens.Remove(resourcePath);
        }
      }
      if (token == null)
      {
        Uri uri = new Uri(this._serviceHostUri, resourcePath);
        HttpResponse result = this._channel.SendAsyncInternal(new HttpRequest("POST", this._authenticationUri)
        {
          Content = string.Format("wrap_scope={0}&wrap_assertion_format=SWT&wrap_assertion={1}", (object) HttpUtilities.UrlEncode(uri.ToString()), (object) HttpUtilities.UrlEncode(WrapAuthenticationHandler.CreateSignedAssertion(this._issuerName, this._issuerPassword))),
          ContentType = "application/x-www-form-urlencoded"
        }).Result;
        token = result.IsSuccessStatusCode ? new WrapToken(resourcePath, result) : throw new WrapAuthenticationException(result);
        lock (this._syncObject)
        {
          WrapToken wrapToken;
          if (this._tokens.TryGetValue(resourcePath, out wrapToken) && !wrapToken.IsExpired)
            token = wrapToken;
          else
            this._tokens[resourcePath] = token;
        }
      }
      return token;
    }
  }
}
