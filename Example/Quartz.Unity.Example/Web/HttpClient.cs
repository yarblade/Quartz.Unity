using System.Net;

namespace Quartz.Unity.Example.Web
{
    internal class HttpClient : IHttpClient
    {
        public string Get(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(url);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
