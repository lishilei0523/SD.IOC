using Microsoft.Extensions.DependencyInjection;
using System;
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
        private static readonly IServiceCollection _ServiceCollection;

        /// <summary>
        /// 范围容器线程缓存
        /// </summary>
        private static readonly AsyncLocal<IServiceScope> _ServiceScope;

        /// <summary>
        /// 容器
        /// </summary>
        private static IServiceProvider _ServiceProvider;

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
            _ServiceCollection = new ServiceCollection();
#if !NET45
            _ServiceScope = new AsyncLocal<IServiceScope>(OnServiceScopeValueChange);
#else
            _ServiceScope = new AsyncLocal<IServiceScope>();
#endif
            _ContainerBuilt = false;
        }

        #endregion


        #region # 容器是否已初始化 —— static bool ContainerBuilt
        /// <summary>
        /// 容器是否已初始化
        /// </summary>
        public static bool ContainerBuilt
        {
            get => _ContainerBuilt;
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
                _ServiceProvider = _ServiceCollection.BuildServiceProvider();
                _ContainerBuilt = true;
            }
        }
        #endregion

        #region # 获取容器建造者 —— static IServiceCollection GetServiceCollection()
        /// <summary>
        /// 获取容器建造者
        /// </summary>
        public static IServiceCollection GetServiceCollection()
        {
            if (_ContainerBuilt)
            {
                throw new InvalidOperationException("容器已初始化，不可获取容器建造者！");
            }

            return _ServiceCollection;
        }
        #endregion

        #region # 获取容器 —— static IServiceProvider GetServiceProvider()
        /// <summary>
        /// 获取容器
        /// </summary>
        /// <returns>容器</returns>
        public static IServiceProvider GetServiceProvider()
        {
            if (!_ContainerBuilt)
            {
                throw new InvalidOperationException("容器未初始化！");
            }

            return _ServiceProvider;
        }
        #endregion

        #region # 获取范围容器 —— static IServiceScope GetServiceScope()
        /// <summary>
        /// 获取范围容器
        /// </summary>
        /// <returns>范围容器</returns>
        private static IServiceScope GetServiceScope()
        {
            lock (_Sync)
            {
                if (_ServiceScope.Value.Disposed())
                {
                    IServiceProvider serviceProvider = GetServiceProvider();
                    _ServiceScope.Value = serviceProvider.CreateScope();
                }

                return _ServiceScope.Value;
            }
        }
        #endregion

        #region # 获取范围容器可释放对象列表 —— static IList<IDisposable> GetServiceScopeDisposables()
        /// <summary>
        /// 获取范围容器可释放对象列表
        /// </summary>
        /// <returns>可释放对象列表</returns>
        public static IList<IDisposable> GetServiceScopeDisposables()
        {
            return GetDisposableInstances(_ServiceScope.Value);
        }
        #endregion

        #region # 容器是否已被释放 —— static bool Disposed(this IServiceProvider serviceProvider)
        /// <summary>
        /// 容器是否已被释放
        /// </summary>
        /// <param name="serviceProvider">容器</param>
        /// <returns>是否已被释放</returns>
        public static bool Disposed(this IServiceProvider serviceProvider)
        {
            #region # 验证

            if (serviceProvider == null)
            {
                return true;
            }

            #endregion

            bool disposed;
            string filedName;
#if NET45
            filedName = "_disposeCalled";
#else
            filedName = "_disposed";
#endif
            Type type = serviceProvider.GetType();
            FieldInfo field = type.GetField(filedName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                object fieldValue = field.GetValue(serviceProvider);
                disposed = fieldValue == null ? true : Convert.ToBoolean(fieldValue);
            }
            else
            {
                disposed = true;
            }

            return disposed;
        }
        #endregion

        #region # 范围容器是否已被释放 —— static bool Disposed(this IServiceScope serviceScope)
        /// <summary>
        /// 范围容器是否已被释放
        /// </summary>
        /// <param name="serviceScope">范围容器</param>
        /// <returns>是否已被释放</returns>
        public static bool Disposed(this IServiceScope serviceScope)
        {
            #region # 验证

            if (serviceScope == null)
            {
                return true;
            }

            #endregion

            bool disposed;
            Type type = serviceScope.GetType();
#if NET45
            const string scopedProviderFieldName = "_scopedProvider";
            const string disposeCalledFieldName = "_disposeCalled";
            FieldInfo scopedProviderField = type.GetField(scopedProviderFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            object scopedProvider = scopedProviderField.GetValue(serviceScope);
            Type scopedProviderType = scopedProviderField.FieldType;
            FieldInfo disposeCalledField = scopedProviderType.GetField(disposeCalledFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            object value = disposeCalledField.GetValue(scopedProvider);
            disposed = (bool)value;
#else
            const string disposedFieldName = "_disposed";
            FieldInfo disposedField = type.GetField(disposedFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            object value = disposedField.GetValue(serviceScope);
            disposed = (bool)value;
#endif
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
            IServiceScope serviceScope = GetServiceScope();
            T instance = serviceScope.ServiceProvider.GetRequiredService<T>();

            return instance;
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
            IServiceScope serviceScope = GetServiceScope();
            object instance = serviceScope.ServiceProvider.GetRequiredService(type);

            return instance;
        }
        #endregion

        #region # 解析实例 —— static T ResolveOptional<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例</returns>
        /// <remarks>如未注册则返回null</remarks>
        public static T ResolveOptional<T>() where T : class
        {
            IServiceScope serviceScope = GetServiceScope();
            T instance = serviceScope.ServiceProvider.GetService<T>();

            return instance;
        }
        #endregion

        #region # 解析实例 —— static object ResolveOptional(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例</returns>
        /// <remarks>如未注册则返回null</remarks>
        public static object ResolveOptional(Type type)
        {
            IServiceScope serviceScope = GetServiceScope();
            object instance = serviceScope.ServiceProvider.GetService(type);

            return instance;
        }
        #endregion

        #region # 解析实例列表 —— static IEnumerable<T> ResolveAll<T>()
        /// <summary>
        /// 解析实例列表
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例列表</returns>
        public static IEnumerable<T> ResolveAll<T>()
        {
            IServiceScope serviceScope = GetServiceScope();
            IEnumerable<T> instances = serviceScope.ServiceProvider.GetServices<T>();

            return instances;
        }
        #endregion

        #region # 解析实例列表 —— static IEnumerable<object> ResolveAll(Type type)
        /// <summary>
        /// 解析实例列表
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例列表</returns>
        public static IEnumerable<object> ResolveAll(Type type)
        {
            IServiceScope serviceScope = GetServiceScope();
            IEnumerable<object> instances = serviceScope.ServiceProvider.GetServices(type);

            return instances;
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
                if (_ServiceScope.Value != null)
                {
                    Dispose(_ServiceScope.Value);
                    _ServiceScope.Value = null;
                }
            }
        }
        #endregion

        #region # 释放可释放资源 —— static void DisposeDisposables()
        /// <summary>
        /// 释放可释放资源
        /// </summary>
        public static void DisposeDisposables()
        {
            lock (_Sync)
            {
                if (_ServiceScope.Value != null)
                {
                    IList<IDisposable> disposables = GetDisposableInstances(_ServiceScope.Value);
                    OnDispose?.Invoke(disposables);
                }
            }
        }
        #endregion

        #region # 释放范围容器 —— static void Dispose(IServiceScope serviceScope)
        /// <summary>
        /// 释放范围容器
        /// </summary>
        /// <param name="serviceScope">范围容器</param>
        private static void Dispose(IServiceScope serviceScope)
        {
            #region # 验证

            if (serviceScope == null)
            {
                return;
            }

            #endregion

            IList<IDisposable> disposables = new List<IDisposable>();
            try
            {
                disposables = GetDisposableInstances(serviceScope);
                serviceScope.Dispose();
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
        /// <param name="serviceScope">范围容器</param>
        /// <returns>可释放对象列表</returns>
        private static IList<IDisposable> GetDisposableInstances(IServiceScope serviceScope)
        {
            #region # 验证

            if (serviceScope == null)
            {
                return new List<IDisposable>();
            }

            #endregion

            IList<IDisposable> disposables = new List<IDisposable>();
            Type serviceScopeType = serviceScope.GetType();
#if NET45
            const string scopeProviderFieldName = "_scopedProvider";
            const string transientDisposablesFieldName = "_transientDisposables";
            FieldInfo scopeProviderField = serviceScopeType.GetField(scopeProviderFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            object scopeProvider = scopeProviderField?.GetValue(serviceScope);
            Type scopeProviderType = scopeProvider?.GetType();
            FieldInfo transientDisposablesField = scopeProviderType?.GetField(transientDisposablesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (transientDisposablesField?.GetValue(scopeProvider) is List<IDisposable> list)
            {
                disposables = list;
            }
#else
            const string disposablesFieldName = "_disposables";
            FieldInfo disposablesField = serviceScopeType.GetField(disposablesFieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (disposablesField?.GetValue(serviceScope) is List<object> list)
            {
                disposables = list.Select(x => (IDisposable)x).ToList();
            }
#endif
            return disposables;
        }
        #endregion

        #region # AsyncLocal值变化事件 —— static void OnServiceScopeValueChange(...
#if !NET45
        /// <summary>
        /// AsyncLocal值变化事件
        /// </summary>
        private static void OnServiceScopeValueChange(AsyncLocalValueChangedArgs<IServiceScope> eventArgs)
        {
            if ((eventArgs.CurrentValue == null || eventArgs.CurrentValue.Disposed()) && !eventArgs.PreviousValue.Disposed())
            {
                _ServiceScope.Value = eventArgs.PreviousValue;
            }
        }
#endif 
        #endregion
    }
}
