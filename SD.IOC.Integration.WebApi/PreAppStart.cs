﻿using System.ComponentModel;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace SD.IOC.Integration.WebApi
{
    /// <summary>
    /// WebApi应用程序依赖注入
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class PreAppStart
    {
        /// <summary>
        /// 是否已初始化
        /// </summary>
        private static bool _InitWasCalled;

        /// <summary>
        /// 初始化依赖注入
        /// </summary>
        public static void InitInjection()
        {
            if (!_InitWasCalled)
            {
                _InitWasCalled = true;
                DynamicModuleUtility.RegisterModule(typeof(RequestLifetimeHttpModule));
            }
        }
    }
}
