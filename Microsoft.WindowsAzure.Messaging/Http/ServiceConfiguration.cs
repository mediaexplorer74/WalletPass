// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.ServiceConfiguration
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal sealed class ServiceConfiguration
  {
    internal Uri ServiceBusUri { get; private set; }

    internal ServiceConfiguration(Uri uri) => this.ServiceBusUri = uri;

    internal Uri GetRegistrationUri(string notificationHubPath, string registrationName = "") => this.FormatUri("{0}/Registrations/{1}", (object) notificationHubPath, (object) registrationName);

    internal Uri CreateRegistrationId(string notificationHubPath) => this.FormatUri("{0}/registrationids", (object) notificationHubPath);

    internal Uri GetListRegistrationsUri(string notificationHubPath, string channelUri) => this.FormatUri("{0}/Registrations/?$filter=channeluri+eq+'{1}'", (object) notificationHubPath, (object) channelUri);

    internal Uri GetUpdateChannelUriPath(string notificationHubPath) => this.FormatUri("{0}/Registrations/updatepnshandle", (object) notificationHubPath);

    internal Uri GetItemsRangeQuery(Uri containerUri, int firstItem, int count) => this.FormatUri("{0}?$skip={1}&$top={2}", (object) containerUri.ToString(), (object) firstItem, (object) count);

    private Uri FormatUri(string format, params object[] args)
    {
      string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, args);
      string absoluteUri = this.ServiceBusUri.AbsoluteUri;
      if (!absoluteUri.EndsWith("/"))
        absoluteUri += "/";
      return new Uri(absoluteUri + str);
    }
  }
}
