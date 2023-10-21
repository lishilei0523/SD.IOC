// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// WCF服务客户端代理基类
    /// </summary>
    public abstract class ServiceProxy : IDisposable
    {
        #region # 信道实例属性名 —— string ChannelPropertyName
        /// <summary>
        /// 信道实例属性名
        /// </summary>
        public const string ChannelPropertyName = "Channel";
        #endregion

        #region # 释放资源 —— abstract void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void Dispose();
        #endregion
    }
}
