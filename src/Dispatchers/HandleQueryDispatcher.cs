using System;
using System.Threading.Tasks;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Dispatchers
{
    public class HandleQueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider services;

        public HandleQueryDispatcher(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, IContext context)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResponse));

            dynamic handler = services.GetService(handlerType);
            return await handler.Handle(query, context);
        }
    }
}