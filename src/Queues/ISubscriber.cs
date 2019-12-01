using System.Threading.Tasks;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface ISubscriber
    {
        ISubscriber SubscribeCommand<TCommand>(string queueName = null) 
                        where TCommand : ICommand;

        ISubscriber SubscribeEvent<TEvent>(string queueName = null)
                        where TEvent : IEvent;
    }
}