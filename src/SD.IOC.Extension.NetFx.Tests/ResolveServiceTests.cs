﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Mediators;
using SD.IOC.StubImplement.Implements;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Extension.NetFx.Tests
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
        public void Init()
        {
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
