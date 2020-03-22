using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Builders
{
    public interface IPostProcessorPipelineBuilder<out TRequest, TResponse> : IExceptionsPipelineBuilder<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        IPostProcessorPipelineBuilder<TRequest, TResponse> AddPostProcessor<TProcessor>() 
            where TProcessor : class, IRequestPostProcessor<TRequest, TResponse>;
    }
}