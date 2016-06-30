using SD.IOC.Integration.WCFTests.Interfaces;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Integration.WCFTests.Implements
{
    /// <summary>
    /// 商品管理服务实现
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// 商品管理外部服务接口
        /// </summary>
        private readonly IProductContract _productContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        /// <param name="productContract">商品管理外部服务接口</param>
        public ProductService(IProductContract productContract)
        {
            this._productContract = productContract;
        }

        /// <summary>
        /// 获取商品集
        /// </summary>
        /// <returns>商品集</returns>
        public string GetProducts()
        {
            return this._productContract.GetProducts();
        }
    }
}
