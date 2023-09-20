using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services;

public class CertificationsService : ICertificationsService
{
    private readonly IGenericRepository<Certifications> _repository;

    public CertificationsService(IGenericRepository<Certifications> repository)
        => _repository = repository;
    
    public async Task<IEnumerable<Certifications>> GetAsync()
        => await _repository.GetAsync();

    public async Task<Certifications> GetByIdAsync(string id)
        => await _repository.GetByIdAsync(id);

    public async Task<Certifications> CreateAsync(Certifications certification)
        => await _repository.CreateAsync(certification);

    public async Task<bool> UpdateAsync(Certifications certification, string id)
        => await _repository.UpdateAsync(id, certification);

    public async Task<bool> DeleteAsync(string id)
        => await _repository.DeleteAsync(id);
}