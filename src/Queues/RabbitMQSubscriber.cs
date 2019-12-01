using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Bijector.Infrastructure.Types.Messages;
using Bijector.Infrastructure.Handlers;
using System.Text;

namespace Bijector.Infrastructure.Queues
{
    public class RabbitMQSubscriber : ISubscriber
    {
        private readonly IServiceProvider services;

        private readonly IConnection connection;

        private readonly RabbitMQOptions options;

        private readonly INameResolver nameResolver;

        public RabbitMQSubscriber(IApplicationBuilder builder)
        {                                
            this.services = builder.ApplicationServices.GetRequiredService<IServiceProvider>();
            connection = services.GetService<IConnection>();
            options  = services.GetService<IOptions<RabbitMQOptions>>().Value;
            nameResolver = services.GetService<INameResolver>();
        }

        public ISubscriber SubscribeCommand<TCommand>(string routingKey = null) where TCommand : ICommand
        {
            var channel = connection.CreateModel();
            string exchange = nameResolver.GetExchangeSourceName<TCommand>();
            string bindingKey = string.IsNullOrWhiteSpace(routingKey) ? 
                                nameResolver.GetRoutingKey<TCommand>() : routingKey;

            channel.ExchangeDeclare(exchange, options.ExchangeType);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, bindingKey);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) => 
            {
                var json = Encoding.UTF8.GetString(ea.Body);
                var message = JsonConvert.DeserializeObject<RabbitMQMessage<TCommand>>(json);
                var handler = services.GetService<ICommandHandler<TCommand>>();
                await handler.Handle(message.Content, message.Context);   
            };
            
            channel.BasicConsume(queueName, true, consumer);
            
            return this;
        }

        public ISubscriber SubscribeEvent<TEvent>(string routingKey = null) where TEvent : IEvent
        {
            var channel = connection.CreateModel();
            string exchange = nameResolver.GetExchangeSourceName<TEvent>();
            string bindingKey = string.IsNullOrWhiteSpace(routingKey) ? 
                                nameResolver.GetRoutingKey<TEvent>() : routingKey;
            channel.ExchangeDeclare(exchange, options.ExchangeType);
            
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, exchange, bindingKey);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) => 
            {
                var json = Encoding.UTF8.GetString(ea.Body);
                var message = JsonConvert.DeserializeObject<RabbitMQMessage<TEvent>>(json);
                var handler = services.GetService<IEventHandler<TEvent>>();
                await handler.Handle(message.Content, message.Context);   
            };
                
            channel.BasicConsume(queueName, true, consumer);
            
            return this;
        }
    }
}