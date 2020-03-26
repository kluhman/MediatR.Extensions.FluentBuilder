using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;
using Autofac.Core;

using MediatR.Extensions.FluentBuilder.Internal;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class PipelineBuilderTests
    {
        [Fact]
        public void RegisterInternal_ShouldRegisterClass()
        {
            var builder = new ContainerBuilder();
            var wrapper = new PipelineBuilderWrapper(builder);

            wrapper.RegisterInternal<IRequestHandler<TestRequest, TestResponse>, TestRequest.Handler>();

            Assert.True(builder.Build().ComponentRegistry.IsRegistered(new TypedService(typeof(IRequestHandler<TestRequest, TestResponse>))));
        }

        private sealed class PipelineBuilderWrapper : PipelineBuilder<TestRequest, TestResponse>
        {
            public PipelineBuilderWrapper(ContainerBuilder builder) : base(builder)
            {
            }

            public new void RegisterInternal<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
            {
                base.RegisterInternal<TInterface, TImplementation>();
            }
        }
    }
}