using System.Net;

namespace ComponentTesting.Inprocess.Services
{
    public class DefaultHttpInvoker:IHttpInvoker
    {
        public string InvokeHttp(string url)
        {
            return new WebClient().DownloadString(url);
        }
    }
}