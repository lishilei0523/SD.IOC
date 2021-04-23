using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core;
using SD.IOC.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Extensions;

namespace SD.IOC.Extension.NetFx
{
    /// <summary>
    /// 容器建造者扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        //Public

        #region # 注册配置 —— static void RegisterConfigs(this IServiceCollection...
        /// <summary>
        /// 注册配置
        /// </summary>
        /// <param name="serviceCollection">容器建造者</param>
        public static void RegisterConfigs(this IServiceCollection serviceCollection)
        {
            RegisterInterfaceAssemblies(serviceCollection);
            RegisterBaseAssemblies(serviceCollection);
            RegisterSelfAssemblies(serviceCollection);
            RegisterInterfaceTypes(serviceCollection);
            RegisterBaseTypes(serviceCollection);
            RegisterSelfTypes(serviceCollection);
            RegisterWcfInterfaces(serviceCollection);
        }
        #endregion


        //Private

        #region # 注册接口形式程序集 —— static void RegisterInterfaceAssemblies(IServiceCollection...
        /// <summary>
        /// 注册接口形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterInterfaceAssemblies(this IServiceCollection builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsInterfaceAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                IEnumerable<Type> types = currentAssembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface);
                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                foreach (Type type in types)
                {
                    foreach (Type @interface in type.GetInterfaces())
                    {
                        ServiceDescriptor descriptor = new ServiceDescriptor(@interface, type, lifetimeMode);
                        builder.Add(descriptor);
                    }
                }
            }
        }
        #endregion

        #region # 注册基类形式程序集 —— static void RegisterBaseAssemblies(IServiceCollection builder)
        /// <summary>
        /// 注册基类形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterBaseAssemblies(IServiceCollection builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsBaseAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                IEnumerable<Type> types = currentAssembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface);
                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                foreach (Type type in types)
                {
                    ServiceDescriptor descriptor = new ServiceDescriptor(type.BaseType, type, lifetimeMode);
                    builder.Add(descriptor);
                }
            }
        }
        #endregion

        #region # 注册自身形式程序集 —— static void RegisterSelfAssemblies(IServiceCollection builder)
        /// <summary>
        /// 注册自身形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterSelfAssemblies(IServiceCollection builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsSelfAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                IEnumerable<Type> types = currentAssembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface);
                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                foreach (Type type in types)
                {
                    ServiceDescriptor descriptor = new ServiceDescriptor(type, type, lifetimeMode);
                    builder.Add(descriptor);
                }
            }
        }
        #endregion

        #region # 注册接口形式类型 —— static void RegisterInterfaceTypes(IServiceCollection builder)
        /// <summary>
        /// 注册接口形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterInterfaceTypes(IServiceCollection builder)
        {
            foreach (TypeElement element in DependencyInjectionSection.Setting.AsInterfaceTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException($"程序集\"{element.Assembly.Trim()}\"中不存在类型\"{element.Name.Trim()}\"！");
                }

                #endregion

                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();

                foreach (Type @interface in type.GetInterfaces())
                {
                    ServiceDescriptor descriptor = new ServiceDescriptor(@interface, type, lifetimeMode);
                    builder.Add(descriptor);
                }
            }
        }
        #endregion

        #region # 注册基类形式类型 —— static void RegisterBaseTypes(IServiceCollection builder)
        /// <summary>
        /// 注册基类形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterBaseTypes(IServiceCollection builder)
        {
            foreach (TypeElement element in DependencyInjectionSection.Setting.AsBaseTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException($"程序集\"{element.Assembly.Trim()}\"中不存在类型\"{element.Name.Trim()}\"！");
                }

                #endregion

                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();
                ServiceDescriptor descriptor = new ServiceDescriptor(type.BaseType, type, lifetimeMode);
                builder.Add(descriptor);
            }
        }
        #endregion

        #region # 注册自身形式类型 —— static void RegisterSelfTypes(IServiceCollection builder)
        /// <summary>
        /// 注册自身形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterSelfTypes(IServiceCollection builder)
        {
            foreach (TypeElement element in DependencyInjectionSection.Setting.AsSelfTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException($"程序集\"{element.Assembly.Trim()}\"中不存在类型\"{element.Name.Trim()}\"！");
                }

                #endregion

                ServiceLifetime lifetimeMode = element.LifetimeMode.ToLifetime();
                ServiceDescriptor descriptor = new ServiceDescriptor(type, type, lifetimeMode);
                builder.Add(descriptor);
            }
        }
        #endregion

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
