using Common.Logging;

using Microsoft.Practices.Unity;

namespace Quartz.Unity.Jobs
{
    internal class JobWrapper : IInterruptableJob
    {
        private readonly IUnityContainer _container;
        private readonly IJobDetail _jobDetail;
        private readonly ILog _log = LogManager.GetLogger(typeof(JobWrapper));
        private IJob _executingJob;

        public JobWrapper(IJobDetail jobDetail, IUnityContainer container)
        {
            _jobDetail = jobDetail;
            _container = container;
        }

        public void Execute(IJobExecutionContext context)
        {
            var childContainer = _container.CreateChildContainer();
            try
            {
                _executingJob = (IJob)childContainer.Resolve(_jobDetail.JobType);

                DebugFormat("Job '{0}' was started.", _jobDetail.Key);
                _executingJob.Execute(context);
                DebugFormat("Job '{0}' was finished.", _jobDetail.Key);
            }
            finally
            {
                _executingJob = null;
                childContainer.Dispose();
            }
        }

        public void Interrupt()
        {
            var interruptableJob = _executingJob as IInterruptableJob;
            if (interruptableJob != null)
            {
                interruptableJob.Interrupt();

                _log.InfoFormat("Job '{0}' was interrupted.", _jobDetail.Key);
            }
        }

        private void DebugFormat(string format, params object[] args)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat(format, args);
            }
        }
    }
}
