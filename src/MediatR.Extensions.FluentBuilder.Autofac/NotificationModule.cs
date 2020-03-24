using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Extensions.FluentBuilder.Internal;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class NotificationModule<TNotification> : Module, INotificationModule<TNotification> where TNotification : INotification
    {
        protected sealed override void Load(ContainerBuilder services)
        {
            RegisterHandlers(new NotificationRegistry<TNotification>(services));
        }

        public abstract void RegisterHandlers(INotificationRegistry<TNotification> registry);
    }
}