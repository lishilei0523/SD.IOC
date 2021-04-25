using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using SD.IOC.Integration.MVC.DependencyResolvers;
using System;
using System.Web;
using System.Web.Mvc;

namespace SD.IOC.Integration.MVC.Modules
{
    /// <summary>
    /// 依赖注入模块
    /// </summary>
    internal class RequestLifetimeModule : IHttpModule
    {
        /// <summary>
        /// MVC依赖解析者
        /// </summary>
        private readonly MvcDependencyResolver _dependencyResolver;

        /// <summary>
        /// 构造器
        /// </summary>
        public RequestLifetimeModule()
        {
            //初始化容器
            this.InitContainer();

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
                throw new ArgumentNullException(nameof(context));
            }

            DependencyResolver.SetResolver(this._dependencyResolver);
            context.EndRequest += this.OnEndRequest;
        }


        /// <summary>
        /// 初始化容器
        /// </summary>
        private void InitContainer()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();
                ResolveMediator.Build();
            }
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