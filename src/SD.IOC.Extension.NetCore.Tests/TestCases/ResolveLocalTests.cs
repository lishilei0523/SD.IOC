using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IOC.Core;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore.Tests.StubAppServices;
using SD.IOC.Extension.NetCore.Tests.StubIAppServices;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IOC.Extension.NetCore.Tests.TestCases
{
    /// <summary>
    /// 测试解析本地
    /// </summary>
    [TestClass]
    public class ResolveLocalTests
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
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

        /// <summary>
        /// 测试清理
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            ResolveMediator.Dispose();
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        [TestMethod]
        public void TestResolveType()
        {
            object orderContract = ResolveMediator.Resolve(typeof(IOrderContract));

            Assert.IsNotNull(orderContract);
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalType()
        {
            object orderContract = ResolveMediator.ResolveOptional(typeof(OrderContract));

            Assert.IsNull(orderContract);
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IOrderContract orderContract = ResolveMediator.Resolve<IOrderContract>();

            Assert.IsNotNull(orderContract);
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalGeneric()
        {
            IOrderContract orderContract = ResolveMediator.ResolveOptional<OrderContract>();

            Assert.IsNull(orderContract);
        }

        /// <summary>
        /// 测试实例代理
        /// </summary>
        [TestMethod]
        public void TestProxy()
        {
            string order = Proxy<IOrderContract>.Instance.GetOrder();
            Assert.IsNotNull(order);
        }
    }
}
