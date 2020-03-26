using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class PipelineBuilder<TRequest, TResponse> : BasePipelineBuilder<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IServiceCollection _services;

        public PipelineBuilder(IServiceCollection services)
        {
            _services = services;
        }

        protected override void RegisterInternal<TInterface, TImplementation>()
        {
            _services.AddTransient<TInterface, TImplementation>();
        }
    }
}