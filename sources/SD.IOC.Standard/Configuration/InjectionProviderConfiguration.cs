using System;
using System.Configuration;

namespace SD.IOC.Standard.Configuration
{
    /// <summary>
    /// 依赖注入提供者配置
    /// </summary>
    public class InjectionProviderConfiguration : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly InjectionProviderConfiguration _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static InjectionProviderConfiguration()
        {
            InjectionProviderConfiguration._Setting = (InjectionProviderConfiguration)ConfigurationManager.GetSection("injectionProviderConfiguration");

            #region # 非空验证

            if (InjectionProviderConfiguration._Setting == null)
            {
                throw new ApplicationException("依赖注入提供者节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static InjectionProviderConfiguration Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static InjectionProviderConfiguration Setting
        {
            get { return InjectionProviderConfiguration._Setting; }
        }
        #endregion

        #region # 类型 —— string Type
        /// <summary>
        /// 类型
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return this["type"].ToString(); }
            set { this["type"] = value; }
        }
        #endregion

        #region # 程序集 —— string Assembly
        /// <summary>
        /// 程序集
        /// </summary>
        [ConfigurationProperty("assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }
        #endregion
    }
}
