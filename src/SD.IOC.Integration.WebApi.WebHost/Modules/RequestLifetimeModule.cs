using SD.IOC.Integration.WebApi.Extensions;
using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.WebHost.Modules
{
    /// <summary>
    /// ASP.NET WebApi����ע��ģ��
    /// </summary>
    internal class RequestLifetimeModule : IHttpModule
    {
        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">Ӧ�ó���������</param>
        public void Init(HttpApplication context)
        {
            //ע������ע��
            GlobalConfiguration.Configuration.RegisterDependencyResolver();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}
