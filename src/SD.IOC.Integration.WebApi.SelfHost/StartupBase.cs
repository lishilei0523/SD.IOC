using Microsoft.Extensions.DependencyInjection;
using Owin;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.SelfHost
{
    /// <summary>
    /// Startup基类
    /// </summary>
    public abstract class StartupBase
    {
        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //初始化容器
            this.InitContainer();
            config.DependencyResolver = new WebApiDependencyResolver();

            this.Configuration(appBuilder, config);

            appBuilder.UseWebApi(config);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        /// <param name="appBuilder">应用程序建造者</param>
        /// <param name="httpConfiguration">Http配置</param>
        protected abstract void Configuration(IAppBuilder appBuilder, HttpConfiguration httpConfiguration);

        /// <summary>
        /// 初始化容器
        /// </summary>
        private void InitContainer()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();
                ResolveMediator.Build();
            }
        }
    }
}
