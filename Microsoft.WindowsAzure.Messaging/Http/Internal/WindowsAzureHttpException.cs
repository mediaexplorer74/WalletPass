// Microsoft.WindowsAzure.Messaging.Http.Internal.WindowsAzureHttpException

using System;
using System.Globalization;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal class WindowsAzureHttpException : WindowsAzureException
  {
    internal WindowsAzureHttpException(string shortMessage, HttpResponse response)
      : base(WindowsAzureHttpException.GetHttpErrorMessage(shortMessage, response), WindowsAzureHttpException.GetHttpErrorCode(response))
    {
      this.Method = response.Request.Method;
    }

    internal WindowsAzureHttpException(string shortMessage, int errorCode)
      : base(shortMessage, errorCode)
    {
    }

    public string Method { get; set; }

    private static int GetHttpErrorCode(HttpResponse response) => response.StatusCode;

    private static string GetHttpErrorMessage(string message, HttpResponse response)
    {
      string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture,
          "HttpDetails", 
          (object) response.StatusCode, (object) response.ReasonPhrase, (object) response.Content);
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture,
          "HttpErrorMessage", (object) message, (object) str);
    }
  }
}
