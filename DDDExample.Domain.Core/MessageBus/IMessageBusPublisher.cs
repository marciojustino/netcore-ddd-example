using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Core.MessageBus
{
    public interface IMessageBusPublisher<TMessage> : IDisposable
    {
        Task<Result> PublishAsync(TMessage message);
    }
}
