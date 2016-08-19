using System.Web.Http;

namespace SD.IOC.Integration.WebApiTests
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //01.WebApi中有两套文档传输格式，XML与JSON，默认为XML
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.EnableSystemDiagnosticsTracing();
        }
    }
}
