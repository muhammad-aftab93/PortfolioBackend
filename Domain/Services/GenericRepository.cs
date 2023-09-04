using Common;
using Database.Services.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Database.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository()
        {
            var client = new MongoClient(MongoDbSettings.ConnectionURI);
            var database = client.GetDatabase(MongoDbSettings.DatabaseName);
            _collection = database.GetCollection<T>(
                HelperFunctions.EvaluateCollectionName<T>()
            );
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            var cursor = await _collection.FindAsync(filter);
            return await cursor.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filterExpression)
        {
            var cursor = await _collection.FindAsync(filterExpression);
            return await cursor.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var cursor = await _collection.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> UpdateAsync(string id, T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await _collection.ReplaceOneAsync(filter, entity);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
