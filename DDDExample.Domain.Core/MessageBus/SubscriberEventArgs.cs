using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Domain.Core.MessageBus
{
    public class SubscriberEventArgs<TMessage> : EventArgs
    {
        public SubscriberEventArgs(TMessage message)
        {
            Message = message;
        }

        public TMessage Message { get; }
    }
}
