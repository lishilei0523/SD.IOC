using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IOC.Core.Mediators;
using SD.IOC.StubAppService.Implements;
using SD.IOC.StubIAppService.Interfaces;
using System.Configuration;
using System.Reflection;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IOC.Core.Tests.TestCases
{
    /// <summary>
    /// 测试解析实例
    /// </summary>
    [TestClass]
    public class ResolveTests
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            DependencyInjectionSection.Initialize(configuration);
#endif
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.AddTransient(typeof(IProductContract), typeof(ProductContract));

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
            object productContract = ResolveMediator.Resolve(typeof(IProductContract));

            Assert.IsNotNull(productContract);
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalType()
        {
            object productContract = ResolveMediator.ResolveOptional(typeof(ProductContract));

            Assert.IsNull(productContract);
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();

            Assert.IsNotNull(productContract);
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalGeneric()
        {
            IProductContract productContract = ResolveMediator.ResolveOptional<ProductContract>();

            Assert.IsNull(productContract);
        }

        /// <summary>
        /// 测试实例代理
        /// </summary>
        [TestMethod]
        public void TestProxy()
        {
            string products = Proxy<IProductContract>.Instance.GetProducts();
            Assert.IsNotNull(products);
        }
    }
}
