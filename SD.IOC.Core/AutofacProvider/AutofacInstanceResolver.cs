using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using SD.IOC.Core.Interfaces;

namespace SD.IOC.Core.AutofacProvider
{
    /// <summary>
    /// Autofac实例解析者
    /// </summary>
    public class AutofacInstanceResolver : IInstanceResolver
    {
        #region # 字段及构造器

        /// <summary>
        /// Autofac容器
        /// </summary>
        private readonly IContainer _container;

        /// <summary>
        /// 构造器
        /// </summary>
        public AutofacInstanceResolver()
        {
            try
            {
                this._container = AutofacContainer.Current;
            }
            catch (TypeInitializationException exception)
            {
                if (exception.InnerException != null)
                {
                    throw exception.InnerException;
                }
                throw;
            }
        }

        #endregion

        #region # 解析实例 —— T Resolve<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例</returns>
        public T Resolve<T>()
        {
            return this._container.Resolve<T>();
        }
        #endregion

        #region # 解析实例 —— object Resolve(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例</returns>
        public object Resolve(Type type)
        {
            return this._container.Resolve(type);
        }
        #endregion

        #region # 解析实例 —— T ResolveOptional<T>()
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例，如未注册则返回null</returns>
        public T ResolveOptional<T>() where T : class
        {
            return this._container.ResolveOptional<T>();
        }
        #endregion

        #region # 解析实例 —— object ResolveOptional(Type type)
        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例，如未注册则返回null</returns>
        public object ResolveOptional(Type type)
        {
            return this._container.ResolveOptional(type);
        }
        #endregion

        #region # 解析实例集 —— IEnumerable<T> ResolveAll<T>()
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns>实例集</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return this._container.Resolve<IEnumerable<T>>();
        }
        #endregion

        #region # 解析实例集 —— IEnumerable<object> ResolveAll(Type type)
        /// <summary>
        /// 解析实例集
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <returns>实例集</returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            Type typ = typeof(IEnumerable<>).MakeGenericType(type);

            object instance;
            if (this._container.TryResolve(typ, out instance))
            {
                return ((IEnumerable)instance).Cast<object>();
            }
            return Enumerable.Empty<object>();
        }
        #endregion
    }
}
