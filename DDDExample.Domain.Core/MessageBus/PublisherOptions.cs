namespace DDDExample.Domain.Core.MessageBus
{
    public class PublisherOptions
    {
        public PublisherOptions(string exchangeName) => ExchangeName = exchangeName;

        public string ExchangeName { get; }
    }
}