using Microsoft.Owin.Hosting;
using System;

namespace SD.IOC.Integration.WebApi.SelfHost.Tests
{
    /// <summary>
    /// 服务启动器
    /// </summary>
    public class ServiceLauncher
    {
        private IDisposable _webApp;

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            StartOptions startOptions = new StartOptions();
            startOptions.Urls.Add("http://localhost:33101");

            //开启服务
            this._webApp = WebApp.Start<Startup>(startOptions);

            Console.WriteLine("服务已启动...");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            //关闭服务 
            this._webApp.Dispose();

            Console.WriteLine("服务已关闭...");
        }
    }
}
