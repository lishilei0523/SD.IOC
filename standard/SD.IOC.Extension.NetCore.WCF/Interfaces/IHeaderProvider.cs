using System.ServiceModel.Channels;

// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// 消息头提供者
    /// </summary>
    public interface IHeaderProvider
    {
        /// <summary>
        /// 获取消息头列表
        /// </summary>
        /// <returns>消息头列表</returns>
        AddressHeader[] GetHeaders();
    }
}
