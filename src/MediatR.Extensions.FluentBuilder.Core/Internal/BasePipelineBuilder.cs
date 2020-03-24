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
        private bool _areExceptionActionsRegistered;
        private bool _areExceptionHandlingRegistered;
        
        public abstract IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>()
            where TBehavior : class, IPipelineBehavior<TRequest, TResponse>;

        public abstract IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>()
            where THandler : class, IRequestHandler<TRequest, TResponse>;

        public abstract IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>()
            where TProcessor : class, IRequestPreProcessor<TRequest>;

        public IBehaviorPipelineBuilder<TRequest, TResponse> AddExceptionHandling()
        {
            _areExceptionHandlingRegistered = true;
            return AddBehavior<RequestExceptionProcessorBehavior<TRequest, TResponse>>();
        }
        
        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandler<TException, THandler>()
            where TException : Exception where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>
        {
            if (!_areExceptionHandlingRegistered)
            {
                var message = $"Cannot register exception handler before calling {nameof(AddExceptionHandling)}";
                throw new ArgumentException(message);
            }
            
            return AddExceptionHandlerInternal<TException, THandler>();
        }

        public IBehaviorPipelineBuilder<TRequest, TResponse> AddExceptionActions()
        {
            _areExceptionActionsRegistered = true;
            return AddBehavior<RequestExceptionActionProcessorBehavior<TRequest, TResponse>>();
        }
        
        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionAction<TException, TAction>()
            where TException : Exception where TAction : class, IRequestExceptionAction<TRequest, TException>
        {
            if (!_areExceptionActionsRegistered)
            {
                var message = $"Cannot register exception action before calling {nameof(AddExceptionActions)}";
                throw new ArgumentException(message);
            }
            
            return AddExceptionActionInternal<TException, TAction>();
        }
        
        public abstract IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>()
            where TProcessor : class, IRequestPostProcessor<TRequest, TResponse>;
        
        protected abstract IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionActionInternal<TException, TAction>()
            where TException : Exception where TAction : class, IRequestExceptionAction<TRequest, TException>;
        
        protected abstract IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandlerInternal<TException, THandler>()
            where TException : Exception where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>;
    }
}