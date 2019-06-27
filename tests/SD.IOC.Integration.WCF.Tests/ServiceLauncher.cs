using SD.IOC.Integration.WCF.Tests.Implements;
using System;
using System.ServiceModel;

namespace SD.IOC.Integration.WCF.Tests
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private readonly ServiceHost _productServiceHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._productServiceHost = new ServiceHost(typeof(ProductService));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._productServiceHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._productServiceHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
