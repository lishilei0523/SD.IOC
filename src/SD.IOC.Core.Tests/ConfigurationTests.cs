using Microsoft.VisualStudio.TestTools.UnitTesting;
using SD.IOC.Core.Configurations;
using System.Diagnostics;

namespace SD.IOC.Core.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
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
