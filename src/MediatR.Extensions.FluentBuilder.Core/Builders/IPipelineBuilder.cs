using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Builders
{
    public interface IPipelineBuilder<out TRequest, TResponse> : IBehaviorPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        IPipelineBuilder<TRequest, TResponse> AddPreProcessor<TProcessor>() 
            where TProcessor : class, IRequestPreProcessor<TRequest>;
    }
}