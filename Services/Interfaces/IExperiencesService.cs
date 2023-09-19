using Database.Entities;

namespace Services.Interfaces
{
    public interface IExperiencesService
    {
        Task<IEnumerable<Experiences>> GetAsync();
        Task<Experiences> GetByIdAsync(string id);
        Task<Experiences> CreateAsync(Experiences experience);
        Task<bool> UpdateAsync(Experiences experience, string id);
        Task<bool> DeleteAsync(string id);
    }
}
