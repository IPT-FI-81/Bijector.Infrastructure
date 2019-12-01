using System.Threading.Tasks;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Handlers
{
    public interface ICommandHandler<in TCommand>
                where TCommand : ICommand
    {
        Task Handle(TCommand command, IContext context);
    }
}