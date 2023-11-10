using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Entities;

namespace Services.Interfaces
{
    public interface IPersonalDetailsService
    {
        Task<PersonalDetails?> GetAsync();
        Task<PersonalDetails> SaveAsync(PersonalDetails personalDetails, bool savePicture = false);
    }
}
