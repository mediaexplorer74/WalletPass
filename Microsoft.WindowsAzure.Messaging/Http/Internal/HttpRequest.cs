// Microsoft.WindowsAzure.Messaging.Http.Internal.HttpRequest

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Messaging.Http.Internal
{
  internal sealed class HttpRequest
  {
    private HttpWebRequest request;

    public Uri Uri => this.request.RequestUri;

    public string Method
    {
      get => this.request.Method;
      set => this.request.Method = value;
    }

    public WebHeaderCollection Headers => this.request.Headers;

    public string ContentType
    {
      get => this.request.ContentType;
      set => this.request.ContentType = value;
    }

    public long ContentLength
        {
            get
            {
                return (this.request).ToString().Length;
            }

            set
            {
               // (this.request).ToString().Length = value;
            }
        }

        public string UserAgent
        {
            get
            {
                return "UserAgent";//this.request.UserAgent;
            }

            set
            {
                //this.request.UserAgent = value;
            }
        }

        public string Content { get; set; }

    public bool RaiseExceptionForNotFound { get; set; }

    public HttpRequest(string method, Uri uri)
    {
      Validator.ArgumentIsNotNullOrEmptyString(nameof (method), method);
      Validator.ArgumentIsNotNull(nameof (uri), (object) uri);
      this.request = WebRequest.CreateHttp(uri);
      this.request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString();
      
      /*      
      this.request.UserAgent = string.Format((IFormatProvider) CultureInfo.InvariantCulture,
          "SERVICEBUS/2014-01(api-origin=WindowsPhoneSdk;os={0};os-version={1};)",
          (object) "Windows", //Environment.OSVersion.Platform
          (object) "1");//Environment.OSVersion.Version
      */
      this.request.Method = method;
      this.RaiseExceptionForNotFound = true;
    }

    internal void AddEtag(string ETag)
    {
      if (string.IsNullOrEmpty(ETag))
        return;
      this.Headers["If-Match"] = ETag == "\"*\"" ? ETag : string.Format("\"{0}\"", (object) ETag);
    }

    internal async Task SendContentAsync()
    {
            if (!string.IsNullOrEmpty(this.Content))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(this.Content);
                //this.request.ContentLength = (long) bytes.Length;
                using (Stream requestStream = await Task<Stream>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(((WebRequest)this.request).BeginGetRequestStream), new Func<IAsyncResult, Stream>(((WebRequest)this.request).EndGetRequestStream), (object)null))
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);
            }
            else
            {
                //this.request.ContentLength = 0L;
            }
    }

    internal async Task<HttpResponse> GetResponseAsync()
    {
      WebResponse response;
      try
      {
        response = await Task<WebResponse>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(((WebRequest) this.request).BeginGetResponse), new Func<IAsyncResult, WebResponse>(((WebRequest) this.request).EndGetResponse), (object) null);
      }
      catch (WebException ex)
      {
        response = ex.Response;
      }
      return await new HttpResponse(this, response).PrepareContentAsync();
    }
  }
}
