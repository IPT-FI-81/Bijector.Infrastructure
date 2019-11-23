using System.Threading.Tasks;
using Infrastructure.Types.Messages;

namespace Infrastructure.Handlers
{
    public interface IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
    {
         Task<TResponse> Handle(TQuery query);
    }
}