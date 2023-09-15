using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services;

public class MyServicesService : IMyServicesService
{
    private readonly IGenericRepository<MyServices> _repository;
    
    public MyServicesService(IGenericRepository<MyServices> repository)
        => _repository = repository;
    
    public async Task<IEnumerable<MyServices>> GetAsync()
        => await _repository.GetAsync();

    public async Task<MyServices> GetByIdAsync(string id)
        => await _repository.GetByIdAsync(id);

    public async Task<MyServices> CreateAsync(MyServices myServices)
        => await _repository.CreateAsync(myServices);

    public async Task<bool> UpdateAsync(MyServices myServices, string id)
        => await _repository.UpdateAsync(id, myServices);

    public async Task<bool> DeleteAsync(string id)
        => await _repository.DeleteAsync(id);
}