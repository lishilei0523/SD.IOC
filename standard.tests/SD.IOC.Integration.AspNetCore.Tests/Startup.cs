using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore;
using SD.IOC.Extension.NetCore.ServiceModel;
using System;

namespace SD.IOC.Integration.AspNetCore.Tests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }



        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IServiceCollection builder = ResolveMediator.GetServiceCollection();
            foreach (ServiceDescriptor descriptor in services)
            {
                builder.Add(descriptor);
            }

            builder.RegisterConfigs();
            builder.RegisterServiceModels();

            ResolveMediator.Build();

            return ResolveMediator.GetServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
