// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.ConnectionString
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;

namespace Microsoft.WindowsAzure.Messaging
{
  public static class ConnectionString
  {
    public static string CreateUsingSharedAccessKeyWithFullAccess(
      Uri endPoint,
      string fullAccessSecret)
    {
      return !string.IsNullOrWhiteSpace(fullAccessSecret) ? ConnectionString.CreateUsingSharedAccessKey(endPoint, "DefaultFullSharedAccessSignature", fullAccessSecret) : throw new ArgumentNullException(nameof (fullAccessSecret));
    }

    public static string CreateUsingSharedAccessKeyWithListenAccess(
      Uri endPoint,
      string listenAccessSecret)
    {
      return !string.IsNullOrWhiteSpace(listenAccessSecret) ? ConnectionString.CreateUsingSharedAccessKey(endPoint, "DefaultListenSharedAccessSignature", listenAccessSecret) : throw new ArgumentNullException(nameof (listenAccessSecret));
    }

    public static string CreateUsingSharedAccessKey(
      Uri endPoint,
      string keyName,
      string accessSecret)
    {
      if (endPoint == (Uri) null)
        throw new ArgumentNullException(nameof (endPoint));
      if (string.IsNullOrWhiteSpace(keyName))
        throw new ArgumentNullException(nameof (keyName));
      if (string.IsNullOrWhiteSpace(accessSecret))
        throw new ArgumentNullException(nameof (accessSecret));
      return string.Format("Endpoint={0};{1}={2};{3}={4}", (object) endPoint.AbsoluteUri, (object) "SharedAccessKeyName", (object) keyName, (object) "SharedAccessKey", (object) accessSecret);
    }

    public static string CreateUsingSharedSecret(Uri endPoint, string issuer, string issuerSecret)
    {
      if (endPoint == (Uri) null)
        throw new ArgumentNullException(nameof (endPoint));
      if (string.IsNullOrWhiteSpace(issuer))
        throw new ArgumentNullException(nameof (issuer));
      if (string.IsNullOrWhiteSpace(issuerSecret))
        throw new ArgumentNullException(nameof (issuerSecret));
      return string.Format("Endpoint={0};{1}={2};{3}={4}", (object) endPoint.AbsoluteUri, (object) "SharedSecretIssuer", (object) issuer, (object) "SharedSecretValue", (object) issuerSecret);
    }

    internal static string CreateUsingEndpoint(Uri endPoint)
    {
      if (endPoint == (Uri) null)
        throw new ArgumentNullException(nameof (endPoint));
      return "Endpoint=" + endPoint.AbsoluteUri;
    }
  }
}
