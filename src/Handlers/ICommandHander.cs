using System.Threading.Tasks;
using Infrastructure.Types;
using Infrastructure.Types.Messages;

namespace Infrastructure.Handlers
{
    public interface ICommandHandler<in TCommand>
                where TCommand : ICommand
    {
        Task Handle(TCommand command, IContext context);
    }
}