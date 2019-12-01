using System.Threading.Tasks;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Queues
{
    public interface ISubscriber
    {
        Task SubscribeCommand<TCommand>(string queueName = null) 
                        where TCommand : ICommand;

        Task SubscribeEvent<TEvent>(string queueName = null)
                        where TEvent : IEvent;
    }
}