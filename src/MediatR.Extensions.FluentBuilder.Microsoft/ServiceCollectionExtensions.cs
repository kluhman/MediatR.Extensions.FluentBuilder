using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Module = MediatR.Extensions.FluentBuilder.Internal;

namespace MediatR.Extensions.FluentBuilder
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModule<TModule>(this IServiceCollection services) where TModule : Module, new()
        {
            services.AddModule(new TModule());
        }
        
        public static void AddModule(this IServiceCollection services, Module module)
        {
            module.Load(services);
        }
        
        public static void AddRequestModules(this IServiceCollection services, Assembly assembly)
        {
            foreach (var module in assembly.GetRequestModulesAs<Module>())
            {
                services.AddModule(module);
            }
        }
    }
}