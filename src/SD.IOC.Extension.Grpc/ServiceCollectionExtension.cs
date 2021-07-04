using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core;
using SD.IOC.Core.Configurations;
using SD.Toolkits.Grpc.Client.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.IOC.Extension.Grpc
{
    /// <summary>
    /// 容器建造者扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        //Public

        #region # 注册gRPC配置 —— static void RegisterGrpcServiceModels(this IServiceCollection...
        /// <summary>
        /// 注册gRPC配置
        /// </summary>
        /// <param name="serviceCollection">容器建造者</param>
        public static void RegisterGrpcServiceModels(this IServiceCollection serviceCollection)
        {
            RegisterGrpcInterfaces(serviceCollection);
        }
        #endregion


        //Private

        #region # 注册gRPC接口列表 —— static void RegisterGrpcInterfaces(IServiceCollection builder)
        /// <summary>
        /// 注册gRPC接口列表
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterGrpcInterfaces(IServiceCollection builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.GrpcInterfaces)
            {
                //加载程序集
                Assembly grpcInterfaceAssembly = Assembly.Load(element.Name);

                //获取gRPC接口类型集
                IEnumerable<Type> types = grpcInterfaceAssembly.GetTypes().Where(type => type.IsInterface);

                //获取服务代理泛型类型
                Type proxyGenericType = typeof(GrpcServiceProxy<>);

                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                //注册gRPC接口
                foreach (Type type in types)
                {
                    Type proxyType = proxyGenericType.MakeGenericType(type);
                    PropertyInfo propChannel = proxyType.GetProperty(GrpcServiceProxy.ChannelPropertyName, type);

                    ServiceDescriptor proxyDescriptor = new ServiceDescriptor(proxyType, proxyType, lifetimeMode);
                    builder.Add(proxyDescriptor);

                    ServiceDescriptor descriptor = new ServiceDescriptor(type, factory => propChannel.GetValue(factory.GetRequiredService(proxyType)), lifetimeMode);
                    builder.Add(descriptor);
                }
            }
        }
        #endregion

        #region # 转换ServiceLifetime —— static ServiceLifetime ToLifetime(this LifetimeMode?...
        /// <summary>
        /// 转换ServiceLifetime
        /// </summary>
        private static ServiceLifetime ToLifetime(this LifetimeMode? lifetimeMode)
        {
            ServiceLifetime serviceLifetime;

            if (lifetimeMode == LifetimeMode.PerCall)
            {
                serviceLifetime = ServiceLifetime.Transient;
            }
            else if (lifetimeMode == LifetimeMode.PerSession)
            {
                serviceLifetime = ServiceLifetime.Scoped;
            }
            else if (lifetimeMode == LifetimeMode.Singleton)
            {
                serviceLifetime = ServiceLifetime.Singleton;
            }
            else
            {
                serviceLifetime = ServiceLifetime.Transient;
            }

            return serviceLifetime;
        }
        #endregion
    }
}
