using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using System;
using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi
{
    /// <summary>
    /// ����ע��HttpModule
    /// </summary>
    internal class RequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">Ӧ�ó���������</param>
        public void Init(HttpApplication context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            //��ʼ������
            this.InitContainer();

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new WebApiDependencyResolver();
        }

        /// <summary>
        /// ��ʼ������
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
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}