using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IOC.Core;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.Tests.StubAppServices;
using SD.IOC.Extension.Tests.StubIAppServices;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IOC.Extension.NetCore.Tests.TestCases
{
    /// <summary>
    /// 解析本地服务测试
    /// </summary>
    [TestClass]
    public class ResolveLocalTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            DependencyInjectionSection.Initialize(configuration);

            //初始化依赖注入容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterConfigs();

                ResolveMediator.Build();
            }
        }
        #endregion

        #region # 测试清理 —— void Cleanup()
        /// <summary>
        /// 测试清理
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            ResolveMediator.Dispose();
        }
        #endregion

        #region # 测试解析实例 —— void TestResolveType()
        /// <summary>
        /// 测试解析实例
        /// </summary>
        [TestMethod]
        public void TestResolveType()
        {
            object orderContract = ResolveMediator.Resolve(typeof(IOrderContract));

            Assert.IsNotNull(orderContract);
        }
        #endregion

        #region # 测试解析实例 —— void TestResolveOptionalType()
        /// <summary>
        /// 测试解析实例
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalType()
        {
            object orderContract = ResolveMediator.ResolveOptional(typeof(OrderContract));

            Assert.IsNull(orderContract);
        }
        #endregion

        #region # 测试解析泛型实例 —— void TestResolveGeneric()
        /// <summary>
        /// 测试解析泛型实例
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IOrderContract orderContract = ResolveMediator.Resolve<IOrderContract>();

            Assert.IsNotNull(orderContract);
        }
        #endregion

        #region # 测试解析泛型实例 —— void TestResolveOptionalGeneric()
        /// <summary>
        /// 测试解析泛型实例
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalGeneric()
        {
            IOrderContract orderContract = ResolveMediator.ResolveOptional<OrderContract>();

            Assert.IsNull(orderContract);
        }
        #endregion

        #region # 测试实例代理 —— void TestProxy()
        /// <summary>
        /// 测试实例代理
        /// </summary>
        [TestMethod]
        public void TestProxy()
        {
            string order = Proxy<IOrderContract>.Instance.GetOrder();
            Assert.IsNotNull(order);
        }
        #endregion
    }
}
