using System.Collections.Generic;
using System.Linq;
using Consul;

namespace Bijector.Infrastructure.Discovery
{
    public class ConsulDiscover : IServiceDiscover
    {
        private readonly IConsulClient client;

        public ConsulDiscover(IConsulClient client)
        {
            this.client = client;
        }

        public IDictionary<string, string[]> GetActiveServices()
        {
            return client.Catalog.Services().Result.Response;
        }

        public IEnumerable<string> GetActiveServices(string name)
        {            
            return client.Catalog.Service(name).Result.Response
                .Select((catalog)=>$"{catalog.ServiceAddress}:{catalog.ServicePort}");
        }

        public IEnumerable<string> GetPathByTag(string tag)
        {
            var result = new List<string>();
            var services = client.Agent.Services().Result.Response;
            foreach (var service in services)
            {
                if(service.Value.Tags.Any(stag => stag == tag))
                {
                    result.Add(service.Value.Address);
                }
            }
            return result;
        }

        public string ResolveServicePath(string name)
        {
            return GetActiveServices(name).FirstOrDefault();
        }
    }
}