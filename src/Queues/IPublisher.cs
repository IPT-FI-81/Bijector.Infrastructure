using System.Threading.Tasks;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface IPublisher
    {
        Task Send<TCommand>(TCommand command, IContext context) where TCommand : ICommand;

        Task Publish<TEvent>(TEvent @event, IContext context) where TEvent : IEvent;
    }
}