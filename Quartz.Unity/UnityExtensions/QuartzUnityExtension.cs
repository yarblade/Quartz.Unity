using Microsoft.Practices.Unity;

using Quartz.Unity.Factories;

namespace Quartz.Unity.UnityExtensions
{
    public class QuartzUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<ISchedulerFactory, UnitySchedulerFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IScheduler>(new InjectionFactory(c => c.Resolve<ISchedulerFactory>().GetScheduler()));
        }
    }
}
