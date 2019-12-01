using Bijector.Infrastructure.Types;

namespace Bijector.Infrastructure.Queues
{
    public interface INameResolver
    {
        string GetExchangeDestinationName<T>(IContext context);

        string GetExchangeSourceName<T>();

        string GetRoutingKey<T>();
    }
}