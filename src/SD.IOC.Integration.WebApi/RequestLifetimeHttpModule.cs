using System;
using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi
{
    /// <summary>
    /// 依赖注入HttpModule
    /// </summary>
    internal class RequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">应用程序上下文</param>
        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new WebApiDependencyResolver();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { }
    }
}