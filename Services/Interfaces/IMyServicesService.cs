using Database.Entities;

namespace Services.Interfaces;

public interface IMyServicesService
{
    Task<IEnumerable<MyServices>> GetAsync();
    Task<MyServices> GetByIdAsync(string id);
    Task<MyServices> CreateAsync(MyServices myServices);
    Task<bool> UpdateAsync(MyServices myServices, string id);
    Task<bool> DeleteAsync(string id);
}