using System.Threading.Tasks;
using Infrastructure.Types;
using Infrastructure.Types.Messages;

namespace Infrastructure.Handlers
{
    public interface IEventHandler<TEvent>
            where TEvent : IEvent
    {
        Task Handle(TEvent command, IContext context);
    }
}