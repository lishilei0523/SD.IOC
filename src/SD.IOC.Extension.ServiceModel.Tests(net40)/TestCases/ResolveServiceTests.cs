using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Extensions;
using SD.IOC.Core.Mediators;
using SD.IOC.StubIAppService.Interfaces;

namespace SD.IOC.Core.Tests.TestCases
{
    /// <summary>
    /// 解析远程服务测试
    /// </summary>
    [TestClass]
    public class ResolveServiceTests
    {
        #region # 测试初始化 —— void Initialize()
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                ContainerBuilder builder = ResolveMediator.GetContainerBuilder();
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
            object productContract = ResolveMediator.Resolve(typeof(IProductContract));

            Assert.IsNotNull(productContract);
        }
        #endregion

        #region # 测试解析泛型实例 —— void TestResolveGeneric()
        /// <summary>
        /// 测试解析泛型实例
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IProductContract productContract1 = ResolveMediator.Resolve<IProductContract>();
            IProductContract productContract2 = ResolveMediator.Resolve<IProductContract>();

            Assert.IsNotNull(productContract1);
            Assert.IsNotNull(productContract2);
        }
        #endregion
    }
}
