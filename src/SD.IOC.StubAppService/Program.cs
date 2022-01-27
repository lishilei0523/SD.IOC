using Topshelf;

namespace SD.IOC.StubAppService
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

                config.SetServiceName("SD.IOC.StubAppService");
                config.SetDisplayName("SD.IOC.StubAppService");
                config.SetDescription("SD.IOC.StubAppService");
            });
        }
    }
}
