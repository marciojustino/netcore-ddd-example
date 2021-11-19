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
using System.Text.Json;
using System.Threading.Tasks;

namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    public class RabbitMQPublisher : IMessageBusPublisher<string>
    {
        private readonly IBusConnection<IModel> _connection;
        private readonly string _exchangeName;
        private readonly ILogger<RabbitMQPublisher> _logger;
        private IModel _channel;
        private IBasicProperties _properties;

        public RabbitMQPublisher(
            IBusConnection<IModel> connection,
            string exchangeName,
            ILogger<RabbitMQPublisher> logger)
        {
            if (string.IsNullOrWhiteSpace(exchangeName))
                throw new ArgumentException($"'{nameof(exchangeName)}' cannot be null or whitespace", nameof(exchangeName));

            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _exchangeName = exchangeName;
            _logger = logger;

            _channel = _connection.CreateChannel();
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
            _properties = _channel.CreateBasicProperties();
        }

        public Task<Result> PublishAsync(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);

                _channel.BasicPublish(
                    exchange: _exchangeName,
                    routingKey: string.Empty,
                    mandatory: true,
                    basicProperties: _properties,
                    body: body);

                _logger.LogDebug("[x] Message has been published to message bus {0}", _exchangeName);
                return Task.FromResult(Result.Success());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error on publish message to message bus {0}", _exchangeName);
                return Task.FromResult(Result.Failure(ex.Message));
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _channel = null;
        }
    }
}
