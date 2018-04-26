using Microsoft.AspNetCore.Mvc;
using SD.IOC.Integration.AspNetCore.Tests.Interfaces;

namespace SD.IOC.Integration.AspNetCore.Tests.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductPresenter _productPresenter;

        public HomeController(IProductPresenter productPresenter)
        {
            this._productPresenter = productPresenter;
        }

        public IActionResult Index()
        {
            string products = this._productPresenter.GetProducts();
            base.ViewBag.Products = products;

            return base.View();
        }
    }
}
