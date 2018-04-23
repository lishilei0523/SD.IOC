using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.StubImplement.Implements;
using SD.IOC.StubInterface.Interfaces;
using System;

namespace SD.IOC.Extension.NetCore.ServiceModel.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            TestResolveType();
            TestResolveOptionalType();
            TestResolveGeneric();
            TestResolveOptionalGeneric();
            TestProxy();

            ResolveMediator.Dispose();

            Console.WriteLine("测试OK");
            Console.ReadKey();
        }

        /// <summary>
        /// 测试初始化
        /// </summary>
        static void Init()
        {
            if (!ResolveMediator.ContainerBuilt)
            {
                IServiceCollection builder = ResolveMediator.GetServiceCollection();
                builder.RegisterServiceModels();

                ResolveMediator.Build();
            }
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        static void TestResolveType()
        {
            object productContract = ResolveMediator.Resolve(typeof(IProductContract));

            if (productContract == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        static void TestResolveOptionalType()
        {
            object productContract = ResolveMediator.ResolveOptional(typeof(ProductContract));

            if (productContract != null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        static void TestResolveGeneric()
        {
            IProductContract productContract = ResolveMediator.Resolve<IProductContract>();

            if (productContract == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        static void TestResolveOptionalGeneric()
        {
            IProductContract productContract = ResolveMediator.ResolveOptional<ProductContract>();

            if (productContract != null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试实例代理
        /// </summary>
        static void TestProxy()
        {
            string products = Proxy<IProductContract>.Instance.GetProducts();

            if (products == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
