namespace DDDExample.Domain.Core.MessageBus
{
    using System;
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;

    public interface IMessageBusPublisher<in TMessage> : IDisposable
    {
        Task<Result> PublishAsync(string exchangeName, TMessage message);
    }
}