using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using Bijector.Infrastructure.Types;

namespace Bijector.Infrastructure.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IIdentifiable
    {
        private readonly IMongoCollection<T> collection;

        public MongoRepository(IMongoDatabase database, string collectionName = null)
        {               
            if(string.IsNullOrEmpty(collectionName))
                collectionName = typeof(T).Name;            

            collection = database.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T item)
        {            
            var accountWithMax = await collection.Find(FilterDefinition<T>.Empty).SortByDescending(t => t.Id).FirstOrDefaultAsync();
            if(accountWithMax != null)
                item.Id = accountWithMax.Id + 1;
            else
                item.Id = 1;
            await collection.InsertOneAsync(item);
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate)
        {                      
            return await collection.Find(predicate).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(predicate).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await collection.Find(FilterDefinition<T>.Empty).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {                                 
            return await collection.Find(item => item.Id == id).SingleAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await collection.Find(item => item.Id == id).AnyAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(predicate).AnyAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await collection.DeleteOneAsync(item => item.Id == id);
        }

        public async Task UpdateAsync(int id, T item)
        {            
            await collection.ReplaceOneAsync(old => old.Id == id, item);
        }
    }
}