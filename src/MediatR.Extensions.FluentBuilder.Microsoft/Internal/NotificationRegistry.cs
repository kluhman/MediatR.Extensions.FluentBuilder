using System;
using System.Collections.Generic;
using System.Linq;

using MediatR.Extensions.FluentBuilder.Builders;

using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.FluentBuilder.Internal
{
    internal class NotificationRegistry<TNotification> : INotificationRegistry<TNotification> where TNotification : INotification
    {
        private readonly IServiceCollection _services;

        public NotificationRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public INotificationRegistry<TNotification> AddHandler<THandler>() where THandler : class, INotificationHandler<TNotification>
        {
            _services.AddTransient<INotificationHandler<TNotification>, THandler>();
            return this;
        }
    }
}