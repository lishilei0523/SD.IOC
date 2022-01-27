using SD.IOC.StubIAppService.Interfaces;
using System.Web.Mvc;

namespace SD.IOC.Integration.AspNetMvc.Tests.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductContract _productContract;

        public HomeController(IProductContract productContract)
        {
            this._productContract = productContract;
        }

        public ActionResult Index()
        {
            this.ViewBag.Hello = this._productContract.GetProducts();

            return this.View();
        }

    }
}
