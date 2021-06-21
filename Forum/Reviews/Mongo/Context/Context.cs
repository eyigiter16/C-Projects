using MongoDB.Driver;
using Reviews.Models;

namespace Reviews.Mongo.Context
{
    public class Context<T>: IContext<T> where T: Document
    {
        private IMongoDatabase _mongoDatabase;

        public Context(IMongoClient mongoClient, string dbName)
        {
            _mongoDatabase = mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<T> DbSet<T>(string collection)
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
    }
}