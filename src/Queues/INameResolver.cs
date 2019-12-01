using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface INameResolver
    {
        string GetExchangeDestinationName<T>(IContext context);

        string GetExchangeSourceName<T>();

        string GetRoutingKey<T>();
    }
}