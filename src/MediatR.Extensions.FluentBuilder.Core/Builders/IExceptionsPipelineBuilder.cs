using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Pipeline;

namespace MediatR.Extensions.FluentBuilder.Builders
{
    public interface IExceptionsPipelineBuilder<out TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionHandler<TException, THandler>()
            where TException : Exception
            where THandler : class, IRequestExceptionHandler<TRequest, TResponse, TException>;

        IExceptionsPipelineBuilder<TRequest, TResponse> AddExceptionAction<TException, TAction>()
            where TException : Exception
            where TAction : class, IRequestExceptionAction<TRequest, TException>;
    }
}