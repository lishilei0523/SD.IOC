using System.Threading;
using SD.IOC.StubIAppService.Interfaces;
#if NET40_OR_GREATER
using System.ServiceModel;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
#endif

namespace SD.IOC.StubAppService.Implements
{
    /// <summary>
    /// 产品管理服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class ProductContract : IProductContract
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        public string GetProducts()
        {
            Thread.Sleep(500);

            return "Hello World";
        }
    }
}
