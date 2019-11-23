using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using Infrastructure.Types;
using Infrastructure.Types.Messages;
using Infrastructure.Handlers;

namespace Infrastructure.Queues
{
    public class RabbitMQSubscriber : ISubscriber
    {
        private IServiceProvider services;

        private IBusClient client;

        public RabbitMQSubscriber(IApplicationBuilder builder)
        {
            this.services = builder.ApplicationServices.GetRequiredService<IServiceProvider>();
            client = services.GetService<IBusClient>();
        }

        public async Task SubscribeCommand<TCommand>(string queueName = null) where TCommand : ICommand
        {
            await client.SubscribeAsync<TCommand, IContext>(async (command, context) =>
            {
                var handler = services.GetService<ICommandHandler<TCommand>>();
                await handler.Handle(command, context);
            });
        }

        public async Task SubscribeEvent<TEvent>(string queueName = null) where TEvent : IEvent
        {
            await client.SubscribeAsync<TEvent, IContext>(async (@event, context) =>
            {
                var handler = services.GetService<IEventHandler<TEvent>>();
                await handler.Handle(@event, context);
            });
        }
    }
}