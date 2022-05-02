using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.Common;
using SD.IOC.Integration.WCF.Behaviors;
using SD.IOC.Integration.WCF.Tests.Implements;
using System.Collections.Generic;
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
            //添加WCF服务
            services.AddServiceModelServices();
            services.AddServiceModelMetadata();

            //添加WCF配置
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //配置WCF服务
            ServiceMetadataBehavior metadataBehavior = appBuilder.ApplicationServices.GetRequiredService<ServiceMetadataBehavior>();
            metadataBehavior.HttpGetEnabled = true;
            metadataBehavior.HttpsGetEnabled = true;
            UseRequestHeadersForMetadataAddressBehavior addressBehavior = new UseRequestHeadersForMetadataAddressBehavior();
            DependencyInjectionBehavior dependencyInjectionBehavior = new DependencyInjectionBehavior();
            IList<IServiceBehavior> serviceBehaviors = new List<IServiceBehavior>
            {
                addressBehavior, dependencyInjectionBehavior
            };

            appBuilder.UseServiceModel(builder =>
            {
                builder.ConfigureServiceHostBase<ProductService>(host => host.Description.Behaviors.AddRange(serviceBehaviors));
            });
        }
    }
}
