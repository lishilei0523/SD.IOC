﻿using Topshelf;

namespace SD.IOC.StubImplement
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

                config.SetServiceName("SD.IOC.StubImplement");
                config.SetDisplayName("SD.IOC.StubImplement");
                config.SetDescription("SD.IOC.StubImplement");
            });
        }
    }
}
