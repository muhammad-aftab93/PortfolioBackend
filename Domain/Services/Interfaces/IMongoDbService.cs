using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Interfaces
{
    public interface IMongoDbService<T>
    {
        Task<List<T>> GetAsync();
        Task<List<T>> GetAsync(FilterDefinition<T> filter);
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T item);
        Task<bool> UpdateAsync(T item, string id);
        Task<bool> DeleteAsync(string id);
    }
}
