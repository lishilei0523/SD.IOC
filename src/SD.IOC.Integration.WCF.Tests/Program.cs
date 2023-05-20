using SD.IOC.Integration.WCF.Tests.Implements;
using System;
using System.ServiceModel;

namespace SD.IOC.Integration.WCF.Tests
{
    class Program
    {
        static void Main()
        {
            ServiceHost productServiceHost = new ServiceHost(typeof(ProductService));
            productServiceHost.Open();

            Console.WriteLine("服务已启动...");
            Console.ReadKey();
        }
    }
}
