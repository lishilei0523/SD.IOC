﻿using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using SD.IOC.Core.Mediator;

namespace SD.IOC.Integration.WebApi
{
    /// <summary>
    /// WebApi依赖解析者
    /// </summary>
    public class WebApiDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// 解析支持任意对象创建的一次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务或对象的类型</param>
        /// <returns> 请求的服务或对象 </returns>
        public virtual object GetService(Type serviceType)
        {
            return ResolveMediator.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 解析多次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务的类型</param>
        /// <returns>请求的服务</returns>
        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            return ResolveMediator.ResolveAll(serviceType);
        }

        /// <summary>
        /// Starts a resolution scope. 
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        public virtual IDependencyScope BeginScope()
        {
            return this;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            ResolveMediator.Dispose();
        }
    }
}