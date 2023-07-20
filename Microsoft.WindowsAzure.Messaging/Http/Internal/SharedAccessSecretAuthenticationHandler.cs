// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.Internal.SharedAccessSecretAuthenticationHandler
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal sealed class SharedAccessSecretAuthenticationHandler : IHttpHandler, IDisposable
  {
    private readonly HttpChannel _channel;
    private readonly TimeSpan _timeToLive;
    private readonly string _keyName;
    private readonly string _secret;

    public SharedAccessSecretAuthenticationHandler(
      string shareAccessSecretKeyName,
      string shareAccessSecret,
      TimeSpan? timeToLive = null)
    {
      Validator.ArgumentIsNotNullOrEmptyString(nameof (shareAccessSecretKeyName), shareAccessSecretKeyName);
      Validator.ArgumentIsNotNull(nameof (shareAccessSecret), (object) shareAccessSecret);
      this._channel = new HttpChannel(new IHttpHandler[0]);
      this._keyName = shareAccessSecretKeyName;
      this._secret = shareAccessSecret;
      this._timeToLive = !timeToLive.HasValue ? TimeSpan.FromMinutes(20.0) : timeToLive.Value;
    }

    public void Dispose()
    {
      this._channel.Dispose();
      GC.SuppressFinalize((object) this);
    }

    HttpRequest IHttpHandler.ProcessRequest(HttpRequest request)
    {
      Validator.ArgumentIsNotNull(nameof (request), (object) request);
      request.Headers["Authorization"] = this.GetToken(request.Uri);
      return request;
    }

    HttpResponse IHttpHandler.ProcessResponse(HttpResponse response)
    {
      Validator.ArgumentIsNotNull(nameof (response), (object) response);
      return response;
    }

    internal string GetToken(Uri requestUri)
    {
      string str1 = HttpUtilities.UrlEncode(SharedAccessSecretAuthenticationHandler.NormalizeUri(requestUri).ToLowerInvariant());
      string content = this.BuildExpiresOn(this._timeToLive);
      string str2 = HttpUtilities.UrlEncode(this.Sign(string.Join("\n", (IEnumerable<string>) new List<string>()
      {
        str1,
        content
      }), this._secret));
      return string.Format("{0} {1}={2}&{3}={4}&{5}={6}&{7}={8}", (object) "SharedAccessSignature", (object) "sr", (object) str1, (object) "sig", (object) str2, (object) "se", (object) HttpUtilities.UrlEncode(content), (object) "skn", (object) HttpUtilities.UrlEncode(this._keyName));
    }

    private static string NormalizeUri(Uri requestUri)
    {
      UriBuilder uriBuilder = new UriBuilder(requestUri)
      {
        Scheme = "http",
        Port = -1
      };
      if (!uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
        uriBuilder.Path += "/";
      return uriBuilder.Uri.AbsoluteUri;
    }

    private string BuildExpiresOn(TimeSpan timeToLive) => Convert.ToString(EpochTimeHelper.GetTotalSecondsByTimeSpan(timeToLive), (IFormatProvider) CultureInfo.InvariantCulture);

    private string Sign(string requestString, string sharedKey) => Convert.ToBase64String(((HashAlgorithm) new HMACSHA256(Encoding.UTF8.GetBytes(sharedKey))).ComputeHash(Encoding.UTF8.GetBytes(requestString)));
  }
}
