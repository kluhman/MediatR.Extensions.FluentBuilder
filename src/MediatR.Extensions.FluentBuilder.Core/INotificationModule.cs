using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder
{
    public interface INotificationModule<in TNotification> where TNotification : INotification
    {
        void RegisterHandlers(INotificationRegistry<TNotification> registry);
    }
}