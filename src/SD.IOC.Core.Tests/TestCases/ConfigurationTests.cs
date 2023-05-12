using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.Common;
using SD.IOC.Core.Configurations;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace SD.IOC.Core.Tests.TestCases
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestInitialize]
        public void Initialize()
        {
#if NETCOREAPP3_1_OR_GREATER
            Assembly entryAssembly = Assembly.GetExecutingAssembly();
            Configuration configuration = ConfigurationExtension.GetConfigurationFromAssembly(entryAssembly);
            DependencyInjectionSection.Initialize(configuration);
#endif
        }

        [TestMethod]
        public void TestConfigurations()
        {
            foreach (AssemblyElement assembly in DependencyInjectionSection.Setting.AsInterfaceAssemblies)
            {
                Trace.WriteLine(assembly.Name);
                Trace.WriteLine(assembly.LifetimeMode);
            }
            foreach (AssemblyElement assembly in DependencyInjectionSection.Setting.AsBaseAssemblies)
            {
                Trace.WriteLine(assembly.Name);
                Trace.WriteLine(assembly.LifetimeMode);
            }
            foreach (AssemblyElement assembly in DependencyInjectionSection.Setting.AsSelfAssemblies)
            {
                Trace.WriteLine(assembly.Name);
                Trace.WriteLine(assembly.LifetimeMode);
            }
            foreach (TypeElement type in DependencyInjectionSection.Setting.AsInterfaceTypes)
            {
                Trace.WriteLine(type.Name);
                Trace.WriteLine(type.Assembly);
                Trace.WriteLine(type.LifetimeMode);
            }
            foreach (TypeElement type in DependencyInjectionSection.Setting.AsBaseTypes)
            {
                Trace.WriteLine(type.Name);
                Trace.WriteLine(type.Assembly);
                Trace.WriteLine(type.LifetimeMode);
            }
            foreach (TypeElement type in DependencyInjectionSection.Setting.AsSelfTypes)
            {
                Trace.WriteLine(type.Name);
                Trace.WriteLine(type.Assembly);
                Trace.WriteLine(type.LifetimeMode);
            }
            foreach (AssemblyElement assembly in DependencyInjectionSection.Setting.WcfInterfaces)
            {
                Trace.WriteLine(assembly.Name);
                Trace.WriteLine(assembly.LifetimeMode);
            }
        }
    }
}
