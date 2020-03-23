using System;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    public abstract class BasePipelineBuilder<TRequest, TResponse> : 
        IPipelineBuilder<TRequest, TResponse>,
        IPostProcessorPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public abstract IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>()
            where TBehavior : class, IPipelineBehavior<TRequest, TResponse>;

        public abstract IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>()
            where THandler : class, IRequestHandler<TRequest, TResponse>;

        public abstract IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>()
            where TProcessor : class, IRequestPreProcessor<TRequest>;

        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandler<TException, THandler>()
            where TException : Exception where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>
        {
            if (!HasRegisteredExceptionHandlerProcessor())
            {
                var message = $"Cannot register exception handler before calling builder.{nameof(PipelineBuilderExtensions.AddExceptionHandling)}";
                throw new ArgumentException(message);
            }
            
            return AddExceptionHandlerInternal<TException, THandler>();
        }

        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionAction<TException, TAction>()
            where TException : Exception where TAction : class, IRequestExceptionAction<TRequest, TException>
        {
            if (!HasRegisteredExceptionActionProcessor())
            {
                var message = $"Cannot register exception action before calling builder.{nameof(PipelineBuilderExtensions.AddExceptionActions)}";
                throw new ArgumentException(message);
            }
            
            return AddExceptionActionInternal<TException, TAction>();
        }
        
        public abstract IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>()
            where TProcessor : class, IRequestPostProcessor<TRequest, TResponse>;

        protected abstract bool HasRegisteredExceptionActionProcessor();
        protected abstract bool HasRegisteredExceptionHandlerProcessor();
        protected abstract IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionActionInternal<TException, TAction>()
            where TException : Exception where TAction : class, IRequestExceptionAction<TRequest, TException>;
        protected abstract IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandlerInternal<TException, THandler>()
            where TException : Exception where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>;
    }
}