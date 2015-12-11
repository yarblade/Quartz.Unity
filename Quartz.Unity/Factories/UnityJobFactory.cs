using System;
using System.Globalization;

using Common.Logging;

using Microsoft.Practices.Unity;

using Quartz.Spi;
using Quartz.Unity.Jobs;

namespace Quartz.Unity.Factories
{
    internal class UnityJobFactory : IJobFactory
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log = LogManager.GetLogger(typeof(UnityJobFactory));

        public UnityJobFactory(IUnityContainer container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle.JobDetail.JobType == null)
            {
                throw new InvalidOperationException("Cannot instantiate null");
            }

            try
            {
                if (_log.IsDebugEnabled)
                {
                    _log.DebugFormat("Producing instance of Job '{0}', class={1}", bundle.JobDetail.Key, bundle.JobDetail.JobType.FullName);
                }

                return new JobWrapper(bundle.JobDetail, _container);
            }
            catch (Exception ex)
            {
                throw new SchedulerException(
                    string.Format(CultureInfo.InvariantCulture, "Problem instantiating class '{0}'", bundle.JobDetail.JobType.FullName), ex);
            }
        }

        public void ReturnJob(IJob job)
        {
            // Disposing of job not needed, because it will be disposed with UnityContainer
        }
    }
}
