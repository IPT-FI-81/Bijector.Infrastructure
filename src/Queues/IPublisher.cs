using System.Threading.Tasks;
using Infrastructure.Types;
using Infrastructure.Types.Messages;

namespace Infrastructure.Queues
{
    public interface IPublisher
    {
        Task Send<TCommand>(TCommand command, IContext context) where TCommand : ICommand;

        Task Publish<TEvent>(TEvent @event, IContext context) where TEvent : IEvent;
    }
}