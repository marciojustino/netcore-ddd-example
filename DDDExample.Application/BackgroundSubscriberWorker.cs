using CSharpFunctionalExtensions;
using DDDExample.Domain.Core.MessageBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDDExample.Api
{
    public class BackgroundSubscriberWorker : BackgroundService
    {
        private readonly IMessageBusSubscriber<string> _subscriber;
        private readonly ILogger<BackgroundSubscriberWorker> _logger;

        public BackgroundSubscriberWorker(IMessageBusSubscriber<string> subscriber, ILogger<BackgroundSubscriberWorker> logger)
        {
            _subscriber = subscriber;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Start();
            _subscriber.OnMessage += ReceivedMessage;
        }

        private void ReceivedMessage(object sender, SubscriberEventArgs<string> e)
        {
            _logger.LogDebug("[x] Received message form message bus.|Message={0}", e.Message);
        }
    }
}
