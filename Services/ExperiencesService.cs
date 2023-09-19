using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;
using Database.Services.Interfaces;

namespace Services
{
    public class ExperiencesService : IExperiencesService
    {
        private readonly IGenericRepository<Experiences> _repository;

        public ExperiencesService(IGenericRepository<Experiences> repository)
            => _repository = repository;

        public async Task<IEnumerable<Experiences>> GetAsync()
            => await _repository.GetAsync();

        public async Task<Experiences> GetByIdAsync(string id)
            => await _repository.GetByIdAsync(id);

        public async Task<Experiences> CreateAsync(Experiences experience)
            => await _repository.CreateAsync(experience);

        public async Task<bool> UpdateAsync(Experiences experience, string id)
            => await _repository.UpdateAsync(id, experience);

        public async Task<bool> DeleteAsync(string id)
            => await _repository.DeleteAsync(id);
    }
}
