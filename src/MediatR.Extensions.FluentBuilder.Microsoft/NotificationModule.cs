using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Builders;
using MediatR.Extensions.FluentBuilder.Internal;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder
{
    public abstract class NotificationModule<TNotification> : Module, INotificationModule<TNotification> where TNotification : INotification
    {
        public sealed override void Load(IServiceCollection services)
        {
            RegisterHandlers(new NotificationRegistry<TNotification>(services));
        }

        public abstract void RegisterHandlers(INotificationRegistry<TNotification> registry);
    }
}