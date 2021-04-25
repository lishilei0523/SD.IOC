using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetFx;
using SD.IOC.Integration.WCF.Providers;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace SD.IOC.Integration.WCF.Behaviors
{
    /// <summary>
    /// WCF依赖注入行为
    /// </summary>
    internal class DependencyInjectionBehavior : IServiceBehavior
    {
        #region # 适用依赖注入 —— void ApplyDispatchBehavior(ServiceDescription serviceDescription...
        /// <summary>
        /// 适用依赖注入
        /// </summary>
        /// <param name="serviceDescription">服务描述</param>
        /// <param name="serviceHostBase">服务主机</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            //初始化容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();
                ResolveMediator.Build();
            }

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
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }
}
