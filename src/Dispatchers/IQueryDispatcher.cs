using System.Threading.Tasks;
using Infrastructure.Types.Messages;

namespace Infrastructure.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> command);
    }
}