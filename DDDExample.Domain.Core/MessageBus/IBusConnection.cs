namespace DDDExample.Domain.Core.MessageBus
{
    using System;

    public interface IBusConnection<TChannel> : IDisposable
    {
        bool IsConnected();

        TChannel CreateChannel();
    }
}