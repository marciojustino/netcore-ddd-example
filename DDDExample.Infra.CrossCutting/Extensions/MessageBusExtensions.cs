using DDDExample.Domain.Core.MessageBus;
using DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ;
using DDDExample.Infra.CrossCutting.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Infra.CrossCutting.Extensions
{
    public static class MessageBusExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("MessageBus"));
            services.AddTransient<IMessageBusSender, RabbitMQMessageBus>();
            services.AddTransient<IMessageBusHandler, RabbitMQMessageBus>();

            return services;
        }
    }
}
