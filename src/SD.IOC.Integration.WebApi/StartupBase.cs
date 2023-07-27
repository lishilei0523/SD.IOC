using Owin;
using SD.IOC.Integration.WebApi.Extensions;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi
{
    /// <summary>
    /// OWIN启动器基类
    /// </summary>
    public abstract class StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
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
        protected abstract void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration);
    }
}
