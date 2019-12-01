using System.Text;
using Microsoft.Extensions.Configuration;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public class BaseNameResolver : INameResolver
    {
        private readonly IConfiguration configuration;

        public BaseNameResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetExchangeDestinationName<T>(IContext context)
        {
            return context.ResourceTo.ToLowerInvariant();
        }

        public string GetExchangeSourceName<T>()
        {
            return configuration.GetValue<string>("AppName").ToLowerInvariant();
        }

        public string GetRoutingKey<T>()
        {
            var routingKeyBuilder = new StringBuilder();
            if(typeof(T).IsAssignableFrom(typeof(ICommand)))
                routingKeyBuilder.Append("commands.");
            if(typeof(T).IsAssignableFrom(typeof(IEvent)))
                routingKeyBuilder.Append("events.");
            
            routingKeyBuilder.Append(typeof(T).Name.ToLowerInvariant());

            return routingKeyBuilder.ToString();
        }
    }
}