using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class NotificationRegistry<TNotification> : INotificationRegistry<TNotification> where TNotification : INotification
    {
        private readonly ContainerBuilder _builder;

        public NotificationRegistry(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public INotificationRegistry<TNotification> AddHandler<THandler>() where THandler : class, INotificationHandler<TNotification>
        {
            _builder.RegisterType<THandler>().As<INotificationHandler<TNotification>>().InstancePerRequest();
            return this;
        }
    }
}