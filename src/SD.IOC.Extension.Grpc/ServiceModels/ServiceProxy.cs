using System;

namespace SD.IOC.Extension.Grpc.ServiceModels
{
    /// <summary>
    /// gRPC服务客户端代理基类
    /// </summary>
    public abstract class ServiceProxy : IDisposable
    {
        #region # 字段及构造器

        /// <summary>
        /// 信道实例属性名
        /// </summary>
        public const string ChannelPropertyName = "Channel";

        /// <summary>
        /// 同步锁
        /// </summary>
        protected static readonly object _Sync;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ServiceProxy()
        {
            _Sync = new object();
        }

        #endregion

        #region # 释放资源 —— abstract void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void Dispose();
        #endregion
    }
}
