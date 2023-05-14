using Microsoft.Owin;
using SD.IOC.Integration.WebApi.Tests;
using Topshelf;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IOC.Integration.WebApi.Tests
{
    class Program
    {
        static void Main()
        {
            HostFactory.Run(config =>
            {
                config.Service<ServiceLauncher>(host =>
                {
                    host.ConstructUsing(name => new ServiceLauncher());
                    host.WhenStarted(launcher => launcher.Start());
                    host.WhenStopped(launcher => launcher.Stop());
                });
                config.RunAsLocalSystem();

                config.SetServiceName("ASP.NET WebApi");
                config.SetDisplayName("ASP.NET WebApi");
                config.SetDescription("ASP.NET WebApi");
            });
        }
    }
}
