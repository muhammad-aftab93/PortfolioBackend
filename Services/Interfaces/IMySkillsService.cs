using Database.Entities;

namespace Services.Interfaces;

public interface IMySkillsService
{
    Task<IEnumerable<MySkills>> GetAsync();
    Task<MySkills> GetByIdAsync(string id);
    Task<MySkills> CreateAsync(MySkills mySkill);
    Task<bool> UpdateAsync(MySkills mySkill, string id);
    Task<bool> DeleteAsync(string id);
}