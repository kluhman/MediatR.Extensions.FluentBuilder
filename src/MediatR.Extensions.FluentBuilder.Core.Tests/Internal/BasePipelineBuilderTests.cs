using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Internal;
using MediatR.Pipeline;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests.Internal
{
    public class BasePipelineBuilderTests : BasePipelineBuilder<TestRequest, TestResponse>
    {
        private readonly List<KeyValuePair<Type, Type>> _registrations;

        public BasePipelineBuilderTests()
        {
            _registrations = new List<KeyValuePair<Type, Type>>();
        }

        [Fact]
        public void RequiredProcessors_ShouldOnlyBeRegisteredOnce()
        {
            AddHandler<TestRequest.Handler>();
            AddHandler<TestRequest.Handler>();

            Assert_RequiredTypesAreRegistered();
        }

        [Fact]
        public void AddPreProcessor_ShouldRegisterType()
        {
            AddPreProcessor<IRequestPreProcessor<TestRequest>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IRequestPreProcessor<TestRequest>, IRequestPreProcessor<TestRequest>>();
        }

        [Fact]
        public void AddBehavior_ShouldRegisterType()
        {
            AddBehavior<IPipelineBehavior<TestRequest, TestResponse>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IPipelineBehavior<TestRequest, TestResponse>, IPipelineBehavior<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddHandler_ShouldRegisterType()
        {
            AddHandler<IRequestHandler<TestRequest, TestResponse>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IRequestHandler<TestRequest, TestResponse>, IRequestHandler<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddPostProcessor_ShouldRegisterType()
        {
            AddPostProcessor<IRequestPostProcessor<TestRequest, TestResponse>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IRequestPostProcessor<TestRequest, TestResponse>, IRequestPostProcessor<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddExceptionHandler_ShouldRegisterType()
        {
            AddExceptionHandler<Exception, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IRequestExceptionHandler<TestRequest, TestResponse, Exception>, IRequestExceptionHandler<TestRequest, TestResponse, Exception>>();
        }

        [Fact]
        public void AddExceptionAction_ShouldRegisterType()
        {
            AddExceptionAction<Exception, IRequestExceptionAction<TestRequest, Exception>>();

            Assert_RequiredTypesAreRegistered();
            Assert_TypeIsRegistered<IRequestExceptionAction<TestRequest, Exception>, IRequestExceptionAction<TestRequest, Exception>>();
        }

        private void Assert_RequiredTypesAreRegistered()
        {
            Assert_TypeIsRegistered<IPipelineBehavior<TestRequest, TestResponse>, RequestPreProcessorBehavior<TestRequest, TestResponse>>();
            Assert_TypeIsRegistered<IPipelineBehavior<TestRequest, TestResponse>, RequestPostProcessorBehavior<TestRequest, TestResponse>>();
            Assert_TypeIsRegistered<IPipelineBehavior<TestRequest, TestResponse>, RequestExceptionProcessorBehavior<TestRequest, TestResponse>>();
            Assert_TypeIsRegistered<IPipelineBehavior<TestRequest, TestResponse>, RequestExceptionActionProcessorBehavior<TestRequest, TestResponse>>();
        }

        private void Assert_TypeIsRegistered<TInterface, TImplementation>()
        {
            Assert.Single(_registrations, x => x.Key == typeof(TInterface) && x.Value == typeof(TImplementation));
        }

        protected override void RegisterInternal<TInterface, TImplementation>()
        {
            _registrations.Add(new KeyValuePair<Type, Type>(typeof(TInterface), typeof(TImplementation)));
        }
    }
}