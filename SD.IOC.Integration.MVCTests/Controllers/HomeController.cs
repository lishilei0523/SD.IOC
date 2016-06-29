using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Integration.MVCTests.Controllers
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
