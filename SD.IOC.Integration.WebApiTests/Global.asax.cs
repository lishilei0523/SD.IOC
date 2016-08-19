using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApiTests
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}