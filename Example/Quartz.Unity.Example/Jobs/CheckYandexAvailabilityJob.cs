using System;
using System.Threading;

using Quartz.Unity.Example.Web;

namespace Quartz.Unity.Example.Jobs
{
    internal class CheckYandexAvailabilityJob : InterruptableJob
    {
        private readonly IHttpClient _httpClient;

        public CheckYandexAvailabilityJob(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override void Execute(IJobExecutionContext context, CancellationToken token)
        {
            var i = 0;

            while (i++ < int.MaxValue)
            {
                token.ThrowIfCancellationRequested();

                var response = _httpClient.Get("http://ya.ru");

                Console.WriteLine(!string.IsNullOrEmpty(response) ? "Yandex was available at {0}" : "Yandex was not available at {0}", DateTime.Now);

                Thread.Sleep(TimeSpan.FromSeconds(10));
            }
        }
    }
}
