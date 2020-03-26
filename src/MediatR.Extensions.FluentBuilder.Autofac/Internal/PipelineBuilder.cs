using System;
using System.Collections.Generic;
using System.Linq;

using Autofac;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class PipelineBuilder<TRequest, TResponse> : BasePipelineBuilder<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ContainerBuilder _builder;

        public PipelineBuilder(ContainerBuilder builder)
        {
            _builder = builder;
        }

        protected override void RegisterInternal<TInterface, TImplementation>()
        {
            _builder.RegisterType<TImplementation>().As<TInterface>().InstancePerRequest();
        }
    }
}