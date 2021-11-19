namespace DDDExample.Infra.CrossCutting.Extensions
{
    using Domain.Core.MessageBus;
    using MessageBus.RabbitMQ;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using RabbitMQ.Client;

    public static class MessageBusExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("MessageBus:RabbitMQ");
            var exchangeName = rabbitMqConfig["Exchange"];
            var queueName = rabbitMqConfig["Queue"];
            var deadLetterExchange = rabbitMqConfig["DeadLetterExchange"];
            var deadLetterQueue = rabbitMqConfig["DeadLetterQueue"];
            var subscriberOptions = new RabbitMqSubscriberOptions(exchangeName, queueName, deadLetterExchange, deadLetterQueue);
            var publisherOptions = new RabbitMqPublisherOptions(exchangeName);
            services.AddSingleton<ISubscriberOptions>(subscriberOptions);
            services.AddSingleton<IPublisherOptions>(publisherOptions);

            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfig["HostName"],
                UserName = rabbitMqConfig["UserName"],
                Password = rabbitMqConfig["Password"],
                Port = AmqpTcpEndpoint.UseDefaultPort,
                DispatchConsumersAsync = true, // this is mandatory to have Async Subscribers
            };
            
            services.AddSingleton<IConnectionFactory>(connectionFactory);
            services.AddSingleton<IBusConnection<IModel>, RabbitMQPersistentConnection>();
            services.AddSingleton<IMessageBusPublisher<string>, RabbitMqPublisher>();
            services.AddSingleton<IMessageBusSubscriber<string>, RabbitMqSubscriber>();

            return services;
        }
    }
}