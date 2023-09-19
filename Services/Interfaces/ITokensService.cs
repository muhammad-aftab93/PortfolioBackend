using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITokensService
    {
        Task<bool> InvalidateTokenAsync(string token);
        Task<bool> IsTokenBlacklistedAsync(string token);
    }
}
