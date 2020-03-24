using System.Threading;
using System.Threading.Tasks;

using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class TestNotification : INotification
    {
        internal class Handler : INotificationHandler<TestNotification>
        {
            public Task Handle(TestNotification notification, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
        
        internal class Module : NotificationModule<TestNotification>
        {
            public override void RegisterHandlers(INotificationRegistry<TestNotification> registry)
            {
                registry.AddHandler<Handler>();
            }
        }
    }
}