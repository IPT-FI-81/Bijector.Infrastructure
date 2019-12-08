using System.Collections.Generic;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Discovery
{
    public interface IServiceDiscover
    {
        string ResolveServicePath(string name);

        IEnumerable<string> GetActiveServices(string name);       

        IDictionary<string, string[]> GetActiveServices();

        IEnumerable<string> GetPathByTag(string tag);
    }
}