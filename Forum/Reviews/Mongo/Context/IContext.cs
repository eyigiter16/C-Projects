using MongoDB.Driver;
using Reviews.Models;

namespace Reviews.Mongo.Context
{
    public interface IContext<T> where T: Document
    {
        IMongoCollection<T>DbSet<T>(string collection);
        
    }
}