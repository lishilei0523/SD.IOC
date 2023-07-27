using Owin;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.Tests
{
    /// <summary>
    /// OWIN启动器
    /// </summary>
    public class Startup : StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        protected override void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "Api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}
