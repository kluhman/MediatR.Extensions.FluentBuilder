using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class RequestModule<TRequest, TResponse> : Module, IRequestModule<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected override void Load(ContainerBuilder builder)
        {
            
        }

        public abstract IExceptionsPipelineBuilder<TRequest, TResponse> BuildPipeline(IPipelineBuilder<TRequest, TResponse> builder);
    }
}