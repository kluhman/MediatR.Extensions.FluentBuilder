using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    public abstract class Module
    {
        public abstract void Load(IServiceCollection services);
    }
}