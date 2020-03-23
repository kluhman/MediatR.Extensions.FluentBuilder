using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Extensions.FluentBuilder.Internal;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class RequestModule<TRequest, TResponse> : Module, IRequestModule<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public sealed override void Load(IServiceCollection services)
        {
            BuildPipeline(new PipelineBuilder<TRequest, TResponse>(services));
        }

        // Return type used to enforce pipeline is completely configured
        // ReSharper disable once UnusedMethodReturnValue.Global
        public abstract IExceptionsPipelineBuilder<TRequest, TResponse> BuildPipeline(IPipelineBuilder<TRequest, TResponse> builder);
    }
}