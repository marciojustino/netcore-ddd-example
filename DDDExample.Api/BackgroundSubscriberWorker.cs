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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Start();
            _subscriber.OnSubscription += OnReceiveMessage;
            return Task.CompletedTask;
        }

        private Task<Result> OnReceiveMessage(string message)
        {
            _logger.LogDebug("[x] Received message form message bus.|Message={0}", message);
            return Task.FromResult(Result.Success());
        }
    }
}
