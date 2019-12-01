using System;

namespace Bijector.Infrastructure.Queues
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }

        public string VirtualHost { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public TimeSpan? NetworkRecoveryInterval{ get; set; }
 
        public int? Port { get; set; }

        public int? RequestedConnectionTimeout { get; set; }

        public int? SocketReadTimeout { get; set; }

        public int? SocketWriteTimeout { get; set; }

        public bool? IsAutomaticRecoveryEnabled { get; set; }

        public bool? IsTopologyRecoveryEnabled { get; set; }

        public string ExchangeType { get; set; }

        public bool IsExchangeDurable { get; set; }

        public bool IsExchangeAutoDelete { get; set; }
    }
}