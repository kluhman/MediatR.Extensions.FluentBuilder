using System;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Module = MediatR.Extensions.FluentBuilder.Internal;

namespace MediatR.Extensions.FluentBuilder
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModule<TModule>(this IServiceCollection services) where TModule : Module, new()
        {
            services.AddModule(new TModule());

            return services;
        }

        public static IServiceCollection AddModule(this IServiceCollection services, Module module)
        {
            module.Load(services);

            return services;
        }

        public static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(p => p.GetService);

            return services;
        }

        public static IServiceCollection AddRequestModules(this IServiceCollection services, Assembly assembly)
        {
            foreach (var module in assembly.GetRequestModulesAs<Module>())
            {
                services.AddModule(module);
            }

            return services;
        }

        public static IServiceCollection AddNotificationModules(this IServiceCollection services, Assembly assembly)
        {
            foreach (var module in assembly.GetNotificationModulesAs<Module>())
            {
                services.AddModule(module);
            }

            return services;
        }

        public static IServiceCollection AddModules(this IServiceCollection services)
        {
            var assemblies = AppDomain
                .CurrentDomain
                .GetAssemblies();

            var requestModules = assemblies.SelectMany(a => a.GetRequestModulesAs<Module>());
            var notificationModules = assemblies.SelectMany(a => a.GetNotificationModulesAs<Module>());

            var allModules = requestModules.Union(notificationModules).Distinct();

            foreach (var module in allModules)
            {
                services.AddModule(module);
            }

            return services;
        }
    }
}