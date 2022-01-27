using SD.IOC.StubAppService.Implements;
using System;
using System.ServiceModel;

namespace SD.IOC.StubAppService
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private readonly ServiceHost _productContractHost;

        /// <summary>
        /// 构造器
        /// </summary>
        public ServiceLauncher()
        {
            this._productContractHost = new ServiceHost(typeof(ProductContract));
        }

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._productContractHost.Open();

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._productContractHost.Close();

            Console.WriteLine("服务已关闭...");
        }
    }
}
