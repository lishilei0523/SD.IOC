using System;
using System.Web;
using System.Web.Mvc;

namespace SD.IOC.Integration.MVC
{
    /// <summary>
    /// ����ע��HttpModule
    /// </summary>
    internal class RequestLifetimeHttpModule : IHttpModule
    {
        /// <summary>
        /// MVC����������
        /// </summary>
        private readonly MvcDependencyResolver _dependencyResolver;

        /// <summary>
        /// ������
        /// </summary>
        public RequestLifetimeHttpModule()
        {
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
                throw new ArgumentNullException("context");
            }

            DependencyResolver.SetResolver(this._dependencyResolver);
            context.EndRequest += this.OnEndRequest;
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