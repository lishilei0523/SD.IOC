using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SD.IOC.Standard.WcfTools
{
    /// <summary>
    /// ChannelFactory管理者
    /// </summary>
    internal sealed class ChannelFactoryManager : IDisposable
    {
        #region # 字段及构造器

        /// <summary>
        /// 信道工厂幂等字典
        /// </summary>
        private static readonly IDictionary<Type, ChannelFactory> _Factories;

        /// <summary>
        /// 信道工厂管理者单例
        /// </summary>
        private static readonly ChannelFactoryManager _Current;

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ChannelFactoryManager()
        {
            ChannelFactoryManager._Factories = new Dictionary<Type, ChannelFactory>();
            ChannelFactoryManager._Current = new ChannelFactoryManager();
            ChannelFactoryManager._Sync = new object();
        }

        /// <summary>
        /// 私有化构造器
        /// </summary>
        private ChannelFactoryManager() { }

        #endregion

        #region # 访问器 —— static ChannelFactoryManager Current
        /// <summary>
        /// 访问器
        /// </summary>
        public static ChannelFactoryManager Current
        {
            get { return ChannelFactoryManager._Current; }
        }
        #endregion

        #region # 获取给定服务契约类型的ChannelFactory实例 —— ChannelFactory<T> GetFactory<T>()
        /// <summary>
        /// 获取给定服务契约类型的ChannelFactory实例
        /// </summary>
        /// <typeparam name="T">服务契约类型</typeparam>
        /// <returns>给定服务契约类型的ChannelFactory实例</returns>
        public ChannelFactory<T> GetFactory<T>()
        {
            lock (ChannelFactoryManager._Sync)
            {
                ChannelFactory factory = null;
                try
                {
                    if (!ChannelFactoryManager._Factories.TryGetValue(typeof(T), out factory))
                    {
                        factory = new ChannelFactory<T>(typeof(T).FullName);
                        ChannelFactoryManager._Factories.Add(typeof(T), factory);
                    }
                    return factory as ChannelFactory<T>;
                }
                catch
                {
                    if (factory != null)
                    {
                        factory.CloseChannel();
                    }
                    throw;
                }

            }
        }
        #endregion

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            lock (ChannelFactoryManager._Sync)
            {
                foreach (Type type in ChannelFactoryManager._Factories.Keys)
                {
                    ChannelFactory factory = ChannelFactoryManager._Factories[type];
                    if (factory != null)
                    {
                        factory.CloseChannel();
                    }
                }
                ChannelFactoryManager._Factories.Clear();
            }
        }
        #endregion
    }
}
