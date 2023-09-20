using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEducationsService
    {
        Task<IEnumerable<Educations>> GetAsync();
        Task<Educations> GetByIdAsync(string id);
        Task<Educations> CreateAsync(Educations education);
        Task<bool> UpdateAsync(Educations education, string id);
        Task<bool> DeleteAsync(string id);
    }
}
