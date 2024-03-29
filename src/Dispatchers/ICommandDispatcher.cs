using System.Threading.Tasks;
using Bijector.Infrastructure.Types;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command, IContext context) where TCommand : ICommand;
    }
}