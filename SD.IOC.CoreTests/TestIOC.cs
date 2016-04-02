using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Mediator;
using SD.IOC.StubIAppService.Interfaces;

namespace SD.IOC.CoreTests
{
    /// <summary>
    /// 测试依赖注入
    /// </summary>
    [TestClass]
    public class TestIOC
    {
        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        [TestMethod]
        public void TestResolveType()
        {
            object productContract = ResolveMediator.Resolve(typeof(IProductContract));
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalType()
        {
            object productContract = ResolveMediator.ResolveOptional(typeof(IProductContract));
            Assert.IsNotNull(productContract);
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveGeneric()
        {
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        [TestMethod]
        public void TestResolveOptionalGeneric()
        {
            IProductContract productContract = ResolveMediator.ResolveOptional<IProductContract>();
            Assert.IsNotNull(productContract);
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
