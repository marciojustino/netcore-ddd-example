namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    using System;
    using Domain.Core.MessageBus;
    using global::RabbitMQ.Client;

    public class RabbitMQPersistentConnection : IBusConnection<IModel>
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly object semaphore = new();
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory) => _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

        public IModel CreateChannel()
        {
            TryConnect();

            if (!IsConnected())
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _connection.Dispose();
            _disposed = true;
        }

        public bool IsConnected() => _connection is not null && _connection.IsOpen && !_disposed;

        private void TryConnect()
        {
            lock (semaphore)
            {
                if (IsConnected())
                    return;

                _connection = _connectionFactory.CreateConnection();
                _connection.ConnectionShutdown += (sender, ea) => TryConnect();
                _connection.ConnectionBlocked += (sender, ea) => TryConnect();
                _connection.CallbackException += (sender, ea) => TryConnect();
            }
        }
    }
}