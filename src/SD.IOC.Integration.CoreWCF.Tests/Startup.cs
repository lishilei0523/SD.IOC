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
    /// Ӧ�ó���������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ���÷���
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            //���WCF����
            services.AddServiceModelServices();
            services.AddServiceModelMetadata();

            //���WCF����
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //����WCF����
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
