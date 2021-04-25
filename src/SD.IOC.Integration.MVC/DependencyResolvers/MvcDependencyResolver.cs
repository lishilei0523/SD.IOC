using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SD.IOC.Integration.MVC.DependencyResolvers
{
    /// <summary>
    /// MVC依赖解析者
    /// </summary>
    public sealed class MvcDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// 获取服务契约实例事件
        /// </summary>
        public static event Action OnGetInstance;

        /// <summary>
        /// 销毁服务契约实例事件
        /// </summary>
        public static event Action OnReleaseInstance;

        /// <summary>
        /// 解析支持任意对象创建的一次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务或对象的类型</param>
        /// <returns> 请求的服务或对象 </returns>
        public object GetService(Type serviceType)
        {
            if (OnGetInstance != null)
            {
                OnGetInstance.Invoke();
            }

            return ResolveMediator.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 解析多次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务的类型</param>
        /// <returns>请求的服务</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (OnGetInstance != null)
            {
                OnGetInstance.Invoke();
            }

            return ResolveMediator.ResolveAll(serviceType);
        }

        /// <summary>
        /// 清理服务
        /// </summary>
        public void ReleaseService()
        {
            if (OnReleaseInstance != null)
            {
                OnReleaseInstance.Invoke();
            }

            ResolveMediator.Dispose();
        }
    }
}