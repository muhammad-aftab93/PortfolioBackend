using Database.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Services.Interfaces;
using MongoDB.Bson;
using Common;

namespace Database.Services
{
    public class MongoDbService<T> : IMongoDbService<T> where T : new()
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbService()
        {
            var client = new MongoClient(MongoDbSettings.ConnectionURI);
            var database = client.GetDatabase(MongoDbSettings.DatabaseName);
            _collection = database.GetCollection<T>(
                HelperFunctions.EvaluateCollectionName<T>()
                );
        }

        public async Task<List<T>> GetAsync()
            => await _collection.Find<T>(new BsonDocument()).ToListAsync();

        public async Task<T> GetByIdAsync(string id)
            => await _collection.Find<T>(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();

        public async Task<T> CreateAsync(T item)
        {
            await _collection.InsertOneAsync(item);
            return item;
        }

        public async Task<bool> UpdateAsync(T item, string id)
        {
            var result = await _collection.UpdateOneAsync(new BsonDocument("_id", new ObjectId(id)), new BsonDocument("$set", item.ToBsonDocument()));
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
