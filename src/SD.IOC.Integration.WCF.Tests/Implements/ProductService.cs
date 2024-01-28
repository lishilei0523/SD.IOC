using SD.IOC.Integration.WCF.Tests.Interfaces;
using SD.IOC.StubIAppService.Interfaces;
#if NET40_OR_GREATER
using System.ServiceModel;
#endif
#if NETCOREAPP3_1_OR_GREATER
using CoreWCF;
#endif

namespace SD.IOC.Integration.WCF.Tests.Implements
{
    /// <summary>
    /// 产品管理服务契约实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class ProductService : IProductService
    {
        /// <summary>
        /// 产品管理服务契约接口
        /// </summary>
        private readonly IProductContract _productContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ProductService(IProductContract productContract)
        {
            this._productContract = productContract;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        public string GetProducts()
        {
            return this._productContract.GetProducts();
        }
    }
}
