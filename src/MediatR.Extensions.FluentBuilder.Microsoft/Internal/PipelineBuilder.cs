using System;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class PipelineBuilder<TRequest, TResponse> : 
        IPipelineBuilder<TRequest, TResponse>,
        IPostProcessorPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IServiceCollection _services;

        public PipelineBuilder(IServiceCollection services)
        {
            _services = services;
        }

        IPipelineBuilder<TRequest, TResponse> IPipelineBuilder<TRequest, TResponse>.AddPreProcessor<TProcessor>() 
        {
            _services.AddTransient<IRequestPreProcessor<TRequest>, TProcessor>();
            return this;
        }
        
        IBehaviorPipelineBuilder<TRequest, TResponse> IBehaviorPipelineBuilder<TRequest, TResponse>.AddBehavior<TBehavior>() 
        {
            _services.AddTransient<IPipelineBehavior<TRequest, TResponse>, TBehavior>();
            return this;
        }

        IPostProcessorPipelineBuilder<TRequest, TResponse> IBehaviorPipelineBuilder<TRequest, TResponse>.AddHandler<THandler>()
        {
            _services.AddTransient<IRequestHandler<TRequest, TResponse>, THandler>();
            return this;
        }
        
        IPostProcessorPipelineBuilder<TRequest, TResponse> IPostProcessorPipelineBuilder<TRequest, TResponse>.AddPostProcessor<TProcessor>()
        {
            _services.AddTransient<IRequestPostProcessor<TRequest, TResponse>, TProcessor>();
            return this;
        }

        IExceptionsPipelineBuilder<TRequest, TResponse> IExceptionsPipelineBuilder<TRequest, TResponse>.AddExceptionHandler<TException, THandler>()
        {
            _services.AddTransient<IRequestExceptionHandler<TRequest, TResponse, TException>, THandler>();
            return this;
        }

        IExceptionsPipelineBuilder<TRequest, TResponse> IExceptionsPipelineBuilder<TRequest, TResponse>.AddExceptionAction<TException, TAction>()
        {
            _services.AddTransient<IRequestExceptionAction<TRequest, TException>, TAction>();
            return this;
        }
    }
}