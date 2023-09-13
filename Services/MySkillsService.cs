using Database.Entities;
using Database.Services.Interfaces;
using Services.Interfaces;

namespace Services;

public class MySkillsService : IMySkillsService
{
    private readonly IGenericRepository<MySkills> _repository;
    
    public MySkillsService(IGenericRepository<MySkills> repository)
        => _repository = repository;
    
    public async Task<IEnumerable<MySkills>> GetAsync()
        => await _repository.GetAsync();

    public async Task<MySkills> GetByIdAsync(string id)
        => await _repository.GetByIdAsync(id);

    public async Task<MySkills> CreateAsync(MySkills mySkill)
        => await _repository.CreateAsync(mySkill);

    public async Task<bool> UpdateAsync(MySkills mySkill, string id)
        => await _repository.UpdateAsync(id, mySkill);

    public async Task<bool> DeleteAsync(string id)
        => await _repository.DeleteAsync(id);
}