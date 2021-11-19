namespace DDDExample.Domain.Core.MessageBus
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public interface IMessageBusSubscriber<TMessage>
    {
        public delegate Task<Result> OnMessageReceived(TMessage message);

        void Start();

        event OnMessageReceived OnSubscription;
        // event EventHandler<SubscriberEventArgs<TMessage>> OnMessage;
    }
}