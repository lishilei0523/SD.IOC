using Microsoft.Extensions.DependencyInjection;
using SD.IOC.Core.Mediators;
using SD.IOC.Extension.NetCore.Tests.StubImplements;
using SD.IOC.Extension.NetCore.Tests.StubInterfaces;
using System;

namespace SD.IOC.Extension.NetCore.Tests
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
                builder.RegisterConfigs();

                ResolveMediator.Build();
            }
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        static void TestResolveType()
        {
            object orderContract = ResolveMediator.ResolveOptional(typeof(IOrderContract));

            if (orderContract == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例方法
        /// </summary>
        static void TestResolveOptionalType()
        {
            object orderContract = ResolveMediator.ResolveOptional(typeof(OrderContract));

            if (orderContract != null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        static void TestResolveGeneric()
        {
            IOrderContract orderContract = ResolveMediator.ResolveOptional<IOrderContract>();

            if (orderContract == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试解析实例泛型方法
        /// </summary>
        static void TestResolveOptionalGeneric()
        {
            IOrderContract orderContract = ResolveMediator.ResolveOptional<OrderContract>();

            if (orderContract != null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 测试实例代理
        /// </summary>
        static void TestProxy()
        {
            string order = Proxy<IOrderContract>.Instance.GetOrder();

            if (order == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
