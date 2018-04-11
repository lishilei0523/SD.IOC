using SD.IOC.Core.Configuration;
using SD.IOC.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace SD.IOC.Core.Mediator
{
    /// <summary>
    /// 实例解析者实例
    /// </summary>
    internal class InstanceProvider : IDisposable
    {
        #region # 常量、字段及构造器

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 程序集名称
        /// </summary>
        private static readonly string _AssemblyName;

        /// <summary>
        /// 类型全名
        /// </summary>
        private static readonly string _TypeFullName;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static InstanceProvider()
        {
            _Sync = new object();

            //读取配置文件获取依赖注入提供者
            _AssemblyName = InjectionProviderConfiguration.Setting.Assembly;
            _TypeFullName = InjectionProviderConfiguration.Setting.Type;
        }

        /// <summary>
        /// 实例解析者
        /// </summary>
        private readonly IInstanceResolver _instanceResolver;

        /// <summary>
        /// 私有构造器
        /// </summary>
        private InstanceProvider()
        {
            //读取配置文件获取依赖注入提供者
            Assembly impAssembly = Assembly.Load(_AssemblyName);
            Type implType = impAssembly.GetType(_TypeFullName);

            this._instanceResolver = (IInstanceResolver)Activator.CreateInstance(implType);
        }

        #endregion

        #region # 访问器 —— static ResolveMediator Current
        /// <summary>
        /// 访问器
        /// </summary>
        public static InstanceProvider Current
        {
            get
            {
                lock (_Sync)
                {
                    LocalDataStoreSlot slot = Thread.GetNamedDataSlot(typeof(InstanceProvider).FullName);

                    if (!(Thread.GetData(slot) is InstanceProvider provider))
                    {
                        provider = new InstanceProvider();
                        Thread.SetData(slot, provider);
                    }

                    return provider;
                }
            }
        }
        #endregion

        #region # 解析实例 —— T Resolve<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例</returns>
        public T Resolve<T>()
        {
            return this._instanceResolver.Resolve<T>();
        }
        #endregion

        #region # 解析实例 —— object Resolve(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例</returns>
        public object Resolve(Type type)
        {
            return this._instanceResolver.Resolve(type);
        }
        #endregion

        #region # 解析实例 —— T ResolveOptional<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例，如未注册则返回null</returns>
        public T ResolveOptional<T>() where T : class
        {
            return this._instanceResolver.ResolveOptional<T>();
        }
        #endregion

        #region # 解析实例 —— object ResolveOptional(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例，如未注册则返回null</returns>
        public object ResolveOptional(Type type)
        {
            return this._instanceResolver.ResolveOptional(type);
        }
        #endregion

        #region # 解析实例集 —— IEnumerable<T> ResolveAll<T>()
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例集</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return this._instanceResolver.ResolveAll<T>();
        }
        #endregion

        #region # 解析实例集 —— IEnumerable<object> ResolveAll(Type type)
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例集</returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            return this._instanceResolver.ResolveAll(type);
        }
        #endregion

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this._instanceResolver != null)
            {
                this._instanceResolver.Dispose();
                Thread.FreeNamedDataSlot(typeof(InstanceProvider).FullName);
            }
        }
        #endregion
    }
}
