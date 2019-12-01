using System.Threading.Tasks;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;
using System.Text;
using Microsoft.Extensions.Options;

namespace Bijector.Infrastructure.Queues
{
    public class RabbitMQPublisher : IPublisher
    {   
        private readonly IConnection connection;

        private readonly INameResolver nameResolver;

        private readonly RabbitMQOptions options;

        public RabbitMQPublisher(IConnection connection, INameResolver nameResolver, IOptions<RabbitMQOptions> options)
        {
            this.connection = connection;
            this.nameResolver = nameResolver;
            this.options = options.Value;
        }

        public async Task Publish<TEvent>(TEvent @event, IContext context) where TEvent : IEvent
        {
            using (var channel = connection.CreateModel())
            {
                string exchange = nameResolver.GetExchangeName<TEvent>(context);
                string rootingKey = nameResolver.GetRoutingKey<TEvent>();
                channel.ExchangeDeclare(exchange, options.ExchangeType, options.IsExchangeDurable,
                                        options.IsExchangeAutoDelete);
                
                var message = new RabbitMQMessage<TEvent>(@event, context);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                await Task.Factory.StartNew(() =>
                {
                    channel.BasicPublish(exchange, rootingKey, channel.CreateBasicProperties(), body);
                });
            }
        }

        public async Task Send<TCommand>(TCommand command, IContext context) where TCommand : ICommand
        {
            using (var channel = connection.CreateModel())
            {
                string exchange = nameResolver.GetExchangeName<TCommand>(context);
                string rootingKey = nameResolver.GetRoutingKey<TCommand>();
                channel.ExchangeDeclare(exchange, options.ExchangeType, options.IsExchangeDurable,
                                        options.IsExchangeAutoDelete);
                
                var message = new RabbitMQMessage<TCommand>(command, context);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                await Task.Factory.StartNew(() =>
                {
                    channel.BasicPublish(exchange, rootingKey, channel.CreateBasicProperties(), body);
                });
            }
        }
    }
}