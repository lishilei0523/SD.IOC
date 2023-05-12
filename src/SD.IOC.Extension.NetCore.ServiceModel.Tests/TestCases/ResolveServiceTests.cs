using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IOC.Core;
using SD.IOC.Core.Mediators;
using SD.IOC.StubIAppService.Interfaces;
using System.Configuration;
using System.Reflection;
using System.ServiceModel;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace SD.IOC.Extension.NetCore.ServiceModel.Tests.TestCases
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
            //初始化配置文件
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            ServiceModelSectionGroup.Initialize(configuration);
            DependencyInjectionSection.Initialize(configuration);

            //初始化依赖注入容器
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterServiceModels();

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
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();

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
