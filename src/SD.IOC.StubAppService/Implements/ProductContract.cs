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
    /// 商品管理实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class ProductContract : IProductContract
    {
        /// <summary>
        /// 获取商品集
        /// </summary>
        /// <returns>商品集</returns>
        public string GetProducts()
        {
            return "Hello World";
        }
    }
}
