using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Bijector.Infrastructure.Handlers;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Dispatchers
{
    public class HandleCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider services;

        public HandleCommandDispatcher(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task SendAsync<TCommand>(TCommand command, IContext context) where TCommand : ICommand
        {            
            var handler = services.GetService<ICommandHandler<TCommand>>();
            await handler.Handle(command, context);
        }
    }
}