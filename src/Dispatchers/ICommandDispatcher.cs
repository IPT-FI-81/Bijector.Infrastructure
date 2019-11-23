using System.Threading.Tasks;
using Infrastructure.Types.Messages;

namespace Infrastructure.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}