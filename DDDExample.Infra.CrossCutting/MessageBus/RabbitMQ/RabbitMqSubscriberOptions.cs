namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    using Domain.Core.MessageBus;

    public class RabbitMqSubscriberOptions : ISubscriberOptions
    {
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string DeadLetterExchangeName { get; set; }
        public string DeadLetterQueue { get; set; }

        public RabbitMqSubscriberOptions(string exchangeName, string queueName, string deadLetterExchangeName, string deadLetterQueue)
        {
            ExchangeName = exchangeName;
            QueueName = queueName;
            DeadLetterExchangeName = deadLetterExchangeName;
            DeadLetterQueue = deadLetterQueue;
        }
    }
}