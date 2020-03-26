using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR.Extensions.FluentBuilder.Builders;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class TestRequest : IRequest<TestResponse>
    {
        internal class Handler : IRequestHandler<TestRequest, TestResponse>
        {
            public Task<TestResponse> Handle(TestRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new TestResponse());
            }
        }

        internal class Module : IRequestModule<TestRequest, TestResponse>
        {
            public IExceptionsPipelineBuilder<TestRequest, TestResponse> BuildPipeline(IPipelineBuilder<TestRequest, TestResponse> builder)
            {
                return builder.AddHandler<Handler>();
            }
        }
    }
}