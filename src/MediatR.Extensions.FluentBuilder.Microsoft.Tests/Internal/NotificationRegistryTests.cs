using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Internal;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class NotificationRegistryTests
    {
        private readonly Mock<IServiceCollection> _services;
        private readonly NotificationRegistry<TestNotification> _registry;

        public NotificationRegistryTests()
        {
            _services = new Mock<IServiceCollection>();
            _registry = new NotificationRegistry<TestNotification>(_services.Object);
        }

        [Fact]
        public void AddHandler_ShouldAddHandler()
        {
            _registry.AddHandler<TestNotification.Handler>();

            _services.Verify(x => x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(INotificationHandler<TestNotification>) &&
                                                                      d.ImplementationType == typeof(TestNotification.Handler))));
        }
    }
}