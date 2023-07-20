// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.UriExtensions
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging
{
  internal static class UriExtensions
  {
    public static Uri AddTop(this Uri target, int top) => top < 1 ? target : UriExtensions.AddQueryParameter(target, top.ToString((IFormatProvider) CultureInfo.InvariantCulture), nameof (top));

    public static Uri AddApiVersion(this Uri itemUri) => UriExtensions.AddQueryParameter(itemUri, "api-version=2014-01");

    public static Uri AddContinuationToken(this Uri target, string continuationToken) => UriExtensions.AddQueryParameter(target, continuationToken, "continuationtoken");

    private static Uri AddQueryParameter(Uri target, string parameter, string parameterName = null)
    {
      if (string.IsNullOrWhiteSpace(parameter))
        return target;
      if (!string.IsNullOrWhiteSpace(parameterName))
        parameter = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}={1}", (object) parameterName, (object) parameter);
      if (target.Query.Contains(parameter))
        return target;
      UriBuilder uriBuilder = new UriBuilder(target);
      uriBuilder.Query = uriBuilder.Query.Length <= 1 ? parameter : uriBuilder.Query.Substring(1) + "&" + parameter;
      return target = uriBuilder.Uri;
    }
  }
}
