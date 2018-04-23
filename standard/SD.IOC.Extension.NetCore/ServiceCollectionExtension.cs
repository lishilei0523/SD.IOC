using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.IOC.Extension.NetCore
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsInterfaceAssemblies)
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsBaseAssemblies)
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsSelfAssemblies)
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsInterfaceTypes)
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsBaseTypes)
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsSelfTypes)
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
