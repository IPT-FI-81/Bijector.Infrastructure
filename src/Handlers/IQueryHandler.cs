using System.Threading.Tasks;
using Bijector.Infrastructure.Types.Messages;

namespace Bijector.Infrastructure.Handlers
{
    public interface IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
    {
         Task<TResponse> Handle(TQuery query);
    }
}