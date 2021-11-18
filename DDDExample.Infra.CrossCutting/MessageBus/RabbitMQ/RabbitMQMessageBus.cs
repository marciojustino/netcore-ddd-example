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
    public class RabbitMQMessageBus : IMessageBusSender, IMessageBusHandler
    {
        private readonly RabbitMQSettings _settings;
        private readonly ILogger<RabbitMQMessageBus> _logger;

        public RabbitMQMessageBus(IOptionsMonitor<RabbitMQSettings> optionsMonitor, ILogger<RabbitMQMessageBus> logger)
        {
            _settings = optionsMonitor.CurrentValue;
            _logger = logger;
        }

        protected IConnection CreateConnection()
        {
            var factory = new ConnectionFactory() { HostName = _settings.Host };
            return factory.CreateConnection();
        }

        public Task<Result> PublishAsTopicAsync(string topic, string message)
        {
            if(string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            var exchange = $"topic_{topic.ToLower()}";
            var routingKey = $"*.{topic}.data";
            var body = Encoding.UTF8.GetBytes(message);

            using var connection = CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);
            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: null, body: body);
            _logger.LogDebug("[x] Sent message to topic {0}", topic);
            return Task.FromResult(Result.Success());
        }

        /// <summary>
        /// Handle topic message async.
        /// </summary>
        /// <param name="topic">A topic name.</param>
        /// <param name="action">A function that receives a message parameter as string and return a Task as result.</param>
        /// <returns></returns>
        public Task HandleAsTopicAsync(string topic, Func<string, Task<Result>> action)
        {
            var exchange = $"topic_{topic.ToLower()}";
            var routingKey = $"*.{topic}.data";
            using var connection = CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
            _logger.LogDebug("[x] Waiting for messages from topic {0}", topic);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, args) =>
            {
                try
                {
                    var body = args.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var callbackResult = await action(message);
                    if (callbackResult.IsSuccess)
                    {
                        _logger.LogDebug("[x] Received message from topic {0}", topic);
                        channel.BasicAck(args.DeliveryTag, false);
                    }
                    else
                    {
                        _logger.LogError("Error on consume message from topic {0}.|Error={1}", topic, callbackResult.Error);
                        channel.BasicNack(args.DeliveryTag, false, false);
                    }
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Error on consume message from topic {0}", topic);
                    channel.BasicNack(args.DeliveryTag, false, false);
                }
            };
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            return Task.FromResult(Result.Success());
        }
    }
}
