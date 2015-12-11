using System;
using System.Threading;

using Quartz.Unity.Example.Web;

namespace Quartz.Unity.Example.Jobs
{
    internal class CheckBingAvailabilityJob : IJob
    {
        private readonly IHttpClient _httpClient;

        public CheckBingAvailabilityJob(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Execute(IJobExecutionContext context)
        {
            var i = 0;

            while (i++ < int.MaxValue)
            {
                var response = _httpClient.Get("http://bing.com");

                Console.WriteLine(!string.IsNullOrEmpty(response) ? "Bing was available at {0}" : "Bing was not available at {0}", DateTime.Now);

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
    }
}
