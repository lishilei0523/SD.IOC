﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SD.IOC.Core.Mediators
{
    /// <summary>
    /// 解析中介者
    /// </summary>
    public static class ResolveMediator
    {
        #region # 字段及静态构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 容器建造者
        /// </summary>
        private static readonly IServiceCollection _ServiceCollection;

        /// <summary>
        /// 范围容器线程缓存
        /// </summary>
        private static readonly AsyncLocal<IServiceScope> _ServiceScope;

        /// <summary>
        /// 容器
        /// </summary>
        private static IServiceProvider _ServiceProvider;

        /// <summary>
        /// 容器是否已初始化
        /// </summary>
        private static bool _ContainerBuilt;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ResolveMediator()
        {
            ResolveMediator._Sync = new object();
            ResolveMediator._ServiceCollection = new ServiceCollection();
            ResolveMediator._ServiceScope = new AsyncLocal<IServiceScope>();
            ResolveMediator._ContainerBuilt = false;
        }

        #endregion

        #region # 容器是否已初始化 —— static bool ContainerBuilt
        /// <summary>
        /// 容器是否已初始化
        /// </summary>
        public static bool ContainerBuilt
        {
            get { return _ContainerBuilt; }
        }
        #endregion

        #region # 初始化建造容器 —— static void Build()
        /// <summary>
        /// 初始化建造容器
        /// </summary>
        public static void Build()
        {
            lock (ResolveMediator._Sync)
            {
                ResolveMediator._ServiceProvider = ResolveMediator._ServiceCollection.BuildServiceProvider();
                ResolveMediator._ContainerBuilt = true;
            }
        }
        #endregion

        #region # 获取容器建造者 —— static IServiceCollection GetServiceCollection()
        /// <summary>
        /// 获取容器建造者
        /// </summary>
        /// <returns>Service集合</returns>
        public static IServiceCollection GetServiceCollection()
        {
            if (ResolveMediator._ContainerBuilt)
            {
                throw new InvalidOperationException("容器已初始化，不可获取容器建造者！");
            }

            return ResolveMediator._ServiceCollection;
        }
        #endregion

        #region # 获取容器 —— static IServiceProvider GetServiceProvider()
        /// <summary>
        /// 获取容器
        /// </summary>
        /// <returns>容器</returns>
        public static IServiceProvider GetServiceProvider()
        {
            if (!ResolveMediator._ContainerBuilt)
            {
                throw new InvalidOperationException("容器未初始化！");
            }

            return ResolveMediator._ServiceProvider;
        }
        #endregion

        #region # 获取范围容器 —— static IServiceScope GetServiceScope()
        /// <summary>
        /// 获取范围容器
        /// </summary>
        /// <returns>范围容器</returns>
        public static IServiceScope GetServiceScope()
        {
            IServiceProvider serviceProvider = ResolveMediator.GetServiceProvider();

            if (ResolveMediator._ServiceScope.Value == null)
            {
                ResolveMediator._ServiceScope.Value = serviceProvider.CreateScope();
            }

            return ResolveMediator._ServiceScope.Value;
        }
        #endregion


        #region # 解析实例 —— static T Resolve<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例</returns>
        public static T Resolve<T>()
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetRequiredService<T>();
        }
        #endregion

        #region # 解析实例 —— static object Resolve(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例</returns>
        public static object Resolve(Type type)
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetRequiredService(type);
        }
        #endregion

        #region # 解析实例 —— static T ResolveOptional<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例，如未注册则返回null</returns>
        public static T ResolveOptional<T>() where T : class
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetService<T>();
        }
        #endregion

        #region # 解析实例 —— static object ResolveOptional(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例，如未注册则返回null</returns>
        public static object ResolveOptional(Type type)
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetService(type);
        }
        #endregion

        #region # 解析实例集 —— static IEnumerable<T> ResolveAll<T>()
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例集</returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetServices<T>();
        }
        #endregion

        #region # 解析实例集 —— static IEnumerable<object> ResolveAll(Type type)
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例集</returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            IServiceScope serviceScope = ResolveMediator.GetServiceScope();

            return serviceScope.ServiceProvider.GetServices(type);
        }
        #endregion


        #region # 释放资源 —— static void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            if (ResolveMediator._ServiceScope.Value != null)
            {
                ResolveMediator._ServiceScope.Value.Dispose();
            }
        }
        #endregion
    }
}
