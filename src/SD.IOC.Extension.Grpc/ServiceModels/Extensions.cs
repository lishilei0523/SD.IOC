using Grpc.Core;
using ServiceModel.Grpc.Client;

namespace SD.IOC.Extension.Grpc.ServiceModels
{
    /// <summary>
    /// gRPC扩展
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// 创建gRPC服务
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <param name="channel">gRPC信道</param>
        /// <returns>gRPC服务实例</returns>
        public static T CreateGrpcService<T>(this ChannelBase channel) where T : class
        {
            IClientFactory clientFactory = new ClientFactory();
            T serviceInstance = clientFactory.CreateClient<T>(channel);

            return serviceInstance;
        }
    }
}
