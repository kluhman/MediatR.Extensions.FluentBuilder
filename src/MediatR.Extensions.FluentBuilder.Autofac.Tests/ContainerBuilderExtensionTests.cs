using Autofac;

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