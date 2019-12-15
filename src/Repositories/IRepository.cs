using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Bijector.Infrastructure.Types;

namespace Bijector.Infrastructure.Repositories
{
    public interface IRepository<T> where T : IIdentifiable
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        Task<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(int id);

        Task<bool> IsExistsAsync(int id);

        Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T item);

        Task UpdateAsync(int id, T item);

        Task RemoveAsync(int id);
    }
}