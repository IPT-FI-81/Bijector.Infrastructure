using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;
using Bijector.Infrastructure.Discovery;

namespace Bijector.Infrastructure.Dispatchers
{
    public class ForwardQueryDispatcher : IQueryDispatcher
    {
        private readonly HttpClient client;

        private readonly IRouter router;

        public ForwardQueryDispatcher(HttpClient client, IRouter router)
        {
            this.client = client;
            this.router = router;
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, IContext context)
        {
            var uri = router.ResolveQueryPath(query, context.ResourceTo);            
            var responseMessage = await client.GetAsync(uri);
            var json = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(json);
        }
    }
}