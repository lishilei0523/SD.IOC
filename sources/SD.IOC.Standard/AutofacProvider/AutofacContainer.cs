using Autofac;
using SD.IOC.Standard.Configuration;
using SD.IOC.Standard.WcfTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SD.IOC.Standard.AutofacProvider
{
    /// <summary>
    /// Autofac依赖注入容器
    /// </summary>
    internal static class AutofacContainer
    {
        #region # 访问器 —— static IContainer Current
        /// <summary>
        /// 访问器
        /// </summary>
        public static IContainer Current
        {
            get { return AutofacContainer._Container; }
        }
        #endregion

        #region # Autofac IOC容器实例 —— static readonly IContainer _Container
        /// <summary>
        /// Autofac IOC容器实例
        /// </summary>
        private static readonly IContainer _Container;
        #endregion

        #region # 静态构造器
        /// <summary>
        /// 静态构造器
        /// </summary>
        static AutofacContainer()
        {
            //实例化容器建造者
            ContainerBuilder builder = new ContainerBuilder();

            AutofacContainer.RegisterInterfaceAssemblies(builder);
            AutofacContainer.RegisterBaseAssemblies(builder);
            AutofacContainer.RegisterSelfAssemblies(builder);
            AutofacContainer.RegisterInterfaceTypes(builder);
            AutofacContainer.RegisterBaseTypes(builder);
            AutofacContainer.RegisterSelfTypes(builder);

            //得到容器对象
            AutofacContainer._Container = builder.Build();
        }
        #endregion

        #region # 注册接口形式程序集 —— static void RegisterInterfaceAssemblies(ContainerBuilder...
        /// <summary>
        /// 注册接口形式程序集
        /// </summary>
        /// <param name="builder">容器建造者</param>
        private static void RegisterInterfaceAssemblies(ContainerBuilder builder)
        {
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsInterfaceAssemblies)
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsBaseAssemblies)
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.AsSelfAssemblies)
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsInterfaceTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException(string.Format("程序集\"{0}\"中不存在类型\"{1}\"！", element.Assembly.Trim(), element.Name.Trim()));
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsBaseTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException(string.Format("程序集\"{0}\"中不存在类型\"{1}\"！", element.Assembly.Trim(), element.Name.Trim()));
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
            foreach (TypeElement element in InjectionRegisterConfiguration.Setting.AsSelfTypes)
            {
                Assembly currentAssembly = Assembly.Load(element.Assembly.Trim());
                Type type = currentAssembly.GetType(element.Name.Trim());

                #region # 验证类型

                if (type == null)
                {
                    throw new NullReferenceException(string.Format("程序集\"{0}\"中不存在类型\"{1}\"！", element.Assembly.Trim(), element.Name.Trim()));
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
            foreach (AssemblyElement element in InjectionRegisterConfiguration.Setting.WcfInterfaces)
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

                    builder.RegisterType(proxyType).OnRelease(proxy => ((IDisposable)proxy).Dispose());
                    builder.Register(container => propChannel.GetValue(container.Resolve(proxyType))).
                        As(type).
                        OnRelease(channel => channel.CloseChannel());
                }
            }
        }
        #endregion
    }
}
