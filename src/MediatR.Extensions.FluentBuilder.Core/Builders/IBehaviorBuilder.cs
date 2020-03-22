namespace MediatR.Extensions.FluentBuilder.Core.Builders
{
    public interface IBehaviorBuilder<out TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        IBehaviorBuilder<TRequest, TResponse> AddBehavior<TBehavior>() where TBehavior : IPipelineBehavior<TRequest, TResponse>;
        IExceptionsBuilder<TRequest, TResponse> AddHandler<THandler>() where THandler : IRequestHandler<TRequest, TResponse>;
    }
}