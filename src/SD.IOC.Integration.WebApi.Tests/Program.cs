using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using SD.IOC.Integration.WebApi.Tests;
using SD.Toolkits.AspNet;
using System;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IOC.Integration.WebApi.Tests
{
    class Program
    {
        static void Main()
        {
            StartOptions startOptions = new StartOptions();
            foreach (string url in AspNetSetting.OwinUrls)
            {
                Console.WriteLine($"Listening: {url}");
                startOptions.Urls.Add(url);
            }

            //开启服务
            WebApp.Start<Startup>(startOptions);

            Console.WriteLine("服务已启动...");
            Console.ReadKey();
        }
    }
}
