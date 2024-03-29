﻿// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// WCF服务客户端代理
    /// </summary>
    /// <typeparam name="T">服务契约类型</typeparam>
    public sealed class ServiceProxy<T> : ServiceProxy
    {
        #region # 字段及构造器

        /// <summary>
        /// 信道实例
        /// </summary>
        private T _channel;

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ServiceProxy()
        {
            _Sync = new object();
        }

        #endregion

        #region # 只读属性 - 信道 —— T Channel
        /// <summary>
        /// 只读属性 - 信道
        /// </summary>
        public T Channel
        {
            get
            {
                lock (_Sync)
                {
                    if (this._channel != null)
                    {
                        ICommunicationObject communicationObject = (ICommunicationObject)this._channel;
                        if (communicationObject.State == CommunicationState.Opened)
                        {
                            return this._channel;
                        }
                    }

                    ChannelFactory<T> factory = ChannelFactoryManager.Current.GetFactory<T>();
                    this._channel = factory.CreateChannel();

                    return this._channel;
                }
            }
        }
        #endregion

        #region # 关闭客户端信道 —— void Close()
        /// <summary>
        /// 关闭客户端信道
        /// </summary>
        public void Close()
        {
            lock (_Sync)
            {
                this._channel?.CloseChannel();
            }
        }
        #endregion

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            this.Close();
        }
        #endregion
    }
}
