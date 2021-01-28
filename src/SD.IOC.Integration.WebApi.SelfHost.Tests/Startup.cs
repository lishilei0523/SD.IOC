using Microsoft.Owin;
using Owin;
using SD.IOC.Integration.WebApi.SelfHost.Tests;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace SD.IOC.Integration.WebApi.SelfHost.Tests
{
    public class Startup : StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        /// <param name="httpConfiguration">Http配置</param>
        protected override void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Routes.MapHttpRoute(
                "DefaultApi",
                "{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}
