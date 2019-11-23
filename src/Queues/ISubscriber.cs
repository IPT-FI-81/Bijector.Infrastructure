using System.Threading.Tasks;
using Infrastructure.Types.Messages;

namespace Infrastructure.Queues
{
    public interface ISubscriber
    {
        Task SubscribeCommand<TCommand>(string queueName = null) 
                        where TCommand : ICommand;

        Task SubscribeEvent<TEvent>(string queueName = null)
                        where TEvent : IEvent;
    }
}