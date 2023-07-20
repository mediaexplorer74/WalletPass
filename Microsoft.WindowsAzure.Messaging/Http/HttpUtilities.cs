// Decompiled with JetBrains decompiler
// Type: Microsoft.WindowsAzure.Messaging.Http.HttpUtilities
// Assembly: Microsoft.WindowsAzure.Messaging, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B9918A97-7C70-4F7D-AF4D-7D1C4EF615F3
// Assembly location: C:\Users\Admin\Desktop\re\wp\4\Microsoft.WindowsAzure.Messaging.dll

using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Microsoft.WindowsAzure.Messaging.Http
{
  internal static class HttpUtilities
  {
    public static XNamespace serviceBusDef = (XNamespace) "http://schemas.microsoft.com/netservices/2010/10/servicebus/connect";

    public static Exception ConvertToRegistrationException(Exception ex)
    {
      Exception innerException1 = ex is AggregateException ? ((Exception) ((AggregateException) ex).Flatten()).InnerException : ex;
      WindowsAzureException exception = innerException1 as WindowsAzureException;
      TimeoutException innerException2 = innerException1 as TimeoutException;
      if (innerException1 is UnauthorizedAccessException innerException3)
        return (Exception) new RegistrationAuthorizationException(innerException3.Message, (Exception) innerException3);
      if (innerException2 != null)
        return (Exception) new RegistrationCommunicationException(innerException2.Message, (Exception) innerException2);
      return exception != null ? exception.ConvertToRegistrationManagementException() : (Exception) new RegistrationException(innerException1.Message, innerException1);
    }

    public static Exception ConvertToRegistrationManagementException(
      this WindowsAzureException exception)
    {
      switch (exception.ErrorCode)
      {
        case 204:
        case 404:
          return (Exception) new RegistrationNotFoundException(exception.Message, (Exception) exception);
        case 400:
          return (Exception) new RegistrationBadRequestException(exception.Message, (Exception) exception);
        case 401:
          return (Exception) new RegistrationAuthorizationException(exception.Message, (Exception) exception);
        case 403:
          return (Exception) new QuotaExceededException(exception.Message, (Exception) exception);
        case 409:
          return (Exception) new RegistrationAlreadyExistsException(exception.Message, (Exception) exception);
        case 410:
          return (Exception) new RegistrationGoneException(exception.Message, (Exception) exception);
        case 412:
          return (Exception) new RegistrationMismatchedETagException(exception.Message, (Exception) exception);
        case 503:
          return (Exception) new ServerBusyException(exception.Message, (Exception) exception);
        default:
          return (Exception) new RegistrationException(exception.Message, (Exception) exception);
      }
    }

    public static string WrapAsAtomItem(string item)
    {
      XNamespace xnamespace = (XNamespace) "http://www.w3.org/2005/Atom";
      return ((object) new XElement(xnamespace + "entry", (object) new XElement(xnamespace + "content", new object[2]
      {
        (object) new XAttribute((XName) "type", (object) "text/xml"),
        (object) XElement.Parse(item)
      }))).ToString();
    }

    public static string UrlEncode(string content) => Regex.Replace(WebUtility.UrlEncode(content), "%([0-9|A-Z][0-9|A-Z])", (MatchEvaluator) (m => m.ToString().ToLower()));
  }
}
