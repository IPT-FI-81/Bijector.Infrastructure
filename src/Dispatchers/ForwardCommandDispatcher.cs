using System.Threading.Tasks;
using Bijector.Infrastructure.Queues;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Dispatchers
{
    public class ForwardCommandDispatcher : ICommandDispatcher
    {
        private readonly IPublisher publisher;

        public ForwardCommandDispatcher(IPublisher publisher)
        {
            this.publisher = publisher;
        }

        public async Task SendAsync<TCommand>(TCommand command, IContext context) where TCommand : ICommand
        {
            await publisher.Send(command, context);
        }
    }
}