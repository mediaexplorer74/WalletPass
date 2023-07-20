// Microsoft.WindowsAzure.Messaging.Http.WrapAuthenticationException

using Microsoft.WindowsAzure.Messaging.Http.Internal;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal class WrapAuthenticationException : WindowsAzureHttpException
  {
    internal WrapAuthenticationException(HttpResponse response)
      : base("ErrorWrapAuthentication", response)
    {
    }
  }
}
