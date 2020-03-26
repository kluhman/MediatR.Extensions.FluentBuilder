using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    public abstract class BasePipelineBuilder<TRequest, TResponse> :
        IPipelineBuilder<TRequest, TResponse>,
        IPostProcessorPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private bool _registeredRequiredProcessors;
        protected abstract void RegisterInternal<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface;

        public IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>() where TProcessor : class, IRequestPreProcessor<TRequest>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IRequestPreProcessor<TRequest>, TProcessor>();
            return this;
        }

        public IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>() where TBehavior : class, IPipelineBehavior<TRequest, TResponse>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
            return this;
        }

        public IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>() where THandler : class, IRequestHandler<TRequest, TResponse>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IRequestHandler<TRequest, TResponse>, THandler>();
            return this;
        }

        public IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>() where TProcessor : class, IRequestPostProcessor<TRequest, TResponse>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IRequestPostProcessor<TRequest, TResponse>, TProcessor>();
            return this;
        }

        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandler<TException, THandler>() where TException : Exception where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IRequestExceptionHandler<TRequest, TResponse, TException>, THandler>();
            return this;
        }

        public IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionAction<TException, TAction>() where TException : Exception where TAction : class, IRequestExceptionAction<TRequest, TException>
        {
            RegisterRequiredProcessors();
            RegisterInternal<IRequestExceptionAction<TRequest, TException>, TAction>();
            return this;
        }

        private void RegisterRequiredProcessors()
        {
            if (_registeredRequiredProcessors)
            {
                return;
            }

            RegisterInternal<IPipelineBehavior<TRequest, TResponse>, RequestPreProcessorBehavior<TRequest, TResponse>>();
            RegisterInternal<IPipelineBehavior<TRequest, TResponse>, RequestPostProcessorBehavior<TRequest, TResponse>>();
            RegisterInternal<IPipelineBehavior<TRequest, TResponse>, RequestExceptionActionProcessorBehavior<TRequest, TResponse>>();
            RegisterInternal<IPipelineBehavior<TRequest, TResponse>, RequestExceptionProcessorBehavior<TRequest, TResponse>>();

            _registeredRequiredProcessors = true;
        }
    }
}