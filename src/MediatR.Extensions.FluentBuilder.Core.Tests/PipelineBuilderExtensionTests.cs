using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Pipeline;

using Moq;

using Xunit;

namespace MediatR.Extensions.FluentBuilder.Tests
{
    public class PipelineBuilderExtensionTests
    {
        private readonly Mock<IBehaviorPipelineBuilder<TestRequest, TestResponse>> _builder;

        public PipelineBuilderExtensionTests()
        {
            _builder = new Mock<IBehaviorPipelineBuilder<TestRequest, TestResponse>>();
        }

        [Fact]
        public void AddExceptionHandling_ShouldAddExceptionHandlingProcessor()
        {
            _builder.Object.AddExceptionHandling();
            
            _builder.Verify(x => x.AddBehavior<RequestExceptionProcessorBehavior<TestRequest, TestResponse>>());
        }
        
        [Fact]
        public void AddExceptionActions_ShouldAddExceptionHandlingProcessor()
        {
            _builder.Object.AddExceptionActions();
            
            _builder.Verify(x => x.AddBehavior<RequestExceptionActionProcessorBehavior<TestRequest, TestResponse>>());
        }
    }
}