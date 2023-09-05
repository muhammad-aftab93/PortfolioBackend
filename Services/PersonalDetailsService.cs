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

        public async Task<bool> SaveAsync(PersonalDetails personalDetails)
        {
            if (string.IsNullOrEmpty(personalDetails.Id))
            {
                var result = await _repository.CreateAsync(personalDetails);
                return !string.IsNullOrEmpty(result.Id);
            }
            else
                return await _repository.UpdateAsync(personalDetails.Id, personalDetails);
        }
    }
}
