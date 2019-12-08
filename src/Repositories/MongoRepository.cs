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

            BsonClassMap.RegisterClassMap<T>((map)=>
            {
                map.AutoMap();                         
                map.MapIdProperty(c => c.Id).SetIdGenerator(new GuidGenerator());
            });

            collection = database.GetCollection<T>(collectionName);
        }

        public async Task AddAsync(T item)
        {            
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

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await collection.Find(item => item.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await collection.Find(item => item.Id == id).AnyAsync();
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await collection.Find(predicate).AnyAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            await collection.DeleteOneAsync(item => item.Id == id);
        }

        public async Task UpdateAsync(Guid id, T item)
        {            
            await collection.ReplaceOneAsync(old => old.Id == id, item);
        }
    }
}