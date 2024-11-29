using Grpc.Core;
using ServiceModel.Grpc.Client;
using ServiceModel.Grpc.Configuration;

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
            ServiceModelGrpcClientOptions clientOptions = new ServiceModelGrpcClientOptions
            {
                MarshallerFactory = MessagePackMarshallerFactory.Default
            };
            IClientFactory clientFactory = new ClientFactory(clientOptions);
            T serviceInstance = clientFactory.CreateClient<T>(channel);

            return serviceInstance;
        }
    }
}
