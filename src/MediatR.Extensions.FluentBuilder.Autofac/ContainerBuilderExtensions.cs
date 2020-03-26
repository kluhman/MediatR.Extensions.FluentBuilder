using System.Reflection;

using Autofac;

using Module = Autofac.Module;

namespace MediatR.Extensions.FluentBuilder
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterMediatR(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerRequest();
            builder.Register<ServiceFactory>(context =>
            {
                var innerContext = context.Resolve<IComponentContext>();
                return type => innerContext.Resolve(type);
            });
        }
        
        public static void RegisterRequestModules(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var module in assembly.GetRequestModulesAs<Module>())
            {
                builder.RegisterModule(module);
            }
        }
        
        public static void RegisterNotificationModules(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var module in assembly.GetNotificationModulesAs<Module>())
            {
                builder.RegisterModule(module);
            }
        }
    }
}