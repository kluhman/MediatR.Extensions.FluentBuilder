using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class Module
    {
        public abstract void Load(IServiceCollection services);
    }
}