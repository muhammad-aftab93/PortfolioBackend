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
    public class EducationsService : IEducationsService
    {
        private readonly IGenericRepository<Educations> _repository;

        public EducationsService(IGenericRepository<Educations> repository)
            => _repository = repository;

        public async Task<IEnumerable<Educations>> GetAsync()
            => await _repository.GetAsync();

        public async Task<Educations> GetByIdAsync(string id)
            => await _repository.GetByIdAsync(id);

        public async Task<Educations> CreateAsync(Educations education)
            => await _repository.CreateAsync(education);

        public async Task<bool> UpdateAsync(Educations education, string id)
            => await _repository.UpdateAsync(id, education);

        public async Task<bool> DeleteAsync(string id)
            => await _repository.DeleteAsync(id);
    }
}
