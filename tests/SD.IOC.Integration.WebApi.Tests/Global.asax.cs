using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.Tests
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}