using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Integration.WCF.Behaviors;
using SD.IOC.Integration.WCF.Tests.Implements;
using System.Configuration;

namespace SD.IOC.Integration.WCF.Tests
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices();

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置WCF服务
            DependencyInjectionBehavior dependencyInjectionBehavior = new DependencyInjectionBehavior();

            appBuilder.UseServiceModel(builder =>
            {
                builder.ConfigureServiceHostBase<ProductService>(host =>
                {
                    host.Description.Behaviors.Add(dependencyInjectionBehavior);
                });
            });
        }
    }
}
