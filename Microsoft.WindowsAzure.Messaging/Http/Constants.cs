// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.Constants
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal static class Constants
  {
    private const int ComErrorMask = -2147221504;
    internal const int WrapErrorMask = -2147221504;
    internal const int HttpErrorMask = -2147217408;
    internal const string ServiceBusServiceUri = "https://{0}.servicebus.windows.net/";
    internal const string ServiceBusAuthenticationUri = "https://{0}-sb.accesscontrol.windows.net/wrapv0.9/";
    internal const string ServiceBusAuthenticationUriPathExtension = "wrapv0.9/";
    internal const string ServiceBusScopeUri = "http://{0}.servicebus.windows.net/";
    internal const string NamespaceExpression = "^(.+)\\.servicebus\\.windows\\.net/?$";
    private const string ApiVersionValue = "2014-01";
    internal const string UserAgentTemplate = "SERVICEBUS/2014-01(api-origin=WindowsPhoneSdk;os={0};os-version={1};)";
    internal const string ApiVersion = "api-version=2014-01";
    internal const string RangeQueryUri = "{0}?$skip={1}&$top={2}";
    internal const string ContinuationTokenQueryName = "continuationtoken";
    internal const string TopQueryName = "top";
    internal const string RegistrationPath = "{0}/Registrations/{1}";
    internal const string RegistrationIdPath = "{0}/registrationids";
    internal const string LocationHeaderName = "Location";
    internal const string ListRegistrationsPath = "{0}/Registrations/?$filter=channeluri+eq+'{1}'";
    internal const string UpdateChannelUriPath = "{0}/Registrations/updatepnshandle";
    internal const string WrapTokenAuthenticationString = "WRAP access_token=\"{0}\"";
    internal const string SerializationContentType = "application/xml";
    internal const string BodyContentType = "application/atom+xml;type=entry;charset=utf-8";
    internal const string JsonBodyContentType = "application/json";
    internal const string DefaultMessageContentType = "text/plain";
    internal const string WrapAuthenticationContentType = "application/x-www-form-urlencoded";
    internal const string AcceptHeader = "Accept";
    internal const string BrokerPropertiesHeader = "BrokerProperties";
    internal const string DateHeader = "Date";
    public const string ContinuationTokenHeaderName = "x-ms-continuationtoken";
    internal const int CompatibilityLevel = 20;
    internal const string JsonNullValue = "null";
    internal const string ContentTypeHeader = "Content-Type";
    internal const string AtomFeedElementName = "feed";
    internal const string AtomEntryElementName = "entry";
    internal const string AtomEntryContentElementName = "content";
    internal const string RegistrationName = "MpnsRegistrationDescription";
    internal const string TemplateRegistrationName = "MpnsTemplateRegistrationDescription";
    internal const string EndpointKey = "Endpoint";
    internal const string StsEndpointKey = "StsEndpoint";
    internal const string SecretIssuerKey = "SharedSecretIssuer";
    internal const string SecretValueKey = "SharedSecretValue";
    internal const string SasKeyNameKey = "SharedAccessKeyName";
    internal const string SasValueKey = "SharedAccessKey";
  }
}
