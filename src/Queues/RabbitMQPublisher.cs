using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using Infrastructure.Types;
using Infrastructure.Types.Messages;

namespace Infrastructure.Queues
{
    public class RabbitMQPublisher : IPublisher
    {   
        private IBusClient client;

        public RabbitMQPublisher(IBusClient client)
        {
            this.client = client;
        }

        public async Task Publish<TEvent>(TEvent @event, IContext context) where TEvent : IEvent
        {
            await client.PublishAsync(@event, (context) => context.UseMessageContext(context));
        }

        public async Task Send<TCommand>(TCommand command, IContext context) where TCommand : ICommand
        {
            await client.PublishAsync(command, (context) => context.UseMessageContext(context));
        }
    }
}