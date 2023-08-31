using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IMongoDbService<User> _dbService;

        public UserService(IMongoDbService<User> dbService)
            => _dbService = dbService;

        public async Task<List<User>> GetAsync()
            => await _dbService.GetAsync();

        public async Task<User> GetByIdAsync(string id)
            => await _dbService.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
            => await _dbService.CreateAsync(user);

        public Task<bool> UpdateAsync(User user, string id)
            => _dbService.UpdateAsync(user, id);

        public async Task<bool> DeleteAsync(string id)
            => await _dbService.DeleteAsync(id);

    }
}
