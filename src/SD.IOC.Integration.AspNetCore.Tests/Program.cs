using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SD.IOC.Integration.AspNetCore.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();

            //WebHost配置
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options => options.ListenLocalhost(6345));
                webBuilder.UseStartup<Startup>();
            });

            //依赖注入配置
            hostBuilder.UseServiceProviderFactory(new ServiceProviderFactory());

            IHost host = hostBuilder.Build();
            host.Run();
        }
    }
}
