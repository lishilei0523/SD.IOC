using Microsoft.AspNetCore.Mvc;
using SD.IOC.Integration.AspNetCore.Tests.Interfaces;

namespace SD.IOC.Integration.AspNetCore.Tests.Controllers
{
    [ApiController]
    [Route("Api/[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IProductPresenter _productPresenter;

        public HomeController(IProductPresenter productPresenter)
        {
            this._productPresenter = productPresenter;
        }

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
