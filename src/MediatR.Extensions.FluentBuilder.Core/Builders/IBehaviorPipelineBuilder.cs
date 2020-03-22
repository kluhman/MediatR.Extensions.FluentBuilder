namespace MediatR.Extensions.FluentBuilder.Builders
{
    public interface IBehaviorPipelineBuilder<out TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        IBehaviorPipelineBuilder<TRequest, TResponse> AddBehavior<TBehavior>() 
            where TBehavior : class, IPipelineBehavior<TRequest, TResponse>;
        IPostProcessorPipelineBuilder<TRequest, TResponse> AddHandler<THandler>() 
            where THandler : class, IRequestHandler<TRequest, TResponse>;
    }
}