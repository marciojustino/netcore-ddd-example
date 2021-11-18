using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Core.MessageBus
{
    public interface IMessageBusHandler
    {
        Task HandleAsTopicAsync(string queueName, Func<string, Task<Result>> action);
    }
}
