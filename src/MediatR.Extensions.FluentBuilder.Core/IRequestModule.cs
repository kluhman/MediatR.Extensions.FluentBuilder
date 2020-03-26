using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder
{
    public interface IRequestModule<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        // Return type used to enforce pipeline is completely configured
        // ReSharper disable once UnusedMethodReturnValue.Global
        IExceptionsPipelineBuilder<TRequest, TResponse> BuildPipeline(IPipelineBuilder<TRequest, TResponse> builder);
    }
}