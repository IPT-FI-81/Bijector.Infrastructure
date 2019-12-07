using Bijector.Infrastructure.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Bijector.Infrastructure.Repositories
{
    public static class MongoDbExtensions
    {
        public static void AddMongoDb(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.Configure<MongoDbOptions>(configuration.GetSection("MongoDbOptions"));
            collection.AddTransient<MongoClient>();

            var connectionString = configuration.GetSection("MongoDbOptions").GetValue<string>("ConnectionString");
            var dbName = configuration.GetSection("MongoDbOptions").GetValue<string>("DbName");

            collection.AddSingleton<IMongoDatabase>((services) =>
            {
                var client = new MongoClient(connectionString);
                return client.GetDatabase(dbName);
            });
        }

        public static void AddMongoDbRepository<T>(this IServiceCollection collection, string collectionName = null) 
                    where T: IIdentifiable
        {
            collection.AddSingleton<IRepository<T>>((services) => {
                var db = services.GetService<IMongoDatabase>();
                return new MongoRepository<T>(db, collectionName);
            });
        }
    }
}