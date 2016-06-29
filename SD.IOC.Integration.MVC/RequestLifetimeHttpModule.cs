using System;
using System.Web;
using System.Web.Mvc;
using SD.IOC.Core.Mediator;

namespace SD.IOC.Integration.MVC
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
                throw new ArgumentNullException("context");
            }

            DependencyResolver.SetResolver(new MvcDependencyResolver());
            context.EndRequest += this.OnEndRequest;
        }


        /// <summary>
        /// ��������¼�
        /// </summary>
        private void OnEndRequest(object sender, EventArgs e)
        {
            ResolveMediator.Dispose();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}