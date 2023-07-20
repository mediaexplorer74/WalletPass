// Microsoft.WindowsAzure.Messaging.Http.Internal.HttpResponse

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal sealed class HttpResponse
  {
    private WebResponse webResponse;

    public HttpRequest Request { get; private set; }

    public int StatusCode { get; set; }

    public bool IsSuccessStatusCode => this.StatusCode >= 200 && this.StatusCode < 299;

    public bool IsNotFoundStatusCode => this.StatusCode == 404;

    public string ReasonPhrase { get; set; }

    public string Content { get; set; }

    public IDictionary<string, string> Headers { get; private set; }

    public HttpResponse(HttpRequest originalRequest, int statusCode)
    {
      Validator.ArgumentIsNotNull(nameof (originalRequest), (object) originalRequest);
      Validator.ArgumentIsValidEnumValue<HttpStatusCode>(nameof (statusCode), (object) statusCode);
      this.Request = originalRequest;
      this.StatusCode = statusCode;
      this.Headers = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    internal HttpResponse(HttpRequest originalRequest, WebResponse response)
    {
      this.Request = originalRequest;
      this.webResponse = response;
      if (response is HttpWebResponse httpWebResponse)
      {
        this.StatusCode = (int) httpWebResponse.StatusCode;
        this.ReasonPhrase = httpWebResponse.StatusDescription;
      }
      this.Headers = (IDictionary<string, string>) new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (string allKey in response.Headers.AllKeys)
        this.Headers.Add(allKey, response.Headers[allKey]);
    }

    internal async Task<HttpResponse> PrepareContentAsync()
    {
      using (StreamReader sr = new StreamReader(this.webResponse.GetResponseStream()))
        this.Content = await sr.ReadToEndAsync();

      this.webResponse.Dispose();//.Close();
      return this;
    }
  }
}
