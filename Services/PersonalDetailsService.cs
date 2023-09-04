using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PersonalDetailsService : IPersonalDetailsService
    {
        private readonly IGenericRepository<PersonalDetails> _repository;

        public PersonalDetailsService(IGenericRepository<PersonalDetails> repository)
            => _repository = repository;

        public async Task<PersonalDetails?> GetAsync()
            => (await _repository.GetAsync()).FirstOrDefault();
    }
}
