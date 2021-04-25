﻿using SD.IOC.Integration.WebApi.WebHost;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("SD.IOC.Integration.WebApi.WebHost")]
[assembly: AssemblyDescription("SD.IOC 依赖注入组件 - 集成ASP.NET WebApi WebHost")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("SD")]
[assembly: AssemblyProduct("SD.IOC.Integration.WebApi.WebHost")]
[assembly: AssemblyCopyright("Copyright © SD 2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("9b0f9898-eaeb-4641-866d-4ecece94f6f0")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
// 可以指定所有值，也可以使用以下所示的 "*" 预置版本号和修订号
//通过使用 "*"，如下所示:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.0.0")]
[assembly: AssemblyFileVersion("2.0.0.0")]

//注射至WebApi应用程序
[assembly: PreApplicationStartMethod(typeof(PreAppStart), nameof(PreAppStart.InitInjection))]