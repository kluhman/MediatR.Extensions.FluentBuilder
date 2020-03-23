using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    public class PipelineBuilder<TRequest, TResponse> : 
        IPipelineBuilder<TRequest, TResponse>,
        IPostProcessorPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ContainerBuilder _builder;

        public PipelineBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }
        
        IPipelineBuilder<TRequest, TResponse> IPipelineBuilder<TRequest, TResponse>.AddPreProcessor<TProcessor>() 
        {
            _builder.RegisterType<TProcessor>().As<IRequestPreProcessor<TRequest>>().InstancePerRequest();
            return this;
        }
        
        IBehaviorPipelineBuilder<TRequest, TResponse> IBehaviorPipelineBuilder<TRequest, TResponse>.AddBehavior<TBehavior>() 
        {
            _builder.RegisterType<TBehavior>().As<IPipelineBehavior<TRequest, TResponse>>();
            return this;
        }

        IPostProcessorPipelineBuilder<TRequest, TResponse> IBehaviorPipelineBuilder<TRequest, TResponse>.AddHandler<THandler>()
        {
            _builder.RegisterType<THandler>().As<IRequestHandler<TRequest, TResponse>>();
            return this;
        }
        
        IPostProcessorPipelineBuilder<TRequest, TResponse> IPostProcessorPipelineBuilder<TRequest, TResponse>.AddPostProcessor<TProcessor>()
        {
            _builder.RegisterType<TProcessor>().As<IRequestPostProcessor<TRequest, TResponse>>();
            return this;
        }

        IExceptionsPipelineBuilder<TRequest, TResponse> IExceptionsPipelineBuilder<TRequest, TResponse>.AddExceptionHandler<TException, THandler>()
        {
            _builder.RegisterType<THandler>().As<IRequestExceptionHandler<TRequest, TResponse, TException>>();
            return this;
        }

        IExceptionsPipelineBuilder<TRequest, TResponse> IExceptionsPipelineBuilder<TRequest, TResponse>.AddExceptionAction<TException, TAction>()
        {
            _builder.RegisterType<TAction>().As<IRequestExceptionAction<TRequest, TException>>();
            return this;
        }
    }
}