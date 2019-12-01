using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Bijector.Infrastructure.Queues
{
    public static class RabbitMQExtensions
    {
        public static void AddRabbitMQ(this IServiceCollection services, IConfiguration configuration, INameResolver nameResolver = null)
        {                        
            services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));

            if(nameResolver == null)
            {
                services.AddSingleton<INameResolver, BaseNameResolver>();
            }
            else 
            {
                services.AddSingleton<INameResolver>(nameResolver);
            }


            var options = new RabbitMQOptions();
            configuration.GetSection("RabbitMQOptions").Bind(options);
            var factory = new ConnectionFactory();
            factory.HostName = options.HostName;
            factory.UserName = options.UserName;
            factory.Password = options.Password;

            if(string.IsNullOrEmpty(options.VirtualHost))
                factory.VirtualHost = options.VirtualHost;
            if(options.IsAutomaticRecoveryEnabled.HasValue)
                factory.AutomaticRecoveryEnabled = options.IsAutomaticRecoveryEnabled.Value;
            if(options.IsTopologyRecoveryEnabled.HasValue)
                factory.TopologyRecoveryEnabled = options.IsTopologyRecoveryEnabled.Value;
            if(options.NetworkRecoveryInterval.HasValue)
                factory.NetworkRecoveryInterval = options.NetworkRecoveryInterval.Value;
            if(options.RequestedConnectionTimeout.HasValue)
                factory.RequestedConnectionTimeout = options.RequestedConnectionTimeout.Value;
            if(options.Port.HasValue)
                factory.Port = options.Port.Value;
            if(options.SocketReadTimeout.HasValue)
                factory.SocketReadTimeout = options.SocketReadTimeout.Value;
            if(options.SocketWriteTimeout.HasValue)
                factory.SocketWriteTimeout = options.SocketWriteTimeout.Value;

            var connection = factory.CreateConnection();

            services.AddSingleton<IConnection>(connection);            
            services.AddTransient<IPublisher, RabbitMQPublisher>();
            services.AddTransient<ISubscriber, RabbitMQSubscriber>();
        }

        public static ISubscriber UseRabbitMQ(this IApplicationBuilder builder)
        {
            return new RabbitMQSubscriber(builder.ApplicationServices.GetRequiredService<IServiceProvider>());
        }

    }
}