using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class ServiceCollectionExtensionTests
    {
        private readonly IServiceCollection _services;

        public ServiceCollectionExtensionTests()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddModule_ShouldLoadNewModule()
        {
            _services.AddModule<TestRequest.Module>();

            Assert.NotEmpty(_services);
        }

        [Fact]
        public void AddModule_ShouldLoadModule()
        {
            var module = new Mock<Module>();
            _services.AddModule(module.Object);

            module.Verify(x => x.Load(_services));
        }

        [Fact]
        public void AddMediatR_ShouldRegisterRequiredDependencies()
        {
            _services.AddMediatR();

            Assert.Single(_services, x => x.ServiceType == typeof(IMediator));
            Assert.Single(_services, x => x.ServiceType == typeof(ServiceFactory));
        }

        [Fact]
        public void AddRequestModules_ShouldLoadModule()
        {
            _services.AddRequestModules(typeof(ServiceCollectionExtensionTests).Assembly);

            Assert.NotEmpty(_services);
        }

        [Fact]
        public void AddNotificationModules_ShouldLoadModule()
        {
            _services.AddNotificationModules(typeof(ServiceCollectionExtensionTests).Assembly);

            Assert.NotEmpty(_services);
        }

        [Fact]
        public void AddModules_ShouldLoadModules()
        {
            _services.AddModules();
            
            Assert.NotEmpty(_services);
        }
    }
}