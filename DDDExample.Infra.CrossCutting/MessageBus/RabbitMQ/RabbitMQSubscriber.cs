using CSharpFunctionalExtensions;
using DDDExample.Domain.Core.MessageBus;
using DDDExample.Infra.CrossCutting.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    public class RabbitMQSubscriber : IMessageBusSubscriber<string>
    {
        private readonly IBusConnection<IModel> _connection;
        private RabbitSubscriberOptions _options;
        private IModel _channel;
        private readonly ILogger<RabbitMQPublisher> _logger;

        public event EventHandler<SubscriberEventArgs<string>> OnMessage;

        public record RabbitSubscriberOptions(string ExchangeName, string QueueName, string DeadLetterExchangeName, string DeadLetterQueue);

        public RabbitMQSubscriber(IBusConnection<IModel> connection, RabbitSubscriberOptions options, ILogger<RabbitMQPublisher> logger)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Start()
        {
            InitChannel();
            InitSubscription();
        }

        private void InitSubscription()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceivedAsync; ;

            _channel.BasicConsume(queue: _options.QueueName, autoAck: false, consumer: consumer);
        }

        private Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
        {
            var consumer = sender as IBasicConsumer;
            var channel = consumer?.Model ?? _channel;
            try
            {
                var body = Encoding.UTF8.GetString(eventArgs.Body.Span);
                OnMessage(this, new SubscriberEventArgs<string>(body));
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error has occurred while processing message from message bus.|Error={ex.Message}|Trace={ex.StackTrace}");
                if (eventArgs.Redelivered)
                    channel.BasicReject(eventArgs.DeliveryTag, false);
                else
                    channel.BasicNack(eventArgs.DeliveryTag, false, true);
                return Task.FromException(ex);
            }
        }

        private void InitChannel()
        {
            _channel?.Dispose();
            _channel = _connection.CreateChannel();

            _channel.ExchangeDeclare(exchange: _options.DeadLetterExchangeName, type: ExchangeType.Fanout);
            
            _channel.QueueDeclare(queue: _options.DeadLetterQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            _channel.QueueBind(_options.DeadLetterQueue, _options.DeadLetterExchangeName, routingKey: string.Empty, arguments: null);
            
            _channel.ExchangeDeclare(exchange: _options.ExchangeName, type: ExchangeType.Fanout);

            _channel.QueueDeclare(queue: _options.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: true,
                arguments: null);

            _channel.QueueBind(_options.QueueName, _options.ExchangeName, string.Empty, null);

            _channel.CallbackException += (sender, ea) =>
            {
                Start();
            };
        }
    }
}
