using DDDExample.Domain.Core.MessageBus;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDExample.Infra.CrossCutting.MessageBus.RabbitMQ
{
    public class RabbitMQPersistentConnection : IBusConnection<IModel>
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;
        private readonly object semaphore = new object();

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public IModel CreateChannel()
        {
            TryConnect();

            if (!IsConnected())
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");

            return _connection.CreateModel();
        }

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

        public void Dispose()
        {
            if (_disposed)
                return;

            _connection.Dispose();
            _disposed = true;
        }

        public bool IsConnected()
        {
            return _connection is not null && _connection.IsOpen && !_disposed;
        }
    }
}
