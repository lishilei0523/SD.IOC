using SD.IOC.StubIAppService.Interfaces;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.Tests.Controllers
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    public class HomeController : ApiController
    {
        /// <summary>
        /// 产品服务契约接口
        /// </summary>
        private readonly IProductContract _productContract;

        /// <summary>
        /// 依赖注入构造器
        /// </summary>
        public HomeController(IProductContract productContract)
        {
            this._productContract = productContract;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        [HttpGet]
        public string GetProducts()
        {
            return this._productContract.GetProducts();
        }
    }
}
