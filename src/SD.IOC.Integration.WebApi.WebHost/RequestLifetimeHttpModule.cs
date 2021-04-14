using System.Web;
using System.Web.Http;

namespace SD.IOC.Integration.WebApi.WebHost
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
            //ע������ע��
            GlobalConfiguration.Configuration.RegisterDependencyResolver();
        }

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose() { }
    }
}