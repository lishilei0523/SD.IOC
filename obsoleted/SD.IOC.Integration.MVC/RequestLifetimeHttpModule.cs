using System;
using System.Web;
using System.Web.Mvc;

namespace SD.IOC.Integration.MVC
{
    /// <summary>
    /// 依赖注入HttpModule
    /// </summary>
    internal class RequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        /// MVC依赖解析者
        /// </summary>
        private readonly MvcDependencyResolver _dependencyResolver;

        /// <summary>
        /// 构造器
        /// </summary>
        public RequestLifetimeHttpModule()
        {
            this._dependencyResolver = new MvcDependencyResolver();
        }

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

            DependencyResolver.SetResolver(this._dependencyResolver);
            context.EndRequest += this.OnEndRequest;
        }

        /// <summary>
        /// 请求结束事件
        /// </summary>
        private void OnEndRequest(object sender, EventArgs e)
        {
            this._dependencyResolver.ReleaseService();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose() { }
    }
}