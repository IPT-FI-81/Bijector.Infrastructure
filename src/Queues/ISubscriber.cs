using System.Threading.Tasks;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface ISubscriber
    {
        ISubscriber SubscribeCommand<TCommand>(string routingKey = null) 
                        where TCommand : ICommand;

        ISubscriber SubscribeEvent<TEvent>(string routingKey = null)
                        where TEvent : IEvent;
    }
}