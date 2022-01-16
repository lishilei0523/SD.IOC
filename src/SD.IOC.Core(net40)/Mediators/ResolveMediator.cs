using Autofac;
using Autofac.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SD.IOC.Core.Mediators
{
    /// <summary>
    /// 解析中介者
    /// </summary>
    public static class ResolveMediator
    {
        #region # 字段及构造器

        /// <summary>
        /// 范围容器已释放事件
        /// </summary>
        public static event Action<IList<IDisposable>> OnDispose;

        /// <summary>
        /// 同步锁
        /// </summary>
        private static readonly object _Sync;

        /// <summary>
        /// 容器建造者
        /// </summary>
        private static readonly ContainerBuilder _ContainerBuilder;

        /// <summary>
        /// 范围容器线程缓存
        /// </summary>
        private static readonly AsyncLocal<ILifetimeScope> _LifetimeScope;

        /// <summary>
        /// 容器
        /// </summary>
        private static IContainer _Container;

        /// <summary>
        /// 容器是否已初始化
        /// </summary>
        private static bool _ContainerBuilt;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static ResolveMediator()
        {
            _Sync = new object();
            _ContainerBuilder = new ContainerBuilder();
            _LifetimeScope = new AsyncLocal<ILifetimeScope>();
            _ContainerBuilt = false;
        }

        #endregion


        #region # 容器是否已初始化 —— static bool ContainerBuilt
        /// <summary>
        /// 容器是否已初始化
        /// </summary>
        public static bool ContainerBuilt
        {
            get { return _ContainerBuilt; }
        }
        #endregion

        #region # 初始化建造容器 —— static void Build()
        /// <summary>
        /// 初始化建造容器
        /// </summary>
        public static void Build()
        {
            lock (_Sync)
            {
                _Container = _ContainerBuilder.Build();
                _ContainerBuilt = true;
            }
        }
        #endregion

        #region # 获取容器建造者 —— static ContainerBuilder GetContainerBuilder()
        /// <summary>
        /// 获取容器建造者
        /// </summary>
        /// <returns>Service集合</returns>
        public static ContainerBuilder GetContainerBuilder()
        {
            if (_ContainerBuilt)
            {
                throw new InvalidOperationException("容器已初始化，不可获取容器建造者！");
            }

            return _ContainerBuilder;
        }
        #endregion

        #region # 获取容器 —— static IContainer GetContainer()
        /// <summary>
        /// 获取容器
        /// </summary>
        /// <returns>容器</returns>
        public static IContainer GetContainer()
        {
            if (!_ContainerBuilt)
            {
                throw new InvalidOperationException("容器未初始化！");
            }

            return _Container;
        }
        #endregion

        #region # 获取范围容器 —— static ILifetimeScope GetLifetimeScope()
        /// <summary>
        /// 获取范围容器
        /// </summary>
        /// <returns>范围容器</returns>
        private static ILifetimeScope GetLifetimeScope()
        {
            lock (_Sync)
            {
                if (_LifetimeScope.Value.Disposed())
                {
                    IContainer container = GetContainer();
                    _LifetimeScope.Value = container.BeginLifetimeScope();
                }

                return _LifetimeScope.Value;
            }
        }
        #endregion

        #region # 获取范围容器可释放对象列表 —— static IList<IDisposable> GetLifetimeScopeDisposables()
        /// <summary>
        /// 获取范围容器可释放对象列表
        /// </summary>
        /// <returns>可释放对象列表</returns>
        public static IList<IDisposable> GetLifetimeScopeDisposables()
        {
            return GetDisposableInstances(_LifetimeScope.Value);
        }
        #endregion

        #region # 范围容器是否已被释放 —— static bool Disposed(this ILifetimeScope lifetimeScope)
        /// <summary>
        /// 范围容器是否已被释放
        /// </summary>
        /// <param name="lifetimeScope">范围容器</param>
        /// <returns>是否已被释放</returns>
        public static bool Disposed(this ILifetimeScope lifetimeScope)
        {
            #region # 验证

            if (lifetimeScope == null)
            {
                return true;
            }

            #endregion

            Type type = lifetimeScope.GetType();

            const string disposedPropertyName = "IsDisposed";
            PropertyInfo disposedProperty = type.GetProperty(disposedPropertyName, BindingFlags.NonPublic | BindingFlags.Instance);
            object value = disposedProperty.GetValue(lifetimeScope, null);
            bool disposed = (bool)value;

            return disposed;
        }
        #endregion


        #region # 解析实例 —— static T Resolve<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例</returns>
        public static T Resolve<T>()
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            return lifetimeScope.Resolve<T>();
        }
        #endregion

        #region # 解析实例 —— static object Resolve(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例</returns>
        public static object Resolve(Type type)
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            return lifetimeScope.Resolve(type);
        }
        #endregion

        #region # 解析实例 —— static T ResolveOptional<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例，如未注册则返回null</returns>
        public static T ResolveOptional<T>() where T : class
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            return lifetimeScope.ResolveOptional<T>();
        }
        #endregion

        #region # 解析实例 —— static object ResolveOptional(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例，如未注册则返回null</returns>
        public static object ResolveOptional(Type type)
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            return lifetimeScope.ResolveOptional(type);
        }
        #endregion

        #region # 解析实例集 —— static IEnumerable<T> ResolveAll<T>()
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例集</returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            return lifetimeScope.Resolve<IEnumerable<T>>();
        }
        #endregion

        #region # 解析实例集 —— static IEnumerable<object> ResolveAll(Type type)
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例集</returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            ILifetimeScope lifetimeScope = GetLifetimeScope();

            Type genericType = typeof(IEnumerable<>).MakeGenericType(type);
            if (lifetimeScope.TryResolve(genericType, out object instance))
            {
                return ((IEnumerable)instance).Cast<object>();
            }

            return Enumerable.Empty<object>();
        }
        #endregion


        #region # 释放资源 —— static void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            lock (_Sync)
            {
                if (_LifetimeScope.Value != null)
                {
                    Dispose(_LifetimeScope.Value);
                    _LifetimeScope.Value = null;
                }
            }
        }
        #endregion

        #region # 释放范围容器 —— static void Dispose(ILifetimeScope lifetimeScope)
        /// <summary>
        /// 释放范围容器
        /// </summary>
        /// <param name="lifetimeScope">范围容器</param>
        private static void Dispose(ILifetimeScope lifetimeScope)
        {
            #region # 验证

            if (lifetimeScope == null)
            {
                return;
            }

            #endregion

            IList<IDisposable> disposables = new List<IDisposable>();
            try
            {
                disposables = GetDisposableInstances(lifetimeScope);
                lifetimeScope.Dispose();
            }
            finally
            {
                OnDispose?.Invoke(disposables);
            }
        }
        #endregion

        #region # 获取可释放对象列表 —— static IList<IDisposable> GetDisposableInstances(...
        /// <summary>
        /// 获取可释放对象列表
        /// </summary>
        /// <param name="lifetimeScope">范围容器</param>
        /// <returns>可释放对象列表</returns>
        private static IList<IDisposable> GetDisposableInstances(ILifetimeScope lifetimeScope)
        {
            #region # 验证

            if (lifetimeScope == null)
            {
                return new List<IDisposable>();
            }

            #endregion

            IList<IDisposable> disposables = new List<IDisposable>();
            Type lifetimeScopeType = lifetimeScope.GetType();

            const string disposerPropertyName = "Disposer";
            const string disposableItemsFieldName = "_items";
            PropertyInfo disposerProperty = lifetimeScopeType.GetProperty(disposerPropertyName, BindingFlags.Public | BindingFlags.Instance);
            object disposer = disposerProperty.GetValue(lifetimeScope, null);
            Type disposerType = disposer.GetType();
            FieldInfo disposableItemsField = disposerType.GetField(disposableItemsFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            object disposableItems = disposableItemsField?.GetValue(disposer);
            if (disposableItems is Stack<IDisposable> stack)
            {
                foreach (IDisposable disposable in stack)
                {
                    Type releaseActionType = disposable.GetType();

                    const string actionFieldName = "_action";
                    const string eventArgsFieldName = "e";
                    FieldInfo actionField = releaseActionType.GetField(actionFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                    Action action = (Action)actionField.GetValue(disposable);
                    Type targetType = action.Target.GetType();
                    FieldInfo eventArgsField = targetType.GetField(eventArgsFieldName, BindingFlags.Public | BindingFlags.Instance);
                    ActivatingEventArgs<object> eventArgs = (ActivatingEventArgs<object>)eventArgsField.GetValue(action.Target);

                    disposables.Add((IDisposable)eventArgs.Instance);
                }
            }

            return disposables;
        }
        #endregion
    }
}
