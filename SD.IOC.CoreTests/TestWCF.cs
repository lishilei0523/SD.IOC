using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.StubImplement.Implements;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.CoreTests
{
    [TestClass]
    public class TestWCF
    {
        /// <summary>
        /// 测试初始化
        /// </summary>
        [TestInitialize]
        public void Init()
        {


            using (ServiceHost host = new ServiceHost(typeof(ProductContract)))
            {
                host.Open();
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            //绑定类型，如何访问服务器
            WSHttpBinding binding = new WSHttpBinding();

            //地址，从哪儿找到服务
            string address = "http://localhost:9999/WCFDemo/Hello";

            //建立与服务器指定的服务通道
            ChannelFactory<IProductContract> factory = new ChannelFactory<IProductContract>(binding, address);

            //创建通道实现通信
            IProductContract productContract = factory.CreateChannel();

            string result = productContract.GetProducts();

            //调用服务器端暴露的操作（方法）
            Assert.IsNotNull(result);
        }
    }
}
