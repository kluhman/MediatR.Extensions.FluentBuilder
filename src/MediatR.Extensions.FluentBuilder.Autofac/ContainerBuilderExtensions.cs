using System.Reflection;

using Autofac;

using Module = Autofac.Module;

namespace MediatR.Extensions.FluentBuilder
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterRequestModules(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var module in assembly.GetRequestModulesAs<Module>())
            {
                builder.RegisterModule(module);
            }
        }
    }
}