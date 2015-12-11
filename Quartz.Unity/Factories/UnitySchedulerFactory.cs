using Quartz.Core;
using Quartz.Impl;

namespace Quartz.Unity.Factories
{
    internal class UnitySchedulerFactory : StdSchedulerFactory
    {
        private readonly UnityJobFactory _unityJobFactory;

        public UnitySchedulerFactory(UnityJobFactory unityJobFactory)
        {
            _unityJobFactory = unityJobFactory;
        }

        protected override IScheduler Instantiate(QuartzSchedulerResources schedulerResources, QuartzScheduler scheduler)
        {
            scheduler.JobFactory = _unityJobFactory;

            return base.Instantiate(schedulerResources, scheduler);
        }
    }
}
