using System;
using System.Configuration;

namespace SD.IOC.Standard.Configuration
{
    /// <summary>
    /// 程序集节点
    /// </summary>
    public class AssemblyElement : ConfigurationElement
    {
        #region # 程序集名称 —— string Name
        /// <summary>
        /// 程序集名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
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
