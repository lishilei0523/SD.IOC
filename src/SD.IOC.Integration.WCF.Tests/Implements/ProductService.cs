﻿using SD.IOC.Integration.WCF.Tests.Interfaces;
using SD.IOC.StubIAppService.Interfaces;
#if NET40_OR_GREATER
using System.ServiceModel;
#else
using CoreWCF;
#endif

namespace SD.IOC.Integration.WCF.Tests.Implements
{
    /// <summary>
    /// 商品管理服务实现
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
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
