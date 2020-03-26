using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class ContainerBuilderExtensionTests
    {
        private readonly ContainerBuilder _builder;

        public ContainerBuilderExtensionTests()
        {
            _builder = new ContainerBuilder();
        }

        [Fact]
        public void RegisterMediatR_ShouldRegisterRequiredDependencies()
        {
            _builder.RegisterMediatR();

            var container = _builder.Build();
            Assert.True(container.ComponentRegistry.IsRegistered(new TypedService(typeof(IMediator))));
            Assert.True(container.ComponentRegistry.IsRegistered(new TypedService(typeof(ServiceFactory))));
        }

        [Fact]
        public void AddRequestModules_ShouldLoadModule()
        {
            _builder.RegisterRequestModules(typeof(ContainerBuilderExtensionTests).Assembly);

            Assert.True(_builder.Build().IsRegistered<IRequestHandler<TestRequest, TestResponse>>());
        }

        [Fact]
        public void AddNotificationModules_ShouldLoadModule()
        {
            _builder.RegisterNotificationModules(typeof(ContainerBuilderExtensionTests).Assembly);

            Assert.True(_builder.Build().IsRegistered<INotificationHandler<TestNotification>>());
        }
    }
}