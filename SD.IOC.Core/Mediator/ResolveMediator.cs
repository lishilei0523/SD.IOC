using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using SD.IOC.Core.Configuration;
using SD.IOC.Core.Interfaces;

namespace SD.IOC.Core.Mediator
{
    /// <summary>
    /// 解析中介者
    /// </summary>
    public static class ResolveMediator
    {
        #region # 字段及构造器

        /// <summary>
        /// Autofac依赖注入容器
        /// </summary>
        private static readonly IContainer _Container;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ResolveMediator()
        {
            ContainerBuilder builder = new ContainerBuilder();

            //读取配置文件获取依赖注入提供者
            Assembly cacheImpAssembly = Assembly.Load(InjectionProviderConfiguration.Setting.Assembly);
            Type cacheImplType = cacheImpAssembly.GetType(InjectionProviderConfiguration.Setting.Type);

            builder.RegisterType(cacheImplType).As(typeof(IInstanceResolver)).InstancePerDependency();

            _Container = builder.Build();
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
            IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>();
            return instanceResolver.Resolve<T>();
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
            using (IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>())
            {
                return instanceResolver.Resolve(type);
            }
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
            using (IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>())
            {
                return instanceResolver.ResolveOptional<T>();
            }
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
            using (IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>())
            {
                return instanceResolver.ResolveOptional(type);
            }

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
            using (IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>())
            {
                return instanceResolver.ResolveAll<T>();
            }
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
            using (IInstanceResolver instanceResolver = _Container.Resolve<IInstanceResolver>())
            {
                return instanceResolver.ResolveAll(type);
            }
        }
        #endregion
    }
}
