namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    using Domain.Core.MessageBus;

    public class RabbitMqPublisherOptions : IPublisherOptions
    {
        public RabbitMqPublisherOptions(string exchangeName) => ExchangeName = exchangeName;

        public string ExchangeName { get; }
    }
}