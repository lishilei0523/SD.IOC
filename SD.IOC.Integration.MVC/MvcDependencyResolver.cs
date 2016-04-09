﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SD.IOC.Core.Mediator;

namespace SD.IOC.Integration.MVC
{
    /// <summary>
    /// MVC依赖解决者
    /// </summary>
    public class MvcDependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// 解析支持任意对象创建的一次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务或对象的类型</param>
        /// <returns> 请求的服务或对象 </returns>
        public object GetService(Type serviceType)
        {
            return ResolveMediator.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 解析多次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务的类型</param>
        /// <returns>请求的服务</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ResolveMediator.ResolveAll(serviceType);
        }
    }
}