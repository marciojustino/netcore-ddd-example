namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Domain.Core.MessageBus;
    using global::RabbitMQ.Client;
    using Microsoft.Extensions.Logging;

    public class RabbitMqPublisher : IMessageBusPublisher<string>
    {
        private readonly ILogger<RabbitMqPublisher> _logger;
        private readonly IBasicProperties _properties;
        private IModel _channel;

        public RabbitMqPublisher(
            IBusConnection<IModel> connection,
            IPublisherOptions options,
            ILogger<RabbitMqPublisher> logger)
        {
            var connection1 = connection ?? throw new ArgumentNullException(nameof(connection));
            var options1 = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;

            _channel = connection1.CreateChannel();
            _channel.ExchangeDeclare(options1.ExchangeName, ExchangeType.Fanout);
            _properties = _channel.CreateBasicProperties();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _channel = null;
        }

        public Task<Result> PublishAsync(string exchangeName, string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(
                    exchangeName,
                    string.Empty,
                    true,
                    _properties,
                    body);

                _logger.LogDebug("[x] Message has been published to message bus {0}", exchangeName);
                return Task.FromResult(Result.Success());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on publish message to message bus {0}", exchangeName);
                return Task.FromResult(Result.Failure(ex.Message));
            }
        }
    }
}