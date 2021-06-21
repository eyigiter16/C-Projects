using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Reviews.Models;
using Reviews.Mongo.Context;

namespace Reviews.Mongo
{
    public class Repository<T> where T: Document
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public Repository(IContext<T> context, string collection = "reviews")
        {
            _mongoCollection = context.DbSet<T>(collection);
        }

        public void CreateRecord(T record)
        {
            if (record.Id.ToString() == string.Empty)
            {
                record.Id = Guid.NewGuid();
            }
            _mongoCollection.InsertOne(record);
        }

        public void UpdateRecord(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            _mongoCollection.UpdateOne(filter, update);
        }

        public List<T> ReadRecord(FilterDefinition<T> filter)
        {
            return _mongoCollection.Find(filter).ToList();
        }
    }
}