using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Interfaces;
using MongoDB.Driver;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMongoDbService<User> _dbService;

        public UserService(IMongoDbService<User> dbService, IGenericRepository<User> dbService2)
            => _dbService = dbService;

        public async Task<List<User>> GetAsync()
            => await _dbService.GetAsync();

        public async Task<User> GetByIdAsync(string id)
            => await _dbService.GetByIdAsync(id);

        public async Task<User?> GetByEmailAsync(string email)
        {
            var emailFilter = Builders<User>.Filter.Eq("email", email);

            // in case of compound filters //
            //var password = "test";
            //var passwordFilter = Builders<User>.Filter.Eq("password", password);
            //var compoundFilterForTesting = Builders<User>.Filter.And(emailFilter, passwordFilter);

            var users = await _dbService.GetAsync(emailFilter);
            return users?.FirstOrDefault();
        }

        public async Task<User> CreateAsync(User user)
            => await _dbService.CreateAsync(user);

        public Task<bool> UpdateAsync(User user, string id)
            => _dbService.UpdateAsync(user, id);

        public async Task<bool> DeleteAsync(string id)
            => await _dbService.DeleteAsync(id);

    }
}
