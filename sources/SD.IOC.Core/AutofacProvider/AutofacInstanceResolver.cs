﻿using Autofac;
using SD.IOC.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        private readonly ILifetimeScope _container;

        /// <summary>
        /// 构造器
        /// </summary>
        public AutofacInstanceResolver()
        {
            try
            {
                this._container = AutofacContainer.Current.BeginLifetimeScope();
            }
            catch (TypeInitializationException exception)
            {
                if (exception.InnerException != null)
                {
                    if (exception.InnerException is ReflectionTypeLoadException)
                    {
                        ReflectionTypeLoadException innerException = (ReflectionTypeLoadException)exception.InnerException;

                        StringBuilder builder = new StringBuilder();

                        foreach (Exception item in innerException.LoaderExceptions)
                        {
                            if (item is TypeLoadException)
                            {
                                TypeLoadException typeLoadException = (TypeLoadException)item;
                                builder.AppendFormat("'{0}'", typeLoadException.TypeName);
                                builder.Append(',');
                            }
                        }

                        string message = builder.Length > 0
                            ? builder.ToString().Substring(0, builder.Length - 1)
                            : string.Empty;

                        throw new TypeLoadException(string.Format("无法加载类型\"{0}\"！", message));
                    }

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

        #region # 释放资源 —— void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (this._container != null)
            {
                this._container.Dispose();
            }
        }
        #endregion
    }
}