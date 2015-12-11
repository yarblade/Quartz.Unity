using System;
using System.Threading;

using Quartz.Unity.Example.Web;

namespace Quartz.Unity.Example.Jobs
{
    internal class CheckGoogleAvailabilityJob : IInterruptableJob
    {
        private readonly IHttpClient _httpClient;
        private bool _interrupted;

        public CheckGoogleAvailabilityJob(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Execute(IJobExecutionContext context)
        {
            while (true)
            {
                if (_interrupted)
                {
                    return;
                }

                var response = _httpClient.Get("http://ya.ru");

                Console.WriteLine(!string.IsNullOrEmpty(response) ? "Google was available at {0}" : "Google was not available at {0}", DateTime.Now);

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }

        public void Interrupt()
        {
            _interrupted = true;
        }
    }
}
