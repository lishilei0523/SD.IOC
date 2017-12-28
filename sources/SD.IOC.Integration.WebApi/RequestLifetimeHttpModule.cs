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
                throw new ArgumentNullException("context");
            }

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new WebApiDependencyResolver();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}