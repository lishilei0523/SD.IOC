using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Integration.WCF.Behaviors;
using SD.IOC.Integration.WCF.Tests.Implements;
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
            services.AddServiceModelServices();

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            services.AddServiceModelConfigurationManagerFile(configuration.FilePath);
        }

        /// <summary>
        /// ����Ӧ�ó���
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            //����WCF����
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
