namespace DDDExample.Domain.Core.MessageBus
{
    public interface ISubscriberOptions
    {
        public string ExchangeName { get; set; }
        string QueueName { get; set; }
        string DeadLetterExchangeName { get; set; }
        string DeadLetterQueue { get; set; }
    }
}