using Database.Entities;

namespace Services.Interfaces;

public interface ICertificationsService
{
    Task<IEnumerable<Certifications>> GetAsync();
    Task<Certifications> GetByIdAsync(string id);
    Task<Certifications> CreateAsync(Certifications certification);
    Task<bool> UpdateAsync(Certifications certification, string id);
    Task<bool> DeleteAsync(string id);
}