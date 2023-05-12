using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Extensions;
using SD.IOC.Core.Mediators;
using SD.IOC.StubIAppService.Interfaces;

namespace SD.IOC.Core.Tests.TestCases
{
    /// <summary>
    /// 测试解析远程服务
    /// </summary>
    [TestClass]
    public class ResolveServiceTests
    {
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
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IProductContract productContract1 = ResolveMediator.Resolve<IProductContract>();
            IProductContract productContract2 = ResolveMediator.Resolve<IProductContract>();

            Assert.IsNotNull(productContract1);
            Assert.IsNotNull(productContract2);
        }
    }
}
