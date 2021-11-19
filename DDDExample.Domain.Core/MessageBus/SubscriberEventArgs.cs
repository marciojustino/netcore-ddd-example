namespace DDDExample.Domain.Core.MessageBus
{
    using System;

    public class SubscriberEventArgs<TMessage> : EventArgs
    {
        public SubscriberEventArgs(TMessage message) => Message = message;

        public TMessage Message { get; }
    }
}