using SD.IOC.Integration.AspNetCore.Tests.Interfaces;
using SD.IOC.StubIAppService.Interfaces;

namespace SD.IOC.Integration.AspNetCore.Tests.Implements
{
    /// <summary>
    /// 产品呈现器实现
    /// </summary>
    public class ProductPresenter : IProductPresenter
    {
        /// <summary>
        /// 产品服务契约接口
        /// </summary>
        private readonly IProductContract _productContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public ProductPresenter(IProductContract productContract)
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
