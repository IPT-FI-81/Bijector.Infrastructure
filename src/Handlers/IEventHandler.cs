using System.Threading.Tasks;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Handlers
{
    public interface IEventHandler<TEvent>
            where TEvent : IEvent
    {
        Task Handle(TEvent command, IContext context);
    }
}