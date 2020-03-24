using Autofac;

using MediatR.Extensions.FluentBuilder.Internal;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class NotificationRegistryTests
    {
        private readonly ContainerBuilder _services;
        private readonly NotificationRegistry<TestNotification> _registry;

        public NotificationRegistryTests()
        {
            _services = new ContainerBuilder();
            _registry = new NotificationRegistry<TestNotification>(_services);
        }

        [Fact]
        public void AddHandler_ShouldAddHandler()
        {
            _registry.AddHandler<TestNotification.Handler>();
            
            Assert.True(_services.Build().IsRegistered<INotificationHandler<TestNotification>>());
        }
    }
}