using Owin;
using SD.IOC.Integration.WebApi.Extensions;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.SelfHost
{
    /// <summary>
    /// OWIN启动器基类
    /// </summary>
    public abstract class StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration httpConfiguration = new HttpConfiguration();
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);

            //注册依赖注入
            httpConfiguration.RegisterDependencyResolver();

            this.Configuration(appBuilder, httpConfiguration);

            appBuilder.UseWebApi(httpConfiguration);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        /// <param name="httpConfiguration">Http配置</param>
        protected abstract void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration);
    }
}
