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

        public async Task<PersonalDetails> SaveAsync(PersonalDetails personalDetails, bool savePicture = false)
        {
            if (string.IsNullOrEmpty(personalDetails.Id))
                return await _repository.CreateAsync(personalDetails);

            var existing = await _repository.GetByIdAsync(personalDetails.Id);
            if (!savePicture)
                personalDetails.Picture = existing.Picture;
            await _repository.UpdateAsync(personalDetails.Id, personalDetails);
            return personalDetails;
        }
    }
}
