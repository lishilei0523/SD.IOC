using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core;
using SD.IOC.Core.Configurations;
using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Extensions;

namespace SD.IOC.Extension.Xamarin.ServiceModel
{
    /// <summary>
    /// 容器建造者扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        //Public

        #region # 注册WCF配置 —— static void RegisterServiceModels(this IServiceCollection...
        /// <summary>
        /// 注册WCF配置
        /// </summary>
        /// <param name="serviceCollection">容器建造者</param>
        public static void RegisterServiceModels(this IServiceCollection serviceCollection)
        {
            RegisterWcfInterfaces(serviceCollection);
        }
        #endregion


        //Private

        #region # 注册WCF接口列表 —— static void RegisterWcfInterfaces(IServiceCollection builder)
        /// <summary>
        /// 注册WCF接口列表
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterWcfInterfaces(IServiceCollection builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.WcfInterfaces)
            {
                //加载程序集
                Assembly wcfInterfaceAssembly = Assembly.Load(element.Name);

                //获取WCF接口类型集
                IEnumerable<Type> types = wcfInterfaceAssembly.GetTypes().Where(type => type.IsInterface);

                //获取服务代理泛型类型
                Type proxyGenericType = typeof(ServiceProxy<>);

                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                //注册WCF接口
                foreach (Type type in types)
                {
                    Type proxyType = proxyGenericType.MakeGenericType(type);
                    PropertyInfo propChannel = proxyType.GetProperty(ServiceProxy.ChannelPropertyName, type);

                    ServiceDescriptor proxyDescriptor = new ServiceDescriptor(proxyType, proxyType, lifetimeMode);
                    builder.Add(proxyDescriptor);

                    ServiceDescriptor descriptor = new ServiceDescriptor(type, factory => propChannel.GetValue(factory.GetRequiredService(proxyType)), lifetimeMode);
                    builder.Add(descriptor);
                }
            }

            ResolveMediator.OnDispose += disposables =>
            {
                foreach (IDisposable disposable in disposables)
                {
                    if (disposable is ICommunicationObject)
                    {
                        try
                        {
                            disposable.Dispose();
                        }
                        finally
                        {
                            disposable.CloseChannel();
                        }
                    }
                }
            };
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
