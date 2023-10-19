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
    /// 解析实例测试
    /// </summary>
    [TestClass]
    public class ResolveTests
    {
        #region # 测试初始化 —— void Initialize()
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
            object productContract = ResolveMediator.Resolve(typeof(IProductContract));

            Assert.IsNotNull(productContract);
        }
        #endregion

        #region # 测试解析实例 —— void TestResolveOptionalType()
        /// <summary>
        /// 测试解析实例
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalType()
        {
            object productContract = ResolveMediator.ResolveOptional(typeof(ProductContract));

            Assert.IsNull(productContract);
        }
        #endregion

        #region # 测试解析泛型实例 —— void TestResolveGeneric()
        /// <summary>
        /// 测试解析泛型实例
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();

            Assert.IsNotNull(productContract);
        }
        #endregion

        #region # 测试解析泛型实例 —— void TestResolveOptionalGeneric()
        /// <summary>
        /// 测试解析泛型实例
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalGeneric()
        {
            IProductContract productContract = ResolveMediator.ResolveOptional<ProductContract>();

            Assert.IsNull(productContract);
        }
        #endregion
    }
}
