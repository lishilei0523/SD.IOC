using SD.IOC.Integration.WebApi.Extensions;
using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.WebHost.Modules
{
    /// <summary>
    /// ASP.NET WebApi依赖注入模块
    /// </summary>
    internal class RequestLifetimeModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        public void Init(HttpApplication context)
        {
            //注册依赖注入
            GlobalConfiguration.Configuration.RegisterDependencyResolver();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { }
    }
}
