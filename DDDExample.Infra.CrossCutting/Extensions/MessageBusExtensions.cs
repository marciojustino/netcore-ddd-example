using DDDExample.Domain.Core.MessageBus;
using DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ;
using DDDExample.Infra.CrossCutting.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
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
            var rabbitMQConfig = configuration.GetSection("MessageBus:RabbitMQ");
            var exchangeName = rabbitMQConfig["Exchange"];
            var queueName = rabbitMQConfig["Queue"];
            var deadLetterExchange = rabbitMQConfig["DeadLetterExchange"];
            var deadLetterQueue = rabbitMQConfig["DeadLetterQueue"];
            var subscriberOptions = new RabbitMQSubscriber.RabbitSubscriberOptions(exchangeName, queueName, deadLetterExchange, deadLetterQueue);
            services.AddSingleton(subscriberOptions);

            var connectionFactory = new ConnectionFactory()
            {
                HostName = rabbitMQConfig["HostName"],
                UserName = rabbitMQConfig["UserName"],
                Password = rabbitMQConfig["Password"],
                Port = AmqpTcpEndpoint.UseDefaultPort,
                DispatchConsumersAsync = true // this is mandatory to have Async Subscribers
            };
            services.AddSingleton<IConnectionFactory>(connectionFactory);
            services.AddSingleton<IBusConnection<IModel>, RabbitMQPersistentConnection>();
            services.AddSingleton<IMessageBusPublisher<string>>(sp =>
            {
                var connection = sp.GetRequiredService<IBusConnection<IModel>>();
                var logger = sp.GetRequiredService <ILogger<RabbitMQPublisher>>();
                return new RabbitMQPublisher(connection, exchangeName, logger);
            });
            services.AddSingleton<IMessageBusSubscriber<string>, RabbitMQSubscriber>();

            return services;
        }
    }
}
