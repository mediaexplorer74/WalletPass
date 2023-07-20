// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.Internal.WrapToken
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal class WrapToken
  {
    private DateTime _expirationDate;

    internal string Scope { get; private set; }

    internal string Token { get; private set; }

    internal bool IsExpired => DateTime.Now > this._expirationDate;

    internal WrapToken(string resourcePath, HttpResponse response)
    {
      this.Scope = resourcePath;
      Dictionary<string, string> dictionary = HttpQueryStringParser.Parse(response.Content);
      this.Token = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "WRAP access_token=\"{0}\"", (object) WebUtility.UrlDecode(dictionary["wrap_access_token"]));
      this._expirationDate = DateTime.Now + TimeSpan.FromSeconds((double) (int.Parse(dictionary["wrap_access_token_expires_in"]) / 2));
    }

    internal HttpRequest Authorize(HttpRequest request)
    {
      request.Headers["Authorization"] = this.Token;
      return request;
    }
  }
}
