using SD.IOC.StubAppService.Implements;
using System;
using System.ServiceModel;

namespace SD.IOC.StubAppService.Host
{
    class Program
    {
        static void Main()
        {
            ServiceHost productContractHost = new ServiceHost(typeof(ProductContract));
            productContractHost.Open();

            Console.WriteLine("服务已启动...");
            Console.ReadKey();
        }
    }
}
