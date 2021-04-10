using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SD.IOC.Integration.AspNetCore.Tests
{
    public class Startup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        /// <summary>
        /// 配置应用程序
        /// </summary>
        public void Configure(IApplicationBuilder appBuilder)
        {
            appBuilder.UseRouting();
            appBuilder.UseEndpoints(routeBuilder => routeBuilder.MapControllers());
        }
    }
}
