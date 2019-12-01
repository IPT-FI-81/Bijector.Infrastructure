using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface INameResolver
    {
        string GetExchangeName<T>(IContext context);

        string GetRoutingKey<T>();
    }
}