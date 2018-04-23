using SD.IOC.Core.Configurations;
using System;
using System.Configuration;

// ReSharper disable once CheckNamespace
namespace SD.IOC.Core
{
    /// <summary>
    /// 依赖注入注册配置
    /// </summary>
    public class InjectionRegisterConfiguration : ConfigurationSection
    {
        #region # 字段及构造器

        /// <summary>
        /// 单例
        /// </summary>
        private static readonly InjectionRegisterConfiguration _Setting;

        /// <summary>
        /// 静态构造器
        /// </summary>
        static InjectionRegisterConfiguration()
        {
            InjectionRegisterConfiguration._Setting = (InjectionRegisterConfiguration)ConfigurationManager.GetSection("injectionRegisterConfiguration");

            #region # 非空验证

            if (InjectionRegisterConfiguration._Setting == null)
            {
                throw new ApplicationException("依赖注入注册节点未配置，请检查程序！");
            }

            #endregion
        }

        #endregion

        #region # 访问器 —— static InjectionRegisterConfiguration Setting
        /// <summary>
        /// 访问器
        /// </summary>
        public static InjectionRegisterConfiguration Setting
        {
            get { return InjectionRegisterConfiguration._Setting; }
        }
        #endregion

        #region # As接口程序集列表 —— AssemblyElementCollection AsInterfaceAssemblies
        /// <summary>
        /// As接口程序集列表
        /// </summary>
        [ConfigurationProperty("asInterfaceAssemblies")]
        [ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
        public AssemblyElementCollection AsInterfaceAssemblies
        {
            get
            {
                AssemblyElementCollection collection = this["asInterfaceAssemblies"] as AssemblyElementCollection;
                return collection ?? new AssemblyElementCollection();
            }
            set { this["asInterfaceAssemblies"] = value; }
        }
        #endregion

        #region # As基类程序集列表 —— AssemblyElementCollection AsBaseAssemblies
        /// <summary>
        /// As基类程序集列表
        /// </summary>
        [ConfigurationProperty("asBaseAssemblies")]
        [ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
        public AssemblyElementCollection AsBaseAssemblies
        {
            get
            {
                AssemblyElementCollection collection = this["asBaseAssemblies"] as AssemblyElementCollection;
                return collection ?? new AssemblyElementCollection();
            }
            set { this["asBaseAssemblies"] = value; }
        }
        #endregion

        #region # As自身程序集列表 —— AssemblyElementCollection AsSelfAssemblies
        /// <summary>
        /// As自身程序集列表
        /// </summary>
        [ConfigurationProperty("asSelfAssemblies")]
        [ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
        public AssemblyElementCollection AsSelfAssemblies
        {
            get
            {
                AssemblyElementCollection collection = this["asSelfAssemblies"] as AssemblyElementCollection;
                return collection ?? new AssemblyElementCollection();
            }
            set { this["asSelfAssemblies"] = value; }
        }
        #endregion

        #region # As接口类型列表 —— TypeElementCollection AsInterfaceTypes
        /// <summary>
        /// As接口类型列表
        /// </summary>
        [ConfigurationProperty("asInterfaceTypes")]
        [ConfigurationCollection(typeof(TypeElementCollection), AddItemName = "type")]
        public TypeElementCollection AsInterfaceTypes
        {
            get
            {
                TypeElementCollection collection = this["asInterfaceTypes"] as TypeElementCollection;
                return collection ?? new TypeElementCollection();
            }
            set { this["asInterfaceTypes"] = value; }
        }
        #endregion

        #region # As基类类型列表 —— TypeElementCollection AsBaseTypes
        /// <summary>
        /// As基类类型列表
        /// </summary>
        [ConfigurationProperty("asBaseTypes")]
        [ConfigurationCollection(typeof(TypeElementCollection), AddItemName = "type")]
        public TypeElementCollection AsBaseTypes
        {
            get
            {
                TypeElementCollection collection = this["asBaseTypes"] as TypeElementCollection;
                return collection ?? new TypeElementCollection();
            }
            set { this["asBaseTypes"] = value; }
        }
        #endregion

        #region # As自身类型列表 —— TypeElementCollection AsSelfTypes
        /// <summary>
        /// As自身类型列表
        /// </summary>
        [ConfigurationProperty("asSelfTypes")]
        [ConfigurationCollection(typeof(TypeElementCollection), AddItemName = "type")]
        public TypeElementCollection AsSelfTypes
        {
            get
            {
                TypeElementCollection collection = this["asSelfTypes"] as TypeElementCollection;
                return collection ?? new TypeElementCollection();
            }
            set { this["asSelfTypes"] = value; }
        }
        #endregion

        #region # WCF接口列表 —— AssemblyElementCollection WcfInterfaces
        /// <summary>
        /// WCF接口列表
        /// </summary>
        [ConfigurationProperty("wcfInterfaces")]
        [ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
        public AssemblyElementCollection WcfInterfaces
        {
            get
            {
                AssemblyElementCollection collection = this["wcfInterfaces"] as AssemblyElementCollection;
                return collection ?? new AssemblyElementCollection();
            }
            set { this["wcfInterfaces"] = value; }
        }
        #endregion
    }
}
