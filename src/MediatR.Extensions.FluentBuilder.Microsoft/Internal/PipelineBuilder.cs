using System;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class PipelineBuilder<TRequest, TResponse> : BasePipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IServiceCollection _services;

        public PipelineBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public override IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>()
        {
            _services.AddTransient<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
            return this;
        }

        public override IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>()
        {
            _services.AddTransient<IRequestHandler<TRequest, TResponse>, THandler>();
            return this;
        }

        public override IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>()
        {
            _services.AddTransient<IRequestPreProcessor<TRequest>, TProcessor>();
            return this;
        }

        public override IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>()
        {
            _services.AddTransient<IRequestPostProcessor<TRequest, TResponse>, TProcessor>();
            return this;
        }

        protected override bool HasRegisteredExceptionActionProcessor()
        {
            return _services.Any(x => x.ImplementationType == typeof(RequestExceptionActionProcessorBehavior<TRequest, TResponse>));
        }

        protected override bool HasRegisteredExceptionHandlerProcessor()
        {
            return _services.Any(x => x.ImplementationType == typeof(RequestExceptionProcessorBehavior<TRequest, TResponse>));
        }

        protected override IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionActionInternal<TException, TAction>()
        {
            _services.AddTransient<IRequestExceptionAction<TRequest, TException>, TAction>();
            return this;
        }

        protected override IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandlerInternal<TException, THandler>()
        {
            _services.AddTransient<IRequestExceptionHandler<TRequest, TResponse, TException>, THandler>();
            return this;
        }
    }
}