using System;
using System.Collections.Generic;

using MediatR.Extensions.FluentBuilder.Internal;
using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class PipelineBuilderTests
    {
        private readonly Mock<IServiceCollection> _services;
        private readonly PipelineBuilder<TestRequest, TestResponse> _builder;

        public PipelineBuilderTests()
        {
            _services = new Mock<IServiceCollection>();
            _builder = new PipelineBuilder<TestRequest, TestResponse>(_services.Object);

            _services.Setup(x => x.GetEnumerator())
                .Returns(new List<ServiceDescriptor>
                {
                    ServiceDescriptor.Transient<IPipelineBehavior<TestRequest, TestResponse>, RequestExceptionProcessorBehavior<TestRequest, TestResponse>>(),
                    ServiceDescriptor.Transient<IPipelineBehavior<TestRequest, TestResponse>, RequestExceptionActionProcessorBehavior<TestRequest, TestResponse>>()
                }.GetEnumerator());
        }
        
        [Fact]
        public void AddBehavior_ShouldAddBehavior()
        {
            _builder.AddBehavior<IPipelineBehavior<TestRequest, TestResponse>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IPipelineBehavior<TestRequest, TestResponse>) &&
                                                            d.ImplementationType == typeof(IPipelineBehavior<TestRequest, TestResponse>))));
        }

        [Fact]
        public void AddHandler_ShouldAddHandler()
        {
            _builder.AddHandler<IRequestHandler<TestRequest, TestResponse>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IRequestHandler<TestRequest, TestResponse>) &&
                                                            d.ImplementationType == typeof(IRequestHandler<TestRequest, TestResponse>))));
        }

        [Fact]
        public void AddPreProcessor_ShouldAddProcessor()
        {
            _builder.AddPreProcessor<IRequestPreProcessor<TestRequest>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IRequestPreProcessor<TestRequest>) &&
                                                            d.ImplementationType == typeof(IRequestPreProcessor<TestRequest>))));
        }

        [Fact]
        public void AddPostProcessor_ShouldAddProcessor()
        {
            _builder.AddPostProcessor<IRequestPostProcessor<TestRequest, TestResponse>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IRequestPostProcessor<TestRequest, TestResponse>) &&
                                                            d.ImplementationType == typeof(IRequestPostProcessor<TestRequest, TestResponse>))));
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldThrowException_WhenProcessorIsNotRegistered()
        {
            var services = new ServiceCollection();
            var builder = new PipelineBuilder<TestRequest, TestResponse>(services);

            Assert.Throws<ArgumentException>(() => builder.AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>());
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldNotThrowException_WhenProcessorIsRegistered()
        {
            var services = new ServiceCollection();
            var builder = new PipelineBuilder<TestRequest, TestResponse>(services);

            builder
                .AddExceptionHandling()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldRegisterExceptionHandler()
        {
            _builder.AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IRequestExceptionHandler<TestRequest, TestResponse, Exception>) &&
                                                            d.ImplementationType == typeof(IRequestExceptionHandler<TestRequest, TestResponse, Exception>))));
        }
        
        [Fact]
        public void AddExceptionAction_ShouldThrowException_WhenProcessorIsNotRegistered()
        {
            var services = new ServiceCollection();
            var builder = new PipelineBuilder<TestRequest, TestResponse>(services);

            Assert.Throws<ArgumentException>(() => builder.AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>());
        }
        
        [Fact]
        public void AddExceptionAction_ShouldNotThrowException_WhenProcessorIsRegistered()
        {
            var services = new ServiceCollection();
            var builder = new PipelineBuilder<TestRequest, TestResponse>(services);

            builder
                .AddExceptionActions()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();
        }

        [Fact]
        public void AddExceptionAction_ShouldRegisterExceptionAction()
        {
            _builder.AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();
            
            _services.Verify(x => 
                x.Add(It.Is<ServiceDescriptor>(d => d.ServiceType == typeof(IRequestExceptionAction<TestRequest, Exception>) &&
                                                                                 d.ImplementationType == typeof(IRequestExceptionAction<TestRequest, Exception>))));
        }
    }
}