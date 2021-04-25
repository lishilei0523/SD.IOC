using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using SD.IOC.Integration.WebApi.DependencyResolvers;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.Extensions
{
    /// <summary>
    /// Http配置扩展
    /// </summary>
    public static class HttpConfigurationExtension
    {
        /// <summary>
        /// 注册依赖注入解析者
        /// </summary>
        /// <param name="httpConfiguration">Http配置</param>
        public static void RegisterDependencyResolver(this HttpConfiguration httpConfiguration)
        {
            //初始化依赖注入容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();
                ResolveMediator.Build();
            }

            httpConfiguration.DependencyResolver = new WebApiDependencyResolver();
        }
    }
}
