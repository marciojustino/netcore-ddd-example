using DDDExample.Domain.Core.MessageBus;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    public class RabbitMqSubscriber : IMessageBusSubscriber<string>
    {
        private readonly IBusConnection<IModel> _connection;
        private ISubscriberOptions _options;
        private IModel _channel;
        private readonly ILogger<RabbitMqSubscriber> _logger;

        public RabbitMqSubscriber(IBusConnection<IModel> connection, ISubscriberOptions options, ILogger<RabbitMqSubscriber> logger)
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

        public event IMessageBusSubscriber<string>.OnMessageReceived OnSubscription;

        private void InitSubscription()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += OnMessageReceivedAsync;

            _channel.BasicConsume(queue: _options.QueueName, autoAck: false, consumer: consumer);
        }

        private async Task OnMessageReceivedAsync(object sender, BasicDeliverEventArgs eventArgs)
        {
            var consumer = sender as IBasicConsumer;
            var channel = consumer?.Model ?? _channel;
            try
            {
                var body = Encoding.UTF8.GetString(eventArgs.Body.Span);
                if (OnSubscription is not null)
                    await OnSubscription(body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error has occurred while processing message from message bus.|Error={ex.Message}|Trace={ex.StackTrace}");
                if (eventArgs.Redelivered)
                    channel.BasicReject(eventArgs.DeliveryTag, false);
                else
                    channel.BasicNack(eventArgs.DeliveryTag, false, true);
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

            _channel.CallbackException += (sender, ea) => Start();
        }
    }
}
