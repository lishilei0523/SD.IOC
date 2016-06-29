using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using SD.IOC.Core.Mediator;

namespace SD.IOC.Integration.WCF
{
    /// <summary>
    /// WCF实例提供者基类
    /// </summary>
    public class WcfInstanceProvider : IInstanceProvider
    {
        #region # 字段及构造器

        /// <summary>
        /// 服务契约类型
        /// </summary>
        private readonly Type _serviceType;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="serviceType">服务契约类型</param>
        protected WcfInstanceProvider(Type serviceType)
        {
            this._serviceType = serviceType;
        }

        #endregion

        #region # 获取服务契约实例 —— virtual object GetInstance(InstanceContext instanceContext...
        /// <summary>
        /// 获取服务契约实例
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <param name="message">消息</param>
        /// <returns>服务契约实例</returns>
        public virtual object GetInstance(InstanceContext instanceContext, Message message)
        {
            return this.GetInstance(instanceContext);
        }
        #endregion

        #region # 获取服务契约实例 —— virtual object GetInstance(InstanceContext instanceContext)
        /// <summary>
        /// 获取服务契约实例
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <returns>服务契约实例</returns>
        public virtual object GetInstance(InstanceContext instanceContext)
        {
            return ResolveMediator.Resolve(this._serviceType);
        }
        #endregion

        #region # 清理实例契约 —— virtual void ReleaseInstance(InstanceContext instanceContext...
        /// <summary>
        /// 清理实例契约
        /// </summary>
        /// <param name="instanceContext">WCF上下文对象</param>
        /// <param name="instance">服务契约实例</param>
        public virtual void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            ResolveMediator.Dispose();
        }
        #endregion
    }
}