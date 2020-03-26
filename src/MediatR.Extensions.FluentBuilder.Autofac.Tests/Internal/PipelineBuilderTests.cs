using System;
using System.Collections.Generic;

using Autofac;

using MediatR.Extensions.FluentBuilder.Internal;
using MediatR.Pipeline;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class PipelineBuilderTests
    {
        private readonly ContainerBuilder _services;
        private readonly PipelineBuilder<TestRequest, TestResponse> _builder;

        public PipelineBuilderTests()
        {
            _services = new ContainerBuilder();
            _builder = new PipelineBuilder<TestRequest, TestResponse>(_services);
        }
        
        [Fact]
        public void AddBehavior_ShouldAddBehavior()
        {
            _builder.AddBehavior<IPipelineBehavior<TestRequest, TestResponse>>();

            Assert_ServiceIsRegistered<IPipelineBehavior<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddHandler_ShouldAddHandler()
        {
            _builder.AddHandler<IRequestHandler<TestRequest, TestResponse>>();

            Assert_ServiceIsRegistered<IRequestHandler<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddPreProcessor_ShouldAddProcessor()
        {
            _builder.AddPreProcessor<IRequestPreProcessor<TestRequest>>();

            Assert_ServiceIsRegistered<IRequestPreProcessor<TestRequest>>();
        }

        [Fact]
        public void AddPostProcessor_ShouldAddProcessor()
        {
            _builder.AddPostProcessor<IRequestPostProcessor<TestRequest, TestResponse>>();

            Assert_ServiceIsRegistered<IRequestPostProcessor<TestRequest, TestResponse>>();
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldThrowException_WhenProcessorIsNotRegistered()
        {
            Assert.Throws<ArgumentException>(() => _builder.AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>());
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldNotThrowException_WhenProcessorIsRegistered()
        {
            _builder
                .AddExceptionHandling()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldRegisterExceptionHandler()
        {
            _builder
                .AddExceptionHandling()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();

            Assert_ServiceIsRegistered<IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
        }
        
        [Fact]
        public void AddExceptionAction_ShouldThrowException_WhenProcessorIsNotRegistered()
        {
            Assert.Throws<ArgumentException>(() => _builder.AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>());
        }
        
        [Fact]
        public void AddExceptionAction_ShouldNotThrowException_WhenProcessorIsRegistered()
        {
            _builder
                .AddExceptionActions()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();
        }

        [Fact]
        public void AddExceptionAction_ShouldRegisterExceptionAction()
        {
            _builder
                .AddExceptionActions()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();

            Assert_ServiceIsRegistered<IRequestExceptionAction<TestRequest, Exception>>();
        }

        private void Assert_ServiceIsRegistered<T>() where T : notnull
        {
            Assert.True(_services.Build().IsRegistered<T>());
        }
    }
}