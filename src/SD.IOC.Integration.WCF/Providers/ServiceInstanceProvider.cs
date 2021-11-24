using SD.IOC.Core.Mediators;
using System;
#if NET40_OR_GREATER
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
#endif
#if NETSTANDARD2_0_OR_GREATER
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Dispatcher;
#endif

namespace SD.IOC.Integration.WCF.Providers
{
    /// <summary>
    /// WCF实例提供者
    /// </summary>
    public class ServiceInstanceProvider : IInstanceProvider
    {
        #region # 事件、字段及构造器

        /// <summary>
        /// 获取服务契约实例事件
        /// </summary>
        public static event Action<InstanceContext> OnGetInstance;

        /// <summary>
        /// 销毁服务契约实例事件
        /// </summary>
        public static event Action<InstanceContext, object> OnReleaseInstance;

        /// <summary>
        /// 服务契约类型
        /// </summary>
        private readonly Type _serviceType;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="serviceType">服务契约类型</param>
        public ServiceInstanceProvider(Type serviceType)
        {
            this._serviceType = serviceType;
        }

        #endregion

        #region # 获取服务契约实例 —— object GetInstance(InstanceContext instanceContext...
        /// <summary>
        /// 获取服务契约实例
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <param name="message">消息</param>
        /// <returns>服务契约实例</returns>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }
        #endregion

        #region # 获取服务契约实例 —— object GetInstance(InstanceContext instanceContext)
        /// <summary>
        /// 获取服务契约实例
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <returns>服务契约实例</returns>
        public object GetInstance(InstanceContext instanceContext)
        {
            OnGetInstance?.Invoke(instanceContext);

            return ResolveMediator.Resolve(this._serviceType);
        }
        #endregion

        #region # 清理服务契约实例 —— void ReleaseInstance(InstanceContext instanceContext...
        /// <summary>
        /// 清理服务契约实例
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <param name="instance">服务契约实例</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            OnReleaseInstance?.Invoke(instanceContext, instance);

            ResolveMediator.Dispose();
        }
        #endregion
    }
}