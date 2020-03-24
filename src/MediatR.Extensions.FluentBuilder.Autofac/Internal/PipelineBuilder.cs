using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class PipelineBuilder<TRequest, TResponse> : BasePipelineBuilder<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ContainerBuilder _builder;

        public PipelineBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }
        
        public override IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>()
        {
            _builder.RegisterType<TBehavior>().As<IPipelineBehavior<TRequest, TResponse>>();
            return this;
        }

        public override IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>()
        {
            _builder.RegisterType<THandler>().As<IRequestHandler<TRequest, TResponse>>();
            return this;
        }

        public override IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>()
        {
            _builder.RegisterType<TProcessor>().As<IRequestPreProcessor<TRequest>>();
            return this;
        }

        public override IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>()
        {
            _builder.RegisterType<TProcessor>().As<IRequestPostProcessor<TRequest, TResponse>>();
            return this;
        }

        protected override IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionActionInternal<TException, TAction>()
        {
            _builder.RegisterType<TAction>().As<IRequestExceptionAction<TRequest, TException>>();
            return this;
        }

        protected override IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandlerInternal<TException, THandler>()
        {
            _builder.RegisterType<THandler>().As<IRequestExceptionHandler<TRequest, TResponse, TException>>();
            return this;
        }
    }
}