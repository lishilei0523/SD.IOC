using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.Tests
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Register(GlobalConfiguration.Configuration);
        }

        public static void Register(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            //httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}