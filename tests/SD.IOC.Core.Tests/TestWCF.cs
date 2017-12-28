using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Mediator;
using SD.IOC.Core.WcfTools;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Core.Tests
{
    /// <summary>
    /// 测试WCF注入
    /// </summary>
    [TestClass]
    public class TestWCF
    {
        /// <summary>
        /// 测试服务代理
        /// </summary>
        [TestMethod]
        public void TestServiceProxy()
        {
            //建立与服务器指定的服务通道
            ServiceProxy<IProductContract> proxy = new ServiceProxy<IProductContract>();

            //创建通道实现通信
            IProductContract productContract = proxy.Channel;

            string result = productContract.GetProducts();

            //调用服务器端暴露的操作（方法）
            Assert.IsNotNull(result);
        }


        /// <summary>
        /// 测试服务代理
        /// </summary>
        [TestMethod]
        public void TestResolve()
        {
            //实例化接口
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();

            string result = productContract.GetProducts();

            //调用服务器端暴露的操作（方法）
            Assert.IsNotNull(result);
        }
    }
}
