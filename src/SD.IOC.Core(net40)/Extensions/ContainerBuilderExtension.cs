using Autofac;
using SD.IOC.Core.Configurations;
using SD.IOC.Core.Mediators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Extensions;

namespace SD.IOC.Core.Extensions
{
    /// <summary>
    /// 容器建造者扩展
    /// </summary>
    public static class ContainerBuilderExtension
    {
        //Public

        #region # 注册配置 —— static void RegisterConfigs(this ContainerBuilder...
        /// <summary>
        /// 注册配置
        /// </summary>
        /// <param name="builder">容器建造者</param>
        public static void RegisterConfigs(this ContainerBuilder builder)
        {
            RegisterInterfaceAssemblies(builder);
            RegisterBaseAssemblies(builder);
            RegisterSelfAssemblies(builder);
            RegisterInterfaceTypes(builder);
            RegisterBaseTypes(builder);
            RegisterSelfTypes(builder);
            RegisterWcfInterfaces(builder);
        }
        #endregion


        //Private

        #region # 注册接口形式程序集 —— static void RegisterInterfaceAssemblies(ContainerBuilder...
        /// <summary>
        /// 注册接口形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterInterfaceAssemblies(this ContainerBuilder builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsInterfaceAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                {
                    builder.RegisterAssemblyTypes(currentAssembly).AsImplementedInterfaces();
                }
                if (element.LifetimeMode == LifetimeMode.PerSession)
                {
                    builder.RegisterAssemblyTypes(currentAssembly).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
                if (element.LifetimeMode == LifetimeMode.Singleton)
                {
                    builder.RegisterAssemblyTypes(currentAssembly).AsImplementedInterfaces().SingleInstance();
                }
            }
        }
        #endregion

        #region # 注册基类形式程序集 —— static void RegisterBaseAssemblies(ContainerBuilder builder)
        /// <summary>
        /// 注册基类形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterBaseAssemblies(ContainerBuilder builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsBaseAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                IEnumerable<Type> types = currentAssembly.GetTypes().Where(x => !x.IsAbstract && !x.IsInterface);
                foreach (Type type in types)
                {
                    if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                    {
                        builder.RegisterType(type).As(type.BaseType);
                    }
                    if (element.LifetimeMode == LifetimeMode.PerSession)
                    {
                        builder.RegisterType(type).As(type.BaseType).InstancePerLifetimeScope();
                    }
                    if (element.LifetimeMode == LifetimeMode.Singleton)
                    {
                        builder.RegisterType(type).As(type.BaseType).SingleInstance();
                    }
                }
            }
        }
        #endregion

        #region # 注册自身形式程序集 —— static void RegisterSelfAssemblies(ContainerBuilder builder)
        /// <summary>
        /// 注册自身形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterSelfAssemblies(ContainerBuilder builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.AsSelfAssemblies)
            {
                Assembly currentAssembly = Assembly.Load(element.Name.Trim());
                if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                {
                    builder.RegisterAssemblyTypes(currentAssembly);
                }
                if (element.LifetimeMode == LifetimeMode.PerSession)
                {
                    builder.RegisterAssemblyTypes(currentAssembly).InstancePerLifetimeScope();
                }
                if (element.LifetimeMode == LifetimeMode.Singleton)
                {
                    builder.RegisterAssemblyTypes(currentAssembly).SingleInstance();
                }
            }
        }
        #endregion

        #region # 注册接口形式类型 —— static void RegisterInterfaceTypes(ContainerBuilder builder)
        /// <summary>
        /// 注册接口形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterInterfaceTypes(ContainerBuilder builder)
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

                if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                {
                    builder.RegisterType(type).AsImplementedInterfaces();
                }
                if (element.LifetimeMode == LifetimeMode.PerSession)
                {
                    builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
                }
                if (element.LifetimeMode == LifetimeMode.Singleton)
                {
                    builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();
                }
            }
        }
        #endregion

        #region # 注册基类形式类型 —— static void RegisterBaseTypes(ContainerBuilder builder)
        /// <summary>
        /// 注册基类形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterBaseTypes(ContainerBuilder builder)
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

                if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                {
                    builder.RegisterType(type).As(type.BaseType);
                }
                if (element.LifetimeMode == LifetimeMode.PerSession)
                {
                    builder.RegisterType(type).As(type.BaseType).InstancePerLifetimeScope();
                }
                if (element.LifetimeMode == LifetimeMode.Singleton)
                {
                    builder.RegisterType(type).As(type.BaseType).SingleInstance();
                }
            }
        }
        #endregion

        #region # 注册自身形式类型 —— static void RegisterSelfTypes(ContainerBuilder builder)
        /// <summary>
        /// 注册自身形式类型
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterSelfTypes(ContainerBuilder builder)
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

                if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                {
                    builder.RegisterType(type);
                }
                if (element.LifetimeMode == LifetimeMode.PerSession)
                {
                    builder.RegisterType(type).InstancePerLifetimeScope();
                }
                if (element.LifetimeMode == LifetimeMode.Singleton)
                {
                    builder.RegisterType(type).SingleInstance();
                }
            }
        }
        #endregion

        #region # 注册WCF接口列表 —— static void RegisterWcfInterfaces(ContainerBuilder builder)
        /// <summary>
        /// 注册WCF接口列表
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterWcfInterfaces(ContainerBuilder builder)
        {
            foreach (AssemblyElement element in DependencyInjectionSection.Setting.WcfInterfaces)
            {
                //加载程序集
                Assembly wcfInterfaceAssembly = Assembly.Load(element.Name);

                //获取WCF接口类型集
                IEnumerable<Type> types = wcfInterfaceAssembly.GetTypes().Where(type => type.IsInterface);

                //获取服务代理泛型类型
                Type proxyGenericType = typeof(ServiceProxy<>);

                //注册WCF接口
                foreach (Type type in types)
                {
                    Type proxyType = proxyGenericType.MakeGenericType(type);
                    PropertyInfo propChannel = proxyType.GetProperty(ServiceProxy.ChannelPropertyName, type);
                    if (element.LifetimeMode == LifetimeMode.PerCall || element.LifetimeMode == null)
                    {
                        builder.RegisterType(proxyType)
                            .OnRelease(proxy => ((IDisposable)proxy).Dispose());
                        builder.Register(container => propChannel.GetValue(container.Resolve(proxyType), null))
                            .As(type)
                            .OnRelease(channel => channel.CloseChannel());
                    }
                    if (element.LifetimeMode == LifetimeMode.PerSession)
                    {
                        builder.RegisterType(proxyType)
                            .InstancePerLifetimeScope()
                            .OnRelease(proxy => ((IDisposable)proxy).Dispose());
                        builder.Register(container => propChannel.GetValue(container.Resolve(proxyType), null))
                            .InstancePerLifetimeScope()
                            .As(type)
                            .OnRelease(channel => channel.CloseChannel());
                    }
                    if (element.LifetimeMode == LifetimeMode.Singleton)
                    {
                        builder.RegisterType(proxyType)
                            .SingleInstance()
                            .OnRelease(proxy => ((IDisposable)proxy).Dispose());
                        builder.Register(container => propChannel.GetValue(container.Resolve(proxyType), null))
                            .SingleInstance()
                            .As(type)
                            .OnRelease(channel => channel.CloseChannel());
                    }
                }
            }

            ResolveMediator.OnDispose += disposables =>
            {
                foreach (IDisposable disposable in disposables)
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
            };
        }
        #endregion
    }
}
