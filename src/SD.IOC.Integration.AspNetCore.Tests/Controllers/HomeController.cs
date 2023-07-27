using Microsoft.AspNetCore.Mvc;
using SD.IOC.Integration.AspNetCore.Tests.Interfaces;

namespace SD.IOC.Integration.AspNetCore.Tests.Controllers
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 产品呈现器
        /// </summary>
        private readonly IProductPresenter _productPresenter;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public HomeController(IProductPresenter productPresenter)
        {
            this._productPresenter = productPresenter;
        }

        /// <summary>
        /// 首页
        /// </summary>
        [HttpGet]
        public dynamic Index()
        {
            string productsStr = this._productPresenter.GetProducts();
            var product = new
            {
                Number = "001",
                Name = productsStr
            };

            return product;
        }
    }
}
