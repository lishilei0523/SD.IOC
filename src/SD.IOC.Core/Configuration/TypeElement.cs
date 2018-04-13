using System;
using System.Configuration;

namespace SD.IOC.Core.Configuration
{
    /// <summary>
    /// 类型节点
    /// </summary>
    public class TypeElement : ConfigurationElement
    {
        #region # 类型名称 —— string Name
        /// <summary>
        /// 类型名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        #endregion

        #region # 所在程序集 —— string Assembly
        /// <summary>
        /// 所在程序集
        /// </summary>
        [ConfigurationProperty("assembly", IsRequired = false, IsKey = true)]
        public string Assembly
        {
            get { return (string)this["assembly"]; }
            set { this["assembly"] = value; }
        }
        #endregion

        #region # 实例生命周期模式 —— LifetimeMode? LifetimeMode
        /// <summary>
        /// 实例生命周期模式
        /// </summary>
        [ConfigurationProperty("lifetimeMode", IsRequired = false, IsKey = true)]
        public LifetimeMode? LifetimeMode
        {
            get
            {
                object lifetimeMode = this["lifetimeMode"];
                if (lifetimeMode == null)
                {
                    return null;
                }
                return (LifetimeMode)Enum.Parse(typeof(LifetimeMode), lifetimeMode.ToString());
            }
            set
            {
                this["lifetimeMode"] = value;
            }
        }
        #endregion
    }
}
