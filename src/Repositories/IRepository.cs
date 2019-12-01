using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Bijector.Infrastructure.Types;

namespace Bijector.Infrastructure.Repositories
{
    public interface IRepository<T> where T : IIdentifiable
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> Filter(Func<T, bool> predicate);

        T Find(Func<T, bool> predicate);

        T Find(Guid id);

        void Update(Guid id, T item);

        void Remove(Guid id);
    }
}