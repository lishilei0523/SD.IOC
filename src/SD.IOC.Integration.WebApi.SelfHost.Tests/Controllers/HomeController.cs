using SD.IOC.StubIAppService.Interfaces;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.SelfHost.Tests.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IProductContract _productContract;

        public HomeController(IProductContract productContract)
        {
            this._productContract = productContract;
        }

        [HttpGet]
        [HttpPost]
        public string GetProducts()
        {
            return this._productContract.GetProducts();
        }
    }
}