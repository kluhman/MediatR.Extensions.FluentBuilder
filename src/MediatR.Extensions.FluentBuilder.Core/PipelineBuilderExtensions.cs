using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder
{
    public static class PipelineBuilderExtensions
    {
        public static IBehaviorPipelineBuilder<TRequest, TResponse> AddExceptionHandling<TRequest, TResponse>(this IBehaviorPipelineBuilder<TRequest, TResponse> builder)
            where TRequest : IRequest<TResponse>
        {
            return builder.AddBehavior<RequestExceptionProcessorBehavior<TRequest, TResponse>>();
        }
        
        public static IBehaviorPipelineBuilder<TRequest, TResponse> AddExceptionActions<TRequest, TResponse>(this IBehaviorPipelineBuilder<TRequest, TResponse> builder)
            where TRequest : IRequest<TResponse>
        {
            return builder.AddBehavior<RequestExceptionActionProcessorBehavior<TRequest, TResponse>>();
        }
    }
}