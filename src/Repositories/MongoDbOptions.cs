namespace Bijector.Infrastructure.Repositories
{
    public class MongoDbOptions
    {
        public string ConnectionString {get; set; }

        public string DbName { get; set; }
    }
}