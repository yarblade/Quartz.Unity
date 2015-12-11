using System;

using Microsoft.Practices.Unity;

namespace Quartz.Unity.Example
{
    internal class Program
    {
        private static void Main()
        {
            using (var container = Bootstrapper.CreateContainer())
            {
                container.AddExtension(new QuartzUnityExtension());

                var scheduler = container.Resolve<IScheduler>();

                scheduler.Start();

                Console.WriteLine("================================================================================");
                Console.WriteLine("                       Press any key to shutdown scheduler                      ");
                Console.WriteLine("                              Press Ctrl-C to exit                              ");
                Console.WriteLine("================================================================================");
                Console.ReadKey();

                scheduler.DeleteJob(new JobKey("CheckBingAvailabilityJob"));
                scheduler.Shutdown(true);
            }
        }
    }
}
