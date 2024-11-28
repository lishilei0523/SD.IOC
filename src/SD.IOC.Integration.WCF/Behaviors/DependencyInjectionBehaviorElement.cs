#if NET462_OR_GREATER
using SD.IOC.Integration.WCF.Behaviors;
using System;
using System.ServiceModel.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.IOC.Integration.WCF
{
    /// <summary>
    /// WCF依赖注入行为扩展元素
    /// </summary>
    internal class DependencyInjectionBehaviorElement : BehaviorExtensionElement
    {
        /// <summary>
        /// 行为类型
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(DependencyInjectionBehavior); }
        }

        /// <summary>
        /// 创建行为
        /// </summary>
        /// <returns>行为实例</returns>
        protected override object CreateBehavior()
        {
            return new DependencyInjectionBehavior();
        }
    }
}
#endif
