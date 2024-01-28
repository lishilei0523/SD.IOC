using SD.IOC.Integration.WCF.Providers;
using System.Collections.ObjectModel;
#if NET40_OR_GREATER
using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFramework;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Description;
using CoreWCF.Dispatcher;
#endif

namespace SD.IOC.Integration.WCF.Behaviors
{
    /// <summary>
    /// WCF依赖注入行为
    /// </summary>
    public class DependencyInjectionBehavior : IServiceBehavior
    {
        #region # 适用依赖注入 —— void ApplyDispatchBehavior(ServiceDescription serviceDescription...
        /// <summary>
        /// 适用依赖注入
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务主机</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
#if NET40_OR_GREATER
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();
                ResolveMediator.Build();
            }
#endif
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher dispatcher = (ChannelDispatcher)channelDispatcherBase;
                foreach (EndpointDispatcher endpoint in dispatcher.Endpoints)
                {
                    if (!endpoint.IsSystemEndpoint)
                    {
                        endpoint.DispatchRuntime.InstanceProvider = new ServiceInstanceProvider(serviceDescription.ServiceType);
                    }
                }
            }
        }
        #endregion


        //没有用

        /// <summary>
        /// 添加绑定参数
        /// </summary>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// 验证
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
    }
}
