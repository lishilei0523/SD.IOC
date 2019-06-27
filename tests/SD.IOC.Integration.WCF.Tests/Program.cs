using Topshelf;

namespace SD.IOC.Integration.WCF.Tests
{
    class Program
    {
        static void Main(string[] args)
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

                config.SetServiceName("WCF.DependencyInjection");
                config.SetDisplayName("WCF.DependencyInjection");
                config.SetDescription("WCF依赖注入");
            });
        }
    }
}
