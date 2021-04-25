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
    /// ����ע��ģ��
    /// </summary>
    internal class RequestLifetimeModule : IHttpModule
    {
        /// <summary>
        /// MVC����������
        /// </summary>
        private readonly MvcDependencyResolver _dependencyResolver;

        /// <summary>
        /// ������
        /// </summary>
        public RequestLifetimeModule()
        {
            //��ʼ������
            this.InitContainer();

            this._dependencyResolver = new MvcDependencyResolver();
        }

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

            DependencyResolver.SetResolver(this._dependencyResolver);
            context.EndRequest += this.OnEndRequest;
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
        /// ��������¼�
        /// </summary>
        private void OnEndRequest(object sender, EventArgs e)
        {
            this._dependencyResolver.ReleaseService();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}