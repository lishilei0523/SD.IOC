using System.Web.Http;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Integration.WebApiTests.Controllers
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