using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SD.IOC.Integration.MVCTests
{
    /// <summary>
    /// 全局应用程序类
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}