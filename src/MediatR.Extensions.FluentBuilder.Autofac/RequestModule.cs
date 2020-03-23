using Autofac;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Extensions.FluentBuilder.Internal;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class RequestModule<TRequest, TResponse> : Module, IRequestModule<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        protected override void Load(ContainerBuilder builder)
        {
            BuildPipeline(new PipelineBuilder<TRequest, TResponse>(builder));
        }

        public abstract IExceptionsPipelineBuilder<TRequest, TResponse> BuildPipeline(IPipelineBuilder<TRequest, TResponse> builder);
    }
}