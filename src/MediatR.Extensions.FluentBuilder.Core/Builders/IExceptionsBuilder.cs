using System;

using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Core.Builders
{
    public interface IExceptionsBuilder<out TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        IExceptionsBuilder<TRequest, TResponse> AddExceptionHandler<TException, THandler>()
            where TException : Exception
            where THandler : IRequestExceptionHandler<TRequest, TResponse, TException>;
        IExceptionsBuilder<TRequest, TResponse> AddExceptionAction<TException, TAction>()
            where TException : Exception
            where TAction : IRequestExceptionAction<TRequest, TException>;
    }
}