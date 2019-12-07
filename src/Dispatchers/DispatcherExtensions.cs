using System.Net.Http;
using System.Security.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Bijector.Infrastructure.Dispatchers
{
    public static class DispatcherExtensions
    {
        public static void AddForwardDispatchers(this IServiceCollection collection)
        {            
            collection.AddTransient<ICommandDispatcher, ForwardCommandDispatcher>();
            //collection.AddTransient<IQueryDispatcher, ForwardQueryDispatcher>();            
            collection.AddHttpClient<IQueryDispatcher, ForwardQueryDispatcher>().
            ConfigurePrimaryHttpMessageHandler(services => new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true,
                    SslProtocols = SslProtocols.Tls12
                });
        }

        public static void AddHandleDispatchers(this IServiceCollection collection)
        {            
            collection.AddTransient<ICommandDispatcher, HandleCommandDispatcher>();
            collection.AddTransient<IQueryDispatcher, HandleQueryDispatcher>();            
        }
    }
}