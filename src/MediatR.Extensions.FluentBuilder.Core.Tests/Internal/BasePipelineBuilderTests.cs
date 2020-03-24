using System;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Extensions.FluentBuilder.Internal;
using MediatR.Pipeline;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class BasePipelineBuilderTests
    {
        [Fact]
        public void AddExceptionHandler_ShouldThrowException_WhenProcessorHasNotBeenRegistered()
        {
            var builder = new TestPipelineBuilder();
            Assert.Throws<ArgumentException>(() => builder.AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>());
        }
        
        [Fact]
        public void AddExceptionHandler_ShouldNotThrowException_WhenProcessorHasBeenRegistered()
        {
            var builder = new TestPipelineBuilder();
            builder
                .AddExceptionHandling()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
        }
        
        [Fact]
        public void AddExceptionAction_ShouldThrowException_WhenProcessorHasNotBeenRegistered()
        {
            var builder = new TestPipelineBuilder();
            Assert.Throws<ArgumentException>(() => builder.AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>());
        }
        
        [Fact]
        public void AddExceptionAction_ShouldNotThrowException_WhenProcessorHasBeenRegistered()
        {
            var builder = new TestPipelineBuilder();
            builder
                .AddExceptionActions()
                .AddHandler<TestRequest.Handler>()
                .AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();
        }
        
        private sealed class TestPipelineBuilder : BasePipelineBuilder<TestRequest, TestResponse>
        {
            public override IBehaviorPipelineBuilder<TestRequest, TestResponse> AddBehavior<TBehavior>()
            {
                return this;
            }

            public override IPostProcessorPipelineBuilder<TestRequest, TestResponse> AddHandler<THandler>()
            {
                return this;
            }

            public override IPipelineBuilder<TestRequest, TestResponse> AddPreProcessor<TProcessor>()
            {
                return this;
            }

            public override IPostProcessorPipelineBuilder<TestRequest, TestResponse> AddPostProcessor<TProcessor>()
            {
                return this;
            }

            protected override IExceptionsPipelineBuilder<TestRequest, TestResponse> AddExceptionActionInternal<TException, TAction>()
            {
                return this;
            }

            protected override IExceptionsPipelineBuilder<TestRequest, TestResponse> AddExceptionHandlerInternal<TException, THandler>()
            {
                return this;
            }
        }
    }
}