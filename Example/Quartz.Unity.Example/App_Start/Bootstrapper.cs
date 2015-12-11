using Microsoft.Practices.Unity;

using Quartz.Unity.Example.Web;

namespace Quartz.Unity.Example
{
    internal static class Bootstrapper
    {
        public static IUnityContainer CreateContainer()
        {
            return new UnityContainer()
                .RegisterType<IHttpClient, HttpClient>();
        }
    }
}
