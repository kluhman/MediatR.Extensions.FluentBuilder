using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Internal;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class PipelineBuilderTests
    {
        [Fact]
        public void RegisterInternal_ShouldRegisterClass()
        {
            var services = new ServiceCollection();
            var wrapper = new PipelineBuilderWrapper(services);
            
            wrapper.RegisterInternal<IRequestHandler<TestRequest, TestResponse>, TestRequest.Handler>();
            
            Assert.Single(services, x => x.ServiceType == typeof(IRequestHandler<TestRequest, TestResponse>) && x.ImplementationType == typeof(TestRequest.Handler));
        }
        
        private sealed class PipelineBuilderWrapper : PipelineBuilder<TestRequest, TestResponse>
        {
            public PipelineBuilderWrapper(IServiceCollection services) : base(services)
            {
            }

            public new void RegisterInternal<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
            {
                base.RegisterInternal<TInterface, TImplementation>();
            }
        }
    }
}