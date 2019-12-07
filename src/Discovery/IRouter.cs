using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Discovery
{
    public interface IRouter
    {
        string ResolveQueryPath(IQuery query, string name);
    }
}